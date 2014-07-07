int adc_key_in;
void setup(){
    pinMode(13,OUTPUT);
    Serial.begin(9600);
}
void loop(){
    adc_key_in = analogRead(0);
    digitalWrite(13,LOW);
    Serial.println(adc_key_in); 
}
