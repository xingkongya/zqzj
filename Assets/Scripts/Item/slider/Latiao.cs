using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Latiao : MonoBehaviour
{
    public string 物品名称;
    public string UID;
    public int 单价;
    public int 最大数量;
    public string 货币;
    public string 类型;
    private GameObject 图标;
    private Slider 拉条;
    private Text 描述文本;
    private Text 名字文本;
    private GameObject 确定按钮;
    private GameObject 取消按钮;
    private GameObject 减一按钮;
    private GameObject 加一按钮;
    private GameObject 加满按钮;
    private G_Util gut;
    private PropMgr pm;
    private basicMgr bm;


    private void Awake()
    {
        图标 = gameObject.transform.Find("Panel/图标").gameObject;
        拉条 = gameObject.transform.Find("Slider").GetComponent<Slider>();
        描述文本 = gameObject.transform.Find("描述").GetComponent<Text>();
        名字文本 = gameObject.transform.Find("名字").GetComponent<Text>();
        确定按钮 = gameObject.transform.Find("button/确定").gameObject;
        取消按钮 = gameObject.transform.Find("button/取消").gameObject;
        减一按钮 = gameObject.transform.Find("button/减一").gameObject;
        加一按钮 = gameObject.transform.Find("button/加一").gameObject;
        加满按钮 = gameObject.transform.Find("button/加满").gameObject;
        gut = NameMgr.画布.GetComponent<G_Util>();
        pm = PropMgr.GetInstance();
        bm = basicMgr.GetInstance();
    }


    // Start is called before the first frame update
    void Start()
    {
        //加载名字
        名字文本.text = 物品名称;
        名字文本.color = bm.转换颜色(bm.Xstoi( pm.检索物品(物品名称).qua));

        //加载图标
        Prop_bascis 物品 = pm.检索物品(物品名称);
        if (物品!=null&&!物品.name.Equals(""))
        {
            gut.初始化图标(图标.GetComponent<Image>(), 物品);
        }

        //限制最大数量
        拉条.maxValue = 最大数量;

        //绑定事件
        bm.Banding(取消按钮, 取消);
        bm.Banding(确定按钮, 确定);
        bm.Banding(减一按钮, 数字减一);
        bm.Banding(加一按钮, 数字加一);
        bm.Banding(加满按钮, 数字加满);

        //初始化
        //初始化描述文本颜色
        if (类型.Equals("购买") || 类型.Equals("使用道具"))
        {
            描述文本.color = bm.转换颜色(5);
        }
        else if (类型.Equals("购买"))
        {
            描述文本.color = bm.转换颜色(1);
        }
        //初始化value值为1
        拉条.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void 确定() {
        拉条 = gameObject.transform.Find("Slider").GetComponent<Slider>();
        int 当前数量 = (int)拉条.value;
        //购买
        if (类型.Equals("购买")) {
            string 返回值 = pm.失去金钱(货币, 当前数量 * 单价);
            if (返回值.Equals("成功"))
            {
                pm.获取物品(物品名称, 当前数量);
            }
            else {
                gut.生成警告框(返回值);
                return;
            }
            gut.生成警告框("购买成功!");
            取消();
        }
        //出售
        else if (类型.Equals("出售")) {
            string 返回值;
            if (PropMgr.材料表.ContainsKey(物品名称))
            {
                返回值 = pm.失去物品(物品名称, 当前数量);
            }
            else {
                返回值 = pm.失去装备(UID);
            }
            if (返回值.Equals("成功"))
            {
                Dictionary<string, int> 金钱 = new Dictionary<string, int>();
                金钱.Add(货币, 单价 * 当前数量);
                gut.加金钱(金钱);
                gut.生成获得框("铜币", 单价 * 当前数量);
            }
            else
            {
                gut.生成警告框(返回值);
                return;
            }
            gut.生成警告框("出售成功!");
            取消();
            if (DataMgr.GetInstance().本地对象.ContainsKey("背包")) {
                DataMgr.GetInstance().本地对象["背包"].GetComponent<Bag>().初始化背包();
            }
        }
        //使用道具
        else if (类型.Equals("使用道具")) {
            取消();
            string 返回状态 = gut.使用道具(物品名称, 当前数量);
                if (!返回状态.Equals("使用成功"))
                {
                    gut.生成警告框(返回状态);
                    return;
                }
                else
                {
                    gut.生成警告框("使用成功");
                    return;
                }
          
        }
    }


    public void 实时更新数字() {
        if (拉条!=null&&gameObject.activeSelf) {

            gameObject.transform.Find("数量").GetComponent<Text>().text = "("+拉条.value + "/"+拉条.maxValue+")";
            if (类型.Equals("购买"))
            {
                描述文本.text = "-" + 单价 * (int)拉条.value + 货币;
            }
            else if(类型.Equals("出售"))
            {
                描述文本.text = "+" + 单价 * (int)拉条.value + 货币;
            }
            else if (类型.Equals("使用道具"))
            {
                描述文本.text = "使用"+物品名称+"×" +  (int)拉条.value;
            }
        }
    }


    public void 数字加一() {
        if (拉条.value < 拉条.maxValue)
        {
            拉条.value++;
        }
    }

    public void 数字减一()
    {
        if (拉条.value >= 1)
        {
            拉条.value--;
        }
    }

    public void 数字加满()
    {
        拉条.value=拉条.maxValue;
    }

    public void 取消() {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
