using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MRPApp.View.Process
{
    /// <summary>
    /// ProcessView.xaml에 대한 상호 작용 논리
    /// 1. 공정계획에서 오늘의 생산계획 일정 불러옴
    /// 2. 없으면 에러표시, 시작버튼을 클릭하지 못하게 만듦
    /// 3. 있으면 오늘의 날짜를 표시, 시작버튼 활성화
    ///    3-1. MQTT Subscription 연결 factory1/machine1/data 확인
    /// 4. 시작버튼 클릭시 새 공정 생성, DB에 입력
    ///    공정코드 : PRC20210628001 (PRC+yyyy+MM+dd+NNN)
    /// 5. 공정처리 애니메이션 시작
    /// 6. 로드 타임 후 애니메이션 중지
    /// 7. 센서링값 리턴될 때까지 대기
    /// 8. 센서링 결과 값에 따라서 생산품 색상 변경
    /// 9. 현재 공정의 DB값 업데이트
    /// 10. 결과 레이블 값 수정/표시
    /// </summary>
    public partial class ProcessView : Page
    {
        // 금일 일정
        private Model.Schedules currSchedule;

        public ProcessView()
        {
            InitializeComponent();
        }

        #region 이벤트 작업 영역
        // 공정모니터링 페이지 로드시 
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                currSchedule = Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(Commons.PLANTCODE))
                    .Where(s => s.SchDate.Equals(DateTime.Parse(today))).FirstOrDefault();
                if (currSchedule == null)
                {
                    await Commons.ShowMessageAsync("공정", "공정계획이 없습니다. 계획일정을 먼저 입력하세요");
                    LblProcessDate.Content = string.Empty;
                    LblSchLoadTime.Content = "None";
                    LblSchAmount.Content = "None";
                    BtnStartProcess.IsEnabled = false;
                    return;
                }
                else
                {
                    // 공정계획 표시
                    MessageBox.Show($"{today} 공정 시작합니다.");
                    LblProcessDate.Content = currSchedule.SchDate.ToString("yyyy년 MM월 dd일");
                    LblSchLoadTime.Content = $"{currSchedule.SchLoadTime} 초";
                    LblSchAmount.Content = $"{currSchedule.SchAmount} 개";
                    BtnStartProcess.IsEnabled = true;

                    UpdateDate();
                    InitConnectMqttBroker(); //공정시작시 MQTT 브로커에 연결
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 ProcessView Loaded : {ex}");
                throw ex;
            }
        }

        private void BtnStartProcess_Click(object sender, RoutedEventArgs e)
        {
            if (InsertProcessData())
                StartAnimation(); // HMI 애니메이션 실행
        }

        // 페이지 닫을시
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            // 자원해제
            //if (client.IsConnected) client.Disconnect();
            //timer.Dispose();
            if (client != null) client.Disconnect();
            if (timer != null)
            {
                timer.Dispose();
            }
        }
        #endregion

        #region 이벤트에 사용되는 메서드
        MqttClient client;
        Timer timer = new Timer();
        Stopwatch sw = new Stopwatch();

        private void InitConnectMqttBroker()
        {
            var brokerAddress = IPAddress.Parse("210.119.12.93"); // MQTT Mosquitto Broker IP;
            client = new MqttClient(brokerAddress);
            client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            client.Connect("Monitor");
            client.Subscribe(new string[] { "factory1/machine1/data/" },
                                new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE });

            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (sw.Elapsed.Seconds >= 2) // 2초 대기 후 일 처리
            {
                sw.Stop();
                sw.Reset();
                if (currentData["PRC_MSG"] == "OK")
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        Product.Fill = new SolidColorBrush(Colors.Green);
                    }));
                }
                else if (currentData["PRC_MSG"] == "FAIL")
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        Product.Fill = new SolidColorBrush(Colors.Red);
                    }));
                }

                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    UpdateDate();
                }));
            }
        }

        private void UpdateDate()
        {
            // 성공수량
            var prcOKAmount = Logic.DataAccess.GetProcesses().Where(p => p.SchIdx.Equals(currSchedule.SchIdx))
                .Where(p => p.PrcResult.Equals(true)).Count();

            // 실패수량
            var prcFailAmount = Logic.DataAccess.GetProcesses().Where(p => p.SchIdx.Equals(currSchedule.SchIdx))
                .Where(p => p.PrcResult.Equals(false)).Count();

            // 공정 성공률
            var prcOkRate = ((double)prcOKAmount / (double)currSchedule.SchAmount) * 100;
            //var prcOkRate = ((double)prcOKAmount / (double)(prcOKAmount + prcFailAmount)) * 100;

            // 공정 실패율
            var prcFailRate = ((double)prcFailAmount / (double)currSchedule.SchAmount) * 100;
            //var prcFailRate = ((double)prcFailAmount / (double)(prcOKAmount + prcFailAmount)) * 100;

            LblPrcOkAmount.Content = $"{prcOKAmount} 개";
            LblPrcFailAmount.Content = $"{prcFailAmount} 개";
            LblPrcOkRate.Content = $"{prcOkRate.ToString("#.##")} %";
            LblPrcFailRate.Content = $"{prcFailRate.ToString("#.##")} %";
        }

        Dictionary<string, string> currentData = new Dictionary<string, string>();

        private void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Message);
            currentData = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);

            if (currentData["PRC_MSG"] == "OK" || currentData["PRC_MSG"] == "FAIL")
            {
                sw.Stop();
                sw.Reset();
                sw.Start();

                StartSensorAnimation();
            }
        }

        private void StartSensorAnimation()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                DoubleAnimation ba = new DoubleAnimation();
                ba.From = 1;    //이미지 보임
                ba.To = 0;      //이미지 보이지 않음
                ba.Duration = TimeSpan.FromSeconds(0.5);
                ba.AutoReverse = true;
                //ba.RepeatBehavior = RepeatBehavior.Forever;

                Sensor.BeginAnimation(Canvas.OpacityProperty, ba);
            }));
        }

        private bool InsertProcessData()
        {
            var item = new Model.Process();
            item.SchIdx = currSchedule.SchIdx;
            item.PrcCD = GetProcessCodeFromDB();
            item.PrcDate = DateTime.Now;
            item.PrcLoadTime = currSchedule.SchLoadTime;
            item.PrcStartTime = currSchedule.SchStartTime;
            item.PrcEndTime = currSchedule.SchEndTime;
            item.PrcFacilityID = Commons.FACILITYID;
            item.PrcResult = true; // 공정 성공으로 시작
            item.RegDate = DateTime.Now;
            item.RegID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetProcess(item);
                if (result == 0)
                {
                    Commons.LOGGER.Error("공정데이터 입력 실패!");
                    Commons.ShowMessageAsync("오류", "공정시작 오류발생, 관리자 문의");
                    return false;
                }
                else
                {
                    Commons.LOGGER.Info("공정데이터 입력!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 : {ex}");
                Commons.ShowMessageAsync("오류", "공정시작 오류발생, 관리자 문의");
                return false;
            }
        }

        // 공정코드 생성
        private string GetProcessCodeFromDB()
        {
            var prefix = "PRC";
            var prePrcCode = prefix + DateTime.Now.ToString("yyyyMMdd"); // PRC20210629
            var resultCode = string.Empty;

            // 이전까지 공정이 없는경우(PRC20210629~) null이 넘어오고
            // PRC20210629001, 002, 003, 004가 있는경우 PRC20210629004가 넘어온다.
            var maxCode = Logic.DataAccess.GetProcesses().Where(p => p.PrcCD.Contains(prePrcCode))
                                .OrderByDescending(p => p.PrcCD).FirstOrDefault();

            if (maxCode == null)
            {
                resultCode = prePrcCode + "001";
            }
            else
            {
                var maxPrcCd = maxCode.PrcCD; // PRC20210629004
                var maxVal = int.Parse(maxPrcCd.Substring(11)) + 1; // 004 --> 4 + 1 --> 5

                resultCode = prePrcCode + maxVal.ToString("000");   // 최대공정코드 +1 값
            }

            return resultCode;
        }

        private void StartAnimation()
        {
            Product.Fill = new SolidColorBrush(Colors.Gray);

            // 기어 애니메이션 속성
            DoubleAnimation da = new DoubleAnimation();
            da.From = 0;
            da.To = 360;
            da.Duration = TimeSpan.FromSeconds(currSchedule.SchLoadTime); // 일정의 계획로드타임
            //da.RepeatBehavior = RepeatBehavior.Forever;   // 무한반복

            RotateTransform rt = new RotateTransform();
            Gear1.RenderTransform = rt;
            Gear1.RenderTransformOrigin = new Point(0.5, 0.5);
            Gear2.RenderTransform = rt;
            Gear2.RenderTransformOrigin = new Point(0.5, 0.5);

            rt.BeginAnimation(RotateTransform.AngleProperty, da);

            // 제품 애니메이션 속성
            DoubleAnimation ma = new DoubleAnimation();
            ma.From = 142;
            ma.To = 537;    // 옮겨지는 x값의 최대값
            ma.Duration = TimeSpan.FromSeconds(currSchedule.SchLoadTime);
            //ma.AccelerationRatio = 0.5;   // 가속
            //ma.AutoReverse = true;        // 왔다갔다 반복

            Product.BeginAnimation(Canvas.LeftProperty, ma);
        }
        #endregion

    }
}
