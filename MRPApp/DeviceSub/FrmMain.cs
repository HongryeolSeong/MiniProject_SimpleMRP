using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace DeviceSub
{
    public partial class FrmMain : Form
    {
        MqttClient client;
        string connectionString;    // DB연결문자열 | MQTT Brocker address
        ulong lineCount;            // 리치텍스트박스의 라인 번호

        // 대리자 : 스레드상 윈폼 RichTextbox 텍스트 출력 필요
        delegate void UpdateTextCallback(string message);

        Stopwatch sw = new Stopwatch();

        public FrmMain()
        {
            InitializeComponent();
            InitializeAllDate();
        }

        #region 이벤트 작업 영역
        // 연결: 브로커와 연결된 클라이언트를 이용하여 해당 토픽을 구독한다.
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            client.Connect(TxtClientID.Text);   // SUBSCR01의 이름을 가진 클라이언트로 연결
            //RtbSubscr.AppendText(">>>> Client Connected\n");
            UpdateText(">>>> Client Connected" + "\n");
            client.Subscribe(new string[] { TxtSubcriptionTopic.Text },
                new byte[] { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE }); // enum이고 0의 값을 가진다.
            //RtbSubscr.AppendText(">>>> Subscribing to : " + TxtSubcriptionTopic.Text + "\n");
            UpdateText(">>>> Subscribing to : " + TxtSubcriptionTopic.Text + "\n");

            BtnConnect.Enabled = false;
            BtnDisconnect.Enabled = true;
        }

        // 연결 해제: 클라이언트와 브로커를 연결 해제한다.
        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            //RtbSubscr.AppendText(">>>> client Disconnected.\n");
            UpdateText(">>>> client Disconnected.\n");

            BtnConnect.Enabled = true;
            BtnDisconnect.Enabled = false;
        }
        #endregion

        #region 이벤트에 사용되는 메서드
        // 폼 로드시 초기화
        private void InitializeAllDate()
        {
            connectionString = "Data Source=" + TxtConnectionString.Text + ";Initial Catalog=MRP;" +
                "User ID=sa;Password=mssql_p@ssw0rd!";
            lineCount = 0;
            BtnConnect.Enabled = true;
            BtnDisconnect.Enabled = false;
            IPAddress brokerAddress;


            try
            {
                // IP주소를 IPAddress 클래스로 변환
                brokerAddress = IPAddress.Parse(TxtConnectionString.Text);
                client = new MqttClient(brokerAddress);
                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Timer.Enabled = true;
            Timer.Interval = 1000;  // 1초
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LblResult.Text = sw.Elapsed.Seconds.ToString();
            if (sw.Elapsed.Seconds >= 2)
            {
                sw.Stop();
                sw.Reset();

                // TODO: 실제 처리프로세스 실행
                //UpdateText("처리!!\n");
                PrcCorrectDataToDB();
                //ClearData();
            }
        }

        private void Client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            try
            {
                var message = Encoding.UTF8.GetString(e.Message);
                //RtbSubscr.AppendText($">>>> 받은 메세지 : {message}");
                UpdateText($">>>> 받은 메세지 : {message}");

                // message(json) > C#
                var currentData = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);
                PrcInputDataToList(currentData);

                // 0, 1, 2, 3, stop, 0, 1, 2, 3 ...
                sw.Stop();
                sw.Reset();
                sw.Start();
            }
            catch (Exception ex)
            {
                //RtbSubscr.AppendText($">>>> ERROR! : {ex.Message}");
                UpdateText($">>>> ERROR! : {ex.Message}");
            }
        }

        // 여러 데이터 중 최종 데이터만 DB에 입력처리하는 메서드
        private void PrcCorrectDataToDB()
        {
            if (iotData.Count > 0)
            {
                var correctData = iotData[iotData.Count - 1];
                // TODO: DB에 입력
                //UpdateText("DB 처리\n");
                using (var conn = new SqlConnection(connectionString))
                {
                    var PrcResult = correctData["PRC_MSG"] == "OK" ? 1 : 0;
                    string strUpQry = $"UPDATE Process " +
                                      $"   SET PrcResult = '{PrcResult}' " +
                                      $"     , ModDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                                      $"     , ModID = '{"SYS"}' " +
                                      $" WHERE PrcIdx = " +
                                      $" (SELECT TOP 1 PrcIdx FROM Process ORDER BY PrcIdx DESC)";

                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(strUpQry, conn);
                        if (cmd.ExecuteNonQuery() == 1)
                            UpdateText("[DB] 센싱값 Update 성공\n");
                        else
                            UpdateText("[DB] 센싱값 Update 실패\n");
                    }
                    catch (Exception ex)
                    {
                        UpdateText($">>>> DB ERROR!! : {ex.Message}\n");
                    }
                }
            }

            iotData.Clear(); // 데이터 모두 삭제
        }

        // 2개 이상 dictionary 받기 위한 List 선언
        List<Dictionary<string, string>> iotData = new List<Dictionary<string, string>>();

        // 라즈베리파이에서 들어온 메시지를 전역리스트에 입력하는 메서드
        private void PrcInputDataToList(Dictionary<string, string> currentData)
        {
            if (currentData["PRC_MSG"] == "OK" || currentData["PRC_MSG"] == "FAIL")
                iotData.Add(currentData);
        }


        // RtbSubscr.AppendTest() 대체
        private void UpdateText(string message) // 대리자와 파라미터가 같아야한다.
        {
            // RtbSubscr -> UI스레드
            if (RtbSubscr.InvokeRequired)   // 외부 입력이 있는경우 콜백으로 처리하라
            {
                UpdateTextCallback callback = new UpdateTextCallback(UpdateText);
                this.Invoke(callback, new object[] { message });
            }
            else
            {
                lineCount++;
                RtbSubscr.AppendText($"{lineCount} : {message}");
                RtbSubscr.ScrollToCaret(); // 메세지가 쌓일 경우 제일 밑으로 내려준다.
            }
        }
        #endregion

    }
}
