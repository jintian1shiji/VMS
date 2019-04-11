using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using CameraSDK;

public enum VIDEODECODEMODEL
{
    SDK_PLAYMODE,
    PLAYM4_MODE
}

public struct DataContentType
{
    public byte[] data;

    public DataContentType(byte[] target)
    {
        data = target;
    }
}


public class TestMedia_FFmpeg : MonoBehaviour
{
    public static TestMedia_FFmpeg Instance;
    public Button real_Btn;

    public RawImage[] View = new RawImage[100];
    [HideInInspector]
    public int selectedChannel = 0;//通道？

    public bool HiDDNSEnable = false;

    //SDK初始化标志
    private bool m_SDKInited;
    //登录返回userid
    private int m_userId = -1;
    // 实时预览标志
    public int m_realHanle = -1;
    // 录制视屏标志
    private bool m_bRecord = false;
    //查找录像
    private int m_lFindHandle = -1;
    //录像回放
    private int m_PlayHandle = -1;
    //流
    private int lPort = -1;
    //是否打开ffmpeg
    private bool m_IsFFmpeg;

    private Texture2D m_Texture;

    private int[] iIPDevID = new int[96];
    private int[] iChannelNum = new int[96];

    public CHCNetSDK32.NET_DVR_DEVICEINFO_V30 DeviceInfo;
    public CHCNetSDK32.NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40;
    public CHCNetSDK32.NET_DVR_STREAM_MODE m_struStreamMode;
    public CHCNetSDK32.NET_DVR_IPCHANINFO m_struChanInfo;
    public CHCNetSDK32.NET_DVR_IPCHANINFO_V40 m_struChanInfoV40;
    CHCNetSDK32.NET_DVR_JPEGPARA lpJpegPara;
    CHCNetSDK32.NET_DVR_PREVIEWINFO previewInfo;
    /// <summary>
	/// 视频回调取流函数 需要在外部定义，然后在实例化，否则容易被回收机制提前清理 ，造成程序崩溃
	/// </summary>
	private PlayCtrl32.DECCBFUN m_fDisplayFun = null;
    private CHCNetSDK32.REALDATACALLBACK m_RealDataBack = null;
    private CHCNetSDK32.PLAYDATACALLBACK m_PlayBackCall = null;
    private CHCNetSDK32.EXCEPYIONCALLBACK m_ExceptionCall = null;


    private uint dwAChanTotalNum = 0;
    private uint dwDChanTotalNum = 0;


    private List<string> ChannelNames;
    private List<string> listViewFile;
    private int iSelIndex = 0;

    uint dwPicSize;
    uint lpSizeReturned;
    byte[] sJpegPicBuffer;

    private byte[] sourceDataFrame;

    //private Queue<byte[]> dataBackupQueue = new Queue<byte[]>();
    private byte[] transforDataframe;

    private Thread GetPicData;

    private float _farmeTime;

    //设置播放分辨率
    private Resolution? playerResolution;

    //显示协程
    private Coroutine m_ViewPlane;

    //public myButtom btnRecord;
    //public myButtom bitmapBtn;
    //public myButtom jpgBtn;

    //private PTZController PTZ;//云台控制器
    //public GameObject ptz_panel;//云台控制面板

    //public Dropdown IpChannelList;
    //public Dropdown ViewFileList;

    public Button quit;

    public GameObject m_viewRawImage;//预览窗口
    public Dropdown m_viewDropdown;//通道选择
    private List<string> tempNames = new List<string>();
    public bool isAutoPlay = true;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        real_Btn.onClick.AddListener(StartRealPlay);
        quit.onClick.AddListener(Quit);

