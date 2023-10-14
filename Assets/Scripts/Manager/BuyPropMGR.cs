using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPropMGR : BaseManager<BuyPropMGR>
{
    public static Dictionary<string, Dictionary<string, string>> 铜币商城购买表 = 加载铜币商城();
    public static Dictionary<string, Dictionary<string, string>> 金币商城购买表 = 加载金币商城();
    public static Dictionary<string, Dictionary<string, string>> 仙晶商城购买表 = 加载仙晶商城();
    public static Dictionary<string, Dictionary<string, string>> 黑钻商城购买表 = 加载黑钻商城();
    public Dictionary<string, Dictionary<string, string>> 随机购买表 = new Dictionary<string, Dictionary<string, string>>();


    public static Dictionary<string, Dictionary<string, string>> 加载铜币商城() {
        Dictionary<string, Dictionary<string, string>> 购买表 = new Dictionary<string, Dictionary<string, string>>();
        //有且只绑定一次-铜币
        购买表.Add("小还丹", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "5000" }, { "数量", "无限" } });
        购买表.Add("大还丹", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "50000" }, { "数量", "无限" } });
        购买表.Add("血滴子", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "5000" }, { "数量", "无限" } });
        购买表.Add("雷震子", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "50000" }, { "数量", "无限" } });
        购买表.Add("兽皮", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "5000" }, { "数量", "无限" } });
        购买表.Add("兽骨", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "5000" }, { "数量", "无限" } });
        购买表.Add("狗粮", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "500" }, { "数量", "无限" } });
        购买表.Add("美味大骨", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "2000" }, { "数量", "无限" } });
        购买表.Add("宠爱零食", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "5000" }, { "数量", "无限" } });
        购买表.Add("神圣果子", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "30000" }, { "数量", "无限" } });
        购买表.Add("人物成长礼包", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "10000"}, { "数量", "无限" } });
        购买表.Add("宠物成长礼包", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "10000" }, { "数量", "无限" } });
        购买表.Add("仙缘礼包", new Dictionary<string, string>() {{"货币","铜币"}, { "价格", "20000" }, { "数量", "无限" } });
        return 购买表;
    }


    public static Dictionary<string, Dictionary<string, string>> 加载金币商城()
    {
        Dictionary<string, Dictionary<string, string>> 购买表 = new Dictionary<string, Dictionary<string, string>>();
        //有且只绑定一次-金币
        购买表.Add("1000仙晶卡", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "5" }, { "数量", "1" } });
        购买表.Add("归元露礼包(小)", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "2" }, { "数量", "无限" } });
        购买表.Add("生命之果", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "2" }, { "数量", "无限" } });
        购买表.Add("通天塔礼包", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "1" }, { "数量", "无限" } });
        购买表.Add("神圣果子", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "1" }, { "数量", "无限" } });
        购买表.Add("灵芝", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "1" }, { "数量", "无限" } });
        购买表.Add("进化宝石", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "1" }, { "数量", "无限" } });
        购买表.Add("仙兽气息", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "5"}, { "数量", "无限" } });
        购买表.Add("神兽气息", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "10" }, { "数量", "无限" } });
        购买表.Add("通天套装礼盒", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "200" }, { "数量", "1" } });
        购买表.Add("(心法)<通天秘典>", new Dictionary<string, string>() {{"货币","金币"}, { "价格", "500" }, { "数量", "无限" } });
        return 购买表;
    }

    public static Dictionary<string, Dictionary<string, string>> 加载仙晶商城()
    {
        Dictionary<string, Dictionary<string, string>> 购买表 = new Dictionary<string, Dictionary<string, string>>();
        //有且只绑定一次-仙晶
        购买表.Add("拜师贴", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "500" }, { "数量", "无限" } });
        购买表.Add("技能重置券", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "100" }, { "数量", "无限" } });
        购买表.Add("归元露", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "50" }, { "数量", "无限" } });
        购买表.Add("仙兽气息", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "399" }, { "数量", "无限" } });
        购买表.Add("神兽气息", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "999" }, { "数量", "无限" } });
        购买表.Add("人物成长礼包", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "50" }, { "数量", "无限" } });
        购买表.Add("宠物成长礼包", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "50" }, { "数量", "无限" } });
        购买表.Add("仙缘礼包", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "100" }, { "数量", "无限" } });
        购买表.Add("人物技能书礼盒", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "300" }, { "数量", "无限" } });
        购买表.Add("青龙-进化箱", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "500" }, { "数量", "10" } });
        购买表.Add("白虎-进化箱", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "500" }, { "数量", "10" } });
        购买表.Add("朱雀-进化箱", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "500" }, { "数量", "10" } });
        购买表.Add("玄武-进化箱", new Dictionary<string, string>() {{"货币","仙晶"}, { "价格", "500" }, { "数量", "10" } });
        return 购买表;
    }


    public static Dictionary<string, Dictionary<string, string>> 加载黑钻商城()
    {
        Dictionary<string, Dictionary<string, string>> 购买表 = new Dictionary<string, Dictionary<string, string>>();
        //有且只绑定一次-仙晶
        购买表.Add("小蛇之卵", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "50" }, { "数量", "1" } });
        购买表.Add("鼠鼠之卵", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "50" }, { "数量", "1" } });
        购买表.Add("小鸡之卵", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "50" }, { "数量", "1" } });
        购买表.Add("龟龟之卵", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "50" }, { "数量", "1" } });
        购买表.Add("进化宝石", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "1" }, { "数量", "无限" } });
        购买表.Add("绿色精华", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "1" }, { "数量", "无限" } });
        购买表.Add("蓝色精华", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "2" }, { "数量", "无限" } });
        购买表.Add("紫色精华", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "5" }, { "数量", "无限" } });
        购买表.Add("兽皮礼包(小)", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "2" }, { "数量", "无限" } });
        购买表.Add("兽骨礼包(小)", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "2" }, { "数量", "无限" } });
        购买表.Add("至尊礼包", new Dictionary<string, string>() {{"货币","黑钻"}, { "价格", "50" }, { "数量", "1" } });
        return 购买表;
    }


}
