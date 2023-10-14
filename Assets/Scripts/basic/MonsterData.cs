using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterData : MonoBehaviour
{
    public int 方法索引;
    private RectTransform rt;
    private I_monster 怪物脚本;
    private UT_monster 怪物信息;
    private basicMgr bm;
    private PropMgr pm;
    private G_Util gut;

    private void Awake()
    {

        怪物信息 = GameObject.Find("combat_other").GetComponent<UT_monster>();
        怪物脚本 = (I_monster)怪物信息.GetComponent(怪物信息.当前地图脚本名字);
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
        rt = gameObject.GetComponent<RectTransform>();
        gut = NameMgr.画布.GetComponent<G_Util>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (rt.anchoredPosition.x != 0)
            rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y);



        combat cb = NameMgr.cb;
        if (方法索引 == 1)
            怪物脚本.怪物1赋值(cb);
        else if (方法索引 == 2)
            怪物脚本.怪物2赋值(cb);
        else if (方法索引 == 3)
            怪物脚本.怪物3赋值(cb);
        else if (方法索引 == 4)
            怪物脚本.怪物4赋值(cb);
        else if (方法索引 == 5)
            怪物脚本.怪物5赋值(cb);

        //信息填充-前端
        Text name = gameObject.transform.Find("前端/名字").GetComponent<Text>();
        name.text = 怪物信息.怪物名字;
        name.color = bm.转换颜色(怪物信息.怪物品质);
        Image 图标 = gameObject.transform.Find("前端/图标").GetComponent<Image>();
        Image 背景 = gameObject.transform.Find("背景").GetComponent<Image>();
        if (怪物信息.怪物图标.Equals(""))
        {
            图标.sprite = Resources.Load<Sprite>("怪物/水墨龙") as Sprite;
            背景.sprite = Resources.Load<Sprite>("怪物/水墨龙") as Sprite;
        }
        else
        {
            图标.sprite = Resources.Load<Sprite>("怪物/水墨龙") as Sprite;
            背景.sprite = Resources.Load<Sprite>("怪物/水墨龙") as Sprite;
        }

        Text 等级 = gameObject.transform.Find("前端/等级").GetComponent<Text>();
        等级.text = "Lv:" + 怪物信息.最低等级 + "~" + 怪物信息.最高等级;
        Text 品质 = gameObject.transform.Find("前端/品质/品质").GetComponent<Text>();
        Image 边框 = gameObject.transform.Find("背景/边框").GetComponent<Image>();
        if (怪物信息.怪物品质 == 0)
        {
            品质.text = "普通";
        }
        else if (怪物信息.怪物品质 == 1)
        {
            品质.text = "精英";       
        }
        else if (怪物信息.怪物品质 == 2)
        {
            品质.text = "稀有";
        }
        else if (怪物信息.怪物品质 == 3)
        {
            品质.text = "史诗";
        }
        else if (怪物信息.怪物品质 == 4)
        {
            品质.text = "仙兽";
        }
        else if (怪物信息.怪物品质 == 5)
        {
            品质.text = "神兽";
        }
        品质.color = bm.转换颜色(怪物信息.怪物品质);
        边框.color = bm.转换颜色(怪物信息.怪物品质);
        边框.color = bm.改变透明度(边框.gameObject,100f);

        //信息填充-中端
        gameObject.transform.Find("中端/Panel/经验值/Text").GetComponent<Text>().text = (怪物信息.经验值系数 * 怪物信息.当前等级+怪物信息.基础经验值) + "";
        gameObject.transform.Find("中端/Panel/战斗力/Text").GetComponent<Text>().text = gut.数字增加单位( 怪物信息.返回战斗力(怪物信息.最高等级) + "");
        gameObject.transform.Find("中端/Panel/攻击速度/Text").GetComponent<Text>().text = 怪物信息.攻击速度 + "s";
        gameObject.transform.Find("中端/Panel/成长/Text").GetComponent<Text>().text = 怪物信息.成长 + "";

        //信息填充-后端
        /* GameObject 掉落信息表 = gameObject.transform.Find("后端/掉落").gameObject;
         int 表长度;
         if (怪物信息.掉落信息.Count % 3 != 0)
             表长度 = (怪物信息.掉落信息.Count / 3) + 1;
         else
             表长度 = 怪物信息.掉落信息.Count / 3;


         掉落信息表.GetComponent<RectTransform>().sizeDelta = new Vector2(432, 120 * 表长度);
         for (int i = 0; i < 怪物信息.掉落信息.Count; i++)
         {
             gut.生成掉落项(掉落信息表, 怪物信息.掉落信息[i], i, 怪物信息.掉落信息.Count);
         }*/
        Text 掉落信息表 = gameObject.transform.Find("后端/Panel/掉落").GetComponent<Text>();
        string str_掉落="掉落:";
        for (int i = 0; i < 怪物信息.掉落信息.Count; i++) {
            string 物品名字 = 怪物信息.掉落信息[i];
            Prop_bascis 物品 = pm.检索物品(物品名字);
            str_掉落 += "<color="+bm.返回颜色代码(bm.Xstoi(物品.qua))+">"+物品名字+"</color>. ";
        }
        掉落信息表.text = str_掉落;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
