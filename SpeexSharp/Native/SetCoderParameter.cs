namespace SpeexSharp.Native
{
    /// <summary>
    /// Values for setting coder parameter
    /// </summary>
    public enum SetCoderParameter : int
    {
        /// <summary>
        /// Set perceptual enhancer to on (1) or off (0) (int32, default is on) (decoder only)
        /// </summary>
        Enh = 0,

        /// <summary>
        /// Set the encoder speech quality (int32 from 0 to 10, default is 8)
        /// </summary>
        Quality = 4,

        /// <summary>
        ///  Set the mode number, as specified in the RTP spec (int32)
        /// </summary>
        Mode = 6,

        /// <summary>
        /// Set low-band sub-mode to use (wideband only)
        /// </summary>
        LowMode = 8,

        /// <summary>
        /// Set high-band sub-mode to use (wideband only)
        /// </summary>
        HighMode = 10,

        /// <summary>
        /// Set variable bit-rate (VBR) to on (1) or off (0) (int32, default is off)
        /// </summary>
        Vbr = 12,

        /// <summary>
        ///  Set the encoder VBR speech quality (float 0.0 to 10.0, default is 8.0)
        /// </summary>
        VbrQuality = 14,

        /// <summary>
        /// Set the CPU resources allowed for the encoder (int32 from 1 to 10, default is 2)
        /// </summary>
        Complexity = 16,

        /// <summary>
        /// Set the bit-rate to use the closest value not exceeding the parameter (int32 in bits per second)
        /// </summary>
        BitRate = 18,

        /// <summary>
        /// Define a handler function for in-band Speex request
        /// </summary>
        Handler = 20,

        /// <summary>
        /// Define a handler function for in-band user-defined request
        /// </summary>
        UserHandler = 22,

        /// <summary>
        /// Set real sampling rate (int32 in Hz)
        /// </summary>
        SamplingRate = 24,

        /// <summary>
        /// Reset the encoder/decoder state to its original state, clearing all memories (no argument)
        /// </summary>
        ResetState = 26,

        /// <summary>
        /// Set voice activity detection (VAD) to on (1) or off (0) (int32, default is off)
        /// </summary>
        Vad = 30,

        /// <summary>
        ///  Set average bit-rate (ABR) to a value n in bits per second (int32 in bits per second)
        /// </summary>
        Abr = 32,

        /// <summary>
        /// Set discontinuous transmission (DTX) to on (1) or off (0) (int32, default is off)
        /// </summary>
        Dtx = 34,

        /// <summary>
        /// Set submode encoding in each frame (1 for yes, 0 for no, setting to no breaks the standard)
        /// </summary>
        SubModeEncoding = 36,

        /// <summary>
        /// Tell the encoder to optimize encoding for a certain percentage of packet loss (int32 in percent)
        /// </summary>
        PlcTuning = 40,

        /// <summary>
        ///  Set the maximum bit-rate allowed in VBR operation (int32 in bits per second)
        /// </summary>
        VbrMaxBitRate = 42,

        /// <summary>
        /// Set the high-pass filter on (1) or off (0) (int32, default is on)
        /// </summary>
        HighPass = 44
    }
}
