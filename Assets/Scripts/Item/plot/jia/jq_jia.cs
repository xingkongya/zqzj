using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jq_jia : MonoBehaviour
{
    //基础
    private float timer;//定时器

    //自定义
    private Text_DaYin dy;
    private G_Util ut;
    private PropMgr pm;

    private void Awake()
    {
        ut = NameMgr.画布.GetComponent<G_Util>();
        pm = PropMgr.GetInstance();
    }



    // Start is called before the first frame update
    void Start()
    {
        timer= Random.Range(1.0f, 3.0f); // 随机秒数
        ut.移动与坐标.Add("上", "桃源村");
        ut.刷新移动与坐标();
    }

    // Update is called once per frame
    void Update()
    {
        //生成/显示...气泡计时器
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //判断生成还是隐藏气泡
            if (gameObject.transform.Find("气泡_位置").transform.childCount != 0)
            {
                if (gameObject.transform.Find("气泡_位置").transform.Find("气泡(Clone)").gameObject.activeSelf)
                    gameObject.transform.Find("气泡_位置").transform.Find("气泡(Clone)").gameObject.SetActive(false);
                else
                    gameObject.transform.Find("气泡_位置").gameObject.transform.GetChild(0).gameObject.SetActive(true);

                timer = Random.Range(1.0f, 3.0f); // 随机秒数
            }
            else
            {            
                if (gameObject.name.Equals("母亲"))//判断父物体
                    母亲气泡();
                timer = 2.0f; // 显示2秒
            }
            
        }

    }

    void 母亲气泡() {
        string str = "咳.咳.咳...";
        ut.生成气泡(str, gameObject);
        }

    public void 母亲对话索引() {
        int index_talk = PlayerPrefs.GetInt("母亲");
        int index_遗物 = PlayerPrefs.GetInt("村长的帮助");
        if (index_talk == 0)
            母亲剧情0(index_talk);
        else if (index_遗物<1)
            母亲剧情1(index_talk);
        /*else if (index_村长 >= 3&& index_玉佩==0)
            母亲剧情2(index_村长);*/
        else
            母亲对话(index_talk);
    }

    

    private void 母亲对话(int index) {
        string str = "天涯何处无芳草,不要为了一棵树放弃一片森林呀.";
        ut.生成对话框(str, index, 0.08f, "母亲");
    }
    private void 母亲剧情0(int index)
    {
        EventCenter.GetInstance().AddEventListener("对话后剧情", 获得馒头);
        string str = "孩子,振作起来,你一定可以在三年后打败她的!!!这里是一些干粮,你爸在荒原打猎,你去找到他吧...他有些东西要给你";
        ut.生成对话框(str, index, 0.08f, "母亲");
    }


    public void 获得馒头() {
        ut.生成获得框("馒头",10);
        pm.获取物品("馒头", 10);
        EvenMgr.GetInstance().存档添加事件("获得宠物", "无限");
    }


    private void 母亲剧情1(int index)
    {
        string str = "村里的大家也都支持你,你去找村长,他有话要对你说";
        ut.生成对话框(str, index, 0.08f, "母亲");
    }
   

}
