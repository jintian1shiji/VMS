using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBoundry : MonoBehaviour {

    //使用显示轮廓的简单材质  
    public Material mSimpleMat;
    //使用显示轮廓的高级材质  
    public Material mAdvanceMat;
    //默认材质  
    public Material mDefaultMat;

    public GameObject mText;


    void Update()
    {
        //获取鼠标位置  
        Vector3 mPos = Input.mousePosition;
        //向物体发射射线  
        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mHit;
        //射线检验  
        if (Physics.Raycast(mRay, out mHit))
        {
            //Cube  
            if (mHit.collider.gameObject.tag == "Cube")
            {
                //将当前选中的对象材质换成带轮廓线的材质  
                mHit.collider.gameObject.GetComponent<Renderer>().material = mSimpleMat;
                //将未选中的对象材质换成默认材质  
                GameObject.Find("Sphere").GetComponent<Renderer>().material = mDefaultMat;
                //设置提示信息  
                //GameObject.Find("GUIText").GetComponent<GUIText>().text = "当前选择的对象是:Cube";
                //mText.GetComponent<Text>().text = "当前选择的对象是:Cube";
            }
            //Sphere  
            if (mHit.collider.gameObject.tag == "Sphere")
            {
                //将当前选中的对象材质换成带轮廓线的材质  
                mHit.collider.gameObject.GetComponent<Renderer>().material = mSimpleMat;
                //将未选中的对象材质换成默认材质  
                GameObject.Find("Cube").GetComponent<Renderer>().material = mDefaultMat;
                //设置提示信息  
                //GameObject.Find("GUIText").GetComponent<GUIText>().text = "当前选择的对象是:Sphere";
                //mText.GetComponent<Text>().text = "当前选择的对象是:Sphere";
            }
            //Person  
            if (mHit.collider.gameObject.tag == "Person")
            {
                //由于人物模型的材质较为复杂，所以不能使用这种方法  
            }
        }

    }
}