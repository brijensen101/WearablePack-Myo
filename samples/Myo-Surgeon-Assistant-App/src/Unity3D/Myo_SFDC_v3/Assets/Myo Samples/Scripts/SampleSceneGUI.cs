using UnityEngine;
using System.Collections;

// Draw simple instructions for sample scene.
// Check to see if a Myo armband is paired, and if so, if it has been trained.
public class SampleSceneGUI : MonoBehaviour
{
    // Myo game object to connect with.
    // This object must have a ThalmicMyo script attached.
    public GameObject myo = null;

    // Draw some basic instructions.
    void OnGUI ()
    {
        GUI.skin.label.fontSize = 20;

        ThalmicHub hub = ThalmicHub.instance;

        // Access the ThalmicMyo script attached to the Myo object.
        ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

        if (!hub.hubInitialized) {
            GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
                "Bluetooth Dongle unavailable. Press Q to try again."
            );
        } else if (!thalmicMyo.isPaired) {
            GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
                "No Myo currently paired."
            );
        } else if (!thalmicMyo.isTrained) {
            GUI.Label(new Rect (12, 8, Screen.width, Screen.height),
                "Myo has not been trained. Please wear Myo with the USB port facing your elbow and run the trainer."
            );
        } else {
            GUI.Label (new Rect (12, 8, Screen.width, Screen.height),
                "Wear Myo with the USB port facing your elbow\n\n" +
                "Fist: Vibrate Myo armband\n" +
                "Wave in: Set box material to blue\n" +
                "Wave out: Set box material to green\n" +
                "Twist in: Reset box material\n" +
                "Fingers spread: Set forward direction"
            );
        }
    }

    void Update ()
    {
        ThalmicHub hub = ThalmicHub.instance;

        if (Input.GetKeyDown ("q")) {
            hub.ResetHub();
        }
    }
}
