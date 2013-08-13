using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System.Collections.Generic;

[CustomEditor(typeof(MidiReceiver))]
class MidiReceiverEditor : Editor
{
    public override void OnInspectorGUI ()
    {
        if (EditorApplication.isPlaying) {
            var temp = "Detected MIDI endpoints (sources):";
            var endpointCount = MidiReceiver.UnityMIDIReceiver_CountEndpoints ();
            for (var i = 0; i < endpointCount; i++) {
                var id = MidiReceiver.UnityMIDIReceiver_GetEndpointIDAtIndex (i);
                temp += "\n- " + MidiReceiver.UnityMIDIReceiver_GetEndpointName (id);
            }
            EditorGUILayout.HelpBox(temp, MessageType.None);

            temp = "Incoming MIDI messages:";
            foreach (var message in (target as MidiReceiver).History) {
                temp += 
                    "\n" + 
                    message.source.ToString ("X") + " | " +
                        message.status.ToString ("X") + " | " +
                        message.data1.ToString ("X") + ", " +
                        message.data2.ToString ("X");
            }
            EditorGUILayout.HelpBox(temp, MessageType.None);

            EditorUtility.SetDirty(target);
        } else {
            EditorGUILayout.HelpBox("You can view the sutatus on Play Mode.", MessageType.Info);
        }

    }
}