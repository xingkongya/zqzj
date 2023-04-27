using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

[SerializeField]
public class combat : MonoBehaviour
{
    //基础
    public bool isAttack = false;
    public bool isState = false;//有特殊技能时改变变量,技能用字典啊储存;
    public string 等级;
    public string 攻击力;
    public string 防御力;
    public string 暴击率;
    public string 血量;
    public string 回血值;
    public string 攻击速度_;
    public string 移动速度;
    public string 回血时间;
    public string 固定伤害;
    public string 固定减伤;
    public string 伤害加成;
    public string 当前暴伤加成;
    public string 伤害减免;
    public string 固定吸血;
    public string 吸血加成;
    public string 基础攻击力;
    public string 基础防御力;
    public string 基础暴击率;
    public string 基础血量;
    public string 基础回血值;
    public string 基础攻击速度;
    public string 基础移动速度;
    public string 基础固定伤害;
    public string 基础固定减伤;
    public string 基础伤害加成;
    public string 基础暴伤加成;
    public string 基础伤害减免;
    public string 基础固定吸血;
    public string 基础吸血加成;
    public string 金钱加成;
    public string 经验加成;
    public string 暴伤加成;
    public string 攻击力加成;
    public string 防御力加成;
    public string 血量加成;
    public string 回血值加成;
    public string 剩余血量;
    public string 铜币;
    public string 金币;
    public string xor_等级;
    public string xor_攻击力;
    public string xor_防御力;
    public string xor_暴击率;
    public string xor_血量;
    public string xor_回血值;
    public string xor_剩余血量;
    public string xor_攻击速度_;
    public string xor_回血时间;
    public string xor_固定伤害;
    public string xor_固定减伤;
    public string xor_伤害比率;
    public string xor_当前暴伤加成;
    public string xor_伤害减免;
    public string xor_固定吸血;
    public string xor_吸血加成;
    public string xor_金钱加成;
    public string xor_经验加成;
    public string xor_暴伤加成;
    public string xor_铜币;
    public string xor_金币;
    public bool 攻击标记 = false;
    public bool 技能标记 = false;
    public float timer = 0;
    public float Time_hp = 2.0f;
    private string str_tag;
    public bool 暴击;
    public string 经验值;
    public string 稀有怪概率;
    public string 爆率;



    //引用
    private string 怪物名字_;
    private int 怪物品质;
    public string 目标名字;
    public Dictionary<string, int> 仇恨列表 = new Dictionary<string, int>();
    private Color nowColor;
    private Slider 血条;
    private Vector3 攻击坐标;
    private Vector3 原来坐标;
    public Dictionary<Prop_bascis,int> 掉落集合 =new Dictionary<Prop_bascis, int>();
    private Dictionary<string, string> 经验钱币 = new Dictionary<string, string>();
    private Vector3 bagTf;
    private GameObject 画布;
    private GameObject 目标;
    public List<int> 伤害列表 = new List<int>();
    public List<string> 敌人列表 = new List<string>();
    public Dictionary<string, float> 技能与粒度 = new Dictionary<string, float>();
    public Dictionary<string, float> 技能与CD = new Dictionary<string, float>();
    public Dictionary<string, string> 临时属性 = basicMgr.GetInstance().返回空的战斗属性();
    public List<string> 技能名容器 = new List<string>();

    //自定义
    private UT_monster utm;
    private G_Util ut;
    private m_Refresh mrf;
    private io io_;
    public role_Data myData;
    private SkillApplicator sa;
    private basicMgr bm;
    private PetMgr pem;
    private PropMgr pm;
    private ChatMgr cm;
    private MonsterMgr mmgr;
    private DataMgr dm;
    private EventCenter ec;



    private void Awake()
    {
        血条 = gameObject.GetComponent<Slider>();
        画布 = NameMgr.画布;
        ut = 画布.GetComponent<G_Util>();
        io_ = io.GetInstance();
        sa = SkillApplicator.GetInstance();
        str_tag = gameObject.tag;
        myData = io_.load();
        bm = basicMgr.GetInstance();
        dm = DataMgr.GetInstance();
        pem = PetMgr.GetInstance();
        cm = ChatMgr.GetInstance();
        pm = PropMgr.GetInstance();
        ec = EventCenter.GetInstance();
        mmgr = MonsterMgr.GetInstance();
        xor_暴伤加成 = bm.Xor("1.5");
        xor_等级 = bm.Xor("0");
        xor_攻击力 = bm.Xor("0");
        xor_防御力 = bm.Xor("0");
        xor_暴击率 = bm.Xor("0");
        xor_血量 = bm.Xor("0");
        xor_回血值 = bm.Xor("0");
        xor_剩余血量 = bm.Xor("0");
        xor_攻击速度_ = bm.Xor("0");
        xor_回血时间 = bm.Xor("0");
        xor_固定伤害 = bm.Xor("0");
        xor_固定减伤 = bm.Xor("0");
        xor_伤害比率 = bm.Xor("0");
        xor_当前暴伤加成 = bm.Xor("0");
        xor_伤害减免 = bm.Xor("0");
        xor_固定吸血 = bm.Xor("0");
        xor_吸血加成 = bm.Xor("0");
        xor_金钱加成 = bm.Xor("0");
        xor_经验加成 = bm.Xor("0");
        xor_铜币 = bm.Xor("0");
        xor_金币 = bm.Xor("0");


        
    }

