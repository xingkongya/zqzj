using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Register_ : MonoBehaviour
{
    private PropMgr pm;
    private G_Util gut;
    private basicMgr bm;
    private io io_;
    private SkillApplicator sa;



    public void Awake()
    {
        gut = gameObject.GetComponent<G_Util>();
        pm = PropMgr.GetInstance();
        bm = basicMgr.GetInstance();
        io_ = io.GetInstance();
        sa = SkillApplicator.GetInstance();
        
       
    }




    public void Start()
    {
        //加载excel文件
        Ab包预加载();
    }

    private void Ab包预加载()
    {
        StartCoroutine(bm.LoadAB("ui"));
    }



    public void 新的旅程()
    {
        string filePath = io.GetInstance().DataPath;
        FileInfo file = new FileInfo(filePath);
        if (file.Exists)
            GameObject.Find("new").transform.Find("提示1").gameObject.SetActive(true);
        else
        {
            role_Data myData = new role_Data();
            io.GetInstance().save(myData);
            生成画布();
            第一次加载();
        }

    }

    public void 继续旅程()
    {
        string filePath = io.GetInstance().DataPath;
        FileInfo file = new FileInfo(filePath);
        //检查到存档
        if (file.Exists)
        {
            role_Data myData = io.GetInstance().load();
            生成画布();
            //加载UI();

        }
        //未检测到存档
        else
            GameObject.Find("load").transform.Find("提示").gameObject.SetActive(true);

    }

    public void 删除存档()
    {
        string filePath = Application.persistentDataPath + @"/MyData.json";
        FileInfo file = new FileInfo(filePath);
        file.Delete();
        PlayerPrefs.DeleteAll();

    }

    public void 退出游戏()
    {
        Application.Quit();

    }


    public void 第一次加载() {
        //加载音量
        role_Data myData = io_.load();
        AudioSource 主音乐 = GameObject.Find("audio").GetComponent<AudioSource>();
        if (myData.记录.ContainsKey("背景音乐"))
        {
            主音乐.mute = false;
            主音乐.volume = int.Parse(myData.记录["背景音乐"]) / 100.0f;
        }
        else
        {
            myData.记录.Add("背景音乐", "20");
            主音乐.volume = 0.2f;
        }

        if (myData.记录.ContainsKey("游戏音效"))
        {
            主音乐.mute = false;
            主音乐.volume = int.Parse(myData.记录["游戏音效"]) / 100.0f;
        }
        else
        {
            myData.记录.Add("游戏音效", "50");
        }
        EvenMgr.GetInstance().存档添加事件("三年之约", "无限");
        EvenMgr.GetInstance().存档添加事件("山海小师弟", "无限");
    }

    public void 加载游戏初始化() {
        //加载音量
        role_Data myData = io_.load();
        UtilMaGr.GetInstance().存档检测(myData);
        AudioSource 主音乐 = GameObject.Find("audio").GetComponent<AudioSource>();
        if (myData.记录.ContainsKey("背景音乐"))
        {
            主音乐.mute = false;
            主音乐.volume = int.Parse(myData.记录["背景音乐"]) / 100.0f;
        }
        else
            主音乐.mute = true;

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
        DataMgr.GetInstance().本地对象.Add("画布", 画布);
        加载UI();
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
        /*if (myData.记录.ContainsKey("关闭天气") && myData.记录["关闭天气"] != 1 && gameObject.CompareTag("野外"))//加载天气,每次切换场景都加载一次
            gut.生成天气();*/
      
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
        GameObject 父物体 = GameObject.Find("combat(Clone)/role/r2").gameObject;
        //实例化角色(可优化...find步骤可换成从缓存中查找).记得按分辨率的不同调整缩放
        GameObject 角色 = GameObject.Instantiate(对象) as GameObject;
        角色.transform.SetParent(父物体.transform, false);//将画布设置为父物体, 第二个参数可以不用定义许多RectTransform属性
        角色.transform.localPosition = new Vector3(0, 0, 0);//设置生成位置
        角色.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小
        角色.tag = "人物";
        角色.name = "人物";
        DataMgr.GetInstance().本地对象.Add("主角",角色);

        role_Data myData = io.GetInstance().load();
        if (myData.出战宠物UID!=null )
            gut.加载宠物();
        SceneManager.LoadScene(myData.登录场景);
    }


   
        public void 加载UI()
    {
        if (GameObject.Find("UI(Clone)"))//如果有UI了就不加载
            return;
        Dictionary<int, object> 参数集 = new Dictionary<int, object>();
        StartCoroutine(basicMgr.GetInstance().LoadABPrefabs("ui", 实例化UI, 参数集));

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
        UI_.name="UI";


        //显示按钮
       /* role_Data myData = io.GetInstance().load();
        if (gameObject.tag == "特殊场景")
        {
            UI_.transform.Find("战斗页/2级画布/IMG_button/关闭背景").gameObject.SetActive(true);
            GameObject 背景 = UI_.transform.Find("combat_bg").gameObject;
            背景.GetComponent<Image>().color = basicMgr.GetInstance().改变透明度(背景, 0);
        }
        else if (gameObject.tag != "城市" && gameObject.tag != "特殊场景")
        {
            UI_.transform.Find("战斗页/2级画布/IMG_button/关闭天气").gameObject.SetActive(true);
            if (myData.记录 != null && myData.记录.ContainsKey("关闭天气") && myData.记录["关闭天气"].Equals("1"))
            {
                UI_.transform.Find("战斗页/2级画布/IMG_button/关闭天气/开启").gameObject.SetActive(true);
                UI_.transform.Find("战斗页/2级画布/IMG_button/关闭天气/关闭").gameObject.SetActive(false);
            }
            else
            {
                UI_.transform.Find("战斗页/2级画布/IMG_button/关闭天气/开启").gameObject.SetActive(false);
                UI_.transform.Find("战斗页/2级画布/IMG_button/关闭天气/关闭").gameObject.SetActive(true);
            }
        }*/


        //添加绑定
        GameObject 背包按钮 = UI_.transform.Find("战斗页/bag/bag_bg_pic").gameObject;
        GameObject 事件按钮 = UI_.transform.Find("战斗页/event/event_bg_pic").gameObject;
        GameObject 自动战斗按钮 = UI_.transform.Find("战斗页/combat_bg/2级画布/自动战斗/Text").gameObject;
        GameObject 角色面板按钮 = UI_.transform.Find("战斗页/state/head/head_bg").gameObject;//combat_bg
        GameObject 战斗背景 = UI_.transform.Find("战斗页/combat_bg").gameObject;
       /* GameObject 关闭背景按钮 = UI_.transform.Find("战斗页/2级画布/IMG_button/关闭背景/关闭").gameObject;
        GameObject 开启背景按钮 = UI_.transform.Find("战斗页/2级画布/IMG_button/关闭背景/开启").gameObject;
        GameObject 关闭天气按钮 = UI_.transform.Find("战斗页/2级画布/IMG_button/关闭天气/关闭").gameObject;
        GameObject 开启天气按钮 = UI_.transform.Find("战斗页/2级画布/IMG_button/关闭天气/开启").gameObject;*/
        GameObject 幻界按钮 = UI_.transform.Find("menu/world/blue_bg_pic").gameObject;
        GameObject 状态按钮 = UI_.transform.Find("menu/state/blue_bg_pic").gameObject;
        GameObject 战斗按钮 = UI_.transform.Find("menu/combat/blue_bg_pic").gameObject;
        GameObject 设置按钮 = UI_.transform.Find("menu/setting/blue_bg_pic").gameObject;
        GameObject 绝招按钮 = UI_.transform.Find("战斗页/combat_bg/2级画布/绝招/遮罩/Button").gameObject;
        GameObject 道具按钮 = UI_.transform.Find("战斗页/combat_bg/2级画布/道具/遮罩/Button").gameObject;

        GameObject 画布 = NameMgr.画布;
        gut = 画布.GetComponent<G_Util>();

        bm.Banding(自动战斗按钮, gut.自动战斗);
        bm.Banding(背包按钮, gut.点击背包);
        //bm.Banding(事件按钮, gut.敬请期待);
        bm.Banding(角色面板按钮, gut.生成角色面板);
        bm.Banding(战斗背景, gut.关闭杂项);
       /* bm.Banding(关闭背景按钮, gut.关闭背景);
        bm.Banding(开启背景按钮, gut.开启背景);*/
        //bm.Banding(关闭天气按钮, gut.关闭天气);
        //bm.Banding(开启天气按钮, gut.开启天气);
        bm.Banding(幻界按钮, gut.跳转至幻界);
        bm.Banding(状态按钮, gut.打开状态页);
        bm.Banding(战斗按钮, gut.返回战斗页);
        bm.Banding(设置按钮, gut.生成设置界面);
        bm.Banding(绝招按钮, sa.使用绝招);
        bm.Banding(道具按钮, sa.使用CD道具);

        gut.加载UI信息(UI_);
        DataMgr.GetInstance().本地对象.Add("UI", UI_);
        加载游戏初始化();
        生成战斗场地();
       
    }
}
