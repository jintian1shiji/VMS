using System.Collections.Generic;
using UnityEngine;

namespace HKCamera
{
    public struct CameraInfo
    {
        public string strIP;
        public short nPort;
        public string strUserName;
        public string strPassword;
    }

    public interface IDeviceCamera
    {
        Resolution? PlayResolution { get; set; }

        Queue<byte[]> DataOutput { get; set; }

        bool InitCamera(CameraInfo stInfo);

        void SetFarmeTime(float farmeTime);

        void StartCamera();

        void StopCamera();

        void Dispose();
    }
}
