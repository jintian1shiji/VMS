using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class phpMySQL : MonoBehaviour
{
    string url = "http://10.12.6.28/WE/addscore.php";

    void Start()
    {
        //StartCoroutine(submit_highscore(100005, 123456));
        //StartCoroutine(delete_highscore("tom"));
        //StartCoroutine(delete_all_highscore());
        //StartCoroutine(update_highscore("tom", 233));
        //StartCoroutine(show_highscore());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //StartCoroutine(submit_highscore(100005, 123456));
            //StartCoroutine(delete_highscore(100005));
            //StartCoroutine(update_highscore(100001, 456789));
            StartCoroutine(show_highscore());
        }
    }

    //增
    IEnumerator submit_highscore(int player_name, int player_score)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "submit_highscore");
        form.AddField("name", player_name);
        form.AddField("score", player_score);

        WWW www = new WWW(url, form);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        Debug.Log(www.text);
    }

    //删，全部
    IEnumerator delete_all_highscore()
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "delete_highscore");

        WWW www = new WWW(url, form);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        Debug.Log(www.text);
    }

    //删，指定
    IEnumerator delete_highscore(int player_name)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "delete_highscore");
        form.AddField("name", player_name);

        WWW www = new WWW(url, form);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        Debug.Log(www.text);
    }

    //改，指定
    IEnumerator update_highscore(int player_name, int player_score)
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "update_highscore");
        form.AddField("name", player_name);
        form.AddField("score", player_score);

        WWW www = new WWW(url, form);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        Debug.Log(www.text);
    }

    //查
    IEnumerator show_highscore()
    {
        WWWForm form = new WWWForm();
        form.AddField("action", "show_highscore");

        WWW www = new WWW(url, form);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }
        Debug.Log(www.text);

        var received_data = Regex.Split(www.text, "</next>");
        int scores = (received_data.Length - 1) / 2;

        for (int i = 0; i < scores; i++)
        {
            print("Name: " + received_data[2 * i] + " Score: " + received_data[2 * i + 1]);
        }
    }
}
