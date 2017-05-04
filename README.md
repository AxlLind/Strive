# 2D-Platformer
This is a infinite jumper game. The theme idea is
8-bit graphics and sound. It is made using Unity and written in C#. The main
platform for the game is mobile, both android and iOS, using the gyroscope
accelerometer for controls.

#### How to install
You can try to play a web-version of the game [here](https://axllind.github.io/2D-platformer/), might not work since not all browsers support Unity WebGL

Currently you have to clone the repo and open it in the unity program.
Later we will build Android-apks so you can easily install it on an android device. We might also build an iOS version of the project. All of this is quite easily done in Unity.

#### Testing Strategy
Since we are using unity in this project we are continually testing everything we do. Doing traditional Unit Testing is not optimal for this kind of project, since the methods we write all work within Unity's physics and graphics engine. Our testing will still be extensive, but will consist of us playing the game (and possibly outside testers).

### Project plan
Our workflow consists of first creating a milestone in github for larger features, for example a basic UI. In a milestone we then create issues with smaller features contained in the overall milestone, in this example it might be a mute button for the pause screen.

As we notice bugs and missing features we also create issues for those that we fix when we can. For now what we have in mind in terms of features to implement are:

- Different types of platforms, for example moving platforms and trampolines
- Improve UI and graphics with nicer looking sprites
- Add features to UI such as being able to mute audio
- A global highscore list, implemented using a firebase database
