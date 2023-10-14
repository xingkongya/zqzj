using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilMaGr : BaseManager<UtilMaGr>
{
    private basicMgr bm=basicMgr.GetInstance();
    private PetMgr pem = PetMgr.GetInstance();

    public Dictionary<string,string> 加载光环属性() {
        Dictionary<string, string> 属性集合 = new Dictionary<string, string>();
        role_Data myData = io.GetInstance().load();
        if (myData.拓展.ContainsKey("光环模块")) {
            int 修炼光环等级 =bm.Xstoi( myData.拓展["光环模块"]["修炼光环"]);
            int 无敌光环等级 =bm.Xstoi( myData.拓展["光环模块"]["无敌光环"]);
            int 奇遇光环等级 =bm.Xstoi( myData.拓展["光环模块"]["奇遇光环"]);
            int 幸运光环等级 =bm.Xstoi( myData.拓展["光环模块"]["幸运光环"]);
            属性集合.Add("经验加成", bm.Xitos(30 * 修炼光环等级) );
            属性集合.Add("攻击力加成", bm.Xitos(10 * 无敌光环等级));
            属性集合.Add("防御力加成", bm.Xitos(10 * 无敌光环等级));
            属性集合.Add("血量加成", bm.Xitos(10 * 无敌光环等级));
            属性集合.Add("稀有怪概率", bm.Xitos(10 * 奇遇光环等级));
            属性集合.Add("爆率", bm.Xitos(10 * 幸运光环等级));
        }
        return 属性集合;
    }


    public AllBasic 实体类加额外属性(AllBasic 对象,string 属性名,string 属性值) {
        if (!对象.额外拓展.ContainsKey(属性名)) {
            对象.额外拓展.Add(属性名, 属性值);
        }
        return 对象;
    
    }

    public AllBasic 实体类加额外树形属性(AllBasic 对象, string 属性名, Dictionary<string,string> 属性值)
    {
        if (!对象.额外树形拓展.ContainsKey(属性名))
        {
            对象.额外树形拓展.Add(属性名, 属性值);
        }
        return 对象;

    }

    public void 存档检测(role_Data 存档)
    {
        存档= 更新存档(存档);
        io.GetInstance().save(存档);
    }


    public role_Data 更新存档(role_Data 存档) {
        存档=职业更新(存档);
        存档=宠物更新(存档);
        存档 = 被动技能更新(存档);
        return 存档;
    }

    public role_Data 职业更新(role_Data 存档) {
        if (存档.职业 == null || 存档.职业.Count != 11)
        {
            存档.职业 = new Dictionary<string, string>();
            存档.职业.Add("职业", "");
            存档.职业.Add("1技能", "");
            存档.职业.Add("2技能", "");
            存档.职业.Add("3技能", "");
            存档.职业.Add("4技能", "");
            存档.职业.Add("5技能", "");
            存档.职业.Add("6技能", "");
            存档.职业.Add("7技能", "");
            存档.职业.Add("8技能", "");
            存档.职业.Add("基础技能点", "");
            存档.职业.Add("高级技能点", "");
        }
        return 存档;
    }

    private role_Data 被动技能更新(role_Data 存档) {
        if (存档.被动技能 ==null||!存档.被动技能.ContainsKey("职业") ) {
            存档.被动技能 = new Dictionary<string, List<string>>();
            存档.被动技能.Add("职业", new List<string>());
        }
        return 存档;
    }

    private role_Data 宠物更新(role_Data 存档) {
        for (int i=0;i< 存档.宠物栏.Count;i++) {
            pem.宠物信息更新(存档.宠物栏[i]);
        }
        return 存档;
    }

   

}
