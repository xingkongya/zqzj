using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// <summary>
/// Dictionary<X,Y> Dictionary泛型
/// </summary>
/// <typeparam name="T"></typeparam>
public class DictionaryClass<X, Y>
{
    private Dictionary<X, Y> sl;
    public DictionaryClass()
    {
        sl = new Dictionary<X, Y>();
    }

    public void Add(X key, Y vaus)
    {
        sl.Add(key, vaus);

    }

    public Dictionary<X, Y> Get()
    {
        return sl;

    }

    public int Count()
    {

        return sl.Count;
    }
}


/// <summary>
/// list<T> list泛型
/// </summary>
/// <typeparam name="T"></typeparam>
public class ListClass<T>
{
    private List<T> sl;
    public ListClass()
    {
        sl = new List<T>();
    }

    public void Add(T item)
    {
        sl.Add(item);

    }

    public List<T> Get()
    {
        return sl;

    }

    public int Count()
    {

        return sl.Count;
    }
}




public class G_Util : MonoBehaviour
{
    public int 副本步数 = 0;
    string ABPath;
    public bool isAuto = false;
    public int cdTimes = 0;
    //引用
    private io io_;
    public Dictionary<string, GameObject> 现有怪物集合 = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> 现有角色集合 = new Dictionary<string, GameObject>();
    private combat cb;
    private basicMgr bm;
    private DataMgr dm;
    private PropMgr pm;
    private SkillMgr sm;
    private PetMgr pem;
    private SkillApplicator sa;
    private MonsterMgr mmgr;
    private PropEffect pe;
    private SkillEffect se;
    private ScenceTask st;
    public role_Data myData;
    private EventCenter em;
    private combat rcb;
    private AsyncOperation async;
    public Dictionary<string, Action<int>> 道具效果表;
    public Dictionary<string, UnityAction> 场景任务表;
    public List<string> 时间线程名字;
    public List<string> 运行中的时间线程 = new List<string>();
    public Dictionary<string, string> 移动与坐标 = new Dictionary<string, string>();
    public Dictionary<string, GameObject> 实例化对象池 = new Dictionary<string, GameObject>();

    private void Awake()
    {
        dm = DataMgr.GetInstance();
        io_ = io.GetInstance();
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
        mmgr = MonsterMgr.GetInstance();
        sm = SkillMgr.GetInstance();
        pem = PetMgr.GetInstance();
        se = SkillEffect.GetInstance();
        sa = SkillApplicator.GetInstance();
        myData = io_.load();//读取数据初始化用
        pe = PropEffect.GetInstance();
        st = ScenceTask.GetInstance();
        em = EventCenter.GetInstance();
        道具效果表 = pe.加载道具效果();
        场景任务表 = st.加载任务事件表();

        //移动端不能使用Resources动态加载资源

        /*对话框预制体 = (GameObject)Resources.Load<GameObject>("Prefabs/对话框");
        气泡预制体 = (GameObject)Resources.Load<GameObject>("Prefabs/气泡");
        光球预制体 = (GameObject)Resources.Load<GameObject>("Prefabs/光球");
        背包预制体 = (GameObject)Resources.Load<GameObject>("Prefabs/背包");
        物品项预制体 = (GameObject)Resources.Load<GameObject>("Prefabs/物品项");
        获得框预制体 = (GameObject)Resources.Load<GameObject>("Prefabs/获得框");
        选项框预制体 = (GameObject)Resources.Load<GameObject>("Prefabs/选项框");
        选项预制体 = (GameObject)Resources.Load<GameObject>("Prefabs/选项");
        UI预制体 = (GameObject)Resources.Load<GameObject>("Prefabs/UI");
        角色预制体 = (GameObject)Resources.Load<GameObject>("Prefabs/血条");
        伤害预制体= (GameObject)Resources.Load<GameObject>("Prefabs/伤害");*/
        if (gameObject.CompareTag("幻界"))
            加载();
        //RoleMgr.GetInstance().打印等级的经验表(1000);

        运行中的时间线程.Clear();
        sa.CD集合 = 储存_字符串转换为单精度(myData.CD);
        CheckClientTimeScale(1);

    }

