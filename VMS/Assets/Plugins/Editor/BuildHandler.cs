using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class BuildHandler
{
#if UNITY_STANDALONE_WIN
    [UnityEditor.Callbacks.PostProcessBuild(999)]
    public static void OnPostprocessBuild(BuildTarget BuildTarget, string path)
    {
        string platformPath = string.Empty;
        if(BuildTarget == BuildTarget.StandaloneWindows)
        {
            platformPath = "x86";
        }else if(BuildTarget == BuildTarget.StandaloneWindows64)
        {
            platformPath = "x86_64";
        }

        if (!string.IsNullOrEmpty(platformPath))
        {
            //PC(WINDOWS)打包后期脚本：复制文件夹到打包后的Plugins文件夹
            //UnityEngine.Debug.Log(path);
            var strPathFrom = UnityEngine.Application.dataPath + "/Plugins/" + platformPath + "/HCNetSDKCom/";
            var nIdxSlash = path.LastIndexOf('/');
            var nIdxDot = path.LastIndexOf('.');
            var strRootTarget = path.Substring(0, nIdxSlash);
            var strPathTo = strRootTarget + path.Substring(nIdxSlash, nIdxDot - nIdxSlash) + "_Data/Plugins/HCNetSDKCom/";
            //System.IO.File.Copy(strPathFrom, strPathTargetFile);
            bool flag = CopyEntireDir(strPathFrom, strPathTo);
            //UnityEngine.Debug.Log("strPathFrom == " + strPathFrom + "   strPathTo == " + strPathTo);
            if (flag)
            {
                UnityEngine.Debug.Log("复制操作完成。");
            }
            else
            {
                UnityEngine.Debug.Log("复制操作出现了一些问题!");
            }
        }
    }

    public static bool CopyEntireDir(string sourcePath, string destPath)
    {
        if (!Directory.Exists(sourcePath))
        {
            return false;
        }
        
        if (!Directory.Exists(destPath)){
            Directory.CreateDirectory(destPath);
        }
        //Now Create all of the directories
        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
           SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, destPath));
        }

        //Copy all the files & Replaces any files with the same name
        foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
           SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(sourcePath, destPath), true);
        }
        return true;
    }
#endif
}
