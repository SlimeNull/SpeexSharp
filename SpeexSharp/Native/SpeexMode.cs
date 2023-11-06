using System.Runtime.InteropServices;

namespace SpeexSharp.Native
{
    /// <summary>
    /// Struct defining a Speex mode
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SpeexMode
    {
        /// <summary>
        /// Pointer to the low-level mode data
        /// </summary>
        public void* Mode;

        /// <summary>
        /// Pointer to the mode query function
        /// </summary>
        public delegate* unmanaged<void*, int, void*, int> Query;

        /// <summary>
        /// The name of the mode (you should not rely on this to identify the mode)
        /// </summary>
        public byte* ModeName;


        /// <summary>
        /// ID of the mode
        /// </summary>
        public int ModeID;

        /// <summary>
        /// Version number of the bitstream (incremented every time we break bitstream compatibility
        /// </summary>
        public int BitstreamVersion;


        /// <summary>
        /// Pointer to encoder initialization function
        /// </summary>
        public delegate* unmanaged<SpeexMode*, void*> EncInit;

        /// <summary>
        /// Pointer to encoder destruction function
        /// </summary>
        public delegate* unmanaged<void*, void> EncDestroy;

        /// <summary>
        /// Pointer to frame encoding function
        /// </summary>
        public delegate* unmanaged<void*, void*, SpeexBits*, int> Enc;


        /// <summary>
        /// Pointer to decoder initialization function
        /// </summary>
        public delegate* unmanaged<SpeexMode*, void*> DecInit;

        /// <summary>
        /// Pointer to decoder destruction function
        /// </summary>
        public delegate* unmanaged<void*, void> DecDestroy;

        /// <summary>
        /// Pointer to frame decoding function
        /// </summary>
        public delegate* unmanaged<void*, SpeexBits*, void*, int> Dec;

        /// <summary>
        /// ioctl-like requests for encoder
        /// </summary>
        public delegate* unmanaged<void*, int, void*, int> EncCtl;

        /// <summary>
        /// ioctl-like requests for decoder
        /// </summary>
        public delegate* unmanaged<void*, int, void*, int> DecCtl;
    }
}