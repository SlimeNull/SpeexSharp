using System.Runtime.InteropServices;

namespace SpeexSharp.Native
{
    /// <summary>
    /// Speex header info for file-based formats
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SpeexHeader
    {
        /// <summary>
        /// SpeexHeaderStringLength
        /// </summary>
        public const int SpeexHeaderStringLength = 8;

        /// <summary>
        /// SpeexHeaderVersionLength
        /// </summary>
        public const int SpeexHeaderVersionLength = 20;

        /// <summary>
        /// Identifies a Speex bit-stream, always set to "Speex"
        /// </summary>
        public fixed byte SpeexString[SpeexHeaderStringLength];

        /// <summary>
        /// Speex version
        /// </summary>
        public fixed byte SpeexVersion[SpeexHeaderVersionLength];



        /// <summary>
        /// Version for Speex (for checking compatibility)
        /// </summary>
        public int SpeexVersionID;

        /// <summary>
        /// Total size of the header ( sizeof(SpeexHeader) )
        /// </summary>
        public int HeaderSize;

        /// <summary>
        /// Sampling rate used
        /// </summary>
        public int Rate;

        /// <summary>
        /// Mode used (0 for narrowband, 1 for wideband)
        /// </summary>
        public int Mode;

        /// <summary>
        /// Version ID of the bit-stream
        /// </summary>
        public int ModeBitstreamVersion;

        /// <summary>
        /// Number of channels encoded
        /// </summary>
        public int ChannelCount;

        /// <summary>
        /// Bit-rate used
        /// </summary>
        public int Bitrate;

        /// <summary>
        /// Size of frames
        /// </summary>
        public int FrameSize;

        /// <summary>
        /// 1 for a VBR encoding, 0 otherwise
        /// </summary>
        public int Vbr;

        /// <summary>
        /// Number of frames stored per Ogg packet
        /// </summary>
        public int FramesPerPacket;

        /// <summary>
        /// Number of additional headers after the comments
        /// </summary>
        public int ExtraHeaders;

        /// <summary>
        /// Reserved for future use, must be zero
        /// </summary>
        public int Reserved1;

        /// <summary>
        /// Reserved for future use, must be zero
        /// </summary>
        public int Reserved2;
    }
}