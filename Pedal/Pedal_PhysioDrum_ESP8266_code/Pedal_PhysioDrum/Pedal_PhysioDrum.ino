/* 
Pedal - PhysioDrum
Created by: Rômulo Vieira & Marcelo Rocha
Fluminense Federal University (UFF) - Brazil 
GNU General Public License v3

Buy me a coffee --> paypal: romulo_vieira96@yahoo.com.br
*/

// Include Libraries
#include <Wire.h>
#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
#include <OSCMessage.h>

// Constants
#define touch_left_pedal 5  // Touch sensor pin (D1 pin on ESP8266)
#define touch_right_pedal 16 // Touch sensor pin (D0 pin on ESP8266)
const IPAddress outIp(255,255,255,255); // Client computer IP 
const unsigned int outPort = 9998; // Client computer port 
const unsigned int localPort = 2390; // Local port to listen to OSC packets

// Wi-Fi credentials
char ssid[] = "network_name"; // EDIT: Network name
char pass[] = "networki_pin"; // EDIT: Network password
WiFiUDP Udp; // Instance that allows sending and receiving packets using UDP packet

// Variables to track pedal states
bool left_pedal_pressed = false;
bool right_pedal_pressed = false;

void setup() {
  // Baud Rate
  Serial.begin(9600); 

  // Connecting to Wi-Fi Network
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, pass);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nWiFi connected");
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());

  Serial.println("Starting UDP");
  Udp.begin(localPort);
  Serial.print("Local port: ");
  Serial.println(Udp.localPort());
  
  // Configure pedal pins as input
  pinMode(touch_left_pedal, INPUT);
  pinMode(touch_right_pedal, INPUT);
}

void loop() {
  // Left Pedal control logic
  if (digitalRead(touch_left_pedal) == HIGH && !left_pedal_pressed) { // Detect press
    left_pedal_pressed = true;
    Serial.println("Left Pedal");
    
    OSCMessage leftpedal("/hihat");
    leftpedal.add(1); // Send value 1 for left pedal press
    Udp.beginPacket(outIp, outPort);
    leftpedal.send(Udp);
    Udp.endPacket();
    leftpedal.empty();
  } else if (digitalRead(touch_left_pedal) == LOW && left_pedal_pressed) {
    // Reset when pedal is released
    left_pedal_pressed = false;
  }

  // Right Pedal control logic
  if (digitalRead(touch_right_pedal) == HIGH && !right_pedal_pressed) { // Detect press
    right_pedal_pressed = true;
    Serial.println("Right Pedal");
    
    OSCMessage rightpedal("/drumbass");
    rightpedal.add(1); // Send value 1 for right pedal press
    Udp.beginPacket(outIp, outPort);
    rightpedal.send(Udp);
    Udp.endPacket();
    rightpedal.empty();
  } else if (digitalRead(touch_right_pedal) == LOW && right_pedal_pressed) {
    // Reset when pedal is released
    right_pedal_pressed = false;
  }

  delay(10); // Small delay to debounce
}
