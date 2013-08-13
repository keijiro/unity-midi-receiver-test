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
    public static extern int UnityMIDIReceiver_CountEndpoints ();

    [DllImport ("UnityMIDIReceiver")]
    public static extern uint UnityMIDIReceiver_GetEndpointIDAtIndex (int index);

    [DllImport ("UnityMIDIReceiver")]
    public static extern string UnityMIDIReceiver_GetEndpointName (uint id);

    [DllImport ("UnityMIDIReceiver")]
    public static extern ulong UnityMIDIReceiver_DequeueIncomingData ();

    Queue<MidiMessage> messageQueue;

    public bool IsEmpty {
        get { return messageQueue.Count == 0; }
    }

    public MidiMessage PopMessage ()
    {
        return messageQueue.Dequeue ();
    }

    void Start ()
    {
        messageQueue = new Queue<MidiMessage> ();
    }

    void Update ()
    {
        while (true) {
            var data = UnityMIDIReceiver_DequeueIncomingData ();
            if (data == 0)
                break;
            messageQueue.Enqueue (new MidiMessage (data));
        }
    }
}