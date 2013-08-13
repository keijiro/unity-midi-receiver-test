using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(MidiReceiver))]
class MidiReceiverEditor : Editor
{
    public override void OnInspectorGUI ()
    {
        // Only shows the details on Play Mode.
        if (EditorApplication.isPlaying) {
            var endpointCount = UnityMidiReceiver.CountEndpoints ();

            // Endpoints.
            var temp = "Detected MIDI endpoints:";
            for (var i = 0; i < endpointCount; i++) {
                var id = UnityMidiReceiver.GetEndpointIdAtIndex (i);
                var name = UnityMidiReceiver.GetEndpointName (id);
                temp += "\n" + id.ToString ("X8") + ": " + name;
            }
            EditorGUILayout.HelpBox (temp, MessageType.None);

            // Incomming messages.
            temp = "Incoming MIDI messages:";
            foreach (var message in (target as MidiReceiver).History) {
                temp += "\n" + message.ToString ();
            }
            EditorGUILayout.HelpBox (temp, MessageType.None);

            // Make itself dirty to update on every time.
            EditorUtility.SetDirty (target);
        } else {
            EditorGUILayout.HelpBox ("You can view the sutatus on Play Mode.", MessageType.Info);
        }
    }
}