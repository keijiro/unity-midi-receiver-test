using UnityEngine;
using System.Collections;

public class MidiMessageDistributor : MonoBehaviour
{
    public GameObject[] targets;
    UnityMidiReceiver receiver;

    void Start ()
    {
        receiver = FindObjectOfType (typeof(UnityMidiReceiver)) as UnityMidiReceiver;
    }

    void Update ()
    {
        while (!receiver.IsEmpty) {
            var message = receiver.PopMessage ();
            if (message.status == 0x90) {
                foreach (var go in targets) {
                    go.SendMessage ("OnNoteOn", message);
                }
            }
        }
    }
}
