using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class jq_kaoshan : MonoBehaviour
{
    //基础
    private float timer;//定时器
    private Dictionary<string, UnityAction> 村长选项 = new Dictionary<string, UnityAction>();
    private io io_;

    //自定义
    private G_Util ut;

    private void Awake()
    {
        io_ = io.GetInstance();
        ut =NameMgr.画布.GetComponent<G_Util>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ut.移动与坐标.Add("右", "村口小道");
        ut.移动与坐标.Add("下", "木屋(家)");
        ut.刷新移动与坐标();

        timer = Random.Range(1.0f, 3.0f); // 随机秒数

        role_Data myData = io_.load();
        myData.复活城市 = "桃源村";
        io_.save(myData);

        //剧情选择按钮初始化
        if (PlayerPrefs.GetInt("打听父母") < 3) {
            村长选项.Add("打听父母", 村长剧情0);
        }
        else {
            村长选项.Add("如何变强", 村长剧情3);
         }

        if (PlayerPrefs.GetInt("村长的帮助") == 0)
        {
            村长选项.Add("村长的帮助", 村长剧情4);
        }
        if (PlayerPrefs.GetInt("村长馒头") <= 9)
        {
            村长选项.Add("寻求支持", 村长剧情5);
        }
       
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
                if (gameObject.name.Equals("牛铁匠"))//判断父物体
                    牛铁匠气泡();
                else if (gameObject.name.Equals("王二"))
                    王二气泡();
                timer = 2.0f; // 显示2秒
            }
            
        }

    }

    void 牛铁匠气泡() {
        string str = "砰~砰~砰~砰~";
        ut.生成气泡(str, gameObject);
        }

    void 王二气泡()
    {
        string str = "绝世神功八折出售";
        ut.生成气泡(str, gameObject);
    }

    public void 村长对话索引() {
        int index_bt1 = PlayerPrefs.GetInt("打听父母");
        int index_mq = PlayerPrefs.GetInt("母亲");

        if (index_bt1 == 1)
            村长剧情1();
        else if (index_bt1 == 2)
            村长剧情2();
        else if (index_bt1 == 3)
            村长选择项(村长选项);
        else if (index_mq >1&&( index_bt1==0|| index_bt1>= 4)&& 村长选项.Count != 0 )
            村长选择项(村长选项);
        else
            村长对话();

    }

    public void 牛铁匠对话索引()
    {
            牛铁匠选择项();
    }

    public void 王二对话索引()
    {
        int index_talk = PlayerPrefs.GetInt("王二");
        王二剧情0(index_talk, "王二");
        
    }



    private void 村长对话() {
        string str = "加油,娃.刚把爹!";
        ut.生成对话框(str, 0, 0.08f, "村长对话");      
    }
    private void 村长剧情0()
    {
        string str = "咦,娃,你失忆了吗,哈哈!...你父亲是村里最强大的猎手,走过许多地方,在山上猎过大鸟,在海边猎过大蛇,迎娶当初作为村花的你母亲,把我们羡慕的呀.";
        ut.生成对话框(str, 0, 0.07f, "打听父母");
        ut.关闭杂项();
    }

    private void 村长剧情1()
    {
        string str = "你的青梅竹马-李翠花,当初她母亲可是你父亲的追求者,可是你父亲只爱你的母亲,但是你们两家关系一直都很好,还结下了娃娃亲...";
        ut.生成对话框(str, 1, 0.07f, "打听父母");
    }
    private void 村长剧情2()
    {
        string str = "谁曾想翠花这娃竟然有灵根,拜入了仙宗,本来挺好的...谁曾想她变的太势力了,把她父母接进仙宗后,更是断绝了和村里的关系";
        ut.生成对话框(str, 2, 0.07f, "打听父母");
        村长选项.Remove("打听父母");
        村长选项.Add("如何变强", 村长剧情3);
    }
    private void 村长剧情3()
    {
        string str = "你去找牛铁匠问问,他曾经和翠花家有过节,也看不惯翠花的做派,说要给你大大的支持!";
        ut.生成对话框(str, 3, 0.1f, "打听父母");
        ut.关闭杂项();    
        村长选项.Remove("如何变强");
    }

    public void 村长剧情4()
    {
        EventCenter.GetInstance().AddEventListener("对话后剧情",获得柴刀);
        string str = "娃,别的不说了...这是500铜币,还有这把陪伴了我半生的武器-柴刀,也送你了,我曾经用它砍遍了小树林.小树林的动物至今对我闻风丧胆,哈哈哈!";
        ut.生成对话框(str, 0, 0.08f, "村长的帮助");
        ut.关闭杂项();  
        村长选项.Remove("村长的帮助");
        ut.加金钱(new Dictionary<string, int>() { {"铜币",500 } });
    }

    public void 获得柴刀() {
        PropMgr pm = PropMgr.GetInstance();
        ut.生成获得框("染血的柴刀",1);
        pm.获取特定装备("染血的柴刀",5,2);
    }

    private void 村长剧情5()
    {
        int index = PlayerPrefs.GetInt("村长馒头");
        ut.关闭杂项();
        if (index > 9) {
            string str = "地主家也没余粮了...";
            ut.生成对话框(str, 9, 0.08f, "村长馒头");
            村长选项.Remove("寻求支持");
            return;
        }
        PropMgr pm = PropMgr.GetInstance();
        ut.生成获得框("馒头",1);
        pm.获取物品("馒头", 1);     
        PlayerPrefs.SetInt("村长馒头", index + 1);
    }


    private void 王二剧情0(int index, string index_Name)
    {
        string str = "娃,你来啦,转眼不见,长这么大了哈.想学我的功夫吗,我教你呀...";
        ut.生成对话框(str, index, 0.07f, index_Name);
        王二选择项();
    }


    private void 王二剧情1(int index, string index_Name)
    {
       //死亡剧情
    }

    public delegate void MyDelegate(List<int> 打造图鉴等级);

    private void 牛铁匠选择项() {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("打造装备", 生成打造界面);
        role_Data myData = io_.load();
        if (!myData.记录.ContainsKey("收集柴火") || int.Parse(myData.记录["收集柴火"]) < 50)
        {
            选项信息.Add("收集柴火", 生成收集柴火任务框);
        }
        ut.生成选项框(选项信息,gameObject);
       
    }

    private void 生成收集柴火任务框() {
        role_Data myData = io_.load();
        int 剩余数量 = 50;
        if (myData.记录.ContainsKey("收集柴火"))
        {
            剩余数量 = 50 - int.Parse(myData.记录["收集柴火"]);
        }
        ut.生成场景任务框("收集柴火", "帮我收集柴火,每收集一个木材给你100铜币的报酬!!!\n收集满50个柴火还有额外惊喜哦!\n剩余数量:<color=blue>"+剩余数量+"</color>", "木材", 1, "系数-100-铜币","");
    }

    public void 生成打造界面() {
        List<int> 打造图鉴等级 = new List<int>();
        打造图鉴等级.Add(1);
        ut.生成装备打造界面(打造图鉴等级);
    }

    private void 王二选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("学习技能", 生成技能学习界面);
        ut.生成选项框(选项信息, gameObject);
      
    }

    private void 生成技能学习界面() {
        Dictionary<string, int> 学习表 = new Dictionary<string, int>();
        学习表.Add("初级御力诀", 100);
        学习表.Add("初级锻体术", 100);
        学习表.Add("初级轻身术", 100);
        学习表.Add("铁砂掌", 200);
        ut.生成技能学习界面(学习表);
    }

    private void 村长选择项(Dictionary<string, UnityAction> 选项信息)
    {
        ut.生成选项框(选项信息, gameObject);
               
    }



}
