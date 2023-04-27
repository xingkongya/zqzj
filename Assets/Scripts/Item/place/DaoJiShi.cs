using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaoJiShi : MonoBehaviour
{
    private Text 时间文本;
    public string boss名字;
    private float 刷新计时;
    private SkillApplicator sa;

    private void Awake()
    {
        sa = SkillApplicator.GetInstance();
        时间文本 = gameObject.GetComponent<Text>();
    }

    private void Start()
    {
       
    }


    // Update is called once per frame
    void Update()
    {
        if (sa.CD集合.ContainsKey(boss名字)) {
            刷新计时 = sa.CD集合[boss名字];
            时间文本.text = (int)刷新计时 + "s";
        }
    }
}
