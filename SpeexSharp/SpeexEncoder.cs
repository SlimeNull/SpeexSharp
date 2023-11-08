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
    /// <summary>
    /// Speex encoder
    /// </summary>
    public unsafe class SpeexEncoder : IDisposable
    {
        SpeexNative.SpeexBits* _bits;
        void* _encoderState;

        int _quality = 8;
        bool _disposed;

        /// <summary>
        /// Create an instance of SpeexEncoder
        /// </summary>
        /// <param name="mode"></param>
        /// <exception cref="ArgumentException">Invalid SpeexMode</exception>
        public SpeexEncoder(SpeexMode mode)
        {
            if (!Enum.IsDefined<SpeexMode>(mode))
                throw new ArgumentException("Invalid SpeexMode", nameof(mode));

            _bits = (SpeexNative.SpeexBits*)NativeMemory.Alloc((nuint)sizeof(SpeexNative.SpeexBits));

            var nativeMode = SpeexUtils.GetNativeMode(mode);
            SpeexNative.Speex.BitsInit(_bits);
            _encoderState = SpeexNative.Speex.EncoderInit(nativeMode);

            SpeexMode = mode;
            FrameSize = GetIntParameter(SpeexNative.GetCoderParameter.FrameSize);
        }

        /// <summary>
        /// Dispose the current encoder
        /// </summary>
        ~SpeexEncoder()
        {
            Dispose(false);
        }

        /// <summary>
        /// Speex mode
        /// </summary>
        public SpeexMode SpeexMode { get; }

        /// <summary>
        /// The number of samples per frame for the current mode
        /// </summary>
        public int FrameSize { get; }

        /// <summary>
        /// The encoder speech quality (int32 from 0 to 10, default is 8)
        /// </summary>
        public int Quality
        {
            get => _quality;
            set => SetIntParameter(SpeexNative.SetCoderParameter.Quality, _quality = value);
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
        /// Variable bit-rate (VBR) status
        /// </summary>
        public bool Vbr
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Vbr) != 0;
            set => SetIntParameter(SpeexNative.SetCoderParameter.Vbr, value ? 1 : 0);
        }

        /// <summary>
        /// The current encoder VBR speech quality (float 0 to 10)
        /// </summary>
        public float VbrQuality
        {
            get => GetFloatParameter(SpeexNative.GetCoderParameter.VbrQuality);
            set => SetFloatParameter(SpeexNative.SetCoderParameter.VbrQuality, value);
        }

        /// <summary>
        /// The CPU resources allowed for the encoder (int32 from 1 to 10, default is 2)
        /// </summary>
        public int Complexity
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Complexity);
            set => SetIntParameter(SpeexNative.SetCoderParameter.Complexity, value);
        }

        /// <summary>
        /// The current bit-rate in use (int32 in bits per second)
        /// </summary>
        public int BitRate
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.BitRate);
            set => SetIntParameter(SpeexNative.SetCoderParameter.BitRate, value);
        }

        /// <summary>
        /// Real sampling rate (int32 in Hz)
        /// </summary>
        public int SamplingRate
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.SamplingRate);
            set => SetIntParameter(SpeexNative.SetCoderParameter.SamplingRate, value);
        }

        /// <summary>
        /// Voice activity detection (VAD) status (int32)
        /// </summary>
        public bool Vad
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Vad) != 0;
            set => SetIntParameter(SpeexNative.SetCoderParameter.Vad, value ? 1 : 0);
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
        /// Average bit-rate (ABR) setting (int32 in bits per second)
        /// </summary>
        public int Abr
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.Abr);
            set => SetIntParameter(SpeexNative.SetCoderParameter.Abr, value);
        }

        /// <summary>
        /// Submode encoding in each frame
        /// </summary>
        public int SubModeEncoding
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.SubModeEncoding);
            set => SetIntParameter(SpeexNative.SetCoderParameter.SubModeEncoding, value);
        }

        /// <summary>
        /// The current tuning of the encoder for PLC (int32 in percent)
        /// </summary>
        public int PlcTuning
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.PlcTuning);
            set => SetIntParameter(SpeexNative.SetCoderParameter.PlcTuning, value);
        }

        /// <summary>
        /// The current maximum bit-rate allowed in VBR operation (int32 in bits per second)
        /// </summary>
        public int VbrMaxBitRate
        {
            get => GetIntParameter(SpeexNative.GetCoderParameter.VbrMaxBitRate);
            set => SetIntParameter(SpeexNative.SetCoderParameter.VbrMaxBitRate, value);
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

        /// <summary>
        /// Get VBR info (mostly used internally)
        /// </summary>
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
            EnsureNotDisposed();

            SpeexNative.Speex.EncoderCtl(_encoderState, (int)parameter, &value);
        }

        private int GetIntParameter(SpeexNative.GetCoderParameter parameter)
        {
            EnsureNotDisposed();

            int result = 0;
            SpeexNative.Speex.EncoderCtl(_encoderState, (int)parameter, &result);
            return result;
        }

        private void SetFloatParameter(SpeexNative.SetCoderParameter parameter, float value)
        {
            EnsureNotDisposed();

            SpeexNative.Speex.EncoderCtl(_encoderState, (int)parameter, &value);
        }

        private float GetFloatParameter(SpeexNative.GetCoderParameter parameter)
        {
            EnsureNotDisposed();

            float result = 0;
            SpeexNative.Speex.EncoderCtl(_encoderState, (int)parameter, &result);
            return result;
        }

        /// <summary>
        /// Encode a frame
        /// </summary>
        /// <param name="samples">Samples for encoding</param>
        /// <returns>Encoded data size</returns>
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

        /// <summary>
        /// Encode a frame
        /// </summary>
        /// <param name="samples">Samples for encoding</param>
        /// <returns>Encoded data size</returns>
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

        /// <summary>
        /// Encode a frame
        /// </summary>
        /// <param name="samples">Samples buffer</param>
        /// <param name="start">Samples start index</param>
        /// <param name="length">Samples count</param>
        /// <returns>Encoded data size</returns>
        public int Encode(float[] samples, int start, int length)
            => Encode(new ReadOnlySpan<float>(samples, start, length));

        /// <summary>
        /// Encode a frame
        /// </summary>
        /// <param name="samples">Samples buffer</param>
        /// <param name="start">Samples start index</param>
        /// <param name="length">Samples count</param>
        /// <returns>Encoded data size</returns>
        public int EncodeInt(short[] samples, int start, int length)
            => EncodeInt(new ReadOnlySpan<short>(samples, start, length));

        /// <summary>
        /// Write the encoded data
        /// </summary>
        /// <param name="dest">Writing destination</param>
        /// <returns>Written data size</returns>
        public int Write(Span<byte> dest)
        {
            EnsureNotDisposed();

            fixed (byte* bufferPtr = dest)
            {
                return SpeexNative.Speex.BitsWrite(_bits, bufferPtr, dest.Length);
            }
        }

        /// <summary>
        /// Write the encoded data
        /// </summary>
        /// <param name="buffer">Writing destination buffer</param>
        /// <param name="start">Start index</param>
        /// <param name="maxLength">Max writing length</param>
        /// <returns>Written data size</returns>
        /// <exception cref="ArgumentOutOfRangeException">Max length is too large</exception>
        public int Write(byte[] buffer, int start, int maxLength)
        {
            if (maxLength + start > buffer.Length)
                throw new ArgumentOutOfRangeException("Max length is too large", nameof(maxLength));

            return Write(new Span<byte>(buffer, start, maxLength));
        }

        /// <summary>
        /// Write the encoded data
        /// </summary>
        /// <param name="dest">Writing destination</param>
        /// <param name="maxLength">Max writing length</param>
        /// <returns>Written data size</returns>
        /// <exception cref="ArgumentException">Stream is not writable</exception>
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

        /// <summary>
        /// Dispose the current encoder
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