        //InitJpedCig();
        //PTZ = ptz_panel.GetComponent<PTZController>();
        //btnRecord.ClickDown += btnRecord_Click;
        //bitmapBtn.ClickDown += btnBMP_Click;
        //jpgBtn.ClickDown += StartGetJpegRate;
    }

    private void Start()
    {
        Instance = this.GetComponent<TestMedia_FFmpeg>();
        tempNames = GetJson.temCnameList;
        UpdateDropdownView(tempNames);
        InitSDK();
        //IpChannelList.ClearOptions(); 
    }

    /// <summary>
    /// 刷新通道名称数据
    /// </summary>
    /// <param name="showNames"></param>
    private void UpdateDropdownView(List<string> showNames)
    {
        m_viewDropdown.options.Clear();
        Dropdown.OptionData tempData;
        for (int i = 0; i < showNames.Count; i++)
        {
            tempData = new Dropdown.OptionData();
            tempData.text = showNames[i];
            m_viewDropdown.options.Add(tempData);
        }
        m_viewDropdown.captionText.text = showNames[0];
    }

    /// <summary>
    /// 初始化SDK
    /// </summary>
    public void InitSDK()
    {
        m_SDKInited = CHCNetSDK32.NET_DVR_Init();
        if (m_SDKInited)
        {
            CHCNetSDK32.NET_DVR_SetLogToFile(3, "C:/SdkLogUnity/", true);
            CHCNetSDK32.NET_DVR_SetConnectTime(2000, 1); //连接时间与重连时间
            CHCNetSDK32.NET_DVR_SetReconnect(10000, 1);

            //Debug.Log("初始化成功!");
            LoginNVR();
        }
        else
        {
            ViewErrorInfo("初始化失败!", CHCNetSDK32.NET_DVR_GetLastError());
        }
    }
    public void LoginNVR()
    {
        //string ip = ip_field.text.Trim();
        //int port = int.Parse(port_field.text.Trim());
        //string account = account_field.text.Trim();
        //string password = password_field.text.Trim();

        string ip = GetJson.ndIp;//改成自己的IP
        int port = int.Parse(GetJson.ndPort);//改成自己的port
        string account = GetJson.ndName;//改成自己的账号
        string password = GetJson.ndPwd;//改成自己的密码

        if (HiDDNSEnable)
        {
            byte[] HiDDSName = System.Text.Encoding.Default.GetBytes(ip);
            byte[] GetIPAddress = new byte[16];
            uint dwPort = 0;
            if (!CHCNetSDK32.NET_DVR_GetDVRIPByResolveSvr_EX("www.hik-online.com",
                80, HiDDSName, (ushort)HiDDSName.Length, null, 0, GetIPAddress, ref dwPort))
            {
                ViewErrorInfo("域名解析失败!", CHCNetSDK32.NET_DVR_GetLastError());
                return;
            }
            else
            {
                ip = System.Text.Encoding.UTF8.GetString(GetIPAddress).TrimEnd('\0');
                port = (Int16)dwPort;
            }
        }
        //登录
        m_userId = CHCNetSDK32.NET_DVR_Login_V30(ip, port, account, password, ref DeviceInfo);
        if (m_userId > -1)
        {
            //Debug.Log("登录成功!, 用户ID:" + m_userId);
            dwAChanTotalNum = (uint)DeviceInfo.byChanNum;
            dwDChanTotalNum = (uint)DeviceInfo.byIPChanNum + 256 * (uint)DeviceInfo.byHighDChanNum;

            //Debug.Log("DCnum:" + dwDChanTotalNum + "AC:" + dwAChanTotalNum);
            if (dwDChanTotalNum > 0)
            {
                GetListChannels();
            }
            else
            {
                for (int i = 0; i < dwAChanTotalNum; i++)
                {
                    ListAnalogChannel(i + 1, 1);
                    iChannelNum[i] = i + (int)DeviceInfo.byStartChan;
                }
            }
        }
        else
        {
            ViewErrorInfo("登录失败!", CHCNetSDK32.NET_DVR_GetLastError());
        }

        //设置异常消息回调
        m_ExceptionCall = new CHCNetSDK32.EXCEPYIONCALLBACK(ExceptionCallBack);
        CHCNetSDK32.NET_DVR_SetExceptionCallBack_V30(0, IntPtr.Zero, m_ExceptionCall, IntPtr.Zero);
    }

    /// <summary>
    /// 开始实时预览
    /// </summary>
    public void StartRealPlay()
    {
        if (m_userId < 0)
        {
            //Debug.Log("请先登录!");
            return;
        }
        try
        {
            if (!m_viewRawImage.activeSelf) m_viewRawImage.SetActive(true);
            if (m_realHanle < 0)
            {
                //Debug.Log(m_viewDropdown.value);
                if(isAutoPlay)
                {

                }
                else
                {
                    selectedChannel = m_viewDropdown.value;
                }
                isAutoPlay = false;
                previewInfo.hPlayWnd = IntPtr.Zero;
                previewInfo.lChannel = iChannelNum[selectedChannel];
                //Debug.Log("目前播放的是第" + selectedChannel + "路视频？" );
                previewInfo.dwStreamType = 0;
                previewInfo.dwLinkMode = 0;
                previewInfo.bBlocked = false;
                previewInfo.dwDisplayBufNum = 15;//播放最大帧数

                m_RealDataBack = new CHCNetSDK32.REALDATACALLBACK(FFmpegModeCallBack);
                previewInfo.hPlayWnd = IntPtr.Zero;//预览窗口 live view window
                m_realHanle = CHCNetSDK32.NET_DVR_RealPlay_V40(m_userId, ref previewInfo, m_RealDataBack, IntPtr.Zero);

                if (m_realHanle < 0)
                {
                    ViewErrorInfo("预览失败!", CHCNetSDK32.NET_DVR_GetLastError());
                }
                else
                {
                    real_Btn.GetComponentInChildren<Text>().text = "停止预览";
                    //Debug.Log("开始预览");

                    m_ViewPlane = StartCoroutine(AnalizeData());
                    StartCoroutine(StartFFmpeg());//开始基于ffmpeg的格式转换
                }
            }
            else
            {
                StopRealPlay();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }


    //解码回调函数
    private void DecCallbackFUN(int nPort, IntPtr pBuf, int nSize, ref PlayCtrl32.FRAME_INFO pFrameInfo,
        int nReserved1, int nReserved2)
    {
        if (m_realHanle > -1)
        {
            // 将pBuf解码后视频输入写入文件中（解码后YUV数据量极大，尤其是高清码流，不建议在回调函数中处理）
            if (pFrameInfo.nType == 3)
            {
                if (playerResolution == null)
                {
                    var resolution = new Resolution();
                    resolution.width = pFrameInfo.nWidth;
                    resolution.height = pFrameInfo.nHeight;
                    resolution.refreshRate = pFrameInfo.nFrameRate;
                    playerResolution = resolution;
                    transforDataframe = new byte[(long)pFrameInfo.nWidth * pFrameInfo.nHeight * 4];
                }
                if(sourceDataFrame == null)
                {
                    sourceDataFrame = new byte[nSize];
                }

                Marshal.Copy(pBuf, sourceDataFrame, 0, nSize);
            }
        }
    }

    /// <summary>
    /// 获取数据流回调函数
    /// </summary>
    /// <param name="lRealHandle">L real handle.</param>
    /// <param name="dwDataType">Dw data type.</param>
    /// <param name="pBuffer">P buffer.</param>
    /// <param name="dwBufSize">Dw buffer size.</param>
    /// <param name="pUser">P user.</param>
    public void FFmpegModeCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer,
        UInt32 dwBufSize, IntPtr pUser)
    {
        //下面数据处理建议使用委托的方式
        switch (dwDataType)
        {
            case CHCNetSDK32.NET_DVR_SYSHEAD:
                if (dwBufSize > 0)
                {
                    if (lPort > -1)
                    {
                        return; //同一路码流不需要多次调用开流接口
                    }
                    if (!PlayCtrl32.PlayM4_GetPort(ref lPort))
                    {
                        ViewErrorInfo("获取播放库未使用的通道号失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
                        break;
                    }
                    if (!PlayCtrl32.PlayM4_SetStreamOpenMode(lPort, PlayCtrl32.STREAME_REALTIME))
                    {
                        ViewErrorInfo("设置实时流播放模式失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
                    }
                    if (!PlayCtrl32.PlayM4_OpenStream(lPort, pBuffer, dwBufSize, 2 * 1024 * 1024))
                    {
                        ViewErrorInfo("打开码流失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
                        break;
                    }
                    if (!PlayCtrl32.PlayM4_SetDisplayBuf(lPort, 15)) //15
                    {
                        ViewErrorInfo("设置显示缓冲区个数失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
                    }
                    if (!PlayCtrl32.PlayM4_SetOverlayMode(lPort, 0, 0)) //play off screen 
                    {
                        ViewErrorInfo("设置显示模式失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
                    }
                    m_fDisplayFun = new PlayCtrl32.DECCBFUN(DecCallbackFUN);
                    if (!PlayCtrl32.PlayM4_SetDecCallBack(lPort, m_fDisplayFun))
                    {
                        ViewErrorInfo("设置解码回调函数失败!", 0);
                    }
                    if (!PlayCtrl32.PlayM4_Play(lPort, IntPtr.Zero))
                    {
                        ViewErrorInfo("开始解码失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
                        break;
                    }
                    //print("开始解码, port: " + lPort);
                }
                break;
            default:     //送入其他数据 Input the other data
                if (dwBufSize > 0 && lPort > -1)
                {
                    for (int i = 0; i < 999; i++)
                    {
                        if (!PlayCtrl32.PlayM4_InputData(lPort, pBuffer, dwBufSize))
                        {
                            ViewErrorInfo("送入码流数据进行解码失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
                            Thread.Sleep(10);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                break;
        }
    }

    private IEnumerator StartFFmpeg()
    {
        var endframe = new WaitForEndOfFrame();
        while (playerResolution == null)
        {
            yield return endframe;
        }
        //print(string.Format("分辨率: {0}X{1} , rate{2}", playerResolution.Value.width, playerResolution.Value.height,
        //    playerResolution.Value.refreshRate));
        if (!m_IsFFmpeg && m_realHanle > -1)
        {
            if (FFMPEG32.StartConvert_Updated(playerResolution.Value.width, playerResolution.Value.height
                , playerResolution.Value.width, playerResolution.Value.height, 0, 2))
            {
                m_IsFFmpeg = true;

                GetPicData = new Thread(StartTransformData);
                GetPicData.IsBackground = true;
                GetPicData.Start();
               // Debug.Log("开启解析线程");
            }
        }
    }

    void EndFFmpeg()
    {
        if (m_IsFFmpeg)
        {
            m_IsFFmpeg = false;
            if (GetPicData != null)
            {
                GetPicData.Abort();
                GetPicData = null;
            }
            //Debug.Log("停止解析线程");
            //dataBackupQueue.Clear();
            transforDataframe = null;
            playerResolution = null;
            sourceDataFrame = null;
            FFMPEG32.StartConvert_Updated(0, 0, 0, 0, 0, 2);//----结束基于ffmpeg的格式转换
        }
    }

    private void StartTransformData()
    {
        while (m_IsFFmpeg && m_userId > -1)
        {
            if (sourceDataFrame != null)
            {
                //var transforDataframe = new byte[playerResolution.Value.width * playerResolution.Value.height * 3];
                if (FFMPEG32.YV12toRgb_Updated(transforDataframe, sourceDataFrame, playerResolution.Value.width, playerResolution.Value.height))
                {
                    //dataBackupQueue.Enqueue(transforDataframe);
                }
                else
                    Debug.Log("转换失败");
            }
            //Thread.Sleep(100);
            float waitTime = Mathf.Max(16, _farmeTime * 1000);
            Thread.Sleep((int)waitTime);
        }
    }

    public void StopRealPlay()
    {
        if (m_realHanle > -1)
        {
            EndFFmpeg();//结束基于ffmpeg的格式转换

            if (!CHCNetSDK32.NET_DVR_StopRealPlay(m_realHanle))
            {
                ViewErrorInfo("停止预览失败!", CHCNetSDK32.NET_DVR_GetLastError());
            }
            else
            {
                //print("停止预览");

            }

            m_realHanle = -1;

            playerResolution = null;

            real_Btn.GetComponentInChildren<Text>().text = "开始预览";

            if (lPort > -1)
            {
                if (!PlayCtrl32.PlayM4_Stop(lPort))
                {
                    ViewErrorInfo("停止解码失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
                }
                if (!PlayCtrl32.PlayM4_CloseStream(lPort))
                {
                    ViewErrorInfo("关闭解码流失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
                }
                if (!PlayCtrl32.PlayM4_FreePort(lPort))
                {
                    ViewErrorInfo("清除解码失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
                }
                lPort = -1;
                //print("Play清除");
            }

            if (m_ViewPlane != null)
            {
                StopCoroutine(m_ViewPlane);
            }
            m_ViewPlane = null;

            if (m_viewRawImage.activeSelf)
            {
                m_viewRawImage.GetComponent<RawImage>().texture = null;
                m_viewRawImage.SetActive(false);
            }   
        }
    }

    private IEnumerator AnalizeData()
    {
        var endFrame = new WaitForEndOfFrame();
        while ((m_realHanle > -1 || m_PlayHandle > -1) && m_userId > -1)
        {

            if (playerResolution != null && m_Texture == null)
            {
                m_Texture = new Texture2D(playerResolution.Value.width, playerResolution.Value.height
                    , TextureFormat.RGB24, false);
            }
            if (m_Texture != null && transforDataframe!=null)//dataBackupQueue.Count > 0)
            {
                //View[selectedChannel].texture = m_Texture;
                View[0].texture = m_Texture;
                m_Texture.LoadRawTextureData(transforDataframe);//(dataBackupQueue.Dequeue());
                m_Texture.Apply();
            }
            _farmeTime = Time.deltaTime;
            yield return endFrame;
        }
    }

    //系统默认播放方式
    public void RealDataCallBack(int RealHandle, UInt32 dataType, IntPtr dBuffer, UInt32 dBufSize,
        IntPtr UserP)
    {
        switch (dataType)
        {
            case CHCNetSDK32.NET_DVR_SYSHEAD:
                //debugInfo +=  ("系统头：" + dBufSize);
                break;
            case CHCNetSDK32.NET_DVR_STREAMDATA:
                //debugInfo +=  ("视频流：" + dBuffer + "数据长度：" + dBufSize);
                /*byte[] byteBuf = new byte[dBufSize];
                Marshal.Copy (dBuffer, byteBuf, 0, (int)dBufSize);
                DataContentType dy = new DataContentType (byteBuf);
                DataGather.Push (dy);*/
                break;
            default:
                //debugInfo +=  ("其他数据：" + dBufSize);
                break;
        }
    }



    /// <summary>
    /// 退出
    /// </summary>
    public void LogOutNVR()
    {
        if (m_userId > -1)
        {
            if (!CHCNetSDK32.NET_DVR_Logout(m_userId))
            {
                ViewErrorInfo("退出失败!", CHCNetSDK32.NET_DVR_GetLastError());
            }
            else
            {
                //Debug.Log("退出成功!");
                m_userId = -1;
            }
        }
    }

    private void ExceptionCallBack(uint dwType, int lUserID, int lHandle, IntPtr pUser)
    {
        switch (dwType)
        {
            case CHCNetSDK32.EXCEPTION_RECONNECT:
                //Debug.Log("重新连接!" + System.DateTime.Now.ToShortTimeString());
                break;
            default:
                //Debug.Log("异常消息! 类型: " + dwType);
                break;
        }
    }

    public void CleanSDK()
    {
        if (m_SDKInited)
        {
            sourceDataFrame = null;
            playerResolution = null;
            transforDataframe = null;
            //dataBackupQueue = null;
            GetPicData = null;

            CHCNetSDK32.NET_DVR_Cleanup();
            m_SDKInited = false;
            //Debug.Log("清除SDK");
        }
        m_fDisplayFun = null;
        m_RealDataBack = null;
        m_ExceptionCall = null;
        m_PlayBackCall = null;

        GC.Collect();
    }

    void Quit()
    {
        StopRealPlay();
        LogOutNVR();
        CleanSDK();
        
        if (m_realHanle > -1)
        {
            CHCNetSDK32.NET_DVR_StopRealPlay(m_realHanle);
            m_realHanle = -1;
        }
        if (m_userId > -1)
        {
            CHCNetSDK32.NET_DVR_Logout(m_userId);
            m_userId = -1;
        }
        if (m_SDKInited)
        {
            CHCNetSDK32.NET_DVR_Cleanup();
            m_SDKInited = false;
        }
        if (m_viewRawImage.activeSelf)
        {
            m_viewRawImage.GetComponent<RawImage>().texture = null;
            m_viewRawImage.SetActive(false);
        }
        StopAllCoroutines();
        if(GetJson.platform != "Editor" || GetJson.platform == "Windows")
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();//终止当前正在运行的线程
        }
        
        Application.Quit();

        //System.Environment.Exit(System.Environment.ExitCode);
        //System.Diagnostics.Process.GetCurrentProcess().Kill();
    }

    public void OnDestroy()
    {
        Quit();
    }

    private void ViewErrorInfo(string errorMessage, uint errorCode)
    {
        Debug.Log(errorMessage + ", 错误代码: " + errorCode);
    }

    public void StartGetJpegRate()
    {
        if (m_realHanle > -1)
        {
            //Debug.Log("请先关闭预览!");
            return;
        }
        GetPicData = new Thread(btnJPEG_Click);
        GetPicData.IsBackground = true;
        GetPicData.Start();
    }

    /// <summary>
    /// 配置参数
    /// </summary>
    private void Config()
    {

    }

    /// <summary>
    /// 通道遍历
    /// </summary>
    private void GetListChannels()
    {
        if (ChannelNames == null)
        {
            ChannelNames = new List<string>();
        }
        ChannelNames.Clear();

        uint dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40);

        IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
        Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);

        uint dwReturn = 0;
        int iGroupNo = 0;  //该Demo仅获取第一组64个通道，如果设备IP通道大于64路，需要按组号0~i多次调用NET_DVR_GET_IPPARACFG_V40获取

        if (!CHCNetSDK32.NET_DVR_GetDVRConfig(m_userId, CHCNetSDK32.NET_DVR_GET_IPPARACFG_V40, iGroupNo, ptrIpParaCfgV40, dwSize, ref dwReturn))
        {
            ViewErrorInfo("获取通道信息失败!", CHCNetSDK32.NET_DVR_GetLastError());
        }
        else
        {
            //Debug.Log("获取通道成功!");
            m_struIpParaCfgV40 = (CHCNetSDK32.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(CHCNetSDK32.NET_DVR_IPPARACFG_V40));
            for (int i = 0; i < dwAChanTotalNum; i++)
            {
                ListAnalogChannel(i + 1, m_struIpParaCfgV40.byAnalogChanEnable[i]);
                iChannelNum[i] = i + (int)DeviceInfo.byStartChan;
            }

            byte byStreamType = 0;
            uint iDChanNum = 64;

            if (dwDChanTotalNum < 64)
            {
                iDChanNum = dwDChanTotalNum; //如果设备IP通道小于64路，按实际路数获取
            }
            
            for (int i = 0; i < iDChanNum; i++)
            {
                iChannelNum[i + dwAChanTotalNum] = i + (int)m_struIpParaCfgV40.dwStartDChan;
                byStreamType = m_struIpParaCfgV40.struStreamMode[i].byGetStreamType;
                dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40.struStreamMode[i].uGetStream);
                switch (byStreamType)
                {
                    //目前NVR仅支持直接从设备取流 NVR supports only the mode: get stream from device directly
                    case 0:
                        IntPtr ptrChanInfo = Marshal.AllocHGlobal((Int32)dwSize);
                        Marshal.StructureToPtr(m_struIpParaCfgV40.struStreamMode[i].uGetStream, ptrChanInfo, false);
                        m_struChanInfo = (CHCNetSDK32.NET_DVR_IPCHANINFO)Marshal.PtrToStructure(ptrChanInfo, typeof(CHCNetSDK32.NET_DVR_IPCHANINFO));

                        //列出IP通道 List the IP channel
                        AddIpChannelItems(i + 1, m_struChanInfo.byEnable, m_struChanInfo.byIPID);
                        iIPDevID[i] = m_struChanInfo.byIPID + m_struChanInfo.byIPIDHigh * 256 - iGroupNo * 64 - 1;
                        //Debug.Log(iIPDevID[i]);
                        Marshal.FreeHGlobal(ptrChanInfo);
                        break;
                    case 6:
                        IntPtr ptrChanInfoV40 = Marshal.AllocHGlobal((Int32)dwSize);
                        Marshal.StructureToPtr(m_struIpParaCfgV40.struStreamMode[i].uGetStream, ptrChanInfoV40, false);
                        m_struChanInfoV40 = (CHCNetSDK32.NET_DVR_IPCHANINFO_V40)Marshal.PtrToStructure(ptrChanInfoV40, typeof(CHCNetSDK32.NET_DVR_IPCHANINFO_V40));

                        //列出IP通道 List the IP channel
                        AddIpChannelItems(i + 1, m_struChanInfoV40.byEnable, m_struChanInfoV40.wIPID);
                        iIPDevID[i] = m_struChanInfoV40.wIPID - iGroupNo * 64 - 1;
                        //Debug.Log(iIPDevID[i]);
                        Marshal.FreeHGlobal(ptrChanInfoV40);
                        break;
                    default:
                        break;
                }
            }
            if (GetJson.intCnameNum != iDChanNum)//修改通道列表
            {
                tempNames.Clear();
                for(int k = 0;k < iDChanNum;k++)
                {
                    tempNames.Add(ChannelNames[k]);
                }
                UpdateDropdownView(tempNames);
                
            }
            //IpChannelList.AddOptions(ChannelNames);
        }
        Marshal.FreeHGlobal(ptrIpParaCfgV40);
    }

    /// <summary>
    /// 链接NVR时通道显示
    /// </summary>
    /// <param name="iChanNo">I chan no.</param>
    /// <param name="byOnline">By online.</param>
    /// <param name="byIPID">By IPI.</param>
    private void AddIpChannelItems(Int32 iChanNo, byte byOnline, int byIPID)
    {
        var str1 = String.Format("IPCamera {0}", iChanNo);
        var str2 = string.Empty;

        if (byIPID == 0)
        {
            str2 = "X"; //通道空闲，没有添加前端设备 the channel is idle                  
        }
        else
        {
            if (byOnline == 0)
            {
                str2 = "offline"; //通道不在线 the channel is off-line
            }
            else
                str2 = "online"; //通道在线 The channel is on-line
        }
        ChannelNames.Add(str1 + "_" + str2);

    }

    /// <summary>
    /// 直接连摄像机时通道显示
    /// </summary>
    /// <param name="iChanNo">I chan no.</param>
    /// <param name="byEnable">By enable.</param>
    private void ListAnalogChannel(Int32 iChanNo, byte byEnable)
    {
        var str1 = String.Format("Camera {0}", iChanNo);
        var str2 = string.Empty;
        //Debug.Log(str1);
        if (byEnable == 0)
        {
            str2 = "Disabled"; //通道已被禁用 This channel has been disabled               
        }
        else
        {
            str2 = "Enabled"; //通道处于启用状态 This channel has been enabled
        }
        if (ChannelNames == null)
        {
            ChannelNames = new List<string>();
        }
        ChannelNames.Add(str1 + "_" + str2);
    }

    /// <summary>
    /// 更新选择的相机通道
    /// </summary>
    public void SelcetedChannelChanged()
    {
        //if (realHanle > 0)
        //{
        //    debugInfo += "请先停止预览";
        //    IpChannelList.value = selectedChannel;
        //}
        //else
        //{
        //    selectedChannel = IpChannelList.value;
        //}
    }

    public void SelectedVideoChanged()
    {
        //if (m_PlayHandle > 0)
        //{
        //    debugInfo += "请先停止回放";
        //    ViewFileList.value = iSelIndex;
        //}
        //else
        //{
        //    iSelIndex = ViewFileList.value;
        //}
    }

    /// <summary>
    /// 录像回放取流
    /// </summary>
    /// <param name="lPlayHandle">L play handle.</param>
    /// <param name="dwDataType">Dw data type.</param>
    /// <param name="pBuffer">P buffer.</param>
    /// <param name="dwBufSize">Dw buffer size.</param>
    /// <param name="dwUser">Dw user.</param>
    public void PlayBackFunc(int lPlayHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, uint dwUser)
    {
        FFmpegModeCallBack(lPlayHandle, dwDataType, pBuffer, dwBufSize, IntPtr.Zero);
    }

    /// <summary>
    /// 打开云台控制面板
    /// </summary>
    public void OpenPTZ_Panel()
    {
        if (m_userId == -1)
        {
            //Debug.Log("请先登录！");
            return;
        }
        //if (PTZ != null)
        //{
        //    PTZ.user_id = this.user_id;
        //    PTZ.channel = 33;
        //}
        //if (ptz_panel.activeSelf)
        //{
        //    ptz_panel.SetActive(false);
        //}
        //else
        //{
        //    ptz_panel.SetActive(true);
        //}
    }





    private void InitJpedCig()
    {
        if (playerResolution == null)
        {
            print("预览");
            return;
        }
        dwPicSize = (uint)(playerResolution.Value.width * playerResolution.Value.height);
        lpSizeReturned = dwPicSize;
        sJpegPicBuffer = new byte[dwPicSize];
        lpJpegPara = new CHCNetSDK32.NET_DVR_JPEGPARA();
        lpJpegPara.wPicQuality = 2; //图像质量 Image quality
        lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档
        lpJpegPara.wPicSize = 1;
    }
    private void btnJPEG_Click()
    {
        while (!m_IsFFmpeg && m_userId > -1)
        {
            /*
    string sJpegPicFileName;
    //图片保存路径和文件名 the path and file name to save
    sJpegPicFileName = Application.dataPath+"/JPEG_test.jpg";
     //通道号 Channel number
    */


            //JPEG抓图 Capture a JPEG picture
            /*if (!CHCNetSDK32.NET_DVR_CaptureJPEGPicture(user_id, PTZ.channel, ref lpJpegPara, sJpegPicFileName))
    {
        error_num = CHCNetSDK32.NET_DVR_GetLastError();
        debugInfo += ("NET_DVR_CaptureJPEGPicture failed, error code= " + error_num);
        return;
    }
    else
    {
        debugInfo += ("Successful to capture the JPEG file and the saved file is " + sJpegPicFileName);
    }*/


            //if (!CHCNetSDK32.NET_DVR_CaptureJPEGPicture_NEW(user_id, PTZ.channel, ref lpJpegPara, sJpegPicBuffer, dwPicSize, ref lpSizeReturned))
            //{
            //    error_num = CHCNetSDK32.NET_DVR_GetLastError();
            //    debugInfo += ("NET_DVR_CaptureJPEGPicture failed, error code= " + error_num + "返回码:" + lpSizeReturned);

            //    return;
            //}
            //else
            //{
            //    debugInfo += ("Successful to capture the JPEG file and the saved file is ");

            //}
            //if (transforDataframe == null)
            //{
            //    transforDataframe = sJpegPicBuffer;
            //    Debug.Log(sJpegPicBuffer.Length.ToString());
            //}
            Thread.Sleep(100);
        }
    }



    /// <summary>
    /// 视频录制
    /// </summary>
    public void btnRecord_Click()
    {
        //录像保存路径和文件名
        string sVideoFileName;
        sVideoFileName = "Record_test.mp4";

        if (m_bRecord == false)
        {
            //开始录像 Start recording
            if (!CHCNetSDK32.NET_DVR_SaveRealData(m_realHanle, sVideoFileName))
            {
                ViewErrorInfo("开始录像失败!", CHCNetSDK32.NET_DVR_GetLastError());
                return;
            }
            else
            {
                m_bRecord = true;
            }
        }
        else
        {
            if (!CHCNetSDK32.NET_DVR_StopSaveRealData(m_realHanle))
            {
                ViewErrorInfo("停止录像失败!", CHCNetSDK32.NET_DVR_GetLastError());
                return;
            }
            else
            {
                //Debug.Log("录制成功! 文件: " + sVideoFileName);
                m_bRecord = false;
            }
        }

        return;
    }

    public void btnBMP_Click()
    {
        if (m_realHanle < 0)
        {
            //Debug.Log("先关闭预览!"); //BMP抓图需要先打开预览
            return;
        }

        string sBmpPicFileName;
        //图片保存路径和文件名
        sBmpPicFileName = "test.bmp";


        int iWidth = 0, iHeight = 0;
        uint iActualSize = 0;

        if (!PlayCtrl32.PlayM4_GetPictureSize(lPort, ref iWidth, ref iHeight))
        {
            ViewErrorInfo("BMP抓图失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
            return;
        }

        uint nBufSize = (uint)(iWidth * iHeight) * 5;
        byte[] pBitmap = new byte[nBufSize];

        if (!PlayCtrl32.PlayM4_GetBMP(lPort, pBitmap, nBufSize, ref iActualSize))
        {
            ViewErrorInfo("BMP抓图 PlayM4_GetBMP 失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
        }
        else
        {
            m_Texture.LoadImage(pBitmap);
            View[selectedChannel].texture = m_Texture;
            //Debug.Log("抓图成功! 文件: " + sBmpPicFileName);
        }
        return;
    }

    public void FindBackVideo()
    {
        if (listViewFile == null)
        {
            listViewFile = new List<string>();
        }//清空文件列表 
         // ViewFileList.ClearOptions();

        CHCNetSDK32.NET_DVR_FILECOND struFileCond_V40 = new CHCNetSDK32.NET_DVR_FILECOND();

        struFileCond_V40.lChannel = iChannelNum[selectedChannel]; //通道号 Channel number

                                                                  //struFileCond_V40.lChannel = 1;//通道号 Channel number
        struFileCond_V40.dwFileType = 0xff; //0xff-全部，0-定时录像，1-移动侦测，2-报警触发，...
        struFileCond_V40.dwIsLocked = 0xff; //0-未锁定文件，1-锁定文件，0xff表示所有文件（包括锁定和未锁定）

        //设置录像查找的开始时间 Set the starting time to search video files
        struFileCond_V40.struStartTime.dwYear = (uint)2018;
        struFileCond_V40.struStartTime.dwMonth = (uint)3;
        struFileCond_V40.struStartTime.dwDay = (uint)1;
        struFileCond_V40.struStartTime.dwHour = (uint)0;
        struFileCond_V40.struStartTime.dwMinute = (uint)0;
        struFileCond_V40.struStartTime.dwSecond = (uint)0;

        //设置录像查找的结束时间 Set the stopping time to search video files
        struFileCond_V40.struStopTime.dwYear = (uint)2018;
        struFileCond_V40.struStopTime.dwMonth = (uint)4;
        struFileCond_V40.struStopTime.dwDay = (uint)4;
        struFileCond_V40.struStopTime.dwHour = (uint)0;
        struFileCond_V40.struStopTime.dwMinute = (uint)0;
        struFileCond_V40.struStopTime.dwSecond = (uint)0;


        //开始录像文件查找 Start to search video files 
        m_lFindHandle = CHCNetSDK32.NET_DVR_FindFile_V30(m_userId, ref struFileCond_V40);

        if (m_lFindHandle < 0)
        {
            ViewErrorInfo("查找录像文件失败!", CHCNetSDK32.NET_DVR_GetLastError());
            return;
        }
        else
        {
            CHCNetSDK32.NET_DVR_FINDDATA_V30 struFileData = new CHCNetSDK32.NET_DVR_FINDDATA_V30(); ;
            while (true)
            {
                //逐个获取查找到的文件信息 Get file information one by one.
                int result = CHCNetSDK32.NET_DVR_FindNextFile_V30(m_lFindHandle, ref struFileData);

                if (result == CHCNetSDK32.NET_DVR_ISFINDING)  //正在查找请等待 Searching, please wait
                {
                    continue;
                }
                else if (result == CHCNetSDK32.NET_DVR_FILE_SUCCESS) //获取文件信息成功
                {
                    var str1 = struFileData.sFileName;

                    var str2 = Convert.ToString(struFileData.struStartTime.dwYear) + "-" +
                        Convert.ToString(struFileData.struStartTime.dwMonth) + "-" +
                        Convert.ToString(struFileData.struStartTime.dwDay) + " " +
                        Convert.ToString(struFileData.struStartTime.dwHour) + ":" +
                        Convert.ToString(struFileData.struStartTime.dwMinute) + ":" +
                        Convert.ToString(struFileData.struStartTime.dwSecond);

                    /*str3 = Convert.ToString(struFileData.struStopTime.dwYear) + "-" +
						Convert.ToString(struFileData.struStopTime.dwMonth) + "-" +
						Convert.ToString(struFileData.struStopTime.dwDay) + " " +
						Convert.ToString(struFileData.struStopTime.dwHour) + ":" +
						Convert.ToString(struFileData.struStopTime.dwMinute) + ":" +
						Convert.ToString(struFileData.struStopTime.dwSecond);*/

                    //listViewFile.Items.Add(new ListViewItem(new string[] { str1, str2, str3}));//将查找的录像文件添加到列表中
                    listViewFile.Add(str1);

                }
                else if (result == CHCNetSDK32.NET_DVR_FILE_NOFIND || result == CHCNetSDK32.NET_DVR_NOMOREFILE)
                {
                    break; //未查找到文件或者查找结束，退出   No file found or no more file found, search is finished 
                }
                else
                {
                    break;
                }
                Thread.Sleep(2);
            }
            //ViewFileList.AddOptions(listViewFile);
        }
    }

    /// <summary>
    /// 开始录像回放
    /// </summary>
    public void StartPlayBackVideo()
    {
        if (listViewFile == null)
        {
            //Debug.Log("请选择录像文件");
            return;
        }
        if (m_PlayHandle >= 0)
        {
            if (!CHCNetSDK32.NET_DVR_StopPlayBack(m_PlayHandle))
            {
                ViewErrorInfo("停止录像回放失败!", CHCNetSDK32.NET_DVR_GetLastError());
                return;
            }
            m_PlayHandle = -1;
        }
        m_PlayHandle = CHCNetSDK32.NET_DVR_PlayBackByName(m_userId, listViewFile[iSelIndex], IntPtr.Zero);
        if (m_PlayHandle < 0)
        {
            ViewErrorInfo("开始录像回放失败!", CHCNetSDK32.NET_DVR_GetLastError());
            return;
        }
        m_PlayBackCall = new CHCNetSDK32.PLAYDATACALLBACK(PlayBackFunc);
        CHCNetSDK32.NET_DVR_SetPlayDataCallBack(m_PlayHandle, m_PlayBackCall, 0);

        uint OutValue = 0;
        if (!CHCNetSDK32.NET_DVR_PlayBackControl_V40(m_PlayHandle, CHCNetSDK32.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref OutValue))
        {
            ViewErrorInfo("播放录像回放失败!", CHCNetSDK32.NET_DVR_GetLastError());
            return;
        }
        StartFFmpeg();

        m_ViewPlane = StartCoroutine(AnalizeData());
    }

    /// <summary>
    /// 停止录像回放
    /// </summary>
    public void StopPlayBackVideo()
    {
        if (m_PlayHandle < 0)
        {
            return;
        }

        //停止回放
        if (!CHCNetSDK32.NET_DVR_StopPlayBack(m_PlayHandle))
        {
            ViewErrorInfo("停止录像回放失败!", CHCNetSDK32.NET_DVR_GetLastError());
            return;
        }

        if (lPort >= 0)
        {
            if (!PlayCtrl32.PlayM4_Stop(lPort))
            {
                ViewErrorInfo("停止解码失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
            }
            if (!PlayCtrl32.PlayM4_CloseStream(lPort))
            {
                ViewErrorInfo("关闭解码流失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
            }
            if (!PlayCtrl32.PlayM4_FreePort(lPort))
            {
                ViewErrorInfo("清除解码失败!", PlayCtrl32.PlayM4_GetLastError(lPort));
            }
            lPort = -1;
        }
        m_PlayHandle = -1;
        EndFFmpeg();

        if (m_ViewPlane != null)
            StopCoroutine(m_ViewPlane);
    }
}
