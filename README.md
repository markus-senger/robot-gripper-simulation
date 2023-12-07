# robot-gripper-simulation
In this university project, a robot gripper system was developed and simulated to efficiently manipulate cubes of different sizes. The primary goal was to design a versatile robotic tool that could adapt to various cube dimensions. (Exercise for university)

https://github.com/markus-senger/robot-gripper-simulation/assets/77236323/e2c01b42-34a1-495a-872e-024d1d2462c8


## Mobile Control App
In addition, a simple mobile control app was developed using Flutter, which displays the current status of the robot and gripper. For this purpose, the Firestore database from Firebase was utilized, enabling mobile monitoring worldwide.

It is important to note, however, that the necessary credentials for accessing Firebase are not included in this repository due to privacy reasons.

For integrating the Firestore database in Flutter, Firebase provides a suitable SDK, while for integration in Unity for Windows, the "Google.Cloud.Firestore" NuGet package was manually installed.

Please note that due to data transmission via a Google database, minor delays may occur. This app has been developed to showcase the capabilities of Firebase; however, it is worth noting that Firebase may be less suitable for real-time transmission in an industrial environment.


https://github.com/markus-senger/robot-gripper-simulation/assets/77236323/6b8d62bd-24af-4c3f-bf1d-a257c69c6ea6


## Unity Application Download Information
The Unity application can be downloaded under [Releases](https://github.com/markus-senger/robot-gripper-simulation/releases/tag/v1-2023-12-03), but please note that the control app is not included in this download.


