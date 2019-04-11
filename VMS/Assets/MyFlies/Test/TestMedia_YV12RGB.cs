using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HKCamera;
using UnityEngine.UI;

public class TestMedia_YV12RGB : MonoBehaviour
{
    public RawImage View;

    public Button realBtn;

    public Button quit;

    private IDeviceCamera hkCamera;

    //显示协程
    private Coroutine m_ViewPlane;


    // Use this for initialization
    void Start ()
    {
        hkCamera = new DeviceCamera();

        string ip = "10.12.6.210"; //改成自己的IP
        short port = 8000;//改成自己的
        string account = "admin"; //改成自己的
        string password = "zykj2617";//改成自己的

        var info = new CameraInfo();
        info.strIP = ip;
        info.nPort = 3000;
        info.strUserName = account;
        info.strPassword = password;

        bool flag = hkCamera.InitCamera(info);
        if (flag)
        {
            realBtn.onClick.AddListener(RealView);
        }
        else
        {
            Debug.Log("???????????");
        }
        quit.onClick.AddListener(Quit);
    }

    private bool _init;

    private void RealView()
    {
        if (!_init)
        {
            hkCamera.StartCamera();
            m_ViewPlane = StartCoroutine(AnalizeData());
        }
        else
        {
            hkCamera.StopCamera();
            if (m_ViewPlane != null)
            {
                StopCoroutine(m_ViewPlane);
            }
            m_ViewPlane = null;
        }
        _init = !_init;
    }

    private Texture2D m_Texture;

    private IEnumerator AnalizeData()
    {
        print("TTTTTTTTTTTTT");
        var endFrame = new WaitForFixedUpdate();
        while (true)
        {
            if (hkCamera.DataOutput != null && hkCamera.PlayResolution != null)
            {
                hkCamera.SetFarmeTime(Time.deltaTime);

                if (m_Texture == null)
                {
                    m_Texture = new Texture2D(hkCamera.PlayResolution.Value.width, 
                        hkCamera.PlayResolution.Value.height, TextureFormat.RGBA32, false);
                }
                if (m_Texture != null && hkCamera.DataOutput.Count > 0)
                {
                    m_Texture.LoadRawTextureData(hkCamera.DataOutput.Dequeue());
                    m_Texture.Apply();
                    View.texture = m_Texture;
                }
            }

            yield return endFrame;
        }
    }

    private void Quit()
    {
        if (hkCamera != null)
        {
            hkCamera.Dispose();
            hkCamera = null;
        }
        if (m_ViewPlane != null)
        {
            StopCoroutine(m_ViewPlane);
        }
        m_ViewPlane = null;
    }

    private void OnDestroy()
    {
        Quit();
    }
}
