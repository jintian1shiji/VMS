using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    #region loading界面组件
    public Slider progressSlider;//进度条
    public Text ProgressSliderText;//进度条进度显示文字
    private int nowProcess;//当前加载进度
    private AsyncOperation async;
    #endregion

    #region 脚踏车动画
    //轮子和脚踏板
    public List<GameObject> loadlist01;
    //轨道
    public List<GameObject> loadlist02;
    //字
    public List<GameObject> loadlist03;
    #endregion

    #region 加载 logo 开始判断
    //是否结束
    private bool gamebool = false;
    //公司名称logo
    [SerializeField]
    private Text loadNameText;
    //场景AB地址
    private string abUl = "";
    #endregion

    #region 扫描雷达
    public List<Vector2> Listpos;
    public List<GameObject> Dgame;
    public List<GameObject> Qgame;
    public GameObject Saomiao;
    private int speedload = 0;
    private int rot = 360;
    Quaternion mCameraRot = Quaternion.identity;
    private int vis = 0;
    private int visspeed = 0;
    private int Dnum = 0;
    #endregion


    void Start()
    {
        #region 获取场景地址
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            abUl = Application.streamingAssetsPath + "/AssetBundles/PC/scene/MS_M.unity3d";
        }
        else
        {
            abUl = Application.streamingAssetsPath + "/AssetBundles/Web/scene/MS_M.unity3d";
        }
        #endregion
        loadNameText.text = GetJson.companyName;
        ShowProgressBar(2);
    }

    
    void Update()
    {
        #region loading中
        if (!gamebool)
        {
            UpLoading();
        }
        else
        {
            if (async != null)
            {
                async = null;
            }
        }

        if (async == null)
        {
            return;
        }

        //Debug.Log("async");
        int toProcess;
        // async.progress 你正在读取的场景的进度值  0---0.9    
        // 如果当前的进度小于0.9，说明它还没有加载完成，就说明进度条还需要移动    
        // 如果，场景的数据加载完毕，async.progress 的值就会等于0.9  
        if (async.progress < 0.9f)
        {
            toProcess = (int)async.progress * 100;
        }
        else
        {
            toProcess = 100;
        }
        // 如果滑动条的当前进度，小于，当前加载场景的方法返回的进度   
        if (nowProcess < toProcess)
        {
            nowProcess++;
        }

        progressSlider.value = nowProcess / 100f;
        //设置progressText进度显示
        ProgressSliderText.text = progressSlider.value * 100 + "%";
        // 设置为true的时候，如果场景数据加载完毕，就可以自动跳转场景   
        if (nowProcess == 100)
        {
            async.allowSceneActivation = true;
            gamebool = true;
        }
        #endregion
    }

    #region 场景加载
    //异步加载scene
    IEnumerator LoadScene(int sceneId)
    {
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(abUl);
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log(request.error);
        }
        else
        {
            AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
            AsyncOperation async = SceneManager.LoadSceneAsync("_MySence");
            yield return async;
            ab.Unload(false);
        }
    }
    //外部调用的加载的方法
    public void ShowProgressBar(int id)
    {
        StartCoroutine("LoadScene", id);
    }
    #endregion

    
    void UpLoading()
    {
        if (!gamebool)
        {
            //OneLoad();
            TwoLoad();
        }
    }

    #region 扫描雷达
    void TwoLoad()
    {
        //扫描雷达
        mCameraRot.eulerAngles = new Vector3(0, 0, rot);
        Saomiao.transform.localRotation = mCameraRot;

        if (rot >= 2)
        {
            rot -= 2;
        }
        else rot = 360;

        //点
        if (visspeed == 10)
        {
            int xxx = 0;

            if (Dnum < Dgame.Count)
            {
                foreach (GameObject elenment in Dgame)
                {
                    if (xxx == Dnum)
                    {
                        elenment.SetActive(true);
                    }
                    xxx++;
                }

                Dnum++;
            }
            else
            {
                Dnum = 1;
                foreach (GameObject elenment in Dgame)
                {
                    if (xxx != 0)
                    {
                        elenment.SetActive(false);
                    }
                    xxx++;
                }
            }

            visspeed = 0;

            //Debug.Log((int)(Random.Range(0, (float)20.5)));

            int randomxx = (int)(Random.Range(0, (float)2.5));
            int randomyy = (int)(Random.Range(0, (float)19.5));
            int kkk = 0;
            foreach (GameObject elemen in Qgame)
            {
                if (kkk == randomxx)
                {
                    elemen.SetActive(true);
                    elemen.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(Listpos[randomyy].x, Listpos[randomyy].y, 0);
                }
                else elemen.SetActive(false);

                kkk++;
            }
        }
        visspeed++;
    }
    #endregion


    #region 单车动画
    void OneLoad()
    {
        //loading轨道
        if (speedload == 5)
        {
            foreach (GameObject elenment in loadlist02)
            {
                Vector3 pos = elenment.GetComponent<RectTransform>().anchoredPosition3D;
                if (pos.x - 1 <= -101)
                {
                    elenment.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(115, -80, 0);
                }
                else
                {
                    elenment.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(pos.x - 1, -80, 0);
                }
            }
            speedload = 0;
        }
        speedload++;

        //loading字
        if (visspeed >= 10)
        {
            int xx = 0;
            foreach (GameObject elenment in loadlist03)
            {
                if (xx == vis)
                {
                    elenment.SetActive(true);
                    break;
                }
                xx++;
            }
            vis++;
            if (vis >= loadlist03.Count)
            {
                vis = 0;
                foreach (GameObject elenment in loadlist03)
                {
                    elenment.SetActive(false);
                }
            }

            visspeed = 0;
        }
        visspeed++;

        //loading轮子和脚踏板
        foreach (GameObject elenment in loadlist01)
        {
            if (rot - 1 >= 0)
            {
                rot -= 1;
            }
            else rot = 360;

            mCameraRot.eulerAngles = new Vector3(0, 0, rot);
            elenment.transform.localRotation = mCameraRot;
        }
    }
    #endregion
}
