using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;

public class UnityMidiReceiver : MonoBehaviour
{
    [DllImport ("UnityMIDIReceiver")]
    private static extern void UnityMIDIReceiver_Initialize ();

    [DllImport ("UnityMIDIReceiver")]
    private static extern string UnityMIDIReceiver_GetLogText ();

    string text;

    void Start ()
    {
        UnityMIDIReceiver_Initialize ();
    }

    void Update ()
    {
        text = UnityMIDIReceiver_GetLogText ();
    }

    void OnGUI ()
    {
        GUILayout.Label (text);
    }
}
