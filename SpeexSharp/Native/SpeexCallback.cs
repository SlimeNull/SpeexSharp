using System.Runtime.InteropServices;

namespace SpeexSharp.Native
{
    /// <summary>
    /// Callback information
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SpeexCallback
    {
        /// <summary>
        /// ID associated to the callback
        /// </summary>
        public int CallbackID;

        /// <summary>
        /// Callback handler function
        /// </summary>
        public delegate* unmanaged<SpeexBits*, void*, void*, int> Func;

        /// <summary>
        /// Data that will be sent to the handler
        /// </summary>
        public void* Data;


        /// <summary>
        /// Reserved
        /// </summary>
        public void* Reserved1;

        /// <summary>
        /// Reserved
        /// </summary>
        public int Reserved2;
    }
}