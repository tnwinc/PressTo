#include <DigiUSB.h>

// Pin setup
int btnPinGreen = 1;
int btnPinRed = 2;
int swPinDial = 5;

// State tracking
int value = 0;
int RotaryValue = 0;
int redState = 0;
int greenState = 0;

// USB input
String command;
String temp1,temp2;
char inByte;
char carray[6]; 

void setup()
{
  DigiUSB.begin();
  
  pinMode(btnPinGreen, INPUT);
  pinMode(btnPinRed, INPUT);
  pinMode(swPinDial, INPUT);
}

void loop()
{
  // Button Input
  value = digitalRead(btnPinGreen);
  if (value != greenState) {
    if (value == HIGH) {
      DigiUSB.println("BUTTON_GREEN");
    }
  }
  greenState = value;
  
  value = digitalRead(btnPinRed);
  if (value != redState) {
    if (value == HIGH) {
      DigiUSB.println("BUTTON_RED");
    }
  }
  redState = value;
  
  // Dial Input
  int swValue = analogRead(swPinDial);
  if (abs(RotaryValue - swValue) > 5) {
    RotaryValue = swValue;
    DigiUSB.print("SWITCH_");
    DigiUSB.println(swValue);
  }
  
  
  delay(50);  // delay in between reads for stability
}
