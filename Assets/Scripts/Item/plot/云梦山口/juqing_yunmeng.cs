using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class juqing_yunmeng : MonoBehaviour
{
    private Dictionary<string, UnityAction> 柳医师选项 = new Dictionary<string, UnityAction>();
    private G_Util ut;

    private void Awake()
    {
        ut = NameMgr.画布.GetComponent<G_Util>();
        柳医师选项.Add("治疗", ut.治疗);
        if(PlayerPrefs.GetInt("柳医师获得护符")==0)
            柳医师选项.Add("您在这里干嘛", 柳医师剧情0);
        if (PlayerPrefs.GetInt("拜师柳医师") == 0)
            柳医师选项.Add("学习医术", 柳医师剧情3); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 柳医师对话索引()
    {    
            柳医师选择项(柳医师选项);
    }

    private void 柳医师剧情0()
    {
        ut.关闭杂项();
        string str = "传闻这一带有人参娃娃出没,打败它有几率掉一种让人百病不侵的护符,真是神奇呀.我来这是想研究一番.";
        ut.生成对话框(str, 0, 0.08f, "柳医师对话");
        柳医师选项.Remove("您在这里干嘛");
        柳医师选项.Add("我找来给您!", 柳医师剧情1);
    }

    private void 柳医师剧情1() {
        ut.关闭杂项();
        ut.生成场景任务框("寻找让人百病不侵的护符", "柳医师正在寻找一种让人百病不侵的护符来研究,你自告奋勇的去寻找,并且听说这种护符出自这一带的人参娃娃身上...", "人参的守护",1,"灵芝", "让人百病不侵的护符?");
    }

    public void 柳医师剧情2() {
        ut.关闭杂项();
        柳医师选项.Remove("我找来给您!");
        string str = "哇哦.原来让人百病不侵的原因是护符上含有人参娃娃的药性...原来如此,我把这个护符的药性提取出来了.也送你一份";
        ut.生成对话框(str, 0, 0.08f, "柳医师获得护符");
    }

    public void 柳医师剧情3()
    {
        ut.关闭杂项();
        ut.生成场景任务框("学习医术", "看在都是村里人的分上我就教给你,不过你需要给我一份拜师礼...emm,那就给我一份人参的精华吧", "人参精华", 1, "包扎技能书", "");
    }

    public void 柳医师剧情4()
    {
        ut.关闭杂项();
        柳医师选项.Remove("学习医术");
        string str = "话说一日为师,终身为父...要不,你叫我一声爸爸听听...";
        ut.生成对话框(str, 0, 0.08f, "拜师柳医师");
    }

    private void 柳医师选择项(Dictionary<string, UnityAction> 选项信息)
    {
        ut.生成选项框(选项信息, gameObject);

    }
}
