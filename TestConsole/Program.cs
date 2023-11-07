using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SpeexSharp;
using SpeexSharp.Native;

var pcmFileName = "Oriens.pcm";
var outputSpeexFileName = "Oriens_output.speex";
var outputPcmFileName = "Oriens_output.pcm";

unsafe
{
    // speex mode
    var mode = SpeexSharp.SpeexMode.Narrowband;

    // init encoder and decoder
    using var encoder = new SpeexEncoder(mode);
    using var decoder = new SpeexDecoder(mode);

    // read test file content
    byte[] pcmBytes = File.ReadAllBytes(pcmFileName);


    // convert to samples
    int paddedLength = GetPaddedLength(pcmBytes.Length / 2, encoder.FrameSize);
    short[] samples = new short[paddedLength];
    for (int i = 0; i < pcmBytes.Length; i += 2)
        samples[i / 2] = BitConverter.ToInt16(pcmBytes, i);


    // save encoded content for decoding
    MemoryStream speexStream = new MemoryStream();


    // output files
    using FileStream outputSpeex = File.Create(outputSpeexFileName);
    using FileStream outputPcm = File.Create(outputPcmFileName);


    // encode
    for (int i = 0; i < samples.Length; i += encoder.FrameSize)
    {
        Span<short> inputSpan = new Span<short>(samples, i, encoder.FrameSize);
        int count = encoder.EncodeInt(inputSpan);

        byte[] encodeBuffer = new byte[count];
        encoder.Write(encodeBuffer, 0, encodeBuffer.Length);

        speexStream.WriteByte((byte)count);
        outputSpeex.WriteByte((byte)count);
        speexStream.Write(encodeBuffer, 0, count);
        outputSpeex.Write(encodeBuffer, 0, count);

        Console.WriteLine($"Encode Once, size after: {count}");
    }


    // output message
    Console.WriteLine("Speex encode end");


    // get encoded bytes
    byte[] speexBytes = speexStream.ToArray();


    // decode
    // encode and decode buffer
    short[] frame = new short[decoder.FrameSize];
    for (int index = 0; index < speexBytes.Length; )
    {
        int count = speexBytes[index];
        index++;

        Span<byte> inputSpan = new Span<byte>(speexBytes, index, count);
        Span<short> outputSpan = new Span<short>(frame);
        decoder.DecodeInt(inputSpan, outputSpan);

        Span<byte> outputByteSpan = MemoryMarshal.Cast<short, byte>(outputSpan);
        outputPcm.Write(outputByteSpan);

        index += count;

        Console.WriteLine($"Decode Once, size before: {count}");
    }


    // output message
    Console.WriteLine("Pcm decode end");


    // ok
    Console.WriteLine("OK");
}

int GetPaddedLength(int origin, int n)
{
    int padLength = origin % n;
    if (padLength != 0)
        padLength = n - padLength;

    return origin + padLength;
}