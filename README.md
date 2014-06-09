WearablePack-Myo
================
![Flow Chart](https://cloud.githubusercontent.com/assets/6456976/3211270/82bf990c-ef0c-11e3-88f0-d9a2f4834aad.png)

## Overview
###Description:
Salesforce and Thalmic Labs have come together to create a touchless surgery device. During surgery a Dr. will use a Myo controller and a Salesforce.com intergration to view images (X-Rays), order an additonal post op x-rays, notifly the post op team about the end of surgery, and send notifications to others.

###Architecture Components:
There are three architectural componnets to the solution:

-  **The Connected Device - Thalmic Labs' Myo** -  https://www.thalmic.com/en/myo/

The Myo is a wearable device that is able to detect hand/spacial gestures and uses a bluetooth connection to allow the wearer to control an application.  In this usecase, the specific value for the surgeon is the touchless control, which preserves the clean environment needed for surgery.

- **The connected App - Unity 3D**  http://unity3d.com/

Unity 3D is a powerful platform for building applications iwth 3D interfaces. Traditionally, its origin is deeply rooted in game development. However, icreasingly, Unity3D is being used as the development platform of choice by 3D and spacially aware wearable devices such as the Myo. The Myo SDK also comes with a plug in for Unity for rapid 3D interface development.

- **The Salesforce1 Platform** - https://developer.salesforce.com/platform/overview

The Salesforce1 platform transforms the Myo Sergeon App in to a connected app, feeding it with data (patient info, Xrays, procedure details) as well as exposing business processes and workflows directly within the app.

###Integration Architectue 


- ** Myo to Unity3D integration **
The Myo SDK includes a Unity Package that provides the connection between Unity3D and the Myo device.  Within this package are the components required to access the gyroscope and accelerometer as well as the five preset gestures.  This package is provided as part of the Myo Alpha 6 SDK.

- ** Unity3D to Salesforce.com Integration **
This project illustrates three ways in which Salesforce.com and Unity3D can be integrated. Lets cover each one briefly:

####Data Integration using the REST API and OAuth**
**The demo video shows X-Ray images being rendered in the unity 3D enviornment. This is achieved by retrieving the images from Salesforce using the REST API. The images are attachments on the medical procedure (Case) record. Here is a snippet of the client code for logging-in, retrieving an access token and performing queries:**




**2. User Interface Integration using Visualforce**

**3. Data Integration via Apex Remote Methods and javascript**


###Required Unity3D Add-ons
**Playmaker** http://www.hutonggames.com/
This add-on is a visual scripter, providing a declarative development capability within Unity3D.  Playmaker provides the ability to quickly prototype and build your Unity scenes without the need to write extensive code.

**Coherent UI** http://coherent-labs.com/
This is an add-on that provides the ability to render web oages within the 3D environment. This was used to render Visualforce pages. Coherent UI also provides a two way communication between the embedded browser and the Unity3D scene via Javascript, which was used to call Visualforce remote acrtions.

**Myo Packages**
This is a Unity Package that provides the connection between Unity3D and the Myo device.  Within this prefab you can access the gyroscope and accelerometer as well as the five preset gestures.  This package is provided as part of the Myo Alpha 6 SDK
