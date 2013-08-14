unity-midi-receiver-test
========================

This is a sample project for [unity-midi-receiver]
(https://github.com/keijiro/unity-midi-receiver).

See a [short demo](https://vine.co/v/hMAeTub9ZEm).

System Requirements
-------------------
- Unity 4 Pro
- Runs on desktop platforms (Mac OS X and Windows)

How Does It Work?
-----------------

The main component of this plug-in is **MidiReceiver**. You have to attach this
script to a game object, and then you can retrieve received messages with 
**MidiReceiver.PopMessage**. Don’t forget to check **MidiReceiver.IsEmpty**
before retrieval.

Received messages are given as a **MidiMessage** struct value. This struct
contains not only MIDI payloads (status byte and data bytes) but also an
**endpoint ID**. These endpoint IDs are 32-bit integer value which is uniquely
assigned to each endpoint, and therefore you can distinguish MIDI devices/ports
with using these IDs.

For the details about the plug-in, see the description on [unity-midi-receiver]
(https://github.com/keijiro/unity-midi-receiver).

**TIPS** - Project-independent modules are stored in **Assets/Plugins** and
**Assets/UnityMidiReceiver**. You can easily export these directories and
import it to your own projects.

License
-------

Copyright (c) 2013 Keijiro Takahashi

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
