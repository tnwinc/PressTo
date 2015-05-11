PressTo
=======

Pronounced \'pres-tō\\. This repository contains details on how to make an Adruino powered switch box to select and launch an application. "Press To ..."

# Parts
* Arduino (Uno, Nano, etc., tested with the ATmega328 microcontroller)
* Rotary Switch (12 position or less is currently tested) http://www.robotmesh.com/rotary-switch-module-v1
* Green Button (http://amzn.com/B0094GRZPE)
* Red Button (http://amzn.com/B0097A8PK6)
* Plastic Printer (for the housing)

# Hardware
Hopefully a schematic of our design will be provided, but it is not yet available...

## Microcontroller code
Look up the code in ArduinoSide/SelectorDevice/SelectorDevice.ino. The microcontroller code is hooked to A0 (for the Rotary analog input), D2 (green button), D3 (red button), and power (5V) and ground are run as needed to all three components. 

On a button press, the appropriate signal is sent, namely BUTTON_GREEN or BUTTON_RED, but currently a command is only sent on the down press (there is not a separate signal for ON or OFF).

The Rotary switch sends SWITCH_####. The number can be from 0 - 1023, but 1023 means that no connection was made and that value should be screened by the software (it tends to event SWITCH_1023 when switching from one position to another).

# Software
PressTo.sln is the solution, which is located in ComputerSide.

Hardware.csproj contains everything to encapsulate the serial connection and raise events for button clicks and rotary changes.

Switcher.csproj handles the software UI, which listens to signals from the device and responds appropriately. It will hide after the configured timeout (5 seconds) and auto configures the device the first time it is run. Once it is configured, select a position and press the green button to configure an action. An action can launch an executable with parameters (if desired). The Name field shows up when the item is in the Next or Previous switch position state, where as the Description shows up when the item is selected. The image will appear for the Previous, Current, or Next positions of the switch.

# Special Thanks
* The Network - for supplying the hardware and allocating time to the project
* Mit Raval - for suggesting the name
* Tej Gandham - for contributions to the node server and Slack integration
