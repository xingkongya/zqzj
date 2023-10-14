using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BuyPropItem : MonoBehaviour
{
    private io io_;
    private G_Util gut;
    private PropMgr pm;
    private basicMgr bm;
    private Image 货币图标;
    public int index;
    public int 总条数;
    public string 购买数量;
    public string 钱币;
    public string 价格;
    public string 名字;
    string SceneName;



    private void Awake()
    {
        bm = basicMgr.GetInstance();
        io_ = io.GetInstance();
        pm = PropMgr.GetInstance();
        货币图标 = gameObject.transform.Find("价格/货币").GetComponent<Image>();
        gut = NameMgr.画布.GetComponent<G_Util>();
    }
    // Start is called before the first frame update
    void Start()
    {
        名字 = gameObject.transform.Find("名字/Text").GetComponent<Text>().text;
        价格 = gameObject.transform.Find("价格/Text").GetComponent<Text>().text;
        SceneName = SceneManager.GetActiveScene().name;

        if (钱币.Equals("铜钱"))
            货币图标.sprite = Resources.Load("图标/蜡笔铜币", typeof(Sprite)) as Sprite;
        else if (钱币.Equals("金币"))
            货币图标.sprite = Resources.Load("图标/蜡笔金币", typeof(Sprite)) as Sprite;
        else if (钱币.Equals("仙晶"))
            货币图标.sprite = Resources.Load("图标/仙晶", typeof(Sprite)) as Sprite;
        else if (钱币.Equals("黑钻"))
        {
            货币图标.sprite = Resources.Load("图标/黑钻图标", typeof(Sprite)) as Sprite;
            bm.改变颜色(货币图标, "262626");
        }

        /*if (myData.记录.ContainsKey(SceneName + "_" + 名字))
            购买数量 = myData.记录[SceneName + "_" + 名字];
        else
            myData.记录.Add(SceneName + "_" + 名字, 购买数量);*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void 生成购买拉条()
    {
        /* role_Data myData = io_.load();
         if (!购买数量.Equals("无限")) {
             if (myData.列表型记录.ContainsKey("道具限购"))//判断有没有这个"道具限购"这个键
             {
                 if (myData.列表型记录["道具限购"].ContainsKey(SceneName + "_" + 名字))//判断有没有这个物品的限购信息
                 {
                     购买数量 = myData.列表型记录["道具限购"][SceneName + "_" + 名字][0];
                 }
                 else {
                     myData.列表型记录["道具限购"].Add(SceneName + "_" + 名字, new List<string>() { { 购买数量 } });
                 }
             }
             else {
                 myData.列表型记录.Add("道具限购", new Dictionary<string, List<string>>() { { SceneName + "_" + 名字, new List<string>() { { 购买数量 } } } });
             }
         }


         if (购买数量.Equals("无限")|| int.Parse(购买数量) > 0)
         {
             if (pm.失去金钱(钱币, int.Parse(价格)).Equals("成功"))
             {
                 //Prop_bascis 物品 = pm.检索物品(名字);
                 pm.获取物品(名字, 1);
                 gut.生成警告框("购买成功");
                 if (!购买数量.Equals("无限")) {
                     购买数量 = int.Parse(购买数量) - 1 + "";
                     myData = io_.load();
                     myData.列表型记录["道具限购"][SceneName + "_" + 名字][0] = 购买数量;
                     io_.save(myData);
                 }
             }
             else {
                 gut.生成警告框("金钱不足");
             }
         }
         else {
             gut.生成警告框("购买达上限");
         }*/


        //先不考虑限购的问题
        role_Data myData = io_.load();
        int 最大购买数 = bm.Xstoi(myData.金钱[钱币]) /int.Parse( 价格);
        if (最大购买数 < 1)
        {
            gut.生成警告框("金钱不足!");
        }
        /*else if (最大购买数 < 2)
        {
            string 返回值 = pm.失去金钱(钱币,int.Parse( 价格));
            if (返回值.Equals("成功"))
            {
                pm.获取物品(名字,1);
            }
            else {
                gut.生成警告框(返回值);
            }
        }*/
        else {
            gut.生成拉条(名字,"" ,int.Parse(价格), 最大购买数, 钱币, "购买");
        }
    }
}
