using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MRPApp.View.Setting
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingList : Page
    {
        public SettingList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadGridData();
                InitErrorMessage();
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
            try
            {
                var setting = GrdData.SelectedItem as Model.Settings;
                TxtBasicCode.Text = setting.BasicCode;
                TxtCodeName.Text = setting.CodeName;
                TxtCodeDesc.Text = setting.CodeDesc;

                TxtBasicCode.IsReadOnly = true;
                TxtBasicCode.Background = new SolidColorBrush(Colors.LightGray);
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

        // 입력: 텍스트에 입력된 내용대로 DB에 새로운 공정설정을 추가한다.
        private async void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs() != true) return;

            var setting = new Model.Settings();
            setting.BasicCode = TxtBasicCode.Text;
            setting.CodeName = TxtCodeName.Text;
            setting.CodeDesc = TxtCodeDesc.Text;
            setting.RegDate = DateTime.Now;
            setting.RegID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSettings(setting);
                if (result == 0)
                {
                    Commons.LOGGER.Error("데이터 입력시 오류발생");
                    await Commons.ShowMessageAsync("오류", "데이터 입력 실패!");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 입력 성공 : {setting.BasicCode}");
                    ClearInput();
                    LoadGridData();
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 {ex}");
            }
        }

        // 수정: 선택된 셀의 공정설정에 대해 수정한다.
        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs2() != true) return;

            var setting = GrdData.SelectedItem as Model.Settings;
            setting.CodeName = TxtCodeName.Text;
            setting.CodeDesc = TxtCodeDesc.Text;
            setting.ModDate = DateTime.Now;
            setting.ModID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSettings(setting);
                if (result == 0)
                {
                    Commons.LOGGER.Error("데이터 수정시 오류발생");
                    await Commons.ShowMessageAsync("오류", "데이터 수정 실패!");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 수정 성공 : {setting.BasicCode}");
                    ClearInput();
                    LoadGridData();
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외 발생 {ex}");
            }
        }

        // 삭제: 선택된 셀의 공정설정을 지운다.
        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var setting = GrdData.SelectedItem as Model.Settings;

            if (setting == null)
            {
                await Commons.ShowMessageAsync("삭제", "삭제할 코드를 선택하세요");
                return;
            }
            else
            {
                try
                {
                    var result = Logic.DataAccess.DelSettings(setting);

                    if (result == 0)
                    {
                        Commons.LOGGER.Error("데이터 삭제시 오류발생");
                        await Commons.ShowMessageAsync("오류", "데이터 삭제 실패!");
                    }
                    else
                    {
                        Commons.LOGGER.Info($"데이터 삭제 성공 : {setting.BasicCode}");
                        ClearInput();
                        LoadGridData();
                    }
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외 발생 {ex}");
                }
            }
        }

        // 검색: TxtSearch의 내용을 검색 후 표시한다.
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = TxtSearch.Text.Trim();
            var settings = Logic.DataAccess.GetSettings().Where(s => s.CodeName.Contains(search)).ToList();
            this.DataContext = settings;
        }

        // TxtSearch에 내용을 타이핑한 후 엔터 누를시 검색버튼을 클릭한 것과 같게 한다.
        private void TxtSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                BtnSearch_Click(sender, e);
        }
        #endregion

        #region 이벤트에 사용되는 메서드
        // 공정설정 데이터 갱신
        private void LoadGridData()
        {
            List<Model.Settings> settings = Logic.DataAccess.GetSettings();
            this.DataContext = settings;
        }

        // 에러메시지 Label 초기화
        private void InitErrorMessage()
        {
            LblBasicCode.Visibility = LblCodeDesc.Visibility = LblCodeName.Visibility = Visibility.Hidden;
        }

        // 입력데이터 검증 메서드(입력 버튼용)
        public bool IsValidInputs() // 단위 테스트 위해 private -> public
        {
            var isValid = true;
            InitErrorMessage();

            if (string.IsNullOrEmpty(TxtBasicCode.Text))
            {
                LblBasicCode.Visibility = Visibility.Visible;
                LblBasicCode.Text = "코드를 입력하세요";
                isValid = false;
            }
            else if (Logic.DataAccess.GetSettings().Where(s => s.BasicCode.Equals(TxtBasicCode.Text)).Count() > 0) // 중복 코드 방지
            {
                LblBasicCode.Visibility = Visibility.Visible;
                LblBasicCode.Text = "중복 코드가 존재합니다";
                isValid = false;
            }

            if (string.IsNullOrEmpty(TxtCodeName.Text))
            {
                LblCodeName.Visibility = Visibility.Visible;
                LblCodeName.Text = "코드명를 입력하세요";
                isValid = false;
            }

            return isValid;
        }

        // 입력데이터 검증 메서드(수정 버튼용)
        public bool IsValidInputs2() // 단위 테스트 위해 private -> public
        {
            var isValid = true;
            InitErrorMessage();

            if (string.IsNullOrEmpty(TxtCodeName.Text))
            {
                LblCodeName.Visibility = Visibility.Visible;
                LblCodeName.Text = "코드명를 입력하세요";
                isValid = false;
            }

            return isValid;
        }

        // 내용을 신규/입력/수정/삭제 후 텍스트박스 초기화
        private void ClearInput()
        {
            TxtBasicCode.IsReadOnly = false;
            TxtBasicCode.Background = new SolidColorBrush(Colors.White);

            TxtBasicCode.Text = TxtCodeName.Text = TxtCodeDesc.Text = string.Empty;
            TxtBasicCode.Focus();
        }
        #endregion

    }
}
