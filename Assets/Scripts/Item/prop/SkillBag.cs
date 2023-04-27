using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBag : MonoBehaviour
{

    private io io_;
    private G_Util gut;
    private GameObject 列表;
    private PropMgr pm;
    private SkillMgr sm;


    private void Awake()
    {
        io_ = io.GetInstance();
        pm = PropMgr.GetInstance();
        sm = SkillMgr.GetInstance();
        列表 = gameObject.transform.Find("背包界面/Scroll View/Viewport/Content").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ini_SKillBag()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        int index = 0;
        string place = gameObject.transform.Find("背包界面/Panel/Text").GetComponent<Text>().text;
       
        role_Data myData = io_.load();
        if (!myData.技能背包.ContainsKey(place))//没有主键则创建一个
        {
            myData.技能背包.Add(place, new List<SkillData>());
            io_.save(myData);
        }
        List<SkillData> 单一位置背包 = myData.技能背包[place];
        foreach (SkillData sd in 单一位置背包) {
            gut.生成背包技能项(sd,列表,index,单一位置背包.Count);
            index++;
        }
    }

    public void 使用技能()
    {
        GameObject 选中项 = GameObject.FindGameObjectWithTag("选中");
        if (选中项 != null)
        {
            string SkillName = 选中项.transform.Find("name").GetComponent<Text>().text;
            SkillData sd = sm.返回技能背包该名称的技能(SkillName);
            role_Data myData = io_.load();
            bool isChong = false;
            foreach (string name in myData.技能槽.Keys)//遍历该技能是否使用过是否
            {
                if (sd.name.Equals(myData.技能槽[name]))
                    isChong = true;
            }
            if (isChong)//已使用
            {
                gut.生成警告框("技能使用中...");
                return;
            }
            else
            {
                if (gameObject.transform.Find("标记").GetComponent<Text>().text.Equals("0"))
                    sm.加载心法();
                else if (gameObject.transform.Find("标记").GetComponent<Text>().text.Equals("1"))
                    sm.加载绝招();
                else if (gameObject.transform.Find("标记").GetComponent<Text>().text.Equals("2"))
                    sm.加载被动一();
                else if (gameObject.transform.Find("标记").GetComponent<Text>().text.Equals("3"))
                    sm.加载被动二();
                else if (gameObject.transform.Find("标记").GetComponent<Text>().text.Equals("4"))
                    sm.加载被动三();
            }

            //效果
            combat cb =NameMgr.人物.GetComponent<combat>();
            cb.人物属性刷新();
            cb.myData = io_.load();
            GameObject 属性面板 = GameObject.Find("属性面板(Clone)");
            if(属性面板!=null)
                gut.面板属性刷新(属性面板);
            选中项.transform.Find("遮罩").gameObject.SetActive(true);

            gameObject.SetActive(false);
        }
    }


    public void 遗忘技能() {
        GameObject 选中项 = GameObject.FindGameObjectWithTag("选中");
        if (选中项 != null)
        {
            string SkillName = 选中项.transform.Find("name").GetComponent<Text>().text;
            SkillData sd = sm.返回技能背包该名称的技能(SkillName);
            role_Data myData = io_.load();
            bool isChong = false;
            foreach (string name in myData.技能槽.Keys)//遍历该技能是否使用过是否
            {
                if (sd.name.Equals(myData.技能槽[name]))
                    isChong = true;
            }
            if (isChong)//已使用
            {
                gut.生成警告框("技能使用中,无法遗忘");
                return;
            }
            else
            {
                sm.遗忘技能_无参();
                gut.生成警告框("遗忘成功");
            }

         }
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

}
