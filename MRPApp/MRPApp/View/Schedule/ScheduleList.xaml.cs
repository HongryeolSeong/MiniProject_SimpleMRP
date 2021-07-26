using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace MRPApp.View.Schedule
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ScheduleList : Page
    {
        public ScheduleList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadControlData();  // 콤보박스 데이터 로딩
                LoadGridData();     // 테이블데이터 그리드 표시
                InitErrorMessage(); // 항목 초기화
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 StoreList Loaded : {ex}");
                throw ex;
            }
        }

        #region 이벤트 작업 영역
        // 셀 선택시 처리되는 이벤트
        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ClearInput();
            try
            {
                var item = GrdData.SelectedItem as Model.Schedules;
                TxtSchIdx.Text = item.SchIdx.ToString();
                CboPlantCode.SelectedValue = item.PlantCode;
                DtpSchDate.Text = item.SchDate.ToString();
                TxtSchLoadTime.Text = item.SchLoadTime.ToString();
                if (item.SchStartTime != null)
                    TmpSchStartTime.SelectedDateTime = new DateTime(item.SchStartTime.Value.Ticks);
                if (item.SchEndTime != null)
                    TmpSchEndTime.SelectedDateTime = new DateTime(item.SchEndTime.Value.Ticks);
                CboSchFacilityID.SelectedValue = item.SchFacilityID;
                NudSchAmount.Value = item.SchAmount;
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 {ex}");
                ClearInput();
            }
        }

        // 신규: 모든 텍스트박스를 초기화한다.
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        // 입력: 텍스트에 입력된 내용 및 설정 내용 그대로 DB에 새로운 공정일정을 추가한다.
        private async void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs() != true) return;

            var item = new Model.Schedules();
            item.PlantCode = CboPlantCode.SelectedValue.ToString();
            item.SchDate = DateTime.Parse(DtpSchDate.Text);
            item.SchLoadTime = int.Parse(TxtSchLoadTime.Text);
            if (TmpSchStartTime.SelectedDateTime != null)
                item.SchStartTime = TmpSchStartTime.SelectedDateTime.Value.TimeOfDay;
            if (TmpSchEndTime.SelectedDateTime != null)
                item.SchEndTime = TmpSchEndTime.SelectedDateTime.Value.TimeOfDay;
            item.SchFacilityID = CboSchFacilityID.SelectedValue.ToString();
            item.SchAmount = (int)NudSchAmount.Value;

            item.ModDate = DateTime.Now;
            item.ModID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSchedule(item);
                if (result == 0)
                {
                    Commons.LOGGER.Error("데이터 수정시 오류발생");
                    await Commons.ShowMessageAsync("오류", "데이터 수정 실패!");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 수정 성공 : {item.SchIdx}");
                    ClearInput();
                    LoadGridData();
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 {ex}");
            }
        }

        // 수정: 선택된 셀의 공정일정에 대해 수정한다.
        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidUpdates() != true) return;

            var item = GrdData.SelectedItem as Model.Schedules;
            item.PlantCode = CboPlantCode.SelectedValue.ToString();
            item.SchDate = DateTime.Parse(DtpSchDate.Text);
            item.SchLoadTime = int.Parse(TxtSchLoadTime.Text);
            item.SchStartTime = TmpSchStartTime.SelectedDateTime.Value.TimeOfDay;
            item.SchEndTime = TmpSchEndTime.SelectedDateTime.Value.TimeOfDay;
            item.SchFacilityID = CboSchFacilityID.SelectedValue.ToString();
            item.SchAmount = (int)NudSchAmount.Value;

            item.ModDate = DateTime.Now;
            item.ModID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSchedule(item);
                if (result == 0)
                {
                    Commons.LOGGER.Error("데이터 수정시 오류발생");
                    await Commons.ShowMessageAsync("오류", "데이터 수정 실패!");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 수정 성공 : {item.SchIdx}");
                    ClearInput();
                    LoadGridData();
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 {ex}");
            }
        }

        // 검색: TxtSearch의 내용을 검색 후 표시한다.
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = DtpSearchDate.Text;
            var list = Logic.DataAccess.GetSchedules().Where(s => s.SchDate.Equals(DateTime.Parse(search))).ToList();
            this.DataContext = list;
        }
        #endregion

        #region 이벤트에 사용되는 메서드
        // 공정일정 데이터 갱신
        private void LoadGridData()
        {
            List<Model.Schedules> list = Logic.DataAccess.GetSchedules();
            this.DataContext = list;
        }

        // 공정일정 공장, 설비 데이터 갱신
        private void LoadControlData()
        {
            var plantCodes = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("PC01")).ToList();
            CboPlantCode.ItemsSource = plantCodes;
            CboGridPlantCode.ItemsSource = plantCodes;

            var facilityIds = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("FAC1")).ToList();
            CboSchFacilityID.ItemsSource = facilityIds;
        }

        // 에러메시지 Label 초기화
        private void InitErrorMessage()
        {
            LblSchDate.Visibility = LblPlantCode.Visibility = LblSchEndTime.Visibility =
                LblSchLoadTime.Visibility = LblSchStartTime.Visibility = LblSchFacilityID.Visibility =
                LblSchAmount.Visibility = Visibility.Hidden;
        }

        // 입력데이터 검증 메서드(입력 버튼용)
        public bool IsValidInputs() // 단위 테스트 위해 private -> public
        {
            var isValid = true;
            InitErrorMessage();

            if (CboPlantCode.SelectedValue == null)
            {
                LblPlantCode.Visibility = Visibility.Visible;
                LblPlantCode.Text = "공장을 선택하세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(DtpSchDate.Text))
            {
                LblSchDate.Visibility = Visibility.Visible;
                LblSchDate.Text = "공정일을 입력하세요";
                isValid = false;
            }

            if (CboPlantCode.SelectedValue != null && !string.IsNullOrEmpty(DtpSchDate.Text))
            {
                // 공장별로 공정일일 DB값이 있으면 입력되면 안됨
                // PC01001 (수원) 2021-06-24 
                var result = Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(CboPlantCode.SelectedValue.ToString()))
                    .Where(d => d.SchDate.Equals(DateTime.Parse(DtpSchDate.Text))).Count();
                if (result > 0)
                {
                    LblSchDate.Visibility = Visibility.Visible;
                    LblSchDate.Text = "해당공장 공정일에 계획이 이미있습니다";
                    isValid = false;
                }
            }

            if (string.IsNullOrEmpty(TxtSchLoadTime.Text))
            {
                LblSchLoadTime.Visibility = Visibility.Visible;
                LblSchLoadTime.Text = "로드타임을 입력하세요";
                isValid = false;
            }

            if (CboSchFacilityID.SelectedValue == null)
            {
                LblSchFacilityID.Visibility = Visibility.Visible;
                LblSchFacilityID.Text = "공장설비를 선택하세요";
                isValid = false;
            }

            if (NudSchAmount.Value <= 0)
            {
                LblSchAmount.Visibility = Visibility.Visible;
                LblSchAmount.Text = "게획수량은 0개 이상입니다";
            }

            return isValid;
        }

        // 입력데이터 검증 메서드(수정 버튼용)
        public bool IsValidUpdates()
        {
            var isValid = true;
            InitErrorMessage();

            if (CboPlantCode.SelectedValue == null)
            {
                LblPlantCode.Visibility = Visibility.Visible;
                LblPlantCode.Text = "공장을 선택하세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(DtpSchDate.Text))
            {
                LblSchDate.Visibility = Visibility.Visible;
                LblSchDate.Text = "공정일을 입력하세요";
                isValid = false;
            }

            if (string.IsNullOrEmpty(TxtSchLoadTime.Text))
            {
                LblSchLoadTime.Visibility = Visibility.Visible;
                LblSchLoadTime.Text = "로드타임을 입력하세요";
                isValid = false;
            }

            if (CboSchFacilityID.SelectedValue == null)
            {
                LblSchFacilityID.Visibility = Visibility.Visible;
                LblSchFacilityID.Text = "공장설비를 선택하세요";
                isValid = false;
            }

            if (NudSchAmount.Value <= 0)
            {
                LblSchAmount.Visibility = Visibility.Visible;
                LblSchAmount.Text = "게획수량은 0개 이상입니다";
            }

            return isValid;
        }

        // 내용을 신규/입력/수정/삭제 후 텍스트박스 초기화
        private void ClearInput()
        {
            TxtSchIdx.Text = "";
            CboPlantCode.SelectedItem = null;
            DtpSchDate.Text = "";
            TxtSchLoadTime.Text = "";
            TmpSchStartTime.SelectedDateTime = null;
            TmpSchEndTime.SelectedDateTime = null;
            CboSchFacilityID.SelectedItem = null;
            NudSchAmount.Value = 0;

            CboPlantCode.Focus();
        }
        #endregion

    }
}
