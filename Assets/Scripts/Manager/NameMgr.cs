using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameMgr : BaseManager<NameMgr>
{
    public static Dictionary<string, GameObject> 对象池 = new Dictionary<string, GameObject>();


    public static GameObject 画布 {

        get
        {
            if (DataMgr.GetInstance().本地对象.ContainsKey("老画布"))
                return DataMgr.GetInstance().本地对象["老画布"];
            else if (GameObject.Find("老画布") != null)
                return GameObject.Find("老画布");
            else
                return GameObject.Find("画布");

        }
    }


    public static Camera Camera
    {

        get
        {
            return 画布.transform.Find("Main Camera").GetComponent<Camera>();

        }
       
    }

    public static GameObject 背包
    {

        get
        {
            if (对象池.ContainsKey("背包(Clone)"))
                return 对象池["背包(Clone)"];
            else if (GameObject.Find("背包(Clone)") != null)
                return GameObject.Find("背包(Clone)");
            else
                return null;

        }
    }

    public static Vector3 背包图片坐标 {
        get
        {
            return GameObject.Find("bag_bg_pic").transform.position;

        }
    }

    public static GameObject 人物
    {

        get
        {
            if (对象池.ContainsKey("人物"))
                return 对象池["人物"];
            else if (GameObject.FindGameObjectWithTag("人物")!= null)
                return DataMgr.GetInstance().本地对象["主角"];
            else
                return null;

        }
    }

    public static GameObject chat
    {
        get {
            if (GameObject.Find("chat_bg") != null)
                return GameObject.Find("chat_bg");
            else
                return null;
        }

    }


    public static GameObject UI
    {

        get
        {
            if (对象池.ContainsKey("UI"))
                return 对象池["UI"];
            else if (GameObject.Find("UI") != null)
                return GameObject.Find("UI");
            else
                return null;

        }
    }

    public static combat cb
    {

        get
        {
            GameObject NewGOJ= GameObject.Find("NewGOJ");
            if (NewGOJ == null)
            {
                NewGOJ = new GameObject("NewGOJ");
                NewGOJ.AddComponent<combat>();
            }
            return NewGOJ.GetComponent<combat>();

        }
    }


    public static System.Random 随机类 {

        get {        
        return new System.Random(Guid.NewGuid().GetHashCode());
        }
    }


    public static io IO
    {

        get
        {
            return io.GetInstance();
        }
    }


}
