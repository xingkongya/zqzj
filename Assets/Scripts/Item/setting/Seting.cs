using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Seting : MonoBehaviour
{
    private io io_;
    GameObject 背景音乐;
    GameObject 游戏音效;
    Slider 背景音乐拉条;
    Slider 游戏音效拉条;
    AudioSource 主音乐;

    private void Awake()
    {
        io_ = io.GetInstance();
    }

    private void Start()
    {
        role_Data myData = io_.load();
        背景音乐 = gameObject.transform.Find("声音/背景音乐").gameObject;
        游戏音效 = gameObject.transform.Find("声音/游戏音效").gameObject;
        背景音乐拉条 = 背景音乐.transform.Find("Slider").GetComponent<Slider>();
        游戏音效拉条 = 游戏音效.transform.Find("Slider").GetComponent<Slider>();
        主音乐 = GameObject.Find("audio").GetComponent<AudioSource>();

        //根据存档记录设置背景音乐
        if (myData.记录.ContainsKey("背景音乐"))
        {
            背景音乐.transform.Find("Button/打勾").gameObject.SetActive(true);
            背景音乐拉条.value =int.Parse( myData.记录["背景音乐"]);
            背景音乐.transform.Find("Panel/Text_num").GetComponent<Text>().text =myData.记录["背景音乐"]+ "";
        }
        else {
            背景音乐.transform.Find("Button/打勾").gameObject.SetActive(false);
            //背景音乐拉条.value = 50f;
            背景音乐.transform.Find("Panel/Text_num").GetComponent<Text>().text = "off";
        }
        //根据存档记录设置游戏音效
        if (myData.记录.ContainsKey("游戏音效"))
        {
            游戏音效.transform.Find("Button/打勾").gameObject.SetActive(true);
            游戏音效拉条.value =int.Parse( myData.记录["游戏音效"]);
            游戏音效.transform.Find("Panel/Text_num").GetComponent<Text>().text = myData.记录["游戏音效"] + "";
        }
        else
        {
            游戏音效.transform.Find("Button/打勾").gameObject.SetActive(false);
            //游戏音效拉条.value = 50f;
            游戏音效.transform.Find("Panel/Text_num").GetComponent<Text>().text = "off";
        }
    }

    private void 关闭背景音乐() {
        Debug.Log("关音乐");
        背景音乐.transform.Find("Button/打勾").gameObject.SetActive(false);
        背景音乐拉条 = 背景音乐.transform.Find("Slider").GetComponent<Slider>();
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey("背景音乐"))
            myData.记录.Remove("背景音乐");
        else
            Debug.Log("未找到背景音乐的设置信息");
        背景音乐.transform.Find("Panel/Text_num").GetComponent<Text>().text = "off";
        主音乐.mute = true;
        io_.save(myData);
    }

    private void 关闭游戏音效()
    {
        
        游戏音效.transform.Find("Button/打勾").gameObject.SetActive(false);
        游戏音效拉条 = 游戏音效.transform.Find("Slider").GetComponent<Slider>();
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey("游戏音效"))
            myData.记录.Remove("游戏音效");
        else
            Debug.Log("未找到游戏音效的设置信息");
        游戏音效.transform.Find("Panel/Text_num").GetComponent<Text>().text = "off";
        io_.save(myData);
    }

    private void 开启背景音乐()
    {
       
        背景音乐.transform.Find("Button/打勾").gameObject.SetActive(true);
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey("背景音乐"))
            背景音乐拉条.value =int.Parse( myData.记录["背景音乐"]);
        else
        {
            int num = (int)背景音乐.transform.Find("Slider").GetComponent<Slider>().value;
            myData.记录.Add("背景音乐", num+"");
        }
        背景音乐.transform.Find("Panel/Text_num").GetComponent<Text>().text = myData.记录["背景音乐"]+"";
        主音乐.volume = int.Parse(myData.记录["背景音乐"])/100f;
        主音乐.mute = false;
        io_.save(myData);
    }

    private void 开启游戏音效()
    {       
        游戏音效.transform.Find("Button/打勾").gameObject.SetActive(true);
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey("游戏音效"))
            游戏音效拉条.value = int.Parse(myData.记录["游戏音效"]);
        else
        {
            int num = (int)游戏音效.transform.Find("Slider").GetComponent<Slider>().value;
            myData.记录.Add("游戏音效", num+"");
        }
        游戏音效.transform.Find("Panel/Text_num").GetComponent<Text>().text = myData.记录["游戏音效"] + "";
        io_.save(myData);
    }

    public void 点击开关背景音乐按钮() {
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey("背景音乐"))
            关闭背景音乐();
        else
            开启背景音乐();
    }

    public void 点击开关游戏音效按钮()
    {
        role_Data myData = io_.load();
        if (myData.记录.ContainsKey("游戏音效"))
            关闭游戏音效();
        else
            开启游戏音效();
    }

    public void 拉动背景音乐滑杆() {
        string SceneName = SceneManager.GetActiveScene().name;
        if (SceneName.Equals("登入界面"))
            return;
        else {
            背景音乐.transform.Find("Button/打勾").gameObject.SetActive(true);
            主音乐.mute = false;
            role_Data myData = io_.load();
            if (myData.记录.ContainsKey("背景音乐"))
                myData.记录["背景音乐"] = (int)背景音乐拉条.value+"";
            else
                myData.记录.Add("背景音乐", (int)背景音乐拉条.value+"");
            背景音乐.transform.Find("Panel/Text_num").GetComponent<Text>().text = (int)背景音乐拉条.value + "";
            主音乐.volume = int.Parse(myData.记录["背景音乐"]) / 100.0f;
            io_.save(myData);
        }

    }


    public void 拉动游戏音效滑杆()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        if (SceneName.Equals("登入界面"))
            return;
        else
        {
            游戏音效.transform.Find("Button/打勾").gameObject.SetActive(true);
            role_Data myData = io_.load();
            if (myData.记录.ContainsKey("游戏音效"))
                myData.记录["游戏音效"] = (int)游戏音效拉条.value+"";
            else
                myData.记录.Add("游戏音效", (int)游戏音效拉条.value+"");
            游戏音效.transform.Find("Panel/Text_num").GetComponent<Text>().text = (int)游戏音效拉条.value + "";
            io_.save(myData);
        }
    }

}