    private void OnEnable()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        //设置-屏幕常亮
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        生成道具界面();
    }

    // Update is called once per frame
    void Update()
    {
        检索倒计时();
    }

    private void CheckClientTimeScale(int nTimes)

    {

        StartCoroutine(DoCheckClientTimeScale(nTimes));

    }

    private IEnumerator DoCheckClientTimeScale(int nTimes)

    {

        yield return new WaitForSeconds(1f);

        if (Time.timeScale > 1f)

        {

            cdTimes++;

            if (cdTimes >= 2)

            {

                cdTimes = 0;

                Time.timeScale = 1;

            }

        }

        CheckClientTimeScale(nTimes);

    }

    public Dictionary<string, string> 储存_单精度转换为字符串(Dictionary<string, float> 单精度) {
        Dictionary<string, string> 字符串字典 = new Dictionary<string, string>();
        foreach (string 冷却名 in 单精度.Keys) {
            字符串字典.Add(冷却名, 单精度[冷却名] + "");
        }
        return 字符串字典;
    }


    public Dictionary<string, float> 储存_字符串转换为单精度(Dictionary<string, string> 整型)
    {
        Dictionary<string, float> 单精度字典 = new Dictionary<string, float>();
        foreach (string 冷却名 in 整型.Keys)
        {
            单精度字典.Add(冷却名, float.Parse(整型[冷却名]));
        }
        return 单精度字典;
    }

    public void 刷新网络时间()
    {
        StartCoroutine(bm.GetWebTime());
    }

    public void 刷新移动与坐标() {
        GameObject 上 = gameObject.transform.Find("UI/战斗页/2级画布/address/move/up").gameObject;
        GameObject 下 = gameObject.transform.Find("UI/战斗页/2级画布/address/move/down").gameObject;
        GameObject 左 = gameObject.transform.Find("UI/战斗页/2级画布/address/move/left").gameObject;
        GameObject 右 = gameObject.transform.Find("UI/战斗页/2级画布/address/move/right").gameObject;
        //先把按钮全关闭了
        上.SetActive(false);
        下.SetActive(false);
        左.SetActive(false);
        右.SetActive(false);
        foreach (string 方向 in 移动与坐标.Keys) {
            if (方向.Equals("上"))
            {
                上.SetActive(true);
                上.transform.Find("Text").GetComponent<Text>().text = 移动与坐标[方向];
            }
            else if (方向.Equals("下")) {
                下.SetActive(true);
                下.transform.Find("Text").GetComponent<Text>().text = 移动与坐标[方向];

            }
            else if (方向.Equals("左"))
            {
                左.SetActive(true);
                左.transform.Find("Text").GetComponent<Text>().text = 移动与坐标[方向];

            }
            else if (方向.Equals("右"))
            {
                右.SetActive(true);
                右.transform.Find("Text").GetComponent<Text>().text = 移动与坐标[方向];

            }
        }
        移动与坐标.Clear();
    }

    public void 检索倒计时()
    {
        时间线程名字 = new List<string>();
        if (sa.CD集合.Count != 0)
        {
            foreach (string 冷却名 in sa.CD集合.Keys)
            {
                时间线程名字.Add(冷却名);
            }

            for (int i = 0; i < 时间线程名字.Count; i++) {
                if (!运行中的时间线程.Contains(时间线程名字[i])) {
                    StartCoroutine(时间线程(时间线程名字[i]));
                }
            }

        }
    }


    public IEnumerator 时间线程(string key)
    {
        //Debug.Log(key + "冷却开始");
        if (!运行中的时间线程.Contains(key))
            运行中的时间线程.Add(key);

        float L_time = sa.CD集合[key];
        while (true)
        {
            L_time -= Time.deltaTime;
            sa.CD集合[key] = L_time;
            if (L_time < 0)
            {
                sa.CD集合.Remove(key);
                break;
            }
            yield return 0;
        }
        //Debug.Log(key + "冷却结束");
        if (运行中的时间线程.Contains(key))
        {
            运行中的时间线程.Remove(key);
        }
    }

    public void 初始化绝招图标() {
        //GameObject UI = GameObject.Find("UI");
        myData = io_.load();
        if (myData.技能槽.ContainsKey("1") && !myData.技能槽["1"].Equals(""))
        {
            string 绝招名字 = myData.技能槽["1"];
            SkillData sd = pm.检索技能(绝招名字);
            if (sd.get != null)
            {
                GameObject UI = GameObject.FindGameObjectWithTag("UI");
                Image 绝招图片 = UI.transform.Find("战斗页/combat_bg/2级画布/绝招/遮罩/图标").gameObject.GetComponent<Image>();
                if (sd.get.Equals("攻击"))
                    绝招图片.sprite = Resources.Load("图标/攻击技能图标", typeof(Sprite)) as Sprite;
                else if (sd.get.Equals("治疗"))
                    绝招图片.sprite = Resources.Load("图标/治疗技能图标", typeof(Sprite)) as Sprite;
                else if (sd.get.Contains("状态"))
                    绝招图片.sprite = Resources.Load("图标/状态技能图标", typeof(Sprite)) as Sprite;
            }
        }

    }




    public string 使用道具(string 道具名称,int 使用数量)
    {
        pe.返回状态 = "";
        if (!道具效果表.ContainsKey(道具名称))//检查道具表里是否绑定了委托
            return "未绑定效果,请联系作者";
        else
            道具效果表[道具名称](使用数量);//使用道具
        /* GameObject 物品项 = GameObject.Find(道具名称);
         if (物品项 != null)
         {
             Text 数量 = 物品项.transform.Find("数量").GetComponent<Text>();
             int num = int.Parse(数量.text);
             num=num-使用数量;
             if (num == 0)
             {
                 GameObject bag = NameMgr.背包;
                 if (bag != null)
                     bag.GetComponent<Bag>().初始化背包();
             }
             else if (num < 0)
                 num = 0;
             数量.text = num + "";
         }*/

        if (DataMgr.GetInstance().本地对象.ContainsKey("背包")) {
            DataMgr.GetInstance().本地对象["背包"].GetComponent<Bag>().初始化背包();
        }
        if (!pe.返回状态.Equals("")) //检测使用成败,如果失败接受失败原因字符串
            return pe.返回状态;

        return "使用成功";
    }

    private void 生成画布()
    {

        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(basicMgr.GetInstance().LoadABPrefabs("画布", 实例化画布, 参数集));

    }

    private void 实例化画布(GameObject 对象, Dictionary<int, object> 参数集)
    {
        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 画布 = GameObject.Instantiate(对象) as GameObject;
        画布.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);//recttransform必不可少的属性(半知半解)
        画布.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        画布.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        画布.transform.SetAsLastSibling();
        画布.name = "老画布";
        DontDestroyOnLoad(画布);
        星空战斗图标加载();
    }

    private void 生成战斗场地()
    {
        if (GameObject.Find("combat(Clone)"))
            return;
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("combat", 实例化战斗场地, 参数集));

    }

    private void 实例化战斗场地(GameObject 对象, Dictionary<int, object> 参数集)
    {
        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 场地 = GameObject.Instantiate(对象) as GameObject;
        场地.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 910);//recttransform必不可少的属性(半知半解)
        场地.transform.localPosition = new Vector3(0, 100, 0);//设置生成位置
        场地.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        场地.transform.SetAsLastSibling();
        加载角色();
        if (myData.记录.ContainsKey("关闭天气") && myData.记录["关闭天气"] != "1" && gameObject.CompareTag("野外"))//加载天气,每次切换场景都加载一次
            生成天气();
    }

    public void 生成对话框(string talk, int index, float 语速, string index_Name)
    {
        GameObject 对话框_last = GameObject.Find("对话框(Clone)");
        if (对话框_last != null)
        {
            Destroy(对话框_last);
        }
        语速 -= 0.03f;

        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, talk);
        参数集.Add(1, index);
        参数集.Add(2, 语速);
        参数集.Add(3, index_Name);
        StartCoroutine(bm.LoadABPrefabs("对话框", 实例化对话框, 参数集));

    }

    private void 实例化对话框(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = NameMgr.画布;
        //实例化对话框(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 对话框 = GameObject.Instantiate(对象) as GameObject;
        对话框.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        对话框.transform.localPosition = new Vector2(0, 100);//设置生成位置
        对话框.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        对话框.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        string talk = 参数集[0] as string;
        int index = (int)参数集[1];
        float 语速 = (float)参数集[2];
        string index_Name = 参数集[3] as string;
        Text_DaYin dy = 对话框.GetComponent<Text_DaYin>();//加载组件, 因为是按钮触发所以不能放在Awake里
        dy.wenZiText = 对话框.transform.Find("Text_talk").GetComponent<Text>();
        //显示对话内容
        dy.str = talk;
        dy.语速 = 语速;
        dy.playText();
        PlayerPrefs.SetInt(index_Name, index + 1);
    }




    public void 生成气泡(string str, GameObject 父物体)
    {
        if (父物体.transform.Find("气泡_位置").transform.childCount == 0)//判断是否创建了气泡
        {
            GameObject 气泡_位置 = 父物体.transform.Find("气泡_位置").gameObject;
            Dictionary<int, object> 参数集 = new Dictionary<int, object>();
            参数集.Add(0, str);
            参数集.Add(1, 气泡_位置);
            StartCoroutine(bm.LoadABPrefabs("气泡", 实例化气泡, 参数集));
        }
        else
        {
            父物体.transform.Find("气泡_位置").gameObject.transform.GetChild(0).gameObject.SetActive(true);
            父物体.transform.Find("气泡_位置").gameObject.transform.GetChild(0).gameObject.transform.Find("Text").GetComponent<Text>().text = str;//同步气泡内容
        }
    }

    private void 实例化气泡(GameObject 对象, Dictionary<int, object> 参数集)
    {
        string str = 参数集[0] as string;
        GameObject 父物体 = 参数集[1] as GameObject;
        //实例化气泡(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 气泡 = GameObject.Instantiate(对象) as GameObject;
        气泡.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        气泡.GetComponent<RectTransform>().sizeDelta = new Vector2(260, 50);//recttransform必不可少的属性(半知半解)
        气泡.transform.localPosition = new Vector2(0, 0);//设置生成位置
        气泡.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        气泡.transform.Find("Text").GetComponent<Text>().text = str;//同步气泡内容
        气泡.transform.SetAsFirstSibling();
    }

    public void 剧情重启()
    {
        PlayerPrefs.DeleteAll();
    }

    public void 初始化图标(Image 图标, Prop_bascis 物品)
    {

        if (物品.type.Equals("1") || 物品.type.Equals("2"))
        {
            if (物品.icon.Equals("收集品"))
                图标.sprite = Resources.Load("图标/材料图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("道具"))
                图标.sprite = Resources.Load("图标/道具图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("兽皮"))
                图标.sprite = Resources.Load("图标/兽皮图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("兽骨"))
                图标.sprite = Resources.Load("图标/兽骨图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("宠物蛋"))
                图标.sprite = Resources.Load("图标/宠物蛋图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("神兽蛋"))
                图标.sprite = Resources.Load("图标/宠物蛋图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("技能书"))
                图标.sprite = Resources.Load("图标/技能书图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("钥匙"))
                图标.sprite = Resources.Load("图标/钥匙图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("进化材料"))
                图标.sprite = Resources.Load("图标/进化材料图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("礼包"))
                图标.sprite = Resources.Load("图标/礼包图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("礼盒"))
                图标.sprite = Resources.Load("图标/礼盒图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("宝箱"))
                图标.sprite = Resources.Load("图标/宝箱图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("仙晶卡"))
                图标.sprite = Resources.Load("图标/仙晶卡图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("精华"))
            {
                图标.sprite = Resources.Load("图标/精华图标", typeof(Sprite)) as Sprite;
                图标.color = bm.转换颜色(bm.Xstoi(物品.qua));
            }
            else if (物品.icon.Equals("晶矿"))
            {
                图标.sprite = Resources.Load("图标/晶矿图标", typeof(Sprite)) as Sprite;
                图标.color = bm.转换颜色(bm.Xstoi(物品.qua));
            }
            else if (物品.icon.Equals("空间结晶"))
                图标.sprite = Resources.Load("图标/空间结晶图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("血药"))
                图标.sprite = Resources.Load("图标/血药图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("暗器"))
                图标.sprite = Resources.Load("图标/暗器图标1", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("钱币"))
                图标.sprite = Resources.Load("图标/钱币图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("药水"))
                图标.sprite = Resources.Load("图标/药水图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("人物经验丹"))
                图标.sprite = Resources.Load("图标/人物经验丹图标", typeof(Sprite)) as Sprite;
            else if (物品.icon.Equals("宠物经验丹"))
                图标.sprite = Resources.Load("图标/宠物经验丹图标", typeof(Sprite)) as Sprite;
        }
        else if (物品.type.Equals("3"))
        {
            Equipment 装备 = 物品 as Equipment;
            if (装备.icon!=null&&!装备.icon.Equals(""))//特殊装备
            {
                if (装备.icon.Equals("剑1"))
                    图标.sprite = Resources.Load("装备/剑1图标", typeof(Sprite)) as Sprite;
                else if (装备.icon.Equals("甲1"))
                    图标.sprite = Resources.Load("装备/甲1图标", typeof(Sprite)) as Sprite;
                else if (装备.icon.Equals("头1"))
                    图标.sprite = Resources.Load("装备/头1图标", typeof(Sprite)) as Sprite;
                else if (装备.icon.Equals("鞋1"))
                    图标.sprite = Resources.Load("装备/鞋1图标", typeof(Sprite)) as Sprite;
                else if (装备.icon.Equals("戒1"))
                    图标.sprite = Resources.Load("装备/戒1图标", typeof(Sprite)) as Sprite;

            }
            else {
                if (装备.place.Equals("武器"))
                    图标.sprite = Resources.Load("图标/剑图标", typeof(Sprite)) as Sprite;
                else if (装备.place.Equals("衣服"))
                    图标.sprite = Resources.Load("图标/衣服图标", typeof(Sprite)) as Sprite;
                else if (装备.place.Equals("头部"))
                    图标.sprite = Resources.Load("图标/头部图标", typeof(Sprite)) as Sprite;
                else if (装备.place.Equals("左卡"))
                    图标.sprite = Resources.Load("图标/左卡图标", typeof(Sprite)) as Sprite;
                else if (装备.place.Equals("右卡"))
                    图标.sprite = Resources.Load("图标/右卡图标", typeof(Sprite)) as Sprite;
                else if (装备.place.Equals("饰品"))
                    图标.sprite = Resources.Load("图标/饰品图标", typeof(Sprite)) as Sprite;
                else if (装备.place.Equals("下装"))
                    图标.sprite = Resources.Load("图标/下装图标", typeof(Sprite)) as Sprite;
            }
        }else
            图标.sprite = Resources.Load("图标/灰色背景图标", typeof(Sprite)) as Sprite;
    }

    public string 获取对象Text上的字符串(GameObject 对象) {
        string str = 对象.GetComponent<Text>().text;
        return str;
    }
    public void 移动显示(Dictionary<string, string> 移动信息)
    {
        //写死
        if (移动信息.ContainsKey("上"))
        {
            GameObject up = GameObject.Find("move").transform.Find("up").gameObject;
            up.gameObject.SetActive(true);
            up.transform.GetComponent<Text>().text = 移动信息["上"];
        }
        else if (移动信息.ContainsKey("下"))
        {
            GameObject down = GameObject.Find("move").transform.Find("down").gameObject;
            down.gameObject.SetActive(true);
            down.transform.GetComponent<Text>().text = 移动信息["下"];
        }
        else if (移动信息.ContainsKey("左"))
        {
            GameObject left = GameObject.Find("move").transform.Find("left").gameObject;
            left.gameObject.SetActive(true);
            left.transform.GetComponent<Text>().text = 移动信息["左"];
        }
        else if (移动信息.ContainsKey("下"))
        {
            GameObject right = GameObject.Find("move").transform.Find("right").gameObject;
            right.gameObject.SetActive(true);
            right.transform.GetComponent<Text>().text = 移动信息["右"];
        }
    }




    public void 生成选项框(Dictionary<string, UnityAction> 选项信息, GameObject 父物体)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        参数集.Add(1, 选项信息);
        StartCoroutine(bm.LoadABPrefabs("选项框", 实例化选项框, 参数集));
    }



    private void 实例化选项框(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject;
        Dictionary<string, UnityAction> 选项信息 = 参数集[1] as Dictionary<string, UnityAction>;
        //实例化选项框(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 选项_bg = GameObject.Instantiate(对象) as GameObject;
        选项_bg.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        选项_bg.transform.Find("选项框").GetComponent<RectTransform>().sizeDelta = new Vector2(200, 100 * 选项信息.Count);//recttransform必不可少的属性(半知半解)
        选项_bg.transform.localPosition = new Vector2(0, 0);//设置生成位置
        选项_bg.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        选项_bg.transform.SetAsLastSibling();
        参数集[0] = 选项_bg;
        bm.Banding(选项_bg, 关闭杂项);
        StartCoroutine(bm.LoadABPrefabs("选项", 实例化选项, 参数集));

    }


    private void 实例化选项(GameObject 对象, Dictionary<int, object> 参数集)
    {

        GameObject 选项_bg = 参数集[0] as GameObject;
        Dictionary<string, UnityAction> 选项信息 = 参数集[1] as Dictionary<string, UnityAction>;
        //实例化选项
        int n = 0;
        foreach (string 事件名 in 选项信息.Keys)
        {
            GameObject 选项 = GameObject.Instantiate(对象) as GameObject;
            选项.transform.SetParent(选项_bg.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
            选项.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 50);//recttransform必不可少的属性(半知半解)
            选项.transform.localPosition = new Vector3(0, 50 * 选项信息.Count - (100 * n) - 50, 0);//设置生成位置
            选项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
            选项.transform.Find("Text").GetComponent<Text>().text = 事件名;
            选项.name = 事件名;
            bm.Banding(选项, 选项信息[事件名]);
            n++;
        }
    }


    public void 关闭杂项()
    {
        //关闭选项框
        GameObject[] 选项框 = GameObject.FindGameObjectsWithTag("杂项");
        if (选项框.Length != 0)
        {
            for (int i = 0; i < 选项框.Length; i++)
            {
                Destroy(选项框[i]);
            }
        }
    }


    public void 隐藏杂项()
    {
        //关闭选项框
        GameObject[] 选项框 = GameObject.FindGameObjectsWithTag("杂项");
        if (选项框.Length != 0)
        {
            for (int i = 0; i < 选项框.Length; i++)
            {
                选项框[i].gameObject.SetActive(false);
            }
        }
    }


    public bool 概率(int 分子, int 分母)
    {
        int 随机数 = UnityEngine.Random.Range(1, 分母 + 1);
        if (随机数 <= 分子)
            return true;
        else return false;
    }


    /// <summary>
    /// 礼包分母为10000
    /// </summary>
    /// <param name="礼包内容与概率"></param>
    /// <returns></returns>
    public string 概率_礼包(Dictionary<string,int>礼包内容与概率,string 保底) {
        int 随机数 = UnityEngine.Random.Range(1, 10000 + 1);
        int index = 0;
        foreach (string name in 礼包内容与概率.Keys) {
            index += 礼包内容与概率[name];
            if (随机数 <= index)
                return name;
        }
        return 保底;
    }

    public void 加载()
    {

        if (gameObject.CompareTag("幻界"))
            加载UI_Other();
        else
        {
            生成战斗场地();
            加载UI();
        }
    }


    public void 星空战斗图标加载()
    {
        生成战斗场地();
        加载UI();
    }


    private void 空方法()
    {

    }

    public void 加载UI()
    {
        if (NameMgr.画布!=null)//如果有UI了就不加载
            return;
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("ui", 实例化UI, 参数集));

    }

    private void 实例化UI(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = NameMgr.画布;
        //实例化UI(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject UI_ = GameObject.Instantiate(对象) as GameObject;
        UI_.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        UI_.transform.localPosition = new Vector3(0, 0, -10);//设置生成位置
        UI_.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        UI_.transform.SetAsFirstSibling();

        //显示按钮

      /*  if (gameObject.tag == "特殊场景")
        {
            UI_.transform.Find("2级画布/IMG_button/关闭背景").gameObject.SetActive(true);
            GameObject 背景 = UI_.transform.Find("combat_bg").gameObject;
            背景.GetComponent<Image>().color = bm.改变透明度(背景, 0);
        }
        else if (gameObject.tag != "城市" && gameObject.tag != "特殊场景")
        {
            UI_.transform.Find("2级画布/IMG_button/关闭天气").gameObject.SetActive(true);
            if (myData.记录 != null && myData.记录.ContainsKey("关闭天气") && myData.记录["关闭天气"].Equals("1"))
            {
                UI_.transform.Find("2级画布/IMG_button/关闭天气/开启").gameObject.SetActive(true);
                UI_.transform.Find("2级画布/IMG_button/关闭天气/关闭").gameObject.SetActive(false);
            }
            else
            {
                UI_.transform.Find("2级画布/IMG_button/关闭天气/开启").gameObject.SetActive(false);
                UI_.transform.Find("2级画布/IMG_button/关闭天气/关闭").gameObject.SetActive(true);
            }
        }
      */


        //添加绑定
        GameObject 背包按钮 = UI_.transform.Find("bag/bag_bg_pic").gameObject;
        GameObject 自动战斗按钮 = UI_.transform.Find("combat_bg/2级画布/自动战斗/Text").gameObject;
        GameObject 角色面板按钮 = UI_.transform.Find("state/head/head_bg").gameObject;//combat_bg
        GameObject 战斗背景 = UI_.transform.Find("combat_bg").gameObject;
        //GameObject 关闭背景按钮 = UI_.transform.Find("2级画布/IMG_button/关闭背景/关闭").gameObject;
       // GameObject 开启背景按钮 = UI_.transform.Find("2级画布/IMG_button/关闭背景/开启").gameObject;
        //GameObject 关闭天气按钮 = UI_.transform.Find("2级画布/IMG_button/关闭天气/关闭").gameObject;
        //GameObject 开启天气按钮 = UI_.transform.Find("2级画布/IMG_button/关闭天气/开启").gameObject;
        GameObject 幻界按钮 = UI_.transform.Find("menu/world/blue_bg_pic").gameObject;
        GameObject 设置按钮 = UI_.transform.Find("menu/setting/blue_bg_pic").gameObject;


        bm.Banding(自动战斗按钮, 自动战斗);
        bm.Banding(背包按钮, 点击背包);
        bm.Banding(角色面板按钮, 生成角色面板);
        bm.Banding(战斗背景, 关闭杂项);
        /*bm.Banding(关闭背景按钮, 关闭背景);
        bm.Banding(开启背景按钮, 开启背景);
        bm.Banding(关闭天气按钮, 关闭天气);
        bm.Banding(开启天气按钮, 开启天气);*/
        bm.Banding(设置按钮, 生成设置界面);

        刷新金钱UI(UI_);
        刷新战斗力UI(UI_);
        加载UI信息(UI_);
    }

    public void 加载UI信息(GameObject UI)
    {
        myData = io_.load();
        UI.transform.Find("战斗页/state/name_bg/name_Text").GetComponent<Text>().text = myData.名字;
        //加载绝招UI
        if (myData.技能槽.ContainsKey("1") && !myData.技能槽["1"].Equals("")) {
            string 绝招名字 = myData.技能槽["1"];
            初始化绝招图标();
        }
        //加载CD道具
        if (myData.记录.ContainsKey("CD道具")) {
            刷新背包CD道具UI(myData.记录["CD道具"]);
        }
        刷新经验条UI(UI);

    }



    public void 跳转至幻界()
    {
        myData = io_.load();
        if (myData.记录.ContainsKey("通关仙路"))
        {
            保留人物血量();
            移走对象();
            gameObject.transform.Find("Main Camera").GetComponent<Camera>().enabled = false;
            if (GameObject.Find("Xing_Camera"))
                GameObject.Find("Xing_Camera").GetComponent<Camera>().enabled = true;
            else
                SceneManager.LoadScene("幻界星空");
        }
        else {
            //生成对话框("通关仙路副本后解锁!",0,0.08f,"点击幻界");
            敬请期待();
        }
    }

    public void 保留人物血量()
    {
        role_Data myData = io_.load();
        combat cbData = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
        myData.剩余血量 = cbData.剩余血量;
        io_.save(myData);
    }


    public void 移动(string ScenceName)
    {
        GameObject 地图 = DataMgr.GetInstance().本地对象["UI"].transform.Find("战斗页").transform.Find("地图").gameObject;
        if (地图.activeSelf)
            地图.SetActive(false);
        if (AdressMgr.GetInstance().检测战斗力是否达标(ScenceName))
        {
            跳转场景(ScenceName);
        }
        else {
            生成对话框("至少需要"+AdressMgr.地图表[ScenceName]+"点战斗力才能进入该地图.",0,0,"进图失败");       
        }

    }



    public void 跳转场景(string ScenceName)
    {
        role_Data myData = io_.load();
        GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
        GameObject 宠物 = GameObject.FindGameObjectWithTag("宠物");
        if (宠物 != null)
        {
            combat cbData = 宠物.GetComponent<combat>();
            cbData.isAttack = false;
            cbData.目标名字 = null;
            cbData.timer = 0;//移动重置普攻
        }
        if (人物 != null)
        {
            combat cbData = 人物.GetComponent<combat>();
            cbData.isAttack = false;
            cbData.目标名字 = null;
            cbData.timer = 0;//移动重置普攻
            myData.剩余血量 = cbData.剩余血量;
            io_.save(myData);
            关闭杂项();
            现有怪物集合.Clear();
            关闭自动攻击();

            GameObject[] 场地组 = GameObject.FindGameObjectsWithTag("场地");
            if (场地组 != null)
            {
                for (int i = 0; i < 场地组.Length; i++)
                {
                    UT_monster utm = 场地组[i].GetComponent<UT_monster>();
                    utm.删除对象();
                }
            }
            dm.储存缓存数据();


            同步跳转(ScenceName);
       
        }
    }


    public IEnumerator 异步跳转(string ScenceName)
    {
        
        SceneManager.LoadSceneAsync(ScenceName);
        yield return async;
    }

    public void 同步跳转(string ScenceName)
    {
            SceneManager.LoadScene(ScenceName);
        
    }

    public void 打开状态页() {
        gameObject.transform.Find("UI/状态页").gameObject.SetActive(true);
    }

    public void 返回战斗页()
    {
        gameObject.transform.Find("UI/状态页").gameObject.SetActive(false);
    }

    private void 加载UI_Other()
    {

        if (GameObject.Find("UI_Other(Clone)"))//如果有UI了就不加载
            return;
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("ui_other", 实例化UI_Other, 参数集));

    }

    private void 实例化UI_Other(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = NameMgr.画布.gameObject;
        //实例化UI(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject UI_ = GameObject.Instantiate(对象) as GameObject;
        UI_.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        UI_.transform.localPosition = new Vector3(0, 0, -10);//设置生成位置
        UI_.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        //UI_.transform.SetAsFirstSibling();


        //添加绑定
        GameObject 战斗按钮 = UI_.transform.Find("menu/combat/blue_bg_pic").gameObject;
        GameObject 设置按钮 = UI_.transform.Find("menu/setting/blue_bg_pic").gameObject;

        bm.Banding(战斗按钮, 跳转至登录场景);
        bm.Banding(设置按钮, 生成设置界面);

    }

    public void 跳转至登录场景()
    {
        role_Data myData = io_.load();
        GameObject.Find("Xing_Camera").GetComponent<Camera>().enabled = false;
        初始化老画布();
        NameMgr.画布.transform.Find("Main Camera").GetComponent<Camera>().enabled = true;
        SceneManager.LoadScene(myData.登录场景);
    }

    public void 初始化老画布()
    {
        GameObject 老画布 = NameMgr.画布;
        老画布.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);//recttransform必不可少的属性(半知半解)
        老画布.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        老画布.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
    }

    private void 加载角色()
    {
        if (GameObject.FindGameObjectWithTag("人物"))//如果有了就不加载
            return;
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("血条", 实例化角色, 参数集));

    }

    private void 实例化角色(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = GameObject.Find("r2").gameObject;
        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 角色 = GameObject.Instantiate(对象) as GameObject;
        角色.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        角色.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        角色.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        角色.tag = "人物";

    }


    public void 扣血显示(GameObject 父物体, int 伤害, string type)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        参数集.Add(1, 伤害);
        参数集.Add(2, type);
        StartCoroutine(bm.LoadABPrefabs("伤害", 实例化扣血, 参数集));

    }

    private void 实例化扣血(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject;
        int 伤害 = (int)参数集[1];
        string type = 参数集[2] + "";
        //实例化扣血文字(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 伤害_ = GameObject.Instantiate(对象) as GameObject;
        if (父物体.CompareTag("boss"))
        {
            父物体 = 父物体.transform.Find("图片").gameObject;
        }
        伤害_.transform.SetParent(父物体.transform, false);//第二个参数可以不用定义许多RectTransform属性
        //伤害_.GetComponent<RectTransform>().sizeDelta = new Vector2(240, 100);//recttransform必不可少的属性(半知半解)
        伤害_.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        伤害_.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        伤害_.transform.GetComponent<Text>().text = 数字增加单位(伤害 + "");
        伤害_.name = type;
    }

    public string 数字增加单位(string 数字) {

        if (数字.IndexOf("万") != -1 || 数字.IndexOf("亿") != -1)
        {
            if (数字.IndexOf("万") != -1) {
                数字.Replace("万", "");
                if (float.Parse(数字) / 10000f >= 1) {
                    数字 = (float.Parse(数字) / 10000).ToString("0.00") + "亿";
                }
            }
        }
        else {
            if (float.Parse(数字) / 100000000 >= 1)
            {
                数字 = (float.Parse(数字) / 100000000).ToString("0.00") + "亿";
            }
            else if (float.Parse(数字) / 10000 >= 1)
            {
                数字 = (float.Parse(数字) / 10000).ToString("0.00") + "万";
            }
        }

        return 数字;

    }


    //储存方式(目前使用)
    public void PlayerPrefs储存(string 键, int 值)
    {
        PlayerPrefs.SetInt(键, 值);
    }
    public void 刷新战斗力UI(GameObject UI)
    {
        myData = io_.load();
        Text 战斗力 = UI.transform.Find("战斗页/state/zdl_bg/zdl_Text").GetComponent<Text>();
        Text 等级 = UI.transform.Find("战斗页/state/grade_bg/grade_Text").GetComponent<Text>();
        cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>(); //加载猪脚的战斗脚本
        战斗力.text = 返回主角战斗力();
        等级.text = bm.Xor(cb.等级);

    }


    public string 返回主角战斗力() {
        cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>(); //加载猪脚的战斗脚本
        double 四维战斗力 = bm.Xstoi(cb.攻击力) * 0.5f * (1.0 + bm.Xstof(cb.攻击力加成) / 100f) + bm.Xstoi(cb.防御力) * 0.5f * (1.0 + bm.Xstof(cb.防御力加成) / 100f) + bm.Xstoi(cb.血量) * 0.05f * (1.0 + bm.Xstof(cb.血量加成) / 100f) + bm.Xstoi(cb.回血值) * 0.05f * (1.0 + bm.Xstof(cb.回血值加成) / 100f);//战斗力基数:攻击*0.5,防御*0.5,血量*0.05
        double 攻击附加战斗力 = bm.Xstoi(cb.攻击力)*0.2f * (1.0 + bm.Xstof(cb.伤害加成)) * (1.0 + bm.Xstof(cb.吸血加成)) * ((bm.Xstof(cb.暴击率) / 100.0f) > 1 ? 1 : (bm.Xstof(cb.暴击率) / 100.0f) + 1) * (1.5f / bm.Xstof(cb.攻击速度_)) * (bm.Xstof(cb.暴伤加成) - 0.5f);
        double 防御附加战斗力 = bm.Xstoi(cb.防御力) * 0.2f * (1.0 + bm.Xstof(cb.伤害减免));
        double 附加真实战斗力 = bm.Xstoi(cb.固定伤害) * 0.75f + bm.Xstoi(cb.固定减伤) * 0.75f + bm.Xstoi(cb.固定吸血) * 1f;
        return 数字增加单位((int)(四维战斗力 +攻击附加战斗力+防御附加战斗力+ 附加真实战斗力) + "");

    }

    public int 返回主角战斗力_无单位()
    {
        cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>(); //加载猪脚的战斗脚本
        double 四维战斗力 = bm.Xstoi(cb.攻击力) * 0.5f * (1.0 + bm.Xstof(cb.攻击力加成) / 100f) + bm.Xstoi(cb.防御力) * 0.5f * (1.0 + bm.Xstof(cb.防御力加成) / 100f) + bm.Xstoi(cb.血量) * 0.05f * (1.0 + bm.Xstof(cb.血量加成) / 100f) + bm.Xstoi(cb.回血值) * 0.05f * (1.0 + bm.Xstof(cb.回血值加成) / 100f);//战斗力基数:攻击*0.5,防御*0.5,血量*0.05
        double 攻击附加战斗力 = bm.Xstoi(cb.攻击力) * 0.2f * (1.0 + bm.Xstof(cb.伤害加成)) * (1.0 + bm.Xstof(cb.吸血加成)) * ((bm.Xstof(cb.暴击率) / 100.0f) > 1 ? 1 : (bm.Xstof(cb.暴击率) / 100.0f) + 1) * (1.5f / bm.Xstof(cb.攻击速度_)) * (bm.Xstof(cb.暴伤加成) - 0.5f);
        double 防御附加战斗力 = bm.Xstoi(cb.防御力) * 0.2f * (1.0 + bm.Xstof(cb.伤害减免));
        double 附加真实战斗力 = bm.Xstoi(cb.固定伤害) * 0.75f + bm.Xstoi(cb.固定减伤) * 0.75f + bm.Xstoi(cb.固定吸血) * 1f;
        return (int)(四维战斗力 + 攻击附加战斗力 + 防御附加战斗力 + 附加真实战斗力);

    }

    public void 刷新经验条UI(GameObject UI)
    {
        myData = io_.load();
        Slider 经验条 = UI.transform.Find("战斗页/experience").GetComponent<Slider>();
        经验条.maxValue = RoleMgr.GetInstance().经验表(bm.Xstoi(myData.等级));
        经验条.value = long.Parse(bm.Xor(myData.当前经验));
        //未完
    }

    public void 刷新经验条UI_不进行IO(GameObject UI,int 经验值)
    {
        //临时数据存储
        if (dm.临时经验与钱币.ContainsKey("经验")) {
            dm.临时经验与钱币["经验"] += 经验值;
        }
        else{
            dm.临时经验与钱币.Add("经验", 经验值);
        }


        //判断是否需要立即存档(条件是经验达到升级标准)
        Slider 经验条 = UI.transform.Find("战斗页/experience").GetComponent<Slider>();
        if (经验条.value + dm.临时经验与钱币["经验"] >= 经验条.maxValue && !myData.限制等级.Equals(myData.等级))
        {
            dm.储存缓存数据();
        }
        else {
            myData.当前经验 = bm.Xitos(bm.Xstoi(myData.当前经验) + 经验值);
        }
        经验条.maxValue = RoleMgr.GetInstance().经验表(bm.Xstoi(myData.等级));
        经验条.value = bm.Xstoi(myData.当前经验);
        //未完
    }





    public void 生成普攻特效(GameObject 父物体, Vector3 目标位置, float 时间)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        参数集.Add(1, 目标位置);
        参数集.Add(2, 时间);
        StartCoroutine(bm.LoadABPrefabs("普攻特效", 实例化普攻特效, 参数集));
    }

    private void 实例化普攻特效(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject; ;
        Vector3 目标位置_世界 = (Vector3)参数集[1];
        float 时间 = (float)参数集[2];

        //实例化气泡(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 子弹 = GameObject.Instantiate(对象) as GameObject;
        子弹.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        子弹.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);//recttransform必不可少的属性(半知半解)
        子弹.transform.localPosition = new Vector2(0, 0);//设置生成位置
        子弹.transform.localScale = new Vector3(0.5f, 0.5f, 1);//设置生成的大小
        子弹.transform.SetAsLastSibling();

        StartCoroutine(bm.MoveTo(子弹.transform, 目标位置_世界, 时间));
    }

    public void 生成宠物普攻特效(GameObject 父物体, Vector3 目标位置)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        参数集.Add(1, 目标位置);
        StartCoroutine(bm.LoadABPrefabs("兽爪特效", 实例化宠物普攻特效, 参数集));
    }

    private void 实例化宠物普攻特效(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject; ;
        Vector3 目标位置_世界 = (Vector3)参数集[1];

        //实例化气泡(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 兽爪 = GameObject.Instantiate(对象) as GameObject;
        兽爪.transform.SetParent(父物体.transform.parent, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        兽爪.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);//recttransform必不可少的属性(半知半解)
        兽爪.transform.position = 目标位置_世界 + new Vector3(-2, 2, 0);//设置生成位置
        兽爪.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        兽爪.transform.SetAsLastSibling();
        StartCoroutine(bm.MoveTo(兽爪.transform, 目标位置_世界 + new Vector3(4, -4, 0), 0.18f));

    }

    public void 生成光球by品质(int qua, GameObject 父物体, Vector3 目标位置, float 时间)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        参数集.Add(1, qua);
        参数集.Add(2, 目标位置);
        参数集.Add(3, 时间);
        StartCoroutine(bm.LoadABPrefabs("光球", 实例化光球, 参数集));
    }



    public void 生成光球by名字(string 名字, GameObject 父物体, Vector3 目标位置, float 时间)
    {

        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        参数集.Add(1, 名字);
        参数集.Add(2, 目标位置);
        参数集.Add(3, 时间);
        参数集.Add(4, "辨别字符串");
        StartCoroutine(bm.LoadABPrefabs("光球", 实例化光球, 参数集));
    }


    private void 实例化光球(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject; ;
        int qua;
        if (参数集.Count == 4)
            qua = (int)参数集[1];
        else
        {
            string 名字 = 参数集[1] as string;
            Prop_bascis 物品 = pm.检索物品(名字);
            if (物品.qua != null)
                qua = bm.Xstoi(物品.qua);
            else
                qua = 0;
        }
        Vector3 目标位置_世界 = (Vector3)参数集[2];

        float 时间 = (float)参数集[3];


        int x轴偏移 =  UnityEngine.Random.Range(-120, 120);
        int y轴偏移 =  UnityEngine.Random.Range(-80, 80);
        //实例化气泡(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 光球 = GameObject.Instantiate(对象) as GameObject;
        光球.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        光球.GetComponent<RectTransform>().sizeDelta = new Vector2(45, 45);//recttransform必不可少的属性(半知半解)
        光球.transform.localPosition = new Vector2(x轴偏移, y轴偏移);//设置生成位置
        光球.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        光球.GetComponent<Image>().color = bm.转换光球颜色(qua, true);//光球外环改变颜色
        GameObject 光球核心 = 光球.transform.Find("光球核心").gameObject;
        光球核心.GetComponent<Image>().color = bm.转换光球颜色(qua, false);//光球核心改变颜色
        光球核心.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);//recttransform必不可少的属性(半知半解)
        光球.transform.SetAsLastSibling();

        StartCoroutine(bm.MoveTo(光球.transform, 目标位置_世界, 时间));
    }

    public void 点击背包()
    {
        if (NameMgr.背包 == null)
        {
            dm.储存缓存数据();
            生成背包();
        }

    }
    public void 生成背包()
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("背包", 实例化背包, 参数集));
    }

    private void 实例化背包(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = NameMgr.画布;
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 背包 = GameObject.Instantiate(对象) as GameObject;
        DataMgr.GetInstance().本地对象的添加("背包", 背包);
        背包.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        背包.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        背包.transform.localPosition = new Vector2(0, 0);//设置生成位置
        背包.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        //绑定点击事件
        Bag bag =NameMgr.背包.GetComponent<Bag>();
        bm.Banding(背包.transform.Find("Panel/标签/道具").gameObject, bag.点击道具页);
        bm.Banding(背包.transform.Find("Panel/标签/装备").gameObject, bag.点击装备页);
        bm.Banding(背包.transform.Find("Panel/标签/关闭").gameObject, bag.关闭背包);
        bm.Banding(背包.transform.Find("Panel/按钮/使用").gameObject, 使用道具);
        bm.Banding(背包.transform.Find("Panel/按钮/查看").gameObject, 生成物品信息_无参);



    }



    public void 生成物品项(int num, Prop_bascis 物品, int 数量)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, num);
        参数集.Add(1, 物品);
        参数集.Add(2, 数量);
        StartCoroutine(bm.LoadABPrefabs("物品项", 实例化物品项, 参数集));
    }


    private void 实例化物品项(GameObject 对象, Dictionary<int, object> 参数集)
    {
        int num = (int)参数集[0];
        Prop_bascis 物品 = 参数集[1] as Prop_bascis;
        int 数量 = (int)参数集[2];

        GameObject 父物体 = GameObject.Find("Bag_Content");

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 物品项 = GameObject.Instantiate(对象) as GameObject;
        物品项.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        物品项.GetComponent<RectTransform>().sizeDelta = new Vector2(570, 80);//recttransform必不可少的属性(半知半解)
        物品项.transform.localPosition = new Vector2(285, -80 * num + 40);//设置生成位置
        物品项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        物品项.name = 物品.name;
        Text 名字文本 = 物品项.transform.Find("名字").GetComponent<Text>();
        名字文本.text = 物品.name;
        名字文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
        物品项.transform.Find("数量").GetComponent<Text>().text = 数量 + "";
        Image 图标 = 物品项.transform.Find("图标").GetComponent<Image>();
        初始化图标(图标, 物品);
        Image 锁图标 = 物品项.transform.Find("锁").GetComponent<Image>();
        if (物品.islock.Equals("0"))
            锁图标.sprite = Resources.Load("图标/开锁图标", typeof(Sprite)) as Sprite;
        else
            锁图标.sprite = Resources.Load("图标/关锁图标", typeof(Sprite)) as Sprite;

        Bag_PropOption bpo = 物品项.GetComponent<Bag_PropOption>();
        bpo.名字 = 物品.name;
        bpo.地址 = bm.获取对象地址(物品);

    }

    public void 生成装备项(int num, string UID,role_Data MyData)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, num);
        参数集.Add(1, UID);
        参数集.Add(2, MyData);
        StartCoroutine(bm.LoadABPrefabs("物品项", 实例化装备项, 参数集));
    }

    private void 实例化装备项(GameObject 对象, Dictionary<int, object> 参数集)
    {
        role_Data myData = 参数集[2] as role_Data;
        int num = (int)参数集[0];
        string UID = 参数集[1] +"";
        Equipment 装备 = myData.装备背包[UID];

        GameObject 父物体 = GameObject.Find("Bag_Content");

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 装备项 = GameObject.Instantiate(对象) as GameObject;
        装备项.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        装备项.GetComponent<RectTransform>().sizeDelta = new Vector2(570, 80);//recttransform必不可少的属性(半知半解)
        装备项.transform.localPosition = new Vector2(285, -80 * num + 40);//设置生成位置
        装备项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        装备项.name = UID;
        Text 名字文本 = 装备项.transform.Find("名字").GetComponent<Text>();
        名字文本.text = 装备.name;
        名字文本.color = bm.转换颜色(bm.Xstoi(装备.qua));
        Text 强化等级 = 名字文本.gameObject.transform.Find("等级").GetComponent<Text>();  
            if (bm.Xstoi(装备.lv) != 0)
            {
                强化等级.gameObject.SetActive(true);
                强化等级.text = "+" + bm.Xstoi(装备.lv);
            }
            else
            {
                强化等级.gameObject.SetActive(false);
            }
        强化等级.color = bm.转换颜色(bm.Xstoi(装备.qua));
        Text 等级文本 = 装备项.transform.Find("数量").GetComponent<Text>();
        等级文本.text =bm.Xstoi( 装备.lessgrade)+"";
        if (bm.Xstoi(myData.等级) >= bm.Xstoi(装备.lessgrade))
        {
            等级文本.color = bm.转换颜色(1);
        }
        else {
            等级文本.color = bm.转换颜色(5);
        }
        Image 图标 = 装备项.transform.Find("图标").GetComponent<Image>();
        初始化图标(图标, 装备);
        Image 锁图标 = 装备项.transform.Find("锁").GetComponent<Image>();
        if (装备.islock.Equals("0"))
            锁图标.sprite = Resources.Load("图标/开锁图标", typeof(Sprite)) as Sprite;
        else
            锁图标.sprite = Resources.Load("图标/关锁图标", typeof(Sprite)) as Sprite;

        Bag_PropOption bpo = 装备项.GetComponent<Bag_PropOption>();
        bpo.名字 = 装备.name;
        bpo.UID = UID;
    }



    public void 生成获得框(String 物品名,int 数量)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 物品名);
        参数集.Add(1, 数量);
        StartCoroutine(bm.LoadABPrefabs("获得框", 实例化获得框, 参数集));
    }

    private void 实例化获得框(GameObject 对象, Dictionary<int, object> 参数集)
    {
        string 物品名 = 参数集[0] + "";
        int 数量 = int.Parse( 参数集[1]+"");
        Prop_bascis 物品 = pm.检索物品(物品名);
        GameObject 父物体 = NameMgr.画布;
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 获得框 = GameObject.Instantiate(对象) as GameObject;
        获得框.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        获得框.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 100);//recttransform必不可少的属性(半知半解)
        获得框.transform.localPosition = new Vector2(0, 0);//设置生成位置
        获得框.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        Text 文本 = 获得框.transform.Find("名字").GetComponent<Text>();
        if (物品.name != null)
        {
            文本.text = 物品.name+"X"+数量;
            文本.color = bm.转换颜色(bm.Xstoi(物品.qua));
        }
        else {
            文本.text = 物品名 + "X" + 数量;
            文本.color = bm.转换颜色(-1);
        }
        获得框.transform.SetAsLastSibling();
    }


    public void 生成事件框()
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("事件框", 实例化事件框, 参数集));
    }

    private void 实例化事件框(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = NameMgr.画布;
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 事件框 = GameObject.Instantiate(对象) as GameObject;
        事件框.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        事件框.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 200);//recttransform必不可少的属性(半知半解)
        事件框.transform.localPosition = new Vector2(0, 400);//设置生成位置
        事件框.transform.localScale = new Vector3(1f, 1f, 1f);//设置生成的大小
        事件框.transform.SetAsLastSibling();
    }

    public void 生成获得提示(List<Prop_bascis> 物品集合, string type)
    {
        //移动端传list<T> T为引用数据会丢失数据,需要泛型类来存储数据
        ListClass<Prop_bascis> 列表 = new ListClass<Prop_bascis>();
        for (int i = 0; i < 物品集合.Count; i++)
        {
            列表.Add(物品集合[i]);
        }

        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 列表);
        参数集.Add(1, type);
        StartCoroutine(bm.LoadABPrefabs("获得提示", 实例化获得提示, 参数集));
    }
    private void 实例化获得提示(GameObject 对象, Dictionary<int, object> 参数集)
    {
        ListClass<Prop_bascis> 列表 = 参数集[0] as ListClass<Prop_bascis>;
        string type = 参数集[1] + "";
        List<Prop_bascis> 物品集合 = 列表.Get();
        GameObject 父物体;
        if (type.Equals("背包"))
            父物体 = NameMgr.背包.gameObject;
        else
            父物体 = GameObject.Find("物品栏").gameObject;


        StartCoroutine(获得提示移动(对象, 父物体, 1.2f, 物品集合));

       

    }


    public IEnumerator 获得提示移动(GameObject 对象,GameObject 父物体, float 用时,List<Prop_bascis> 物品集合)
    {
        int i = 0;
        foreach (Prop_bascis 物品信息 in 物品集合)
        {
            //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
            GameObject 获得提示 = GameObject.Instantiate(对象) as GameObject;
            获得提示.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
            获得提示.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 62);//recttransform必不可少的属性(半知半解)
            获得提示.transform.localPosition = new Vector2(0, -100);//设置生成位置
            获得提示.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
            Text 文本 = 获得提示.transform.Find("物品名").GetComponent<Text>();
            文本.text = 物品信息.name;
            文本.color = bm.转换颜色(bm.Xstoi(物品信息.qua));
            获得提示.transform.SetAsLastSibling();
            i++; //物品计数
            StartCoroutine(bm.MoveTo(获得提示.transform, 父物体.transform.position + new Vector3(0, 30, 0), 用时));
            yield return new WaitForSeconds(0.2f);

        }

        
    }

    public void 自动战斗()
    {
        if (isAuto)
            关闭自动攻击();
        else
            开启自动战斗();
    }

    public void 关闭自动攻击()
    {
        Color nowColor;
        GameObject 自动按钮 = GameObject.Find("自动战斗");
        string 按钮文字 = "";
        GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
        if (人物 != null)
        {
            cb = 人物.GetComponent<combat>(); //加载猪脚的战斗脚本
            GameObject 宠物 = GameObject.FindGameObjectWithTag("宠物");
            combat petcb = null;
            if (宠物 != null)
            {
                petcb = 宠物.GetComponent<combat>(); //加载猪脚宠物的战斗脚本
                petcb.isAttack = false;
                petcb.目标名字 = "";
            }
            isAuto = false;
            cb.isAttack = false;
            if (现有怪物集合.Count == 0)
                cb.目标名字 = "";

        }
        nowColor = new Color(28 / 255f, 219 / 255f, 211 / 255f, 255f / 255f);
        按钮文字 = "开启自动战斗";
        自动按钮.GetComponent<Image>().color = nowColor;
        自动按钮.transform.Find("Text").GetComponent<Text>().text = 按钮文字;
    }

    public void 开启自动战斗()
    {
        Color nowColor;
        GameObject 自动按钮 = GameObject.Find("自动战斗");
        string 按钮文字 = "";
        isAuto = true;
        人物自动攻击();
        nowColor = new Color(246 / 255f, 128 / 255f, 100 / 255f, 255f / 255f);
        按钮文字 = "自动战斗中...";
        自动按钮.GetComponent<Image>().color = nowColor;
        自动按钮.transform.Find("Text").GetComponent<Text>().text = 按钮文字;
    }

    public void 人物自动攻击()
    {
        GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
        if (人物 != null)
        {
            cb = 人物.GetComponent<combat>(); //加载猪脚的战斗脚本
            自动攻击(cb);
            GameObject 宠物 = GameObject.FindGameObjectWithTag("宠物");
            if (宠物 != null) {
                宠物.GetComponent<combat>().开启战斗();
            }
        }
    }

    public void 自动攻击(combat cb) {

        if (cb.仇恨列表.Count != 0)
        {
            随机攻击怪物(cb);
        }
        else
        {
            GameObject 怪物 = mmgr.随意寻找一个目标("怪物");
            if (怪物 != null)
            {
                cb.目标名字 = 怪物.name;
                cb.开启战斗();
                combat mcb = 怪物.GetComponent<combat>();
                mcb.开启战斗();
            }
        }

        /*if (bm.Xstof(cb.攻击速度_) > 0.5f)
            cb.timer = bm.Xstof(cb.攻击速度_);
        else
            cb.timer = 0.5f;//攻击间隔最低为0.5f*/
        cb.timer = bm.Xstof(cb.攻击速度_);
    }


    public void 回血(combat cb,int 回血值) {
        cb.剩余血量 = bm.Xitos(bm.Xstoi(cb.剩余血量) + 回血值);
        /*HPReduce HpR = gameObject.GetComponent<HPReduce>();
        HpR.AddHPEffect();*/
        扣血显示(cb.gameObject.transform.parent.gameObject, 回血值, "回血");
    }

    public void 随机攻击怪物(combat cb) {
        int 随机数 =  UnityEngine.Random.Range(0, cb.仇恨列表.Count);
        List<string> names = new List<string>();
        foreach (string name in cb.仇恨列表.Keys)
        {
            names.Add(name);
        }
        cb.目标名字 = names[随机数];
        cb.开启战斗();
        GameObject 目标 = GameObject.Find(cb.目标名字);
        if (目标 != null) {
            combat mscb = 目标.GetComponent<combat>();
            mscb.开启战斗();
        }
    }



    public void 使用道具()
    {
        UseEqm useEqm = NameMgr.背包.GetComponent<UseEqm>();
        string str = useEqm.使用_Data();
        if(!str.Equals("null"))
            生成警告框(str);

    }

    public void 生成警告框(string str)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, str);
        StartCoroutine(bm.LoadABPrefabs("警告框", 实例化警告框, 参数集));
    }
    private void 实例化警告框(GameObject 对象, Dictionary<int, object> 参数集)
    {
        string str = 参数集[0] as string;
        GameObject 父物体 = NameMgr.画布;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 警告框 = GameObject.Instantiate(对象) as GameObject;
        警告框.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        警告框.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        警告框.transform.localPosition = new Vector2(0, 0);//设置生成位置
        警告框.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        Text 文本 = 警告框.transform.Find("警告面板/文字").GetComponent<Text>();
        Image 面板 = 警告框.transform.Find("警告面板").GetComponent<Image>();
        文本.color = bm.转换颜色(5);//不透明红色
        面板.color = bm.转换光球颜色(5, true);//较透明的红色
        if (str.Equals("未选中"))
            文本.text = "未选中";
        else if (str.Equals("等级不足"))
            文本.text = "等级不足";
        else if (str.Contains("成功"))
        {
            文本.text = str;
            文本.color = bm.转换颜色(1);//不透明绿色
            面板.color = bm.转换光球颜色(1, true);//较透明的绿色
        }
        else
        {
            文本.text = str;
        }


        警告框.transform.SetAsLastSibling();
        StartCoroutine(bm.MoveTo(警告框.transform, 父物体.transform.position + new Vector3(0, 20, 0), 2f));

    }

    public void 生成角色面板()
    {
        if (gameObject.transform.Find("属性面板(Clone)"))
        {
            gameObject.transform.Find("属性面板(Clone)").gameObject.SetActive(true);
            return;
        }
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("属性面板", 实例化角色面板, 参数集));

    }
    private void 实例化角色面板(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = NameMgr.画布;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 角色面板 = GameObject.Instantiate(对象) as GameObject;
        角色面板.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        角色面板.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);//recttransform必不可少的属性(半知半解)
        角色面板.transform.localPosition = new Vector2(-92, 208);//设置生成位置
        角色面板.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        角色面板.transform.SetAsLastSibling();
        面板属性刷新(角色面板);//信息填充-属性
        if (DataMgr.GetInstance().本地对象.ContainsKey("属性面板"))
        {
            DataMgr.GetInstance().本地对象["属性面板"] = 角色面板;
        }
        else
        {
            DataMgr.GetInstance().本地对象.Add("属性面板", 角色面板);
        }
    }

    public void 面板属性刷新(GameObject 角色面板)
    {
        dm.储存缓存数据();
        Text 等级 = 角色面板.transform.Find("bg/尾部/属性展示/等级/Text").GetComponent<Text>();
        Text 灵根 = 角色面板.transform.Find("bg/尾部/属性展示/灵根/Text").GetComponent<Text>();
        Text 攻击力 = 角色面板.transform.Find("bg/尾部/属性展示/攻击力/Text").GetComponent<Text>();
        Text 防御力 = 角色面板.transform.Find("bg/尾部/属性展示/防御力/Text").GetComponent<Text>();
        Text 生命值 = 角色面板.transform.Find("bg/尾部/属性展示/生命值/Text").GetComponent<Text>();
        Text 回血值 = 角色面板.transform.Find("bg/尾部/属性展示/回血量/Text").GetComponent<Text>();
        Text 攻击速度 = 角色面板.transform.Find("bg/尾部/属性展示/攻击速度/Text").GetComponent<Text>();
        Text 暴击率 = 角色面板.transform.Find("bg/尾部/属性展示/暴击率/Text").GetComponent<Text>();
        Text 固定吸血 = 角色面板.transform.Find("bg/尾部/属性展示/固定吸血/Text").GetComponent<Text>();
        Text 吸血加成 = 角色面板.transform.Find("bg/尾部/属性展示/百分比吸血/Text").GetComponent<Text>();
        Text 伤害加成 = 角色面板.transform.Find("bg/尾部/属性展示/伤害加成/Text").GetComponent<Text>();
        Text 固定伤害 = 角色面板.transform.Find("bg/尾部/属性展示/固定伤害/Text").GetComponent<Text>();
        Text 固定减伤 = 角色面板.transform.Find("bg/尾部/属性展示/固定减伤/Text").GetComponent<Text>();
        Text 伤害减免 = 角色面板.transform.Find("bg/尾部/属性展示/伤害减免/Text").GetComponent<Text>();
        Text 金钱爆率 = 角色面板.transform.Find("bg/尾部/属性展示/金钱爆率/Text").GetComponent<Text>();
        Text 经验爆率 = 角色面板.transform.Find("bg/尾部/属性展示/经验爆率/Text").GetComponent<Text>();
        Text 现有套装 = 角色面板.transform.Find("bg/中部/装备页/套装显示/生效的套装").GetComponent<Text>();
        Text 经验值 = 角色面板.transform.Find("bg/头部/经验/经验值").GetComponent<Text>();
        Text 战斗力 = 角色面板.transform.Find("bg/头部/战斗力/战斗力").GetComponent<Text>();
        Text 名字 = 角色面板.transform.Find("bg/头部/名字/name").GetComponent<Text>();



        role_Data myData = io_.load();
        经验值.text = 数字增加单位(bm.Xstol(myData.当前经验)+"") + "<color=black>" + " / " + 数字增加单位(RoleMgr.GetInstance().经验表(bm.Xstoi(myData.等级))+"") + "</color>";
        战斗力.text = 返回主角战斗力();
        名字.text = myData.名字;
        string Str_suit = "";
        foreach (string 套装名 in PropMgr.GetInstance().装备套装信息.Keys)
        {
            if (PropMgr.GetInstance().装备套装信息[套装名] >= 2)
            {
                suit_Data suData = PropMgr.套装表[套装名];
                if (suData.s_qua.Equals("0"))
                    Str_suit += "<color=black>" + suData.s_name + "</color>\n";
                else if (suData.s_qua.Equals("1"))
                    Str_suit += "<color=green>" + suData.s_name + "</color>\n";
                else if (suData.s_qua.Equals("2"))
                    Str_suit += "<color=blue>" + suData.s_name + "</color>\n";
                else if (suData.s_qua.Equals("3"))
                    Str_suit += "<color=purple>" + suData.s_name + "</color>\n";
                else if (suData.s_qua.Equals("4"))
                    Str_suit += "<color=yellow>" + suData.s_name + "</color>\n";
                else if (suData.s_qua.Equals("5"))
                    Str_suit += "<color=red>" + suData.s_name + "</color>\n";
            }
        }
        现有套装.text = Str_suit;
        GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
        if (!gameObject.CompareTag("幻界") && 人物 != null)
        {
            cb = 人物.GetComponent<combat>(); //加载猪脚的战斗脚本
            cb.人物属性刷新();
            等级.text = bm.Xstoi(cb.等级) + " <color=blue>  / " + bm.Xstoi(myData.限制等级) + "</color>";
            灵根.text = bm.Xstoi(myData.灵根) + "";
            攻击力.text = 数字增加单位(bm.Xstoi(cb.攻击力) + "");
            防御力.text = 数字增加单位(bm.Xstoi(cb.防御力) + "");
            生命值.text = 数字增加单位(bm.Xstoi(cb.血量) + "");
            回血值.text = 数字增加单位(bm.Xstoi(cb.回血值) + "");
            攻击速度.text = Math.Round(1f/bm.Xstof(cb.攻击速度_), 2)+"" ;//按照四舍五入的国际标准 + "";       
            暴击率.text = bm.Xstoi(cb.暴击率) + "%";
            固定吸血.text = 数字增加单位(bm.Xstoi(cb.固定吸血) + "");
            吸血加成.text =  bm.Xstof(cb.吸血加成) * 100 + "%";
            伤害加成.text = (bm.Xstof(cb.伤害加成) - 1) * 100 + "%";
            伤害减免.text = bm.Xstof(cb.伤害减免) * 100 + "%";
            金钱爆率.text = bm.Xstof(cb.金钱加成) * 100 + "%";
            经验爆率.text = bm.Xstof(cb.经验加成) * 100 + "%";
            固定伤害.text = 数字增加单位(bm.Xstoi(cb.固定伤害) + "");
            固定减伤.text = 数字增加单位(bm.Xstoi(cb.固定减伤) + "");
        }

    }



    public void 生成天气()
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("天气", 实例化天气, 参数集));
    }
    private void 实例化天气(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = GameObject.Find("combat_prefabs_UIjia");//bug
        if (父物体 == null)
            父物体 = GameObject.Find("combat(Clone)");

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 天气 = GameObject.Instantiate(对象) as GameObject;
        天气.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        天气.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 910);//recttransform必不可少的属性(半知半解)
        天气.transform.localPosition = new Vector2(0, 0);//设置生成位置
        天气.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        天气.transform.SetAsLastSibling();
    }


    public void 生成物品信息_无参()
    {
        if (GameObject.FindGameObjectWithTag("选中"))//有选中目标
        {
            GameObject 选中项 = GameObject.FindGameObjectWithTag("选中");
            string 键 = 选中项.name;
            if (PropMgr.材料表.ContainsKey(键))//物品为道具
            {
                Prop_bascis 物品 = pm.检索物品(选中项.transform.Find("名字").GetComponent<Text>().text);
                生成物品信息(物品, 0, 键);
            }
            else if (PropMgr.装备表.ContainsKey(键)) {//物品为打造装备的名字
                myData = io_.load();
                Equipment 装备 =(Equipment) pm.检索物品(选中项.transform.Find("名字").GetComponent<Text>().text);
                if (myData.装备槽[装备.place] != null)
                {
                    Equipment 比对的装备 = myData.装备槽[装备.place];
                    生成物品信息(比对的装备, 1, 装备.place);
                    生成物品信息(装备, 2, 键);
                    return;
                }
                生成物品信息(装备, 0, 键);
            }
            else//物品为背包里的装备
            {
                myData = io_.load();
                Equipment 装备 = myData.装备背包[选中项.name];
                if (myData.装备槽[装备.place] != null)
                {
                    Equipment 比对的装备 = myData.装备槽[装备.place];
                    生成物品信息(比对的装备, 1, 装备.place);
                    生成物品信息(装备, 2, 键);
                    return;
                }
                生成物品信息(装备, 0, 键);
            }
           
            
        }
        else  //无选中,生成警告框
            生成警告框("未选中");

    }


    public void 生成技能信息_无参()
    {
        if (GameObject.FindGameObjectWithTag("选中"))//有选中目标
        {
            string name = GameObject.FindGameObjectWithTag("选中").transform.Find("name").GetComponent<Text>().text;
            SkillData sd = pm.检索技能(name);
            生成技能信息(sd, 1);
        }
        else  //无选中,生成警告框
            生成警告框("未选中");

    }

    public void 生成技能信息(SkillData sd, int 状态)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, sd);
        参数集.Add(1, 状态);
        StartCoroutine(bm.LoadABPrefabs("技能信息界面", 实例化技能信息, 参数集));
    }

    private void 实例化技能信息(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体;
        SkillData sd = 参数集[0] as SkillData;
        int type = (int)参数集[1];
        if (type == 0)
            父物体 = GameObject.Find("技能背包(Clone)").gameObject;
        else if (type == 1)
            父物体 = GameObject.Find("道具界面(Clone)").gameObject;
        else
            父物体 = NameMgr.画布;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能信息 = GameObject.Instantiate(对象) as GameObject;
        技能信息.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能信息.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 520);//recttransform必不可少的属性(半知半解)
        if (type == 1)//1为中心
            技能信息.transform.localPosition = new Vector2(0, 100);//设置生成位置
        else//0为旁边
            技能信息.transform.localPosition = new Vector2(300, -67.5f);//设置生成位置
        技能信息.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        技能信息.transform.SetAsLastSibling();

        //信息填充
        //名字
        Text name = 技能信息.transform.Find("信息/name").GetComponent<Text>();
        name.text = sd.name;
        name.color = bm.转换颜色(bm.Xstoi(sd.qua));
        //星
        技能信息.transform.Find("信息/xing").GetComponent<Text>().text = bm.Xor(sd.xing);
        //LV
        技能信息.transform.Find("信息/LV").GetComponent<Text>().text = bm.Xor(sd.lessgrade) + "";
        //冷却
        技能信息.transform.Find("信息/Text_CD").GetComponent<Text>().text = bm.Xor(sd.cd);
        //位置
        技能信息.transform.Find("信息/Text_Place").GetComponent<Text>().text = sd.place + "";
        //通用
        技能信息.transform.Find("信息/Text_Type").GetComponent<Text>().text = sd.type + "";
        //效果介绍
        Text comment = 技能信息.transform.Find("信息/Scroll View/Viewport/Content/Text").GetComponent<Text>();
        comment.text = sd.comment;

        if (type == 0)
        {
            技能信息.transform.Find("遮挡").gameObject.SetActive(false);
        }
        else
        {
            //绑定事件
            bm.Banding(技能信息.transform.Find("遮挡").gameObject, 关闭杂项);
        }
    }


    public void 生成物品信息(Prop_bascis 物品, int 状态,string 键)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 物品);
        参数集.Add(1, 状态);
        参数集.Add(2, 键);
        StartCoroutine(bm.LoadABPrefabs("物品信息", 实例化物品信息, 参数集));
    }

    private void 实例化物品信息(GameObject 对象, Dictionary<int, object> 参数集)
    {
        Prop_bascis 物品 = 参数集[0] as Prop_bascis;
        int 状态 = (int)参数集[1];
        string 键 = 参数集[2] + "";
        GameObject 父物体 = NameMgr.画布;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 信息框 = GameObject.Instantiate(对象) as GameObject;
        信息框.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        信息框.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        if (状态 == 0)
        {
            信息框.transform.localPosition = new Vector2(0, 0);//设置生成位置
            if (PropMgr.GetInstance().主属性表.ContainsKey(键)) {
                信息框.transform.Find("主面板/边框/遮罩面板").gameObject.transform.Find("已装备遮罩").gameObject.SetActive(true);
            }
        }
        else if (状态 == 1)
        {
            信息框.transform.localPosition = new Vector2(-230, 0);//设置生成位置
            信息框.transform.Find("主面板/边框/遮罩面板").gameObject.transform.Find("已装备遮罩").gameObject.SetActive(true);
        }
        else if (状态 == 2)
            信息框.transform.localPosition = new Vector2(230, 0);//设置生成位置
        信息框.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        信息框.transform.SetAsLastSibling();

        //绑定点击事件
        bm.Banding(信息框.transform.Find("bt_Panel").gameObject, 关闭杂项);


        信息框.GetComponent<EquipmentDate>().Key = 键;

        //信息填充-名字及其颜色
        GameObject 基本属性 = 信息框.transform.Find("主面板/边框/基本属性").gameObject;
        Text name = 基本属性.transform.Find("名字/名字文本").GetComponent<Text>();
        //文本集合.Add(name);
        name.text = 物品.name;
        name.color = bm.转换颜色(bm.Xstoi(物品.qua));

        //贴图
        Image 图标 = 基本属性.transform.Find("图标").GetComponent<Image>();
        初始化图标(图标, 物品);

        //星级
        /* Text xing = 信息框.transform.Find("主面板/panel_up/Xing/Text").GetComponent<Text>();
         xing.text = bm.Xor(物品.xing);
         //是否绑定/可以交易
         Text bang = 信息框.transform.Find("主面板/panel_up/Bang").GetComponent<Text>();
         if (物品.isbang.Equals("0"))
         {
             bang.text = "可交易";
             bang.color = bm.转换颜色(1);
         }
         else
         {
             bang.text = "不可交易";
             bang.color = bm.转换颜色(5);
         }*/
        //最低使用等级
        Text lv = 基本属性.transform.Find("最低等级/最低等级文本").GetComponent<Text>();
        //文本集合.Add(lv);
        lv.text ="Lv:1";

        //装备面板和道具面板
        GameObject 装备面板= 信息框.transform.Find("主面板/边框/装备面板").gameObject;
        GameObject 道具面板= 信息框.transform.Find("主面板/边框/道具面板").gameObject;

        if (物品.type.Equals("3"))//物品是装备
        {
            装备面板.SetActive(true);
            道具面板.SetActive(false);
            Equipment 装备 = (Equipment)物品;
            Text 强化等级 = 基本属性.transform.Find("强化等级/强化等级文本").GetComponent<Text>();
            if (bm.Xstoi(装备.lv) != 0)
            {
                强化等级.transform.parent.gameObject.SetActive(true);
                强化等级.text = "+" + bm.Xstoi(装备.lv);
            }
            else {
                强化等级.transform.parent.gameObject.SetActive(false);
            }
            强化等级.color = bm.转换颜色(bm.Xstoi(物品.qua));
            lv.text = "Lv:" + bm.Xor(装备.lessgrade);
            if (bm.Xstoi(装备.lessgrade) > bm.Xstoi(myData.等级))
            {
                Color nowColor;
                ColorUtility.TryParseHtmlString("#DD3737", out nowColor);
                lv.color = nowColor;
            }
            //显示各种属性
            Text 主属性文本 = 装备面板.transform.Find("主属性值/主属性值文本").GetComponent<Text>();
            //文本集合.Add(主属性文本);
            if (bm.Xstoi(装备.lv) < int.Parse(PropMgr.GetInstance().颜色等级上限[bm.Xstoi(装备.qua) + ""]))
            {

                主属性文本.text = PropMgr.GetInstance().主属性表[装备.place] + bm.Xstoi(装备.head_attribute_num) + "<color=green>  (可强化)</color>";
            }
            else {
                主属性文本.text = PropMgr.GetInstance().主属性表[装备.place] + bm.Xstoi(装备.head_attribute_num) + "<color=red>  (已满级)</color>";
            }
            GameObject 副属性 = 装备面板.transform.Find("副属性值/副属性值文本").gameObject;
            //副属性
            if (装备.next_attribute != null && !装备.next_attribute.Equals(""))
            {
                副属性.transform.parent.gameObject.SetActive(true);
                Text 副属性文本 = 副属性.GetComponent<Text>();
                //文本集合.Add(副属性文本);
                副属性文本.text = 装备.next_attribute + bm.Xstoi(装备.next_num);
            }
            else {
                副属性.transform.parent.gameObject.SetActive(false);
            }

            GameObject 额外属性 = 装备面板.transform.Find("额外属性值/额外属性值文本").gameObject;
            //额外属性
            if (装备.extar_attribute != null && 装备.extar_attribute.Count > 0)
            {
                额外属性.transform.parent.gameObject.SetActive(true);
                Text 额外属性文本 = 额外属性.GetComponent<Text>();
                if (!PropMgr.装备表.ContainsKey(键)) {//已有装备
                    额外属性文本.text = "";
                    int index = 0;
                    //文本集合.Add(额外属性文本);
                    foreach (string 属性名 in 装备.extar_attribute.Keys)
                    {
                        if (index == 0)
                        {
                            额外属性文本.text += 属性名 + bm.Xstoi(装备.extar_attribute[属性名]);
                        }
                        else
                        {
                            额外属性文本.text += "\n" + 属性名 + bm.Xstoi(装备.extar_attribute[属性名]);
                        }
                        index++;
                    }
                }
                else { 
                    额外属性文本.text = "随机生成"+ 装备.extar_attribute.Count+"条额外属性";
                }
            }
            else {
                额外属性.transform.parent.gameObject.SetActive(false);
            }


            if (!装备.tao.Equals(""))//显示套装信息
            {
                装备面板.transform.Find("套装").gameObject.SetActive(true);
                装备面板.transform.Find("套装基本信息").gameObject.SetActive(true);
                装备面板.transform.Find("套装属性信息").gameObject.SetActive(true);
                Text 套装名字文本 = 装备面板.transform.Find("套装基本信息/套装基本信息文本").GetComponent<Text>();
                //文本集合.Add(套装名字文本);
                套装名字文本.gameObject.SetActive(true);
                Text 套装属性文本 = 装备面板.transform.Find("套装属性信息/套装属性信息文本").GetComponent<Text>();
                //文本集合.Add(套装属性文本);
                套装属性文本.gameObject.SetActive(true);
                //信息填写
                suit_Data suData = PropMgr.套装表[装备.tao];
                /*信息框.transform.Find("额外面板/套装_name").GetComponent<Text>().text = "<" + suData.s_name + ">";
                信息框.transform.Find("额外面板/套装_num").GetComponent<Text>().text = "(" + suData.s_index + "/" + suData.s_total + ")";*/
                套装名字文本.text = "<" + suData.s_name + "> " + "(" + suData.s_total + "/" + suData.s_index + ")";
                string 效果文字 = 返回套装效果的文字描述(suData);
                /*信息框.transform.Find("额外面板/效果").GetComponent<Text>().text = 效果文字;
                int 文字大小 = 80;
                if (int.Parse(suData.s_total) > 2)
                    文字大小 = 80 - (int.Parse(suData.s_total) - 2) * 3;
                信息框.transform.Find("额外面板/效果").GetComponent<Text>().fontSize = 文字大小;*/
                套装属性文本.text = 效果文字;
            }
            else {
                装备面板.transform.Find("套装").gameObject.SetActive(false);
                装备面板.transform.Find("套装基本信息").gameObject.SetActive(false);
                装备面板.transform.Find("套装属性信息").gameObject.SetActive(false);
            }
            //显示按钮并绑定事件
            GameObject 各种按钮 = 装备面板.transform.Find("按钮").gameObject;
            if (!PropMgr.装备表.ContainsKey(键))
            {
                GameObject 穿戴按钮 = 各种按钮.transform.Find("穿戴").gameObject;
                if (PropMgr.GetInstance().主属性表.ContainsKey(键)) {
                    穿戴按钮.transform.Find("穿戴/穿戴文本").GetComponent<Text>().text = "卸下";
                }
                各种按钮.SetActive(true);

            }
            else {
                各种按钮.SetActive(false);
            }

        }
        //物品为道具
        else
        {
            装备面板.SetActive(false);
            道具面板.SetActive(true);
            //效果/属性
            Text fun = 道具面板.transform.Find("道具效果/道具效果文本").GetComponent<Text>();
            //文本集合.Add(fun);
            if (物品.type.Equals("1") && 物品.fun.Equals(""))
                fun.text = "材料/收集品\n" + "出售单价为" + bm.Xstoi(物品.price);
            else if (物品.type.Equals("2"))
                fun.text = "使用效果:" + 物品.fun;     
            else
            {
                fun.text = 物品.fun;
            }
        }
        /*
         Dictionary<string, string> 装备属性 = pm.提取装备属性(装备);
            string 属性文本 = "";
            foreach (string 属性词条 in 装备属性.Keys)
            {
                属性文本 += 属性词条 + bm.Xstoi(装备属性[属性词条]) + "\n";
            }
            fun.text = 属性文本;
         */



        //描述
        if (物品.comment != null && !物品.comment.Equals(""))
        {
            Text comment = 信息框.transform.Find("主面板/边框/简介/简介文本").GetComponent<Text>();
            //文本集合.Add(comment);
            comment.text = 物品.comment;
        }
        // Debug.Log(物品.comment+物品.name);

        

        //手动绑定材质
        /*for (int i = 0; i < 文本集合.Count; i++)
        {
            文本集合[i].fontSharedMaterial = new Material(Shader.Find("TextMeshPro/Bitmap"));
            文本集合[i].font = (TMP_FontAsset)Resources.Load<TMP_FontAsset>("Font/SanJi SDF");
            
        }*/
    }





    public  void 生成物品信息2(Prop_bascis 物品, int 状态)
    {
      
        GameObject 父物体 = NameMgr.画布;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 信息框 = Resources.Load<GameObject>("部件/物品信息");
        //信息框.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        信息框.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        if (状态 == 0)
            信息框.transform.localPosition = new Vector2(0, 0);//设置生成位置
        else if (状态 == 1)
        {
            信息框.transform.localPosition = new Vector2(-230, 0);//设置生成位置
            信息框.transform.Find("已装备遮罩").gameObject.SetActive(true);
        }
        else if (状态 == 2)
            信息框.transform.localPosition = new Vector2(230, 0);//设置生成位置
        信息框.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        信息框.transform.SetAsLastSibling();

        //绑定点击事件
        bm.Banding(信息框.transform.Find("bt_Panel").gameObject, 关闭杂项);



        List<TMP_Text> 文本集合 = new List<TMP_Text>();

        //信息填充-名字及其颜色
        GameObject 基本属性 = 信息框.transform.Find("主面板/边框/基本属性").gameObject;
        TMP_Text name = 基本属性.transform.Find("名字").GetComponent<TMP_Text>();
        //文本集合.Add(name);
        name.text = 物品.name;
        name.color = bm.转换颜色(bm.Xstoi(物品.qua));

        //贴图
        Image 图标 = 基本属性.transform.Find("图标").GetComponent<Image>();
        初始化图标(图标, 物品);

        //星级
        /* Text xing = 信息框.transform.Find("主面板/panel_up/Xing/Text").GetComponent<Text>();
         xing.text = bm.Xor(物品.xing);
         //是否绑定/可以交易
         Text bang = 信息框.transform.Find("主面板/panel_up/Bang").GetComponent<Text>();
         if (物品.isbang.Equals("0"))
         {
             bang.text = "可交易";
             bang.color = bm.转换颜色(1);
         }
         else
         {
             bang.text = "不可交易";
             bang.color = bm.转换颜色(5);
         }*/
        //最低使用等级
        TMP_Text lv = 基本属性.transform.Find("最低等级").GetComponent<TMP_Text>();
        //文本集合.Add(lv);
        lv.text = "Lv: 1";

        //装备面板和道具面板
        GameObject 装备面板 = 信息框.transform.Find("主面板/边框/装备面板").gameObject;
        GameObject 道具面板 = 信息框.transform.Find("主面板/边框/道具面板").gameObject;

        if (物品.type.Equals("3"))//物品是装备
        {
            装备面板.SetActive(true);
            道具面板.SetActive(false);
            Equipment 装备 = (Equipment)物品;
            lv.text = "Lv: " + bm.Xor(装备.lessgrade);
            if (bm.Xstoi(装备.lessgrade) > bm.Xstoi(myData.等级))
            {
                Color nowColor;
                ColorUtility.TryParseHtmlString("#DD3737", out nowColor);
                lv.color = nowColor;
            }
            //显示各种属性
            TMP_Text 主属性文本 = 装备面板.transform.Find("主属性值").GetComponent<TMP_Text>();
            //文本集合.Add(主属性文本);
            主属性文本.text = PropMgr.GetInstance().主属性表[装备.place] + bm.Xstoi(装备.head_attribute_num);//待补
            //副属性
            if (装备.next_attribute != null && !装备.next_attribute.Equals(""))
            {
                GameObject 副属性 = 装备面板.transform.Find("副属性值").gameObject;
                副属性.SetActive(true);
                TMP_Text 副属性文本 = 副属性.GetComponent<TMP_Text>();
                //文本集合.Add(副属性文本);
                副属性文本.text = 装备.next_attribute + bm.Xstoi(装备.next_num);
            }
            //额外属性
            if (装备.extar_attribute != null && 装备.extar_attribute.Count > 0)
            {
                GameObject 额外属性 = 装备面板.transform.Find("额外属性值").gameObject;
                额外属性.SetActive(true);
                TMP_Text 额外属性文本 = 额外属性.GetComponent<TMP_Text>();
                //文本集合.Add(额外属性文本);
                foreach (string 属性名 in 装备.extar_attribute.Keys)
                {
                    额外属性文本.text += "\n" + 属性名 + bm.Xstoi(装备.extar_attribute[属性名]);
                }
            }


            if (!装备.tao.Equals(""))//显示套装信息
            {
                装备面板.transform.Find("套装").gameObject.SetActive(true);
                TMP_Text 套装名字文本 = 装备面板.transform.Find("套装基本信息").GetComponent<TMP_Text>();
                //文本集合.Add(套装名字文本);
                套装名字文本.gameObject.SetActive(true);
                TMP_Text 套装属性文本 = 装备面板.transform.Find("套装").GetComponent<TMP_Text>();
                //文本集合.Add(套装属性文本);
                套装属性文本.gameObject.SetActive(true);
                //信息填写
                suit_Data suData = PropMgr.套装表[装备.tao];
                /*信息框.transform.Find("额外面板/套装_name").GetComponent<Text>().text = "<" + suData.s_name + ">";
                信息框.transform.Find("额外面板/套装_num").GetComponent<Text>().text = "(" + suData.s_index + "/" + suData.s_total + ")";*/
                套装名字文本.text = "<" + suData.s_name + "> " + "(" + suData.s_total + "/" + suData.s_index + ")";
                string 效果文字 = 返回套装效果的文字描述(suData);
                /*信息框.transform.Find("额外面板/效果").GetComponent<Text>().text = 效果文字;
                int 文字大小 = 80;
                if (int.Parse(suData.s_total) > 2)
                    文字大小 = 80 - (int.Parse(suData.s_total) - 2) * 3;
                信息框.transform.Find("额外面板/效果").GetComponent<Text>().fontSize = 文字大小;*/
                套装属性文本.text = 效果文字;
            }
            //显示按钮并绑定事件
            GameObject 各种按钮 = 装备面板.transform.Find("按钮").gameObject;
            各种按钮.SetActive(true);

            //手动绑定材质
            for (int i = 0; i < 文本集合.Count; i++)
            {
                文本集合[i].fontSharedMaterial = (Material)Resources.Load<Material>("材质球/TMP默认材质");
            }


        }
        //物品为道具
        else
        {
            装备面板.SetActive(false);
            道具面板.SetActive(true);
            //效果/属性
            TMP_Text fun = 道具面板.transform.Find("道具效果").GetComponent<TMP_Text>();
            if (物品.type.Equals("1") && 物品.fun.Equals(""))
                fun.text = "材料/收集品\n" + "出售单价为" + bm.Xstoi(物品.price);
            else if (物品.type.Equals("2"))
                fun.text = "使用效果 : " + 物品.fun;
            else
            {
                fun.text = 物品.fun;
            }
        }
        /*
         Dictionary<string, string> 装备属性 = pm.提取装备属性(装备);
            string 属性文本 = "";
            foreach (string 属性词条 in 装备属性.Keys)
            {
                属性文本 += 属性词条 + bm.Xstoi(装备属性[属性词条]) + "\n";
            }
            fun.text = 属性文本;
         */


        //描述
        if (物品.comment != null && !物品.comment.Equals(""))
        {
            TMP_Text comment = 信息框.transform.Find("主面板/边框/简介").GetComponent<TMP_Text>();
            comment.text = 物品.comment;
        }
        // Debug.Log(物品.comment+物品.name);


    }


    public string 返回套装效果的文字描述(suit_Data suData)
    {
        string 效果 = "";
        Dictionary<int, string> 文字条目 = new Dictionary<int, string>();
        文字条目.Add(2, suData.s_two);
        文字条目.Add(3, suData.s_three);
        文字条目.Add(4, suData.s_four);
        文字条目.Add(5, suData.s_five);
        文字条目.Add(6, suData.s_six);
        文字条目.Add(7, suData.s_seven);
        for (int i = 2; i <= int.Parse(suData.s_total); i++)
        {
            if (int.Parse(suData.s_index) >= i)
                效果 += "<color=green>" + i + "件套 :  " + 文字条目[i] + "</color>\n";
            else if (i == int.Parse(suData.s_total))
                效果 += "<color=#848484>" + i + "件套 :  " + 文字条目[i] + "</color>";
            else
                效果 += "<color=#848484>" + i + "件套 :  " + 文字条目[i] + "</color>\n";
        }

        return 效果;
    }

    public void 生成技能学习界面(Dictionary<string, int> 技能列表)
    {
        关闭杂项();
        GameObject 对话框 = GameObject.Find("对话框(Clone)");
        if (对话框 != null)
            删除对象(对话框);
        GameObject 技能学习界面 = gameObject.transform.Find("道具界面(Clone)").gameObject;
        技能学习界面.SetActive(true);
        Ini_SkillPanel isp = 技能学习界面.GetComponent<Ini_SkillPanel>();
        isp.enabled = true;
        isp.ini_building(技能列表);
    }

    public void 生成道具购买界面(Dictionary<string, string> 道具列表)
    {
        关闭杂项();
        GameObject 对话框 = GameObject.Find("对话框(Clone)");
        if (对话框 != null)
            删除对象(对话框);
        GameObject 道具购买界面 = gameObject.transform.Find("道具界面(Clone)").gameObject;
        道具购买界面.SetActive(true);
        Ini_PropPanel ipp = 道具购买界面.GetComponent<Ini_PropPanel>();
        ipp.enabled = true;
        ipp.ini_building(道具列表);
    }




    public void 生成商城界面(string str_index)
    {
        关闭杂项();
        GameObject 对话框 = GameObject.Find("对话框(Clone)");
        if (对话框 != null)
            删除对象(对话框);
        GameObject 商城界面 = gameObject.transform.Find("道具界面(Clone)").gameObject;
        商城界面.SetActive(true);
        Ini_BuyProp ibp = 商城界面.GetComponent<Ini_BuyProp>();
        ibp.enabled = true;
        ibp.ini_building(str_index);
    }

    public void 初始化铜币商城()
    {
        Dictionary<string, Dictionary<string,int>> 道具表 = new Dictionary<string, Dictionary<string, int>>();
        生成商城界面("铜币商城");
    }


    public void 初始化金币商城()
    {
        生成商城界面("金币商城");
    }


    public void 初始化仙晶商城()
    {
        生成商城界面("仙晶商城");
    }


    public void 初始化黑钻商城()
    {
        生成商城界面("黑钻商城");
    }

    public void 生成装备打造界面(List<int> 打造图鉴等级)
    {
        关闭杂项();
        GameObject 对话框 = GameObject.Find("对话框(Clone)");
        if (对话框 != null)
            删除对象(对话框);
        GameObject 打造界面 = gameObject.transform.Find("道具界面(Clone)").gameObject;
        打造界面.SetActive(true);
        Ini_Building ib = 打造界面.GetComponent<Ini_Building>();
        ib.enabled = true;
        ib.ini_building(打造图鉴等级);
    }

    public void 生成道具界面()
    {
        关闭杂项();
        if (gameObject.transform.Find("道具界面(Clone)"))
        {
            gameObject.transform.Find("道具界面(Clone)").gameObject.SetActive(true);
            return;
        }
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("道具界面", 实例化道具界面, 参数集));
    }
    private void 实例化道具界面(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = NameMgr.画布;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 道具界面 = GameObject.Instantiate(对象) as GameObject;
        道具界面.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        道具界面.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        道具界面.transform.localPosition = new Vector2(0, 0);//设置生成位置
        道具界面.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        道具界面.transform.SetAsLastSibling();
        道具界面.SetActive(false);
    }


    public void 生成强化界面(string 键)
    {
        关闭杂项();
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 键);
        StartCoroutine(bm.LoadABPrefabs("强化界面", 实例化强化界面, 参数集));
    }
    private void 实例化强化界面(GameObject 对象, Dictionary<int, object> 参数集)
    {
        string 键 = 参数集[0] + "";

        GameObject 父物体 = NameMgr.画布;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 强化界面 = GameObject.Instantiate(对象) as GameObject;
        强化界面.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        强化界面.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 850);//recttransform必不可少的属性(半知半解)
        强化界面.transform.localPosition = new Vector2(0, 150);//设置生成位置
        强化界面.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        强化界面.transform.SetAsLastSibling();

        强化界面.GetComponent<EQU_Intensify>().Key = 键;
    }

    public void 生成合成界面(string 键)
    {
        关闭杂项();
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 键);
        StartCoroutine(bm.LoadABPrefabs("合成界面", 实例化合成界面, 参数集));
    }
    private void 实例化合成界面(GameObject 对象, Dictionary<int, object> 参数集)
    {
        string 键 = 参数集[0] + "";

        GameObject 父物体 = NameMgr.画布;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 合成界面 = GameObject.Instantiate(对象) as GameObject;
        合成界面.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        合成界面.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 850);//recttransform必不可少的属性(半知半解)
        合成界面.transform.localPosition = new Vector2(0, 150);//设置生成位置
        合成界面.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        合成界面.transform.SetAsLastSibling();

        合成界面.GetComponent<HeCheng>().Key = 键;
    }


    public void 生成设置界面()
    {
        Transform 设置界面 = gameObject.transform.Find("设置(Clone)");
        if (设置界面 != null)
        {
            string SceneName = SceneManager.GetActiveScene().name;
            if (SceneName.Equals("幻界星空"))
            {
                Destroy(设置界面);
            }
            else
            {
                设置界面.gameObject.SetActive(true);
                return;
            }
        }
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("设置", 实例化设置界面, 参数集));
    }
    private void 实例化设置界面(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体;
        string SceneName = SceneManager.GetActiveScene().name;
        if (SceneName.Equals("幻界星空"))
            父物体 = GameObject.Find("画布");
        else
            父物体 = NameMgr.画布;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 设置界面 = GameObject.Instantiate(对象) as GameObject;
        设置界面.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        设置界面.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 600);//recttransform必不可少的属性(半知半解)
        设置界面.transform.localPosition = new Vector2(0, 0);//设置生成位置
        设置界面.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        设置界面.transform.SetAsLastSibling();

    }


    public void 生成打造装备信息(Equipment 装备, GameObject 父物体, int index, Dictionary<string, int> 需求材料, int 总条数)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 装备);
        参数集.Add(1, 父物体);
        参数集.Add(2, index);
        参数集.Add(3, 需求材料);
        参数集.Add(4, 总条数);
        StartCoroutine(bm.LoadABPrefabs("打造装备信息", 实例化打造装备信息, 参数集));
        父物体.GetComponent<RectTransform>().sizeDelta = new Vector2(680, 153 * (总条数));//展开
    }
    private void 实例化打造装备信息(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject; ;
        Equipment 装备 = 参数集[0] as Equipment;
        int index = (int)参数集[2];
        int 总条数 = (int)参数集[4];
        Dictionary<string, int> 需求材料 = 参数集[3] as Dictionary<string, int>;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 打造信息 = GameObject.Instantiate(对象) as GameObject;
        打造信息.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        打造信息.GetComponent<RectTransform>().sizeDelta = new Vector2(678, 150);//recttransform必不可少的属性(半知半解)
        打造信息.transform.localPosition = new Vector2(0, -75 - 155 * index);//设置生成位置
        打造信息.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        打造信息.transform.SetAsLastSibling();

        //信息填充
        打造信息.name = 装备.name;
        DazaoItem dz = 打造信息.GetComponent<DazaoItem>();
        dz.需求材料 = 需求材料;
        Image 图标 = 打造信息.transform.Find("Image").GetComponent<Image>();
        初始化图标(图标, 装备);
        Text name = 打造信息.transform.Find("名字").GetComponent<Text>();
        name.text = 装备.name;
        name.color = bm.转换颜色(bm.Xstoi(装备.qua));
        /*Text bang = 打造信息.transform.Find("bang").GetComponent<Text>();
        if (装备.isbang == 0)
        {
            bang.text = "可交易";
            bang.color = bm.转换颜色(1);
        }
        else
        {
            bang.text = "不可交易";
            bang.color = bm.转换颜色(5);
        }
        打造信息.transform.Find("lv/Text").GetComponent<Text>().text = 装备.lessgrade + "";
        打造信息.transform.Find("xing").GetComponent<Text>().text = 装备.xing + "";
        打造信息.transform.Find("place/Text").GetComponent<Text>().text = 装备.place;*/
        string str = "";
        int i = 0;
        foreach (string s in 需求材料.Keys)
        {
            Dictionary<string, int> 当前材料 = new Dictionary<string, int>();
            当前材料.Add(s, 需求材料[s]);
            i++;
            if (i == 需求材料.Count)
            {
                if (pm.检测物品是否满足(当前材料))
                    str += s + "X" + 需求材料[s] + "(✔)";
                else
                    str += "<color=red>" + s + "X" + 需求材料[s] + "(✗)</color>";
            }
            else
            {
                if (pm.检测物品是否满足(当前材料))
                    str += s + "X" + 需求材料[s] + "(✔) ,";
                else
                    str += "<color=red>" + s + "X" + 需求材料[s] + "(✗)</color> ,";
            }
        }
        打造信息.transform.Find("Text").GetComponent<Text>().text = str;

        GameObject 查看按钮 = 打造信息.transform.Find("button/查看").gameObject;
        bm.Banding(查看按钮, 生成物品信息_无参);


        GameObject 打造按钮 = 打造信息.transform.Find("button/打造").gameObject;
        Ini_Building ib = GameObject.Find("道具界面(Clone)").GetComponent<Ini_Building>();
        bm.Banding(打造按钮, ib.打造装备);
        //判断材料是否足够,足够按钮亮,否则暗
        if (!pm.检测物品是否满足(需求材料))
        {
            Color nowColor;
            ColorUtility.TryParseHtmlString("#E0E0E0", out nowColor);
            打造按钮.GetComponent<Image>().color = nowColor;
        }


    }

    public void 生成技能学习项(SkillData sd, int money, string 钱币, GameObject 父物体, int index, int 总条数)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, sd);
        参数集.Add(1, money);
        参数集.Add(2, 父物体);
        参数集.Add(3, index);
        参数集.Add(4, 钱币);
        父物体.GetComponent<RectTransform>().sizeDelta = new Vector2(680, 121 * (总条数));//展开
        StartCoroutine(bm.LoadABPrefabs("技能学习项", 实例化技能学习项, 参数集));
    }

    private void 实例化技能学习项(GameObject 对象, Dictionary<int, object> 参数集) {
        SkillData sd = 参数集[0] as SkillData; ;
        int money = (int)参数集[1];
        int index = (int)参数集[3];
        string 钱币 = 参数集[4] + "";
        GameObject 父物体 = 参数集[2] as GameObject;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能学习项 = GameObject.Instantiate(对象) as GameObject;
        技能学习项.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能学习项.GetComponent<RectTransform>().sizeDelta = new Vector2(678, 120);//recttransform必不可少的属性(半知半解)
        技能学习项.transform.localPosition = new Vector2(0, -60 - 122 * index);//设置生成位置
        技能学习项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        技能学习项.transform.SetAsLastSibling();

        //信息填充
        Text 名字文本 = 技能学习项.transform.Find("name").GetComponent<Text>();
        名字文本.text = sd.name;
        名字文本.color = bm.转换颜色(bm.Xstoi(sd.qua));
        技能学习项.transform.Find("place").GetComponent<Text>().text = sd.place;
        技能学习项.transform.Find("demand/money").GetComponent<Text>().text = money + "-" + 钱币;

        //调用方法
        技能学习项.GetComponent<StudieSkillItem>().检索技能学习状态();

        //绑定脚本
        GameObject 查看按钮 = 技能学习项.transform.Find("Button/查看").gameObject;
        GameObject 学习按钮 = 技能学习项.transform.Find("Button/学习").gameObject;
        bm.Banding(查看按钮, 生成技能信息_无参);
        bm.Banding(学习按钮, 学习技能_学习面板);
    }

    public void 生成道具购买项(Prop_bascis pb, int money, string 钱币, GameObject 父物体, int index, int 总条数, string 购买数量)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, pb);
        参数集.Add(1, money);
        参数集.Add(2, 父物体);
        参数集.Add(3, index);
        参数集.Add(4, 钱币);
        参数集.Add(5, 总条数);
        参数集.Add(6, 购买数量);
        父物体.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(680, 290 * (总条数 / 4 + 1) - 30);//展开
        StartCoroutine(bm.LoadABPrefabs("道具购买项", 实例化道具购买项, 参数集));
    }

    private void 实例化道具购买项(GameObject 对象, Dictionary<int, object> 参数集)
    {
        Prop_bascis pb = 参数集[0] as Prop_bascis; ;
        int money = (int)参数集[1];
        int index = (int)参数集[3];
        string 钱币 = 参数集[4] + "";
        int 总条数 = (int)参数集[5];
        string 购买数量 = 参数集[6] + "";
        GameObject 父物体 = 参数集[2] as GameObject;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 道具购买项 = GameObject.Instantiate(对象) as GameObject;
        道具购买项.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性 
        道具购买项.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 240);//recttransform必不可少的属性(半知半解)
        道具购买项.transform.localPosition = new Vector2(-340 + 75 * (index % 4 + 1) + index % 4 * 85, (((总条数 / 4 + 1) * 290 - 30) / 2) - 140 * (index / 4 + 1) - 150 * (index / 4));//设置生成位置
        //Debug.Log("第" + (index + 1) + "个的位置是:X->" + 道具购买项.transform.localPosition.x + "---Y->" + 道具购买项.transform.localPosition.y);
        道具购买项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        道具购买项.transform.SetAsLastSibling();

        //信息填充
        Text 名字文本 = 道具购买项.transform.Find("名字/Text").GetComponent<Text>();
        名字文本.text = pb.name;
        名字文本.color = bm.转换颜色(bm.Xstoi(pb.qua));
        GameObject 图标对象 = 道具购买项.transform.Find("背景/图标").gameObject;
        Image 图标 = 图标对象.GetComponent<Image>();
        初始化图标(图标, pb);
        Text 限购文本 = 图标对象.transform.Find("Text").GetComponent<Text>();
        if (购买数量.Equals("无限"))
            限购文本.text = 购买数量;
        else
        {
            限购文本.text = "限购:" + 购买数量;
            限购文本.color = bm.转换颜色(5);
        }
        Text 价格文本 = 道具购买项.transform.Find("价格/Text").GetComponent<Text>();
        价格文本.text = money + "";
         
        BuyPropItem bpi = 道具购买项.GetComponent<BuyPropItem>();
        bpi.index = index;
        bpi.总条数 = 总条数;
        bpi.购买数量 = 购买数量;
        bpi.钱币 = 钱币;
    }


    public void 生成主角光环界面(GameObject 父物体)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        StartCoroutine(bm.LoadABPrefabs("主角光环界面", 实例化主角光环界面, 参数集));
    }

    private void 实例化主角光环界面(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能项 = GameObject.Instantiate(对象) as GameObject;
        技能项.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能项.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 800);//recttransform必不可少的属性(半知半解)
        技能项.transform.localPosition = new Vector2(0, 100);//设置生成位置
        技能项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        技能项.transform.SetAsLastSibling();

    }


    public void 生成深红加点界面(GameObject 父物体)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        StartCoroutine(bm.LoadABPrefabs("加点", 实例化深红加点界面, 参数集));
    }

    private void 实例化深红加点界面(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能项 = GameObject.Instantiate(对象) as GameObject;
        技能项.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能项.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 900);//recttransform必不可少的属性(半知半解)
        技能项.transform.localPosition = new Vector2(0, 200);//设置生成位置
        技能项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        技能项.transform.SetAsLastSibling();

    }

    public void 生成背包技能项(SkillData sd, GameObject 父物体, int index, int 总条数)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, sd);
        参数集.Add(1, 父物体);
        参数集.Add(2, index);
        父物体.GetComponent<RectTransform>().sizeDelta = new Vector2(284, 55 * (总条数 + 1));//展开
        StartCoroutine(bm.LoadABPrefabs("技能项", 实例化背包技能项, 参数集));
    }

    private void 实例化背包技能项(GameObject 对象, Dictionary<int, object> 参数集)
    {
        SkillData sd = 参数集[0] as SkillData; ;
        int index = (int)参数集[2];
        GameObject 父物体 = 参数集[1] as GameObject;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能项 = GameObject.Instantiate(对象) as GameObject;
        技能项.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能项.GetComponent<RectTransform>().sizeDelta = new Vector2(280, 65);//recttransform必不可少的属性(半知半解)
        技能项.transform.localPosition = new Vector2(0, -32.5f - 65 * index);//设置生成位置
        技能项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        技能项.transform.SetAsLastSibling();

        //显示技能使用情况
        role_Data myData = io_.load();
        foreach (string name in myData.技能槽.Keys) {
            if (sd.name.Equals(myData.技能槽[name]))
                技能项.transform.Find("遮罩").gameObject.SetActive(true);
        }


        //信息填充
        Text 技能名 = 技能项.transform.Find("name").GetComponent<Text>();
        技能名.text = sd.name;
        技能名.color = bm.转换颜色(bm.Xstoi(sd.qua));

    }

    public void 学习技能_学习面板() {
        GameObject 选中项 = GameObject.FindGameObjectWithTag("选中");
        string str = 选中项.transform.Find("demand/money").GetComponent<Text>().text;
        string[] strs = str.Split('-');
        string SkillName = 选中项.transform.Find("name").GetComponent<Text>().text;
        SkillData sd = pm.检索技能(SkillName);
        role_Data myData = io_.load();
        if (bm.Xstoi(myData.等级) < bm.Xstoi(sd.lessgrade))
        {
            生成警告框(bm.Xstoi(sd.lessgrade) + "级才能学习该技能");
            return;
        }
        else {
            if (sm.技能查重(sd.place, sd))
            {
                生成警告框("已经学过了");
                return;
            }
            else
            {
                string 返回值 = pm.失去金钱(strs[1], int.Parse(strs[0]));
                if (!返回值.Equals("成功"))
                {
                    生成警告框(返回值);
                    return;
                }
                sm.学习技能_无参();
                选中项.GetComponent<StudieSkillItem>().检索技能学习状态();
            }
        }
    }


    public void 操作子物体(GameObject 父物体, int 指令)
    {
        if (指令 == 1)//指令1,隐藏
        {
            for (int i = 0; i < 父物体.transform.childCount; i++)
            {
                父物体.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (指令 == 2)//指令2,显示
        {
            for (int i = 0; i < 父物体.transform.childCount; i++)
            {
                父物体.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else if (指令 == 3)//指令3,删除
        {
            for (int i = 0; i < 父物体.transform.childCount; i++)
            {
                Destroy(父物体.transform.GetChild(i).gameObject);
            }
        }

    }

    public void 关闭背景()
    {
        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        操作子物体(GameObject.Find("combat_other/combat_bg"), 1);
       // GameObject UI_ = GameObject.Find("UI");
        GameObject 背景 = UI.transform.Find("combat_bg").gameObject;
        背景.GetComponent<Image>().color = bm.改变透明度(背景, 180);
        UI.transform.Find("2级画布/IMG_button/关闭背景/开启").gameObject.SetActive(true);
        UI.transform.Find("2级画布/IMG_button/关闭背景/关闭").gameObject.SetActive(false);
    }

    public void 开启背景()
    {
        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        操作子物体(GameObject.Find("combat_other/combat_bg"), 2);
        //GameObject UI_ = GameObject.Find("UI");
        GameObject 背景 = UI.transform.Find("combat_bg").gameObject;
        背景.GetComponent<Image>().color = bm.改变透明度(背景, 0);
        UI.transform.Find("2级画布/IMG_button/关闭背景/关闭").gameObject.SetActive(true);
        UI.transform.Find("2级画布/IMG_button/关闭背景/开启").gameObject.SetActive(false);
    }

    public void 关闭天气()
    {
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey("关闭天气"))
            myData.记录["关闭天气"] = "1";
        else
            myData.记录.Add("关闭天气", "1");

        io_.save(myData);

        GameObject 父物体 = GameObject.Find("combat_other");
        父物体.transform.Find("天气(Clone)").gameObject.SetActive(false);
        GameObject 天气 = GameObject.Find("UI/2级画布/IMG_button/关闭天气");
        天气.transform.Find("开启").gameObject.SetActive(true);
        天气.transform.Find("关闭").gameObject.SetActive(false);
    }

    public void 开启天气()
    {
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey("关闭天气"))
            myData.记录["关闭天气"] = "0";
        else
            myData.记录.Add("关闭天气", "0");

        io_.save(myData);

        GameObject 父物体 = GameObject.Find("combat_other");
        父物体.transform.Find("天气(Clone)").gameObject.SetActive(true);
        GameObject 天气 = GameObject.Find("UI/2级画布/IMG_button/关闭天气");
        天气.transform.Find("开启").gameObject.SetActive(false);
        天气.transform.Find("关闭").gameObject.SetActive(true);
    }

    public void 移走对象()
    {

        gameObject.transform.position = new Vector3(3000, 3000, -500);//设置生成位置
    }


    public void 删除对象(GameObject 对象)
    {
        Destroy(对象);
    }

    public void 治疗()
    {
        关闭杂项();
        role_Data myData = io_.load();
        combat cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
        GameObject 宠物 = GameObject.FindGameObjectWithTag("宠物");
        if (宠物 != null)
        {
            combat ccb = 宠物.GetComponent<combat>();
            ccb.剩余血量 = ccb.血量;
            pem.返回宠物(myData.出战宠物UID).state = "";
        }
        cb.剩余血量 = cb.血量;
        myData.剩余血量 = myData.血量;
        io_.save(myData);
    }



    public void 加经验值(long 经验值)
    {
        bool isUP = RoleMgr.GetInstance().经验值结算(经验值);
        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        if (isUP)
        {
            combat cb_role =DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
            cb_role.人物属性刷新();
            cb_role.剩余血量 = cb_role.血量;

            ChatMgr.GetInstance().系统播报(new Prop_bascis(), "", bm.Xstoi(myData.等级), "升级");
            GameObject 父物体= DataMgr.GetInstance().本地对象["主角"].transform.parent.gameObject;
            生成升级特效(父物体,"1");
            刷新战斗力UI(UI);
        }
        NameMgr.画布.GetComponent<G_Util>().myData = myData;
        刷新经验条UI(UI);
    }


    public void 经验池加经验值(long 经验值)
    {
        RoleMgr.GetInstance().经验池结算(经验值);
    }

    public void 加金钱(Dictionary<string, int> 获得金钱)
    {
        RoleMgr.GetInstance().金钱结算(获得金钱);
        if (GameObject.FindGameObjectWithTag("UI"))
        {
            GameObject UI = GameObject.FindGameObjectWithTag("UI").gameObject;
            刷新金钱UI(UI);
        }
        else
        {
            Debug.Log("查不到UI");
        }

    }

    public void 刷新金钱UI(GameObject UI)
    {
        role_Data myData = io_.load();
        string SceneName = SceneManager.GetActiveScene().name;
        if (SceneName.Equals("幻界星空"))
        {
            Debug.Log("幻界星空的钱币,待补充");
        }
        else
        {
            Text 铜币 = UI.transform.Find("战斗页/money/tong/num_tong").GetComponent<Text>();
            Text 金币 = UI.transform.Find("战斗页/money/jin/num_jin").GetComponent<Text>();
            Text 仙晶 = UI.transform.Find("战斗页/money/zuan/num_zuan").GetComponent<Text>();
            Text 临时_铜币 = 铜币.transform.Find("num").GetComponent<Text>();
            Text 临时_金币 = 金币.transform.Find("num").GetComponent<Text>();
            Text 临时_仙晶 = 仙晶.transform.Find("num").GetComponent<Text>();
            foreach (string 货币名 in myData.金钱.Keys)
            {
                if (货币名.Equals("铜币"))
                {
                    铜币.text = 数字增加单位(bm.Xor(myData.金钱["铜币"]));
                    临时_铜币.text = bm.Xor(myData.金钱["铜币"]);
                }
                else if (货币名.Equals("金币"))
                {
                    金币.text = 数字增加单位(bm.Xor(myData.金钱["金币"]));
                    临时_金币.text = bm.Xor(myData.金钱["金币"]);
                }
                else if (货币名.Equals("仙晶"))
                {
                    仙晶.text = 数字增加单位(bm.Xor(myData.金钱["仙晶"]));
                    临时_仙晶.text = bm.Xor(myData.金钱["仙晶"]);
                }
            }
        }
    }


    public void 临时刷新金钱UI(GameObject UI,Dictionary<string,int>钱币)
    {
        Text 铜币 = UI.transform.Find("战斗页/money/tong/num_tong").GetComponent<Text>();
        Text 金币 = UI.transform.Find("战斗页/money/jin/num_jin").GetComponent<Text>();
        Text 仙晶 = UI.transform.Find("战斗页/money/zuan/num_zuan").GetComponent<Text>();
        Text 临时_铜币 = 铜币.transform.Find("num").GetComponent<Text>();
        Text 临时_金币 = 金币.transform.Find("num").GetComponent<Text>();
        Text 临时_仙晶 = 仙晶.transform.Find("num").GetComponent<Text>();

        //临时数据存储
        foreach (string 货币名 in 钱币.Keys) {
            if (dm.临时经验与钱币.ContainsKey(货币名))
            {
                dm.临时经验与钱币[货币名] += 钱币[货币名];
            }
            else
            {
                dm.临时经验与钱币.Add(货币名, 钱币[货币名]);
            }

            if (货币名.Equals("铜币"))
                铜币.text = 数字增加单位(int.Parse(临时_铜币.text) + 钱币[货币名]+"");
            else if (货币名.Equals("金币"))
                金币.text = 数字增加单位(int.Parse(临时_金币.text) + 钱币[货币名] + "");
            else if (货币名.Equals("仙晶"))
                仙晶.text = 数字增加单位(int.Parse(临时_仙晶.text) + 钱币[货币名] + "");
        }

    }


    public void 生成套装流光(GameObject 父物体, Color 颜色)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 父物体);
        参数集.Add(2, 颜色);
        StartCoroutine(bm.LoadABPrefabs("流光", 实例化套装流光, 参数集));
    }
    private void 实例化套装流光(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;
        Color 颜色 = (Color)参数集[2];
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 套装流光 = GameObject.Instantiate(对象) as GameObject;
        套装流光.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        套装流光.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);//recttransform必不可少的属性(半知半解)
        套装流光.transform.localPosition = new Vector2(-40.5f, 38.9f);//设置生成位置
        套装流光.transform.localScale = new Vector3(3f, 3f, 1);//设置生成的大小
        套装流光.transform.SetAsLastSibling();

        //绑定材质
        TrailRenderer trd = 套装流光.GetComponent<TrailRenderer>();
        trd.startColor = 颜色;
        trd.endColor = 颜色;
        /*Material 流光材质 = (Material)Resources.Load<Material>("材质球/普通光线");
        trd.materials[0] = 流光材质;
        Material 默认粒子效果 = (Material)Resources.Load<Material>("材质球/默认材质");
        trd.materials[1] = 默认粒子效果;*/

        //绑定动画
        套装流光.AddComponent<Animator>();
        RuntimeAnimatorController rac = (RuntimeAnimatorController)Resources.Load<RuntimeAnimatorController>("Animation/Particle System");
        套装流光.GetComponent<Animator>().runtimeAnimatorController = rac;
    }


    public void 生成技能流光_攻击竖排敌人(GameObject 父物体, Color 颜色, int 攻击数量)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 父物体);
        参数集.Add(2, 颜色);
        参数集.Add(3, 攻击数量);
        StartCoroutine(bm.LoadABPrefabs("流光", 实例化技能流光_攻击竖排敌人, 参数集));
    }
    private void 实例化技能流光_攻击竖排敌人(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;
        if (父物体.CompareTag("boss"))
        {
            父物体 = 父物体.transform.Find("图片").gameObject;
        }
        Color 颜色 = (Color)参数集[2];
        int num = (int)参数集[3];
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能流光 = GameObject.Instantiate(对象) as GameObject;
        技能流光.transform.SetParent(父物体.transform.parent, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能流光.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        技能流光.transform.localPosition = new Vector3(0, -100, 0);//设置生成位置
        技能流光.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        技能流光.transform.SetAsLastSibling();

        //唤醒音频源
        /*AudioSource 音频源 = 技能流光.GetComponent<AudioSource>();
        音频源.enabled = true;*/
        //绑定材质
        TrailRenderer trd = 技能流光.GetComponent<TrailRenderer>();
        trd.startWidth = 6.0f;
        trd.endWidth = 3.0f;
        trd.time = 0.3f;
        trd.startColor = 颜色;
        trd.endColor = 颜色;
        Material 流光材质 = (Material)Resources.Load<Material>("材质球/普通光线");
        trd.material = 流光材质;


        Vector2 目标位置 = 技能流光.transform.position + new Vector3(0, 20 * (num - 1) + 20, 0);
        StartCoroutine(bm.MoveTo(技能流光.transform, 目标位置, 0.3f));
    }


    public void 生成技能特效_水爆(GameObject 父物体)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 父物体);
        StartCoroutine(bm.LoadABPrefabs("水爆", 实例化技能特效_水爆, 参数集));
    }
    private void 实例化技能特效_水爆(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;
        if (父物体.CompareTag("boss"))
        {
            父物体 = 父物体.transform.Find("图片").gameObject;
        }
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能特效 = GameObject.Instantiate(对象) as GameObject;
        技能特效.transform.SetParent(父物体.transform.parent, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能特效.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        技能特效.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        技能特效.transform.localScale = new Vector3(80f, 80f, 80);//设置生成的大小
        技能特效.transform.SetAsLastSibling();

        //唤醒音频源
        /*AudioSource 音频源 = 技能特效.GetComponent<AudioSource>();
        音频源.enabled = true;*/

        //绑定材质
        //绑定材质
        Material 材质1 = (Material)Resources.Load<Material>("材质球/CFX_Anim_Splash_4frms");
        Material 材质2 = (Material)Resources.Load<Material>("材质球/CFX_Foam_AddSoft");
        Material 材质3 = (Material)Resources.Load<Material>("材质球/CFX_Ripple_AddSoft");
        Material 材质4 = (Material)Resources.Load<Material>("材质球/CFX_WhiteCircle");
        //升级特效.GetComponent<ParticleSystemRenderer>().material = 光阵材质1;
        技能特效.GetComponent<Transform>().localRotation = Quaternion.Euler(45f, 0f, 0f);
        技能特效.GetComponent<ParticleSystemRenderer>().material = 材质1;
        技能特效.transform.Find("Foam").GetComponent<ParticleSystemRenderer>().material = 材质2;
        技能特效.transform.Find("Ripples").GetComponent<ParticleSystemRenderer>().material = 材质3;
        技能特效.transform.Find("Drops").GetComponent<ParticleSystemRenderer>().material = 材质4;
    }


    public void 生成技能特效_火爆(GameObject 父物体)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 父物体);
        StartCoroutine(bm.LoadABPrefabs("火焰爆炸", 实例化技能特效_火爆, 参数集));
    }
    private void 实例化技能特效_火爆(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;
        if (父物体.CompareTag("boss"))
        {
            父物体 = 父物体.transform.Find("图片").gameObject;
        }
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能特效 = GameObject.Instantiate(对象) as GameObject;
        技能特效.transform.SetParent(父物体.transform.parent, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能特效.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        技能特效.transform.localPosition = new Vector3(0, 100, 0);//设置生成位置
        技能特效.transform.localScale = new Vector3(150f, 150f, 1);//设置生成的大小
        技能特效.transform.SetAsLastSibling();

        //唤醒音频源
       /* AudioSource 音频源 = 技能特效.GetComponent<AudioSource>();
        音频源.enabled = true;*/


    }


    public void 生成地址(GameObject 父物体,int index,string 地名)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 父物体);
        参数集.Add(2, index);
        参数集.Add(3, 地名);
        StartCoroutine(bm.LoadABPrefabs("地址", 实例化地址, 参数集));
    }
    private void 实例化地址(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;
        int index = (int)参数集[2];
        string 地名 = 参数集[3] + "";


        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 地址 = GameObject.Instantiate(对象) as GameObject;
        地址.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        地址.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 80);//recttransform必不可少的属性(半知半解)
        地址.transform.localPosition = new Vector3(父物体.transform.localPosition.x / 2 + (260*((index%3==0?3: index % 3)-1) +130), 父物体.transform.localPosition.y/2-((160*(index%3==0? index / 3-1 : index/3))+80), 0);//设置生成位置
        地址.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        地址.transform.SetAsLastSibling();

        Text 地址文本 = 地址.transform.Find("Text").GetComponent<Text>();
        地址文本.text = 地名;
        地址.name = 地名;
        if (!AdressMgr.GetInstance().检测战斗力是否达标(地名)) {
            地址文本.color = bm.转换颜色(5);
        }

    }


    public void 生成技能特效_三剑(GameObject 父物体)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 父物体);
        StartCoroutine(bm.LoadABPrefabs("三剑特效", 实例化技能特效_三剑, 参数集));
    }
    private void 实例化技能特效_三剑(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;
        if (父物体.CompareTag("boss"))
        {
            父物体 = 父物体.transform.Find("图片").gameObject;
        }
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能特效 = GameObject.Instantiate(对象) as GameObject;
        技能特效.transform.SetParent(父物体.transform.parent, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能特效.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        技能特效.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        技能特效.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);//设置生成的大小
        技能特效.transform.SetAsLastSibling();

        //唤醒音频源
        /*AudioSource 音频源 = 技能特效.GetComponent<AudioSource>();
        音频源.enabled = true;*/
        StartCoroutine(延时销毁自己(技能特效, 0.7f));
    }



    public void 生成技能特效_拳爆(GameObject 父物体)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 父物体);
        StartCoroutine(bm.LoadABPrefabs("拳爆特效", 实例化技能特效_拳爆, 参数集));
    }
    private void 实例化技能特效_拳爆(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;
        if (父物体.CompareTag("boss"))
        {
            父物体 = 父物体.transform.Find("图片").gameObject;
        }
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能特效 = GameObject.Instantiate(对象) as GameObject;
        技能特效.transform.SetParent(父物体.transform.parent, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能特效.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        技能特效.transform.position = new Vector3(0, 30, 0);//设置生成位置
        技能特效.transform.localScale = new Vector3(200, 200, 1);//设置生成的大小
        技能特效.transform.SetAsLastSibling();

        //唤醒音频源
        //AudioSource 音频源 = 技能特效.GetComponent<AudioSource>();
        //音频源.enabled = true;
        StartCoroutine(延时销毁自己(技能特效, 0.85f));
    }




    public void 生成技能特效_单体(GameObject 父物体)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 父物体);
        StartCoroutine(bm.LoadABPrefabs("单体特效", 实例化技能特效_单体, 参数集));
    }
    private void 实例化技能特效_单体(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能特效 = GameObject.Instantiate(对象) as GameObject;
        技能特效.transform.SetParent(父物体.transform.parent, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能特效.GetComponent<RectTransform>().sizeDelta = new Vector2(420, 260);//recttransform必不可少的属性(半知半解)
        if (父物体.CompareTag("boss"))
        {
            技能特效.transform.position = 父物体.transform.Find("图片").position;//boss图片位置
        }
        else
        {
            技能特效.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        }
        技能特效.transform.localScale = new Vector3(1, 1, 1);//设置生成的大小
        技能特效.transform.SetAsLastSibling();

        //唤醒音频源
        /*AudioSource 音频源 = 技能特效.GetComponent<AudioSource>();
        音频源.enabled = true;*/
        //技能特效.GetComponent<Transform>().localRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(70, 111));
        StartCoroutine(延时销毁自己(技能特效, 0.2f));
    }



    public void 生成天赋框(GameObject 父物体,TianFu tf)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, tf);
        参数集.Add(1, 父物体);
        StartCoroutine(bm.LoadABPrefabs("天赋框", 实例化天赋框, 参数集));
    }
    private void 实例化天赋框(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;
        TianFu tf = 参数集[0] as TianFu;
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 天赋框 = GameObject.Instantiate(对象) as GameObject;
        天赋框.transform.SetParent(父物体.transform.parent, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        天赋框.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 500);//recttransform必不可少的属性(半知半解)
        天赋框.transform.localPosition = new Vector3(0, 200, 0);//设置生成位置
        天赋框.transform.localScale = new Vector3(1, 1, 1);//设置生成的大小
        天赋框.transform.SetAsLastSibling();


        if (tf.comment.Equals("核心")) {
            天赋框.transform.Find("button").gameObject.SetActive(false);
        }

        TianFuPanel tfp= 天赋框.GetComponent<TianFuPanel>();
        tfp.tf = tf;
        tfp.TFName = tf.name;
        tfp.刷新天赋信息(tf);
        GameObject Panel = 天赋框.transform.Find("Panel").gameObject;
        bm.Banding(Panel,关闭杂项);
    }

    public void 生成技能特效_防御(GameObject 父物体,int 持续时间)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 父物体);
        参数集.Add(2, 持续时间);
        StartCoroutine(bm.LoadABPrefabs("防御特效", 实例化技能特效_防御, 参数集));
    }
    private void 实例化技能特效_防御(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;
        int 持续时间 = (int)参数集[2];


        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能特效 = GameObject.Instantiate(对象) as GameObject;

        if (父物体.CompareTag("boss"))
        {
            技能特效.transform.SetParent(父物体.transform.Find("图片"), false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
            技能特效.GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);//recttransform必不可少的属性(半知半解)
        }
        else {
            技能特效.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
            技能特效.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);//recttransform必不可少的属性(半知半解)
        }
       
        技能特效.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        技能特效.transform.localScale = new Vector3(1, 1, 1);//设置生成的大小
        技能特效.transform.SetAsLastSibling();


        StartCoroutine(延时销毁自己(技能特效, 持续时间));
    }


    public void 生成技能特效_攻击(GameObject 父物体)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 父物体);
        StartCoroutine(bm.LoadABPrefabs("攻击特效", 实例化技能特效_攻击, 参数集));
    }
    private void 实例化技能特效_攻击(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[1] as GameObject;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能特效 = GameObject.Instantiate(对象) as GameObject;
        if (父物体.CompareTag("boss"))
        {
            技能特效.transform.SetParent(父物体.transform.Find("图片"), false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
            技能特效.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 400);//recttransform必不可少的属性(半知半解)
        }
        else
        {
            技能特效.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
            技能特效.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 200);//recttransform必不可少的属性(半知半解)
        }

        技能特效.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        技能特效.transform.localScale = new Vector3(1, 1, 1);//设置生成的大小
        技能特效.transform.SetAsLastSibling();


        StartCoroutine(放大渐隐协程(技能特效));
    }



    public IEnumerator 放大渐隐协程(GameObject 对象)
    {
        float 透明值 = 255;
        while (透明值 > 0) {
            透明值 -= 20f;
            对象.GetComponent<Image>().color= bm.改变透明度(对象, 透明值);
            对象.transform.localScale += new Vector3(0.06f,0.06f,0.06f);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(对象);
        yield break;
    }



    public void 启动延时方法(UnityAction 方法, float 延时时间)
    { 
        StartCoroutine(延时方法(方法,延时时间));
    }

    public IEnumerator 延时方法(UnityAction 方法, float 延时时间)
    {
        yield return new WaitForSeconds(延时时间);
        方法();
    }

    public void 启动延迟伤害(combat cb, int 伤害, GameObject 目标对象, float 延时时间) {

        StartCoroutine(延时伤害(cb, 伤害, 目标对象, 延时时间));
    }

    public IEnumerator 延时伤害(combat cb, int 伤害, GameObject 目标对象, float 延时时间)
    {
        yield return new WaitForSeconds(延时时间);
        se.可暴伤加成(cb, 伤害, 目标对象);
    }

    public IEnumerator 延时销毁自己( GameObject 目标对象, float 延时时间)
    {
        yield return new WaitForSeconds(延时时间);
        Destroy(目标对象);
    }

    /// <summary>
    /// 间隔一段时间提升一次属性,持续一大段时间/例如10秒内每秒提升5%的攻击力
    /// </summary>
    /// <param name="cb"></param>
    /// <param name="属性名"></param>
    /// <param name="提升幅度"></param>
    /// <param name="总提升次数"></param>
    /// <param name="时间粒度"></param>
    public void 持续提升属性(combat cb, string 属性名, float 提升幅度, int 总提升次数, float 时间粒度) {
        StartCoroutine(持续提升属性携程(cb, 属性名, 提升幅度, 总提升次数, 时间粒度));
    }

    private IEnumerator 持续提升属性携程(combat cb, string 属性名, float 提升幅度,int 总提升次数  ,float 时间粒度) {
        for (int i = 0; i < 总提升次数; i++)
        {
            yield return new WaitForSeconds(时间粒度);
            if (cb != null)
            {
                提升属性(cb, 属性名, 提升幅度, 总提升次数 * 时间粒度);
            }
         }
     }






    public void 获得buff(combat cb,string buff名字,BuffData buff)
    {
        if (cb.buff栏.ContainsKey(buff名字))
        {
            //buff可以叠加,刷新属性和时间
            if (buff.是否可以叠加 == true&& cb.buff栏[buff名字].层数< cb.buff栏[buff名字].最大层数)
            {
                cb.buff栏[buff名字].层数 += 1;
                cb.buff栏[buff名字].持续时间 = buff.持续时间;
                cb.buff栏[buff名字].buff提升的属性值 += buff.buff提升的属性值;

                //Debug.Log("叠加一层buff,当前层数:" + cb.buff栏[buff名字].层数 + "...当前提升属性:" + cb.buff栏[buff名字].buff提升的属性值);
                //额外增加属性
                属性变化方法(cb, buff.buff提升的属性名, buff.buff提升的属性值);
            }
            //buff不能叠加,刷新时间
            else
            {
                cb.buff栏[buff名字].持续时间 = buff.持续时间;
                //Debug.Log("刷新时间");
            }
        }
        else {
            cb.buff栏.Add(buff名字, buff);
            //启动属性协程
            提升buff属性(cb,buff名字);
            Debug.Log("开启buff:"+ buff名字);
        }
    }

    public void 提升buff属性(combat cb, string buff名字)
    {
        StartCoroutine(提升buff属性携程(cb, buff名字));
    }

    public IEnumerator 提升buff属性携程(combat cb, string buff名字)
    {
        //Debug.Log("提升属性:"+ cb.buff栏[buff名字].buff提升的属性名+"---"+  cb.buff栏[buff名字].buff提升的属性值);
        属性变化方法(cb, cb.buff栏[buff名字].buff提升的属性名, cb.buff栏[buff名字].buff提升的属性值);

        while (cb.buff栏[buff名字].持续时间 > 0) {
            cb.buff栏[buff名字].持续时间 -= 0.5f;
            yield return new WaitForSeconds(0.5f);
        }
        属性变化方法(cb, cb.buff栏[buff名字].buff提升的属性名,  cb.buff栏[buff名字].buff提升的属性值 * (-1));
        //Debug.Log("最后提升的属性:" + cb.buff栏[buff名字].buff提升的属性名 + "---" + cb.buff栏[buff名字].buff提升的属性值+"剩余:"+bm.Xstof(cb.临时属性["攻击速度"]));
        //Debug.Log("剩余:"+bm.Xstof(cb.临时属性["攻击速度"]));
        cb.buff栏.Remove(buff名字);
        //yield break;
    }




    /// <summary>
    /// 一段时间只有一次提升/例如提升50%的攻击力,持续10秒
    /// </summary>
    /// <param name="cb"></param>
    /// <param name="属性名"></param>
    /// <param name="提升幅度"></param>
    /// <param name="持续时间"></param>
    public void 提升属性(combat cb, string 属性名, float 提升幅度, float 持续时间) {
        StartCoroutine(提升属性携程(cb, 属性名, 提升幅度, 持续时间));
    }

    public IEnumerator 提升属性携程(combat cb, string 属性名, float 提升幅度, float 持续时间)
    {
        属性变化方法(cb, 属性名, 提升幅度);
        yield return new WaitForSeconds(持续时间);
        属性变化方法(cb, 属性名, 提升幅度*(-1));
    }

    private void 属性变化方法(combat cb, string 属性名, float 提升幅度) {
        if (属性名.Equals("攻击力"))
            cb.临时属性["攻击力"] = bm.Xftos(bm.Xstof(cb.临时属性["攻击力"]) + (float)提升幅度);
        else if (属性名.Equals("防御力"))
            cb.临时属性["防御力"] = bm.Xftos(bm.Xstof(cb.临时属性["防御力"]) + (float)提升幅度);
        else if (属性名.Equals("血量"))
            cb.临时属性["血量"] = bm.Xftos(bm.Xstof(cb.临时属性["血量"]) + (float)提升幅度);
        else if (属性名.Equals("回血值"))
            cb.临时属性["回血值"] = bm.Xftos(bm.Xstof(cb.临时属性["回血值"]) + (float)提升幅度);
        else if (属性名.Equals("暴击率"))
            cb.临时属性["暴击率"] = bm.Xftos(bm.Xstof(cb.临时属性["暴击率"]) + (float)(提升幅度));
        else if (属性名.Equals("暴伤加成"))
            cb.临时属性["暴伤加成"] = bm.Xftos(bm.Xstof(cb.临时属性["暴伤加成"]) + (float)(提升幅度));
        else if (属性名.Equals("攻击速度"))
        {
            cb.临时属性["攻击速度"] = bm.Xftos(bm.Xstof(cb.临时属性["攻击速度"]) + (float)(提升幅度));
        }
        else if (属性名.Equals("移动速度"))
            cb.临时属性["移动速度"] = bm.Xftos(bm.Xstof(cb.临时属性["移动速度"]) + (float)提升幅度);
        else if (属性名.Equals("固定伤害"))
            cb.临时属性["固定伤害"] = bm.Xftos(bm.Xstof(cb.临时属性["固定伤害"]) + (float)提升幅度);
        else if (属性名.Equals("固定减伤"))
            cb.临时属性["固定减伤"] = bm.Xftos(bm.Xstof(cb.临时属性["固定减伤"]) + (float)提升幅度);
        else if (属性名.Equals("伤害加成"))
            cb.临时属性["伤害加成"] = bm.Xftos(bm.Xstof(cb.临时属性["伤害加成"]) + (float)(提升幅度));
        else if (属性名.Equals("伤害减免"))
            cb.临时属性["伤害减免"] = bm.Xftos(bm.Xstof(cb.临时属性["伤害加成"]) + (float)(提升幅度));
        else if (属性名.Equals("固定吸血"))
            cb.临时属性["固定吸血"] = bm.Xftos(bm.Xstof(cb.临时属性["固定吸血"]) + (float)提升幅度);
        else if (属性名.Equals("吸血加成"))
            cb.临时属性["吸血加成"] = bm.Xftos(bm.Xstof(cb.临时属性["吸血加成"]) + (float)(提升幅度));


        dm.加载临时属性(cb);
        //死亡删除所有buff所以不需要触发
        if (cb!=null&&cb.gameObject.activeSelf)
        {
            生成显示buff_逻辑层(cb, 属性名, 提升幅度);
        }
    }



    public void 生成显示buff_逻辑层(combat cb, string 属性名, float 提升幅度) {

        if (cb.buff显示栏.ContainsKey(属性名))
        {
            //属性值归零,删除buff图标后进行排序
            if (bm.Xstoi(cb.临时属性[属性名]) == 0)
            {
                cb.buff显示栏[属性名].SetActive(false);
                cb.buff显示栏.Remove(属性名);
                StartCoroutine(生成显示buff_视图层(cb));
                Debug.Log("删除方法:" + 属性名);
                return;
            }
            else {
                int 显示值 = int.Parse(cb.buff显示栏[属性名].transform.Find("数值").GetComponent<Text>().text);
                //属性值大于999,显示值为99时,不做改变
                if (bm.Xstoi(cb.临时属性[属性名]) >= 999 && 显示值 == 999)
                {
                    return;
                }
                else {
                    StartCoroutine( 改变显示buff_视图层(cb,cb.buff显示栏[属性名], bm.Xstoi(cb.临时属性[属性名])));
                    //cb.buff显示栏[属性名].transform.Find("数值").GetComponent<Text>().text = "222";
                    //Debug.Log("改变方法:"+属性名+ bm.Xstoi(cb.临时属性[属性名]));

                }
            }
           
        }
        else {
            //排序
            StartCoroutine(生成显示buff_视图层(cb));
            //Debug.Log("增加方法:" + 属性名);
        }
       

    }

    public IEnumerator 改变显示buff_视图层(combat cb,GameObject buff,int 值) {
        if (值 > 999)
            值 = 999;
        //后续添加改变颜色
        buff.transform.Find("数值").GetComponent<Text>().text = 值 + "";
        yield break;

    }


    public IEnumerator 生成显示buff_视图层(combat cb)
    {
        GameObject buff = Resources.Load<GameObject>("预制体/Buff");
        int index = 0;
        foreach (string 属性名字 in cb.临时属性.Keys)
        {
            //判断是否为0,0不需要考虑
            if (bm.Xstoi(cb.临时属性[属性名字]) != 0)
            {
                if (!cb.buff显示栏.ContainsKey(属性名字)) {
                    //实例化
                    GameObject buff实例化 = GameObject.Instantiate(buff) as GameObject;
                    Sprite img = Resources.Load<Sprite>("Buff/" + 属性名字 + "buff");
                    if (img == null)
                    {
                        img = Resources.Load<Sprite>("Buff/四维buff");
                    }
                    buff实例化.GetComponent<Image>().sprite = img;
                    buff实例化.transform.SetParent(cb.gameObject.transform.Find("buff栏"), false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
                    buff实例化.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 30);//recttransform必不可少的属性(半知半解)
                    buff实例化.transform.localPosition = new Vector2(index * 25 - 75, 0);//设置生成位置
                    buff实例化.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
                    buff实例化.transform.Find("数值").GetComponent<Text>().text = bm.Xstoi(cb.临时属性[属性名字]) + "";


                    //判断是否添加了buff
                    if (!cb.buff显示栏.ContainsKey(属性名字))
                    {
                        cb.buff显示栏.Add(属性名字, buff实例化);
                    }
                    else
                    {
                        cb.buff显示栏[属性名字] = buff实例化;
                    }
                }
                else {
                    StartCoroutine(改变显示buff_视图层(cb,cb.buff显示栏[属性名字], bm.Xstoi(cb.临时属性[属性名字])));
                }
                index++;
            }
            else
            {
                if (cb.buff显示栏.ContainsKey(属性名字))
                {
                    cb.buff显示栏.Remove(属性名字);
                    continue;
                }
            }
        }
        yield break;
    }



    public void 恢复血量(combat cb, int 单次恢复量) {
        /*if (cb.CompareTag("boss"))
            单次恢复量 *= 3;*/
        if (cb != null)
        {
            cb.剩余血量 = bm.Xor(bm.Xstoi(cb.剩余血量) + 单次恢复量 + "");
            扣血显示(cb.gameObject, 单次恢复量, "回血");
        }
    }

    public void 持续恢复血量(combat cb, int 单次恢复量, int 总恢复次数, float 时间粒度)
    {
        /*if (cb.CompareTag("boss"))
            单次恢复量 *= 2;*/
        StartCoroutine(持续恢复血量携程(cb, 单次恢复量, 总恢复次数, 时间粒度));
    }

    public IEnumerator 持续恢复血量携程(combat cb, int 单次恢复量, int 总恢复次数, float 时间粒度)
    {
        for (int i = 0; i < 总恢复次数; i++) {
            yield return new WaitForSeconds(时间粒度);
            if (cb != null) {
                cb.剩余血量 = bm.Xor(bm.Xstoi(cb.剩余血量) + 单次恢复量 + "");
                扣血显示(cb.gameObject, 单次恢复量, "回血");
            }
        }
    }


    public void 检索装备栏生成流光()
    {
        GameObject[] 所有装备栏 = GameObject.FindGameObjectsWithTag("装备栏");
        foreach (GameObject 装备栏 in 所有装备栏)
        {
            Panel_Eqm pe = 装备栏.GetComponent<Panel_Eqm>();
            pe.删除无用流光();
        }
    }


    public void 生成奖励获取界面(Sprite 头图片, string 内容, string 奖励物品名)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(1, 头图片);
        参数集.Add(2, 内容);
        参数集.Add(3, 奖励物品名);
        StartCoroutine(bm.LoadABPrefabs("奖品获取框", 实例化奖励获取界面, 参数集));
    }
    private void 实例化奖励获取界面(GameObject 对象, Dictionary<int, object> 参数集)
    {
        Sprite 头图片 = 参数集[1] as Sprite;
        string 内容 = (string)参数集[2];
        string 奖励物品名 = (string)参数集[3];
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 奖励界面 = GameObject.Instantiate(对象) as GameObject;
        奖励界面.transform.SetParent(NameMgr.画布.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        奖励界面.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        奖励界面.transform.localPosition = new Vector2(-0, 0);//设置生成位置
        奖励界面.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        奖励界面.transform.SetAsLastSibling();

        //填充信息
        奖励界面.transform.Find("Panel/头部/Panel").GetComponent<Image>().sprite = 头图片;
        奖励界面.transform.Find("Panel/头部/Panel/名字").GetComponent<Text>().text = 奖励物品名;
        奖励界面.transform.Find("Panel/身体/Panel/Scroll View/Viewport/Content/Text").GetComponent<Text>().text = 内容;

        //按钮初始化
        GameObject 领取按钮 = 奖励界面.transform.Find("Panel/脚部/Panel/领取按钮").gameObject;
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey(奖励物品名))
        {
            Color nowColor;
            ColorUtility.TryParseHtmlString("#F5FBFA", out nowColor);//白色
            领取按钮.GetComponent<Image>().color = nowColor;
            领取按钮.transform.Find("Text").GetComponent<Text>().text = "已领取";
        }
        else
        {
            //绑定点击事件       
            bm.Banding(领取按钮, 奖励界面领取按钮点击事件);
        }

        //绑定点击事件   
        GameObject 关闭按钮 = 奖励界面.transform.Find("Panel/头部/Panel/关闭").gameObject;
        bm.Banding(关闭按钮, 关闭奖品获取框页面);
    }

    public void 奖励界面领取按钮点击事件()
    {
        GameObject 按钮 = GameObject.Find("领取按钮");
        if (按钮 != null)
        {
            string 奖励物品名 = 按钮.transform.parent.parent.parent.Find("头部/Panel/名字").GetComponent<Text>().text;
            role_Data myData = io_.load();
            if (myData.记录.ContainsKey(奖励物品名))//有领取记录的防止重复领取
                return;
            //持久化物品数据
            if (!pm.检索物品(奖励物品名).name.Equals(""))//非空判断
            {
                生成获得框(奖励物品名,1);
                pm.获取物品(奖励物品名, 1);
                myData = io_.load();
                myData.记录.Add(奖励物品名, "1");
                io_.save(myData);
            }


            Color nowColor;
            ColorUtility.TryParseHtmlString("#F5FBFA", out nowColor);//白色
            按钮.GetComponent<Image>().color = nowColor;
            按钮.transform.Find("Text").GetComponent<Text>().text = "已领取";
        }
    }

    private void 关闭奖品获取框页面()
    {
        GameObject 奖品获取框 = GameObject.Find("奖品获取框(Clone)");
        Destroy(奖品获取框);
    }

    public void 生成兑换码界面()
    {
        if (gameObject.transform.Find("兑换码(Clone)"))
        {
            gameObject.transform.Find("兑换码(Clone)").gameObject.SetActive(true);
            gameObject.transform.Find("兑换码(Clone)").gameObject.transform.SetAsLastSibling();
            return;
        }
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("兑换码", 实例兑换码界面, 参数集));
    }
    private void 实例兑换码界面(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = NameMgr.画布;
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 兑换码界面 = GameObject.Instantiate(对象) as GameObject;
        兑换码界面.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        兑换码界面.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        兑换码界面.transform.localPosition = new Vector2(0, 0);//设置生成位置
        兑换码界面.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        兑换码界面.transform.SetAsLastSibling();

        //绑定点击事件
        GameObject 兑换按钮 = 兑换码界面.transform.Find("Panel/兑换").gameObject;
        bm.Banding(兑换按钮, 兑换码按钮);
    }

    public void 兑换码按钮() {
        string str_cdk = GameObject.Find("兑换码输入框/Text").GetComponent<Text>().text;//获取输入内容
        Dictionary<string, Action> AllCDK = CDkMgr.GetInstance().返回兑换码列表();

        if (str_cdk.Equals("")) //先判断是否为空
            生成警告框("CDK为空");
        else if (!AllCDK.ContainsKey(str_cdk))//在判断是否有该兑换码
            生成警告框("无效兑换码");
        else if (io_.load().记录.ContainsKey("CDK_" + str_cdk))//最后判断是否重复使用
            生成警告框("你已经使用过该兑换码");
        else
            AllCDK[str_cdk]();
    }

    public void 宠物升级(Pet_Data 宠物) {
        role_Data myData = io_.load();
        //使用引用传递,所以传递来的是存档里的宠物数据
        宠物.grade += 1;
        io_.save(myData);
    }


    public void 生成技能背包(string type, int index)
    {
        GameObject 技能背包 = GameObject.Find("技能背包(Clone)");
        if (技能背包 != null)
        {
            Destroy(技能背包);
        }
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, type);
        参数集.Add(1, index);
        StartCoroutine(bm.LoadABPrefabs("技能背包", 实例技能背包, 参数集));
    }
    private void 实例技能背包(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = GameObject.Find("属性面板(Clone)");
        string type = 参数集[0] + "";
        int index = (int)参数集[1];
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能背包 = GameObject.Instantiate(对象) as GameObject;
        技能背包.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        技能背包.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        技能背包.transform.localPosition = new Vector2(0, 0);//设置生成位置
        技能背包.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        技能背包.transform.SetAsLastSibling();
        技能背包.transform.Find("背包界面/Panel/Text").GetComponent<Text>().text = type;

        //调用方法
        技能背包.GetComponent<SkillBag>().Ini_SKillBag();

        //标记
        技能背包.transform.Find("标记").GetComponent<Text>().text = index + "";

    }

    public void 生成确认框(string str, UnityAction action)
    {
        if (dm.本地对象.ContainsKey("确认框"))
        {
            Destroy(dm.本地对象["确认框"]);
            dm.本地对象.Remove("确认框");
        }
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, str);
        参数集.Add(1, action);
        StartCoroutine(bm.LoadABPrefabs("确认框", 实例确认框, 参数集));
    }
    private void 实例确认框(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = NameMgr.画布;
        string str = 参数集[0] + "";
        UnityAction action = 参数集[1] as UnityAction;
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 确认框 = GameObject.Instantiate(对象) as GameObject;
        DataMgr.GetInstance().本地对象.Add("确认框", 确认框);
        确认框.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        确认框.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 400);//recttransform必不可少的属性(半知半解)
        确认框.transform.localPosition = new Vector2(0, 200);//设置生成位置
        确认框.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        确认框.transform.SetAsLastSibling();
        确认框.transform.Find("Text").GetComponent<Text>().text = str;
        GameObject 确定按钮 = 确认框.transform.Find("button/确定").gameObject;
        GameObject 取消按钮 = 确认框.transform.Find("button/取消").gameObject;
        bm.Banding(确定按钮, action);
        bm.Banding(取消按钮, 删除确认框);

    }

    public void 删除确认框() {
        if (dm.本地对象.ContainsKey("确认框"))
        {
            Destroy(dm.本地对象["确认框"]);
            dm.本地对象.Remove("确认框");
        }
    }

    public void 生成场景任务框(string 标题, string 描述, string 物品名, int 物品数量, string 奖品名, string 隐藏后物品的名字)
    {
        关闭杂项();
        强制关闭对话框();
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 标题);
        参数集.Add(1, 描述);
        参数集.Add(2, 物品名);
        参数集.Add(3, 物品数量);
        参数集.Add(4, 奖品名);
        参数集.Add(5, 隐藏后物品的名字);
        StartCoroutine(bm.LoadABPrefabs("场景任务框", 实例场景任务框, 参数集));
    }
    private void 实例场景任务框(GameObject 对象, Dictionary<int, object> 参数集)
    {
        string 标题 = 参数集[0] + "";
        string 描述 = 参数集[1] + "";
        string 物品名 = 参数集[2] + "";
        int 物品数量 = (int)参数集[3];
        string 奖品名 = 参数集[4] + "";
        string 隐藏后物品的名字 = 参数集[5] + "";


        int num = pm.返回背包该物品的数量(物品名);//背包里该道具的数量
        if (奖品名.Contains("-"))//奖励如果不是固定的话,尝试分割.格式为"系数-10-铜币"
        {
            string[] 分割结果 = 奖品名.Split('-');
            if (分割结果[0].Equals("系数"))
            {
                奖品名 = (int.Parse(分割结果[1]) * num) + 分割结果[2];
            }
        }
        GameObject 父物体 = NameMgr.画布;
        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 场景任务框 = GameObject.Instantiate(对象) as GameObject;
        场景任务框.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        场景任务框.GetComponent<RectTransform>().sizeDelta = new Vector2(680, 620);//recttransform必不可少的属性(半知半解)
        场景任务框.transform.localPosition = new Vector2(0, 150);//设置生成位置
        场景任务框.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        场景任务框.transform.SetAsLastSibling();

        //信息填充
        场景任务框.transform.Find("title").GetComponent<Text>().text = 标题;
        场景任务框.transform.Find("comment/Text").GetComponent<Text>().text = 描述;
        if (!隐藏后物品的名字.Equals(""))
            场景任务框.transform.Find("state/demand/demand_prop").GetComponent<Text>().text = 隐藏后物品的名字 + "( " + "<color=blue>" + num + "</color>" + " / " + 物品数量 + " )";
        else
            场景任务框.transform.Find("state/demand/demand_prop").GetComponent<Text>().text = 物品名 + "( " + "<color=blue>" + num + "</color>" + " / " + 物品数量 + " )";


        GameObject 确定按钮 = 场景任务框.transform.Find("button/确定").gameObject;
        if (num < 物品数量)
        {
            场景任务框.transform.Find("state/demand/demand_state").GetComponent<Text>().text = "未完成";
            Color nowColor;
            ColorUtility.TryParseHtmlString("#F5FBFA", out nowColor);//白色
            确定按钮.GetComponent<Image>().color = nowColor;
        }
        else {
            场景任务框.transform.Find("state/demand/demand_state").GetComponent<Text>().text = "已完成";
            bm.Banding(确定按钮, 场景任务表[标题]);
        }

        Text 奖品文本 = 场景任务框.transform.Find("award/award_prop").GetComponent<Text>();
        奖品文本.text = 奖品名;
        奖品文本.color = bm.转换颜色(bm.Xstoi( pm.检索物品(奖品名).qua));

    }

    public void 强制关闭对话框()
    {
        GameObject 对话框 = GameObject.Find("对话框(Clone)");
        if (对话框 != null)
        {
            Text_DaYin tdy = 对话框.GetComponent<Text_DaYin>();
            tdy.强制关闭对话框();
        }
    }


    public void 生成死亡动画(GameObject 死亡对象)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 死亡对象);
        StartCoroutine(bm.LoadABPrefabs("死亡", 实例化死亡动画, 参数集));

    }
    private void 实例化死亡动画(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 死亡对象 = 参数集[0] as GameObject;
        GameObject 父物体 = 死亡对象.transform.parent.gameObject;

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 死亡 = GameObject.Instantiate(对象) as GameObject;
        死亡.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        死亡.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        死亡.transform.localPosition = new Vector2(0, 0);//设置生成位置
        死亡.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        死亡.transform.SetAsLastSibling();
        死亡.GetComponent<move_MuBei>().复活目标 = 死亡对象;

    }



    public void 生成场景怪物信息(int index, int 总数, int 目标方法)
    {
        GameObject 父物体 = NameMgr.画布.transform.Find("UI/战斗页/场景信息/场景信息面板/Scroll View/Viewport/Content").gameObject;
        父物体.GetComponent<RectTransform>().sizeDelta = new Vector2(784, 260 * 总数);
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 目标方法);
        参数集.Add(1, 父物体);
        参数集.Add(2, index);
        StartCoroutine(bm.LoadABPrefabs("场景怪物信息", 实例化场景怪物信息, 参数集));

    }
    private void 实例化场景怪物信息(GameObject 对象, Dictionary<int, object> 参数集)
    {
        int 目标方法 = (int)参数集[0];
        GameObject 父物体 = 参数集[1] as GameObject;
        int index = (int)参数集[2];

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 怪物信息 = GameObject.Instantiate(对象) as GameObject;
        怪物信息.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        怪物信息.GetComponent<RectTransform>().sizeDelta = new Vector2(780, 260);//recttransform必不可少的属性(半知半解)
        怪物信息.transform.localPosition = new Vector2(0, index * (-260) + 130);//设置生成位置

        怪物信息.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        //怪物信息.transform.SetAsLastSibling();
        怪物信息.GetComponent<MonsterData>().方法索引 = 目标方法;

    }


    public void 生成场景怪物信息方法(I_monster mon) {
        int index = 1;
        int All = 0;
        combat cb = NameMgr.cb;

        if (mon.怪物1赋值(cb)!=0)
            All++;
        if (mon.怪物2赋值(cb) != 0)
            All++;
        if (mon.怪物3赋值(cb) != 0)
            All++;
        if (mon.怪物4赋值(cb) != 0)
            All++;
        if (mon.怪物5赋值(cb) != 0)
            All++;


        if (mon.怪物1赋值(cb) != 0)
            生成场景怪物信息(index++, All, 1);
        if (mon.怪物2赋值(cb) != 0)
            生成场景怪物信息(index++, All, 2);
        if (mon.怪物3赋值(cb) != 0)
            生成场景怪物信息(index++, All, 3);
        if (mon.怪物4赋值(cb) != 0)
            生成场景怪物信息(index++, All, 4);
        if (mon.怪物5赋值(cb) != 0)
            生成场景怪物信息(index++, All, 5);
    }

    public void 清空场景怪物信息() {
        Text 场景名字 = gameObject.transform.Find("UI/战斗页/场景信息/场景信息面板/title/Text").GetComponent<Text>();
        场景名字.text = SceneManager.GetActiveScene().name;
        GameObject 场景怪物信息表 = gameObject.transform.Find("UI/战斗页/场景信息/场景信息面板/Scroll View/Viewport/Content").gameObject;
        操作子物体(场景怪物信息表, 3);
    }

    public void 生成掉落项(GameObject 父物体, string 物品名, int index, int all)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        参数集.Add(1, 物品名);
        参数集.Add(2, index);
        参数集.Add(3, all);
        StartCoroutine(bm.LoadABPrefabs("掉落项", 实例化掉落项, 参数集));

    }
    private void 实例化掉落项(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject;
        string 物品名 = 参数集[1] + "";
        int index = (int)参数集[2];
        int all = (int)参数集[3];
        Prop_bascis 物品 = pm.检索物品(物品名);


        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 掉落项 = GameObject.Instantiate(对象) as GameObject;
        掉落项.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        掉落项.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 120);//recttransform必不可少的属性(半知半解)
        掉落项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        int y坐标;
        if (index % 3 != 0)
            y坐标 = index / 3;
        else
            y坐标 = (index / 3) + 1;
        掉落项.transform.localPosition = new Vector2((index % 3) * 150 - 150, y坐标 * (-120) + 60);//设置生成位置

        //信息填充
        Text 名字 = 掉落项.transform.Find("名字").GetComponent<Text>();
        名字.text = 物品.name;
        名字.color = bm.转换颜色(bm.Xstoi(物品.qua));
        掉落项.GetComponent<DiaoLuoItem>().index = index;
        掉落项.GetComponent<DiaoLuoItem>().all = all;

        //绑定事件
        bm.Banding(掉落项, 生成物品信息_无参);

    }


    public void 生成宠物项(int index, Pet_Data 宠物, string UID,GameObject 父物体)
    {
        GameObject 宠物资源 = Resources.Load<GameObject>("部件/宠物项");

        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 宠物项 = GameObject.Instantiate(宠物资源) as GameObject;
        宠物项.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        宠物项.GetComponent<RectTransform>().sizeDelta = new Vector2(280, 100);//recttransform必不可少的属性(半知半解)
        宠物项.transform.localPosition = new Vector2(index % 3 * 285 - 285, 105- index / 3 *105);//设置生成位置
        宠物项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小

        //信息填充
        Text 宠物名字 = 宠物项.transform.Find("名字").GetComponent<Text>();
        宠物名字.text = 宠物.name;
        宠物名字.color = bm.转换颜色(bm.Xstoi(宠物.qua));
        Image 锁图标 = 宠物项.transform.Find("锁图标").GetComponent<Image>();
        if (宠物.islock == "1")
            锁图标.sprite = Resources.Load<Sprite>("图标/关锁图标");
        PetItem item = 宠物项.GetComponent<PetItem>();
        item.宠物 = 宠物;
        item.UID = UID;
        myData = io_.load();
        if (myData.出战宠物UID!=null&&myData.出战宠物UID.Equals(UID))
            宠物项.transform.Find("出战显示").gameObject.SetActive(true);
        else
            宠物项.transform.Find("出战显示").gameObject.SetActive(false);
    }




    public void 生成升级特效(GameObject 父物体,string index)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        参数集.Add(1,index);
        StartCoroutine(bm.LoadABPrefabs("升级特效", 实例化升级特效, 参数集));

    }
    private void 实例化升级特效(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject;
        string index = 参数集[1] + "";


        //实例化(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 升级特效 = GameObject.Instantiate(对象) as GameObject;
        升级特效.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        升级特效.transform.SetParent(父物体.transform, false);//设置父物体, 第二个参数可以不用定义许多RectTransform属性
        升级特效.transform.localPosition = new Vector3(0, -50, 0);//设置生成位置
        升级特效.transform.localScale = new Vector3(60, 60, 60);//设置生成的大小

        //1为战斗页用的升级特效,2为状态页用的
        if (index.Equals("2")) {
            升级特效.transform.localPosition = new Vector3(0, -120, 0);//设置生成位置
            升级特效.transform.localScale = new Vector3(90, 90, 90);//设置生成的大小
            foreach (Renderer 子渲染器 in 升级特效.transform.GetComponentsInChildren<Renderer>()) {
                子渲染器.sortingOrder = 30;
            }
        }
        升级特效.transform.SetAsLastSibling();


        //绑定材质
        Material 光阵材质1 = (Material)Resources.Load<Material>("材质球/CFX3_AuraRunic ADD");
        Material 光阵材质2 = (Material)Resources.Load<Material>("材质球/CFX3_GlowStar ADD");
        Material 光阵材质3 = (Material)Resources.Load<Material>("材质球/CFX_RayRounded");
        Material 粒子材质 = (Material)Resources.Load<Material>("材质球/glow_add_color");
        //升级特效.GetComponent<ParticleSystemRenderer>().material = 光阵材质1;
        升级特效.transform.Find("Spiral_02.1").GetComponent<Transform>().localRotation = Quaternion.Euler(90f, 0f, 0f);
        升级特效.transform.Find("CFX3 Small Aura").GetComponent<ParticleSystemRenderer>().material = 光阵材质1;
        升级特效.transform.Find("CFX3 Stars").GetComponent<ParticleSystemRenderer>().material = 光阵材质2;
        升级特效.transform.Find("CFX3 Spikes").GetComponent<ParticleSystemRenderer>().material = 光阵材质3;
        升级特效.transform.Find("Spiral_02.1/particle glow master").GetComponent<ParticleSystemRenderer>().material = 粒子材质;
        升级特效.transform.Find("Spiral_02.1/particle glow master/particle glow slave birth trail").GetComponent<ParticleSystemRenderer>().material = 粒子材质;
        升级特效.transform.Find("Spiral_02.1/particle glow master/particle glow slave birth spark").GetComponent<ParticleSystemRenderer>().material = 粒子材质;

    }

    public void 加载宠物()
    {
        GameObject 宠物 = GameObject.FindGameObjectWithTag("宠物");
        if (宠物 != null)//如果有了就销毁
            Destroy(宠物);
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(bm.LoadABPrefabs("血条", 实例化宠物, 参数集));

    }

    private void 实例化宠物(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = GameObject.Find("combat(Clone)/role/r5").gameObject;
        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 宠物 = GameObject.Instantiate(对象) as GameObject;
        宠物.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        宠物.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        宠物.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        宠物.tag = "宠物";
        宠物.name = "宠物";
        role_Data myData = io.GetInstance().load();
        Pet_Data petData =pem.返回宠物(myData.出战宠物UID);
        Text 宠物名字文本 = 宠物.transform.Find("name").GetComponent<Text>();
        宠物名字文本.color = basicMgr.GetInstance().转换颜色(bm.Xstoi(petData.qua));
        宠物名字文本.text = petData.name;
    }


    public void 生成技能框(GameObject 父物体, string name)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        参数集.Add(1, name);
        StartCoroutine(bm.LoadABPrefabs("技能框", 实例化技能框, 参数集));

    }

    private void 实例化技能框(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject;
        string name = 参数集[1] as string;
        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 技能框 = GameObject.Instantiate(对象) as GameObject;
        技能框.transform.SetParent(父物体.transform.parent, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        技能框.GetComponent<RectTransform>().sizeDelta = new Vector2(260, 100);//recttransform必不可少的属性(半知半解)
        技能框.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        技能框.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        技能框.transform.SetAsLastSibling();

        技能框.transform.Find("Text").GetComponent<Text>().text = name;
        StartCoroutine(计时销毁(0.8f, 技能框));

    }

    public IEnumerator 计时销毁(float time, GameObject 目标) {
        yield return new WaitForSeconds(time);
        Destroy(目标);
    }


    public void 背包使用CD道具() {
        string 名字 = GameObject.FindGameObjectWithTag("选中").transform.Find("名字").GetComponent<Text>().text;
        刷新背包CD道具UI(名字);
        pm.背包CD道具持久化(名字);//dao层
    }

    public void 刷新背包CD道具UI(string 名字) {
        myData = io_.load();
        GameObject 道具图标 = GameObject.Find("道具图标");
        Image 图标 = 道具图标.GetComponent<Image>();
        CDSlider cs = GameObject.Find("道具拉条").GetComponent<CDSlider>();
        if (!myData.材料背包.ContainsKey(名字)) {         
            图标.sprite = Resources.Load("图标/灰色背景图标", typeof(Sprite)) as Sprite;
            道具图标.transform.parent.Find("数量").GetComponent<Text>().text = "";
            cs.pb = null;
            cs.初始化拉条数据();
            if (myData.记录.ContainsKey("CD道具"))
            {
                myData.记录.Remove("CD道具");
                io_.save(myData);
                if (NameMgr.人物 != null)
                {
                    combat cb = NameMgr.人物.GetComponent<combat>();
                    cb.myData = myData;
                }
            }
            return;
        }
        Prop_bascis 物品 = myData.材料背包[名字];
        cs.pb = 物品;
        cs.初始化拉条数据();
        if (物品.icon.Equals("血药"))
            图标.sprite = Resources.Load("图标/血药图标", typeof(Sprite)) as Sprite;
        else if (物品.icon.Equals("暗器"))
            图标.sprite = Resources.Load("图标/暗器图标1", typeof(Sprite)) as Sprite;
        道具图标.transform.parent.Find("数量").GetComponent<Text>().text = bm.Xstoi(物品.num) + "";
    }


    public void 自动使用CD道具(role_Data myData,combat cb) {
        if (myData.记录.ContainsKey("CD道具")&& !myData.记录["CD道具"].Equals(""))
        {
            Prop_bascis 物品 = myData.材料背包[myData.记录["CD道具"]];
            if (!sa.CD集合.ContainsKey("CD道具"))
            {
                if (物品.icon.Equals("血药") && bm.Xstof(cb.剩余血量) / bm.Xstof(cb.血量) > 0.7f)
                    return;
                else if (物品.icon.Equals("暗器") && 现有怪物集合.Count == 0)
                    return;

                sa.使用CD道具();

            }
        }
    }


    public void 暗器伤害(int 伤害) {
        cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
        GameObject 目标;
        if (cb.目标名字.Equals(""))
            目标 = mmgr.随意寻找一个目标("怪物");
        else
            目标 = GameObject.Find(cb.目标名字);
        cb.扣血行为(目标, "暗器", 伤害);
    }


    public void 被动技能伤害(int 伤害)
    {
        cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
        GameObject 目标;
        if (cb.目标名字.Equals(""))
            目标 = mmgr.随意寻找一个目标("怪物");
        else
            目标 = GameObject.Find(cb.目标名字);
        cb.扣血行为(目标, "暗器", 伤害);
        生成技能特效_单体(目标);
    }

    public void 生成活动项(GameObject 父物体, string name,int index,string type)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 父物体);
        参数集.Add(1, name);
        参数集.Add(2, index);
        参数集.Add(3, type);
        StartCoroutine(bm.LoadABPrefabs("活动项", 实例化活动项, 参数集));

    }

    private void 实例化活动项(GameObject 对象, Dictionary<int, object> 参数集)
    {
        GameObject 父物体 = 参数集[0] as GameObject;
        string name = 参数集[1] as string;
        int index = (int)参数集[2];
        string type= 参数集[3]+"";
        float 宽度 = 父物体.GetComponent<RectTransform>().sizeDelta.x;
        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 活动项 = GameObject.Instantiate(对象) as GameObject;
        活动项.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        活动项.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 184);//recttransform必不可少的属性(半知半解)
        活动项.transform.localPosition = new Vector2(宽度 / 2.0f * -1 + 100 + (index * 200), 0);//设置生成位置
        活动项.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        活动项.transform.SetAsLastSibling();

        活动项.transform.Find("名字").GetComponent<Text>().text = name;
        Sprite 图标 = Resources.Load<Sprite>("活动/" + name + "图标");
        if (图标 == null)
        {
            if (type.Equals("事件"))
            {
                图标 = Resources.Load<Sprite>("图标/警告图标");
            }
            else
            {
                图标 = Resources.Load<Sprite>("活动/默认活动图标");
            }
        }
        活动项.transform.Find("图标").GetComponent<Image>().sprite = 图标;
        if (type.Equals("活动"))
        {
            Activities at = GameObject.Find("活动界面").GetComponent<Activities>();
            bm.Banding(活动项, at.点击活动图标);
        }
        else if (type.Equals("事件")) {
            Event_UI eu = GameObject.Find("事件界面").GetComponent<Event_UI>();
            bm.Banding(活动项, eu.点击事件图标);
        }

    }


    public void 生成引导(Vector2 坐标)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 坐标);
        //参数集.Add(1, 事件中心名称);
        StartCoroutine(bm.LoadABPrefabs("引导", 实例化引导, 参数集));

    }

    private void 实例化引导(GameObject 对象, Dictionary<int, object> 参数集)
    {
        Vector2 坐标 = (Vector2)参数集[0];
        //string 事件中心名称 = 参数集[1] as string;

        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 引导 = GameObject.Instantiate(对象) as GameObject;
        实例化对象池.Add("引导", 引导);
        引导.transform.SetParent(NameMgr.画布.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        引导.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        引导.transform.localPosition = 坐标;//设置生成位置
        引导.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        引导.transform.SetAsLastSibling();

        bm.Banding(引导.transform.Find("遮罩/点击块").gameObject, 触发引导事件中心);

    }


    public void 生成拉条(string 物品名称,string UID ,int 单价,int 最大数量,string 货币,string 类型)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 物品名称);
        参数集.Add(1, 单价);
        参数集.Add(2, 最大数量);
        参数集.Add(3, 货币);
        参数集.Add(4, 类型);
        参数集.Add(5, UID);
        StartCoroutine(bm.LoadABPrefabs("拉条", 实例化拉条, 参数集));

    }

    private void 实例化拉条(GameObject 对象, Dictionary<int, object> 参数集)
    {
        string 物品名称 = 参数集[0] + "";
        string UID = 参数集[5] + "";
        int 单价 = int.Parse(参数集[1] + "");
        int 最大数量 = int.Parse( 参数集[2] + "");
        string 货币= 参数集[3] + "";
        string 类型= 参数集[4] + "";

        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 拉条 = GameObject.Instantiate(对象) as GameObject;
        拉条.transform.SetParent(NameMgr.画布.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        拉条.GetComponent<RectTransform>().sizeDelta = new Vector2(450, 300);//recttransform必不可少的属性(半知半解)
        拉条.transform.localPosition = new Vector2(0, 0);//设置生成位置
        拉条.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        拉条.transform.SetAsLastSibling();

        Latiao lt = 拉条.GetComponent<Latiao>();
        lt.物品名称 = 物品名称;
        lt.UID = UID;
        lt.单价 = 单价;
        lt.最大数量 = 最大数量;
        lt.货币 = 货币;
        lt.类型 = 类型;

    }



    private void 触发引导事件中心() {
        实例化对象池["引导"].gameObject.SetActive(false);
        em.EventTrigger("引导" );
        em.ClearEventListener("引导" );
        em.EventTrigger("生成引导");
        em.ClearEventListener("生成引导");
    }


    public void 关闭对话框() {
        NewPlay np = NewPlay.GetInstance();
        np.关闭对话框();
    }



    public void 生成提示(string 名字, Sprite 立绘,string 内容)
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        参数集.Add(0, 名字);
        参数集.Add(1, 立绘);
        参数集.Add(2, 内容);
        StartCoroutine(bm.LoadABPrefabs("提示", 实例化提示, 参数集));

    }

    private void 实例化提示(GameObject 对象, Dictionary<int, object> 参数集)
    {
        string  名字 = 参数集[0] as string;
        Sprite 立绘 = 参数集[1] as Sprite;
        string 内容 = 参数集[2] as string;

        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 提示 = GameObject.Instantiate(对象) as GameObject;
        实例化对象池.Add("提示", 提示);
        提示.transform.SetParent(NameMgr.画布.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        提示.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);//recttransform必不可少的属性(半知半解)
        提示.transform.localPosition = new Vector3(0,0,0);//设置生成位置
        提示.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        提示.transform.SetAsLastSibling();

        提示.transform.Find("角色立绘").GetComponent<Image>().sprite = 立绘;
        提示.transform.Find("角色立绘/名字/Text").GetComponent<Text>().text = 名字;
        提示.transform.Find("描述").GetComponent<Text_DaYin>().str = 内容;
        Text_DaYin tdy = 提示.transform.Find("描述").GetComponent<Text_DaYin>();
        tdy.wenZiText = 提示.transform.Find("描述/Text").GetComponent<Text>();
        tdy.语速 = 0.05f;
        tdy.playText();
    }

    public void 生成剧情画布()
    {
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        //参数集.Add(1, 事件中心名称);
        StartCoroutine(bm.LoadABPrefabs("剧情_画布", 实例化剧情画布, 参数集));

    }

    private void 实例化剧情画布(GameObject 对象, Dictionary<int, object> 参数集)
    {


        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 剧情画布 = GameObject.Instantiate(对象) as GameObject;
        剧情画布.transform.SetParent(NameMgr.画布.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        剧情画布.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 1920);//recttransform必不可少的属性(半知半解)
        剧情画布.transform.localPosition = new Vector3(0,0,0);//设置生成位置
        剧情画布.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        剧情画布.transform.SetAsLastSibling();

    }

    public void 存档记录(string 事件名,string 事件值) {
        myData = io_.load();
        if (myData.记录.ContainsKey(事件名))
        {
            myData.记录[事件名] = 事件值;
        }
        else {
            myData.记录.Add(事件名, 事件值);
        }
        io_.save(myData);
    }

    public void 存档记录值相加(string 事件名, int 事件值) {
        myData = io_.load();
        if (myData.记录.ContainsKey(事件名))
        {
            myData.记录[事件名] = int.Parse(myData.记录[事件名])+事件值+"";
        }
        else
        {
            myData.记录.Add(事件名, 事件值+"");
        }
        io_.save(myData);
    }


    public void 敬请期待()
    {
        关闭杂项();
        string str = "敬请期待...";
        生成对话框(str, 0, 0.08f, "敬请期待");
    }

    private void OnApplicationQuit() {
        dm.储存缓存数据();
    }




    private void OnDisable()
    {
        运行中的时间线程.Clear();
        myData = io_.load();
        myData.CD = 储存_单精度转换为字符串(sa.CD集合);
        io_.save(myData);
        pem.宠物全部满血();
    }
}
