WearablePack-Myo
================
![Flow Chart](https://cloud.githubusercontent.com/assets/6456976/3211270/82bf990c-ef0c-11e3-88f0-d9a2f4834aad.png)

## Overview
###Description:
Salesforce and Thalmic Labs have come together to create a touchless surgery device. During surgery a Dr. will use a Myo controller and a Salesforce.com intergration to view images (X-Rays), order an additonal post op x-rays, notifly the post op team about the end of surgery, and send notifications to others.
###Architecture:
####Thalmic Labs - Myo (The Controller) https://www.thalmic.com/en/myo/
Value of the Myo - This wearable device uses a bluetooth connection that will allow the user to control the screen.  The specific value for the surgeon will be that because it is touchless it preserves the clean environment needed for surgery.
####Unity 3D (The UI Environment) http://unity3d.com/
Unity 3D is a powerful gaming engine that provide an 3D environment in which to create an application.  The Myo SDK also comes with a plug in for Unity for rapid 3D interface development.
Unity Plugins/Packages
- **Myo Packages**
This is a Unity Package that can be loaded to the Unity project.  The package has all the scripts needed to use the Myo device in scenes.  There is a Myo prefab that is added to the scene that provides the connection to Unity and the Myo.  With in this prefab you can access the gyroscope and accelerometer, and the five preset gestures.  This package is provided in the Myo Alpha 6 SDK

####Playmaker http://www.hutonggames.com/
This is a plugin is a visual scripter.  This provides the ability to quickly prototype and build your Unity scenes without the need to write extensive code.
####Coherent UI http://coherent-labs.com/
This is a plugin for Unity3d that provides a browser access with in your scenes.  This plugin also provides a two way communication between a browser and the Unity3d scene via Javascript
