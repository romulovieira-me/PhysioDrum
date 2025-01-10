/* 
PhysioDrum
Created by: Rômulo Vieira & Marcelo Rocha
Fluminense Federal University (UFF) - Brazil 
GNU General Public License v3
*/

// Include Libraries
#include <ESP8266WiFi.h>
#include <WiFiUdp.h>
#include <OSCMessage.h>

// Vibration motor PIN
#define VIBRATION_PIN D1

// Constants
#define touch 14 // Touch sensor pin (D5 pin on ESP8266)
const IPAddress outIp(255, 255, 255, 255); // Client computer IP
const unsigned int outPort = 9999;        // Client computer port
const unsigned int localPort = 2391;      // Local port to listen to OSC packets

// Wi-fi Setup
char ssid[] = "network_name";       // EDIT: Network name
char pass[] = "network_pin";    // EDIT: Network password
WiFiUDP Udp;                  

// Touch sensor status variable
int value = 0;

// Function to handle received OSC messages
void handleOSCMessage(OSCMessage &msg) {
  // Check if OSC address is "/ledright"
  if (msg.fullMatch("/ledright")) {
    if (msg.isInt(0)) {
      int ledState = msg.getInt(0);
      Serial.print("OSC message received on /ledright with value: ");
      Serial.println(ledState);

      // Vibrate the motor if the received value is 1
      if (ledState == 1) {
        Serial.println("Activating vibration motor for 1 second...");
        digitalWrite(VIBRATION_PIN, HIGH); // Turn ON the vibration motor
        delay(1000);                       // Vibrates for 1 second
        digitalWrite(VIBRATION_PIN, LOW);  // Turno OFF the vibration motor
        Serial.println("Vibration motor disabled");
      }
    } else {
      Serial.println("Received OSC message does not contain a valid integer value");
    }
  } else {
    // OSC address not recognized
    Serial.print("Unknown OSC address: ");
    Serial.println(msg.getAddress());
  }
}

void setup() {
  // Serial setup
  Serial.begin(9600);

  // Wi-Fi info
  Serial.println();
  Serial.println("Connecting to Wi-Fi...");
  WiFi.begin(ssid, pass);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println();
  Serial.println("WiFi connected");
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());

  // Starting UDP
  Serial.println("Starting UDP...");
  Udp.begin(localPort);
  Serial.print("Local UDP port: ");
  Serial.println(Udp.localPort());

  // PIN setup
  pinMode(touch, INPUT);
  pinMode(VIBRATION_PIN, OUTPUT);
  digitalWrite(VIBRATION_PIN, LOW); // Starting vibration motor in low mode
}

void loop() {
  // Touch sensor control 
  value = digitalRead(touch); // Reading touch sensor
  Serial.print("Touch sensor state: ");
  Serial.println(value); // 

  // Send touch button state via OSC
  OSCMessage touchMsg("/value");
  touchMsg.add(value);
  Udp.beginPacket(outIp, outPort);
  touchMsg.send(Udp);
  Udp.endPacket();
  touchMsg.empty();

  // Checks for incoming UDP packets
  int packetSize = Udp.parsePacket();
  if (packetSize > 0) {
    OSCMessage msg; // Creates an instance of OSCMessage
    while (packetSize--) {
      msg.fill(Udp.read()); // Fill the message with the received data
    }

    // Process the message if there are no errors
    if (!msg.hasError()) {
      handleOSCMessage(msg); // Call the function to handle the message
    } else {
      Serial.println("Error processing OSC message");
    }
  }

  delay(100); 
}
