using System.Runtime.InteropServices;

namespace SpeexSharp.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SpeexHeader
    {
        const int SpeexHeaderStringLength = 8;
        const int SpeexHeaderVersionLength = 20;

        public fixed byte SpeexString[SpeexHeaderStringLength];
        public fixed byte SpeexVersion[SpeexHeaderVersionLength];

        public int SpeexVersionID;
        public int HeaderSize;
        public int Rate;
        public int Mode;
        public int ModeBitstreamVersion;
        public int NbChannels;
        public int Bitrate;
        public int FrameSize;
        public int Vbr;
        public int FramesPerPacket;
        public int ExtraHeaders;
        public int Reserved1;
        public int Reserved2;
    }
}