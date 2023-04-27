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
    private G_Util gut;

    private void Awake()
    {
        怪物信息 = GameObject.Find("combat_other").GetComponent<UT_monster>();
        怪物脚本 = (I_monster)怪物信息.GetComponent(怪物信息.当前地图脚本名字);
        bm = basicMgr.GetInstance();
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
        if (怪物信息.怪物图标.Equals(""))
            图标.sprite = Resources.Load<Sprite>("怪物/普通怪物") as Sprite;
        else
            图标.sprite = Resources.Load<Sprite>("怪物/普通怪物") as Sprite;

        Text 等级 = gameObject.transform.Find("前端/等级").GetComponent<Text>();
        等级.text = "Lv:" + 怪物信息.最低等级 + "~" + 怪物信息.最高等级;
        Text 品质 = gameObject.transform.Find("前端/品质/品质").GetComponent<Text>();
        if (怪物信息.怪物品质 == 0)
        {
            品质.text = "普通";
        }
        else if (怪物信息.怪物品质 == 1)
        {
            品质.text = "精英";
            品质.color = bm.转换颜色(怪物信息.怪物品质); ;
        }
        else if (怪物信息.怪物品质 == 2)
        {
            品质.text = "稀有";
            品质.color = bm.转换颜色(怪物信息.怪物品质); ;
        }
        else if (怪物信息.怪物品质 == 3)
        {
            品质.text = "史诗";
            品质.color = bm.转换颜色(怪物信息.怪物品质); ;
        }
        else if (怪物信息.怪物品质 == 4)
        {
            品质.text = "仙兽";
            品质.color = bm.转换颜色(怪物信息.怪物品质); ;
        }
        else if (怪物信息.怪物品质 == 5)
        {
            品质.text = "神兽";
            品质.color = bm.转换颜色(怪物信息.怪物品质); ;
        }

        //信息填充-中端
        gameObject.transform.Find("中端/Panel/攻击资质/Text").GetComponent<Text>().text = (怪物信息.攻击力资质 * 1000) + "";
        gameObject.transform.Find("中端/Panel/防御力资质/Text").GetComponent<Text>().text = (怪物信息.防御力资质 * 1000) + "";
        gameObject.transform.Find("中端/Panel/血量资质/Text").GetComponent<Text>().text = (怪物信息.血量资质 * 1000) + "";
        gameObject.transform.Find("中端/Panel/攻击速度/Text").GetComponent<Text>().text = 怪物信息.攻击速度 + "s";
        gameObject.transform.Find("中端/Panel/暴击概率/Text").GetComponent<Text>().text = 怪物信息.基础暴击率 + "%";
        gameObject.transform.Find("中端/Panel/成长/Text").GetComponent<Text>().text = 怪物信息.成长 + "";

        //信息填充-后端
        GameObject 掉落信息表 = gameObject.transform.Find("后端/掉落/Panel/Scroll View/Viewport/Content").gameObject;
        int 表长度;
        if (怪物信息.掉落信息.Count % 3 != 0)
            表长度 = (怪物信息.掉落信息.Count / 3) + 1;
        else
            表长度 = 怪物信息.掉落信息.Count / 3;


        掉落信息表.GetComponent<RectTransform>().sizeDelta = new Vector2(432, 120 * 表长度);
        for (int i = 0; i < 怪物信息.掉落信息.Count; i++)
        {
            gut.生成掉落项(掉落信息表, 怪物信息.掉落信息[i], i, 怪物信息.掉落信息.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
