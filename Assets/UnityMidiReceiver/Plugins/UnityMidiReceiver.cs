using UnityEngine;
using System.Runtime.InteropServices;

// Bridge class for unity-midi-receiver plugin.
public static class UnityMidiReceiver
{
    [DllImport ("UnityMIDIReceiver")]
    private static extern int UnityMIDIReceiver_CountEndpoints ();
    
    [DllImport ("UnityMIDIReceiver")]
    private static extern uint UnityMIDIReceiver_GetEndpointIDAtIndex (int index);
    
    [DllImport ("UnityMIDIReceiver")]
    private static extern string UnityMIDIReceiver_GetEndpointName (uint id);
    
    [DllImport ("UnityMIDIReceiver")]
    private static extern ulong UnityMIDIReceiver_DequeueIncomingData ();

    public static int CountEndpoints ()
    {
        return UnityMIDIReceiver_CountEndpoints ();
    }

    public static uint GetEndpointIdAtIndex (int index)
    {
        return UnityMIDIReceiver_GetEndpointIDAtIndex (index);
    }

    public static string GetEndpointName (uint id)
    {
        return UnityMIDIReceiver_GetEndpointName (id);
    }

    public static ulong DequeueIncomingData ()
    {
        return UnityMIDIReceiver_DequeueIncomingData ();
    }
}
