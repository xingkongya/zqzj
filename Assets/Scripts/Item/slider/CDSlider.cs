using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CDSlider : MonoBehaviour
{
    private io io_;
    private PropMgr pm;
    private Slider cdSlider;
    private SkillApplicator sa;
    public SkillData sd;
    public Prop_bascis pb;
    private G_Util gut;
    private basicMgr bm;
    private string 标记="";

    private void Awake()
    {
        bm = basicMgr.GetInstance();
        io_ = io.GetInstance();
        pm = PropMgr.GetInstance();
        sa = SkillApplicator.GetInstance();
        gut = NameMgr.画布.GetComponent<G_Util>();
        cdSlider = gameObject.GetComponent<Slider>();

    }

    // Start is called before the first frame update
    void Start()
    {
        初始化拉条数据();
    }



    public void 初始化拉条数据() {
        role_Data myData = io_.load();

        string type = gameObject.transform.parent.parent.name;
        if (type.Equals("绝招"))
        {
            //加载绝招UI
            if (myData.技能槽.ContainsKey("1") && !myData.技能槽["1"].Equals(""))
            {
                string 绝招名字 = myData.技能槽["1"];
                sd = pm.检索技能(绝招名字);
                cdSlider.maxValue = bm.Xstoi(sd.cd);
            }
            标记 = "绝招";
        }
        else if (type.Equals("道具")) {
            if (myData.记录.ContainsKey("CD道具") && !myData.记录["CD道具"].Equals("")) {
                string 道具名字 = myData.记录["CD道具"];
                pb = pm.检索物品(道具名字);
                cdSlider.maxValue = bm.Xstoi(pb.cd);
            }
            标记 = "CD道具";

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (标记.Equals("绝招"))
        {
            if (sd != null)
            {
                if (sa.CD集合.ContainsKey("绝招"))
                    cdSlider.value = sa.CD集合["绝招"];
                else
                    cdSlider.value = 0;
            }
        }
        else if (标记.Equals("CD道具")) {
            if (pb != null)
            {
                if (sa.CD集合.ContainsKey("CD道具"))
                    cdSlider.value = sa.CD集合["CD道具"];
                else
                    cdSlider.value = 0;
            }
        }
    }


}
