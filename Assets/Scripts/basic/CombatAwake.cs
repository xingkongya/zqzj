using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatAwake : MonoBehaviour
{
   

    private void OnEnable()
    {
        GameObject 画布 = NameMgr.画布;
        if (画布.transform.Find("combat_other"))
            Destroy(gameObject);
        if(画布!=null)
            gameObject.transform.SetParent(画布.transform);


        //战斗场地初始化
        gameObject.transform.localPosition = new Vector3(0,100,0);//设置生成位置
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1080,910);//recttransform必不可少的属性(半知半解)
        gameObject.transform.localScale = new Vector3(1f, 1f, 1);//设置生成的大小

        if (gameObject.name.Equals("combat(Clone)"))
            return;

       
    }

    public void 关闭杂项()
    {
        //关闭选项框
        GameObject[] 选项框 = GameObject.FindGameObjectsWithTag("杂项");
        if (选项框.Length != 0)
        {
            for (int i = 0; i < 选项框.Length; i++)
            {
                Destroy(选项框[i]);
            }
        }
    }







}


