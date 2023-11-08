using System.Runtime.InteropServices;

using SpeexNative = SpeexSharp.Native;

namespace SpeexSharp
{
    /// <summary>
    /// Speex decoder
    /// </summary>
    public unsafe class SpeexDecoder : IDisposable
    {
        SpeexNative.SpeexBits* _bits;
        void* _decoderState;

        bool _disposed;

        /// <summary>
        /// Create an instance of SpeexDecoder
        /// </summary>
        /// <param name="mode"></param>
        /// <exception cref="ArgumentException">Invalid SpeexMode</exception>
        public SpeexDecoder(SpeexMode mode)
        {
            if (!Enum.IsDefined<SpeexMode>(mode))
                throw new ArgumentException("Invalid SpeexMode", nameof(mode));

            _bits = (SpeexNative.SpeexBits*)NativeMemory.Alloc((nuint)sizeof(SpeexNative.SpeexBits));

            var nativeMode = SpeexUtils.GetNativeMode(mode);
            SpeexNative.Speex.BitsInit(_bits);
            _decoderState = SpeexNative.Speex.DecoderInit(nativeMode);

            SpeexMode = mode;
            FrameSize = GetIntParameter(SpeexNative.GetCoderParameter.FrameSize);
        }

        /// <summary>
        /// Dispose the current decoder
        /// </summary>
        ~SpeexDecoder()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        public SpeexMode SpeexMode { get; }

        /// <summary>
        /// The number of samples per frame for the current mode
        /// </summary>
        public int FrameSize { get; }

        /// <summary>
        /// Perceptual enhancer status (decoder only)
        /// </summary>
        public bool Enh 
        { 
            get => GetIntParameter(SpeexNative.GetCoderParameter.Enh) != 0;
            set => SetIntParameter(SpeexNative.SetCoderParameter.Enh, value ? 1 : 0); 
        }

        /// <summary>
        /// Current low-band mode in use (wideband only)
        /// </summary>
        public int Mode
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Mode);
            set => SetIntParameter(SpeexNative.SetCoderParameter.Mode, value);
        }

        /// <summary>
        /// Current high-band mode in use (wideband only)
        /// </summary>
        public int LowMode
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.LowMode);
            set => SetIntParameter(SpeexNative.SetCoderParameter.LowMode, value);
        }

        /// <summary>
        /// Current high-band mode in use (wideband only)
        /// </summary>
        public int HighMode
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.HighMode);
            set => SetIntParameter(SpeexNative.SetCoderParameter.HighMode, value);
        }

        /// <summary>
        /// Discontinuous transmission (DTX) status (int32)
        /// </summary>
        public bool Dtx
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Dtx) != 0;
            set => SetIntParameter(SpeexNative.SetCoderParameter.Dtx, value ? 1 : 0);
        }

        /// <summary>
        /// Status of input/output high-pass filtering
        /// </summary>
        public bool HighPass
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.HighPass) != 0;
            set => SetIntParameter(SpeexNative.SetCoderParameter.HighPass, value ? 1 : 0);
        }

        /// <summary>
        /// Get "activity level" of the last decoded frame, i.e. now much damage we cause if we remove the frame
        /// </summary>
        public int Activity
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Activity);
        }

        /// <summary>
        /// Returns the lookahead used by Speex
        /// </summary>
        public int Lookahead
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Lookahead);
        }

        private void EnsureNotDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(null);
        }

        private void EnsureFrameSize(int frameSize, string argumentName)
        {
            if (frameSize != FrameSize)
                throw new ArgumentException("Size doesn't match the frame size", argumentName);
        }

        private void SetIntParameter(SpeexNative.SetCoderParameter parameter, int value)
        {
            EnsureNotDisposed();

            SpeexNative.Speex.EncoderCtl(_decoderState, (int)parameter, &value);
        }

        private int GetIntParameter(SpeexNative.GetCoderParameter parameter)
        {
            EnsureNotDisposed();

            int result = 0;
            SpeexNative.Speex.EncoderCtl(_decoderState, (int)parameter, &result);
            return result;
        }

        private void SetFloatParameter(SpeexNative.SetCoderParameter parameter, float value)
        {
            EnsureNotDisposed();

            SpeexNative.Speex.EncoderCtl(_decoderState, (int)parameter, &value);
        }

        private float GetFloatParameter(SpeexNative.GetCoderParameter parameter)
        {
            EnsureNotDisposed();

            float result = 0;
            SpeexNative.Speex.EncoderCtl(_decoderState, (int)parameter, &result);
            return result;
        }

        /// <summary>
        /// Decode a frame
        /// </summary>
        /// <param name="speex">Speex frame for decoding</param>
        /// <param name="output">Buffer for storing decoded frame</param>
        /// <returns>return status (0 for no error, -1 for end of stream, -2 corrupt stream)</returns>
        public int Decode(ReadOnlySpan<byte> speex, Span<float> output)
        {
            EnsureNotDisposed();
            EnsureFrameSize(output.Length, nameof(output));

            fixed (byte* speexPtr = speex)
            {
                fixed (float* outputPtr = output)
                {
                    SpeexNative.Speex.BitsReadFrom(_bits, speexPtr, speex.Length);
                    return SpeexNative.Speex.Decode(_decoderState, _bits, outputPtr);
                }
            }
        }

        /// <summary>
        /// Decode a frame
        /// </summary>
        /// <param name="speex">Speex frame for decoding</param>
        /// <param name="output">Buffer for storing decoded frame</param>
        /// <returns>return status (0 for no error, -1 for end of stream, -2 corrupt stream)</returns>
        public int DecodeInt(ReadOnlySpan<byte> speex, Span<short> output)
        {
            EnsureNotDisposed();
            EnsureFrameSize(output.Length, nameof(output));

            fixed (byte* speexPtr = speex)
            {
                fixed (short* outputPtr = output)
                {
                    SpeexNative.Speex.BitsReadFrom(_bits, speexPtr, speex.Length);
                    return SpeexNative.Speex.DecodeInt(_decoderState, _bits, outputPtr);
                }
            }
        }

        /// <summary>
        /// Decode a frame
        /// </summary>
        /// <param name="speex">Speex frame buffer</param>
        /// <param name="start">Start index of speex frame</param>
        /// <param name="length">Length of speex frame</param>
        /// <param name="output">Buffer for storing decoded frame</param>
        /// <returns></returns>
        /// <returns>return status (0 for no error, -1 for end of stream, -2 corrupt stream)</returns>
        public int Decode(byte[] speex, int start, int length, float[] output)
            => Decode(new ReadOnlySpan<byte>(speex, start, length), output);

        /// <summary>
        /// Decode a frame
        /// </summary>
        /// <param name="speex">Speex frame buffer</param>
        /// <param name="start">Start index of speex frame</param>
        /// <param name="length">Length of speex frame</param>
        /// <param name="output">Buffer for storing decoded frame</param>
        /// <returns></returns>
        /// <returns>return status (0 for no error, -1 for end of stream, -2 corrupt stream)</returns>
        public int EncodeInt(byte[] speex, int start, int length, short[] output)
            => DecodeInt(new ReadOnlySpan<byte>(speex, start, length), output);

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            NativeMemory.Free(_bits);
            SpeexNative.Speex.DecoderDestroy(_decoderState);

            _disposed = true;
        }

        /// <summary>
        /// Dispose the current decoder
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
