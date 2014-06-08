#Getting Started

##Step One:
Get a Myo device:
The myo is wearable electronic arm-band to detect gestures without line of sight.
As of the date of the launch the Myo is offering a developer sign up to receive their SDK.  The current Alpha model (which was used for this project) requires a trainer app, and is included in the SDK.  The trainer app is used every time you put the device on.  For more information and ording information goto - https://www.thalmic.com/en/myo/

##Step Two:
Install Unity3D Pro:
Unity 3D is a gaming engine.  3D application development environment.  Myo exposes it’s SDK in this environment.  For more information about unity and downloading a trial goto - http://unity3d.com/unity/download

- ###Step Two A - Install CoherentUI
Unity3D Plugin
Render VF Pages within the 3D world. E.g. Client info panel. Perform JavaScript remote actions from within Unity3D environment.  For more information goto - http://coherent-labs.com/

- ###Step Two B- Install Playmaker
Unity3D Plugin
Unity3D plugin declarative development in Unity3D.  In short is the best visual scripter plugin for Unity.  For more information goto - http://www.hutonggames.com/

##Step Three:
Import Demo-specific Unity3D script
Import to the Unity3D project the C# scripts for salesforce connectivity, coherentUI bindings (remote actions from unity3D), etc.  Links to these files can be found in the gethub project files.

##Step Four:
Configure the Salesforce.com org.
Set up all connected integration points such as WS APIs, Visualforce pages, and Javascript remote methods.  Information for these setups can be found at developer.force.com.

##Training the Myo:
The Alpha version of the myo requires you train the device before it can be use.  Thalmic Labs, the maker of the Myo has provided a simple easy to follow app that the user will need to run every time the device is put on.  This app can be found in the “myo-mac-sdk-alpha6a” folder provided by Thalmic Laps.
