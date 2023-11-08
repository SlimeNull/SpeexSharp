using System.Runtime.InteropServices;

namespace SpeexSharp.Native
{
    /// <summary>
    /// State used for decoding (intensity) stereo information
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct SpeexStereoState
    {
        /// <summary>
        /// Left/right balance info
        /// </summary>
        public float Balance;

        /// <summary>
        /// Ratio of energies: E(left+right)/[E(left)+E(right)]
        /// </summary>
        public float EnergiesRatio;

        /// <summary>
        /// Smoothed left channel gain
        /// </summary>
        public float SmoothLeft;

        /// <summary>
        /// Smoothed right channel gain
        /// </summary>
        public float SmoothRight;


        /// <summary>
        /// Reserved
        /// </summary>
        public float Reserved1;

        /// <summary>
        /// Reserved
        /// </summary>
        public float Reserved2;
    }
}