using System.Runtime.InteropServices;

using SpeexNative = SpeexSharp.Native;

namespace SpeexSharp
{
    public unsafe class SpeexDecoder : IDisposable
    {
        SpeexNative.SpeexBits* _bits;
        void* _decoderState;

        int _frameSize;

        int _quality = 8;
        bool _disposed;

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

        ~SpeexDecoder()
        {
            Dispose(false);
        }

        public SpeexMode SpeexMode { get; }
        public int FrameSize { get; }

        public bool Enh 
        { 
            get => GetIntParameter(SpeexNative.GetCoderParameter.Enh) != 0;
            set => SetIntParameter(SpeexNative.SetCoderParameter.Enh, value ? 1 : 0); 
        }

        public int Quality
        {
            get => _quality;
            set => SetIntParameter(SpeexNative.SetCoderParameter.Quality, _quality = value);
        }

        public int Mode
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Mode);
            set => SetIntParameter(SpeexNative.SetCoderParameter.Mode, value);
        }

        public int LowMode
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.LowMode);
            set => SetIntParameter(SpeexNative.SetCoderParameter.LowMode, value);
        }

        public int HighMode
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.HighMode);
            set => SetIntParameter(SpeexNative.SetCoderParameter.HighMode, value);
        }

        public int Vbr
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Vbr);
            set => SetIntParameter(SpeexNative.SetCoderParameter.Vbr, value);
        }

        public float VbrQuality
        {
            get => GetFloatParameter(SpeexNative.GetCoderParameter.VbrQuality);
            set => SetFloatParameter(SpeexNative.SetCoderParameter.VbrQuality, value);
        }

        public int Complexity
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Complexity);
            set => SetIntParameter(SpeexNative.SetCoderParameter.Complexity, value);
        }

        public int BitRate
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.BitRate);
            set => SetIntParameter(SpeexNative.SetCoderParameter.BitRate, value);
        }

        public int SamplingRate
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.SamplingRate);
            set => SetIntParameter(SpeexNative.SetCoderParameter.SamplingRate, value);
        }

        public int Vad
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Vad);
            set => SetIntParameter(SpeexNative.SetCoderParameter.Vad, value);
        }

        public int Abr
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Abr);
            set => SetIntParameter(SpeexNative.SetCoderParameter.Abr, value);
        }

        public int Dtx
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Dtx);
            set => SetIntParameter(SpeexNative.SetCoderParameter.Dtx, value);
        }

        public int SubModeEncoding
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.SubModeEncoding);
            set => SetIntParameter(SpeexNative.SetCoderParameter.SubModeEncoding, value);
        }

        public int PlcTuning
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.PlcTuning);
            set => SetIntParameter(SpeexNative.SetCoderParameter.PlcTuning, value);
        }

        public int VbrMaxBitRate
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.VbrMaxBitRate);
            set => SetIntParameter(SpeexNative.SetCoderParameter.VbrMaxBitRate, value);
        }

        public int HighPass
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.HighPass);
            set => SetIntParameter(SpeexNative.SetCoderParameter.HighPass, value);
        }

        public int Activity
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Activity);
        }

        public int Lookahead
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Lookahead);
        }

        public int RelativeQuality
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.RelativeQuality);
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
            SpeexNative.Speex.EncoderCtl(_decoderState, (int)parameter, &value);
        }

        private int GetIntParameter(SpeexNative.GetCoderParameter parameter)
        {
            int result = 0;

            SpeexNative.Speex.EncoderCtl(_decoderState, (int)parameter, &result);
            return result;
        }

        private void SetFloatParameter(SpeexNative.SetCoderParameter parameter, float value)
        {
            SpeexNative.Speex.EncoderCtl(_decoderState, (int)parameter, &value);
        }

        private float GetFloatParameter(SpeexNative.GetCoderParameter parameter)
        {
            float result = 0;

            SpeexNative.Speex.EncoderCtl(_decoderState, (int)parameter, &result);
            return result;
        }

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

        public int Decode(byte[] speex, int start, int length, float[] output)
            => Decode(new ReadOnlySpan<byte>(speex, start, length), output);

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

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
