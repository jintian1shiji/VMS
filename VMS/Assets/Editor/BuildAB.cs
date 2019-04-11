using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildAB : MonoBehaviour {
    [MenuItem("Build/Web")]
    static void BuildWebGl()
    {
        GreatAllBundle(BuildTarget.WebGL, "Web");
    }

    [MenuItem("Build/PC")]
    static void BuildPC()
    {
        GreatAllBundle(BuildTarget.StandaloneWindows, "PC");
    }

    static void BuildAllAssetBundles(BuildTarget target,string str)
    {
        string path = Application.streamingAssetsPath + "/AssetBundles/" + str;

        //文件夹不存在，则创建
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);

        }

        //资源打包
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, target);
    }

    [MenuItem("Build/Create_Scene")]
    static void CreateSceneALL()
    {
        //清空一下缓存  
        Caching.ClearCache();

        //获得用户选择的路径的方法，可以打开保存面板（推荐）
        string Path = EditorUtility.SaveFilePanel("保存资源", "SS", "" + "MS_M", "unity3d");

        //另一种获得用户选择的路径，默认把打包后的文件放在Assets目录下
        //string Path = Application.dataPath + "/MyScene.unity3d";

        //选择的要保存的对象 
        string[] levels = { "Assets/_ZP/Sence/_MySence.unity" };

        //打包场景  
        BuildPipeline.BuildPlayer(levels, Path, BuildTarget.WebGL, BuildOptions.BuildAdditionalStreamedScenes);

        // 刷新，可以直接在Unity工程中看见打包后的文件
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 将符合条件的全部分包打包（基本没什么卵用）
    /// </summary>
    /// 
    static void GreatBundle()
    {
        string _path = Application.streamingAssetsPath + "/Manany/";
        //文件夹不存在，则创建
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);

        }
        Object[] selects = Selection.objects;
        foreach (Object item in selects)
        {
            Debug.Log(item);
            BuildPipeline.BuildAssetBundles(_path, BuildAssetBundleOptions.None, BuildTarget.Android);
            AssetDatabase.Refresh();//打包后刷新，不加这行代码的话要手动刷新才可以看得到打包后的Assetbundle包
        }
    }
    /// <summary>
    /// 将选定的多个游戏对象打包一个
    /// </summary>
    /// 
    static void GreatAllBundle(BuildTarget target, string str)
    {
        string path = Application.streamingAssetsPath + "/AssetBundles/" + str + "/camera";

        //文件夹不存在，则创建
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);

        }
        AssetBundleBuild[] builds = new AssetBundleBuild[500];
        Object[] selects = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        string[] TestAsset = new string[selects.Length];
        for (int i = 0; i < selects.Length; i++)
        {
            TestAsset[i] = AssetDatabase.GetAssetPath(selects[i]);
            Debug.Log(TestAsset[i]);
        }
        builds[0].assetNames = TestAsset;
        builds[0].assetBundleName = "cameraab";
        BuildPipeline.BuildAssetBundles(path, builds, BuildAssetBundleOptions.None, target);
        AssetDatabase.Refresh();
    }

    [MenuItem("Build/SetAssetBundleNameExtension")]  //将选定的资源进行统一设置AssetBundle名
    static void SetBundleName()
    {
        Object[] selects = Selection.objects;
        int num = 0;
        foreach (Object item in selects)
        {
            num++;
            string path = AssetDatabase.GetAssetPath(item);
            AssetImporter asset = AssetImporter.GetAtPath(path);
            //asset.assetBundleName = item.name;//设置Bundle文件的名称
            asset.assetBundleName = "camera";//设置Bundle文件的名称
            asset.assetBundleVariant = "ab";//设置Bundle文件的扩展名
            asset.SaveAndReimport();
        }
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 批量清空所有资源的AssetBundleName.
    /// </summary>
    [MenuItem("Build/ClearAssetBundleName")]
    static void ResetSelectFolderFileBundleName()
    {
        int length = AssetDatabase.GetAllAssetBundleNames().Length;
        string[] oldAssetBundleNames = new string[length];
        for (int i = 0; i < length; i++)
        {
            oldAssetBundleNames[i] = AssetDatabase.GetAllAssetBundleNames()[i];
        }

        for (int j = 0; j < oldAssetBundleNames.Length; j++)
        {
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[j], true);
        }

    }


    [MenuItem("Build/ReObjectName")]//批量更改名称
    public static void ToRename()
    {

        Object[] m_objects = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);//选择的所以对象

        int index = 0;//序号

        foreach (Object item in m_objects)
        {

            //string m_name = item.name;
            if (Path.GetExtension(AssetDatabase.GetAssetPath(item)) != "")//判断路径是否为空
            {

                string path = AssetDatabase.GetAssetPath(item);

                index++;
                AssetDatabase.RenameAsset(path, "camera" + index);

            }

        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }
}
