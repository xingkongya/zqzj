using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class move_MuBei : MonoBehaviour
{
    private GameObject 墓碑图标;
    public GameObject 复活目标;
    private float Die_Time=0f;
    private Text 死亡计时;
    private io io_;
    private G_Util gut;


    private void Awake()
    {
        io_ = io.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
        墓碑图标 = gameObject.transform.Find("墓碑").gameObject;
        死亡计时 = gameObject.transform.Find("倒计时/time").gameObject.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (复活目标.CompareTag("人物"))//人物复活时间-6s
            Die_Time = 6f;
        else if (复活目标.CompareTag("宠物"))//宠物复活时间-30s
            Die_Time = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        死亡计时.text = (int)Die_Time+"";//同步倒计时

        if (墓碑图标.transform.localPosition.y > 0)
            墓碑图标.transform.Translate(Vector2.down * Time.deltaTime * 30f);//墓碑移动动画

        Die_Time -= Time.deltaTime;
        if (Die_Time < 0)
            复活事件();
    }


    private void 复活事件() {
        role_Data myData = io_.load();
        if (复活目标 != null && 复活目标.CompareTag("人物"))
        {
            combat cb = 复活目标.GetComponent<combat>();
            cb.剩余血量 = cb.血量;
            cb.timer = 0;//重置普攻
            myData.登录场景 = myData.复活城市;
            io_.save(myData);
            复活目标.SetActive(true);
            gut.关闭杂项();
            gut.现有怪物集合.Clear();
            gut.关闭自动攻击();

            GameObject[] 场地组 = GameObject.FindGameObjectsWithTag("场地");
            if (场地组 != null)
            {
                for (int i = 0; i < 场地组.Length; i++)
                {
                    UT_monster utm = 场地组[i].GetComponent<UT_monster>();
                    utm.删除对象();
                }
            }
            SceneManager.LoadScene(myData.复活城市);
            gameObject.SetActive(false);
        }
        else if (复活目标 != null && 复活目标.CompareTag("宠物"))
        {
            combat cb = 复活目标.GetComponent<combat>();
            cb.剩余血量 = cb.血量;
            cb.timer = 0;//重置普攻
            复活目标.SetActive(true);
            gameObject.SetActive(false);
            初始化宠物战斗状态(cb);
        }
    }

    public void 初始化宠物战斗状态(combat cb) {
        GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
        if (人物 != null)
        {
            combat rcb = 人物.GetComponent<combat>();
            if (rcb.isAttack)
            {
                cb.目标名字 = rcb.目标名字;
                cb.开启战斗();
            }
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
