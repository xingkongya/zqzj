using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Weather : MonoBehaviour
{

    private basicMgr bm;
    GameObject 当前天气;
    Random 随机类;
    int index_Wea;
    int num_Wea=0;


    private void Awake()
    {
        bm = basicMgr.GetInstance();

        index_Wea = 随机天气();
        if (index_Wea >= 50 && index_Wea <= 100)//大晴天路线
        { 
        
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        if (num_Wea <= 70)//5分之3是晴天
            当前天气 = gameObject.transform.GetChild(0).gameObject;          
        else if(num_Wea <= 90)//10分之3是阴天
            当前天气 = gameObject.transform.GetChild(1).gameObject;
        else if (num_Wea <= 100)//10分之一是雨天
            当前天气 = gameObject.transform.GetChild(2).gameObject;

        当前天气.SetActive(true);
      

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 20 == 0)
        {
            int 前缀 = 随机类.Next(-1, 2);
            float 增值_系数 = (float)随机类.NextDouble() * 10;
            float 增值 = (float)前缀 * 增值_系数;
            float 当前透明度 = 当前天气.GetComponent<Image>().color.a * 255;
            if (当前透明度 > 180)
                当前透明度 = 180;
            if (当前透明度 < 0)
                当前透明度 = 0;
            当前天气.GetComponent<Image>().color = bm.改变透明度(当前天气, 当前透明度 + 增值);
        }
    }

    int  随机天气() {
        随机类 = new Random(Guid.NewGuid().GetHashCode());
        index_Wea = 随机类.Next(1, 101);
        return index_Wea;
    }

    void 大晴天路线() {
        num_Wea += 10;
    }
}
