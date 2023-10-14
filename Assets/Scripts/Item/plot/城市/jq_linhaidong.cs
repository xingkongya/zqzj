using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class jq_linhaidong : MonoBehaviour
{
    //基础
    private float timer;//定时器
    private Dictionary<string, UnityAction> 村长选项 = new Dictionary<string, UnityAction>();
    private io io_;

    //自定义
    private G_Util gut;

    private void Awake()
    {
        gut =NameMgr.画布.GetComponent<G_Util>();
        io_ = io.GetInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        gut.移动与坐标.Add("左", "临海城");
        gut.刷新移动与坐标();

        timer = Random.Range(1.0f, 3.0f); // 随机秒数
        role_Data myData = io_.load();
        myData.复活城市 = "临海城";
        io_.save(myData);
    }

    // Update is called once per frame
    void Update()
    {
        //生成/显示...气泡计时器
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //判断生成还是销毁气泡
            if (gameObject.transform.Find("气泡_位置")!=null&&gameObject.transform.Find("气泡_位置").childCount!=0 && gameObject.transform.Find("气泡_位置/气泡(Clone)").gameObject.activeSelf)
            {
                gameObject.transform.Find("气泡_位置/气泡(Clone)").gameObject.SetActive(false);
                timer = Random.Range(1.0f, 3.0f); // 随机秒数
            }
            else
            {
                if (gameObject.name.Equals("罗凡"))//判断父物体
                    罗凡气泡();
                else if (gameObject.name.Equals("李然"))
                    李然气泡();
                timer = 2.0f; // 显示2秒
            }
            
        }

    }

    void 罗凡气泡() {
        string str = "青莲宗是垃圾!";
        gut.生成气泡(str, gameObject);
        }

    void 李然气泡()
    {
        string str = "血刀门是垃圾!";
        gut.生成气泡(str, gameObject);
    }

  
    public void 罗凡对话索引() {
        罗凡对话();
    }

    public void 李然对话索引()
    {
            李然对话();
    }


    public void 裁缝对话索引()
    {
       裁缝对话();
    }





    private void 李然对话()
    {
        string str = "我青莲宗传承千年,名门正派,有金丹真人十二尊,更有奇阵-青莲剑阵,攻防无双!宗门比的是底蕴,岂是青黄不接的魔宗能比的";
        gut.生成对话框(str, 0, 0.07f, "李然对话");
        gut.关闭杂项();
    }



    private void 罗凡对话()
    {
        string str = "我血刀门有元婴期的血刀老祖坐镇,青莲宗一个连元婴期都没有的宗门怎么和我们比?\n光有底蕴没有实力的护持,不过是任人宰割的鱼肉罢了";
        gut.生成对话框(str, 0, 0.07f, "罗凡对话");
        gut.关闭杂项();
    }

    private void 裁缝对话()
    {
        string str = "原先我这里可以制作时装的...\n直到某一天遇到个大佬把我能力封印了,完事嘴里还嘀咕着什么'美术资源太难搞了,关了关了...'.我听不太懂,但是大感冤枉!";
        gut.生成对话框(str, 0, 0.07f, "裁缝对话");
        gut.关闭杂项();
    }



}
