using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

public struct MidiMessage
{
    public byte status;
    public byte data1;
    public byte data2;

    public MidiMessage(uint data) {
        status = (byte)(data & 0xff);
        data1 = (byte)((data >> 8) & 0xff);
        data2 = (byte)((data >> 16) & 0xff);
    }
}

public class UnityMidiReceiver : MonoBehaviour
{
    [DllImport ("UnityMIDIReceiver")]
    private static extern void UnityMIDIReceiver_Initialize ();

    [DllImport ("UnityMIDIReceiver")]
    private static extern string UnityMIDIReceiver_GetLogText ();

    [DllImport ("UnityMIDIReceiver")]
    private static extern int UnityMIDIReceiver_CountEndpoints ();

    [DllImport ("UnityMIDIReceiver")]
    private static extern uint UnityMIDIReceiver_GetEndpointIDAtIndex (int index);

    [DllImport ("UnityMIDIReceiver")]
    private static extern string UnityMIDIReceiver_GetEndpointName (uint id);

    [DllImport ("UnityMIDIReceiver")]
    private static extern uint UnityMIDIReceiver_DequeueMessageDataOnEndpoint (uint id);

    List<uint> ids;
    string text = "";

    public GameObject target;

    void Start ()
    {
        ids = new List<uint> ();

        UnityMIDIReceiver_Initialize ();

        var count = UnityMIDIReceiver_CountEndpoints ();
        for (var i = 0; i < count; i++) {
            ids.Add (UnityMIDIReceiver_GetEndpointIDAtIndex (i));
        }

        foreach (var id in ids) {
            Debug.Log (UnityMIDIReceiver_GetEndpointName (id));
        }
    }

    void Update ()
    {
        foreach (var id in ids) {
            while (true) {
                var data = UnityMIDIReceiver_DequeueMessageDataOnEndpoint (id);
                if (data == 0)
                    break;
                var msg = new MidiMessage(data);
                if (msg.status == 0x90) {
                    target.SendMessage ("OnNoteOn", msg);
                }
            }
        }
    }

    void OnGUI ()
    {
        GUILayout.Label (text);
    }
}
