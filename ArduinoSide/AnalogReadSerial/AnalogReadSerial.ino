int RotaryValue = 0;

void setup() {
  Serial.begin(9600);
}

void loop() {
  int sensorValue = analogRead(A0);
  
  if (abs(RotaryValue - sensorValue) > 5)
  {
    RotaryValue = sensorValue;
    Serial.println(sensorValue);
  }
  delay(1);        // delay in between reads for stability
}

