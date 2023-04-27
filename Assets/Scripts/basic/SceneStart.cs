using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneStart : MonoBehaviour
{

    private float timer = 2.0f;

    private GameObject 地址框;
    GameObject 画布;
    private G_Util gut;

    private void Awake()
    {
        画布 = NameMgr.画布;
        gut = 画布.GetComponent<G_Util>();
    }


    private void OnEnable()
    {
       
        地址框 = 画布.transform.Find("UI/战斗页/2级画布/address/address_").gameObject;
        地址框.SetActive(true);

        if (GameObject.Find("Camera"))
            GameObject.Find("Camera").gameObject.GetComponent<Camera>().enabled=false;

        if (画布.transform.Find("combat_other"))
            Destroy(gameObject);
        if (画布 != null)
            gameObject.transform.SetParent(画布.transform);


        //战斗场地初始化
        gameObject.transform.localPosition = new Vector3(0, 100, 0);//设置生成位置
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 910);//recttransform必不可少的属性(半知半解)
        gameObject.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小

        if (gameObject.name.Equals("combat(Clone)"))
            return;


        //地址初始化
        string SceneName = SceneManager.GetActiveScene().name;
        画布.transform.Find("UI/战斗页/2级画布/address/Text_bg_address/Text_Name").GetComponent<Text>().text = SceneName;
        GameObject UI = 画布.transform.Find("UI").gameObject;
        地址框.transform.Find("address_Text").GetComponent<Text>().text = SceneName;

      
        gut.刷新金钱UI(UI);
        gut.刷新战斗力UI(UI);
        role_Data myData = io.GetInstance().load();
        if(!SceneName.Equals("通天塔"))
            myData.登录场景 = SceneName;
        io.GetInstance().save(myData);

        //属性初始化
        combat rolecb = DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>();
        GameObject 宠物 = GameObject.FindWithTag("宠物");
        combat petcb = null;
        if (宠物 != null)
        {
            petcb = 宠物.GetComponent<combat>();
            petcb.仇恨列表.Clear();
        }
        rolecb.仇恨列表.Clear();

        //界面调整
        GameObject cot = GameObject.Find("combat(Clone)");
        cot.transform.SetAsLastSibling();


        gut.清空场景怪物信息();
       
    }


    private void Start()
    {
        gameObject.transform.localPosition = new Vector3(0, 100, 0);//设置生成位置
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            地址框.SetActive(false);
            timer = 2.0f;
        }
    }
}
