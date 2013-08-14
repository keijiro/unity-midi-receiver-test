using UnityEngine;
using System.Runtime.InteropServices;

// Bridge class for unity-midi-receiver plugin.
public static class UnityMidiReceiver
{
    [DllImport ("UnityMIDIReceiver", EntryPoint="UnityMIDIReceiver_CountEndpoints")]
    public static extern int CountEndpoints ();
    
    [DllImport ("UnityMIDIReceiver", EntryPoint="UnityMIDIReceiver_GetEndpointIDAtIndex")]
    public static extern uint GetEndpointIdAtIndex (int index);
    
    [DllImport ("UnityMIDIReceiver")]
    private static extern System.IntPtr UnityMIDIReceiver_GetEndpointName (uint id);
    
    [DllImport ("UnityMIDIReceiver", EntryPoint="UnityMIDIReceiver_DequeueIncomingData")]
    public static extern ulong DequeueIncomingData ();

    public static string GetEndpointName (uint id)
    {
        return Marshal.PtrToStringAnsi (UnityMIDIReceiver_GetEndpointName (id));
    }
}
