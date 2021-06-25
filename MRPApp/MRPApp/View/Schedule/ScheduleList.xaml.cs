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

        private void LoadControlData()
        {
            var plantCodes = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("PC01")).ToList();
            CboPlantCode.ItemsSource = plantCodes;
            CboGridPlantCode.ItemsSource = plantCodes;

            var facilityIds = Logic.DataAccess.GetSettings().Where(c => c.BasicCode.Contains("FAC1")).ToList();
            CboSchFacilityID.ItemsSource = facilityIds;
        }

        private void InitErrorMessage()
        {
            LblSchDate.Visibility = LblPlantCode.Visibility = LblSchEndTime.Visibility =
                LblSchLoadTime.Visibility = LblSchStartTime.Visibility = LblSchFacilityID.Visibility =
                LblSchAmount.Visibility = Visibility.Hidden;
        }

        private void LoadGridData()
        {
            List<Model.Schedules> list = Logic.DataAccess.GetSchedules();
            this.DataContext = list;
        }

        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //NavigationService.Navigate(new EditUser());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnEditUser_Click : {ex}");
                throw ex;
            }
        }

        private void BtnAddStore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //NavigationService.Navigate(new AddStore());
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnAddStore_Click : {ex}");
                throw ex;
            }
        }

        private void BtnEditStore_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItem == null)
            {
                Commons.ShowMessageAsync("창고수정", "수정할 창고를 선택하세요");
                return;
            }

            try
            {

            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 BtnEditStore_Click : {ex}");
                throw ex;
            }
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        private async void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs() != true) return;

            /*setting.CodeName = TxtCodeName.Text;
            setting.CodeDesc = TxtCodeDesc.Text;
            setting.ModDate = DateTime.Now;
            setting.ModID = "MRP";*/
            var item = new Model.Schedules();
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

        // 입력데이터 검증 메서드
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

            // 공장별로 공정일이 DB값이 있으면 입력되면 안됨
            // ex)PC01001 (수원) 2021-06-24
            try
            {
                if (CboPlantCode.SelectedValue != null && string.IsNullOrEmpty(DtpSchDate.Text))
                {
                    var result = Logic.DataAccess.GetSchedules().Where(s => s.PlantCode.Equals(CboPlantCode.SelectedValue.ToString()))
                .Where(d => d.SchDate.Equals(DateTime.Parse(DtpSchDate.Text))).Count();
                    if (result > 0)
                    {
                        LblSchDate.Visibility = Visibility.Visible;
                        LblSchDate.Text = "해당공장 공정일에 계획이 이미 있습니다";
                        isValid = false;
                    }
                }
                
            }
            catch (Exception ex)
            {

                throw;
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

        public bool IsValidInputs2() // 단위 테스트 위해 private -> public
        {
            var isValid = true;
            InitErrorMessage();

            /*if (string.IsNullOrEmpty(TxtBasicCode.Text))
            {
                LblBasicCode.Visibility = Visibility.Visible;
                LblBasicCode.Text = "코드를 입력하세요";
                isValid = false;
            }
            *//*else if (Logic.DataAccess.GetSettings().Where(s => s.BasicCode.Equals(TxtBasicCode.Text)).Count() > 0) // 중복 코드 방지
            {
                LblBasicCode.Visibility = Visibility.Visible;
                LblBasicCode.Text = "중복 코드가 존재합니다";
                isValid = false;
            }*//*

            if (string.IsNullOrEmpty(TxtCodeName.Text))
            {
                LblCodeName.Visibility = Visibility.Visible;
                LblCodeName.Text = "코드명를 입력하세요";
                isValid = false;
            }*/

            return isValid;
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs() != true) return;

            /*setting.CodeName = TxtCodeName.Text;
            setting.CodeDesc = TxtCodeDesc.Text;
            setting.ModDate = DateTime.Now;
            setting.ModID = "MRP";*/
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

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = DtpSearchDate.Text;
            var list = Logic.DataAccess.GetSchedules().Where(s => s.SchDate.Equals(DateTime.Parse(search))).ToList();
            this.DataContext = list;
        }

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var item = GrdData.SelectedItem as Model.Schedules;
                TxtSchIdx.Text = item.SchIdx.ToString();
                CboPlantCode.SelectedValue = item.PlantCode;
                DtpSchDate.Text = item.SchDate.ToString();
                TxtSchLoadTime.Text = item.SchLoadTime.ToString();
                //TmpSchStartTime.SelectedDateTime = null;
                //TmpSchEndTime.SelectedDateTime = null;
                TmpSchStartTime.SelectedDateTime = new DateTime(item.SchStartTime.Value.Ticks);
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
    }
}
