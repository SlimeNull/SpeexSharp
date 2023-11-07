using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeexSharp
{
    internal static unsafe class SpeexUtils
    {
        public static SpeexSharp.Native.SpeexMode* GetNativeMode(SpeexSharp.SpeexMode mode)
        {
            return mode switch
            {
                SpeexMode.Narrowband => SpeexSharp.Native.Speex.NbMode,
                SpeexMode.Wideband => SpeexSharp.Native.Speex.WbMode,
                SpeexMode.UltraWideband => SpeexSharp.Native.Speex.UwbMode,

                _ => null,
            };
        }

        //public static int GetFrameSize(SpeexSharp.SpeexMode mode)
        //{
        //    return mode switch
        //    {
        //        SpeexMode.Narrowband => 160,
        //        SpeexMode.Wideband => 320,
        //        SpeexMode.UltraWideband => 640,

        //        _ => throw new ArgumentException("Invalid SpeexMode", nameof(mode)),
        //    };
        //}
    }
}
