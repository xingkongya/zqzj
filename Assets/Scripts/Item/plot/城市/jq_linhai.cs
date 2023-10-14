using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class jq_linhai : MonoBehaviour
{
    //基础
    private float timer;//定时器
    private Dictionary<string, UnityAction> 秦镇守选项 ;
    private io io_;

    //自定义
    private G_Util gut;

    private void Awake()
    {
        gut =NameMgr.画布.GetComponent<G_Util>();
        io_ = io.GetInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        gut.移动与坐标.Add("左", "临海城西");
        gut.移动与坐标.Add("右", "临海城东");
        gut.移动与坐标.Add("下", "临海官道");
        gut.刷新移动与坐标();

        timer = Random.Range(1.0f, 3.0f); // 随机秒数
        role_Data myData = io_.load();
        myData.复活城市 = "临海城";
        io_.save(myData);

    }



   

    // Update is called once per frame
    void Update()
    {
        //生成/显示...气泡计时器
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //判断生成还是销毁气泡
            if (gameObject.transform.Find("气泡_位置").childCount!=0 && gameObject.transform.Find("气泡_位置/气泡(Clone)").gameObject.activeSelf)
            {
                gameObject.transform.Find("气泡_位置/气泡(Clone)").gameObject.SetActive(false);
                timer = Random.Range(1.0f, 3.0f); // 随机秒数
            }
            else
            {
                if (gameObject.name.Equals("杂货商"))//判断父物体
                    杂货商气泡();
                else if (gameObject.name.Equals("武馆师傅"))
                    武馆师傅气泡();
                else if (gameObject.name.Equals("药师"))
                    药师气泡();
                timer = 2.0f; // 显示2秒
            }
            
        }

    }

    void 杂货商气泡() {
        string str = "走过路过不要错过";
        gut.生成气泡(str, gameObject);
        }

    void 武馆师傅气泡()
    {
        string str = "锻炼肌肉!防止挨揍!";
        gut.生成气泡(str, gameObject);
    }

    void 药师气泡()
    {
        string str = "妙手回春,嘿嘿~~";
        gut.生成气泡(str, gameObject);
    }

    public void 杂货商对话索引() {
        杂货商选择项();
    }

    public void 铁匠对话索引()
    {
            铁匠选择项();
    }

    public void 武馆师傅对话索引()
    {
        武馆师傅选择项();
    }

    public void 药师对话索引()
    {
        药师选择项();
    }

    public void 秦镇守对话索引()
    {
        秦镇守对话();

        秦镇守选择项初始化();
        if (秦镇守选项.Count > 0)
        {
            秦镇守选择项();
        }
    }


    public void 秦镇守选择项初始化()
    {
        秦镇守选项 = new Dictionary<string, UnityAction>();
        if (PropMgr.GetInstance().返回背包该物品的数量("签名纸") >= 1)
        {
            秦镇守选项.Add("递交签名纸", 秦镇守剧情0);
        }
        if (PlayerPrefs.GetInt("签名纸") >= 1&& PlayerPrefs.GetInt("开启考验")<1)
        {
            秦镇守选项.Add("我想要变强", 秦镇守剧情1);
        }
        if (PlayerPrefs.GetInt("开启考验") >= 1)
        {
            if(PlayerPrefs.GetInt("任务一") < 1)
                秦镇守选项.Add("任务一", 任务一);
            if (PlayerPrefs.GetInt("任务二") < 1)
                秦镇守选项.Add("任务二", 任务二);
            if (PlayerPrefs.GetInt("任务三") < 1)
                秦镇守选项.Add("任务三", 任务三);

            if (PlayerPrefs.GetInt("任务一") >= 1 && PlayerPrefs.GetInt("任务二") >= 1 && PlayerPrefs.GetInt("任务三") >= 1) {
                秦镇守选项.Add("新一轮的任务", 秦镇守剧情2);
            }
        }
    }


    public delegate void MyDelegate(List<int> 打造图鉴等级);

    private void 铁匠选择项() {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("打造装备", 生成打造界面);
        gut.生成选项框(选项信息,gameObject);
       
    }

 
    private void 生成打造界面() {
        List<int> 打造图鉴等级 = new List<int>();
        打造图鉴等级.Add(30);
        //打造图鉴等级.Add(40);
        //打造图鉴等级.Add(50);
        gut.生成装备打造界面(打造图鉴等级);
    }

    private void 武馆师傅选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("学习技能", 生成技能学习界面);
        gut.生成选项框(选项信息, gameObject);
      
    }

    private void 药师选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("治疗", gut.治疗);
        选项信息.Add("购买药品", 生成草药购买界面);
        gut.生成选项框(选项信息, gameObject);

    }

    private void 生成技能学习界面() {
        Dictionary<string, int> 学习表 = new Dictionary<string, int>();
        学习表.Add("中级御力诀", 800);
        学习表.Add("中级锻体术", 800);
        学习表.Add("中级轻身术", 800);
        学习表.Add("鹰爪功", 1500);
        学习表.Add("高级御力诀", 2000);
        学习表.Add("高级锻体术", 2000);
        学习表.Add("高级轻身术", 2000);
        gut.生成技能学习界面(学习表);
    }

    private void 杂货商选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("购买杂货", 生成道具购买界面);
        选项信息.Add("兑换货币", gut.敬请期待);
        gut.生成选项框(选项信息, gameObject);
               
    }

    private void 生成草药购买界面()
    {
        Dictionary<string, string> 道具表 = new Dictionary<string, string>();
        道具表.Add("止血草", "无限");
        道具表.Add("金疮药", "无限");
        道具表.Add("补元散", "无限");
        道具表.Add("愈伤膏", "无限");
        //道具表.Add("含笑半步颠", "99");
        gut.生成道具购买界面(道具表);
    }

    private void 生成道具购买界面()
    {
        Dictionary<string, string> 道具表 = new Dictionary<string, string>();
        道具表.Add("绣花针", "无限");
        道具表.Add("飞镖", "无限");
        道具表.Add("暗箭", "无限");
        道具表.Add("十字弩", "无限");
        //道具表.Add("风雷石", "10");
        //道具表.Add("水雷石", "10");
        //道具表.Add("火雷石", "10");
        gut.生成道具购买界面(道具表);
    }

    private void 秦镇守选择项()
    {
        gut.生成选项框(秦镇守选项, gameObject);

    }

    private void 秦镇守剧情0()
    {
        string str = "贤侄!!...当年的大恩我一直没忘,有什么需要帮忙的尽管提,我一定尽力完成!";
        gut.生成对话框(str, 0, 0.07f, "签名纸");
        gut.关闭杂项();
        PropMgr.GetInstance().失去物品("签名纸", 1);
    }

    private void 秦镇守剧情1()
    {
        string str = "变强吗?没错,实力才是一切的根本!\n我这有一些任务,完成了会有提升实力的奖励!";
        gut.生成对话框(str, 0, 0.07f, "开启考验");
        gut.关闭杂项();
    }

    private void 秦镇守剧情2()
    {
        string str = "前面的任务只是热热身,接下来是我们都会头疼的任务!";
        gut.生成对话框(str, 0, 0.07f, "开启考验二");
        gut.关闭杂项();
    }

    private void 秦镇守对话()
    {
        string str = "此地禁止动武!";
        gut.生成对话框(str, 0, 0.07f, "秦镇守对话");
        gut.关闭杂项();
    }


    private void 任务一()
    {
        gut.关闭杂项();
        gut.生成场景任务框("任务一:清除食人花", "<color=blue>噩梦之森</color>有食人花经常袭击路过的行人,前往噩梦之森收集10个食人花的花冠给我", "食人花的花冠", 10, "百年灵芝", "");
    }

    private void 任务二()
    {
        gut.关闭杂项();
        gut.生成场景任务框("任务二:清剿蟊贼", "<color=blue>临海官道</color>有蟊贼盗取财物,前去将财物取回", "钱袋子", 3, "五帝铜钱", "");
    }

    private void 任务三()
    {
        gut.关闭杂项();
        gut.生成场景任务框("任务三:清剿大蛇", "<color=blue>乱石滩</color>上诞生了大蛇妖魔,请前往除掉,并带来它的伴生物", "听海石", 1, "拜师贴", "");
    }

    public void 秦镇守海底剧情()
    {
        EvenMgr.GetInstance().存档移除事件("开启职业");
        EventCenter.GetInstance().AddEventListener("对话后剧情", 获得听海石);
        string str = "我们发现了这块石头是一把钥匙,只要你在乱石滩使用它...就会开启隐藏的秘境!\n你不是一直想变强吗,秘境里一定有这种宝物,这块石头还给你,去吧!";
        gut.生成对话框(str, 0, 0.07f, "秦镇守对话");
        gut.关闭杂项();

    }

    public void 获得听海石()
    {
        gut.生成获得框("听海石", 1);
        PropMgr.GetInstance().获取物品("听海石", 1);
    }

}
