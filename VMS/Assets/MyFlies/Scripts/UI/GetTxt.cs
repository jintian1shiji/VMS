using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine.UI;
using System.Collections.Generic;

public class GetTxt : MonoBehaviour {

    public static string[][] Array;
    public static List<Vector3> posa;
    public static int alength;

    public static string[][] arrayPath;
    public static List<string> Path;
    void Start()
    {
        //读取csv二进制文件  
        TextAsset binAsset = Resources.Load("MyTxt", typeof(TextAsset)) as TextAsset;

        //读取每一行的内容  
        string[] lineArray = binAsset.text.Split("\r"[0]);

        //创建二维数组  
        Array = new string[lineArray.Length][];

        //把csv中的数据储存在二位数组中  
        for (int i = 0; i < lineArray.Length; i++)
        {
            Array[i] = lineArray[i].Split(',');
        }
        //string t = GetDataByRowAndCol(1, 1);
        //Debug.Log(t);

       // string t1 = GetDataByIdAndName(3, "pos");
        //Debug.Log(t1);

        string t2 = GetIdString(2);
        //Debug.Log(t2);

        alength = Array.Length;
        //Debug.Log(alength);


        /************************读取监控地址**************************/
        Path = new List<string>();

        //读取csv二进制文件  
        TextAsset binAsset1 = Resources.Load("ViewPath", typeof(TextAsset)) as TextAsset;

        //读取每一行的内容  
        string[] lineArray1 = binAsset1.text.Split("\r"[0]);

        //创建二维数组  
        arrayPath = new string[lineArray1.Length][];

        //把csv中的数据储存在二位数组中  
        for (int i = 0; i < lineArray1.Length; i++)
        {
            arrayPath[i] = lineArray1[i].Split(',');
        }
        /************************读取监控地址**************************/

        string pt = GetIdStringPath();
        //Debug.Log(Path[1]);

    }

    /************************读取监控地址**************************/
    public static string GetIdStringPath()
    {
        if (arrayPath.Length <= 0)
            return "";

        int nRow = arrayPath.Length;
        int nCol = arrayPath[0].Length;
        if (Path.Count > 0)
        {
            Path.Clear();
        }
        

        for (int i = 1; i < nRow; ++i)
        {

            for (int j = 0; j < nCol; ++j)
            {
                if (j > 0)
                {
                    Path.Add(arrayPath[i][j]);
                    //Debug.Log(arrayPath[i][j]);
                }
            }

        }
        return "";
    }
    /************************读取监控地址**************************/

    string GetDataByRowAndCol(int nRow, int nCol)//几行几列数据
    {
        if (Array.Length <= 0 || nRow >= Array.Length)
            return "";
        if (nCol >= Array[0].Length)
            return "";

        return Array[nRow][nCol];
    }

   public static string GetIdString(int nId)
    {
        if (Array.Length <= 0)
            return "";

        int nRow = Array.Length;
        int nCol = Array[0].Length;
        string posx = null;


        for (int i = 1; i < nRow; ++i)
        {
            string strId = string.Format("\n{0}", nId);
            if (Array[i][0] == strId)
            {
                for (int j = 0; j < nCol; ++j)
                {
                    if (j > 0)
                    {
                       if (j == nCol - 1)
                       {
                           posx = posx + Array[i][j];
                       }
                       else
                       {
                           posx = posx + Array[i][j] + ",";
                       }
                    }
                }
                return posx;
            }
        }
        return "";
    }

    string GetDataByIdAndName(int nId, string strName)//ID+列名
    {
        if (Array.Length <= 0)
            return "";

        int nRow = Array.Length;
        int nCol = Array[0].Length;
        for (int i = 1; i < nRow; ++i)
        {
            string strId = string.Format("\n{0}", nId);
            if (Array[i][0] == strId)
            {
                for (int j = 0; j < nCol; ++j)
                {
                    if (Array[0][j] == strName)
                    {
                        return Array[i][j];
                    }
                }
            }
        }

        return "";
    }
}
