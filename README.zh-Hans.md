<div align="center">

<h1>SpeexSharp</h1>

[Speex](https://speex.org) 编解码器的 .NET 包装

</div>

<br/><br/>

## 介绍

Speex 编码和解码是逐帧进行的。当使用不同的 Speex 模式时，帧的大小会有所变化。

在编码期间，输入数据大小必须等于帧大小。然后，您将获得编码后的帧。
在解码期间，使用此数据将得到与帧大小一致的输出。

当未启用可变比特率时，编码后的帧的大小是固定的。
但是，当启用可变比特率时，编码结果的长度变为可变。
定义自己的存储格式的最简单方法是在编码帧之前添加一个标头，指示一个帧中有多少字节的数据。

> 您可以在此存储库中查看 TestConsole，这是一个简单的用法示例。

<br/>

## 编码

SpeexSharp 已经封装了 SpeexEncoder 以进行编码。
您只需要创建其实例并调用其方法即可。
内存管理将自动处理。

以下是简单的编码过程：

1. 将原始音频转换为 PCM 格式。
2. 填充原始样本，使其成为帧大小的倍数。
3. 逐帧调用 Encode 方法，然后调用 Write 方法输出编码结果。
4. 执行您自己的逻辑，例如添加标头到帧中，然后将其保存到文件中。

Speex 支持两种 PCM 编码类型：32 位浮点和 16 位有符号整数。
如果要执行浮点编码，则需要调用 Encode 方法。
如果是16位有符号整数，则需要调用 EncodeInt 方法。

> 注意：最好使输入音频的采样率与编码器的采样率保持一致，以实现最佳编码效果。

<br/>

## 解码

SpeexSharp 已经封装了 SpeexDecoder 以进行解码。
您只需要创建其实例并调用其方法即可。
内存管理将自动处理。

以下是简单的解码过程：

1. 获取完整的Speex帧。
2. 调用Decode方法，传入数据并获取解码结果。
3. 执行您自己的逻辑，例如连接解码数据并保存。

Speex 允许解码丢失或损坏的数据。
即使只收到 Speex 帧的一半或内容被修改，仍然可以成功解码。
但是，音频质量将显著降低。

<br/>

## 原生 API

除了使用封装好的 SpeexEncoder 和 SpeexDecoder, 你也可以直接使用库中已经编写好的原生 API 导入. 
大部分 libxspeex.dll 中定义的内容都已经在本库中声明.