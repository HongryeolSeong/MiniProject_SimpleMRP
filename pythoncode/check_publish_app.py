#library 추가
import time
import datetime as dt
from typing import OrderedDict
import RPi.GPIO as GPIO
import paho.mqtt.client as mqtt
import json

s2 = 23     # Raspberry Pi Pin 23
s3 = 24     # Raspberry Pi Pin 24
out = 25    # Raspberry Pi Pin 25
NUM_CYCLES = 10

dev_id = 'MACHINE01'                    # Publish할 ID
broker_address = '210.119.12.93'        # 중간 매개체인 Broker의 IP
pub_topic = 'factory1/machine1/data/'   # Publish/Subscribe할 TOPIC

# 브로커에 결과 값(json)을 Publish하는 함수
def send_data(param):
    message = ''
    if param == 'GREEN':
        message = 'OK'
    elif param == 'RED':
        message = 'FAIL'
    elif param == 'CONN':
        message = 'CONNECTED'
    else:
        message = 'ERR'

    currtime = dt.datetime.now().strftime('%Y-%m-%d %H:%M:%S.%f')
    
    # json data 생성
    raw_data = OrderedDict()
    raw_data['DEV_ID'] = dev_id
    raw_data['PRC_TIME'] = currtime
    raw_data['PRC_MSG'] = message
    
    # publish 데이터(json)로 변환
    pub_data = json.dumps(raw_data, ensure_ascii=False, indent='\t')
    print(pub_data)

    # mqtt_publish
    client2.publish(pub_topic, pub_data)

# 센싱을 통해 측정 값 리턴하는 함수
def read_value(a2, a3):
    GPIO.output(s2, a2)
    GPIO.output(s3, a3)
    # 센서를 조정할 시간을 준다
    time.sleep(0.3)
    # 전체주기 웨이팅
    #GPIO.wait_for_edge(out, GPIO.FALLING)
    #GPIO.wait_for_edge(out, GPIO.RISING)
    
    start = time.time()         # 센싱 시작 시간
    
    for impulse_count in range(NUM_CYCLES):
        GPIO.wait_for_edge(out, GPIO.FALLING)
        
    end = (time.time() - start) # 현재 시각 - start = 센싱 종료 시간
    
    # 색상 결과 리턴
    return NUM_CYCLES / end
    

# GPIO 세팅 함수
def setup():
    GPIO.setmode(GPIO.BCM)
    GPIO.setup(s2, GPIO.OUT)                           # 센싱 결과를 라즈베리파이로 출력
    GPIO.setup(s3, GPIO.OUT)                           # 센싱 결과를 라즈베리파이로 출력
    GPIO.setup(out, GPIO.IN, pull_up_down=GPIO.PUD_UP) # 센싱 결과를 실행파일로 입력

# 센싱 값을 색상별로 구분하여 브로커에 값을 전송하는 함수(무한반복)
def loop():
    result = ''

    while True:
        red = read_value(GPIO.LOW, GPIO.LOW)    # s2 low, s3 low
        time.sleep(0.1)                         # 0.1초 딜레이
        green = read_value(GPIO.HIGH, GPIO.HIGH)# s2 high, s3 high
        time.sleep(0.1)                         # 0.1초 딜레이
        blue = read_value(GPIO.LOW, GPIO.HIGH)  # s2 low, s3 high

        print('red = {0}, green = {1}, blue = {2}'.format(red, green, blue))
        if (red > green) and (red > blue):
            result = 'RED'
        elif (green > red) and (green > blue):
            result = 'GREEN'
        else:
            result = 'ERR'

        send_data(result)
        time.sleep(1)

# MQTT 초기화
client2 = mqtt.Client(dev_id)   # 브로커에 연결할 클라이언트 생성
client2.connect(broker_address) # 클라이언트와 브로커 연결
print('MQTT Client Connected')  # 연결 확인용 출력문


if __name__=='__main__':        # int main()와 동일
    setup()
    send_data('CONN')           # MQTT 접속 성공 메세지 전송

    try:
        loop()
    except KeyboardInterrupt:
        GPIO.cleanup()