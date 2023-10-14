using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdressMgr : BaseManager<AdressMgr>
{
   public static Dictionary<string,int> 地图表=加载地图表();

    public static Dictionary<string, int> 加载地图表() {
        Dictionary<string, int> 地图表 = new Dictionary<string, int>();
        地图表.Add("木屋(家)",0);
        地图表.Add("桃源村",0);
        地图表.Add("村口小道",0);
        地图表.Add("小树林",5);
        地图表.Add("村口东", 15);
        地图表.Add("山丘桃林", 30);
        地图表.Add("十字坡", 50);
        地图表.Add("乱石滩", 200);
        地图表.Add("荒原", 70);
        地图表.Add("噩梦之森", 100);
        地图表.Add("黄沙地", 120);
        地图表.Add("临海官道", 150);
        地图表.Add("临海城", 0);
        地图表.Add("临海城东", 0);
        地图表.Add("临海城西", 0);
        地图表.Add("临海西郊", 260);
        地图表.Add("乱葬岗", 450);
        地图表.Add("断崖", 0);

        return 地图表;
    }




    public bool 检测战斗力是否达标(string 地名) {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        int 主角战斗力= gut.返回主角战斗力_无单位();
        int 最低进入战斗力 = 0;
        if (地图表.ContainsKey(地名)) {
            最低进入战斗力 = 地图表[地名];
        }

        if (主角战斗力 < 最低进入战斗力) {
            return false;
        }

        return true;
    }
}
