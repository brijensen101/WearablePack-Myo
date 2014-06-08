WearablePack-Myo
================
![Flow Chart](https://cloud.githubusercontent.com/assets/6456976/3211270/82bf990c-ef0c-11e3-88f0-d9a2f4834aad.png)

Overview
Description: During surgery a Dr. will use a Myo controller and a Salesforce.com intergration to view images (X-Rays), order an additonal post op x-ray,  notifly the post op team about the end of surgery, and send notifications to others.
Architecture:
Thalmic Laps - Myo (The Controller)
Value of the Myo - This wearable device uses a bluetooth connection that will allow the user to control the screen.  The specific value for the surgeon will be that because it is touchless it preserves the clean environment needed for surgery.

Unity 3D (The UI Environment) - Unity 3D is a powerful gaming engine that provide an 3D environment in which to create an application.  The Myo SDK also comes with a plug in for Unity for rapid 3D interface development.
Unity Plugins/Packages
Myo Packages - This is a Unity Package that can be loaded to the Unity project.  The package has all the scripts needed to use the Myo device in scenes.  There is a Myo prefab that is added to the scene that provides the connection to Unity and the Myo.  With in this prefab you can access the gyroscope and accelerometer, and the five preset gestures.
Playmaker - This is a plugin is a visual scripter.  This provides the ability to quickly prototype and build your Unity scenes without the need to write extensive code.
Coherent UI - This is a plugin for Unity3d that provides a browser access with in your scenes.  This plugin also provides a two way communication between a browser and the Unity3d scene via Javascript
Research
http://www.bbc.com/news/health-18238356
http://www.siemens.com/innovation/en/news/2012/e_inno_1206_1.htm
http://research.microsoft.com/en-us/projects/touchlessinteractionmedical/
http://research.microsoft.com/apps/video/default.aspx?id=174856)
http://cacm.acm.org/magazines/2014/1/170869-touchless-interaction-in-surgery/fulltext

The Myo Advantage Over LeapMotion and Kenect
In the research I have done I found an interesting advantage the Myo has over the Kenect and LeapMotion.  Both the Kenect and LeapMotion use a line-of-sight camera.  This requires the surgeon to work within a specific “viewable” area.  Because the Myo does not use a line-of-sight camera for its interactions reference, and thus the surgeon is not restricted in where a gesture is performed. 

