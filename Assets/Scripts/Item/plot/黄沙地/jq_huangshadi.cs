using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class jq_huangshadi : MonoBehaviour
{
    private Dictionary<string, UnityAction> 父亲选项 = new Dictionary<string, UnityAction>();
    private G_Util ut;

    private void Awake()
    {
        int 打招呼 = PlayerPrefs.GetInt("与父亲打招呼");
        int 问孵化 = PlayerPrefs.GetInt("问孵化");
        int 父亲给的路 = PlayerPrefs.GetInt("父亲给的路");
        ut = NameMgr.画布.GetComponent<G_Util>();
        if (打招呼 == 0)
            父亲选项.Add("打招呼", 父亲剧情0);
        if (打招呼>=2&&问孵化==0)
            父亲选项.Add("父亲给的路", 父亲剧情3);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void 父亲对话索引()
    {
        int 打招呼 = PlayerPrefs.GetInt("与父亲打招呼");
        int 问孵化 = PlayerPrefs.GetInt("问孵化");
        int 父亲给的路 = PlayerPrefs.GetInt("父亲给的路");
        Debug.Log(打招呼+"___"+问孵化+"___"+父亲给的路);
        if (打招呼 == 0)
            父亲选择项(父亲选项);
        else if (打招呼 == 1)
            父亲剧情1();
        else if (打招呼 >= 2 && 父亲给的路 == 0)
            父亲选择项(父亲选项);
        else if (父亲给的路 < 2)
            父亲剧情4();
        else
            父亲对话();
    }

    private void 父亲剧情0()
    {
        ut.关闭杂项();
        string str = "儿呀,你来了,快来看这里...\n(父亲小心翼翼地刨开前面的沙石,露出了一颗鸡蛋大小的蛋.蛋表面覆盖了一层红色的纹路,此时正散发着强烈的热量)";
        ut.生成对话框(str, 0, 0.08f, "与父亲打招呼");
        父亲选项.Remove("打招呼");
    }

    private void 父亲剧情1()
    {
        ut.关闭杂项();
        string str = "这颗蛋是我在半个月前打猎时发现的,发现时它旁边环绕着火焰,当时我一眼就觉得这蛋不简单.于是偷偷的把它藏起来了.\n你不是和翠花约战么,我把它给你,希望能帮到你...";
        role_Data myData = io.GetInstance().load();
        if (!myData.记录.ContainsKey("拿小鸡蛋")) {
            EventCenter.GetInstance().AddEventListener("对话结束", 获得小鸡蛋);
        }
        ut.生成对话框(str, 1, 0.08f, "与父亲打招呼");
        if (!父亲选项.ContainsKey("询问孵化")) {
            父亲选项.Add("父亲给的路", 父亲剧情3);
        }
    }

    public void 获得小鸡蛋() {
        PropMgr pm = PropMgr.GetInstance();
        ut.生成获得框("(完美)小鸡之卵",1);
        pm.获取物品("(完美)小鸡之卵",1);
        ut.存档记录("拿小鸡蛋", "1");
    }

    public void 获得签名纸()
    {
        PropMgr pm = PropMgr.GetInstance();
        ut.生成获得框("签名纸", 1);
        pm.获取物品("签名纸", 1);
        ut.存档记录("签名纸条", "1");
    }

    public void 父亲对话()
    {
        ut.关闭杂项();
        string str = "去去去,别影响我打猎...";
        ut.生成对话框(str, 0, 0.08f, "父亲对话");
    }

    public void 父亲剧情2()
    {
        ut.关闭杂项();
        string str = "我也不太清楚,不过在藏蛋后看到住在雪湖的红衣女子来过.我以为蛋是她的,犹豫着要不要还给她时,她就离开了,我想她应该是知道些什么...现在蛋给你了,是自己想办法孵化还是找她问清楚都交给你决定了.";
        ut.生成对话框(str, 0, 0.08f, "问孵化");
        父亲选项.Remove("询问孵化");
    }

    public void 父亲剧情3()
    {
        ut.关闭杂项();
        string str = "对了,你爹我年轻时也是体面人,曾经在临海城当过除妖师.当年遇到一个落魄青年,在他走投无路时给了他一块饼,他当时很感激,留下了他的签名,告诉了我如果他平步青云了一定要报答我...";
        ut.生成对话框(str, 0, 0.08f, "父亲给的路");
        父亲选项.Remove("父亲给的路");
    }

    public void 父亲剧情4()
    {
        ut.关闭杂项();
        string str = "后来他真的发迹了,听说在临海城当了镇守,这张签名给你.你如果想变强可以去找他(临海城在右上方向)...";
        role_Data myData = io.GetInstance().load();
        if (!myData.记录.ContainsKey("签名纸条"))
        {
            EventCenter.GetInstance().AddEventListener("对话结束", 获得签名纸);
        }
        ut.生成对话框(str, 1, 0.08f, "父亲给的路");
    }

    private void 父亲选择项(Dictionary<string, UnityAction> 选项信息)
    {
        ut.生成选项框(选项信息, gameObject);

    }
}
