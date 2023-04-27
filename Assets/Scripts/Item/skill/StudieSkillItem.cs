using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudieSkillItem : MonoBehaviour
{
    private RectTransform rt;
    private SkillMgr sm;
    private PropMgr pm;
    private G_Util gut;

    private void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
        sm = SkillMgr.GetInstance();
        pm = PropMgr.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void 检索技能学习状态()
    {
        string SkillName = gameObject.transform.Find("name").GetComponent<Text>().text;
        SkillData sd = pm.检索技能(SkillName);
        if (sd != null)
        {
            if (sm.技能查重(sd.place, sd))
            {
                GameObject 学习按钮 = gameObject.transform.Find("Button/学习").gameObject;
                学习按钮.transform.Find("Text").GetComponent<Text>().text = "已学习";
                Color nowColor;
                ColorUtility.TryParseHtmlString("#E7D9D9", out nowColor);//白色
                学习按钮.GetComponent<Image>().color = nowColor;
                学习按钮.GetComponent<Button>().enabled = false;
            }
        }
    }

    public void 空方法() {

    }

    // Update is called once per frame
    void Update()
    {
        if (rt.anchoredPosition.x != 0)
            rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y);
    }

    public void 背包技能项点击事件(){
        gut = NameMgr.画布.GetComponent<G_Util>();
        GameObject 技能信息 = GameObject.Find("技能信息界面(Clone)");
        if (技能信息 != null)
            Destroy(技能信息);

        //变色
        //将原先选中项颜色还原
        Color nowColor;
        if (GameObject.FindGameObjectsWithTag("未选中").Length > 0)
        {
            GameObject[] 未选中栏 = GameObject.FindGameObjectsWithTag("未选中");
            foreach (GameObject 技能项 in 未选中栏)
            {
                ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
                技能项.GetComponent<Image>().color = nowColor;
            }
        }

        //选中点击变色
        ColorUtility.TryParseHtmlString("#DBCDCD", out nowColor);
        gameObject.GetComponent<Image>().color = nowColor;

        string SkillName = gameObject.transform.Find("name").GetComponent<Text>().text;
        SkillData sd = pm.检索技能(SkillName);
        gut.生成技能信息(sd, 0);
    }


}
