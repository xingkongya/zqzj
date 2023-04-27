using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Role_Panel : MonoBehaviour
{
    private Text 左卡;
    private Text 右卡;
    private Text 武器;
    private Text 衣服;
    private Text 头部;
    private Text 下装;
    private Text 饰品;
    private Text 心法;
    private Text 绝招;
    private Text 被动一;
    private Text 被动二;
    private Text 被动三;
    public TianFu 天赋1;
    public TianFu 天赋2;
    public TianFu 天赋3;
    public TianFu 天赋4;
    public TianFu 天赋5;
    public TianFu 天赋6;
    public TianFu 天赋7;
    public TianFu 天赋8;
    private io io_;
    private basicMgr bm;
    private G_Util gut;
    private PropMgr pm;
    private TianFuMgr tf;
    private static Dictionary<string, ArrayList> 职业技能树 = 加载职业天赋树();


    private void Awake()
    {
        io_ = io.GetInstance();
        bm = basicMgr.GetInstance();
        gut =NameMgr.画布.GetComponent<G_Util>();
        pm = PropMgr.GetInstance();
        tf = TianFuMgr.GetInstance();
        左卡 = gameObject.transform.Find("bg/中部/装备页/装备列表/左卡/Text").GetComponent<Text>();
        右卡 = gameObject.transform.Find("bg/中部/装备页/装备列表/右卡/Text").GetComponent<Text>();
        武器 = gameObject.transform.Find("bg/中部/装备页/装备列表/武器/Text").GetComponent<Text>();
        衣服 = gameObject.transform.Find("bg/中部/装备页/装备列表/衣服/Text").GetComponent<Text>();
        头部 = gameObject.transform.Find("bg/中部/装备页/装备列表/头部/Text").GetComponent<Text>();
        下装 = gameObject.transform.Find("bg/中部/装备页/装备列表/下装/Text").GetComponent<Text>();
        饰品 = gameObject.transform.Find("bg/中部/装备页/装备列表/饰品/Text").GetComponent<Text>();
        心法 = gameObject.transform.Find("bg/中部/技能页/心法/Text").GetComponent<Text>();
        绝招 = gameObject.transform.Find("bg/中部/技能页/绝招/Text").GetComponent<Text>();
        被动一 = gameObject.transform.Find("bg/中部/技能页/武学_1/Text").GetComponent<Text>();
        被动二 = gameObject.transform.Find("bg/中部/技能页/武学_2/Text").GetComponent<Text>();
        被动三 = gameObject.transform.Find("bg/中部/技能页/武学_3/Text").GetComponent<Text>();
    }
    private void OnEnable()
    {
        初始化属性面板();
        ini_panel();
        gut.面板属性刷新(gameObject);
        初始化职业();
    }

    public void 初始化属性面板() {
        GameObject 装备页 = gameObject.transform.Find("bg/中部/装备页").gameObject;
        GameObject 技能页 = gameObject.transform.Find("bg/中部/技能页").gameObject;
        GameObject 职业页 = gameObject.transform.Find("bg/中部/职业页").gameObject;
        装备页.SetActive(true);
        技能页.SetActive(false);
        职业页.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private static Dictionary<string, ArrayList> 加载职业天赋树() {
        职业技能树 = new Dictionary<string, ArrayList>();
        ArrayList 基础 = new ArrayList();
        基础.Add("修炼拳脚");
        基础.Add("修炼体魄");
        基础.Add("修炼血脉");
        基础.Add("修炼灵识");
        ArrayList 剑修 = new ArrayList();
        剑修.Add("剑-快剑");
        剑修.Add("剑意");
        剑修.Add("魔剑");
        剑修.Add("剑阵");
        ArrayList 体修 = new ArrayList();
        体修.Add("体-霸体");
        体修.Add("狂意");
        体修.Add("霸拳");
        体修.Add("不灭之躯");
        ArrayList 灵修 = new ArrayList();
        灵修.Add("灵-强决");
        灵修.Add("落雷");
        灵修.Add("三灾");
        灵修.Add("大道之力");
        职业技能树.Add("基础",基础);
        职业技能树.Add("剑修",剑修);
        职业技能树.Add("体修",体修);
        职业技能树.Add("灵修",灵修);
        return 职业技能树;
    }

    public void ini_panel() {
        role_Data myData = io_.load();
        Dictionary<string, Equipment> 装备槽 = myData.装备槽;
        foreach (string 位置 in 装备槽.Keys)
        {
            if (位置.Equals("左卡"))
                初始化装备列表("左卡", 装备槽, 左卡);
            else if (位置.Equals("右卡"))
                初始化装备列表("右卡", 装备槽, 右卡);
            else if (位置.Equals("武器"))
                初始化装备列表("武器", 装备槽, 武器);
            else if (位置.Equals("衣服"))
                初始化装备列表("衣服", 装备槽, 衣服);
            else if (位置.Equals("头部"))
                初始化装备列表("头部", 装备槽, 头部);
            else if (位置.Equals("下装"))
                初始化装备列表("下装", 装备槽, 下装);
            else if (位置.Equals("饰品"))
                初始化装备列表("饰品", 装备槽, 饰品);
        }

        Dictionary<string, string> 技能槽 = myData.技能槽;
        foreach (string 位置 in 技能槽.Keys)
        {
            if (位置.Equals("0"))
                初始化技能列表(位置, 技能槽, 心法);
            else if (位置.Equals("1"))
                初始化技能列表(位置, 技能槽, 绝招);
            else if (位置.Equals("2"))
                初始化技能列表(位置, 技能槽, 被动一);
            else if (位置.Equals("3"))
                初始化技能列表(位置, 技能槽, 被动二);
            else if (位置.Equals("4"))
                初始化技能列表(位置, 技能槽, 被动三);
        }
    }

    void 初始化装备列表(string 位置, Dictionary<string, Equipment> 装备槽,Text 目标文字) {
        Equipment 装备 = 装备槽[位置];
        Image 背景图标 = 目标文字.transform.parent.Find("背景/Panel").GetComponent<Image>();
        if (装备 != null)
        {
            目标文字.text = 装备.name;
            目标文字.color = bm.转换颜色(bm.Xstoi(装备.qua));
            gut.初始化图标(背景图标, 装备);
        }
        else
        {
            Color nowColor;
            ColorUtility.TryParseHtmlString("#323232", out nowColor);
            目标文字.color = nowColor;
            目标文字.text = 位置;
            背景图标.sprite = Resources.Load<Sprite>("图标/灰色背景图标");
        }

    }

    void 初始化技能列表(string 位置, Dictionary<string, string> 技能槽, Text 目标文字)
    {
        SkillData sd = pm.检索技能( 技能槽[位置]);
        if (sd.name!=null)
        {
            目标文字.text = sd.name;
            目标文字.color = bm.转换颜色(bm.Xstoi(sd.qua));
        }
        else
        {
            Color nowColor;
            ColorUtility.TryParseHtmlString("#323232", out nowColor);
            目标文字.color = nowColor;
            if (位置.Equals("0"))
                目标文字.text = "心法";
            else if (位置.Equals("1"))
                目标文字.text = "绝招";
            else if (位置.Equals("2"))
                目标文字.text = "被动一";
            else if (位置.Equals("3"))
                目标文字.text = "被动二";
            else if (位置.Equals("4"))
                目标文字.text = "被动三";
        }

    }

    void 初始化职业()
    {
        GameObject 中部 = gameObject.transform.Find("bg/中部").gameObject;
        GameObject 加点界面=中部.transform.Find("职业页/加点界面").gameObject;
        GameObject 选职界面 = 中部.transform.Find("职业页/初始选职界面").gameObject;
        role_Data myData = io_.load();
        if (myData.职业["职业"].Equals(""))
        {
            加点界面.SetActive(false);
            选职界面.SetActive(true);
        }
        else {
            加点界面.SetActive(true);
            选职界面.SetActive(false);
            刷新加点界面(myData);
            刷新职业属性(myData);
        }
    }


    public void 刷新加点界面(role_Data myData) {
        GameObject 中部 = gameObject.transform.Find("bg/中部").gameObject;
        Text 职业文本 = 中部.transform.Find("职业页/加点界面/职业文本/Text").GetComponent<Text>();
        职业文本.text = myData.职业["职业"];
    
    }


    public void 刷新职业属性(role_Data myData) {
        GameObject 基础技能 = gameObject.transform.Find("bg/中部/职业页/加点界面/背景图/基础加点").gameObject;
        GameObject 高级技能 = gameObject.transform.Find("bg/中部/职业页/加点界面/背景图/高级加点").gameObject;
        GameObject 一技能 = 基础技能.transform.Find("1").gameObject;
        GameObject 二技能 = 基础技能.transform.Find("2").gameObject;
        GameObject 三技能 = 基础技能.transform.Find("3").gameObject;
        GameObject 四技能 = 基础技能.transform.Find("4").gameObject;
        GameObject 五技能 = 高级技能.transform.Find("5").gameObject;
        GameObject 六技能 = 高级技能.transform.Find("6").gameObject;
        GameObject 七技能 = 高级技能.transform.Find("7").gameObject;
        GameObject 八技能 = 高级技能.transform.Find("8").gameObject;

         天赋1 =tf.返回天赋( 返回职业技能信息(1, myData));
         天赋2 =tf.返回天赋( 返回职业技能信息(2, myData));
         天赋3 =tf.返回天赋( 返回职业技能信息(3, myData));
         天赋4 =tf.返回天赋( 返回职业技能信息(4, myData));
         天赋5 =tf.返回天赋( 返回职业技能信息(5, myData));
         天赋6 =tf.返回天赋( 返回职业技能信息(6, myData));
         天赋7 =tf.返回天赋( 返回职业技能信息(7, myData));
         天赋8 =tf.返回天赋( 返回职业技能信息(8, myData));

        tf.天赋加载图标(一技能,天赋1);
        tf.天赋加载图标(二技能,天赋2);
        tf.天赋加载图标(三技能,天赋3);
        tf.天赋加载图标(四技能,天赋4);
        tf.天赋加载图标(五技能,天赋5);
        tf.天赋加载图标(六技能,天赋6);
        tf.天赋加载图标(七技能,天赋7);
        tf.天赋加载图标(八技能,天赋8);

    }


    public void 生成职业一技能信息() {
        天赋1= tf.刷新介绍(天赋1);
        gut.生成天赋框(gameObject,天赋1);
    }


    public void 生成职业二技能信息()
    {
        天赋2 = tf.刷新介绍(天赋2);
        gut.生成天赋框(gameObject, 天赋2);
    }


    public void 生成职业三技能信息()
    {
        天赋3 = tf.刷新介绍(天赋3);
        gut.生成天赋框(gameObject, 天赋3);
    }


    public void 生成职业四技能信息()
    {
        天赋4 = tf.刷新介绍(天赋4);
        gut.生成天赋框(gameObject, 天赋4);
    }


    public void 生成职业五技能信息()
    {
        天赋5 = tf.刷新介绍(天赋5);
        gut.生成天赋框(gameObject, 天赋5);
    }



    public void 生成职业六技能信息()
    {
        天赋6 = tf.刷新介绍(天赋6);
        gut.生成天赋框(gameObject, 天赋6);
    }


    public void 生成职业七技能信息()
    {
        天赋7 = tf.刷新介绍(天赋7);
        gut.生成天赋框(gameObject, 天赋7);
    }


    public void 生成职业八技能信息()
    {
        天赋8 = tf.刷新介绍(天赋8);
        gut.生成天赋框(gameObject, 天赋8);
    }


    public string 返回职业技能信息(int index, role_Data myData) {
        if (index < 5)
        {
            return 职业技能树["基础"][index - 1] + "";
        }
        else {
            return 职业技能树[myData.职业["职业"]][index-5] + "";
        }
    }



    public void 生成心法背包()
    {
        gut.生成技能背包("心法",0);

    }

    public void 生成绝招背包()
    {
        gut.生成技能背包("绝招",1);

    }

    public void 生成被动1背包()
    {
        gut.生成技能背包("被动",2);

    }

    public void 生成被动2背包()
    {
        gut.生成技能背包("被动",3);

    }

    public void 生成被动3背包()
    {
        gut.生成技能背包("被动",4);

    }

    public void 职业选项全取消() {
        GameObject 剑修 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/剑修").gameObject;
        GameObject 体修 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/体修").gameObject;
        GameObject 灵修 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/灵修").gameObject; 
        GameObject 剑修_选择 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/剑修").transform.Find("选他按钮").gameObject;
        GameObject 体修_选择 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/体修").transform.Find("选他按钮").gameObject;
        GameObject 灵修_选择 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/灵修").transform.Find("选他按钮").gameObject;
        剑修.GetComponent<Image>().color = bm.改变透明度(剑修, 0f);
        体修.GetComponent<Image>().color = bm.改变透明度(体修, 0f);
        灵修.GetComponent<Image>().color = bm.改变透明度(灵修, 0f);
        剑修_选择.SetActive(false);
        体修_选择.SetActive(false);
        灵修_选择.SetActive(false);
    }

    public void 剑修选择()
    {
        GameObject 剑修 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/剑修").gameObject;
        GameObject 剑修_选择 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/剑修").transform.Find("选他按钮").gameObject;
        剑修.GetComponent<Image>().color = bm.改变透明度(剑修, 200f);
        剑修_选择.SetActive(true);
    }

    public void 体修选择()
    {
        GameObject 体修 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/体修").gameObject;
        GameObject 体修_选择 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/体修").transform.Find("选他按钮").gameObject;
        体修.GetComponent<Image>().color = bm.改变透明度(体修, 200f);
        体修_选择.SetActive(true);
    }

    public void 灵修选择()
    {
        GameObject 灵修 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/灵修").gameObject;
        GameObject 灵修_选择 = gameObject.transform.Find("bg/中部/职业页/初始选职界面/选择界面/灵修").transform.Find("选他按钮").gameObject;
        灵修.GetComponent<Image>().color = bm.改变透明度(灵修, 200f);
        灵修_选择.SetActive(true);
    }

    public void 职业确认(string 职业) {
        if (职业.Equals("剑修"))
        {
            gut.生成确认框("确认要消耗 <color=red>拜师贴</color>*1 选择<color=red>" + 职业 + "</color>\n\n<color=red>" + 职业 + "</color>每级  (攻,防,血):\n攻击+3→攻击+4\n防御+1→防御+1\n血量+30→血量+40\n<color=green>基础攻速1.2/s→基础攻速1.0/s</color>", 剑修确定);
        }
        else if (职业.Equals("体修")) 
        {
            gut.生成确认框("确认要消耗 <color=red>拜师贴</color>*1 选择<color=red>" + 职业 + "</color>\n\n<color=red>" + 职业 + "</color>每级  (攻,防,血):\n攻击+3→攻击+3\n防御+1→防御+2\n血量+30→血量+50\n<color=green>基础攻速1.2/s→基础攻速1.5/s</color>", 体修确定);
        }
        else if (职业.Equals("灵修"))
        {
            gut.生成确认框("确认要消耗 <color=red>拜师贴</color>*1 选择<color=red>" + 职业 + "</color>\n\n<color=red>" + 职业 + "</color>每级  (攻,防,血):\n攻击+3→攻击+5\n防御+1→防御+1\n血量+30→血量+35\n<color=green>基础攻速1.2/s→基础攻速1.2/s</color>", 灵修确定);
        }
    }

    public void 职业确定(string 职业)
    {
        role_Data myData = io_.load();
        myData.天赋["职业"].Add("修炼拳脚", bm.Xitos(0));
        myData.天赋["职业"].Add("修炼体魄", bm.Xitos(0));
        myData.天赋["职业"].Add("修炼血脉", bm.Xitos(0));
        myData.天赋["职业"].Add("修炼神识", bm.Xitos(0));
        if (职业.Equals("剑修"))
        {
            myData.职业["职业"] = "剑修";
            myData.天赋["职业"].Add("剑-快剑", bm.Xitos(0));
            myData.天赋["职业"].Add("剑意", bm.Xitos(0));
            myData.天赋["职业"].Add("剑阵", bm.Xitos(0));
            myData.天赋["职业"].Add("魔剑", bm.Xitos(0));
        }
        else if (职业.Equals("体修"))
        {
            myData.职业["职业"] = "体修";
            myData.天赋["职业"].Add("体-霸体", bm.Xitos(0));
            myData.天赋["职业"].Add("狂意", bm.Xitos(0));
            myData.天赋["职业"].Add("霸拳", bm.Xitos(0));
            myData.天赋["职业"].Add("不灭之躯", bm.Xitos(0));
        }
        else if (职业.Equals("灵修"))
        {
            myData.职业["职业"] = "灵修";
            myData.天赋["职业"].Add("灵-强诀", bm.Xitos(0));
            myData.天赋["职业"].Add("落雷", bm.Xitos(0));
            myData.天赋["职业"].Add("三灾", bm.Xitos(0));
            myData.天赋["职业"].Add("大道之力", bm.Xitos(0));
        }
        io_.save(myData);
        DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>().人物属性刷新();
    }


    public void 重置职业() {
        role_Data myData = io_.load();
        myData.职业["职业"] = "";
        myData.天赋["职业"].Clear();
        io_.save(myData);
        DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>().人物属性刷新();
        初始化职业();
    }


    public void 关闭自己() {
        gut.删除对象(gameObject);
    }

    public void 剑修确认()
    {
        职业确认("剑修");
    }

    public void 体修确认()
    {
        职业确认("体修");
    }

    public void 灵修确认()
    {
        职业确认("灵修");
    }
    public void 剑修确定()
    {
        职业确定("剑修");
        初始化职业();
        关闭确认框();
    }

    public void 体修确定()
    {
        职业确定("体修");
        初始化职业();
        关闭确认框();
    }

    public void 灵修确定()
    {
        职业确定("灵修");
        初始化职业();
        关闭确认框();
    }

    public void 关闭确认框() {
        gut.删除确认框();
    }
}
