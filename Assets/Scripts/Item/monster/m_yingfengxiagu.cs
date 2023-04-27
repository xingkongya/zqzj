using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class m_yingfengxiagu : MonoBehaviour,I_monster
{
    //自定义
    private UT_monster utm;
    private G_Util utg;
    public bool isFlash = false;
    private float timer;//定时器
    private GameObject 武痴;
    private EventCenter ec;

    private void Awake()
    {
        ec = EventCenter.GetInstance();
        utm = gameObject.GetComponent<UT_monster>();
        utg = NameMgr.画布.GetComponent<G_Util>();
        timer = UnityEngine.Random.Range(1.0f, 3.0f); // 随机秒数
        武痴 = GameObject.Find("武痴");
    }

    private void OnEnable()
    {
        ec.AddEventListener<combat>("怪物失败", 打败武痴);
    }

    private void Start()
    {
        utg.移动与坐标.Add("右", "临海西郊");
        utg.刷新移动与坐标();
        utg.生成场景怪物信息方法(this);

        
        // 技能与粒度.Clear();

    }

    void FixedUpdate() {
        if (武痴!=null) {
            //生成/显示...气泡计时器
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //判断生成还是销毁气泡
                if (武痴.transform.Find("气泡_位置").childCount != 0 && 武痴.transform.Find("气泡_位置/气泡(Clone)").gameObject.activeSelf)
                {
                    武痴.transform.Find("气泡_位置/气泡(Clone)").gameObject.SetActive(false);
                    timer = UnityEngine.Random.Range(1.0f, 3.0f); // 随机秒数
                }
                else
                {
                    武痴气泡();
                    timer = 2.0f; // 显示2秒
                }

            }
        }
    }

    public void 渡桥对话索引() {
        渡桥选择项();
    }

    private void 渡桥选择项() {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("过桥", 过桥);
        GameObject 渡桥 = GameObject.Find("渡桥");
        utg.生成选项框(选项信息, 渡桥);
    }


    public void 书生对话索引()
    {
        书生选择项();
    }

    private void 书生选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("结伴同游去临海", utg.敬请期待);
        GameObject 渡桥 = GameObject.Find("书生");
        utg.生成选项框(选项信息, 渡桥);
    }

    public void 武痴对话索引()
    {
        武痴选择项();
    }

    private void 武痴选择项()
    {
        Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
        选项信息.Add("切磋", 武痴切磋);
        utg.生成选项框(选项信息, 武痴);
    }

    private void 武痴切磋() {
        GameObject 血条2= 武痴.transform.parent.Find("血条2").gameObject;
        combat cb = 血条2.GetComponent<combat>();
        武痴.SetActive(false);
        血条2.SetActive(true);
        怪物1赋值(cb);
        utm.加载赋值后的属性(血条2.GetComponent<combat>(), false);
        cb.怪物初始化行为();
    }

    private void 打败武痴(combat cb)
    {
        utg.存档记录("武痴", "1");
    }



    public void 过桥() {
        role_Data myData = NameMgr.IO.load();
        if (myData.记录.ContainsKey("武痴"))
        {
            utg.跳转场景("云台");
        }
        else {
            utg.关闭杂项();
            string str = "桥头被武痴占住,你需要打败他才能通行";
            utg.生成对话框(str,0,0.08f,"null");
        }
    }



    private void 武痴气泡()
    {      
        string str = "兄弟别走,来切磋一下";
        utg.生成气泡(str, 武痴);
    }

    public bool 怪物1赋值(combat cb)
    {
        utm.怪物名字 = "武痴";
        utm.怪物品质 = 3;
        utm.是否主动攻击 = true;
        utm.最低等级 = 50;
        utm.最高等级 = 50;
        utm.基础攻击力 = 260;
        utm.基础防御力 = 80;
        utm.基础血量 = 8500;
        utm.基础暴击率 = 10;
        utm.基础回血值 = 200;
        utm.攻击力资质 = 1.0f;
        utm.防御力资质 = 0.5f;
        utm.血量资质 = 1.2f;
        utm.成长 = 3;
        utm.攻击速度 = 1.2f;
        utm.基础经验值 = 25000;
        utm.经验值系数 = 20;
        utm.铜币 = (int)(50 * utm.成长);
        cb.技能与粒度.Clear();//赋值前先初始化
        if (!cb.技能与粒度.ContainsKey("热血沸腾"))
            cb.技能与粒度.Add("热血沸腾", 0.3f);
        if (!cb.技能与粒度.ContainsKey("狂战"))
            cb.技能与粒度.Add("狂战", 0.3f);
        if (!cb.技能与粒度.ContainsKey("强击"))
            cb.技能与粒度.Add("强击", 0.4f);
        utm.添加基础掉落(cb,1);
        return true;
    }

    public bool 怪物2赋值(combat cb)
    {
        return false;
    }

    public bool 怪物3赋值(combat cb)
    {
        return false;
    }

    public bool 怪物4赋值(combat cb)
    {
        return false;

    }

    public bool 怪物5赋值(combat cb)
    {
        return false;
    }

    public bool 怪物6赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public bool 怪物7赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public bool 怪物8赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public bool 怪物9赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    public bool 建筑赋值(combat cb)
    {
        throw new System.NotImplementedException();
    }

    private void OnDisable()
    {
        ec.RemoveEventListener<combat>("怪物失败", 打败武痴);
    }
}
