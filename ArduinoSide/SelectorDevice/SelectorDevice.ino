
// Pin setup
int btnPinGreen = 2;
int btnPinRed = 3;
int swPinDial = 0;

// State tracking
int value = 0;
int RotaryValue = 0;
int redState = 0;
int greenState = 0;

// Serial input
String command;
String temp1,temp2;
char inByte;
char carray[6]; 

void setup()
{
  Serial.begin(9600);
  
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
      Serial.println("BUTTON_GREEN");
    }
  }
  greenState = value;
  
  value = digitalRead(btnPinRed);
  if (value != redState) {
    if (value == HIGH) {
      Serial.println("BUTTON_RED");
    }
  }
  redState = value;
  
  // Dial Input
  int swValue = analogRead(swPinDial);
  if (abs(RotaryValue - swValue) > 5) {
    RotaryValue = swValue;
    Serial.print("SWITCH_");
    Serial.println(swValue);
  }
  
  // Query Commands
  if (Serial.available() > 0) {
    inByte = Serial.read();
    
    // upper/lower letters, numbers, underscore
    if ((inByte >= 65 && inByte <= 90) || (inByte >=97 && inByte <=122) || (inByte >= 48 && inByte <=57) || inByte == 95 ) {
      command.concat(inByte);
    }
  }
  
  if (inByte == 10 || inByte == 13) {
    inByte = 0;
  
    if (command.equalsIgnoreCase("get_position")) {
      Serial.print("SWITCH_");
      Serial.println(swValue);
    } else {
      if (!command.equalsIgnoreCase("")) {
        Serial.println("INVALID_REQUEST");
      }
    }
    command = "";
  }
  
  delay(50);  // delay in between reads for stability
}
