using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEditor;

// MIDI message struct.
public struct MidiMessage
{
    // MIDI source (endpoint) ID.
    public uint source;

    // MIDI status byte.
    public byte status;

    // MIDI data bytes.
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

// MIDI Receiver class.
public class MidiReceiver : MonoBehaviour
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

#if UNITY_EDITOR
    Queue<MidiMessage> messageHistory;

    public Queue<MidiMessage> History {
        get { return messageHistory; }
    }
#endif

    public bool IsEmpty {
        get { return messageQueue.Count == 0; }
    }

    public MidiMessage PopMessage ()
    {
        return messageQueue.Dequeue ();
    }

    void Awake ()
    {
        messageQueue = new Queue<MidiMessage> ();
#if UNITY_EDITOR
        messageHistory = new Queue<MidiMessage> ();
#endif
    }

    void Update ()
    {
        while (true) {
            var data = UnityMIDIReceiver_DequeueIncomingData ();
            if (data == 0) {
                break;
            }

            var message = new MidiMessage (data);
            messageQueue.Enqueue (message);
#if UNITY_EDITOR
            messageHistory.Enqueue (message);
#endif
        }
#if UNITY_EDITOR
        while (messageHistory.Count > 8) {
            messageHistory.Dequeue ();
        }
#endif
    }
}