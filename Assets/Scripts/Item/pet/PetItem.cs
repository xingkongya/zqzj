using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetItem : MonoBehaviour
{
    private basicMgr bm;
    public Pet_Data 宠物;
    private io io_;
    private PetMgr pem;
    private G_Util gut;
    private RoleMgr rm;
    public long 经验池;
    public long 所需经验;
    public string UID;
    public StateStar ss;
    public Dictionary<int, string> 品质与精华 = new Dictionary<int, string>();

    private void Awake()
    {
        pem = PetMgr.GetInstance();
        bm = basicMgr.GetInstance();
        io_ = io.GetInstance();
        rm = RoleMgr.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ss = NameMgr.画布.transform.Find("UI").transform.Find("状态页").GetComponent<StateStar>();
        品质与精华.Add(0, "灰色精华");
        品质与精华.Add(1, "绿色精华");
        品质与精华.Add(2, "蓝色精华");
        品质与精华.Add(3, "紫色精华");
        品质与精华.Add(4, "仙兽精华");
        品质与精华.Add(5, "神兽精华");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void 点击宠物项() {
        显示宠物信息();
        显示炼化信息();
        显示洗练信息();

    }




    public void 显示宠物信息() {
        if (ss.子界面.transform.Find("初始界面").gameObject.activeSelf)
        {
            ss.子界面.transform.Find("初始界面").gameObject.SetActive(false);
            ss.子界面.transform.Find("升级界面").gameObject.SetActive(true);
        }
        //变色
        Color nowColor;
        if (GameObject.FindGameObjectsWithTag("未选中").Length > 0)
        {
            GameObject[] 未选中栏 = GameObject.FindGameObjectsWithTag("未选中");
            foreach (GameObject 道具项 in 未选中栏)
            {
                ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
                道具项.GetComponent<Image>().color = nowColor;
            }
        }



        //点击变色
        ColorUtility.TryParseHtmlString("#DBCDCD", out nowColor);
        gameObject.GetComponent<Image>().color = nowColor;



        role_Data myData = io_.load();
        宠物 = PetMgr.GetInstance().返回宠物(UID);

        Sprite 宠物图片 = Resources.Load<Sprite>("怪物/"+宠物.icon);
        if (宠物图片 == null)
        {
            宠物图片 = Resources.Load<Sprite>("怪物/黑白鸟");
        }  
        Image 图标 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/按钮界面/操作/操作界面/背景/图标").GetComponent<Image>();
        //Image 边框 = gameObject.transform.parent.parent.parent.parent.parent.Find("边框").GetComponent<Image>();
        图标.sprite = 宠物图片;
        /*边框.sprite = 宠物图片;
        边框.color = bm.改变透明度(边框.gameObject, 60f);*/



        //信息填充
        Text 当前宠物 = gameObject.transform.parent.parent.Find("标题/当前宠物信息/名字").GetComponent<Text>();
        当前宠物.text= 宠物.name;
        当前宠物.color = bm.转换颜色(bm.Xstoi(宠物.qua));
        Text 名字 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/名字/Text").GetComponent<Text>();
        名字.text = 宠物.name;
        名字.color = bm.转换颜色( bm.Xstoi(宠物.qua));
        Text 评价 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/评价").GetComponent<Text>();
        显示总评价(评价,宠物);
        Text 成长 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/成长/Text").GetComponent<Text>();
        成长.text = bm.Xor(宠物.cc);
        Text 成长上限 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/成长上限/Text").GetComponent<Text>();
        if(bm.Xstoi(宠物.qua)==4)
            成长上限.text = bm.Xstoi(宠物.cc)*2+"";
        else if(bm.Xstoi(宠物.qua) == 5)
            成长上限.text = "无限";
        else
            成长上限.text = bm.Xor(宠物.max_cc);

        Text 等级 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/等级/Text").GetComponent<Text>();
        等级.text = bm.Xor(宠物.grade);
        Text xing = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/星级/Text").GetComponent<Text>();
        xing.text = bm.Xor(宠物.xing);
        Text 攻击力 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/攻击力/Text").GetComponent<Text>();
        攻击力.text = (int)(bm.Xstoi(宠物.ini_atk)+ (bm.Xstoi(宠物.grade)-1)*(bm.Xstoi(宠物.ram_atk) / 1000f) * 3 * bm.Xstoi(宠物.cc))+"";
        Text 防御力 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/防御力/Text").GetComponent<Text>();
        防御力.text = (int)(bm.Xstoi(宠物.ini_def) + (bm.Xstoi(宠物.grade) - 1) * (bm.Xstoi(宠物.ram_def) / 1000f) * 1 * bm.Xstoi(宠物.cc)) + "";
        Text 血量 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/血量/Text").GetComponent<Text>();
        血量.text = (int)(bm.Xstoi(宠物.ini_hp) + (bm.Xstoi(宠物.grade) - 1) * (bm.Xstoi(宠物.ram_hp) / 1000f) * 50 * bm.Xstoi(宠物.cc)) + "";
        Text 回血值 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/回血值/Text").GetComponent<Text>();
        回血值.text = (int)(bm.Xstoi(宠物.ini_hpr) + (bm.Xstoi(宠物.grade) - 1) * (bm.Xstoi(宠物.ram_hpr) / 1000f) * 5 * bm.Xstoi(宠物.cc)) + "";
        Text 攻速 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/攻击速度/Text").GetComponent<Text>();
        攻速.text = bm.Xstoi(宠物.ini_aspd) / 10f + "s";
        Text 暴击 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/属性界面/Panel/属性信息/暴击率/Text").GetComponent<Text>();
        暴击.text = (int)(bm.Xstoi(宠物.ini_cri)  +bm.Xstoi(宠物.qua_cri)/2000f*(bm.Xstoi(宠物.grade)-1))+ "%";


        if (!myData.记录.ContainsKey("经验池"))
            myData.记录.Add("经验池", "0");

        经验池 = long.Parse( myData.记录["经验池"]);
        所需经验 = (rm.经验表(bm.Xstoi(宠物.grade)) / 23) + 33 * bm.Xstoi(宠物.grade);
        Text 经验池文本 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/按钮界面/操作/操作界面/升级背景/经验池").GetComponent<Text>();
        Text 所需经验文本文本 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/按钮界面/操作/操作界面/升级背景/升级所需经验").GetComponent<Text>();
        经验池文本.text ="拥有经验:    "+ 经验池 + "";
        所需经验文本文本.text ="所需经验:    "+ 所需经验 + "";

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
        Text 精华数量文本 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/按钮界面/操作/操作界面/成长背景/拥有数量/num").GetComponent<Text>();
        Text 提升所需文本 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/按钮界面/操作/操作界面/成长背景/提升所需/num").GetComponent<Text>();
        Text 需求精华名字文本 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/按钮界面/操作/操作界面/成长背景/拥有数量/name").GetComponent<Text>();
        Text 拥有精华名字文本 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/按钮界面/操作/操作界面/成长背景/提升所需/name").GetComponent<Text>();
        需求精华名字文本.text = 需求材料;
        拥有精华名字文本.text = 需求材料;
        需求精华名字文本.color = bm.转换颜色(bm.Xstoi(宠物.qua));
        拥有精华名字文本.color = bm.转换颜色(bm.Xstoi(宠物.qua));
        精华数量文本.text ="X"+拥有数量;
        提升所需文本.text = "X" + 需求数量;


        //Debug.Log(myData.出战宠物UID + "\n" + UID);
        Text 出战文本 = gameObject.transform.parent.parent.parent.Find("子界面/升级界面/按钮界面/操作/操作界面/出战/Text").GetComponent<Text>();
        if (myData.出战宠物UID != null && myData.出战宠物UID.Equals(UID))
        {         
            出战文本.text = "出战中...";
            Color nowColor1;
            ColorUtility.TryParseHtmlString("#ECA594", out nowColor1);
            出战文本.transform.parent.GetComponent<Image>().color = nowColor1;
        }
        else {
            出战文本.text = "出战";
            Color nowColor1;
            ColorUtility.TryParseHtmlString("#94DDEC", out nowColor1);
            出战文本.transform.parent.GetComponent<Image>().color = nowColor1;
        }
    }


    public void 显示炼化信息() {
        Text 选中名文本 = GameObject.Find("子界面").transform.Find("炼化界面/界面/炼宠界面/左/Panel/Text").GetComponent<Text>();
        Text 所得名文本 = GameObject.Find("子界面").transform.Find("炼化界面/界面/炼宠界面/右/Panel/Text").GetComponent<Text>();
        Slider 数量拉条 = GameObject.Find("子界面").transform.Find("炼化界面/button/Slider").GetComponent<Slider>();
        InputField 输入文本 = 数量拉条.transform.Find("InputField").GetComponent<InputField>();
        宠物 = pem.返回宠物(UID);
        int qua = bm.Xstoi(宠物.qua);
        选中名文本.text = 宠物.name;
        选中名文本.color = bm.转换颜色(qua);
        所得名文本.text = 品质与精华[qua] + "X "+bm.Xstoi(宠物.cc);
        所得名文本.color = bm.转换颜色(qua);
        数量拉条.maxValue = 1;
        数量拉条.value = 1;
        输入文本.text = "1";
    }


    public void 显示洗练信息() {
        宠物 = pem.返回宠物(UID);
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

        if (宠物.锁.Contains("攻击"))
        {
            子界面.transform.Find("洗练界面/属性/攻击/锁").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
        }
        if (宠物.锁.Contains("防御")) {
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
        Image 图标 = gameObject.transform.parent.parent.parent.Find("子界面/洗练界面/背景/图标").GetComponent<Image>();
        //Image 边框 = gameObject.transform.parent.parent.parent.parent.parent.Find("边框").GetComponent<Image>();
        图标.sprite = 宠物图片;
        Color nowColor;
        ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
        图标.color = nowColor;


        //攻击
        攻击资质.maxValue = bm.Xstoi(宠物.qua_atk);
        攻击资质.value= bm.Xstoi(宠物.ram_atk);
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
    }


    public void 显示总评价(Text 文本, Pet_Data 宠物)
    {
        int MaxValue = bm.Xstoi(宠物.qua_atk) + bm.Xstoi(宠物.qua_def) + bm.Xstoi(宠物.qua_hp) + bm.Xstoi(宠物.qua_hpr);
        int val = bm.Xstoi(宠物.ram_atk) + bm.Xstoi(宠物.ram_def) + bm.Xstoi(宠物.ram_hp) + bm.Xstoi(宠物.ram_hpr);
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

    }

    public void 生成评价(Text 文本,float MaxValue, float value) {
        if (value / MaxValue >= 0.1f && value / MaxValue <= 0.25f) {
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
        else if (value / MaxValue > 0.95f )
        {
            文本.text = "完美";
            文本.color = bm.转换颜色(5);
        }
    }


    public void 点击宠物锁()
    {
        role_Data myData = io_.load();
        Pet_Data 宠物 = pem.返回宠物(UID);
        if (宠物.islock == "1")
        {
            宠物.islock = "0";
            gameObject.transform.Find("锁图标").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/开锁图标");
        }
        else {
            宠物.islock = "1";
            gameObject.transform.Find("锁图标").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
        }
        myData = pem.存档宠物属性覆盖(myData,宠物);
        io_.save(myData);
    }




    private void OnDisable()
    {
        Destroy(gameObject);
    }

}
