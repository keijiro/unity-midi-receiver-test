using UnityEngine;
using System.Collections;

public class ParticleTrigger : MonoBehaviour
{
    void OnNoteOn (MidiMessage midi)
    {
        particleSystem.startSize = midi.data2 / 64.0f;
        particleSystem.Emit (Mathf.Max(midi.data2 / 8, 3));
    }
}
