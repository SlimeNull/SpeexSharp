using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SpeexNative = SpeexSharp.Native;

namespace SpeexSharp
{
    public unsafe class SpeexEncoder : IDisposable
    {
        SpeexNative.SpeexBits* _bits;
        void* _encoderState;

        int _frameSize;

        int _quality = 8;
        bool _disposed;

        public SpeexEncoder(SpeexMode mode)
        {
            if (!Enum.IsDefined<SpeexMode>(mode))
                throw new ArgumentException("Invalid SpeexMode", nameof(mode));

            _bits = (SpeexNative.SpeexBits*)NativeMemory.Alloc((nuint)sizeof(SpeexNative.SpeexBits));

            var nativeMode = SpeexUtils.GetNativeMode(mode);
            SpeexNative.Speex.BitsInit(_bits);
            _encoderState = SpeexNative.Speex.EncoderInit(nativeMode);

            SpeexxMode = mode;
            FrameSize = GetIntParameter(SpeexNative.GetCoderParameter.FrameSize);
        }

        ~SpeexEncoder()
        {
            Dispose(false);
        }

        public SpeexMode SpeexxMode { get; }
        public int FrameSize { get; }

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
            SpeexNative.Speex.EncoderCtl(_encoderState, (int)parameter, &value);
        }

        private int GetIntParameter(SpeexNative.GetCoderParameter parameter)
        {
            int result = 0;

            SpeexNative.Speex.EncoderCtl(_encoderState, (int)parameter, &result);
            return result;
        }

        private void SetFloatParameter(SpeexNative.SetCoderParameter parameter, float value)
        {
            SpeexNative.Speex.EncoderCtl(_encoderState, (int)parameter, &value);
        }

        private float GetFloatParameter(SpeexNative.GetCoderParameter parameter)
        {
            float result = 0;

            SpeexNative.Speex.EncoderCtl(_encoderState, (int)parameter, &result);
            return result;
        }

        public int Encode(ReadOnlySpan<float> samples)
        {
            EnsureNotDisposed();
            EnsureFrameSize(samples.Length, nameof(samples));

            fixed (float* samplesPtr = samples)
            {
                SpeexNative.Speex.BitsReset(_bits);
                SpeexNative.Speex.Encode(_encoderState, samplesPtr, _bits);

                return (_bits->BitCount + 7) >> 3;
            }
        }

        public int EncodeInt(ReadOnlySpan<short> samples)
        {
            EnsureNotDisposed();
            EnsureFrameSize(samples.Length, nameof(samples));

            fixed (short* samplesPtr = samples)
            {
                SpeexNative.Speex.BitsReset(_bits);
                SpeexNative.Speex.EncodeInt(_encoderState, samplesPtr, _bits);
                return (_bits->BitCount + 7) >> 3;
            }
        }

        public int Encode(float[] samples, int start, int length)
            => Encode(new ReadOnlySpan<float>(samples, start, length));

        public int EncodeInt(short[] samples, int start, int length)
            => EncodeInt(new ReadOnlySpan<short>(samples, start, length));

        public int Write(byte[] buffer, int startIndex, int maxLength)
        {
            EnsureNotDisposed();

            if (maxLength + startIndex > buffer.Length)
                throw new ArgumentOutOfRangeException("Max length is too large", nameof(maxLength));

            fixed (byte* bufferPtr = buffer)
            {
                return SpeexNative.Speex.BitsWrite(_bits, bufferPtr + startIndex, maxLength);
            }
        }

        public int Write(Stream dest, int maxLength)
        {
            if (!dest.CanWrite)
                throw new ArgumentException("Stream is not writable", nameof(dest));

            int length = maxLength;
            int bitsDataLength = ((_bits->BitCount) + 7) >> 3;
            if (length > bitsDataLength)
                length = bitsDataLength;

            Span<byte> buffer = new Span<byte>(_bits->Data, length);

            dest.Write(buffer);

            return length;
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }

            NativeMemory.Free(_bits);
            SpeexNative.Speex.EncoderDestroy(_encoderState);

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
