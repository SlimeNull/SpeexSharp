namespace SpeexSharp.Native
{
    /// <summary>
    /// Values for getting coder parameter
    /// </summary>
    public enum GetCoderParameter : int
    {
        /// <summary>
        /// Get perceptual enhancer status (int32) (decoder only)
        /// </summary>
        Enh = 1,

        /// <summary>
        /// Get the number of samples per frame for the current mode (int32)
        /// </summary>
        FrameSize = 3,

        /// <summary>
        /// Get the current mode number, as specified in the RTP spec (int32)
        /// </summary>
        Mode = 7,

        /// <summary>
        /// Get current low-band mode in use (wideband only)
        /// </summary>
        LowMode = 9,

        /// <summary>
        /// Get current high-band mode in use (wideband only)
        /// </summary>
        HighMode = 11,

        /// <summary>
        /// Get variable bit-rate (VBR) status (int32)
        /// </summary>
        Vbr = 13,

        /// <summary>
        /// Get the current encoder VBR speech quality (float 0 to 10)
        /// </summary>
        VbrQuality = 15,

        /// <summary>
        ///  Get the CPU resources allowed for the encoder (int32 from 1 to 10, default is 2)
        /// </summary>
        Complexity = 17,

        /// <summary>
        /// Get the current bit-rate in use (int32 in bits per second)
        /// </summary>
        BitRate = 19,

        /// <summary>
        /// Get real sampling rate (int32 in Hz)
        /// </summary>
        SamplingRate = 25,

        /// <summary>
        /// Get VBR info (mostly used internally)
        /// </summary>
        RelativeQuality = 29,

        /// <summary>
        /// Get voice activity detection (VAD) status (int32)
        /// </summary>
        Vad = 31,

        /// <summary>
        /// Get average bit-rate (ABR) setting (int32 in bits per second)
        /// </summary>
        Abr = 33,

        /// <summary>
        /// Get discontinuous transmission (DTX) status (int32)
        /// </summary>
        Dtx = 35,

        /// <summary>
        /// Get submode encoding in each frame
        /// </summary>
        SubModeEncoding = 37,

        /// <summary>
        /// Returns the lookahead used by Speex
        /// </summary>
        Lookahead = 39,

        /// <summary>
        ///  Get the current tuning of the encoder for PLC (int32 in percent)
        /// </summary>
        PlcTuning = 41,

        /// <summary>
        /// Get the current maximum bit-rate allowed in VBR operation (int32 in bits per second)
        /// </summary>
        VbrMaxBitRate = 43,

        /// <summary>
        /// Get status of input/output high-pass filtering
        /// </summary>
        HighPass = 45,

        /// <summary>
        /// Get "activity level" of the last decoded frame, i.e. now much damage we cause if we remove the frame
        /// </summary>
        Activity = 47
    }
}
