#APP SETUP

##Step 1 - Download App and Sign up to be a Myo Developer
- Desktop App - https://drive.google.com/file/d/0B6UL-FHBmg0AMjhXX3NjaDBLWWM/edit?usp=sharing 
- Myo Developer UIL - https://developer.thalmic.com/apply/

##Step 2 - Set up Demo SFDC org
- Login to the Demo org
- Username - aalammar@myo.com
- Pasword - HappyMyo2014

After logging in you will need to go to: Setup/Security Controls/Network Access
From here add the IP address to the "Trusted IP Ranges"
Please Note - This is only need for demo runs.

##Step 3 - Train your Myo
- First charge the Myo.
- Next plugin the Bluetooth usb hub.  
- Then run the "Training" program.

Please Note - the light on the Myo will turn blue if connected.  If you do not get a blue light trying running the "hello-myo" and "myo-id" in the "bin" folder.  The "bin" folder can be fund in the Myo SDK Alpha 6 zip.

#RUNNING THE APP
Once you have completed the steps above, below is how the app works:

##Gestures:
- **Fist** - this will make the Myo Buzz.  While holding the Fist gesture, move your arm around to nav around the skeleton.  
- **Wave in** - Rotates the skeleton to the left
- **Wave out** - Rotates the skeleton to the right
- **Finger Spread** - Resets the screen target and centers the myo to the screen
- **Twist In** - This is your "mouse click". Placing the target over a hotspot and doing the "Twist In" gesture will be the click for at hotspot.

##Hotspots in the Scene:
- Red Orb on Rt Knee on Skeleton - twist in to turn x-ray thumb nails on and off
- Each x-Ray Thumbnail - twist in will make the x-ray zoom larger and to the left of the screen
- Selected Zoom-In x-Ray - twist in will return the x-ray image to is thumb-nail position
- Stop Watch Icon - twist in will open the 5, 10, 15 time left option.  twist in again will close the 5, 10, 15 icons
- 5, 10, 15 Icons - twist in will send the events to Salesforce, show a 2sec UI message, then close automatically
x-Ray Icon - twist in will open the YES and NO buttons and title quesiton for pos-op x-Ray option.  twist in again will close the YES and NO buttons and title quesiton for pos-op x-Ray
- NO Button - twist in will close the YES and NO buttons and title quesiton for pos-op x-Ray
- YES Button - twist in will send the specific Salesforce events and show a 2sec UI Message, then close automatically

##Steps to App
- Step 1. - Open the "Myo_Surgeon_Assistant"
- Step 2. - Give the app about 10sec to load.  (you will know this has been completed when the thumb-nail xray images disappear)
- Step 3. - Center the Myo and screen target by using the "Finger Spread" gesture.  (note that if you lose or become off center with the myo and your arm movement use the "Finger Spread" gesture to re-center)
- Step 4. - Review x-Ray images
- Step 5. - Order Post-Op x-Ray (from the x-Ray icon)
- Step 6. - Chose a "time left" (from the stop watch icon)

###BUGS:
There are two current bugs:

- If the skeleton flies away from the scene the first time you make a "fist" gesture, restart the app.
- If one of the five x-Rays does not load (this will be noted with a large red x in place of the x-Ray image), restart the app.
