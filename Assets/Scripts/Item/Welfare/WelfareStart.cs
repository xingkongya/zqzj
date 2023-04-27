using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelfareStart : MonoBehaviour
{
    private G_Util gut;

    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 五十级保送礼包界面() {
        Sprite 头图片 = Resources.Load<Sprite>("图片/简约木板");
        string 内容 = "<color=red><size=40><b>新手保送50级大礼包</b></size></color>\n" +
            "<b>10级: </b> 大松果x10, <color=yellow>50仙晶卡</color>x1\n" +
            "<b>20级: </b> 送<color=red>满资质宠物</color> - <color=aqua>≮黄金鸟≯</color>, <color=yellow>50仙晶卡</color>x4, 下品涅槃果x1\n" +
            "<b>30级: </b> 下品涅槃果x10, <color=yellow>100仙晶卡</color>x3, <color=aqua>蓝色精华</color>x1\n" +
            "<b>40级: </b> 中品涅槃果x1, <color=yellow>100仙晶卡</color>x5, <color=aqua>蓝色精华</color>x2\n" +
            "<b>50级: </b> <color=red>造化金丹</color>x1,  <color=red>1000仙晶卡</color>,  <color=aqua>蓝色精华</color>x2";
        gut.生成奖励获取界面(头图片, 内容, "新手保送50级大礼包");
    }

    public void 生成兑换界面() {
        gut.生成兑换码界面();
    }

    public void 生成主角光环界面()
    {
        gut.生成主角光环界面(gameObject.transform.parent.gameObject);
    }

    public void 生成深红加点界面()
    {
        gut.生成深红加点界面(gameObject.transform.parent.gameObject);
    }
}
