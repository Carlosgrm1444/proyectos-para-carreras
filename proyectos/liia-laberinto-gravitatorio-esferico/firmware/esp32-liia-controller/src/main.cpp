#include <Arduino.h>

// ESP32-S3 DevKitC-1: cada botón se conecta entre GPIO y GND.
constexpr uint8_t PIN_UP = 4;
constexpr uint8_t PIN_DOWN = 5;
constexpr uint8_t PIN_LEFT = 6;
constexpr uint8_t PIN_RIGHT = 7;
constexpr uint8_t BUTTON_PINS[] = {PIN_UP, PIN_DOWN, PIN_LEFT, PIN_RIGHT};
constexpr uint16_t DEBOUNCE_MS = 20;
constexpr uint16_t REPORT_PERIOD_MS = 20; // 50 Hz

struct DebouncedButton {
  bool stable = false;
  bool sample = false;
  uint32_t changedAt = 0;

  bool update(uint8_t pin, uint32_t now) {
    const bool rawPressed = digitalRead(pin) == LOW;
    if (rawPressed != sample) {
      sample = rawPressed;
      changedAt = now;
    }
    if (stable != sample && now - changedAt >= DEBOUNCE_MS) stable = sample;
    return stable;
  }
};

DebouncedButton buttons[4];
uint32_t sequenceNumber = 0;
uint32_t lastReportAt = 0;

void reportButtons(uint32_t now) {
  if (now - lastReportAt < REPORT_PERIOD_MS) return;
  lastReportAt = now;
  bool values[4];
  for (uint8_t i = 0; i < 4; i++) values[i] = buttons[i].update(BUTTON_PINS[i], now);
  Serial.printf("BTN,%lu,%u,%u,%u,%u\n", ++sequenceNumber,
                values[0], values[1], values[2], values[3]);
}

void setup() {
  Serial.begin(115200);
  for (uint8_t pin : BUTTON_PINS) pinMode(pin, INPUT_PULLUP);
  delay(120);
  Serial.println("HELLO,LIIA,1");
}

void loop() {
  const uint32_t now = millis();
  while (Serial.available()) {
    if (Serial.readStringUntil('\n') == "PING") Serial.println("HELLO,LIIA,1");
  }
  reportButtons(now);
}