    private void OnEnable()
    {
        if (!gameObject.CompareTag("boss"))
        {
            位置初始化();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        掉落集合.Clear();//初始化之前先清空,防止场景转换后有数据遗留

        原来坐标 = transform.localPosition;
        //人物与怪物判定,人物享有优先攻击权.攻击动画方向不同
        if (gameObject.CompareTag("伙伴") || gameObject.CompareTag("人物") || gameObject.CompareTag("宠物"))
        {
            timer = 0f;
            攻击坐标 = new Vector3(transform.localPosition.x, transform.localPosition.y + 20, transform.localPosition.z);
        }
        else if (gameObject.CompareTag("怪物"))
        {
            timer = (float.Parse(bm.Xor(xor_攻击速度_))) / 2.0f;
            攻击坐标 = new Vector3(transform.localPosition.x, transform.localPosition.y - 20, transform.localPosition.z);
        }
        else if (gameObject.CompareTag("boss"))
        {
            GameObject 图片 = gameObject.transform.Find("图片").gameObject;
            攻击坐标 = new Vector2(图片.transform.localPosition.x, 图片.transform.localPosition.y - 20);
            原来坐标 = 图片.transform.position;
        }
        /* else
         {
             timer = 攻击速度_ / 2;
             攻击坐标 = new Vector3(transform.localPosition.x, transform.localPosition.y + 20, transform.localPosition.z);
         }*/


        int num = GameObject.FindGameObjectsWithTag("场地").Length;

        if (gameObject.CompareTag("人物"))//实例化来的需要实时获取tag值
            人物初始化();
        else if (gameObject.CompareTag("宠物"))
            宠物初始化();
        else if ((gameObject.CompareTag("怪物") || gameObject.CompareTag("boss")) && num == 1)
            怪物初始化();
        else if (gameObject.CompareTag("建筑") && num == 1)
            建筑初始化();


        边框初始化();
    }

    public void 位置初始化() {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }


    public void 边框初始化()
    {
        if (gameObject.transform.Find("边框遮罩/边框") != null)
        {
            Image 边框 = gameObject.transform.Find("边框遮罩/边框").gameObject.GetComponent<Image>();
            //绑定图标
            if (gameObject.CompareTag("怪物"))
            {
                if (怪物名字_ != null && 怪物名字_.Contains("≮"))
                    边框.sprite = Resources.Load("血条边框/boss边框", typeof(Sprite)) as Sprite;
                else
                    边框.sprite = Resources.Load("血条边框/怪物边框", typeof(Sprite)) as Sprite;
            }
            else if (gameObject.CompareTag("建筑"))
                边框.sprite = Resources.Load("血条边框/建筑边框", typeof(Sprite)) as Sprite;
            else if (gameObject.CompareTag("人物"))
                边框.sprite = Resources.Load("血条边框/人物边框", typeof(Sprite)) as Sprite;
            else if (gameObject.CompareTag("宠物"))
                边框.sprite = Resources.Load("血条边框/宠物边框", typeof(Sprite)) as Sprite;
        }
    }


    void 添加失败监听() {
        if (gameObject.CompareTag("人物") || gameObject.CompareTag("宠物"))
            ec.AddEventListener<combat>("角色失败", 角色失败事件);
        else
            ec.AddEventListener<combat>("怪物失败", 怪物失败事件);
    }

    void 移除失败监听()
    {
        if (gameObject.CompareTag("人物") || gameObject.CompareTag("宠物"))
            ec.RemoveEventListener<combat>("角色失败", 角色失败事件);
        else
            ec.RemoveEventListener<combat>("怪物失败", 怪物失败事件);
    }

    void 触发失败监听()
    {
        if (gameObject.CompareTag("人物") || gameObject.CompareTag("宠物"))
            ec.EventTrigger("角色失败", this);
       else
            ec.EventTrigger("怪物失败", this);
    }


    private void OnDisable()
    {
        if (str_tag == "怪物" || str_tag == "建筑" || str_tag == "boss")
        {
            mrf = gameObject.transform.parent.GetComponent<m_Refresh>();
            if (mrf != null)
                mrf.isRefresh = true;
            if (ut.现有怪物集合.ContainsKey(gameObject.name))
                ut.现有怪物集合.Remove(gameObject.name);
        }
        else
        {
            if (ut.现有角色集合.ContainsKey(gameObject.name))
                ut.现有角色集合.Remove(gameObject.name);
        }
    }

    public void 检索倒计时()
    {
        if (技能与CD.Count != 0)
        {
            foreach (string 技能名 in 技能与CD.Keys)
            {
                if (!技能名容器.Contains(技能名))
                    技能名容器.Add(技能名);
            }

            for (int i = 0; i < 技能名容器.Count; i++)
            {
                StartCoroutine(时间线程(技能名容器[i]));
            }
        }
    }


    public IEnumerator 时间线程(string key)
    {
        if (技能与CD.ContainsKey(key))
        {
            float L_time = 技能与CD[key];
            while (true)
            {
                L_time -= Time.deltaTime;
                技能与CD[key] = L_time;
                if (L_time < 0)
                {
                    技能与CD.Remove(key);
                    break;
                }
                yield return 0;
            }
        }
    }

    public void 怪物赋值(string 怪物名字, string 等级, int 怪物品质, bool 是否主动攻击, bool 眩晕状态, string 攻击力, string 防御力, string 血量, string 剩余血量, string 回血值, string 回血时间, string 攻击速度, string 暴击率, string 经验值, string 铜币, string 金币)
    {
        怪物名字_ = 怪物名字;
        this.等级 = 等级;
        this.怪物品质 = 怪物品质;
        isAttack = 是否主动攻击;
        isState = 眩晕状态;
        基础攻击力 = 攻击力;
        基础防御力 = 防御力;
        基础血量 = 血量;
        this.剩余血量 = 剩余血量;
        基础回血值 = 回血值;
        this.回血时间 = 回血时间;
        基础攻击速度 = 攻击速度;
        基础暴击率 = 暴击率;
        this.经验值 = 经验值;
        this.铜币 = 铜币;
        this.金币 = 金币;
        基础伤害加成 = bm.Xor(1.0 + "");
        当前暴伤加成 = bm.Xor(1.5 + "");
        基础伤害减免 = bm.Xor("0.0");
        基础固定伤害 = bm.Xor("0");
        基础固定减伤 = bm.Xor("0");
        基础吸血加成 = bm.Xor("0.0");
        基础固定吸血 = bm.Xor("0");
        金钱加成 = bm.Xor("1.0");
        经验加成 = bm.Xor("1.0");
        基础暴伤加成 = 当前暴伤加成;
    }


    public void 人物初始化()
    {
        role_Data myData = io_.load();
        剩余血量 = bm.Xor("999999999");
        人物属性刷新();
        if (!ut.现有角色集合.ContainsKey(gameObject.name))
            ut.现有角色集合.Add(gameObject.name, gameObject);
    }

    public void 人物属性刷新()
    {
        GameObject UI = GameObject.Find("UI");
        role_Data myData = io_.load();
        gameObject.transform.Find("name").GetComponent<Text>().text = myData.名字;
        //只是基础属性,后面要加上其他属性
        等级 = myData.等级;
        基础攻击力 = bm.Xftos(bm.Xstoi(myData.攻击力) + (bm.Xstoi(myData.等级) - 1) * dm.返回属性系数("攻击")+ ((bm.Xstoi(myData.等级) - 1)*2 * (bm.Xstoi(myData.灵根)-1)));
        基础防御力 = bm.Xftos(bm.Xstoi(myData.防御力) + (bm.Xstoi(myData.等级) - 1) * dm.返回属性系数("防御") + ((bm.Xstoi(myData.等级) - 1) * 1 *(bm.Xstoi(myData.灵根)-1)));
        基础血量 = bm.Xftos(bm.Xstoi(myData.血量) + (bm.Xstoi(myData.等级) - 1) * dm.返回属性系数("血量") + ((bm.Xstoi(myData.等级) - 1) * 30 * (bm.Xstoi(myData.灵根)-1)));

        基础回血值 = bm.Xitos((int)(bm.Xstoi(myData.回血值) + (bm.Xstoi(myData.等级) - 1) * bm.Xstoi(myData.灵根) * 1.5f));
        基础暴击率 = myData.暴击率;
        基础攻击速度 = bm.Xftos(dm.返回属性系数("攻击速度"));
        回血时间 = myData.回血时间;
        基础固定伤害 = myData.固定伤害;
        基础固定减伤 = myData.固定减伤;
        基础伤害加成 = myData.伤害加成;
        基础伤害减免 = myData.伤害减免;
        基础固定吸血 = myData.固定吸血;
        基础吸血加成 = myData.吸血加成;

        dm.加载额外属性(this);
        血条.maxValue = bm.Xstoi(血量);
        ut.刷新经验条UI(UI);
        ut.刷新战斗力UI(UI);
    }




    /// <summary>
    /// 刷新属性的接口,可以加载临时属性
    /// </summary>
    public void 属性刷新(Dictionary<string,string> 属性) {

        //初始化属性列表里未添加的属性,防止累加
        固定减伤 = bm.Xor("0");
        伤害减免 = bm.Xor("0.0");
        暴伤加成 = bm.Xor("1.5");
        经验加成 = bm.Xor("100.0");
        金钱加成 = bm.Xor("100.0");
        稀有怪概率 = bm.Xor("100");
        爆率 = bm.Xor("100");



        string atk = bm.Xor("0");
        string def = bm.Xor("0");
        string hp = bm.Xor("0");
        string hpr = bm.Xor("0");
        string atk_p = bm.Xor("1.0");
        string def_p = bm.Xor("1.0");
        string hp_p = bm.Xor("1.0");
        string hpr_p = bm.Xor("1.0");
        foreach (string 属性名 in 属性.Keys)
        {
            if (属性名.Equals("攻击力"))
                atk = bm.Xitos(bm.Xstoi(atk) + bm.Xstoi(属性[属性名]));
            else if (属性名.Equals("防御力"))
                def = bm.Xitos(bm.Xstoi(def) + bm.Xstoi(属性[属性名]));
            else if (属性名.Equals("血量"))
            {
                hp = bm.Xitos(bm.Xstoi(hp) + bm.Xstoi(属性[属性名]));
                剩余血量 = bm.Xitos(bm.Xstoi(剩余血量) + bm.Xstoi(属性[属性名]));
            }
            else if (属性名.Equals("回血值"))
                hpr = bm.Xitos(bm.Xstoi(hpr) + bm.Xstoi(属性[属性名]));
            else if (属性名.Equals("攻击力加成"))
                atk_p = bm.Xftos(bm.Xstof(atk_p) + bm.Xstof(属性[属性名]) / 100.0f);
            else if (属性名.Equals("防御力加成"))
                def_p = bm.Xftos(bm.Xstof(def_p) + bm.Xstof(属性[属性名]) / 100.0f);
            else if (属性名.Equals("血量加成"))
                hp_p = bm.Xftos(bm.Xstof(hp_p) + bm.Xstof(属性[属性名]) / 100.0f);
            else if (属性名.Equals("回血值加成"))
                hpr_p = bm.Xftos(bm.Xstof(hpr_p) + bm.Xstof(属性[属性名]) / 100.0f);
            else if (属性名.Equals("固定伤害"))
                固定伤害 = bm.Xitos(bm.Xstoi(基础固定伤害) + bm.Xstoi(属性[属性名]));
            else if (属性名.Equals("伤害加成"))
                伤害加成 = bm.Xftos(bm.Xstof(基础伤害加成) + bm.Xstof(属性[属性名]) / 100.0f);
            else if (属性名.Equals("固定减伤"))
                固定减伤 = bm.Xitos(bm.Xstoi(基础固定减伤) + bm.Xstoi(属性[属性名]));
            else if (属性名.Equals("伤害减免"))
                伤害减免 = bm.Xftos(bm.Xstof(基础伤害减免) + bm.Xstof(属性[属性名]) / 100.0f);
            else if (属性名.Equals("固定吸血"))
                固定吸血 = bm.Xitos(bm.Xstoi(基础固定吸血) + bm.Xstoi(属性[属性名]));
            else if (属性名.Equals("吸血加成"))
                吸血加成 = bm.Xftos(bm.Xstof(基础吸血加成) + bm.Xstof(属性[属性名]) / 100.0f);
            else if (属性名.Equals("暴击率"))
                暴击率 = bm.Xftos(bm.Xstof(基础暴击率) + bm.Xstof(属性[属性名]));
            else if (属性名.Equals("金钱加成"))
                金钱加成 = bm.Xftos((bm.Xstof(金钱加成) + bm.Xstof(属性[属性名])) / 100.0f);
            else if (属性名.Equals("经验加成"))
                经验加成 = bm.Xftos((bm.Xstof(经验加成) + bm.Xstof(属性[属性名])) / 100.0f);
            else if (属性名.Equals("攻击速度"))
            {
                //攻击速度_  /= (float)(1 + ((float)属性[属性名] / 100.0f));
                攻击速度_ = bm.Xftos(bm.Xstof(基础攻击速度) / (1 + bm.Xstof(属性[属性名]) / 100.0f));
            }

        }

        攻击力 = bm.Xitos((int)(bm.Xstoi(基础攻击力) * bm.Xstof(atk_p) + bm.Xstoi(atk)));
        防御力 = bm.Xftos(bm.Xstoi(基础防御力) * bm.Xstof(def_p) + bm.Xstoi(def));
        血量 = bm.Xftos(bm.Xstoi(基础血量) * bm.Xstof(hp_p) + bm.Xstoi(hp));
        回血值 = bm.Xftos(bm.Xstoi(基础回血值) * bm.Xstof(hpr_p) + bm.Xstoi(hpr));

    }


    public void 宠物初始化()
    {
        role_Data myData = io_.load();
        Pet_Data petData = pem.返回宠物(myData.出战宠物UID);
        if (petData == null)
        {
            Debug.Log("出战宠物出错!");
            Destroy(gameObject);
        }
        等级 = petData.grade;
        基础攻击力 = bm.Xor(bm.Xstoi(petData.ini_atk) + (bm.Xstoi(petData.grade) - 1) * (bm.Xstoi(petData.ram_atk) / 1000f)*3 * bm.Xstoi(petData.cc) + "");
        基础防御力 = bm.Xor(bm.Xstoi(petData.ini_def) + (bm.Xstoi(petData.grade) - 1) * (bm.Xstoi(petData.ram_def) / 1000f)*1 * bm.Xstoi(petData.cc) + "");
        基础血量 = bm.Xor(bm.Xstoi(petData.ini_hp) + (bm.Xstoi(petData.grade) - 1) * (bm.Xstoi(petData.ram_hp) / 1000f) * 50 * bm.Xstoi(petData.cc) + "");
        基础回血值 = bm.Xor(bm.Xstoi(petData.ini_hpr) + (bm.Xstoi(petData.grade) - 1) * (bm.Xstoi(petData.ram_hpr) / 1000f) * 5 * bm.Xstoi(petData.cc) + "");
        基础暴击率 = bm.Xor(bm.Xstoi(petData.ini_cri) + bm.Xstoi(petData.qua_cri) / 2000f * (bm.Xstoi(petData.grade) - 1) + "");
        基础攻击速度 = bm.Xor(bm.Xstoi(petData.ini_aspd) / 10f + "");
        基础伤害加成 = bm.Xor(bm.Xstoi(petData.harm_p) / 100.0f + "");
        基础伤害减免 = bm.Xor("0");
        基础固定伤害 = petData.harm;
        基础固定减伤 = bm.Xor("0");
        基础暴伤加成 = bm.Xor("1.5");
        基础固定吸血 = petData.vam;
        基础吸血加成 = bm.Xor("0");
        血条.maxValue = bm.Xstoi(基础血量);
        if (petData.state.Equals(""))
            剩余血量 = 基础血量;
        else
            剩余血量 = petData.state;

        宠物初始化行为();
    }


    public void 宠物初始化行为()
    {
        if (!ut.现有角色集合.ContainsKey(gameObject.name))
            ut.现有角色集合.Add(gameObject.name, gameObject);

        宠物技能初始化();
        宠物初始化战斗状态();
        //战斗状态下也能渲染颜色
        初始颜色();
        dm.加载额外属性(this);
    }

    public void 宠物初始化战斗状态()
    {
        GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
        if (人物 != null)
        {
            combat rcb = 人物.GetComponent<combat>();
            if (rcb.isAttack)
            {
                目标名字 = rcb.目标名字;
                开启战斗();
            }
        }
    }

    public void 宠物技能初始化()
    {
        Pet_Data petData = pem.返回宠物(myData.出战宠物UID);
        foreach (string 技能名 in petData.skilllist)
        {
            SkillData sd = pm.检索技能(技能名);
            if (sd.place.Equals("绝招"))
            {
                if (!技能与粒度.ContainsKey(技能名))
                    技能与粒度.Add(技能名, 1.0f);
            }
        }
    }


    public void 怪物初始化()
    {
        技能与粒度.Clear();
        utm = NameMgr.画布.transform.Find("combat_other").GetComponent<UT_monster>();
        utm.怪物赋值(this, false);
        怪物初始化行为();
        边框初始化();
    }


    public void 怪物初始化行为()
    {
        dm.加载额外属性(this);
        if (!ut.现有怪物集合.ContainsKey(gameObject.name))
            ut.现有怪物集合.Add(gameObject.name, gameObject);//ut怪物集合初始化

        GameObject 宠物 = GameObject.FindGameObjectWithTag("宠物");
        myData = io_.load();
        if (ut.isAuto && ut.现有怪物集合.Count == 1)//如果开启了自动攻击
            ut.人物自动攻击();
        else if (宠物 != null && myData.列表型记录["战斗设置"]["战斗设置"].Contains("宠物主动攻击"))
        {
            宠物.GetComponent<combat>().开启战斗();
        }

        血条.maxValue = bm.Xstoi(血量);
        剩余血量 = 血量;
        //怪物初始攻击状态判定
        if (isAttack == true)
            开启战斗();
    }


    public void 建筑初始化()
    {
        utm = GameObject.Find("老画布/combat_other").GetComponent<UT_monster>();
        utm.怪物赋值(this, true);
        血条.maxValue = bm.Xstoi(基础血量);
        剩余血量 = 基础血量;
        血量 = 基础血量;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (血条 != null)
        {
            //判断自己是否还有生命
            if (bm.Xstoi(剩余血量) <= 0)
                失败();

            if (bm.Xstoi(剩余血量) > bm.Xstoi(血量))
                剩余血量 = 血量;
            血条.value = bm.Xstoi(剩余血量);
            if (bm.Xstoi(剩余血量) < bm.Xstoi(血量) && str_tag != "建筑")
                回血();




            //攻击状态下选择一个目标自动普攻
            if (isAttack)
                攻击判定();
            else
            {
                //当战斗设置改变时,另一个脚本更新该cb中的myData的值
                if (gameObject.CompareTag("人物") && myData.列表型记录["战斗设置"]["战斗设置"].Contains("受伤自动反击") && 仇恨列表.Count != 0)
                {
                    isAttack = true;
                    List<string> 反击名单 = new List<string>();
                    foreach (string name in 仇恨列表.Keys)
                    {
                        反击名单.Add(name);
                    }
                    目标名字 = 反击名单[0];
                    开启战斗();
                }
                else if (gameObject.CompareTag("宠物") && myData.列表型记录["战斗设置"]["战斗设置"].Contains("宠物主动攻击"))
                {
                    combat petcb = gameObject.GetComponent<combat>();
                    ut.自动攻击(petcb);
                }
                初始颜色();
            }
        }
    }


    public void 初始颜色()
    {
        //停战色
        if (gameObject.CompareTag("建筑"))
            ColorUtility.TryParseHtmlString("#83B6BA", out nowColor);//青蓝
        else
            ColorUtility.TryParseHtmlString("#6ED271", out nowColor);//绿色
        gameObject.transform.Find("边框遮罩/Fill Area/Fill").GetComponent<Image>().color = nowColor;

    }

    void 回血()
    {
        //回血计时器,5秒回一次血
        Time_hp -= Time.deltaTime;
        if (Time_hp <= 0 && bm.Xstoi(回血值) > 0)
        {
            int 时回血 = bm.Xstoi(回血值);
            回血时间 = bm.Xor("5.0");
            if (!isAttack)
            {
                时回血 = (int)(bm.Xstoi(回血值) * 1.5f);
                if (时回血 > (bm.Xstoi(血量) / 5))
                    时回血 = bm.Xstoi(血量) / 5;
                回血时间 = bm.Xor("3.0");
            }
            剩余血量 = bm.Xitos(bm.Xstoi(剩余血量) + 时回血);
            HPReduce HpR = gameObject.GetComponent<HPReduce>();
            HpR.AddHPEffect();
            Time_hp = bm.Xstof(回血时间);
            ut.扣血显示(gameObject.transform.parent.gameObject, 时回血, "回血");
        }
    }


    public string 怪物仇恨()
    {
        //清除数据
        伤害列表.Clear();
        敌人列表.Clear();
        //怪物先打输出高的
        foreach (string name in 仇恨列表.Keys)
        {
            伤害列表.Add(仇恨列表[name]);
        }
        伤害列表.Sort();
        for (int i = 伤害列表.Count - 1; i >= 0; i--)
        {
            foreach (string name in 仇恨列表.Keys)//根据伤害给敌人排序
            {
                if (伤害列表[i] == 仇恨列表[name])
                    敌人列表.Add(name);
            }
        }

        GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
        if (敌人列表.Count > 0)
            return 敌人列表[0];
        else if (人物 != null && !gameObject.CompareTag("宠物"))
            return 人物.name;
        else
            return "";

    }


    public void 清除怪物的仇恨(string name)
    {
        if (仇恨列表.ContainsKey(name) && 伤害列表.Contains(仇恨列表[name]))
            伤害列表.Remove(仇恨列表[name]);
        if (仇恨列表.ContainsKey(name))
            仇恨列表.Remove(name);
        if (敌人列表.Contains(name))
            敌人列表.Remove(name);
    }

    void 攻击判定()
    {

        if (Time.frameCount % 10 == 0)//因为方法里的循环很消耗性能,所以每10帧执行一次
        {
            if (str_tag.Equals("怪物") || str_tag.Equals("boss"))//怪物是根据仇恨打目标,人物是自己点或者随机打
            {
                目标名字 = 怪物仇恨();
            }

            检索倒计时();
        }



        目标 = GameObject.Find(目标名字);
        if (目标 != null)
        {
            combat cb = 目标.GetComponent<combat>();
            if (cb != null && !cb.仇恨列表.ContainsKey(gameObject.name))//updata是多线程模式,一些线程在宠物或者人物死亡时(本体被隐藏前, 目标还不是null时)..达到了这里,但是cb是null的
                cb.仇恨列表.Add(gameObject.name, 0);

        }
        else
        {
            if (gameObject.CompareTag("怪物") || gameObject.CompareTag("boss"))
            {
                清除怪物的仇恨(目标名字);
                目标名字 = 怪物仇恨();
            }
            else if (gameObject.CompareTag("宠物") && !myData.列表型记录["战斗设置"]["战斗设置"].Contains("宠物主动攻击"))
            {
                isAttack = false;
            }

        }

        //当战斗设置改变时,另一个脚本更新该cb中的myData的值
        if (gameObject.CompareTag("人物") && myData.列表型记录["战斗设置"]["战斗设置"].Contains("自动释放绝招") && !myData.技能槽["1"].Equals("") && !sa.CD集合.ContainsKey("绝招"))
            sa.使用绝招();
        //当战斗设置改变时,另一个脚本更新该cb中的myData的值
        if (gameObject.CompareTag("人物") && myData.列表型记录["战斗设置"]["战斗设置"].Contains("自动使用道具") && myData.记录.ContainsKey("CD道具") && !sa.CD集合.ContainsKey("CD道具"))
            ut.自动使用CD道具(myData, this);


        if (isState)  // 判断是否有特殊技能
            眩晕状态();
        else
            攻击(目标);

    }

    private void 眩晕状态()
    {

    }

    private bool 粒度与CD判定()
    {
        if (技能与粒度.Count != 0 && 技能与CD.Count < 技能与粒度.Count)
        {//先判断有技能没在CD中
            foreach (string 技能名 in 技能与粒度.Keys)
            {
                if (!技能与CD.ContainsKey(技能名) && ut.概率((int)(技能与粒度[技能名] * 1000), 1000))
                {
                    sa.宠物或怪物使用绝招(gameObject, 技能名);
                    技能标记 = true;
                    return true;
                }
            }
        }
        技能标记 = false;
        return false;
    }


    public void 攻击(GameObject 目标)
    {
        timer -= Time.deltaTime;

        if (目标 != null)
        {
            combat cb = 目标.GetComponent<combat>();

            if (cb != null)
            {
                //普攻计时器
                if (gameObject.CompareTag("boss"))//boss攻击,无延迟扣伤害
                {
                    if (timer <= 0f)
                    {
                        if (!技能标记)//先判断技能标记..然后判断怪物技能释放
                        {
                            if (!粒度与CD判定())//宠物,boss技能释放,否则进行普通攻击
                            {
                                StartCoroutine(普通攻击移动());
                                普通攻击动画(cb.gameObject);

                                //暴击
                                暴击 = ut.概率((int)bm.Xstof(暴击率), 100);
                                if (!暴击)//无暴击   
                                    当前暴伤加成 = bm.Xor("1.0");
                                else
                                    当前暴伤加成 = 暴伤加成;

                                int 伤害 = 普攻伤害计算(cb);
                                timer = bm.Xstof(攻击速度_);
                                if (暴击)
                                    扣血行为(目标, "暴击", 伤害);
                                else if (gameObject.CompareTag("boss"))
                                    扣血行为(目标, "普通", 伤害);

                                //吸血
                                if (bm.Xstoi(固定吸血) != 0 || bm.Xstof(吸血加成) != 0)
                                {
                                    int 吸血 = (int)(bm.Xstof(吸血加成) * 伤害 + bm.Xstoi(固定吸血));
                                    剩余血量 = bm.Xitos(bm.Xstoi(剩余血量) + 吸血);
                                    HPReduce HpR = gameObject.GetComponent<HPReduce>();
                                    HpR.AddHPEffect();
                                    ut.扣血显示(gameObject.transform.parent.gameObject, 吸血, "回血");
                                }
                            }
                        }
                        else
                        {
                            timer = bm.Xstof(攻击速度_);
                            技能标记 = false;
                        }
                    }
                }
                else//人物,宠物,伙伴或者怪物攻击
                {

                    if (timer <= 0f)
                    {

                        if (!技能标记)//先判断技能标记..然后判断怪物技能释放
                        {
                            if (!粒度与CD判定())
                            {
                                //暴击
                                暴击 = ut.概率((int)bm.Xstof(暴击率), 100);
                                if (!暴击)//无暴击   
                                    当前暴伤加成 = bm.Xor("1.0");
                                else
                                    当前暴伤加成 = 暴伤加成;


                                int 伤害 = 普攻伤害计算(cb);
                                if (gameObject.CompareTag("人物") || gameObject.CompareTag("伙伴") || gameObject.CompareTag("怪物") || gameObject.CompareTag("宠物"))
                                    StartCoroutine(普通攻击移动());
                                if (!攻击标记)
                                {
                                    普通攻击动画(cb.gameObject);
                                    攻击标记 = true;

                                    //吸血
                                    if (bm.Xstoi(固定吸血) != 0 || bm.Xstof(吸血加成) != 0)
                                    {
                                        int 吸血 = (int)(bm.Xstof(吸血加成) * 伤害 + bm.Xstoi(固定吸血));
                                        剩余血量 = bm.Xitos(bm.Xstoi(剩余血量) + 吸血);
                                        HPReduce HpR = gameObject.GetComponent<HPReduce>();
                                        HpR.AddHPEffect();
                                        ut.扣血显示(gameObject.transform.parent.gameObject, 吸血, "回血");
                                    }

                                    timer = bm.Xstof(攻击速度_);
                                    攻击标记 = false;
                                    if (暴击)
                                    {
                                        扣血行为(目标, "暴击", 伤害);
                                    }
                                    else if (gameObject.CompareTag("宠物"))
                                        扣血行为(目标, "宠物", 伤害);
                                    else
                                        扣血行为(目标, "普通", 伤害);
                                }
                            }
                        }
                        else
                        {
                            timer = bm.Xstof(攻击速度_);
                            技能标记 = false;
                        }

                    }
                }
            }
        }
        //else Debug.Log("目标丢失!");
    }


    public int 普攻伤害计算(combat cb)
    {
        int 伤害 = -1;

        if (bm.Xstof(攻击力) > bm.Xstof(cb.防御力)*1.5f)
            伤害 = (int)((bm.Xstof(攻击力) - bm.Xstof(cb.防御力)) * (bm.Xstof(伤害加成) - bm.Xstof(cb.伤害减免)) * bm.Xstof(当前暴伤加成) + (bm.Xstoi(固定伤害) - bm.Xstoi(cb.固定减伤)));
        else
            伤害 = (int)(bm.Xstof(攻击力)*0.33f*(bm.Xstof(攻击力)/ (bm.Xstof(cb.防御力) * 1.1f )) * (bm.Xstof(伤害加成) - bm.Xstof(cb.伤害减免)) * bm.Xstof(当前暴伤加成) + (bm.Xstoi(固定伤害) - bm.Xstoi(cb.固定减伤)));


        Random 随机类 = new Random(Guid.NewGuid().GetHashCode());
        int 随机比率 = 随机类.Next(900, 1101);
        伤害 = (int)(伤害 * (随机比率 / 1000f));


        if (伤害 <= 0)//伤害为正数
            伤害 = 1;

        return 伤害;
    }


    public void 扣血行为(GameObject 目标, string 类型, int 伤害)
    {
        combat cb = 目标.GetComponent<combat>();
        if (cb != null)
        {
            if (!类型.Equals("回血") && !目标.name.Equals(gameObject.name))
            {

                if (cb.仇恨列表.ContainsKey(gameObject.name))
                    cb.仇恨列表[gameObject.name] += 伤害;
                else
                    cb.仇恨列表.Add(gameObject.name, 伤害);

                if (目标.CompareTag("怪物") || 目标.CompareTag("boss"))
                    cb.开启战斗();

            }
            cb.剩余血量 = bm.Xitos(bm.Xstoi(cb.剩余血量) - 伤害);
           
            ut.扣血显示(目标.transform.parent.gameObject, 伤害, 类型);
            HPReduce HpR = 目标.GetComponent<HPReduce>();
            if (目标.activeSelf)
                StartCoroutine(HpR.ReduceHPEffect());
             }
    }

    public void 普通攻击动画(GameObject 攻击对象)
    {
        if (攻击对象.CompareTag("boss"))//攻击对象如果是boss,它的位置是它的图片;
            攻击对象 = 攻击对象.transform.Find("图片").gameObject;
        if (gameObject.CompareTag("boss"))
            ut.生成宠物普攻特效(攻击对象, 攻击对象.transform.position);
        else
            ut.生成普攻特效(gameObject.transform.parent.gameObject, 攻击对象.transform.position, 0.1f);

    }

    public IEnumerator 普通攻击移动()
    {
        if (gameObject.CompareTag("boss"))//boss只移动图片
        {
            GameObject 图片 = gameObject.transform.Find("图片").gameObject;
            图片.transform.localPosition = Vector2.MoveTowards(图片.transform.localPosition, 攻击坐标, 20);
            yield return new WaitForSeconds(0.15f);
            图片.transform.localPosition = Vector2.MoveTowards(图片.transform.localPosition, 原来坐标, 20);
        }
        else
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, 攻击坐标, 20);
            yield return new WaitForSeconds(0.15f);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, 原来坐标, 20);
        }
        yield break;
    }


    public void 开启战斗()
    {
        if (str_tag != "建筑")
            isAttack = true;
        //怪物脚本者行为
        if (str_tag == "怪物" || str_tag == "boss")
        {
            //联网(多人)时要改成搜索唯一的名字来查找
            GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
            if (目标名字.Equals("") && 人物 != null)
            {
                目标名字 = 人物.gameObject.name;
                timer = bm.Xstof(攻击速度_) / 2.0f;
            }
            //战斗黄
            ColorUtility.TryParseHtmlString("#D2C13F", out nowColor);//黄色
            gameObject.transform.Find("边框遮罩/Fill Area/Fill").GetComponent<Image>().color = nowColor;
        }
        else
        {

            //宠物协同攻击
            GameObject 宠物 = GameObject.FindGameObjectWithTag("宠物");
            combat petcb = null;
            if (宠物 != null && (gameObject.CompareTag("宠物") || gameObject.CompareTag("人物")))
            {
                petcb = 宠物.GetComponent<combat>();
                GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
                if (人物 != null)
                {
                    myData = io_.load();
                    if (myData.列表型记录["战斗设置"]["战斗设置"].Contains("宠物主动攻击"))
                    {
                        if (目标名字 != null && 目标名字.Equals(""))
                        {
                            GameObject 怪物 = GameObject.Find(petcb.怪物仇恨());
                            if (怪物 == null)
                            {
                                怪物 = mmgr.随意寻找一个目标("怪物");
                            }
                            if (petcb != null && 怪物 != null)
                            {
                                petcb.目标名字 = 怪物.name;
                                petcb.isAttack = true;
                            }
                        }
                    }
                    combat rolecb = 人物.GetComponent<combat>();
                    if (!GameObject.Find(petcb.目标名字))
                    {
                        petcb.目标名字 = rolecb.目标名字;
                        petcb.isAttack = true;
                    }
                }
            }
        }
    }


    public void 点击怪物()
    {
        if (gameObject.CompareTag("人物") || gameObject.CompareTag("宠物"))
            return;
        GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
        if (人物 != null)
        {
            combat cb = 人物.GetComponent<combat>();
            //联网(多人)时要改成搜索唯一的名字来查找
            cb.isAttack = true;
            cb.目标名字 = gameObject.name;
            if (!ut.isAuto)
            {
                if (!cb.仇恨列表.ContainsKey(gameObject.name) && cb.isAttack == false)
                    cb.timer = 0;
            }

            cb.开启战斗();
        }
    }

    void 失败()
    {
        if (ut.现有怪物集合.ContainsKey(gameObject.name))//先从现有怪物列表中移除自己,不然有些监听无法触发
            ut.现有怪物集合.Remove(gameObject.name);

        添加失败监听();
        触发失败监听();
        移除失败监听();
    }


    void 怪物失败事件(combat cb) {
        isAttack = false;
       
            //myData = io_.load();
            if (cb.gameObject.CompareTag("boss"))//死亡者属于boss,开始刷新
            {
                mrf = gameObject.transform.parent.GetComponent<m_Refresh>();
                if (mrf != null && !sa.CD集合.ContainsKey(cb.gameObject.name))
                {
                    sa.CD集合.Add(cb.gameObject.name, mrf.刷新时间);
                }

                if (cb.gameObject.transform.parent.parent.Find("倒计时") != null)//如果有计时器,则显示计时器
                    cb.gameObject.transform.parent.parent.Find("倒计时").gameObject.SetActive(true);
            }
            GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
            if (人物 != null)
            {
                combat rcb = 人物.GetComponent<combat>();
                GameObject 宠物 = GameObject.FindGameObjectWithTag("宠物");
                combat petcb;

                if (rcb.仇恨列表.ContainsKey(cb.gameObject.name))//清除人物仇恨
                    rcb.仇恨列表.Remove(cb.gameObject.name);


                //自动状态下或反击状态下,改变人物的攻击目标,与宠物目标
                if (ut.isAuto)
                {
                    rcb.目标名字 = "";
                    ut.人物自动攻击();

                    if (宠物 != null)//如果有宠物,宠物的行为
                    {
                        petcb = 宠物.GetComponent<combat>();
                        if (myData.列表型记录["战斗设置"]["战斗设置"].Contains("宠物主动攻击"))//开启了宠物主动攻击
                        {
                            petcb.开启战斗();
                        }
                        else
                        {
                            if (petcb.仇恨列表.ContainsKey(cb.gameObject.name))//清除宠物仇恨
                                petcb.仇恨列表.Remove(cb.gameObject.name);
                            if (cb.gameObject.name.Equals(petcb.目标名字))//攻击目标转变为人物的攻击目标
                                petcb.目标名字 = rcb.目标名字;
                        }
                    }
                }
                else//点击或反击状态下,改变人物和宠物的攻击目标
                {
                    //如果怪物自己是人物的目标,则改变人物的攻击状态
                    if (cb.gameObject.name.Equals(rcb.目标名字))
                    {
                        if (myData.列表型记录["战斗设置"]["战斗设置"].Contains("受伤自动反击") && rcb.仇恨列表.Count >= 1)//判断是否开了自动反击
                            rcb.timer = 0.5f;
                        else
                            rcb.timer = 0;
                        rcb.目标名字 = "";
                        rcb.isAttack = false;
                    }


                    //如果怪物自己是宠物的目标,则改变宠物的攻击状态
                    if (宠物 != null)
                    {
                        petcb = 宠物.GetComponent<combat>();
                        if (cb.gameObject.name.Equals(petcb.目标名字))
                        {
                            if (myData.列表型记录["战斗设置"]["战斗设置"].Contains("宠物主动攻击"))//开启了宠物主动攻击
                            {
                                if (petcb.仇恨列表.ContainsKey(cb.gameObject.name))//清除宠物仇恨
                                    petcb.仇恨列表.Remove(cb.gameObject.name);
                                petcb.清除怪物的仇恨(cb.gameObject.name);
                                GameObject 目标 = mmgr.随意寻找一个目标("怪物");
                                if (目标 != null)
                                {
                                    petcb.目标名字 = 目标.name;
                                    petcb.开启战斗();
                                }
                            }
                            else
                            {
                                petcb.timer = bm.Xstof(petcb.攻击速度_) / 2.0f;
                                if (rcb.isAttack)
                                {
                                    petcb.目标名字 = rcb.目标名字;
                                }
                                else
                                {
                                    petcb.目标名字 = null;
                                    petcb.isAttack = false;
                                }
                            }
                        }
                    }

                }

                cb.掉落();//待改造
                cb.gameObject.SetActive(false);
            }
        
        cb.gameObject.SetActive(false);
    }



    void 角色失败事件( combat cb) {
   
            if (cb.gameObject.CompareTag("宠物"))
            {
                目标名字 = "";
                if (!sa.CD集合.ContainsKey("宠物复活"))
                    sa.CD集合.Add("宠物复活", 30);
            }
            if (cb.gameObject.CompareTag("人物"))
            {
            cb.isAttack = false;
                目标 = GameObject.Find(目标名字);
                if (目标 != null)
                {
                    combat mcb = 目标.GetComponent<combat>();
                    cm.战斗播报(mcb.怪物名字_, mcb.怪物品质, new Dictionary<Prop_bascis, int>(), new Dictionary<string, string>(), "死亡");
                }
                else
                {
                    cm.战斗播报("怪物", 5, new Dictionary<Prop_bascis, int>(), new Dictionary<string, string>(), "死亡");
                }
            }

            死亡动画();
    }


    void 死亡动画()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        ut.生成死亡动画(gameObject);
    }

    void 掉落()
    {
        combat cb =DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
        GameObject UI = GameObject.FindGameObjectWithTag("UI");
        if (bm.Xstoi(经验值) != 0)//持久化经验
        {
            string 总经验加成;
            if (bm.Xstoi(cb.等级) < bm.Xstoi(等级))//打高于自身的怪物提升经验
                总经验加成 = bm.Xftos(((bm.Xstoi(等级) - bm.Xstoi(cb.等级)) * 0.02f + 1) * bm.Xstof(cb.经验加成));
            else
                总经验加成 = cb.经验加成;


            经验值 = bm.Xor(Math.Floor(bm.Xstof(总经验加成) * bm.Xstoi(经验值)) + "");
            ut.刷新经验条UI_不进行IO(UI,bm.Xstoi(经验值));//临时存经验
            经验钱币.Add("经验", 经验值 + "");
        }
        if (掉落集合.Count > 0)//持久化道具
        {
            //掉落光球效果
            bagTf = NameMgr.背包图片坐标;
            foreach (Prop_bascis 物品 in 掉落集合.Keys)
            {
                ut.生成光球by品质(bm.Xstoi(物品.qua), gameObject.transform.parent.gameObject, bagTf, 0.5f);
            }
            dm.添加临时物品列表(掉落集合);
        }


        if (bm.Xstoi(铜币) != 0|| bm.Xstoi(金币) != 0) //掉落文字展示
        {
            铜币 = bm.Xitos((int)(bm.Xstof(金钱加成) * bm.Xstoi(铜币)));
            金币 = bm.Xitos((int)(bm.Xstof(金钱加成) * bm.Xstoi(金币)));
            ut.刷新金钱UI_不进行IO(UI,new Dictionary<string, int>() { { "铜币", bm.Xstoi(铜币) }, { "金币", bm.Xstoi(金币) } });
            经验钱币.Add("铜币", 铜币 + "");
            经验钱币.Add("金币", 金币 + "");
        }

        if (怪物品质 >= 4 ||掉落集合.Count>0)
            dm.储存缓存数据();
        else
            dm.计数();

        cm.战斗播报(怪物名字_, 怪物品质, 掉落集合, 经验钱币, "掉落");//战斗播报


        掉落集合.Clear();//清空掉落集合
        经验钱币.Clear();//每次执行完成后消除数据
        //ut.经验钱币.Clear();
        铜币 = bm.Xor("0");
        金币 = bm.Xor("0");

    }

    


}


