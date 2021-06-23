#library 추가
import time
import RPi.GPIO as GPIO

s2 = 23     # Raspberry Pi Pin 23
s3 = 24     # Raspberry Pi Pin 24
out = 25    # Raspberry Pi Pin 25
NUM_CYCLES = 10

def read_value(a2, a3):
    GPIO.output(s2, a2)
    GPIO.output(s3, a3)
    # 센서를 조정할 시간을 준다
    time.sleep(0.3)
    # 전체주기 웨이팅
    #GPIO.wait_for_edge(out, GPIO.FALLING)
    #GPIO.wait_for_edge(out, GPIO.RISING)
    start = time.time() # 현재 시각
    for impulse_count in range(NUM_CYCLES):
        GPIO.wait_for_edge(out, GPIO.FALLING)
    end = (time.time() - start)
    return NUM_CYCLES / end  # 색상 결과 리턴
    

## GPIO 세팅
def setup():
    #pass ## 함수 내용이 정해지지않은 경우
    GPIO.setmode(GPIO.BCM)
    GPIO.setup(s2, GPIO.OUT)
    GPIO.setup(s3, GPIO.OUT)
    GPIO.setup(out, GPIO.IN, pull_up_down=GPIO.PUD_UP) # 센서결과 받기

## 반복하면서 일처리
def loop():
    result = ''

    while True:
        red = read_value(GPIO.LOW, GPIO.LOW)   # s2 low, s3 low
        time.sleep(0.1) # 0.1초 딜레이
        green = read_value(GPIO.HIGH, GPIO.HIGH)# s2 high, s3 high
        time.sleep(0.1)
        blue = read_value(GPIO.LOW, GPIO.HIGH)

        print('red = {0}, green = {1}, blue = {2}'.format(red, green, blue))
        time.sleep(1)

## int main()
if __name__=='__main__':
    setup()

    try:
        loop()
    except KeyboardInterrupt:
        GPIO.cleanup()