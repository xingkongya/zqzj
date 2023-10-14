using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Panel_Eqm : MonoBehaviour
{
    private io io_;
    private PropMgr pm;
    private combat cb;
    private G_Util gut;
    private basicMgr bm;

    private void Awake()
    {
        bm = basicMgr.GetInstance();
        io_ = io.GetInstance();
        pm = PropMgr.GetInstance();
        if (!NameMgr.画布.CompareTag("幻界")&&GameObject.Find("人物")!=null)
            cb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
        gut = NameMgr.画布.GetComponent<G_Util>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        装备栏生成流光();
    }


    private void Start()
    {
        装备栏生成流光();
    }

    public void 点击装备框()
    {
        gut.关闭杂项();
        if (GameObject.FindGameObjectsWithTag("选中").Length > 0)
        {
            GameObject[] 选中栏 = GameObject.FindGameObjectsWithTag("选中");
            foreach (GameObject 装备框 in 选中栏)
            {
                装备框.tag = "未选中";
                装备框.transform.Find("边框").gameObject.SetActive(false);
            }
        }

        //改变标签名
        gameObject.tag = "选中";
        gameObject.transform.Find("边框").gameObject.SetActive(true);
        string name = gameObject.transform.Find("Text").GetComponent<Text>().text;
        if (gameObject.name != name)
        {
            Dictionary<string, UnityAction> 选项信息 = new Dictionary<string, UnityAction>();
            选项信息.Add("查看", 查看装备);
            选项信息.Add("卸下装备", 卸下装备);
            gut.生成选项框(选项信息, gameObject);
        }

    }

    public void 查看装备()
    {
        gut.关闭杂项();
        role_Data myData = io_.load();
        string name = gameObject.name;
        Equipment 装备 = myData.装备槽[name];
        gut.生成物品信息(装备, 0,name);
    }

    private void 卸下装备()
    {
        if (gameObject.transform.Find("流光(Clone)"))//卸下装备肯定没流光
            Destroy(gameObject.transform.Find("流光(Clone)").gameObject);

        gut.关闭杂项();
        string 面板装备名 = gameObject.transform.Find("Text").GetComponent<Text>().text;
        if (pm.检索物品(面板装备名).name != null)
        {
            GameObject 人物 = DataMgr.GetInstance().本地对象["主角"];
            if (人物 != null)
            {

                //写入数据
                Role_Panel rp = gameObject.transform.parent.parent.parent.parent.parent.gameObject.GetComponent<Role_Panel>();
                role_Data myData = io_.load();
                Equipment 装备 = myData.装备槽[gameObject.name];
                myData.装备槽[gameObject.name] = null;
                io_.save(myData);
                pm.获取物品(装备);
                cb = 人物.GetComponent<combat>();
                if (cb != null && !NameMgr.画布.CompareTag("幻界"))
                    cb.人物属性刷新();


                //改变UI
                gameObject.transform.Find("边框").gameObject.SetActive(false);
                rp.ini_panel();
                gut.检索装备栏生成流光();

                //刷新属性
                gut.面板属性刷新(gameObject.transform.parent.parent.parent.parent.parent.gameObject);
            }
            else {
                gut.生成警告框("复活中不可操作");
            }
        }
    }



    public void 装备栏生成流光()
    {
        PropMgr pm = PropMgr.GetInstance();
        Dictionary<string, int> 套装信息 = pm.装备套装信息;
        string 装备名_str = gameObject.transform.Find("Text").GetComponent<Text>().text;
        string 装备名;
        if (装备名_str.IndexOf("+") != -1)
        {
            装备名 = 装备名_str.Split('+')[0];
        }
        else {
            装备名 = 装备名_str;
        }
        Equipment 装备 = pm.检索物品(装备名) as Equipment;
        if (装备 != null && !装备.tao.Equals(""))
        {
            Color nowColor = new Color();
            if (bm.Xstoi(装备.qua) == 1)
                ColorUtility.TryParseHtmlString("#00F80E", out nowColor);//绿色
            else if (bm.Xstoi(装备.qua) == 2)
                ColorUtility.TryParseHtmlString("#0023F8", out nowColor);//蓝色
            else if (bm.Xstoi(装备.qua) == 3)
                ColorUtility.TryParseHtmlString("#FF00DF", out nowColor);//紫色
            else if (bm.Xstoi(装备.qua) == 4)
                ColorUtility.TryParseHtmlString("#FDE61E", out nowColor);//金色
            else if (bm.Xstoi(装备.qua) == 5)
                ColorUtility.TryParseHtmlString("#FF0000", out nowColor);//红色

            foreach (string 套装 in 套装信息.Keys)
            {

                if (装备.tao.Equals(套装) && 套装信息[套装] >= 2 && !gameObject.transform.Find("流光(Clone)"))
                {
                    gut.生成套装流光(gameObject, nowColor);
                }
            }
        }
    }

    public void 删除无用流光()
    {
        PropMgr pm = PropMgr.GetInstance();
        Dictionary<string, int> 套装信息 = pm.装备套装信息;
        string 装备名 = gameObject.transform.Find("Text").GetComponent<Text>().text;
        Equipment 装备 = pm.检索物品(装备名) as Equipment;
        if (装备 != null && !装备.tao.Equals(""))
        {
            foreach (string 套装 in 套装信息.Keys)
            {
                if (装备.tao.Equals(套装) && 套装信息[套装] < 2&& gameObject.transform.Find("流光(Clone)"))
                    Destroy(gameObject.transform.Find("流光(Clone)").gameObject);
            }
        }
        else
        {
            if (gameObject.transform.Find("流光(Clone)"))
                Destroy(gameObject.transform.Find("流光(Clone)").gameObject);
        }


    }

    private void OnDisable()
    {
        删除无用流光();
    }
}
