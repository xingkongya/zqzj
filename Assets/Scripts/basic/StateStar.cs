using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateStar : MonoBehaviour
{
    private io io_;
    private G_Util gut;
    private basicMgr bm;
    private RoleMgr rm;
    private PetMgr pem;
    private PropMgr pm;
    private SkillApplicator sa;
    private role_Data myData;
    public GameObject 子界面;
    public string 炼化_当前模式;
    public string 转换_材料;
    private Slider 数量拉条;
    private InputField 输入文本;

    private void Awake()
    {
        io_ = io.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
        bm = basicMgr.GetInstance();
        rm = RoleMgr.GetInstance();
        pem = PetMgr.GetInstance();
        pm = PropMgr.GetInstance();
        sa = SkillApplicator.GetInstance();
        子界面 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面").gameObject;
        GameObject 升级按钮 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/升级背景/升级按钮").gameObject;
        GameObject 提升成长按钮 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/成长背景/提升成长按钮").gameObject;
        GameObject 出战按钮 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/出战").gameObject;
        数量拉条 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/button/Slider").GetComponent<Slider>();
        输入文本 = 数量拉条.transform.Find("InputField").GetComponent<InputField>();
        bm.Banding(升级按钮, 升级);
        bm.Banding(提升成长按钮, 提升成长);
        bm.Banding(出战按钮, 出战);
    }


    private void OnEnable()
    {
        初始化宠物菜单();
    }

    public void 初始化宠物菜单()
    {
        加载宠物菜单(true);
        加载法宝菜单(false);
        点击升级界面();
        初始化宠物栏();
    }

    public void 初始化法宝菜单()
    {
        加载法宝菜单(true);
        加载宠物菜单(false);
    }

    private void 加载宠物菜单(bool isActive)
    {
        GameObject 宠物菜单 = gameObject.transform.Find("背景/菜单栏/宠物菜单").gameObject;
        Image 宠物界面水墨 = gameObject.transform.Find("背景/菜单栏/菜单栏项目/宠物").gameObject.GetComponent<Image>();//FFB66E
        Image 宠物界面图标 = gameObject.transform.Find("背景/菜单栏/菜单栏项目/宠物/图标信息/Image").gameObject.GetComponent<Image>();//484643
        Image 宠物界面标题 = gameObject.transform.Find("背景/菜单栏/菜单栏项目/宠物/图标信息/Image").gameObject.GetComponent<Image>();//FFB66E
        Color nowColor;
        if (isActive)
        {
            ColorUtility.TryParseHtmlString("#FFB66E", out nowColor);
            宠物界面水墨.color = nowColor;
            宠物界面标题.color = nowColor;
            ColorUtility.TryParseHtmlString("#484643", out nowColor);
            宠物界面图标.color = nowColor;
            宠物菜单.SetActive(true);
        }
        else
        {
            ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
            宠物界面水墨.color = nowColor;
            宠物界面图标.color = nowColor;
            ColorUtility.TryParseHtmlString("#323232", out nowColor);
            宠物界面标题.color = nowColor;
            宠物菜单.SetActive(false);
        }
    }


    private void 加载法宝菜单(bool isActive)
    {
        GameObject 法宝菜单 = gameObject.transform.Find("背景/菜单栏/法宝菜单").gameObject;
        Image 法宝界面水墨 = gameObject.transform.Find("背景/菜单栏/菜单栏项目/法宝").gameObject.GetComponent<Image>();//FFB66E
        Image 法宝界面图标 = gameObject.transform.Find("背景/菜单栏/菜单栏项目/法宝/图标信息/Image").gameObject.GetComponent<Image>();//484643
        Image 法宝界面标题 = gameObject.transform.Find("背景/菜单栏/菜单栏项目/法宝/图标信息/Image").gameObject.GetComponent<Image>();//FFB66E
        Color nowColor;
        if (isActive)
        {
            ColorUtility.TryParseHtmlString("#FFB66E", out nowColor);
            法宝界面水墨.color = nowColor;
            法宝界面标题.color = nowColor;
            ColorUtility.TryParseHtmlString("#484643", out nowColor);
            法宝界面图标.color = nowColor;
            法宝菜单.SetActive(true);
        }
        else
        {
            ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
            法宝界面水墨.color = nowColor;
            法宝界面图标.color = nowColor;
            ColorUtility.TryParseHtmlString("#323232", out nowColor);
            法宝界面标题.color = nowColor;
            法宝菜单.SetActive(false);
        }
    }

    private void 初始化宠物栏()
    {
        Text 宠物数量 = gameObject.transform.Find("背景/菜单栏/宠物菜单/宠物栏/标题/宠物数量").GetComponent<Text>();
        GameObject 宠物栏 = gameObject.transform.Find("背景/菜单栏/宠物菜单/宠物栏/Panel").gameObject;
        gut.操作子物体(宠物栏, 3);
        myData = io_.load();

        for (int i = 0; i < myData.宠物栏.Count; i++)
        {
            gut.生成宠物项(i, myData.宠物栏[i], myData.宠物栏[i].id, 宠物栏);
        }
        宠物数量.text = "宠物栏:" + myData.宠物栏.Count + "/6";
    }


    private void 初始化宠物信息()
    {

        Sprite 初始宠物 = Resources.Load<Sprite>("怪物/宠物背景");
        Image 图标 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/背景/图标").GetComponent<Image>();
        //Image 边框= gameObject.transform.Find("背景/边框").GetComponent<Image>();
        //图标.sprite = 初始宠物;
        //边框.sprite = 初始宠物;
        //边框.color = bm.改变透明度(边框.gameObject, 60f);


        //信息填充
        Text 当前宠物 = gameObject.transform.Find("背景/菜单栏/宠物菜单/宠物栏/标题/当前宠物信息/名字").GetComponent<Text>();
        当前宠物.text = "";
        Text 名字 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/名字/Text").GetComponent<Text>();
        名字.text = "";
        Text 评价 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/评价").GetComponent<Text>();
        评价.text = "";
        Text 成长 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/成长/Text").GetComponent<Text>();
        成长.text = "";
        Text 成长上限 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/成长上限/Text").GetComponent<Text>();
        成长上限.text = "";
        Text 等级 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/等级/Text").GetComponent<Text>();
        等级.text = "";
        Text xing = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/星级/Text").GetComponent<Text>();
        xing.text = "";
        Text 攻击力 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/攻击力/Text").GetComponent<Text>();
        攻击力.text = "";
        Text 防御力 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/防御力/Text").GetComponent<Text>();
        防御力.text = "";
        Text 血量 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/血量/Text").GetComponent<Text>();
        血量.text = "";
        Text 回血值 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/回血值/Text").GetComponent<Text>();
        回血值.text = "";
        Text 攻速 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/攻击速度/Text").GetComponent<Text>();
        攻速.text = "";
        Text 暴击 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/属性界面/Panel/属性信息/暴击率/Text").GetComponent<Text>();
        暴击.text = "";

        Text 经验池文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/升级背景/经验池").GetComponent<Text>();
        Text 所需经验文本文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/升级背景/升级所需经验").GetComponent<Text>();
        Text 精华数量文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/成长背景/拥有数量/num").GetComponent<Text>();
        Text 提升所需文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/成长背景/提升所需/num").GetComponent<Text>();
        Text 需求精华名字文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/成长背景/拥有数量/name").GetComponent<Text>();
        Text 拥有精华名字文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/成长背景/提升所需/name").GetComponent<Text>();
        需求精华名字文本.text = "";
        拥有精华名字文本.text = "";
        精华数量文本.text = "";
        提升所需文本.text = "";
        经验池文本.text = "拥有经验:    ";
        所需经验文本文本.text = "所需经验:    ";

        Text 出战文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/出战/Text").GetComponent<Text>();
        出战文本.text = "出战";
        Color nowColor;
        ColorUtility.TryParseHtmlString("#94DDEC", out nowColor);
        出战文本.transform.parent.GetComponent<Image>().color = nowColor;


    }



    public void 点击升级界面()
    {
        gut.操作子物体(子界面, 1);
        子界面.transform.Find("初始界面").gameObject.SetActive(true);
        初始化宠物信息();
    }

    public void 点击炼化界面()
    {
        gut.操作子物体(子界面, 1);
        子界面.transform.Find("炼化界面").gameObject.SetActive(true);
        显示转换模式();
    }

    public void 点击洗练界面()
    {
        gut.操作子物体(子界面, 1);
        子界面.transform.Find("洗练界面").gameObject.SetActive(true);
        初始化洗练信息();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void 显示炼化模式()
    {
        GameObject 炼化模式 = 子界面.transform.Find("炼化界面/界面").gameObject;
        gut.操作子物体(炼化模式, 1);
        炼化模式.transform.Find("炼宠界面").gameObject.SetActive(true);
        子界面.transform.Find("炼化界面/button/合成/Text").GetComponent<Text>().text = "炼化";
        炼化_当前模式 = "炼化模式";
    }

    public void 显示转换模式()
    {
        GameObject 炼化模式 = 子界面.transform.Find("炼化界面/界面").gameObject;
        gut.操作子物体(炼化模式, 1);
        炼化模式.transform.Find("转换界面").gameObject.SetActive(true);
        子界面.transform.Find("炼化界面/button/合成/Text").GetComponent<Text>().text = "合成";
        炼化_当前模式 = "转换模式";
    }

    public void 初始化洗练信息()
    {
        GameObject 子界面 = GameObject.Find("子界面");
        Slider 攻击资质 = 子界面.transform.Find("洗练界面/属性/攻击/Slider").GetComponent<Slider>();
        Text 攻击资质文本 = 子界面.transform.Find("洗练界面/属性/攻击/资质").GetComponent<Text>();
        Text 攻击文本 = 子界面.transform.Find("洗练界面/属性/攻击/评价").GetComponent<Text>();
        Slider 防御资质 = 子界面.transform.Find("洗练界面/属性/防御/Slider").GetComponent<Slider>();
        Text 防御资质文本 = 子界面.transform.Find("洗练界面/属性/防御/资质").GetComponent<Text>();
        Text 防御文本 = 子界面.transform.Find("洗练界面/属性/防御/评价").GetComponent<Text>();
        Slider 血量资质 = 子界面.transform.Find("洗练界面/属性/血量/Slider").GetComponent<Slider>();
        Text 血量资质文本 = 子界面.transform.Find("洗练界面/属性/血量/资质").GetComponent<Text>();
        Text 血量文本 = 子界面.transform.Find("洗练界面/属性/血量/评价").GetComponent<Text>();
        Slider 回血资质 = 子界面.transform.Find("洗练界面/属性/回血/Slider").GetComponent<Slider>();
        Text 回血资质文本 = 子界面.transform.Find("洗练界面/属性/回血/资质").GetComponent<Text>();
        Text 回血文本 = 子界面.transform.Find("洗练界面/属性/回血/评价").GetComponent<Text>();
        Text 总评价文本 = 子界面.transform.Find("洗练界面/文本/总评价/Panel/评价").GetComponent<Text>();
        gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/攻击/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/开锁图标");
        gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/防御/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/开锁图标");
        gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/血量/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/开锁图标");
        gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/回血/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/开锁图标");
        攻击资质.value = 0;
        防御资质.value = 0;
        血量资质.value = 0;
        回血资质.value = 0;
        攻击文本.text = "废材";
        攻击文本.color = bm.转换颜色(0);
        防御文本.text = "废材";
        防御文本.color = bm.转换颜色(0);
        血量文本.text = "废材";
        血量文本.color = bm.转换颜色(0);
        回血文本.text = "废材";
        回血文本.color = bm.转换颜色(0);
        总评价文本.text = "废材";
        总评价文本.color = bm.转换颜色(0);
        攻击资质文本.text = "0 / 0";
        防御资质文本.text = "0 / 0";
        血量资质文本.text = "0 / 0";
        回血资质文本.text = "0 / 0";
        Image 图标 = 子界面.transform.Find("洗练界面/背景/图标").GetComponent<Image>();
        图标.sprite = Resources.Load<Sprite>("图片/古风背景002");
        Color nowColor;
        ColorUtility.TryParseHtmlString("#9C9C9C", out nowColor);
        图标.color = nowColor;
    }




    public void 刷新转换文本()
    {

        if (转换_材料.Equals("灰色精华"))
            点击转换_灰色精华();
        else if (转换_材料.Equals("绿色精华"))
            点击转换_绿色精华();
        else if (转换_材料.Equals("蓝色精华"))
            点击转换_蓝色精华();
        else if (转换_材料.Equals("紫色精华"))
            点击转换_紫色精华();
        else if (转换_材料.Equals("仙兽精华"))
            点击转换_仙兽精华();
        else if (转换_材料.Equals("神兽精华"))
            点击转换_神兽精华();
    }

    public void 刷新炼化文本()
    {
        int num = (int)数量拉条.value;
        string name = 子界面.transform.Find("炼化界面/界面/炼宠界面/左/Panel/Text").GetComponent<Text>().text;
        Text 获得文本 = 子界面.transform.Find("炼化界面/界面/炼宠界面/右/Panel/Text").GetComponent<Text>();
        if (PropMgr.材料表.ContainsKey(name))
        {
            Prop_bascis 物品 = pm.检索物品(name);
            获得文本.text = 品质与精华(bm.Xstoi(物品.qua)) + "X " + num;
        }
        else if (PropMgr.宠物表.ContainsKey(name))
        {
            Pet_Data 宠物 = pem.返回宠物(GameObject.FindGameObjectWithTag("选中").GetComponent<PetItem>().UID);
            if (宠物 != null)
                获得文本.text = 品质与精华(bm.Xstoi(宠物.qua)) + "X " + bm.Xstoi(宠物.cc);
        }

    }

    public void 点击转换_灰色精华()
    {
        Prop_bascis 物品 = pm.检索物品("灰色精华");
        int num = pm.返回背包该物品的数量("灰色精华");
        Text 拥有文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/已拥有/Panel/Text").GetComponent<Text>();
        Text 获得文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/可获得/Panel/Text").GetComponent<Text>();
        拥有文本.fontSize = 36;
        拥有文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
        int 拉条值 = num;
        if (转换_材料.Equals("灰色精华"))
        {
            拉条值 = (int)数量拉条.value;
        }
        转换_材料 = "灰色精华";

        int 获得数量 = num / 3;
        数量拉条.maxValue = 获得数量;
        if (拉条值 != num)
            获得数量 = 拉条值;
        拥有文本.text = "灰色精华  " + "(" +num + "/" + 获得数量 * 3 + ")";
        获得文本.text = "绿色精华X " + 获得数量;
        获得文本.color = bm.转换颜色(bm.Xstoi(物品.qua) + 1);
        数量拉条.value = 获得数量;
        输入文本.text = (int)数量拉条.value + "";

        Debug.Log("合成");

    }

    public void 点击转换_绿色精华()
    {
        Prop_bascis 物品 = pm.检索物品("绿色精华");
        int num = pm.返回背包该物品的数量("绿色精华");
        Text 拥有文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/已拥有/Panel/Text").GetComponent<Text>();
        Text 获得文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/可获得/Panel/Text").GetComponent<Text>();
        拥有文本.text = "绿色精华" + "X " + num;
        拥有文本.fontSize = 36;
        拥有文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
        int 拉条值 = num;
        if (转换_材料.Equals("绿色精华"))
        {
            拉条值 = (int)数量拉条.value;
        }
        转换_材料 = "绿色精华";
        int 获得数量;

        获得数量 = num / 3;
        数量拉条.maxValue = 获得数量;
        if (拉条值 != num)
            获得数量 = 拉条值;
        拥有文本.text = "绿色精华  " + "(" +num + "/" + 获得数量 * 3 + ")";
        获得文本.text = "蓝色精华X " + 获得数量;
        获得文本.color = bm.转换颜色(bm.Xstoi(物品.qua) + 1);

        数量拉条.value = 获得数量;
        输入文本.text = (int)数量拉条.value + "";

    }

    public void 点击转换_蓝色精华()
    {
        Prop_bascis 物品 = pm.检索物品("蓝色精华");
        int num = pm.返回背包该物品的数量("蓝色精华");
        Text 拥有文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/已拥有/Panel/Text").GetComponent<Text>();
        Text 获得文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/可获得/Panel/Text").GetComponent<Text>();
        拥有文本.text = "蓝色精华" + "X " + num;
        拥有文本.fontSize = 36;
        拥有文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
        int 拉条值 = num;
        if (转换_材料.Equals("蓝色精华"))
        {
            拉条值 = (int)数量拉条.value;
        }
        转换_材料 = "蓝色精华";
        int 获得数量;

        获得数量 = num / 3;
        数量拉条.maxValue = 获得数量;
        if (拉条值 != num)
            获得数量 = 拉条值;
        拥有文本.text = "蓝色精华  " + "(" + num + "/" + 获得数量 * 3 + ")";
        获得文本.text = "紫色精华X " + 获得数量;
        获得文本.color = bm.转换颜色(bm.Xstoi(物品.qua) + 1);

        数量拉条.value = 获得数量;
        输入文本.text = (int)数量拉条.value + "";

    }

    public void 点击转换_紫色精华()
    {
        Prop_bascis 物品 = pm.检索物品("紫色精华");
        int num = pm.返回背包该物品的数量("紫色精华");
        int 气息 = pm.返回背包该物品的数量("仙兽气息");
        Text 拥有文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/已拥有/Panel/Text").GetComponent<Text>();
        Text 获得文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/可获得/Panel/Text").GetComponent<Text>();
        拥有文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
        int 拉条值 = num;
        if (转换_材料.Equals("紫色精华"))
        {
            拉条值 = (int)数量拉条.value;
        }
        转换_材料 = "紫色精华";
        int 获得数量;

        拥有文本.fontSize = 36;
        获得数量 = num / 3 < 气息 ? num / 3 : 气息;
        数量拉条.maxValue = 获得数量;
        if (拉条值 != num)
            获得数量 = 拉条值;
        拥有文本.text = "<color=" + bm.返回颜色代码(3) + ">" + "紫色精华  " + "(" + num + "/" + 获得数量 * 3 + ")" + "</color>" + "\n" + "<color=" + bm.返回颜色代码(4) + ">" + " 仙兽气息  " + "(" + 获得数量 + "/" + 气息 + ")" + "</color>";
        获得文本.text = "仙兽精华X " + 获得数量;
        获得文本.color = bm.转换颜色(bm.Xstoi(物品.qua) + 1);

        数量拉条.value = 获得数量;
        输入文本.text = (int)数量拉条.value + "";

    }

    public void 点击转换_仙兽精华()
    {
        Prop_bascis 物品 = pm.检索物品("仙兽精华");
        int num = pm.返回背包该物品的数量("仙兽精华");
        int 气息 = pm.返回背包该物品的数量("神兽气息");
        Text 拥有文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/已拥有/Panel/Text").GetComponent<Text>();
        Text 需求文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/转换所需/Panel/Text").GetComponent<Text>();
        Text 获得文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/可获得/Panel/Text").GetComponent<Text>();
        拥有文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
        需求文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
        int 拉条值 = num;
        if (转换_材料.Equals("仙兽精华"))
        {
            拉条值 = (int)数量拉条.value;
        }
        转换_材料 = "仙兽精华";
        int 获得数量;

        拥有文本.fontSize = 36;
        获得数量 = num / 3 < 气息 ? num / 3 : 气息;
        数量拉条.maxValue = 获得数量;
        if (拉条值 != num)
            获得数量 = 拉条值;
        拥有文本.text = "<color=" + bm.返回颜色代码(4) + ">" + "仙兽精华  " + "(" +num + "/" + 获得数量 * 3 + ")" + "</color>" + "\n" + "<color=" + bm.返回颜色代码(5) + ">" + " 神兽气息  " + "(" + 气息 + "/" + 获得数量  + ")" + "</color>";
        获得文本.text = "神兽精华X " + 获得数量;
        获得文本.color = bm.转换颜色(bm.Xstoi(物品.qua) + 1);

        数量拉条.value = 获得数量;
        输入文本.text = (int)数量拉条.value + "";

    }

    public void 点击转换_神兽精华()
    {
        Prop_bascis 物品 = pm.检索物品("神兽精华");
        int num = pm.返回背包该物品的数量("神兽精华");
        Text 拥有文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/已拥有/Panel/Text").GetComponent<Text>();
        Text 获得文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/界面/转换界面/右/可获得/Panel/Text").GetComponent<Text>();
        拥有文本.text = "神兽精华" + "X " + num;
        拥有文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
        int 拉条值 = num;
        if (转换_材料.Equals("神兽精华"))
        {
            拉条值 = (int)数量拉条.value;
        }
        转换_材料 = "神兽精华";

        拥有文本.text = "神兽精华  " + "(" + num + "/" + 拉条值 * 3 + ")";
        数量拉条.maxValue = 0;
        数量拉条.value = 0;
        输入文本.text = "0";
        获得文本.text = "无法合成";

    }


    public string 品质与精华(int qua)
    {
        if (qua == 0)
            return "灰色精华";
        else if (qua == 1)
            return "绿色精华";
        else if (qua == 2)
            return "蓝色精华";
        else if (qua == 3)
            return "紫色精华";
        else if (qua == 4)
            return "仙兽精华";
        else if (qua == 5)
            return "神兽精华";
        else
            return "";
    }

    public void 炼化转换按钮()
    {
        if (炼化_当前模式.Equals("转换模式"))
        {
            if (转换_材料.Equals(""))
            {
                gut.生成警告框("未选中材料");
            }
            else if (转换_材料.Equals("灰色精华"))// 灰色精华
            {
                int num = (int)数量拉条.value;
                string 返回结果 = pm.失去物品("灰色精华", num * 3);
                if (返回结果.Equals("成功"))
                {
                    gut.生成警告框("合成成功");
                    pm.获取物品("绿色精华", num);
                }
            }
            else if (转换_材料.Equals("绿色精华"))//绿色精华
            {
                int num = (int)数量拉条.value;
                string 返回结果 = pm.失去物品("绿色精华", num * 3);
                if (返回结果.Equals("成功"))
                {
                    gut.生成警告框("合成成功");
                    pm.获取物品("蓝色精华", num);
                }
            }
            else if (转换_材料.Equals("蓝色精华"))//蓝色精华
            {
                int num = (int)数量拉条.value;
                string 返回结果 = pm.失去物品("蓝色精华", num * 3);
                if (返回结果.Equals("成功"))
                {
                    gut.生成警告框("合成成功");
                    pm.获取物品("紫色精华", num);
                }
            }
            else if (转换_材料.Equals("紫色精华"))//紫色精华
            {
                int num = (int)数量拉条.value;
                int 精华数量 = pm.返回背包该物品的数量("紫色精华");
                int 气息数量 = pm.返回背包该物品的数量("仙兽气息");
                if (精华数量 >= num * 3 && 气息数量 >= num)
                {
                    pm.失去物品("紫色精华", num * 3);
                    pm.失去物品("仙兽气息", num);
                    gut.生成警告框("合成成功");
                    pm.获取物品("仙兽精华", num);
                }
            }
            else if (转换_材料.Equals("仙兽精华"))//仙兽精华
            {
                int num = (int)数量拉条.value;
                int 精华数量 = pm.返回背包该物品的数量("仙兽精华");
                int 气息数量 = pm.返回背包该物品的数量("神兽气息");
                if (精华数量 >= num * 3 && 气息数量 >= num)
                {
                    pm.失去物品("仙兽精华", num * 3);
                    pm.失去物品("神兽气息", num);
                    gut.生成警告框("合成成功");
                    pm.获取物品("神兽精华", num);
                }
            }
            else if (转换_材料.Equals("神兽精华"))// 神兽精华
            {
                gut.生成警告框("无法继续合成");
            }
            else
            {
                gut.生成警告框("材料出错");
            }
            刷新转换文本();

        }
        else if (炼化_当前模式.Equals("炼化模式"))
        {
            int num = (int)数量拉条.value;
            string name = 子界面.transform.Find("炼化界面/界面/炼宠界面/左/Panel/Text").GetComponent<Text>().text;
            Text 获得文本 = 子界面.transform.Find("炼化界面/界面/炼宠界面/右/Panel/Text").GetComponent<Text>();
            if (PropMgr.材料表.ContainsKey(name))
            {
                Prop_bascis 物品 = pm.检索物品(name);
                if (pm.失去物品(name, num).Equals("成功"))
                {
                    gut.生成警告框("炼化成功");
                    pm.获取物品(品质与精华(bm.Xstoi(物品.qua)), num);
                }
                else
                {
                    gut.生成警告框("炼化出错");
                }
            }
            else if (PropMgr.宠物表.ContainsKey(name))
            {
                PetItem pi = GameObject.FindGameObjectWithTag("选中").GetComponent<PetItem>();
                Pet_Data 宠物 = pem.返回宠物(pi.UID);
                if (pem.返回宠物(宠物.id).islock.Equals("1"))
                {
                    gut.生成警告框("该宠物被锁定");
                    return;
                }
                else
                {
                    if (数量拉条.value == 0)
                    {
                        gut.生成警告框("0个无法炼化");
                        return;
                    }
                    if (pem.删除宠物(宠物.id))
                    {
                        gut.生成警告框("炼化成功");
                        pm.获取物品(品质与精华(bm.Xstoi(宠物.qua)), bm.Xstoi(宠物.cc));
                        初始化宠物栏();
                    }
                    else
                    {
                        gut.生成警告框("炼化出错");
                    }
                }
            }


            刷新炼化文本();
        }
    }


    private void 刷新宠物战斗数据()
    {
        GameObject 宠物 = GameObject.FindGameObjectWithTag("宠物");
        if (宠物 != null)
        {
            combat cb = 宠物.GetComponent<combat>();
            cb.宠物初始化();
        }

    }

    public void 升级()
    {
        role_Data myData = io_.load();
        GameObject 选中 = GameObject.FindGameObjectWithTag("选中");
        if (选中 != null)
        {
            PetItem pi = 选中.GetComponent<PetItem>();
            Pet_Data 宠物 = pem.返回宠物(pi.UID);
            if (!myData.记录.ContainsKey("经验池"))
            {
                myData.记录.Add("经验池", "0");
            }
            long 经验池 = long.Parse(myData.记录["经验池"]);
            long 所需经验 = (rm.经验表(bm.Xstoi(宠物.grade)) / 23) + 33 * bm.Xstoi(宠物.grade);
            if (经验池 > 所需经验)
            {
                宠物 = pem.宠物升级(宠物);
                myData = pem.存档宠物属性覆盖(myData, 宠物);
                所需经验 = (rm.经验表(bm.Xstoi(宠物.grade)) / 23) + 33 * bm.Xstoi(宠物.grade);
                myData.记录["经验池"] = long.Parse(myData.记录["经验池"]) - 所需经验 + "";
                io_.save(myData);
                pi.显示宠物信息();
                刷新宠物战斗数据();
            }
            else
                gut.生成警告框("经验值不足");
        }
        else
        {
            gut.生成警告框("请选择宠物");
        }
    }





    public void 提升成长()
    {
        role_Data myData = io_.load();
        GameObject 选中 = GameObject.FindGameObjectWithTag("选中");
        if (选中 != null)
        {
            PetItem pi = 选中.GetComponent<PetItem>();
            Pet_Data 宠物 = pem.返回宠物(pi.UID);
            if (bm.Xstoi(宠物.qua) == 4)
                宠物.max_cc = myData.灵根;
            else if (bm.Xstoi(宠物.qua) == 5)
                宠物.max_cc = bm.Xor("99999");
            if (bm.Xstoi(宠物.cc) < bm.Xstoi(宠物.max_cc))
            {
                string 需求材料 = "";
                if (bm.Xstoi(宠物.qua) == 0)
                    需求材料 = "灰色精华";
                else if (bm.Xstoi(宠物.qua) == 1)
                    需求材料 = "绿色精华";
                else if (bm.Xstoi(宠物.qua) == 2)
                    需求材料 = "蓝色精华";
                else if (bm.Xstoi(宠物.qua) == 3)
                    需求材料 = "紫色精华";
                else if (bm.Xstoi(宠物.qua) == 4)
                    需求材料 = "仙兽精华";
                else if (bm.Xstoi(宠物.qua) == 5)
                    需求材料 = "神兽精华";
                int 需求数量 = bm.Xstoi(宠物.cc) / 10 + 1;
                int 拥有数量;
                if (myData.材料背包.ContainsKey(需求材料))
                    拥有数量 = bm.Xstoi(myData.材料背包[需求材料].num);
                else
                    拥有数量 = 0;
                if (拥有数量 >= 需求数量)
                {
                    宠物 = pem.宠物成长提升(宠物);
                    myData = pem.存档宠物属性覆盖(myData, 宠物);
                    io_.save(myData);
                    pm.失去物品(需求材料, 需求数量);
                    pi.显示宠物信息();
                    刷新宠物战斗数据();
                }
                else
                    gut.生成警告框("材料不足");
            }
            else
            {
                gut.生成警告框("成长达上限");
            }
        }
        else
        {
            gut.生成警告框("请选择宠物");
        }

    }




    public void 出战()
    {
        role_Data myData = io_.load();
        GameObject 选中 = GameObject.FindGameObjectWithTag("选中");
        if (选中 != null)
        {
            PetItem pi = 选中.GetComponent<PetItem>();
            if (myData.出战宠物UID != null && myData.出战宠物UID.Equals(pi.UID))//取消出战
            {
                GameObject 宠物 = GameObject.Find("combat(Clone)/role/r5").transform.Find("宠物").gameObject;
                combat cb = 宠物.GetComponent<combat>();
                myData.出战宠物UID = null;
                pi.宠物.state = cb.剩余血量;
                io_.save(myData);
                if (!pi.宠物.state.Equals("") && bm.Xstoi(pi.宠物.state) <= 0)
                {
                    GameObject 死亡 = GameObject.Find("死亡(Clone)");
                    if (死亡 != null)
                        Destroy(死亡);
                }
                else
                {
                    Destroy(宠物);
                }
                Text 出战文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/出战/Text").GetComponent<Text>();
                出战文本.text = "出战";
                Color nowColor;
                ColorUtility.TryParseHtmlString("#94DDEC", out nowColor);
                出战文本.transform.parent.GetComponent<Image>().color = nowColor;
            }
            else
            {//出战宠物
                if (sa.CD集合.ContainsKey("宠物复活"))
                {
                    gut.生成警告框("复活中! 剩余" + (int)sa.CD集合["宠物复活"] + "秒");
                }
                else
                {
                    if (!pi.宠物.state.Equals("") && bm.Xstoi(pi.宠物.state) <= 0)
                        pi.宠物.state = "";
                    myData.出战宠物UID = pi.UID;
                    io_.save(myData);
                    gut.加载宠物();
                    Text 出战文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/按钮界面/操作/操作界面/出战/Text").GetComponent<Text>();
                    出战文本.text = "出战中...";
                    Color nowColor;
                    ColorUtility.TryParseHtmlString("#ECA594", out nowColor);
                    出战文本.transform.parent.GetComponent<Image>().color = nowColor;
                }
            }
        }
        else
        {
            gut.生成警告框("请选择宠物");
        }
    }

    public void 敬请期待()
    {
        gut.生成警告框("敬请期待");
    }

    public void 改变Slider()
    {
        if (数量拉条 != null && 输入文本 != null)
        {
            int value = (int)数量拉条.value;
            输入文本.text = value + "";
        }
    }


    public void 改变输入文本()
    {
        if (数量拉条 != null && 输入文本 != null)
        {
            int value = int.Parse(输入文本.text);
            if (value > 数量拉条.maxValue)
                value = (int)数量拉条.maxValue;
            数量拉条.value = value;
            if (炼化_当前模式.Equals("转换模式"))
                刷新转换文本();
            else if (炼化_当前模式.Equals("炼化模式"))
                刷新炼化文本();
        }
    }

    public void 点击洗练锁_攻击()
    {
        role_Data myData = io_.load();
        GameObject 选中 = GameObject.FindGameObjectWithTag("选中");
        if (选中 != null)
        {
            PetItem pi = 选中.GetComponent<PetItem>();
            Pet_Data 宠物 = pem.返回宠物(pi.UID);
            if (宠物.锁.Contains("攻击"))
            {
                宠物.锁.Remove("攻击");
                gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/攻击/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/开锁图标");
            }
            else
            {
                宠物.锁.Add("攻击");
                gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/攻击/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
            }
            myData = pem.存档宠物属性覆盖(myData, 宠物);
            io_.save(myData);
        }
    }

    public void 点击洗练锁_防御()
    {
        role_Data myData = io_.load();
        GameObject 选中 = GameObject.FindGameObjectWithTag("选中");
        if (选中 != null)
        {
            PetItem pi = 选中.GetComponent<PetItem>();
            Pet_Data 宠物 = pem.返回宠物(pi.UID);
            if (宠物.锁.Contains("防御"))
            {
                宠物.锁.Remove("防御");
                gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/防御/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/开锁图标");
            }
            else
            {
                宠物.锁.Add("防御");
                gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/防御/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
            }
            myData = pem.存档宠物属性覆盖(myData, 宠物);
            io_.save(myData);
        }
    }

    public void 点击洗练锁_血量()
    {
        role_Data myData = io_.load();
        GameObject 选中 = GameObject.FindGameObjectWithTag("选中");
        if (选中 != null)
        {
            PetItem pi = 选中.GetComponent<PetItem>();
            Pet_Data 宠物 = pem.返回宠物(pi.UID);
            if (宠物.锁.Contains("血量"))
            {
                宠物.锁.Remove("血量");
                gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/血量/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/开锁图标");
            }
            else
            {
                宠物.锁.Add("血量");
                gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/血量/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
            }
            myData = pem.存档宠物属性覆盖(myData, 宠物);
            io_.save(myData);
        }
    }

    public void 点击洗练锁_回血()
    {
        role_Data myData = io_.load();
        GameObject 选中 = GameObject.FindGameObjectWithTag("选中");
        if (选中 != null)
        {
            PetItem pi = 选中.GetComponent<PetItem>();
            Pet_Data 宠物 = pem.返回宠物(pi.UID);
            if (宠物.锁.Contains("回血"))
            {
                宠物.锁.Remove("回血");
                gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/回血/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/开锁图标");
            }
            else
            {
                宠物.锁.Add("回血");
                gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/属性/回血/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
            }
            myData = pem.存档宠物属性覆盖(myData, 宠物);
            io_.save(myData);
        }
    }

    public void 宠物洗练()
    {
        role_Data myData = io_.load();
        GameObject 选中 = GameObject.FindGameObjectWithTag("选中");
        if (选中 != null)
        {
            PetItem pi = 选中.GetComponent<PetItem>();
            Pet_Data 宠物 = pem.返回宠物(pi.UID);
            int num = 0;
            if (宠物.锁.Count == 0)
                num = 1;
            else if (宠物.锁.Count == 1)
                num = 2;
            else if (宠物.锁.Count == 2)
                num = 4;
            else if (宠物.锁.Count == 3)
                num = 8;
            else if (宠物.锁.Count == 4)
            {
                gut.生成警告框("资质无法全部锁定!");
                return;
            }
            string 返回结果 = pm.失去物品("归元露", num);
            myData = io_.load();
            if (!返回结果.Equals("成功"))
            {
                gut.生成警告框("归元露不足");
                return;
            }
            宠物 = pem.宠物洗练(宠物);
            myData = pem.存档宠物属性覆盖(myData, 宠物);
            io_.save(myData);
            pi.显示洗练信息();
        }
    }




}
