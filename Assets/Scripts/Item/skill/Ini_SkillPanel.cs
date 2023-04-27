using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ini_SkillPanel : MonoBehaviour
{

    private basicMgr bm;
    private PropMgr pm;
    private G_Util gut;
    private GameObject 列表;

    private void Awake()
    {
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
        列表 = gameObject.transform.Find("Panel/Scroll View/Viewport/Content").gameObject;
    }

    private void OnEnable()
    {
        gameObject.transform.Find("Panel").transform.Find("技能页").gameObject.SetActive(true);//显示技能学习页的样式
        //刷新所有的学习状态();
    }

    public void ini_building(Dictionary<string, int> 技能列表)
    {
        int index = 0;
        
        foreach (string 技能名 in 技能列表.Keys) {
            SkillData sd = pm.检索技能(技能名);
            gut.生成技能学习项(sd, 技能列表[技能名],"铜币", 列表, index, 技能列表.Count);
            index++;

        }
    
    }

        private void OnDisable()
    {
        gut.操作子物体(列表,3);
        gameObject.GetComponent<Ini_SkillPanel>().enabled = false;
    }




}
