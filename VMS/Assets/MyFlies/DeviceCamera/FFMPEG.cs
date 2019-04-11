using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace HKCamera
{
    public class FFMPEG
    {
        //[DllImport("ffmpeg_for_unity", EntryPoint = "fnffmpeg_for_unity", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern string GetTestString();

        [DllImport("ffmpeg_for_unity_win32", EntryPoint = "fnffmpeg_for_unity", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int fnffmpeg_for_unity();



        //[DllImport("ffmpeg_for_unity")]
        //public static extern bool StartConvert(bool start_or_end, int src_width, int src_height, int dst_width, int dst_height);



        /// <summary>
        /// 
        /// </summary>
        /// <returns><c>true</c>, if convert updated was started, <c>false</c> otherwise.</returns>
        /// <param name="start_or_end">If set to <c>true</c> start or end.</param>
        /// <param name="src_width">Source width.</param>
        /// <param name="src_height">Source height.</param>
        /// <param name="dst_width">Dst width.</param>
        /// <param name="dst_height">Dst height.</param>
        /// <param name="src_type">Source Video type.</param>
        /// <param name="dst_type">Dst Video type.</param>
        [DllImport("ffmpeg_for_unity_win32")]
        public static extern bool StartConvert_Updated(int src_width, int src_height, int dst_width, int dst_height, int src_type, int dst_type);

        //[DllImport("ffmpeg_for_unity")]
        //public static extern bool YV12toRgb([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]byte[] pDst, byte[] pSrc, int nWidth, int nHeight, int targetWidth, int targetHeight);

        [DllImport("ffmpeg_for_unity_win32")]
        public static extern bool YV12toRgb_Updated([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]byte[] pDst, byte[] pSrc, int width, int height);
    }
}