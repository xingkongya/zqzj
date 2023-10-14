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
    public Pet_Data 宠物;
    public long 经验池;
    public long 所需经验;

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
       // GameObject 升级按钮 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/按钮界面/操作/操作界面/升级背景/升级按钮").gameObject;
       // GameObject 提升成长按钮 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/按钮界面/操作/操作界面/成长背景/提升成长按钮").gameObject;
        GameObject 出战按钮 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面").transform.Find("按钮界面/操作/操作界面/出战").gameObject;
        //数量拉条 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/炼化界面/button/Slider").GetComponent<Slider>();
        //输入文本 = 数量拉条.transform.Find("InputField").GetComponent<InputField>();
        //bm.Banding(升级按钮, 升级);
       // bm.Banding(提升成长按钮, 提升成长);
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
        初始化宠物界面();
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

        Sprite 初始宠物 = Resources.Load<Sprite>("怪物/水墨龙");
        Image 图标 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/背景/图标").GetComponent<Image>();
        //Image 边框= gameObject.transform.Find("背景/边框").GetComponent<Image>();
        图标.sprite = 初始宠物;
        //边框.sprite = 初始宠物;
        图标.color = bm.改变透明度(图标.gameObject, 0f);


        //信息填充
        Text 当前宠物 = gameObject.transform.Find("背景/菜单栏/宠物菜单/宠物栏/标题/当前宠物信息/名字").GetComponent<Text>();
        当前宠物.text = "";
        Text 名字 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/名字/Text").GetComponent<Text>();
        名字.text = "";
        Text 评价 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/背景/图标/评价").GetComponent<Text>();
        评价.text = "";
        Text 战斗力 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/战斗力/Text").GetComponent<Text>();
        战斗力.text = "";
        Text 成长 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/成长/Text").GetComponent<Text>();
        成长.text = "";
        Text 成长上限 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/成长上限/Text").GetComponent<Text>();
        成长上限.text = "";
        Text 等级 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/等级/Text").GetComponent<Text>();
        等级.text = "";
        Text xing = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/星级/Text").GetComponent<Text>();
        xing.text = "";
        Text 攻击力 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/攻击力/Text").GetComponent<Text>();
        攻击力.text = "";
        Text 防御力 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/防御力/Text").GetComponent<Text>();
        防御力.text = "";
        Text 血量 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/血量/Text").GetComponent<Text>();
        血量.text = "";
        Text 回血值 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/回血值/Text").GetComponent<Text>();
        回血值.text = "";
        Text 攻速 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/攻击速度/Text").GetComponent<Text>();
        攻速.text = "";
        Text 暴击 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/属性界面/Panel/属性信息/暴击率/Text").GetComponent<Text>();
        暴击.text = "";


        Text 出战文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面").transform.Find("按钮界面/操作/操作界面/出战/Text").GetComponent<Text>();
        出战文本.text = "出战";
        Color nowColor;
        ColorUtility.TryParseHtmlString("#94DDEC", out nowColor);
        出战文本.transform.parent.GetComponent<Image>().color = nowColor;


    }


    public void 刷新宠物属性信息() {
        //信息填充
        Text 当前宠物 = 子界面.transform.parent.Find("宠物栏/标题/当前宠物信息/名字").GetComponent<Text>();
        当前宠物.text = 宠物.name;
        当前宠物.color = bm.转换颜色(bm.Xstoi(宠物.qua));
        Text 名字 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/名字/Text").GetComponent<Text>();
        名字.text = 宠物.name;
        名字.color = bm.转换颜色(bm.Xstoi(宠物.qua));
        Text 评价 = 子界面.transform.Find("初始界面/属性界面/背景/图标/评价").GetComponent<Text>();
        显示总评价(评价, 宠物);
        Text 成长 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/成长/Text").GetComponent<Text>();
        成长.text = bm.Xor(宠物.cc);
        Text 成长上限 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/成长上限/Text").GetComponent<Text>();
        if (bm.Xstoi(宠物.qua) == 4)
            成长上限.text = bm.Xstoi(宠物.cc) * 2 + "";
        else if (bm.Xstoi(宠物.qua) == 5)
            成长上限.text = "无限";
        else
            成长上限.text = bm.Xor(宠物.max_cc);

        Text 等级 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/等级/Text").GetComponent<Text>();
        等级.text = bm.Xor(宠物.grade);
        Text xing = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/星级/Text").GetComponent<Text>();
        xing.text = bm.Xor(宠物.xing);
        Text 攻击力 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/攻击力/Text").GetComponent<Text>();
        int num_攻击力 = (int)(bm.Xstoi(宠物.ini_atk) + (bm.Xstoi(宠物.grade) - 1) * (bm.Xstoi(宠物.ram_atk) / 1000f) * 3 * bm.Xstoi(宠物.cc));
        攻击力.text = num_攻击力 + "";
        Text 防御力 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/防御力/Text").GetComponent<Text>();
        int num_防御力 = (int)(bm.Xstoi(宠物.ini_def) + (bm.Xstoi(宠物.grade) - 1) * (bm.Xstoi(宠物.ram_def) / 1000f) * 1 * bm.Xstoi(宠物.cc));
        防御力.text = num_防御力 + "";
        Text 血量 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/血量/Text").GetComponent<Text>();
        int num_血量 = (int)(bm.Xstoi(宠物.ini_hp) + (bm.Xstoi(宠物.grade) - 1) * (bm.Xstoi(宠物.ram_hp) / 1000f) * 50 * bm.Xstoi(宠物.cc));
        血量.text = num_血量 + "";
        Text 回血值 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/回血值/Text").GetComponent<Text>();
        int num_回血值 = (int)(bm.Xstoi(宠物.ini_hpr) + (bm.Xstoi(宠物.grade) - 1) * (bm.Xstoi(宠物.ram_hpr) / 1000f) * 5 * bm.Xstoi(宠物.cc));
        回血值.text = num_回血值 + "";
        Text 攻速 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/攻击速度/Text").GetComponent<Text>();
        float num_攻速 = bm.Xstoi(宠物.ini_aspd) / 10f;
        攻速.text = num_攻速 + "s";
        Text 暴击 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/暴击率/Text").GetComponent<Text>();
        float num_暴击 = (int)(bm.Xstoi(宠物.ini_cri) + bm.Xstoi(宠物.qua_cri) / 2000f * (bm.Xstoi(宠物.grade) - 1));
        暴击.text = num_暴击 + "%";
        Text 战斗力 = 子界面.transform.Find("初始界面/属性界面/Panel/属性信息/战斗力/Text").GetComponent<Text>();
        战斗力.text = PetMgr.GetInstance().返回宠物战斗力(num_攻击力, num_防御力, num_血量, num_回血值, num_攻速, num_暴击);


    }


    public void 显示总评价(Text 文本, Pet_Data 宠物)
    {
        float MaxValue = bm.Xstoi(宠物.qua_atk) + bm.Xstoi(宠物.qua_def) + bm.Xstoi(宠物.qua_hp) + bm.Xstoi(宠物.qua_hpr);
        float val = bm.Xstoi(宠物.ram_atk) + bm.Xstoi(宠物.ram_def) + bm.Xstoi(宠物.ram_hp) + bm.Xstoi(宠物.ram_hpr);
        if (val / MaxValue >= 0.1f && val / MaxValue <= 0.25f)
        {
            文本.text = "废材";
            文本.color = bm.转换颜色(0);
        }
        else if (val / MaxValue > 0.25f && val / MaxValue <= 0.4f)
        {
            文本.text = "平庸";
            文本.color = bm.转换颜色(0);
        }
        else if (val / MaxValue > 0.4f && val / MaxValue <= 0.6f)
        {
            文本.text = "普通";
            文本.color = bm.转换颜色(1);
        }
        else if (val / MaxValue > 0.6f && val / MaxValue <= 0.75f)
        {
            文本.text = "良好";
            文本.color = bm.转换颜色(2);
        }
        else if (val / MaxValue > 0.75f && val / MaxValue <= 0.85f)
        {
            文本.text = "优秀";
            文本.color = bm.转换颜色(3);
        }
        else if (val / MaxValue > 0.85f && val / MaxValue <= 0.95f)
        {
            文本.text = "极品";
            文本.color = bm.转换颜色(4);
        }
        else if (val / MaxValue > 0.95f)
        {
            文本.text = "完美";
            文本.color = bm.转换颜色(5);
        }

        //Debug.Log(宠物.name+"MAX:"+ MaxValue+"---"+"VAL"+val);

    }


    public void 初始化宠物界面()
    {
        gut.操作子物体(子界面, 1);
        子界面.transform.Find("初始界面").gameObject.SetActive(true);
        子界面.transform.Find("初始界面/初始提示").gameObject.SetActive(true);
        子界面.transform.Find("初始界面/按钮界面").gameObject.SetActive(false);
        初始化宠物信息();
    }

    public void 点击炼化界面()
    {
        子界面.transform.Find("炼化界面").gameObject.SetActive(true);
    }

    public void 点击进化界面()
    {
        子界面.transform.Find("进化界面").gameObject.SetActive(true);
        刷新进化信息();
    }

    public void 点击洗练界面()
    {
        子界面.transform.Find("洗练界面").gameObject.SetActive(true);
        初始化洗练信息();
        显示洗练信息();
    }

    public void 点击升级界面()
    {
        子界面.transform.Find("升级界面").gameObject.SetActive(true);
        显示升级信息();
    }



    // Update is called once per frame
    void Update()
    {

    }

    

    private void 初始化洗练信息()
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

    public void 显示升级信息() {
        if (!myData.记录.ContainsKey("经验池"))
            myData.记录.Add("经验池", "0");

        经验池 = long.Parse(myData.记录["经验池"]);
        所需经验 = (rm.经验表(bm.Xstoi(宠物.grade)) / 23) + 33 * bm.Xstoi(宠物.grade);
        Text 经验池文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/升级背景/经验池").GetComponent<Text>();
        Text 所需经验文本文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/升级背景/升级所需经验").GetComponent<Text>();
        Text 精华数量文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/成长背景/拥有数量/num").GetComponent<Text>();
        Text 提升所需文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/成长背景/提升所需/num").GetComponent<Text>();
        Text 需求精华名字文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/成长背景/拥有数量/name").GetComponent<Text>();
        Text 拥有精华名字文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/成长背景/提升所需/name").GetComponent<Text>();
        Text 当前等级文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/Panel/当前等级").GetComponent<Text>();
        Text 当前成长文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/Panel/当前成长").GetComponent<Text>();
        需求精华名字文本.text = "";
        拥有精华名字文本.text = "";
        精华数量文本.text = "";
        提升所需文本.text = "";
        经验池文本.text = "拥有经验:    ";
        所需经验文本文本.text = "所需经验:    ";
        经验池文本.text = "拥有经验:    " + 经验池 + "";
        所需经验文本文本.text = "所需经验:    " + 所需经验 + "";
        当前等级文本.text = "当前等级:" + "<color=red>" + bm.Xstoi(宠物.grade) + "</color>";
        当前成长文本.text = "当前成长:" + "<color=red>" + bm.Xstoi(宠物.cc) + "</color>";

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

        需求精华名字文本.text = 需求材料;
        拥有精华名字文本.text = 需求材料;
        需求精华名字文本.color = bm.转换颜色(bm.Xstoi(宠物.qua));
        拥有精华名字文本.color = bm.转换颜色(bm.Xstoi(宠物.qua));
        精华数量文本.text = "X" + 拥有数量;
        提升所需文本.text = "X" + 需求数量;

        Sprite 宠物图片 = Resources.Load<Sprite>("怪物/" + 宠物.icon);
        if (宠物图片 == null)
        {
            宠物图片 = Resources.Load<Sprite>("怪物/黑白鸟");
        }
        Image 图标 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/背景/图标").GetComponent<Image>();
        //Image 边框 = gameObject.transform.parent.parent.parent.parent.parent.Find("边框").GetComponent<Image>();
        图标.sprite = 宠物图片;
        Color nowColor;
        ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
        图标.color = nowColor;
    }


    public void 显示洗练信息()
    {
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

        if (宠物.锁.Contains("攻击"))
        {
            子界面.transform.Find("洗练界面/属性/攻击/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
        }
        if (宠物.锁.Contains("防御"))
        {
            子界面.transform.Find("洗练界面/属性/防御/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
        }
        if (宠物.锁.Contains("血量"))
        {
            子界面.transform.Find("洗练界面/属性/血量/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
        }
        if (宠物.锁.Contains("回血"))
        {
            子界面.transform.Find("洗练界面/属性/回血/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
        }

        Sprite 宠物图片 = Resources.Load<Sprite>("怪物/" + 宠物.icon);
        if (宠物图片 == null)
        {
            宠物图片 = Resources.Load<Sprite>("怪物/黑白鸟");
        }
        Image 图标 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/背景/图标").GetComponent<Image>();
        //Image 边框 = gameObject.transform.parent.parent.parent.parent.parent.Find("边框").GetComponent<Image>();
        图标.sprite = 宠物图片;
        Color nowColor;
        ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
        图标.color = nowColor;


        //攻击
        攻击资质.maxValue = bm.Xstoi(宠物.qua_atk);
        攻击资质.value = bm.Xstoi(宠物.ram_atk);
        攻击资质文本.text = bm.Xstoi(宠物.qua_atk) + " / " + bm.Xstoi(宠物.ram_atk);
        生成评价(攻击文本, 攻击资质.maxValue, 攻击资质.value);
        //防御
        防御资质.maxValue = bm.Xstoi(宠物.qua_def);
        防御资质.value = bm.Xstoi(宠物.ram_def);
        防御资质文本.text = bm.Xstoi(宠物.qua_def) + " / " + bm.Xstoi(宠物.ram_def);
        生成评价(防御文本, 防御资质.maxValue, 防御资质.value);
        //血量
        血量资质.maxValue = bm.Xstoi(宠物.qua_hp);
        血量资质.value = bm.Xstoi(宠物.ram_hp);
        血量资质文本.text = bm.Xstoi(宠物.qua_hp) + " / " + bm.Xstoi(宠物.ram_hp);
        生成评价(血量文本, 血量资质.maxValue, 血量资质.value);
        //回血
        回血资质.maxValue = bm.Xstoi(宠物.qua_hpr);
        回血资质.value = bm.Xstoi(宠物.ram_hpr);
        回血资质文本.text = bm.Xstoi(宠物.qua_hpr) + " / " + bm.Xstoi(宠物.ram_hpr);
        生成评价(回血文本, 回血资质.maxValue, 回血资质.value);

        //总评价
        float all_max = 攻击资质.maxValue + 防御资质.maxValue + 血量资质.maxValue + 回血资质.maxValue;
        float all_val = 攻击资质.value + 防御资质.value + 血量资质.value + 回血资质.value;
        生成评价(总评价文本, all_max, all_val);
        刷新洗练消耗文本();
    }


    public void 生成评价(Text 文本, float MaxValue, float value)
    {
        if (value / MaxValue >= 0.1f && value / MaxValue <= 0.25f)
        {
            文本.text = "废材";
            文本.color = bm.转换颜色(0);
        }
        else if (value / MaxValue > 0.25f && value / MaxValue <= 0.4f)
        {
            文本.text = "平庸";
            文本.color = bm.转换颜色(0);
        }
        else if (value / MaxValue > 0.4f && value / MaxValue <= 0.6f)
        {
            文本.text = "普通";
            文本.color = bm.转换颜色(1);
        }
        else if (value / MaxValue > 0.6f && value / MaxValue <= 0.75f)
        {
            文本.text = "良好";
            文本.color = bm.转换颜色(2);
        }
        else if (value / MaxValue > 0.75f && value / MaxValue <= 0.85f)
        {
            文本.text = "优秀";
            文本.color = bm.转换颜色(3);
        }
        else if (value / MaxValue > 0.85f && value / MaxValue <= 0.95f)
        {
            文本.text = "极品";
            文本.color = bm.转换颜色(4);
        }
        else if (value / MaxValue > 0.95f)
        {
            文本.text = "完美";
            文本.color = bm.转换颜色(5);
        }
    }




    public void 刷新进化信息() {
        Sprite 宠物图片 = Resources.Load<Sprite>("怪物/" + 宠物.icon);
        Text 成长文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/进化界面/进化需求/成长").GetComponent<Text>();
        Text 额外文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/进化界面/进化需求/钱币").GetComponent<Text>();
        Text 材料文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/进化界面/进化需求/材料").GetComponent<Text>();
        Text 当前名字文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/进化界面/文本/当前名字").GetComponent<Text>();
        Text 进化后名字文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/进化界面/文本/进化后名字").GetComponent<Text>();


        当前名字文本.text = 宠物.name;
        当前名字文本.color = bm.转换颜色(bm.Xstoi(宠物.qua));
        if (宠物图片 == null)
        {
            宠物图片 = Resources.Load<Sprite>("怪物/水墨龙");
        }
        Image 当前图标 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/进化界面/当前宠物/图标").GetComponent<Image>();
        当前图标.sprite = 宠物图片;
        Color nowColor;
        ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
        当前图标.color = nowColor;

        if (PetMgr.GetInstance().宠物进化表.ContainsKey(宠物.name))
        {
            Dictionary<string, string> 当前宠物进化信息 = PetMgr.GetInstance().宠物进化表[宠物.name];
            进化后名字文本.text = 当前宠物进化信息["进化获得"];
            进化后名字文本.color = bm.转换颜色(bm.Xstoi(PropMgr.宠物表[当前宠物进化信息["进化获得"]].qua));
            Sprite 获得图片 = Resources.Load<Sprite>("怪物/" + 去除王者符号(当前宠物进化信息["进化获得"]));


            if (获得图片 == null)
            {
                获得图片 = Resources.Load<Sprite>("怪物/水墨龙");
            }
            Image 获得图标 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/进化界面/进化后/图标").GetComponent<Image>();
            获得图标.sprite = 获得图片;
            获得图标.color = nowColor;

            if (int.Parse(当前宠物进化信息["成长需求"]) <= bm.Xstoi(宠物.cc))
            {
                成长文本.text = "<color=green>" + 当前宠物进化信息["成长需求"] + "成长 ✔" + "</color>";
            }
            else
            {
                成长文本.text = "<color=red>" + 当前宠物进化信息["成长需求"] + "成长 ✗" + "</color>";
            }

            string 额外需求 = 当前宠物进化信息["额外需求"];
            //额外需求是钱
            if (myData.金钱.ContainsKey(额外需求))
            {
                if (int.Parse(当前宠物进化信息["额外数量"]) <= int.Parse(pm.返回金钱数量(额外需求)))
                {
                    额外文本.text = "<color=green>" + 当前宠物进化信息["额外数量"] + 当前宠物进化信息["额外需求"] + " ✔" + "</color>";
                }
                else
                {
                    额外文本.text = "<color=red>" + 当前宠物进化信息["额外数量"] + 当前宠物进化信息["额外需求"] + " ✗" + "</color>";
                }
            }
            //额外需求是物品
            else
            {
                if (int.Parse(当前宠物进化信息["额外数量"]) <= pm.返回背包该物品的数量(额外需求))
                {
                    额外文本.text = "<color=green>" + 当前宠物进化信息["额外数量"] + 当前宠物进化信息["额外需求"] + " ✔" + "</color>";
                }
                else
                {
                    额外文本.text = "<color=red>" + 当前宠物进化信息["额外数量"] + 当前宠物进化信息["额外需求"] + " ✗" + "</color>";
                }
            }

            if (pm.返回背包该物品的数量(当前宠物进化信息["进化需求"]) >= 1)
            {
                材料文本.text = "<color=green>" + 当前宠物进化信息["进化需求"] + " ✔" + "</color>";
            }
            else
            {
                材料文本.text = "<color=red>" + 当前宠物进化信息["进化需求"] + " ✗" + "</color>";
            }
        }
        else {
            成长文本.text = "";
            额外文本.text = "<color=red>无法进化</color>";
            材料文本.text = "";
            Sprite 获得图片 = Resources.Load<Sprite>("图标/灰色背景图标");
            Image 获得图标 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/进化界面/进化后/图标").GetComponent<Image>();
            获得图标.sprite = 获得图片;
            获得图标.color = nowColor;
        }
    }


    public string 去除王者符号(string name) {
        if (name.IndexOf("≮") != -1) {
            name.Replace("≮", "");
        }
        if (name.IndexOf("≯") != -1) {
            name.Replace("≯", "");
        }

        return name;
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
        if (宠物 != null)
        {
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
                刷新宠物战斗数据();
                刷新宠物属性信息();
                显示升级信息();
                GameObject 图标 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/背景/图标").gameObject;
                gut.生成升级特效(图标,"2");
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
        if (宠物 != null)
        {
            if (bm.Xstoi(宠物.qua) == 4)
                宠物.max_cc = myData.灵根;
            else if (bm.Xstoi(宠物.qua) == 5)
                宠物.max_cc = bm.Xor("999999");
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
                    刷新宠物战斗数据();
                    刷新宠物属性信息();
                    显示升级信息();
                    GameObject 图标 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/升级界面/背景/图标").gameObject;
                    gut.生成升级特效(图标,"2");
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
        GameObject 出战显示 = 选中.transform.Find("出战显示").gameObject;
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
                Text 出战文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/按钮界面/操作/操作界面/出战/Text").GetComponent<Text>();
                出战文本.text = "出战";
                出战显示.SetActive(false);
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
                    Text 出战文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/初始界面/按钮界面/操作/操作界面/出战/Text").GetComponent<Text>();
                    出战文本.text = "出战中...";
                    出战显示.SetActive(true);
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



    public void 点击洗练锁_攻击()
    {  
        if (宠物 != null)
        {
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
        刷新洗练消耗文本();
    }

    public void 点击洗练锁_防御()
    {
        if (宠物 != null)
        {
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
        刷新洗练消耗文本();
    }

    public void 点击洗练锁_血量()
    {
        if (宠物 != null)
        {
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
        刷新洗练消耗文本();
    }

    public void 点击洗练锁_回血()
    {
        if (宠物 != null)
        {
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
        刷新洗练消耗文本();
    }


    public void 刷新洗练消耗文本() {
        Text 消耗文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/文本/消耗").GetComponent<Text>();
        Text 拥有文本 = gameObject.transform.Find("背景/菜单栏/宠物菜单/子界面/洗练界面/文本/拥有").GetComponent<Text>();
        int num ;
        if (宠物.锁.Count == 0)
            num = 1;
        else if (宠物.锁.Count == 1)
            num = 2;
        else if (宠物.锁.Count == 2)
            num = 4;
        else
            num = 8;
        消耗文本.text = "本次消耗"+num+"归元露";
        int 拥有 = pm.返回背包该物品的数量("归元露");
        拥有文本.text = "归元露:(" + num + "/" +拥有 + ")";
        if (num < 拥有)
            拥有文本.text += "<color=green>" + " ✔" + "</color>";
        else
            拥有文本.text += "<color=red>" + " ✗" + "</color>";
    }

    public void 宠物洗练()
    {
        if (宠物 != null)
        {
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
            显示洗练信息();
        }
    }




}
