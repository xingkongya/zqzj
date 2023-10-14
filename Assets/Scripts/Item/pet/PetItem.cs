using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetItem : MonoBehaviour
{
    private basicMgr bm;
    public Pet_Data 宠物;
    private io io_;
    private PetMgr pem;
    private G_Util gut;
    private RoleMgr rm;
    public string UID;
    public StateStar ss;
    public Dictionary<int, string> 品质与精华 = new Dictionary<int, string>();

    private void Awake()
    {
        pem = PetMgr.GetInstance();
        bm = basicMgr.GetInstance();
        io_ = io.GetInstance();
        rm = RoleMgr.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ss = NameMgr.画布.transform.Find("UI").transform.Find("状态页").GetComponent<StateStar>();
        品质与精华.Add(0, "灰色精华");
        品质与精华.Add(1, "绿色精华");
        品质与精华.Add(2, "蓝色精华");
        品质与精华.Add(3, "紫色精华");
        品质与精华.Add(4, "仙兽精华");
        品质与精华.Add(5, "神兽精华");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void 点击宠物项() {
        ss.宠物 = PetMgr.GetInstance().返回宠物(UID);
        ss.刷新宠物属性信息();
        显示宠物操作();
    }




    public void 显示宠物操作() {
        if (ss.子界面.transform.Find("初始界面/初始提示").gameObject.activeSelf)
        {
            ss.子界面.transform.Find("初始界面/初始提示").gameObject.SetActive(false);
            ss.子界面.transform.Find("初始界面").transform.Find("按钮界面").gameObject.SetActive(true);
        }
        //变色
        Color nowColor;
        if (GameObject.FindGameObjectsWithTag("未选中").Length > 0)
        {
            GameObject[] 未选中栏 = GameObject.FindGameObjectsWithTag("未选中");
            foreach (GameObject 道具项 in 未选中栏)
            {
                ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
                道具项.GetComponent<Image>().color = nowColor;
            }
        }



        //点击变色
        ColorUtility.TryParseHtmlString("#DBCDCD", out nowColor);
        gameObject.GetComponent<Image>().color = nowColor;



        role_Data myData = io_.load();
        宠物 = PetMgr.GetInstance().返回宠物(UID);

        Sprite 宠物图片 = Resources.Load<Sprite>("怪物/"+宠物.icon);
        if (宠物图片 == null)
        {
            宠物图片 = Resources.Load<Sprite>("怪物/黑白鸟");
        }  
        Image 图标 = gameObject.transform.parent.parent.parent.Find("子界面/初始界面/属性界面/背景/图标").GetComponent<Image>();
        //Image 边框 = gameObject.transform.parent.parent.parent.parent.parent.Find("边框").GetComponent<Image>();
        图标.sprite = 宠物图片;
        //边框.sprite = 宠物图片;
        图标.color = bm.改变透明度(图标.gameObject, 255f);



      

       
        /*Text 经验池文本 = gameObject.transform.parent.parent.parent.Find("子界面/初始界面/按钮界面/操作/操作界面/升级背景/经验池").GetComponent<Text>();
        Text 所需经验文本文本 = gameObject.transform.parent.parent.parent.Find("子界面/初始界面/按钮界面/操作/操作界面/升级背景/升级所需经验").GetComponent<Text>();
        Text 精华数量文本 = gameObject.transform.parent.parent.parent.Find("子界面/初始界面/按钮界面/操作/操作界面/成长背景/拥有数量/num").GetComponent<Text>();
        Text 提升所需文本 = gameObject.transform.parent.parent.parent.Find("子界面/初始界面/按钮界面/操作/操作界面/成长背景/提升所需/num").GetComponent<Text>();
        Text 需求精华名字文本 = gameObject.transform.parent.parent.parent.Find("子界面/初始界面/按钮界面/操作/操作界面/成长背景/拥有数量/name").GetComponent<Text>();
        Text 拥有精华名字文本 = gameObject.transform.parent.parent.parent.Find("子界面/初始界面/按钮界面/操作/操作界面/成长背景/提升所需/name").GetComponent<Text>();*/


        //Debug.Log(myData.出战宠物UID + "\n" + UID);
        Text 出战文本 = gameObject.transform.parent.parent.parent.Find("子界面/初始界面").transform.Find("按钮界面/操作/操作界面/出战/Text").GetComponent<Text>();
        if (myData.出战宠物UID != null && myData.出战宠物UID.Equals(UID))
        {         
            出战文本.text = "出战中...";
            Color nowColor1;
            ColorUtility.TryParseHtmlString("#ECA594", out nowColor1);
            出战文本.transform.parent.GetComponent<Image>().color = nowColor1;
        }
        else {
            出战文本.text = "出战";
            Color nowColor1;
            ColorUtility.TryParseHtmlString("#94DDEC", out nowColor1);
            出战文本.transform.parent.GetComponent<Image>().color = nowColor1;
        }
    }


    public void 显示炼化信息() {
        Text 选中名文本 = GameObject.Find("子界面").transform.Find("炼化界面/界面/炼宠界面/左/Panel/Text").GetComponent<Text>();
        Text 所得名文本 = GameObject.Find("子界面").transform.Find("炼化界面/界面/炼宠界面/右/Panel/Text").GetComponent<Text>();
        Slider 数量拉条 = GameObject.Find("子界面").transform.Find("炼化界面/button/Slider").GetComponent<Slider>();
        InputField 输入文本 = 数量拉条.transform.Find("InputField").GetComponent<InputField>();
        宠物 = pem.返回宠物(UID);
        int qua = bm.Xstoi(宠物.qua);
        选中名文本.text = 宠物.name;
        选中名文本.color = bm.转换颜色(qua);
        所得名文本.text = 品质与精华[qua] + "X "+bm.Xstoi(宠物.cc);
        所得名文本.color = bm.转换颜色(qua);
        数量拉条.maxValue = 1;
        数量拉条.value = 1;
        输入文本.text = "1";
    }




  

   


    public void 点击宠物锁()
    {
        role_Data myData = io_.load();
        Pet_Data 宠物 = pem.返回宠物(UID);
        if (宠物.islock == "1")
        {
            宠物.islock = "0";
            gameObject.transform.Find("锁图标").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/开锁图标");
        }
        else {
            宠物.islock = "1";
            gameObject.transform.Find("锁图标").GetComponent<Image>().sprite = Resources.Load<Sprite>("图标/关锁图标");
        }
        myData = pem.存档宠物属性覆盖(myData,宠物);
        io_.save(myData);
    }




    private void OnDisable()
    {
        Destroy(gameObject);
    }

}
