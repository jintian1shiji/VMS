using System.Linq;
using UnityEngine;

public class CommonFun
{
    public static bool YV12_to_RGB32(byte[] buffYV12, byte[] bufferRGB32, int nWidth, int nHeight)
    {
        if (buffYV12.Length < 0 || bufferRGB32.Length < 0)
            return false;

        long nYLen = (long)nWidth * nHeight;
        int nHfWidth = (nWidth >> 1);

        if (nYLen < 1 || nHfWidth < 1)
            return false;

        byte[] byteYData = buffYV12.Skip(0).Take(nWidth * nHeight).ToArray();
        byte[] byteUData = buffYV12.Skip(nWidth * nHeight).Take((nHeight / 2) * (nWidth / 2)).ToArray();
        byte[] byteVData = buffYV12.Skip(nWidth * nHeight + (nHeight / 2) * (nWidth / 2)).Take((nHeight / 2) * (nWidth / 2)).ToArray();

        if (byteYData.Length < 0 || byteVData.Length < 0 || byteUData.Length < 0)
            return false;

        int[] nRgb = new int[4];
        for (int nRow = 0; nRow < nHeight; nRow++)
        {
            for (int nCol = 0; nCol < nWidth; nCol++)
            {
                int Y = byteYData[nRow * nWidth + nCol];
                int U = byteUData[(nRow / 2) * (nWidth / 2) + (nCol / 2)];
                int V = byteVData[(nRow / 2) * (nWidth / 2) + (nCol / 2)];
                int R = Y + (U - 128) + (((U - 128) * 103) >> 8);
                int G = Y - (((V - 128) * 88) >> 8) - (((U - 128) * 183) >> 8);
                int B = Y + (V - 128) + (((V - 128) * 198) >> 8);

                // r分量值 
                R = R < 0 ? 0 : R;
                nRgb[2] = R > 255 ? 255 : R;

                // g分量值
                G = G < 0 ? 0 : G;
                nRgb[1] = G > 255 ? 255 : G;

                // b分量值 
                B = B < 0 ? 0 : B;
                nRgb[0] = B > 255 ? 255 : B;

                //A分量值
                nRgb[3] = 255;

                //Out RGB Buffer
                bufferRGB32[4 * (nRow * nWidth + nCol) + 0] = (byte)nRgb[0];
                bufferRGB32[4 * (nRow * nWidth + nCol) + 1] = (byte)nRgb[1];
                bufferRGB32[4 * (nRow * nWidth + nCol) + 2] = (byte)nRgb[2];
                bufferRGB32[4 * (nRow * nWidth + nCol) + 3] = (byte)nRgb[3];
            }
        }
        return true;
    }

    //public static Bitmap RGB32_to_Image(byte[] byteBuff, int nWidth, int nHeight)
    //{
    //    if (byteBuff.Length <= 0 || byteBuff.Length < nWidth * nHeight)
    //        return null;
    //    Bitmap bmp = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb);

    //    //锁定内存数据
    //    BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, nWidth, nHeight), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

    //    //输入颜色数据
    //    System.Runtime.InteropServices.Marshal.Copy(byteBuff, 0, bmpData.Scan0, byteBuff.Length);

    //    //解锁
    //    bmp.UnlockBits(bmpData);

    //    return bmp;
    //}
}