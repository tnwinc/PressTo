/*
   SDA -> A4
   SCL -> A5
   IRQ -> D2
*/


#include <Wire.h>
#include <mpr121.h>

int key = -1;
int prevKey = -1;
int inc = 0;
long lastTouchTime = 0;
long delayTime = 3000;
long sentWheelIncTime = 0;
long slowInc = 500;

void setup()
{ 
	//  initialize function
  Serial.begin(9600);
  Wire.begin();
  CapaTouch.begin();

  delay(500);
  Serial.println("START"); 
}

void loop()
{
  key=CapaTouch.wheelKey();
  long time = millis();
  long timeDiff = time - lastTouchTime;
  if (key >= 0)
    lastTouchTime = time;
  
  if (prevKey == 20 && key != 20) {
    Serial.println("wheelButton:up");
    prevKey = -1;
  }
  
  if (prevKey != -1 && timeDiff > delayTime)
  {
    prevKey = -1;
    Serial.println("wheel:timeout");
  }
  
  if(key>0 && key<=16){
    if (prevKey > 0)
    {
      inc = (key - prevKey) % 16;
      
      if (inc > 8)
      {
        inc = inc - 16;
      }
      else if (inc < -8)
      {
        inc = inc + 16;
      }
      
      if (inc != 0){
        Serial.print("wheel:");
        if (inc == 2 && time - sentWheelIncTime >= slowInc)
        {
          Serial.println(1);
        }
        else {
          Serial.println(inc);
        }
        sentWheelIncTime = time;
      }
    } else if (prevKey != 20) Serial.println("wheel:touch");
    prevKey = key;
  }
  else if (key == 20 && prevKey != 20){
    Serial.println("wheelButton:down");
    prevKey = key;
  }
  
  delay(100);
}



 



