WearablePack-Myo
================
![Flow Chart](https://cloud.githubusercontent.com/assets/6456976/3211270/82bf990c-ef0c-11e3-88f0-d9a2f4834aad.png)

## Overview
###Description:
Salesforce and Thalmic Labs have come together to create a touchless surgery device. During surgery a Dr. will use a Myo controller and a Salesforce.com intergration to view images (X-Rays), order an additonal post op x-rays, notifly the post op team about the end of surgery, and send notifications to others.
###Architecture:
There are three architectural componnets to the solution:

-  **The Connected Device - Thalmic Labs' Myo** -  https://www.thalmic.com/en/myo/

The Myo is a wearable device that is able to detect hand/spacial gestures and uses a bluetooth connection to allow the wearer to control an application.  In this usecase, the specific value for the surgeon is the touchless control, which preserves the clean environment needed for surgery.
- **The connected App - Unity 3D**  http://unity3d.com/

Unity 3D is a powerful platform for building applications iwth 3D interfaces. Traditionally, its origin is deeply rooted in game development. However, icreasingly, Unity3D is being used as the development platform of choice by 3D and spacially aware wearable devices such as the Myo. The Myo SDK also comes with a plug in for Unity for rapid 3D interface development.
- **The Salesforce1 Platform** - https://developer.salesforce.com/platform/overview

The Salesforce1 platform transforms the Myo Sergeon App in to a connected app, feeding it with data (patient info, Xrays, procedure details) as well as exposing business processes and workflows directly within the app.


###Required Unity Plugins
**Playmaker** http://www.hutonggames.com/
This is a plugin is a visual scripter.  This provides the ability to quickly prototype and build your Unity scenes without the need to write extensive code.

**Coherent UI** http://coherent-labs.com/
This is a plugin for Unity3d that provides a browser access with in your scenes.  This plugin also provides a two way communication between a browser and the Unity3d scene via Javascript

**Myo Packages**
This is a Unity Package that can be loaded to the Unity project.  The package has all the scripts needed to use the Myo device in scenes.  There is a Myo prefab that is added to the scene that provides the connection to Unity and the Myo.  With in this prefab you can access the gyroscope and accelerometer, and the five preset gestures.  This package is provided in the Myo Alpha 6 SDK
