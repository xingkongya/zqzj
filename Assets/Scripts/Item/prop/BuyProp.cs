using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyProp : MonoBehaviour
{
    private basicMgr bm;
    private G_Util gut;
    private GameObject 铜币按钮;
    private GameObject 金币按钮;
    private GameObject 仙晶按钮;
    private GameObject 黑钻按钮;


    private void Awake()
    {
        bm = basicMgr.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
        铜币按钮 = gameObject.transform.Find("Panel").Find("商城页/选项/铜币商店").gameObject;
        金币按钮 = gameObject.transform.Find("Panel").Find("商城页/选项/金币商店").gameObject;
        仙晶按钮 = gameObject.transform.Find("Panel").Find("商城页/选项/仙晶商店").gameObject;
        黑钻按钮 = gameObject.transform.Find("Panel").Find("商城页/选项/黑钻商店").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void 点击铜币选项()
    {
        bm.改变颜色(铜币按钮.GetComponent<Image>(), "FFFFFF");
        bm.改变颜色(金币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(仙晶按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(黑钻按钮.GetComponent<Image>(), "A4A0A0");
        gut.初始化铜币商城();
    }


   

    public void 点击金币选项()
    {
        bm.改变颜色(铜币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(金币按钮.GetComponent<Image>(), "FFFFFF");
        bm.改变颜色(仙晶按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(黑钻按钮.GetComponent<Image>(), "A4A0A0");
        gut.初始化金币商城();
    }




    public void 点击仙晶选项()
    {
        bm.改变颜色(铜币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(金币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(仙晶按钮.GetComponent<Image>(), "FFFFFF");
        bm.改变颜色(黑钻按钮.GetComponent<Image>(), "A4A0A0");
        gut.初始化仙晶商城();
    }



    public void 点击黑钻选项()
    {
        bm.改变颜色(铜币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(金币按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(仙晶按钮.GetComponent<Image>(), "A4A0A0");
        bm.改变颜色(黑钻按钮.GetComponent<Image>(), "FFFFFF");
        gut.初始化黑钻商城();
    }


}
