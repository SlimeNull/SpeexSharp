using System.Runtime.InteropServices;

namespace SpeexSharp.Native
{
    /// <summary>
    /// Bit-packing data structure representing (part of) a bit-stream.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SpeexBits
    {
        /// <summary>
        /// "raw" data
        /// </summary>
        public byte* Data;

        /// <summary>
        /// Total number of bits stored in the stream
        /// </summary>
        public int BitCount;

        /// <summary>
        /// Position of the byte "cursor"
        /// </summary>
        public nint BytePointer;

        /// <summary>
        /// Position of the bit "cursor" within the current char
        /// </summary>
        public nint BitPointer;

        /// <summary>
        /// Does the struct "own" the "raw" buffer (member "chars")
        /// </summary>
        public int Owner;

        /// <summary>
        /// Set to one if we try to read past the valid data
        /// </summary>
        public int Overflow;

        /// <summary>
        /// Allocated size for buffer
        /// </summary>
        public int BufferSize;


        /// <summary>
        /// Reserved
        /// </summary>
        public int Reserved1;

        /// <summary>
        /// Reseved
        /// </summary>
        public void* Reserved2;
    }
}