using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System.Collections.Generic;

[CustomEditor(typeof(UnityMidiReceiver))]
class UnityMidiReceiverEditor : Editor
{
    Queue<MidiMessage> messages;

    public override void OnInspectorGUI ()
    {
        if (EditorApplication.isPlaying)
            return;

        if (messages == null) {
            messages = new Queue<MidiMessage> ();
        }

        var count = UnityMidiReceiver.UnityMIDIReceiver_CountEndpoints ();
        for (var i = 0; i < count; i++) {
            var id = UnityMidiReceiver.UnityMIDIReceiver_GetEndpointIDAtIndex (i);
            GUILayout.Label (UnityMidiReceiver.UnityMIDIReceiver_GetEndpointName (id));
        }

        GUILayout.Label ("Latest messages:");

        while (true) {
            ulong data = UnityMidiReceiver.UnityMIDIReceiver_DequeueIncomingData ();
            if (data == 0)
                break;
            messages.Enqueue (new MidiMessage (data));
        }

        while (messages.Count > 8) {
            messages.Dequeue ();
        }

        foreach (var message in messages) {
            GUILayout.Label (
                message.source.ToString ("X") + " | " +
                message.status.ToString ("X") + " | " +
                message.data1.ToString ("X") + ", " +
                message.data2.ToString ("X")
            );
        }

        EditorUtility.SetDirty (target);
    }
}
