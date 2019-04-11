using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ToolControlTaskBar : MonoBehaviour
{
    [DllImport("user32.dll")]   //这里是引入 user32.dll 库， 这个库是windows系统自带的。

    public static extern int ShowWindow(int hwnd, int nCmdShow); //这是显示任务栏
    [DllImport("user32.dll")]

    public static extern int FindWindow(string lpClassName, string lpWindowName); //这是隐藏任务栏
    private const int SW_HIDE = 0;  //hied task bar
    private const int SW_RESTORE = 9;//show task bar

    // Use this for initialization
    /// <summary>
    /// show TaskBar
    /// </summary>

    public static void ShowTaskBar()
    {
        ShowWindow(FindWindow("Shell_TrayWnd", null), SW_RESTORE);
    }

    /// <summary>
    /// Hide TaskBar
    /// </summary>

    public static void HideTaskBar()
    {
        ShowWindow(FindWindow("Shell_TrayWnd", null), SW_HIDE);
    }

}
