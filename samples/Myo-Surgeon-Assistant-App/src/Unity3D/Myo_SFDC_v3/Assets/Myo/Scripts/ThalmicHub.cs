using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections.Generic;

// Allows access to one or more Myo armbands, which must be immediate children of the GameObject this script is attached
// to. ThalmicHub is a singleton; only one ThalmicHub instance is allowed in a scene, which can be accessed through
// ThalmicHub.instance. ThalmicHub will persist across scene changes.
public class ThalmicHub : MonoBehaviour
{
    // The single instance of ThalmicHub. Set during Awake.
    public static ThalmicHub instance {
        get { return _instance; }
    }

    // The pairing method affects how the hub decides which Myo armband(s) to pair with:
    // - Any will pair with any nearby Myo armbands.
    // - Adjacent will only pair with Myo armbands that are very close to the Bluetooth antenna of the device on which
    //   the Unity project is running.
    // - ByMacAddress will only pair with one Myo armband with the provided MAC address.
    public enum PairingMethod {
        Any,
        Adjacent,
        ByMacAddress
    };

    // See PairingMethod above.
    public PairingMethod pairingMethod = PairingMethod.Any;

    // If pairingMethod is ByMacAddress, this is the MAC address that the hub will pair with.
    public string pairingMacAddress = "00-00-00-00-00-00";

    // True if and only if the hub initialized successfully; typically this is set during Awake, but it can also be
    // set by calling ResetHub() explicitly. The typical reason for initialization to fail is that the Bluetooth dongle
    // could not be successfully initialized, if a dongle is being used, e.g. because it is unplugged or in use by
    // another application.
    public bool hubInitialized {
        get { return _hub != null; }
    }

    // Reset the hub; if the hub is currently initialized, unpair from any paired Myos and reinitialize. This function
    // is typically used if initialization failed to attempt to initialize again (e.g. after asking the user to ensure
    // their dongle is plugged in).
    public bool ResetHub() {
        if (_hub != null) {
            foreach (ThalmicMyo myo in _myos) {
                myo.internalMyo = null;
            }
            lock (_lock) {
                _hub.Dispose ();
                _newlyPairedMyos.Clear ();
            }
            _hub = null;
        }
#if UNITY_ANDROID && !UNITY_EDITOR
        // The Hub needs to be initialized on the Android UI thread.
        unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
            createHubAndPair ();
        }));
        return true; // Return true assuming the hub constructor will succeed. Debug.Log if it fails.
#else
        return createHubAndPair ();
#endif
    }

    void Awake ()
    {
        // Ensure that there is only one ThalmicHub.
        if (_instance != null) {
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("Can only have one ThalmicHub",
                                        "Your scene contains more than one ThalmicHub. Remove all but one ThalmicHub.",
                                        "OK");
#endif
            Destroy (this.gameObject);
            return;
        } else {
            _instance = this;
        }

        // Do not destroy this game object. This will ensure that it remains active even when
        // switching scenes.
        DontDestroyOnLoad(this);

        for (int i = 0; i < transform.childCount; ++i) {
            Transform child = transform.GetChild (i);

            var myo = child.gameObject.GetComponent<ThalmicMyo> ();
            if (myo != null) {
                _myos.Add(myo);
            }
        }

        if (_myos.Count < 1) {
            string errorMessage = "The ThalmicHub's GameObject must have at least one child with a ThalmicMyo component.";
#if UNITY_EDITOR
            EditorUtility.DisplayDialog ("Thalmic Hub has no Myo children", errorMessage, "OK");
#else
            throw new UnityException (errorMessage);
#endif
        }

        if (_myos.Count > 1 && pairingMethod != PairingMethod.Any && pairingMethod != PairingMethod.Adjacent) {
            string errorMessage = "Multiple Myos are only supported when the pairing method is Any or Adjacent.";
#if UNITY_EDITOR
            EditorUtility.DisplayDialog ("Incompatible pairing method", errorMessage, "OK");
#else
            throw new UnityException (errorMessage);
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var unityActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        var applicationContext = unityActivity.Call<AndroidJavaObject>("getApplicationContext");

        // Need to pass the Android Application Context to the Myo Android plugin before initializing the Hub.
        AndroidJavaClass nativeEventsClass = new AndroidJavaClass("com.thalmic.myo.NativeEvents");
        nativeEventsClass.CallStatic("setApplicationContext", applicationContext);

        // The Hub needs to be initialized on the Android UI thread.
        unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
            createHubAndPair ();
        }));
#else
        createHubAndPair ();
#endif
    }

    private bool createHubAndPair () {
        try {
            _hub = new Thalmic.Myo.Hub ();
        } catch (System.Exception) {
            Debug.Log ("ThalmicHub failed to initialize.");
            return false;
        }
        _hub.Paired += hub_MyoPaired;
        if (pairingMethod == PairingMethod.Any) {
            _hub.PairWithAnyMyos ((uint)_myos.Count);
        } else if (pairingMethod == PairingMethod.Adjacent) {
            _hub.PairWithAdjacentMyos ((uint)_myos.Count);
        } else if (pairingMethod == PairingMethod.ByMacAddress) {
            _hub.PairByMacAddress (Thalmic.Myo.libmyo.string_to_mac_address (pairingMacAddress));
        }
        return true;
    }

    void OnApplicationQuit ()
    {
        if (_hub != null) {
            _hub.Dispose ();
            _hub = null;
        }
    }

    void Update ()
    {
        lock (_lock) {
            foreach (Thalmic.Myo.Myo newMyo in _newlyPairedMyos) {
                foreach (ThalmicMyo myo in _myos) {
                    if (myo.internalMyo == null) {
                        myo.internalMyo = newMyo;
                        break;
                    }
                }
            }
            _newlyPairedMyos.Clear ();
        }
    }

    void hub_MyoPaired (object sender, Thalmic.Myo.MyoEventArgs e)
    {
        lock (_lock) {
            _newlyPairedMyos.Add (e.Myo);
        }
    }

    private static ThalmicHub _instance = null;

    private Thalmic.Myo.Hub _hub = null;

    private Object _lock = new Object ();

    private List<Thalmic.Myo.Myo> _newlyPairedMyos = new List<Thalmic.Myo.Myo> ();

    private List<ThalmicMyo> _myos = new List<ThalmicMyo>();
}
