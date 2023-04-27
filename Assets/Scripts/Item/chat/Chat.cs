using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    private Text 聊天框文本;
    private Scrollbar 聊天框进度值;
    private Dropdown 下拉框;
    private string 选中项;
    private ChatMgr cm;


    private void Awake()
    {
        聊天框进度值 = transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>();
        cm = ChatMgr.GetInstance();
        聊天框文本 = transform.Find("Scroll View/Viewport/Content/Text").GetComponent<Text>();
        下拉框 = transform.Find("chat_channel/Dropdown").GetComponent<Dropdown>();
        选中项 = 下拉框.options[下拉框.value].text;
    }

    public void 刷新聊天框() {
        选中项 = 下拉框.options[下拉框.value].text;
        if (选中项.Equals("综合"))
            刷新综合聊天框();
        else if (选中项.Equals("战斗"))
            刷新战斗聊天框();
        else if (选中项.Equals("系统"))
            刷新系统聊天框();

    }


    public void 刷新综合聊天框() {
        string str_综合信息 = "";
        for (int i = 0; i < cm.综合信息.Count; i++) {
            if (i == cm.综合信息.Count - 1)
                str_综合信息 += cm.综合信息[i];
            else
                str_综合信息 += cm.综合信息[i] + "\n";
        }
        聊天框文本.text = str_综合信息;
    }

    public void 刷新战斗聊天框()
    {
        string str_战斗信息 = "";
        for (int i = 0; i < cm.战斗信息.Count; i++)
        {
            if (i == cm.战斗信息.Count - 1)
                str_战斗信息 += cm.战斗信息[i];
            else
                str_战斗信息 += cm.战斗信息[i] + "\n";
        }
        聊天框文本.text = str_战斗信息;
    }

    public void 刷新系统聊天框()
    {
        string str_系统信息 = "";
        for (int i = 0; i < cm.系统信息.Count; i++)
        {
            if (i == cm.系统信息.Count - 1)
                str_系统信息 += cm.系统信息[i];
            else
                str_系统信息 += cm.系统信息[i] + "\n";
        }
        聊天框文本.text = str_系统信息;
    }
}
