# MES_2021
Project - MES(Manufacturing Execution System) with Raspberry Pi<br/>
실제 분류 공정을 관리 및 모니터링 하는 HMI(Human Machine Interface)를 구현하였습니다.

## 진행 순서
### 1. 메인화면[.xaml.cs 👈 ](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/MRPApp/MRPApp/MainWindow.xaml.cs)
---
###### - Visual Studio를 사용하여 WPF기반으로 구현하였습니다.<br/>
###### - 선택 가능한 메뉴는 공정계획 / 공정모니터링 / 리포트 / 설정 / 종료가 있습니다.
![메인 화면](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/main.png)
<br/>
<br/>
<br/>

### 2. 공정 설정[.xaml.cs 👈 ](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/MRPApp/MRPApp/View/Setting/SettingList.xaml.cs)
---
###### - 공정 설정탭으로 진입하여 실제 공정에 이용될 공장과 설비를 추가합니다.(입력, 수정, 삭제 가능)
![공정 설정](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/%EC%84%A4%EC%A0%95%EC%9E%85%EB%A0%A5%EB%B0%8F%EC%82%AD%EC%A0%9C.gif)
###### - 사용자 입력 오류를 방지하기 위해 유효성 검사 메서드를 이용하여 경고 메세지가 출력되게하였습니다.
![오류메시지](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/%EC%84%A4%EC%A0%95_%EC%9E%85%EB%A0%A5%EA%B2%BD%EA%B3%A0.gif)
###### 해당 코드 👆
```C#
public bool IsValidInputs()
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
```
<br/>
<br/>
<br/>

### 3. 공정 일정 생성[.xaml.cs 👈 ](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/MRPApp/MRPApp/View/Schedule/ScheduleList.xaml.cs)
---
###### - 공정 진행을 위해 해당 날짜 공정을 생성합니다.
###### - 순번(자동생성) / 공장 / 공정일 / 공정로드타임 / 공정시작 및 종료시간 / 공정설비/ 계획수량을 설정합니다.
![공정 일정](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/%EC%8A%A4%EC%BC%80%EC%A4%84%EC%9E%85%EB%A0%A5.gif)
###### - 공정 설정탭과 마찬가지로 이상 입력시 경고메시지가 출력됩니다.
![오류메시지](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/%EC%8A%A4%EC%BC%80%EC%A4%84_%EC%9E%85%EB%A0%A5%EA%B2%BD%EA%B3%A0.gif)
<br/>
<br/>
<br/>

### 4. 공정 진행[.xaml.cs 👈 ](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/MRPApp/MRPApp/View/Process/ProcessView.xaml.cs)
---
#### 공정 진행 순서는 다음과 같습니다.
###### 1. 컬러감지 센서가 연결된 라즈베리파이로 초록색과 빨간색을 구분합니다(실제 공정 과정으로 가정).
![라즈베리파이 회로](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/%ED%9A%8C%EB%A1%9C.gif) <br/><br/>
###### 2. 파이썬 코드를 실행하여 색상 구분 결과를 json형식으로 MQTT(Broker)의 Topic으로 Publish합니다.  [라즈베리파이 구동 코드.py 👈 ](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/pythoncode/check_publish_app.py)
###### MQTT 실행 화면
![MQTT](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/mqtt.gif)
###### factory1/machine1/data/이라는 Topic에 json 값이 넘어옴을 알 수 있습니다. <br/><br/>
###### 3. 해당 Topic을 IoT Device Subsriber에서 Subscribe하여 json 값을 넘겨받은 후 DB에 저장합니다.
###### IoT Device Subsriber 실행 화면  [IoT Device Subsriber 구동 코드.cs 👈 ](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/MRPApp/DeviceSub/FrmMain.cs)
![DeviceSub](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/DeviceSub.gif) <br/><br/>
###### 4. MRP에서는 해당 DB값 및 json값을 읽어들여 모니터링을 진행합니다.
<br/>

#### 모니터링 순서는 다음과 같습니다.
###### 1. 공정모니터링 탭 진입 즉시 MQTT와 연결합니다.
```C#
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
```
###### 2. [IoT Device Subsriber](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/MRPApp/DeviceSub/FrmMain.cs)와 [라즈베리파이 구동 코드.py](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/pythoncode/check_publish_app.py)를 실행한 상태에서 라즈베리파이를 통한 색상 구분을 진행한 후 DB에 저장합니다.
###### 3. MRP는 색상 구분 결과(Green = 'OK', Red = 'FAIL')를 MQTT로 부터 읽어내어 그에 맞는 애니메이션을 실행합니다.
```C#
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
```
###### 4. MRP는 데이터베이스에서 공정 결과 데이터를 불러와 성공/실패수량 및 성공/실패율을 화면에 출력합니다.
```C#
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

    // 공정 실패율
    var prcFailRate = ((double)prcFailAmount / (double)currSchedule.SchAmount) * 100;

    LblPrcOkAmount.Content = $"{prcOKAmount} 개";
    LblPrcFailAmount.Content = $"{prcFailAmount} 개";
    LblPrcOkRate.Content = $"{prcOkRate.ToString("#.##")} %";
    LblPrcFailRate.Content = $"{prcFailRate.ToString("#.##")} %";
}
```

###### 공정 성공(초록색 인식)
![프로세스_성공](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/%ED%94%84%EB%A1%9C%EC%84%B8%EC%8A%A4_%EC%84%B1%EA%B3%B5.gif)
###### 공정 실패(빨간색 인식)
![프로세스_실패](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/%ED%94%84%EB%A1%9C%EC%84%B8%EC%8A%A4_%EC%8B%A4%ED%8C%A8.gif)
<br/>
<br/>
<br/>

### 5. 공정 결과[.xaml.cs 👈 ](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/MRPApp/MRPApp/View/Report/ReportView.xaml.cs)
---
###### NuGet 패키지인 LiveCharts를 활용하여 이전 공정 결과를 확인 할 수 있습니다.
![공정 결과 리포트](https://github.com/HongryeolSeong/MiniProject_SimpleMRP/blob/main/Img/%EB%A6%AC%ED%8F%AC%ED%8A%B8.gif)
<br/>
