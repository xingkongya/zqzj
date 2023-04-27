using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTag : MonoBehaviour
{
    private G_Util gut;
    private PropMgr pm;

    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        pm = PropMgr.GetInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick() {

        if (GameObject.FindGameObjectsWithTag("选中").Length > 0)
        {
            GameObject[] 选中栏 = GameObject.FindGameObjectsWithTag("选中");
            foreach (GameObject 道具项 in 选中栏)
            {
                道具项.tag = "未选中";
            }
        }

        //改变标签名
        gameObject.tag = "选中";
    }


    public void 生成物品信息_道具购买项() {
        GameObject 选中 = GameObject.FindGameObjectWithTag("选中");
        if (选中 != null) {
            string 名字 = 选中.transform.parent.parent.Find("名字/Text").GetComponent<Text>().text;
            Prop_bascis pb = pm.检索物品(名字);
            gut.生成物品信息(pb, 0);
        }
    }




}
