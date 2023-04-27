using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TianFuPanel : MonoBehaviour
{
    private basicMgr bm;
    public TianFu tf;

    private void Awake()
    {
        bm = basicMgr.GetInstance();
    }

    private void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void 刷新天赋信息(TianFu tf) {
        Text 名字文本= gameObject.transform.Find("name").GetComponent<Text>();
        名字文本.text = tf.name;
        名字文本.color = bm.转换颜色(int.Parse( tf.qua));

        Text 当前等级 = gameObject.transform.Find("lv").GetComponent<Text>();
        当前等级.text ="+"+ tf.lv;

        Text 等级上限 = gameObject.transform.Find("maxlv").GetComponent<Text>();
        if (tf.MaxLv.Equals("无限"))
        {
            等级上限.text = "(" + tf.lv + "/∞)";
        }
        else {
            等级上限.text = "(" + tf.lv + "/"+tf.MaxLv+")";

        }

        Text 类型文本 = gameObject.transform.Find("place").GetComponent<Text>();
        类型文本.text = tf.place;

        Text 介绍文本 = gameObject.transform.Find("commend").GetComponent<Text>();
        介绍文本.text = tf.comment;

        Text 当前效果文本 = gameObject.transform.Find("effect").GetComponent<Text>();
        当前效果文本.text = tf.effect;

        Text 下级效果文本 = gameObject.transform.Find("next").GetComponent<Text>();
        下级效果文本.text = tf.next;
    }
}
