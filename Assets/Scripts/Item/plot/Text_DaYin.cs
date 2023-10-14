using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_DaYin : MonoBehaviour
{
    //基本
    private bool is_off = false;
    public Text wenZiText;
    public string str = "";
    string s = "";
    public float 语速 = 0.2f;
    private EventCenter em = EventCenter.GetInstance();
    private G_Util gut;
    public bool isYingdao=false;


    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
    }
    public void playText()
    {
        StartCoroutine(showText(str.Length));
    }
    IEnumerator showText(int strLenght)
    {
        int i = 0;
        s = "";
        while (i < strLenght)
        {
            yield return new WaitForSeconds(语速);
            if (str.Equals(""))
                yield break;
           s += str[i].ToString();
            wenZiText.text = s;
            i += 1;
        }
        if (isYingdao)
        {
            em.EventTrigger("对话结束生成引导");
            em.ClearEventListener("对话结束生成引导");
        }
        else {
            em.EventTrigger("对话结束");
            em.ClearEventListener("对话结束");
        }
        is_off =true;
        StopAllCoroutines();//结束携程
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        s = "";
        str = "";
    }

    public void 关闭对话框()
    {
        //先判断是否可以关闭
        GameObject 对话框 = GameObject.Find("对话框(Clone)");
        if (is_off)
        {
            Destroy(对话框);
            //em.AddEventListener("对话后剧情", gut.关闭杂项);
            em.EventTrigger("对话后剧情");
            em.ClearEventListener("对话后剧情");
        }

    }

    public void 强制关闭对话框()
    {
        GameObject 对话框 = GameObject.Find("对话框(Clone)");
        Destroy(对话框);
    }
}
