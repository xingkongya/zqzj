using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JqIndexMgr : BaseManager<JqIndexMgr>
{
    public int 母亲 = PlayerPrefs.GetInt("母亲");
    public int 村长 = PlayerPrefs.GetInt("村长");
    public int 玉佩 = PlayerPrefs.GetInt("玉佩");
    public int 打听父母 = PlayerPrefs.GetInt("打听父母");
    public int 王二 = PlayerPrefs.GetInt("王二");
    public int 牛铁匠 = PlayerPrefs.GetInt("牛铁匠");
    public int 取回遗物 = PlayerPrefs.GetInt("取回遗物");

}
