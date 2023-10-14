using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class jq_linhaixi : MonoBehaviour
{
    //基础
    private float timer;//定时器
    private Dictionary<string, UnityAction> 秦镇守选项 = new Dictionary<string, UnityAction>();
    private io io_;

    //自定义
    private G_Util ut;

    private void Awake()
    {
        ut =NameMgr.画布.GetComponent<G_Util>();
        io_ = io.GetInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        ut.移动与坐标.Add("左", "临海西郊");
        ut.移动与坐标.Add("右", "临海城");
        ut.刷新移动与坐标();

        timer = Random.Range(1.0f, 3.0f); // 随机秒数
        role_Data myData = io_.load();
        myData.复活城市 = "临海城";
        io_.save(myData);
    }

    // Update is called once per frame
    void Update()
    {
        //生成/显示...气泡计时器
        /*timer -= Time.deltaTime;
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
            
        }*/

    }

    void 杂货商气泡() {
        string str = "走过路过不要错过";
        ut.生成气泡(str, gameObject);
        }

    void 武馆师傅气泡()
    {
        string str = "锻炼肌肉!防止挨揍!";
        ut.生成气泡(str, gameObject);
    }

    void 药师气泡()
    {
        string str = "唐家秘制-含笑半步颠";
        ut.生成气泡(str, gameObject);
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

    public void 御兽师对话索引()
    {
        御兽师对话();
    }



    public delegate void MyDelegate(List<int> 打造图鉴等级);

    private void 铁匠选择项() {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("打造装备", 生成打造界面);
        ut.生成选项框(选项信息,gameObject);
       
    }

 
    private void 生成打造界面() {
        List<int> 打造图鉴等级 = new List<int>();
        打造图鉴等级.Add(30);
        打造图鉴等级.Add(40);
        打造图鉴等级.Add(50);
        ut.生成装备打造界面(打造图鉴等级);
    }

    private void 武馆师傅选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("学习技能", 生成技能学习界面);
        ut.生成选项框(选项信息, gameObject);
      
    }

    private void 药师选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("治疗", ut.治疗);
        选项信息.Add("购买药品", 生成草药购买界面);
        ut.生成选项框(选项信息, gameObject);

    }

    private void 生成技能学习界面() {
        Dictionary<string, int> 学习表 = new Dictionary<string, int>();
        学习表.Add("中级御力诀", 3000);
        学习表.Add("中级锻体术", 3000);
        学习表.Add("中级轻身术", 3000);
        学习表.Add("高级御力诀", 8000);
        学习表.Add("高级锻体术", 8000);
        学习表.Add("高级轻身术", 8000);
        ut.生成技能学习界面(学习表);
    }

    private void 杂货商选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("购买杂货", 生成道具购买界面);
        选项信息.Add("兑换货币", null);
        ut.生成选项框(选项信息, gameObject);
               
    }

    private void 生成草药购买界面()
    {
        Dictionary<string, string> 道具表 = new Dictionary<string, string>();
        道具表.Add("止血草", "无限");
        道具表.Add("金疮药", "无限");
        道具表.Add("补元散", "无限");
        道具表.Add("愈伤膏", "无限");
        道具表.Add("含笑半步颠", "99");
        ut.生成道具购买界面(道具表);
    }

    private void 生成道具购买界面()
    {
        Dictionary<string, string> 道具表 = new Dictionary<string, string>();
        道具表.Add("绣花针", "无限");
        道具表.Add("飞镖", "无限");
        道具表.Add("暗箭", "无限");
        道具表.Add("十字弩", "无限");
        道具表.Add("风雷石", "10");
        道具表.Add("水雷石", "10");
        道具表.Add("火雷石", "10");
        ut.生成道具购买界面(道具表);
    }

    private void 秦镇守选择项()
    {
        
        if (PropMgr.GetInstance().返回背包该物品的数量("签名纸") >= 1)
        {
            秦镇守选项.Add("递交签名纸", 秦镇守剧情0);
        }
       
        ut.生成选项框(秦镇守选项, gameObject);

    }

    private void 秦镇守剧情0()
    {
        string str = "贤侄!!...当年的大恩我一直没忘,有什么需要帮忙的尽管提,我一定尽力完成!\n来人,给公子倒茶!";
        ut.生成对话框(str, 0, 0.07f, "签名纸");
        ut.关闭杂项();
        PropMgr.GetInstance().失去物品("签名纸", 1);
    }

    private void 秦镇守对话()
    {
        string str = "此地禁止动武!";
        ut.生成对话框(str, 0, 0.07f, "秦镇守对话");
        ut.关闭杂项();
    }

    private void 御兽师对话()
    {
        string str = "其实这世间除了剑修,灵修,和体修这三大主流职业之外还有许多副职业.\n这些副职业只存在特定的宗门,比如御兽宗可以学习御兽,药王谷可以炼药,千机门修暗器手法...\n可惜他们都留在大荒域,没有在山海岛这个犄角旮旯留下传承.";
        ut.生成对话框(str, 0, 0.07f, "秦镇守对话");
        ut.关闭杂项();
    }



}
