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
        if(PlayerPrefs.GetInt("柳医师获得松果")==0)
            柳医师选项.Add("神奇松果", 柳医师剧情1);
        if (PlayerPrefs.GetInt("学习医术") == 0)
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
        string str = "传闻这一带有人参娃娃出没,我来这是想研究一番.";
        ut.生成对话框(str, 0, 0.08f, "柳医师对话");
        柳医师选项.Remove("您在这里干嘛");
        柳医师选项.Add("我找来给您!", 柳医师剧情1);
    }

    private void 柳医师剧情1() {
        ut.关闭杂项();
        ut.生成场景任务框("神奇松果", "<color=blue>小树林</color>里有只小松鼠特别有灵性,初步怀疑是它常吃松果的作用...帮我抢一份松果我研究研究", "大松果",1,"灵芝", "");
    }

    public void 柳医师剧情2() {
        ut.关闭杂项();
        柳医师选项.Remove("神奇松果");
        string str = "说来惭愧,研究半天没研究出来什么,不过味道倒是挺不错的,咳咳...";
        ut.生成对话框(str, 0, 0.08f, "柳医师获得松果");
    }

    public void 柳医师剧情3()
    {
        ut.关闭杂项();
        ut.生成场景任务框("学习医术", "啥!这可是我吃饭的手艺呀...罢了,看在都是村里人的份上,帮我从<color=purple>人参娃娃</color>那收集一份<color=purple>人参精华</color>,我就教你. ", "人参精华", 1, "(绝招)<包扎>", "");
    }

    public void 柳医师剧情4()
    {
        ut.关闭杂项();
        柳医师选项.Remove("学习医术");
        string str = "话说一日为师,终身为父...要不,你叫我一声爸爸听听...";
        ut.生成对话框(str, 0, 0.08f, "学习医术");
    }

    private void 柳医师选择项(Dictionary<string, UnityAction> 选项信息)
    {
        ut.生成选项框(选项信息, gameObject);

    }
}
