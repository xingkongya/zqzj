using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move_UP : MonoBehaviour
{
    private float 透明值;
    private GameObject 文本;
    private GameObject 名字;
    private GameObject 边框;
    private basicMgr bm;
    private bool isnull=false;
    private G_Util gut;
    private Vector3 bagTf;
    private GameObject 画布;
    // Start is called before the first frame update
    void Start()
    {
        透明值 = 255;
        文本 = gameObject.transform.Find("Text").gameObject;
        名字 = gameObject.transform.Find("名字").gameObject;
        边框 = gameObject.transform.Find("Panel").gameObject;
        bm = basicMgr.GetInstance();
        画布 = GameObject.Find("老画布");
        if (画布 == null)
            画布 = GameObject.Find("画布");
        gut = 画布.GetComponent<G_Util>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isnull)
        {
            string 名字 = gameObject.transform.Find("名字").GetComponent<Text>().text;
            bagTf =GameObject.Find("bag_bg_pic").transform.position;
            gut.生成光球by名字(名字,gameObject.transform.parent.gameObject, bagTf,0.5f);
            Destroy(gameObject);
        }
        文本.GetComponent<Text>().color = bm.改变透明度(文本, 透明值 -= Time.deltaTime*20);
        名字.GetComponent<Text>().color = bm.改变透明度(名字, 透明值 -= Time.deltaTime*20);
        边框.GetComponent<Image>().color = bm.改变透明度(边框, 透明值 -= Time.deltaTime * 30);
        gameObject.GetComponent<Image>().color = bm.改变透明度(gameObject, 透明值 -= Time.deltaTime*35);
        if (透明值 <= 0)
            isnull = true;
        

    }


}
