using System.Runtime.InteropServices;

namespace SpeexSharp.Native
{
    /// <summary>
    /// Native Speex APIs
    /// </summary>
    public static unsafe class Speex
    {
        static Speex()
        {
            EnsurePlatformSupported();
        }

        static void EnsurePlatformSupported()
        {
            if (!IsSupportedPlatform())
                throw new PlatformNotSupportedException();

            bool IsSupportedPlatform()
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
                    RuntimeInformation.ProcessArchitecture is Architecture.X64 or Architecture.X86)
                    return true;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) &&
                    RuntimeInformation.ProcessArchitecture is Architecture.X64)
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Dynamic link library file name
        /// </summary>
        public const string DllName = "libspeex";

        /// <summary>
        /// Whether it is compatible with the origin libspeex.dll
        /// </summary>
        public static bool CompatibilityMode { get; set; } = false;

        #region Bits

        /// <summary>
        /// Initializes and allocates resources for a <see cref="SpeexBits"/> struct
        /// </summary>
        /// <param name="bits"></param>
        [DllImport(DllName, EntryPoint = "speex_bits_init")]
        public static extern void BitsInit(SpeexBits* bits);


        /// <summary>
        /// Initializes <see cref="SpeexBits"/> struct using a pre-allocated buffer
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        [DllImport(DllName, EntryPoint = "speex_bits_init_buffer")]
        public static extern void BitsInitBuffer(SpeexBits* bits, void* buffer, int bufferSize);


        /// <summary>
        /// Insert a terminator so that the data can be sent as a packet while auto-detecting the number of frames in each packet
        /// </summary>
        /// <param name="bits">Bit-stream to operate on</param>
        [DllImport(DllName, EntryPoint = "speex_bits_insert_ternimator")]
        public static extern void BitsInsertTerminator(SpeexBits* bits);


        /// <summary>
        /// Returns the number of bytes in the bit-stream, including the last one even if it is not "full"
        /// </summary>
        /// <param name="bits">Bit-stream to operate on</param>
        /// <returns>Number of bytes in the stream</returns>
        [DllImport(DllName, EntryPoint = "speex_bits_nbytes")]
        public static extern int BitsGetByteCount(SpeexBits* bits);


        /// <summary>
        /// Append bits to the bit-stream
        /// </summary>
        /// <param name="bits">Bit-stream to operate on</param>
        /// <param name="data">Value to append as integer</param>
        /// <param name="bitCount">Number of bits to consider in "data"</param>
        [DllImport(DllName, EntryPoint = "speex_bits_pack")]
        public static extern void BitsPack(SpeexBits* bits, int data, int bitCount);


        /// <summary>
        /// Get the value of the next bit in the stream, without modifying the "cursor" position
        /// </summary>
        /// <param name="bits">Bit-stream to operate on</param>
        /// <returns>Value of the bit peeked (one bit only)</returns>
        [DllImport(DllName, EntryPoint = "speex_bits_peek")]
        public static extern int BitsPeek(SpeexBits* bits);


        /// <summary>
        /// Same as speex_bits_unpack_unsigned, but without modifying the cursor position
        /// </summary>
        /// <param name="bits">Bit-stream to operate on</param>
        /// <param name="bitCount">Number of bits to look for</param>
        /// <returns>Value of the bits peeked, interpreted as unsigned</returns>
        [DllImport(DllName, EntryPoint = "speex_bits_peek_unsigned")]
        public static extern uint BitsPeekUnsigned(SpeexBits* bits, int bitCount);


        /// <summary>
        /// Initializes the bit-stream from the data in an area of memory
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="bytes"></param>
        /// <param name="len"></param>
        [DllImport(DllName, EntryPoint = "speex_bits_read_from")]
        public static extern void BitsReadFrom(SpeexBits* bits, byte* bytes, int len);


        /// <summary>
        /// Append bytes to the bit-stream
        /// </summary>
        /// <param name="bits">Bit-stream to operate on</param>
        /// <param name="bytes">Pointer to the bytes what will be appended</param>
        /// <param name="len">Number of bytes of append</param>
        [DllImport(DllName, EntryPoint = "speex_bits_read_whole_bytes")]
        public static extern void BitsReadWholeBytes(SpeexBits* bits, byte* bytes, int len);


        /// <summary>
        /// Advances the position of the "bit cursor" in the stream
        /// </summary>
        /// <param name="bits">Bit-stream to operate on</param>
        /// <param name="n">Number of bits to advance</param>
        [DllImport(DllName, EntryPoint = "speex_bits_advance")]
        public static extern void BitsAdvance(SpeexBits* bits, int n);


        /// <summary>
        /// Returns the number of bits remaining to be read in a stream
        /// </summary>
        /// <param name="bits">Bit-stream to operate on</param>
        /// <returns>Number of bits that can still be read from the stream</returns>
        [DllImport(DllName, EntryPoint = "speex_bits_remaining")]
        public static extern int BitsRemaining(SpeexBits* bits);


        /// <summary>
        /// Resets bits to initial value (just after initialization, erasing content)
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_bits_reset")]
        public static extern void BitsReset(SpeexBits* bits);


        /// <summary>
        /// Rewind the bit-stream to the beginning (ready for read) without erasing the content
        /// </summary>
        /// <param name="bits"></param>
        [DllImport(DllName, EntryPoint = "speex_bits_rewind")]
        public static extern void BitsRewind(SpeexBits* bits);


        /// <summary>
        /// Sets the bits in a SpeexBits struct to use data from an existing buffer (for decoding without copying data)
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        [DllImport(DllName, EntryPoint = "speex_bits_set_bit_buffer")]
        public static extern void BitsSetBitBuffer(SpeexBits* bits, void* buffer, int bufferSize);


        /// <summary>
        /// Interpret the next bits in the bit-stream as a signed integer
        /// </summary>
        /// <param name="bits">Bit-stream to operate on</param>
        /// <param name="bitCount">Number of bits to interpret</param>
        /// <returns>A signed integer represented by the bits read</returns>
        [DllImport(DllName, EntryPoint = "speex_bits_unpack_signed")]
        public static extern int BitsUnpackSigned(SpeexBits* bits, int bitCount);


        /// <summary>
        /// Interpret the next bits in the bit-stream as an unsigned integer
        /// </summary>
        /// <param name="bits">Bit-stream to operate on</param>
        /// <param name="bitCount">Number of bits to interpret</param>
        /// <returns>An unsigned integer represented by the bits read</returns>
        [DllImport(DllName, EntryPoint = "speex_bits_unpack_unsigned")]
        public static extern uint BitsUnpackUnsigned(SpeexBits* bits, int bitCount);


        /// <summary>
        /// Write the content of a bit-stream to an area of memory
        /// </summary>
        /// <param name="bits">Write the content of a bit-stream to an area of memory</param>
        /// <param name="bytes">Memory location where to write the bits</param>
        /// <param name="maxLen">Maximum number of bytes to write (i.e. size of the "bytes" buffer)</param>
        /// <returns>Number of bytes written to the "bytes" buffer</returns>
        [DllImport(DllName, EntryPoint = "speex_bits_write")]
        public static extern int BitsWrite(SpeexBits* bits, byte* bytes, int maxLen);


        /// <summary>
        /// Like speex_bits_write, but writes only the complete bytes in the stream. Also removes the written bytes from the stream
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="bytes"></param>
        /// <param name="maxLen"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_bits_write_whole_bytes")]
        public static extern int BitsWriteWholeBytes(SpeexBits* bits, byte* bytes, int maxLen);

        #endregion

        #region Decode


        /// <summary>
        /// Uses an existing decoder state to decode one frame of speech from bit-stream bits. The output speech is saved written to output.
        /// </summary>
        /// <param name="state">Decoder state</param>
        /// <param name="bits">Bit-stream from which to decode the frame (NULL if the packet was lost)</param>
        /// <param name="output">Where to write the decoded frame</param>
        /// <returns>return status (0 for no error, -1 for end of stream, -2 corrupt stream)</returns>
        [DllImport(DllName, EntryPoint = "speex_decode")]
        public static extern int Decode(void* state, SpeexBits* bits, float* output);

        /// <summary>
        /// Uses an existing decoder state to decode one frame of speech from bit-stream bits. The output speech is saved written to output.
        /// </summary>
        /// <param name="state">Decoder state</param>
        /// <param name="bits">Bit-stream from which to decode the frame (NULL if the packet was lost)</param>
        /// <param name="output">Where to write the decoded frame</param>
        /// <returns>return status (0 for no error, -1 for end of stream, -2 corrupt stream)</returns>
        [DllImport(DllName, EntryPoint = "speex_decode_int")]
        public static extern int DecodeInt(void* state, SpeexBits* bits, short* output);


        /// <summary>
        /// Used like the ioctl function to control the encoder parameters
        /// </summary>
        /// <param name="state">Decoder state</param>
        /// <param name="request">ioctl-type request (one of the SPEEX_∗ macros) (<see cref="SetCoderParameter"/> or <see cref="GetCoderParameter"/>)</param>
        /// <param name="ptr">Data exchanged to-from function</param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_decoder_ctl")]
        public static extern int DecoderCtl(void* state, int request, void* ptr);


        /// <summary>
        /// Frees all resources associated to an existing decoder state.
        /// </summary>
        /// <param name="state">State to be destroyed</param>
        [DllImport(DllName, EntryPoint = "speex_decoder_destroy")]
        public static extern void DecoderDestroy(void* state);

        /// <summary>
        /// Returns a handle to a newly created decoder state structure. 
        /// For now, the mode argument can be &amp;nb_mode or &amp;wb_mode. In the future, more modes may be added. 
        /// Note that for now if you have more than one channels to decode, you need one state per channel.
        /// </summary>
        /// <param name="mode">Speex mode (one of speex_nb_mode or speex_wb_mode)</param>
        /// <returns>A newly created decoder state or NULL if state allocation fails</returns>
        [DllImport(DllName, EntryPoint = "speex_decoder_init")]
        public static extern void* DecoderInit([In] SpeexMode* mode);

        #endregion

        #region Encode


        /// <summary>
        /// Uses an existing encoder state to encode one frame of speech pointed to by "input". The encoded bit-stream is saved in "bits".
        /// </summary>
        /// <param name="state">Encoder state</param>
        /// <param name="input">Frame that will be encoded with a +-2∧15 range. This data MAY be overwritten by the encoder and should be considered uninitialised after the call.</param>
        /// <param name="bits"></param>
        /// <returns>Bit-stream where the data will be written</returns>
        [DllImport(DllName, EntryPoint = "speex_encode")]
        public static extern int Encode(void* state, float* input, SpeexBits* bits);


        /// <summary>
        /// Uses an existing encoder state to encode one frame of speech pointed to by "input". The encoded bit-stream is saved in "bits".
        /// </summary>
        /// <param name="state">Encoder state</param>
        /// <param name="input">Frame that will be encoded with a +-2∧15 range</param>
        /// <param name="bits">Bit-stream where the data will be written</param>
        /// <returns>0 if frame needs not be transmitted (DTX only), 1 otherwise</returns>
        [DllImport(DllName, EntryPoint = "speex_encode_int")]
        public static extern int EncodeInt(void* state, short* input, SpeexBits* bits);


        /// <summary>
        /// Used like the ioctl function to control the encoder parameters
        /// </summary>
        /// <param name="state">Encoder state</param>
        /// <param name="request">ioctl-type request (one of the SPEEX_∗ macros) (<see cref="SetCoderParameter"/> or <see cref="GetCoderParameter"/>)</param>
        /// <param name="ptr">Data exchanged to-from function</param>
        /// <returns>0 if no error, -1 if request in unknown, -2 for invalid parameter</returns>
        [DllImport(DllName, EntryPoint = "speex_encoder_ctl")]
        public static extern int EncoderCtl(void* state, int request, void* ptr);


        /// <summary>
        /// Frees all resources associated to an existing Speex encoder state.
        /// </summary>
        /// <param name="state">Encoder state to be destroyed</param>
        [DllImport(DllName, EntryPoint = "speex_encoder_destroy")]
        public static extern void EncoderDestroy(void* state);


        /// <summary>
        /// Returns a handle to a newly created Speex encoder state structure. For now, the "mode" argument can be &amp;nb_mode or &amp;wb_mode . In the future, more modes may be added. Note that for now if you have more than one channels to encode, you need one state per channel.
        /// </summary>
        /// <param name="mode">The mode to use (either speex_nb_mode or speex_wb.mode)</param>
        /// <returns>A newly created encoder state or NULL if state allocation fails</returns>
        [DllImport(DllName, EntryPoint = "speex_encoder_init")]
        public static extern void* EncoderInit([In] SpeexMode* mode);

        #endregion

        #region Stereo

        /// <summary>
        /// Transforms a mono frame into a stereo frame using intensity stereo info
        /// </summary>
        /// <param name="data"></param>
        /// <param name="frameSize"></param>
        /// <param name="stereo"></param>
        [DllImport(DllName, EntryPoint = "speex_decode_stereo")]
        public static extern void DecodeStereo(float* data, int frameSize, SpeexStereoState* stereo);


        /// <summary>
        /// Transforms a mono frame into a stereo frame using intensity stereo info
        /// </summary>
        /// <param name="data"></param>
        /// <param name="frameSize"></param>
        /// <param name="stereo"></param>
        [DllImport(DllName, EntryPoint = "speex_decode_stereo_int")]
        public static extern void DecodeStereoInt(short* data, int frameSize, SpeexStereoState* stereo);


        /// <summary>
        /// Transforms a stereo frame into a mono frame and stores intensity stereo info in ’bits’
        /// </summary>
        /// <param name="data"></param>
        /// <param name="frameSize"></param>
        /// <param name="bits"></param>
        [DllImport(DllName, EntryPoint = "speex_encode_stereo")]
        public static extern void EncodeStereo(float* data, int frameSize, SpeexBits* bits);


        /// <summary>
        /// Transforms a stereo frame into a mono frame and stores intensity stereo info in ’bits’
        /// </summary>
        /// <param name="data"></param>
        /// <param name="frameSize"></param>
        /// <param name="bits"></param>
        [DllImport(DllName, EntryPoint = "speex_encode_stereo_int")]
        public static extern void EncodeStereoInt(short* data, int frameSize, SpeexBits* bits);


        /// <summary>
        /// Callback handler for intensity stereo info
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="state"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_std_stereo_request_handler")]
        public static extern int StdStereoRequestHandler(SpeexBits* bits, void* state, void* data);


        /// <summary>
        /// Initialise/create a stereo stereo state
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_stereo_state_init")]
        public static extern SpeexStereoState* StereoStateInit();


        /// <summary>
        /// Reset/re-initialise an already allocated stereo state
        /// </summary>
        /// <param name="stereo"></param>
        [DllImport(DllName, EntryPoint = "speex_stereo_state_reset")]
        public static extern void StereoStateReset(SpeexStereoState* stereo);


        /// <summary>
        /// Destroy a stereo stereo state
        /// </summary>
        /// <param name="stereo"></param>
        [DllImport(DllName, EntryPoint = "speex_stereo_state_destroy")]
        public static extern void StereoStateDestroy(SpeexStereoState* stereo);

        #endregion

        #region Callback

        /// <summary>
        /// Default handler for user-defined requests: in this case, just ignore
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="state"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_default_user_handler")]
        public static extern int DefaultUserHandler(SpeexBits* bits, void* state, void* data);


        /// <summary>
        /// Handle in-band request
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="callbackList"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_inband_handler")]
        public static extern int InbandHandler(SpeexBits* bits, SpeexCallback* callbackList, void* state);


        /// <summary>
        /// Standard handler for in-band characters (write to stderr)
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="state"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_std_char_handler")]
        public static extern int StdCharHandler(SpeexBits* bits, void* state, void* data);


        /// <summary>
        /// Standard handler for enhancer request (Turn ehnancer on/off, no questions asked)
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="state"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_std_enh_request_handler")]
        public static extern int StdEnhRequestHandler(SpeexBits* bits, void* state, void* data);


        /// <summary>
        /// Standard handler for high mode request (change high mode, no questions asked)
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="state"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_std_high_mode_request_handler")]
        public static extern int StdHightModeRequestHandler(SpeexBits* bits, void* state, void* data);


        /// <summary>
        /// Standard handler for low mode request (change low mode, no questions asked)
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="state"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_std_low_mode_request_handler")]
        public static extern int StdLowModeRequestHandler(SpeexBits* bits, void* state, void* data);


        /// <summary>
        /// Standard handler for mode request (change mode, no questions asked)
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="state"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_std_mode_request_handler")]
        public static extern int StdModeRequestHandler(SpeexBits* bits, void* state, void* data);


        /// <summary>
        /// Standard handler for VBR quality request (Set VBR quality, no questions asked)
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="state"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_std_vbr_quality_request_handler")]
        public static extern int StdVbrQualityRequestHandler(SpeexBits* bits, void* state, void* data);


        /// <summary>
        /// Standard handler for VBR request (Set VBR, no questions asked)
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="state"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_std_vbr_request_handler")]
        public static extern int StdVbrRequestHandler(SpeexBits* bits, void* state, void* data);

        #endregion

        #region Hader

        /// <summary>
        /// Creates the header packet from the header itself (mostly involves endianness conversion)
        /// </summary>
        /// <param name="header"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_header_to_packet")]
        public static extern byte* HeaderToPacket(SpeexHeader* header, int* size);


        /// <summary>
        /// Initializes a SpeexHeader using basic information
        /// </summary>
        /// <param name="header"></param>
        /// <param name="rate"></param>
        /// <param name="channelCount"></param>
        /// <param name="mode"></param>
        [DllImport(DllName, EntryPoint = "speex_init_header")]
        public static extern void InitHeader(SpeexHeader* header, int rate, int channelCount, [In] SpeexMode* mode);


        /// <summary>
        /// Creates a SpeexHeader from a packet
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="size"></param>
        [DllImport(DllName, EntryPoint = "speex_packet_to_header")]
        public static extern void PacketToHeader(byte* packet, int size);


        /// <summary>
        /// Frees the memory allocated by either speex_header_to_packet() or speex_packet_to_header()
        /// </summary>
        /// <param name="ptr"></param>
        [DllImport(DllName, EntryPoint = "speex_header_free")]
        public static extern void HeaderFree(void* ptr);

        #endregion

        #region Lib

        /// <summary>
        /// Functions for controlling the behavior of libspeex
        /// </summary>
        /// <param name="request">ioctl-type request (one of the SPEEX_LIB_∗ macros)</param>
        /// <param name="ptr">Data exchanged to-from function</param>
        /// <returns>0 if no error, -1 if request in unknown, -2 for invalid parameter</returns>
        [DllImport(DllName, EntryPoint = "speex_lib_ctl")]
        public static extern int LibCtl(int request, void* ptr);


        /// <summary>
        /// Obtain one of the modes available
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_lib_get_mode")]
        public static extern SpeexMode* LibGetMode(int mode);


        /// <summary>
        /// Query function for mode information
        /// </summary>
        /// <param name="mode">Speex mode</param>
        /// <param name="request">ioctl-type request (one of the SPEEX_∗ macros)</param>
        /// <param name="ptr">Data exchanged to-from function</param>
        /// <returns>0 if no error, -1 if request in unknown, -2 for invalid parameter</returns>
        [DllImport(DllName, EntryPoint = "speex_mode_query")]
        public static extern int ModeQuery([In] SpeexMode* mode, int request, void* ptr);

        #endregion

        #region Variables

        /// <summary>
        /// Get nb speex mode
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_get_nb_mode")]
        private static extern SpeexMode* GetNbMode();

        /// <summary>
        /// Get wb speex mode
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_get_wb_mode")]
        private static extern SpeexMode* GetWbMode();

        /// <summary>
        /// Get uwb speex mode
        /// </summary>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "speex_get_uwb_mode")]
        private static extern SpeexMode* GetUwbMode();


        static SpeexMode* s_nbMode = null;
        static SpeexMode* s_wbMode = null;
        static SpeexMode* s_uwbMode = null;

        /// <summary>
        /// 8000Hz, 160
        /// </summary>
        public static SpeexMode* NbMode
        {
            get
            {
                if (s_nbMode == null)
                {
                    s_nbMode = CompatibilityMode ? LibGetMode(0) : GetNbMode();
                }

                return s_nbMode;
            }
        }

        /// <summary>
        /// 16000Hz, 320
        /// </summary>
        public static SpeexMode* WbMode
        {
            get
            {
                if (s_wbMode == null)
                {
                    s_wbMode = CompatibilityMode ? LibGetMode(1) : GetWbMode();
                }

                return s_wbMode;
            }
        }

        /// <summary>
        /// 32000Hz, 640
        /// </summary>
        public static SpeexMode* UwbMode
        {
            get
            {
                if (s_uwbMode == null)
                {
                    s_uwbMode = CompatibilityMode ? LibGetMode(2) : GetUwbMode();
                }

                return s_uwbMode;
            }
        }


        #endregion
    }
}