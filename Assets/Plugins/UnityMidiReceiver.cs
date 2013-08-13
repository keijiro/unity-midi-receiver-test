using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

public struct MidiMessage
{
    public uint source;
    public byte status;
    public byte data1;
    public byte data2;

    public MidiMessage (ulong data)
    {
        source = (uint)(data & 0xffffffffUL);
        status = (byte)((data >> 32) & 0xff);
        data1 = (byte)((data >> 40) & 0xff);
        data2 = (byte)((data >> 48) & 0xff);
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
    private static extern ulong UnityMIDIReceiver_DequeueIncomingData ();

    public GameObject target;

    void Start ()
    {
        UnityMIDIReceiver_Initialize ();
        var count = UnityMIDIReceiver_CountEndpoints ();
        for (var i = 0; i < count; i++) {
            var id = UnityMIDIReceiver_GetEndpointIDAtIndex (i);
            Debug.Log (UnityMIDIReceiver_GetEndpointName (id));
        }
    }

    void Update ()
    {
        while (true) {
            var data = UnityMIDIReceiver_DequeueIncomingData ();
            if (data == 0)
                break;
            var msg = new MidiMessage (data);
            if (msg.status == 0x90) {
                target.SendMessage ("OnNoteOn", msg);
            }
        }
    }
}
