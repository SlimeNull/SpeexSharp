// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SpeexSharp.Native;

var pcmFileName = "Oriens.pcm";
var outputSpeexFileName = "Oriens_output.speex";
var outputPcmFileName = "Oriens_output.pcm";

unsafe
{
    SpeexBits encodeBits = new SpeexBits();
    SpeexBits decodeBits = new SpeexBits();

    var mode = Speex.WbMode;
    var encoderState = Speex.EncoderInit(mode);
    var decoderState = Speex.DecoderInit(mode);

    Speex.BitsInit(&encodeBits);
    Speex.BitsInit(&decodeBits);

    byte[] pcmBytes = File.ReadAllBytes(pcmFileName);

    int paddedLength = GetPaddedLength(pcmBytes.Length / 2, 320);
    short[] samples = new short[paddedLength];
    for (int i = 0; i < pcmBytes.Length; i += 2)
        samples[i / 2] = BitConverter.ToInt16(pcmBytes, i);

    MemoryStream speexStream = new MemoryStream();

    using FileStream outputSpeex = File.Create(outputSpeexFileName);
    using FileStream outputPcm = File.Create(outputPcmFileName);
    short[] buffer = new short[320];

    fixed (short* samplesPtr = samples)
    {
        for (int i = 0; i < samples.Length; i += 320)
        {
            int end = Math.Min(i + 320, samples.Length);
            int len = end - i;

            Speex.BitsReset(&encodeBits);
            Speex.EncodeInt(encoderState, samplesPtr + i, &encodeBits);

            byte[] encodeBuffer = new byte[encodeBits.BitCount / 8];
            fixed (byte* encodeBufferPtr = encodeBuffer)
            {
                int writen = Speex.BitsWrite(&encodeBits, encodeBufferPtr, encodeBuffer.Length);

                if (writen > 255)
                    throw new InvalidOperationException();

                speexStream.WriteByte((byte)writen);
                outputSpeex.WriteByte((byte)writen);
                speexStream.Write(encodeBuffer, 0, writen);
                outputSpeex.Write(encodeBuffer, 0, writen);

                Console.WriteLine($"Encoded Once, encoded size: {writen}");
            }
        }
    }

    Console.WriteLine("Speex encode end");

    byte[] speexBytes = speexStream.ToArray();

    fixed (byte* speexBytesPtr = speexBytes)
    {
        fixed (short* bufferPtr = buffer)
        {
            int index = 0;
            int code = 0;

            while (index < speexBytes.Length)
            {
                int count = speexBytesPtr[index];
                index++;

                Speex.BitsReset(&decodeBits);
                Speex.BitsReadFrom(&decodeBits, speexBytesPtr + index, count);
                code = Speex.DecodeInt(decoderState, &decodeBits, bufferPtr);

                var outputSpan = new Span<byte>(bufferPtr, buffer.Length * 2);
                outputPcm.Write(outputSpan);

                index += count;

                Console.WriteLine($"Decode Once, size before: {count}");
            }
        }
    }

    Console.WriteLine("Pcm decode end");

    Console.WriteLine("OK");
}

int GetPaddedLength(int origin, int n)
{
    int padLength = origin % n;
    if (padLength != 0)
        padLength = n - padLength;

    return origin + padLength;
}