#include <Wire.h>
#include <Adafruit_PWMServoDriver.h> 


Adafruit_PWMServoDriver pwm = Adafruit_PWMServoDriver();

#define SERVOMIN  125 // this is the 'minimum' pulse length count (out of 4096)
#define SERVOMAX  575 // this is the 'maximum' pulse length count (out of 4096)

// our servo # counter
//uint8_t servonum = 0;
const int BUFFER_SIZE = 50;
char buf[BUFFER_SIZE];
char COMMAND;
char PARAM;


void setup() {
  Serial.begin(9600);

  pwm.begin();
  pwm.setPWMFreq(60);  // Analog servos run at ~60 Hz updates
  pwm.setPWM(0, 0, angleToPulse(0) );
  pwm.setPWM(1, 0, angleToPulse(0) );
  pwm.setPWM(2, 0, angleToPulse(0) );
  pwm.setPWM(3, 0, angleToPulse(0) );
  pwm.setPWM(4, 0, angleToPulse(0) );  
 
 }

 void loop()
 {
    if (Serial.available() > 0)
    {

      int rlen = Serial.readBytes(buf, BUFFER_SIZE);
      Serial.print("I received: ");
      for(int i = 0; i < rlen; i=i+2)
      {
        byte command = buf[i];
        byte param = buf[i+1];
        DoCommand(command, param);
      }
    }
 }
 
int angleToPulse(int ang){
   int pulse = map(ang,0, 180, SERVOMIN,SERVOMAX);// map angle of 0 to 180 to Servo min and Servo max 
   Serial.print("\n\nAngle: ");Serial.print(ang);
   Serial.print("\nPulse: ");Serial.println(pulse);
   return pulse;
}


void DoCommand(byte command, byte param){
  switch(command){
      case 0:
      case 1:
      case 2:
      case 3:
      case 4:
      case 5:
      case 6:
      case 7:
      case 8:
      case 9:
      case 10:
      case 11:
      case 12:
      case 13:
      case 14:
      case 15:
        pwm.setPWM(command, 0, angleToPulse((int)param) );
        break;
      case 255:
        delay(100*(int)param);
        break;
  }
}
