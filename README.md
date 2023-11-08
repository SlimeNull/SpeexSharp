<div align="center">

<h1>SpeexSharp</h1>

.NET Wrapper for [Speex](https://speex.org) Codec


[![LICENSE](https://img.shields.io/github/license/EleChoNet/EleCho.GoCqHttpSdk)](/LICENSE)
[![nuget](https://img.shields.io/nuget/vpre/SpeexSharp)](https://www.nuget.org/packages/SpeexSharp)
[![nuget](https://img.shields.io/nuget/dt/SpeexSharp)](https://www.nuget.org/packages/SpeexSharp)

</div>

<br/><br/>

## Introduction

Speex encoding and decoding are performed frame by frame. The frame size varies when different Speex modes are used.

During encoding, the input data size must be equal to the frame size. Then, you will obtain the encoded frame.
During decoding, using this data will result in an output that is consistent with the frame size.

When Variable bit-rate is not enabled, the size of the encoded frame is fixed.
However, when it is enabled, the length of the encoded result becomes variable.
The simplest way to define your own storage format is to add a header before the encoded frame, indicating how many bytes of data are in one frame.

> You can view the TestConsole in this repository, which is a simple usage example.

<br/>

## Encoding

SpeexSharp has already encapsulated SpeexEncoder for encoding use.
You just need to create an instance of it and call its methods.
Memory management will be handled automatically.

The following is a simple process for encoding:

1. Convert the original audio to PCM format.
2. Fill the original sample to make it a multiple of the frame size.
3. Call the Encode method frame by frame, and then call the Write method to output the encoded results.
4. Perform your own logic, such as adding a header to frames and then saving them to a file.

Speex supports two types of PCM encoding: 32-bit floating point and 16-bit signed integer. 
If you want to perform floating point encoding, you need to call the Encode method. 
If it is 16-bit signed integer, you need to call the EncodeInt method.

> Note: It is best to keep the sample rate of the input audio consistent with the sample rate of the encoder in order to achieve the best encoding effect.

<br/>

## Decoding

SpeexSharp has already encapsulated SpeexDecoder for decoding use. 
You just need to create an instance of it and call its methods. 
Memory management will be handled automatically.

The following is a simple process for decoding:

1. Obtain a complete speex frame.
2. Call the Decode method, pass in the data, and get the decoded result.
3. Perform your own logic, such as concatenating the decoded data and saving it.

Speex allows decoding of lost or corrupted data.
Even if only half of the Speex frame is received or the content is modified, it can still be successfully decoded.
However, the audio quality will be significantly reduced.


<br/>

## Native APIs

In addition to using the encapsulated SpeexEncoder and SpeexDecoder, you can also directly import the pre-written native API in the library.
Most of the contents defined in libxspeex.dll have already been declared in this library.
