using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class m_Refresh : MonoBehaviour
{
    public bool 是否会刷新 = true;
    public bool isRefresh=false;
    public float 刷新时间 = 5.0f;
    public float 刷新计时器;
    private GameObject monster;
    private SkillApplicator sa;
    private string M_name;
    private G_Util gut;

    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        sa = SkillApplicator.GetInstance();
        monster = gameObject.transform.GetChild(0).gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (monster.tag == "建筑")
        {
            刷新时间 = 2.0f;
            刷新计时器 = 刷新时间;
        }
        else if (monster.CompareTag("boss"))
        {
            M_name = monster.name;
            if (sa.CD集合.ContainsKey(M_name))
            {
                刷新计时器 = sa.CD集合[M_name];
                monster.SetActive(false);
                monster.GetComponent<combat>().掉落集合.Clear();
                if (gameObject.transform.parent.Find("倒计时") != null)
                    gameObject.transform.parent.Find("倒计时").gameObject.SetActive(true);
            }
        }
        else
            刷新计时器 = 刷新时间;
    }

    // Update is called once per frame
    void Update()
    {
        if (是否会刷新)
        {
            if (isRefresh)
            {
                刷新计时器 -= Time.deltaTime;
                if (刷新计时器 < 0)
                {

                    combat cb = monster.GetComponent<combat>();
                    monster.SetActive(true);
                    if (monster.tag == "建筑")
                        cb.建筑初始化();
                    else if (monster.tag == "怪物")
                        cb.怪物初始化();
                    else if (monster.tag == "boss")
                    {
                        cb.掉落集合.Clear();
                        cb.怪物初始化();
                        if (gameObject.transform.parent.Find("倒计时") != null)//如果有计时器,则显示计时器
                            gameObject.transform.parent.Find("倒计时").gameObject.SetActive(false);

                        if (gut.运行中的时间线程.Contains(monster.name))
                            gut.运行中的时间线程.Remove(monster.name);
                    }


                    刷新计时器 = 刷新时间;
                    isRefresh = false;
                }

            }
        }
    }

 

}
