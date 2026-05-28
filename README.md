Cool Rides Production System
Overview

This project simulates how Cool Rides builds cars and minibuses. It shows how vehicles are made step-by-step, from parts to final painting.

The system also demonstrates the use of:

Factory Method Pattern,
Abstract Factory Pattern,
Command Pattern,
Singleton Pattern

What the System Does:
Builds cars (LUX1000) and minibuses (MV500),
Creates vehicle parts (chassis, shell, wheels, trim),
Assembles vehicles in separate assembly lines,
Uses a shared spraybooth to paint vehicles,
Processes orders placed from HQ,
Shows live updates in a GUI

Production Times:
Car Parts - 
Chassis: 2 sec,
Shell: 2 sec,
Wheel: 0.5 sec,
Trim: 1 sec.
Minibus Parts - 
Chassis: 2 sec,
Shell: 3 sec,
Wheel: 0.5 sec,
Trim: 2 sec.
Assembly - 
Car: 2 sec,
Minibus: 3 sec.
Spraybooth - 
Car: 5 sec,
Minibus: 7 sec

Main Parts of the System:
HQ (places orders),
Car Assembly Line,
Minibus Assembly Line,
Car Factory,
Minibus Factory,
Spraybooth (only one allowed at a time),
Vehicle parts (chassis, shell, wheels, trim).

How It Works:
Order is placed in HQ,
Order goes to correct assembly line,
Parts are created using factories,
Vehicle is assembled,
Vehicle goes to spraybooth for painting,
Status is shown in the GUI.

Important -
Assembly lines run at the same time.
Spraybooth can only handle one vehicle at a time.
Orders are processed in order received.
