using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class WebNativeDialog
{
#if UNITY_WEBGL && !UNITY_EDITOR
   
    [DllImport("__Internal")]
    private static extern string SetupOverlayDialogHtml(string defaultValue, int x, int y, int width, int height, int s);

    [DllImport("__Internal")]
    private static extern bool IsOverlayDialogHtmlActive();
    [DllImport("__Internal")]
    private static extern bool IsOverlayDialogHtmlCanceled();
    [DllImport("__Internal")]
    private static extern string GetOverlayHtmlInputFieldValue();
    [DllImport("__Internal")]
    private static extern void HideUnityScreenIfHtmlOverlayCant();
    [DllImport("__Internal")]
    private static extern bool IsRunningOnEdgeBrowser();
#endif


    public static void SetUpOverlayDialog(string defaultValue, int x, int y, int width, int height, int s)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Screen.fullScreen)
        {
            if( IsRunningOnEdgeBrowser() ){
                Screen.fullScreen = false;
            }else{
                HideUnityScreenIfHtmlOverlayCant();
            }
        }
        SetupOverlayDialogHtml(defaultValue,x,y,width,height, s);
#else
#endif
    }


    public static bool IsOverlayDialogActive()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsOverlayDialogHtmlActive();
#else
        return false;
#endif
    }

    public static bool IsOverlayDialogCanceled()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return IsOverlayDialogHtmlCanceled();
#else
        return false;
#endif
    }
    public static string GetOverlayDialogValue()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return GetOverlayHtmlInputFieldValue();
#else
        return "";
#endif
    }
}