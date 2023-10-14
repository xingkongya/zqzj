using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class juqing : MonoBehaviour
{
    //基本
    private int index=-1;
    public string name_ = "王大牛";
    private int str_index = 0;
    private bool is_flash = false;
    private ArrayList 台词;
    //引用
    private Text Text_juqing;
    private G_Util ut;
    //自定义

    private void Awake()
    {
        Text_juqing = GameObject.Find("Text_juqing").GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //根据index判断剧情进度,方法写死
        index=PlayerPrefs.GetInt("剧情");
        if (index == 0)
            台词=初始剧情();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_flash)
        {
            Text_juqing.text = "";
            is_flash = false;
            if (str_index <= 台词.Count)
            {
                for (int i = 0; i < str_index; i++)
                {
                    Text_juqing.text += 台词[i] + "\n";//6个空格
                }
            }else
                屏幕渐黑退出剧情();

        }
    }

    public void 点击屏幕() {
        str_index++;
        is_flash = true;
    }

   ArrayList 初始剧情() {
        台词 = new ArrayList();
        Text_juqing.fontSize = 35;
        string str0 = "<size=50>序章:穿越</size>"; 台词.Add(str0);
        string str0_1 = "我叫" + "<color=green>" + name_ + "</color>,"+"是某点一名扑街的网络写手."; 台词.Add(str0_1);
        string str1 = "我一直认为扑街的原因是因为我的文学积累不够,"+ "所以平时有空的时候就看看同行写的书"; 台词.Add(str1);
        string str2 = "今天舒舒服服的瘫在床上,一边充电一边开始今天的例行扫书..."; 台词.Add(str2);
        string str3 = "《诸天最强主角模拟器》  简介:"; 台词.Add(str3);
        string str4 = "<color=blue>刚刚穿越的你遭到了青梅竹马的退婚.</color>"; 台词.Add(str4);
        string str4_1 = "<color=blue>因为她灵根上佳,拜入山海仙宗成为真传弟子,是万里挑一的天之娇女.</color>"; 台词.Add(str4_1);
        string str4_2 = "<color=blue>而你则是毫无灵根的凡人...</color>"; 台词.Add(str4_2);
        string str5 = "<color=blue>你无法忍受这份羞辱,喊道,'三十年河东,三十年河西,莫欺少年穷!',并约定三年后一战</color>"; 台词.Add(str5);
        string str6 = "<color=blue>她不屑的嗤笑了一声,答应了你的约战,说要当众把你踩到地上.</color>"; 台词.Add(str6);
        string str7 = "<color=blue>你怒不可遏之时,脑海中突然响起'滴,纵横诸天系统加载成功'...</color>"; 台词.Add(str7);
        string str7_0 = "<color=blue>作为穿越者当然明白获得了金手指,不禁嘴角上扬,冷酷一笑.</color>"; 台词.Add(str7_0);
        string str7_1 = "<color=blue>三年后...你在山海宗打败了青梅竹马,才知道了她不过是上界大佬来历练的一尊分身.</color>"; 台词.Add(str7_1);
        string str7_2 = "<color=blue>你毅然决然的决定前往上界,拥有外挂的你无所畏惧.</color>"; 台词.Add(str7_2);
        string str8 = "<color=blue>岁月悠悠,不知道多少年后,当你无敌于诸天,于仙路尽头回望一生,感概道:</color>"; 台词.Add(str8);
        string str9 = "<color=blue>'我" + "<color=green>" + name_ + " </color>" + "一身修为全靠自己努力...深红,加点!'</color>"; 台词.Add(str9);
        string str10 = "看完简介我整个人都麻了,这是什么狗血套路缝合怪."; 台词.Add(str10);
        string str11 = "于是我毫不犹豫的点了关闭."; 台词.Add(str11);
        string str12 = "谁知手机突然黑屏,一阵电流瞬间击中了全身"; 台词.Add(str12);
        string str13 = "身体逐渐的失去了知觉,在意识模糊之时,我仿佛听到了:'滴,诸天纵横系统加载成功'...'"; 台词.Add(str13);

        return 台词;

    }

    void 屏幕渐黑退出剧情() {
        index++;
        PlayerPrefs.SetInt("剧情",-1);
        //添加事件-三年之约
        NewPlay.GetInstance().初次进入游戏();
        gameObject.SetActive(false);
    }




}
