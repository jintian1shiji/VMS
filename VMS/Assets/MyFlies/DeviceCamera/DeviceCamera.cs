using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace HKCamera
{
    public class DeviceCamera : IDeviceCamera
    {
        private CameraInfo m_stCameraInfo;

        private bool m_bInitSDK = false;
        private Int32 m_lUserID = -1;
        private Int32 m_lRealHandle = -1;
        private Int32 m_lPort = -1;
        CHCNetSDK.REALDATACALLBACK RealData = null; //必须得定义为成员变量



        private byte[] sourceDataFrame;

        private bool m_IsTransfor;

        private Thread GetPicData;

        private float _farmeTime;

        //设置播放分辨率
        public Resolution? PlayResolution { get; set; }

        public Queue<byte[]> DataOutput { get; set; }

        public bool InitCamera(CameraInfo stInfo)
        {
            m_stCameraInfo = stInfo;
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (!m_bInitSDK)
            {
                uint nError = CHCNetSDK.NET_DVR_GetLastError();
            }
            CHCNetSDK.NET_DVR_SetConnectTime(5000, 1);
            CHCNetSDK.NET_DVR_SetReconnect(10000, 1);
            if (m_bInitSDK == false)
            {
                Debug.Log("NET_DVR_Init error!");
                return false;
            }
            else
            {
                //保存SDK日志 To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
            }

            string DVRIPAddress = stInfo.strIP;     //设备IP地址或者域名 Device IP
            Int16 DVRPortNumber = stInfo.nPort;     //设备服务端口号 Device Port
            string DVRUserName = stInfo.strUserName;//设备登录用户名 User name to login
            string DVRPassword = stInfo.strPassword;//设备登录密码 Password to login

            CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

            m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
            if (m_lUserID < 0)
            {
                Debug.Log("登录失败！");
                CHCNetSDK.NET_DVR_Cleanup();
                return false;
            }
            Debug.Log("登录成功！");
            return true;
        }

        public void StartCamera()
        { 
            //
            CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
            lpPreviewInfo.hPlayWnd = (IntPtr)null;
            lpPreviewInfo.lChannel = 1;
            lpPreviewInfo.dwStreamType = 0;       //码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
            lpPreviewInfo.dwLinkMode = 0;         //连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
            lpPreviewInfo.bBlocked = true;        //0- 非阻塞取流，1- 阻塞取流
            lpPreviewInfo.dwDisplayBufNum = 15;   //播放库播放缓冲区最大缓冲帧数

            //使用回调函数取摄像头数据
            RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
            IntPtr pUser = new IntPtr();//用户数据

            m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, RealData, pUser);
            //CHCNetSDK.NET_DVR_RigisterDrawFun(m_lRealHandle, new CHCNetSDK.DRAWFUN(cbDrawFun), 0);//回调函数：绘制图标
            if(m_lRealHandle >= 0)
            {
                Debug.Log("开始预览");
                StartTransfor();
            }
        }

        private uint nLastErr = 0;
        private static PlayCtrl.DECCBFUN m_fDisplayFun = null;
        private IntPtr m_ptrRealHandle;
        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            //下面数据处理建议使用委托的方式
            
            switch (dwDataType)
            {
                case CHCNetSDK.NET_DVR_SYSHEAD:     // sys head
                    if (dwBufSize > 0)
                    {
                        if (m_lPort >= 0)
                            return; //同一路码流不需要多次调用开流接口

                        //获取播放句柄 Get the port to play
                        if (!PlayCtrl.PlayM4_GetPort(ref m_lPort))
                        {
                            nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
                            break;
                        }

                        //设置流播放模式 Set the stream mode: real-time stream mode
                        if (!PlayCtrl.PlayM4_SetStreamOpenMode(m_lPort, PlayCtrl.STREAME_REALTIME))
                        {
                            nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
                            //str = "Set STREAME_REALTIME mode failed, error code= " + nLastErr;
                            //this.BeginInvoke( AlarmInfo, str );
                        }

                        //打开码流，送入头数据 Open stream
                        if (!PlayCtrl.PlayM4_OpenStream(m_lPort, pBuffer, dwBufSize, 2 * 1024 * 1024))
                        {
                            nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
                            //str = "PlayM4_OpenStream failed, error code= " + nLastErr;
                            //this.BeginInvoke( AlarmInfo, str );
                            break;
                        }

                        //设置显示缓冲区个数 Set the display buffer number
                        if (!PlayCtrl.PlayM4_SetDisplayBuf(m_lPort, 15))
                        {
                            nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
                            //str = "PlayM4_SetDisplayBuf failed, error code= " + nLastErr;
                            //this.BeginInvoke( AlarmInfo, str );
                        }

                        //设置显示模式 Set the display mode
                        //if ( !PlayCtrl.PlayM4_SetOverlayMode( m_lPort, 0, 0) ) //play off screen 
                        //{
                        //    nLastErr = PlayCtrl.PlayM4_GetLastError( m_lPort );
                        //    //str = "PlayM4_SetOverlayMode failed, error code= " + nLastErr;
                        //    //this.BeginInvoke( AlarmInfo, str );
                        //}

                        //设置解码回调函数，获取解码后音视频原始数据 Set callback function of decoded data
                        m_fDisplayFun = new PlayCtrl.DECCBFUN(DecCallbackFUN);
                        if (!PlayCtrl.PlayM4_SetDecCallBackEx(m_lPort, m_fDisplayFun, IntPtr.Zero, 0))
                        {
                            //this.BeginInvoke( AlarmInfo, "PlayM4_SetDisplayCallBack fail" );
                        }

                        //开始解码 Start to play                       
                        if (!PlayCtrl.PlayM4_Play(m_lPort, m_ptrRealHandle))
                        {
                            nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
                            //str = "PlayM4_Play failed, error code= " + nLastErr;
                            //this.BeginInvoke( AlarmInfo, str );
                            break;
                        }
                    }
                    break;
                case CHCNetSDK.NET_DVR_STREAMDATA: // video stream data
                default: //the other data
                    if (dwBufSize > 0 && m_lPort != -1)
                    {
                        for (int i = 0; i < 999; i++)
                        {
                            //送入码流数据进行解码 Input the stream data to decode
                            if (!PlayCtrl.PlayM4_InputData(m_lPort, pBuffer, dwBufSize))
                            {
                                nLastErr = PlayCtrl.PlayM4_GetLastError(m_lPort);
                                //str = "PlayM4_InputData failed, error code= " + nLastErr;
                                Thread.Sleep(10);
                            }
                            else
                                break;
                        }
                    }
                    break;
            }
            
        }

        //解码回调函数
        private void DecCallbackFUN(int nPort, IntPtr pBuf, int nSize, ref PlayCtrl.FRAME_INFO pFrameInfo, int nReserved1, int nReserved2)
        {
            if (m_lRealHandle >= 0)
            {
                if (pFrameInfo.nType == 3) //#define T_YV12   3
                {
                    if (PlayResolution == null)
                    {
                        var resolution = new Resolution();
                        resolution.width = pFrameInfo.nWidth;
                        resolution.height = pFrameInfo.nHeight;
                        resolution.refreshRate = pFrameInfo.nFrameRate;
                        PlayResolution = resolution;

                        DataOutput = new Queue<byte[]>();
                    }
                    if (sourceDataFrame == null)
                    {
                        sourceDataFrame = new byte[nSize];
                    }

                    Marshal.Copy(pBuf, sourceDataFrame, 0, nSize);
                }
            }
        }

        public void SetFarmeTime(float farmeTime)
        {
            _farmeTime = farmeTime;
        }

        private void StartTransfor()
        {
            if (!m_IsTransfor && m_lRealHandle > -1)
            {
                m_IsTransfor = true;

                GetPicData = new Thread(StartTransformData);
                GetPicData.IsBackground = true;
                GetPicData.Start();
                Debug.Log("开启解析线程");
            }
        }

        private void StartTransformData()
        {
            while (m_IsTransfor && m_lUserID >= 0)
            {
                if (sourceDataFrame != null && PlayResolution != null)
                {
                    var transforData = new byte[(long)PlayResolution.Value.width * PlayResolution.Value.height * 4];
                    if (!CommonFun.YV12_to_RGB32(sourceDataFrame, transforData, 
                        PlayResolution.Value.width, PlayResolution.Value.height))
                    {
                        Debug.Log("转换失败");
                    }
                    else
                    {
                        DataOutput.Enqueue(transforData);
                    }   
                }

                //float waitTime = Mathf.Max(16, _farmeTime * 1000);
                //Debug.Log(waitTime);
                //Thread.Sleep((int)waitTime);
            }
        }

        public void StopCamera()
        {
            if (m_IsTransfor)
            {
                m_IsTransfor = false;
                if (GetPicData != null)
                {
                    GetPicData.Abort();
                    GetPicData = null;
                }
                Debug.Log("停止解析线程");
                PlayResolution = null;
                DataOutput.Clear();
                sourceDataFrame = null;
            }
            if (m_lRealHandle >= 0)
            {
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    ViewErrorInfo("停止预览失败!", CHCNetSDK.NET_DVR_GetLastError());
                }
                else
                {
                    Debug.Log("停止预览");
                }
                sourceDataFrame = null;
                m_lRealHandle = -1;
            }
            
        }

        public void Dispose()
        {
            StopCamera();
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
                m_lUserID = -1;
            }
            if (m_bInitSDK)
            {
                CHCNetSDK.NET_DVR_Cleanup();
                m_bInitSDK = false;
            }
        }

        private void ViewErrorInfo(string errorMessage, uint errorCode)
        {
            Debug.Log(errorMessage + ", 错误代码: " + errorCode);
        }
    }
}
