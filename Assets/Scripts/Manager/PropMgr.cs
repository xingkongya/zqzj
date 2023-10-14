using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class PropMgr : BaseManager<PropMgr>
{

    public static Dictionary<string, Equipment> 装备表 = 解析装备表();
    public static Dictionary<string, Prop_bascis> 材料表 = 解析材料表();
    public static Dictionary<string, suit_Data> 套装表 = 解析套装表();
    public static Dictionary<string, SkillData> 技能表 = 解析技能表();
    public static Dictionary<string, Pet_Data> 宠物表 = 解析宠物表();
    public static Dictionary<string, TianFu> 天赋表 = 解析天赋表();
    public Dictionary<string, int> 装备套装信息;
    public role_Data myData;
    private basicMgr bm = basicMgr.GetInstance();
    private ChatMgr cm = ChatMgr.GetInstance();
    private io io_ = io.GetInstance();
    public Dictionary<string, string> 主属性表 = new Dictionary<string, string>() { { "武器", "攻击力" }, { "衣服", "防御力" }, { "头部", "血量" }, { "下装", "防御力" }, { "饰品", "回血值" }, { "左卡", "攻击力" }, { "右卡", "防御力" } };
    public Dictionary<string, string> 颜色增幅 = new Dictionary<string, string>() { { "0", "1" }, { "1", "1.3" }, { "2", "1.8" }, { "3", "2.5" }, { "4", "3.5" }, { "5", "5" } };
    public Dictionary<string, string> 强化增幅 = new Dictionary<string, string>() { { "0", "0" }, { "1", "10" }, { "2", "20" }, { "3", "30" }, { "4", "45" }, { "5", "60" }, { "6", "80" }, { "7", "100" }, { "8", "120" }, { "9", "150" }, { "10", "180" }, { "11", "210" }, { "12", "240" }, { "13", "290" }, { "14", "340" }, { "15", "400" } };
    public Dictionary<string, string> 颜色等级上限 = new Dictionary<string, string>() { { "0", "3" }, { "1", "5" }, { "2", "8" }, { "3", "10" }, { "4", "15" }, { "5", "15" } };
    Dictionary<string, string> 低级额外属性 = new Dictionary<string, string>() { { "攻击力", "0.5" }, { "防御力", "0.25" }, { "血量", "10" }, { "回血值", "0.5" }, { "固定伤害", "0.4" }, { "固定减伤", "0.4" }, { "固定吸血", "0.1" } };
    Dictionary<string, string> 高级额外属性 = new Dictionary<string, string>() { { "攻击力", "1" }, { "防御力", "0.5" }, { "血量", "20" }, { "回血值", "1" }, { "固定伤害", "0.8" }, { "固定减伤", "0.8" }, { "固定吸血", "0.2" }, { "伤害加成", "2" }, { "伤害减免", "2" }, { "吸血加成", "0.5" }, { "暴击率", "2" }, { "暴击伤害", "8" }, { "攻击加成", "2" }, { "防御加成", "2" }, { "血量加成", "2" }, { "回血值加成", "2" }, { "攻击速度", "4" } };
    List<string> 低级属性 = new List<string>() { { "攻击力" },{ "防御力"}, { "血量" }, { "回血值" }, { "固定伤害" }, { "固定减伤" }, { "固定吸血" } };
    Dictionary<string, string> 五十级比例 = new Dictionary<string, string>() { { "攻击力", "1" }, { "防御力", "0.5" }, { "血量", "15" }, { "回血值", "1" } };
    Dictionary<string, string> 一百级比例 = new Dictionary<string, string>() { { "攻击力", "3" }, { "防御力", "2" }, { "血量", "50" }, { "回血值", "2" } };
    public Dictionary<string, Dictionary<string, string>> 物品合成表 = new Dictionary<string, Dictionary<string, string>>() { 
        {"灰色精华",new Dictionary<string, string>() { {"合成获得","绿色精华" }, { "合成需求", "灰色精华" },{ "需求数量","5" },{"额外需求","铜币" }, { "额外数量", "100" } } },
         {"绿色精华",new Dictionary<string, string>() { {"合成获得","蓝色精华" }, { "合成需求", "绿色精华" },{ "需求数量","5" },{"额外需求","铜币" }, { "额外数量", "300" } } },
     {"蓝色精华",new Dictionary<string, string>() { {"合成获得","紫色精华" }, { "合成需求", "蓝色精华" },{ "需求数量","5" },{"额外需求","铜币" }, { "额外数量", "500" } } },
     {"紫色精华",new Dictionary<string, string>() { {"合成获得","仙兽精华" }, { "合成需求", "紫色精华" },{ "需求数量","5" },{"额外需求","仙兽气息" }, { "额外数量", "1" } } },
     {"仙兽精华",new Dictionary<string, string>() { {"合成获得","神兽精华" }, { "合成需求", "仙兽精华" },{ "需求数量","5" },{"额外需求","神兽精华" }, { "额外数量", "1" } }  },
      {"灰色晶矿",new Dictionary<string, string>() { {"合成获得","绿色晶矿" }, { "合成需求", "灰色晶矿" },{ "需求数量","5" },{"额外需求","铜币" }, { "额外数量", "10" } } },
         {"绿色晶矿",new Dictionary<string, string>() { {"合成获得","蓝色晶矿" }, { "合成需求", "绿色晶矿" },{ "需求数量","5" },{"额外需求","铜币" }, { "额外数量", "20" } } },
     {"蓝色晶矿",new Dictionary<string, string>() { {"合成获得","紫色晶矿" }, { "合成需求", "蓝色晶矿" },{ "需求数量","5" },{"额外需求","铜币" }, { "额外数量", "50" } } },
     {"紫色晶矿",new Dictionary<string, string>() { {"合成获得", "橙色晶矿" }, { "合成需求", "紫色晶矿" },{ "需求数量","5" },{"额外需求","铜币" }, { "额外数量", "100" } } },
     {"橙色晶矿",new Dictionary<string, string>() { {"合成获得","红色晶矿" }, { "合成需求", "橙色晶矿" },{ "需求数量","5" },{"额外需求","铜币" }, { "额外数量", "500" } }  }};


    public static Dictionary<string, Equipment> 解析装备表()
    {
        //返回的格式 -- 字典<键:装备名字. 值:装备类>
        Dictionary<string, Equipment> 装备表 = new Dictionary<string, Equipment>();

        string name = "Data/装备表";


        try
        {
            Equipment[] EqData = null;
            //string str = basicMgr.GetInstance().LoadFile(name);//sm文件为明文,容易被破解
            string str = Resources.Load<TextAsset>(name).text;
            EqData = JsonMapper.ToObject<Equipment[]>(str);
            Equipment 加密装备;
            for (int i = 0; i < EqData.Length; i++)
            {
                加密装备 = basicMgr.GetInstance().装备加密(EqData[i]);
                装备表.Add(EqData[i].name, 加密装备);
            }
        }
        //读取异常
        catch (Exception e)
        {
            Debug.Log("读取失败!" + e);
            return null;
        }
        return 装备表;
    }

    public static Dictionary<string, Prop_bascis> 解析材料表()
    {
        //返回的格式 -- 字典<键:材料名字. 值:道具基类>
        Dictionary<string, Prop_bascis> 材料表 = new Dictionary<string, Prop_bascis>();

        string name = "Data/材料表";

        try
        {
            Prop_bascis[] mtrData = null;
            //string str = basicMgr.GetInstance().LoadFile(name);//sm文件为明文,容易被破解
            string str = Resources.Load<TextAsset>(name).text;
            mtrData = JsonMapper.ToObject<Prop_bascis[]>(str);
            Prop_bascis 加密道具;
            for (int i = 0; i < mtrData.Length; i++)
            {
                加密道具 = basicMgr.GetInstance().道具加密(mtrData[i]);
                材料表.Add(mtrData[i].name, 加密道具);
            }
        }
        //读取异常
        catch (Exception e)
        {
            Debug.Log("读取失败!" + e);
            return null;
        }
        return 材料表;
    }


    public static Dictionary<string, suit_Data> 解析套装表()
    {
        //返回的格式 -- 字典<键:套装名字. 值:套装类>
        Dictionary<string, suit_Data> 套装表 = new Dictionary<string, suit_Data>();

        string name = "Data/套装表";


        try
        {
            suit_Data[] suData = null;
            //string str = basicMgr.GetInstance().LoadFile(name);//sm文件为明文,容易被破解
            string str = Resources.Load<TextAsset>(name).text;
            suData = JsonMapper.ToObject<suit_Data[]>(str);
            for (int i = 0; i < suData.Length; i++)
            {
                套装表.Add(suData[i].s_name, suData[i]);
            }
        }
        //读取异常
        catch (Exception e)
        {
            Debug.Log("读取失败!" + e);
            return null;
        }
        return 套装表;
    }

    public static Dictionary<string, SkillData> 解析技能表()
    {
        //返回的格式 -- 字典<键:套装名字. 值:技能类>
        Dictionary<string, SkillData> 技能表 = new Dictionary<string, SkillData>();

        string name = "Data/技能表";


        try
        {
            SkillData[] skData = null;
            //string str = basicMgr.GetInstance().LoadFile(name);//sm文件为明文,容易被破解
            string str = Resources.Load<TextAsset>(name).text;
            skData = JsonMapper.ToObject<SkillData[]>(str);
            SkillData 加密技能;
            for (int i = 0; i < skData.Length; i++)
            {
                加密技能 = basicMgr.GetInstance().技能加密(skData[i]);
                技能表.Add(skData[i].name, 加密技能);
            }
        }
        //读取异常
        catch (Exception e)
        {
            Debug.Log("读取失败!" + e);
            return null;
        }
        return 技能表;
    }


    public static Dictionary<string, Pet_Data> 解析宠物表()
    {
        //返回的格式 -- 字典<键:宠物名字. 值:宠物类>
        Dictionary<string, Pet_Data> 宠物表 = new Dictionary<string, Pet_Data>();

        string name = "Data/宠物表";


        try
        {
            Pet_Data[] petData = null;
            //string str = basicMgr.GetInstance().LoadFile(name);//sm文件为明文,容易被破解
            string str = Resources.Load<TextAsset>(name).text;
            petData = JsonMapper.ToObject<Pet_Data[]>(str);
            Pet_Data 加密宠物;
            for (int i = 0; i < petData.Length; i++)
            {
                加密宠物 = basicMgr.GetInstance().宠物加密(petData[i]);
                宠物表.Add(petData[i].name, 加密宠物);
            }
        }
        //读取异常
        catch (Exception e)
        {
            Debug.Log("读取失败!" + e);
            return null;
        }
        return 宠物表;
    }

    public static Dictionary<string, TianFu> 解析天赋表()
    {
        //返回的格式 -- 字典<键:宠物名字. 值:宠物类>
        Dictionary<string, TianFu> 天赋表 = new Dictionary<string, TianFu>();

        string name = "Data/天赋表";


        try
        {
            TianFu[] TianFuArray = null;
            //string str = basicMgr.GetInstance().LoadFile(name);//sm文件为明文,容易被破解
            string str = Resources.Load<TextAsset>(name).text;
            TianFuArray = JsonMapper.ToObject<TianFu[]>(str);
            for (int i = 0; i < TianFuArray.Length; i++)
            {
                天赋表.Add(TianFuArray[i].name, TianFuArray[i]);
            }
        }
        //读取异常
        catch (Exception e)
        {
            Debug.Log("读取失败!" + e);
            return null;
        }
        return 天赋表;
    }


    public Prop_bascis 检索物品(string name)
    {
        if (材料表.ContainsKey(name))
            return 材料表[name];
        else if (装备表.ContainsKey(name))
        {
            return 装备特别初始化_打造( 装备表[name]);
        }
        else
            return new Prop_bascis();
    }

    public Equipment 检索装备(string name,int 最低等级,int 最高等级)
    {

       if (装备表.ContainsKey(name))
        {
            return 装备特别初始化_等级区间(装备表[name], 最低等级, 最高等级);
        }
        else
            return new Equipment();
    }

    public Equipment 检索装备_最低品质(string name, int 最低等级, int 最高等级)
    {

        if (装备表.ContainsKey(name))
        {
            return 装备特别初始化_等级区间_最低品质(装备表[name], 最低等级, 最高等级);
        }
        else
            return new Equipment();
    }

    public Equipment 装备初始化(Equipment 装备)
    {

        if (装备表.ContainsKey(装备.name))
        {
            return 装备表[装备.name];
        }
        else
            return new Equipment();
    }

    public string 返回装备初始最低品质(Equipment 装备)
    {

        if (装备表.ContainsKey(装备.name))
        {
            return 装备表[装备.name].qua;
        }
        else
            return "";
    }

    public Equipment 检索装备(string name, int 最低等级, int 最高等级,int qua)
    {

        if (装备表.ContainsKey(name))
        {
            return 装备特别初始化_等级区间(装备表[name], 最低等级, 最高等级,qua);
        }
        else
            return new Equipment();
    }




    public SkillData 检索技能(string name)
    {
        if (技能表.ContainsKey(name))
            return 技能表[name];
        else
            return new SkillData();
    }



    /// <summary>
    /// 获取单一物品方法,掉落结算再roleMar里
    /// </summary>
    public void 获取物品(string 名字, int 数量)
    {
        io io_ = io.GetInstance();
        Prop_bascis 物品 = 检索物品(名字);
        role_Data myData = io_.load();
        if (物品.type.Equals("1") || 物品.type.Equals("2"))//加入材料背包
        {
            //判断
            if (myData.材料背包.ContainsKey(名字))
                myData.材料背包[名字].num = bm.Xitos(bm.Xstoi(myData.材料背包[名字].num) + 数量);
            else
            {
                myData.材料背包.Add(名字, 物品);
                myData.材料背包[名字].num = bm.Xor(数量 + "");
            }
        }
        else//加入装备背包 
        {
            //判断
            /*if (myData.装备背包.ContainsKey(名字))
                myData.装备背包[名字].num = bm.Xitos(bm.Xstoi(myData.装备背包[名字].num) + 数量);
            else
            {
                myData.装备背包.Add(名字, (Equipment)物品);
                myData.装备背包[名字].num = bm.Xor(数量 + "");
            }
            io_.save(myData);*/
            myData.装备背包.Add(bm.获取UID(), 装备随机初始化(物品));
        }
        io_.save(myData);
    }

    public void 获取物品(Equipment 装备)
    {
        io io_ = io.GetInstance();
        role_Data myData = io_.load();
        myData.装备背包.Add(bm.获取UID(), 装备);
        io_.save(myData);
    }


    public string 获取特定装备(string 装备名,int 等级,int 品质)
    {
        io io_ = io.GetInstance();
        role_Data myData = io_.load();
        Equipment 装备 = 检索物品(装备名) as Equipment;
        装备 = 装备特别初始化(装备, 等级, 品质);
        string UID = bm.获取UID();
        myData.装备背包.Add(UID, 装备);
        io_.save(myData);
        return UID;
    }

    public Equipment 装备随机初始化(Prop_bascis 物品) {
        
        Equipment 装备 = (Equipment)物品;
        //初始化装备等级
        装备.lessgrade = bm.Xitos( UnityEngine.Random.Range(bm.Xstoi(装备.lessgrade), bm.Xstoi(装备.max_grade) + 1));
        //初始化品质
        装备 = 递归返回品质_开始(装备);
        //初始化主属性值
        装备 = 装备初始化主属性值(装备);
        //初始化额外属性
        装备 = 装备刷新额外属性(装备);

        return 装备;
    }

    public Equipment 装备特别初始化_打造(Prop_bascis 物品)
    {
        //
        Equipment 装备 = (Equipment)物品;
        //等级为最小级
        装备.lessgrade = 装备.min_grade;
        //品质为最低
        装备.qua = 装备.min_qua;
        //初始化主属性值
        装备 = 装备初始化主属性值(装备);
        //初始化额外属性
        装备 = 装备刷新额外属性(装备);

        return 装备;
    }


    public Equipment 装备特别初始化(Prop_bascis 物品, int 等级)
    {
        //
        Equipment 装备 = (Equipment)物品;
        //初始化装备等级
        装备.lessgrade = bm.Xitos(等级);
        //初始化品质
        装备 = 递归返回品质_开始(装备);
        //初始化主属性值
        装备 = 装备初始化主属性值(装备);
        //初始化额外属性
        装备 = 装备刷新额外属性(装备);

        return 装备;
    }

    public Equipment 装备特别初始化_等级不变其他刷新(Equipment 物品)
    {
        //
        Equipment 装备 = (Equipment)物品;
        //初始化品质
        装备 = 递归返回品质_开始(装备);
        //初始化主属性值
        装备 = 装备初始化主属性值(装备);
        //初始化额外属性
        装备 = 装备刷新额外属性(装备);

        return 装备;
    }


    public Equipment 装备特别初始化(Prop_bascis 物品,int 等级,int qua)
    {
        //
        Equipment 装备 = (Equipment)物品;
        //初始化装备等级
        装备.lessgrade = bm.Xitos(等级);
        //初始化品质
        装备.qua = bm.Xitos(qua);
        //初始化主属性值
        装备 = 装备初始化主属性值(装备);
        //初始化额外属性
        装备 = 装备刷新额外属性(装备);

        return 装备;
    }

    private Equipment 装备特别初始化_等级区间(Prop_bascis 物品, int 等级, int 最高等级)
    {
        
        Equipment 装备 = (Equipment)物品;
        //初始化装备等级
        装备.lessgrade = bm.Xitos( UnityEngine.Random.Range(等级, 最高等级 + 1));
        //初始化品质
        装备 = 递归返回品质_开始(装备);
        //初始化主属性值
        装备 = 装备初始化主属性值(装备);
        //初始化额外属性
        装备 = 装备刷新额外属性(装备);

        return 装备;
    }

    private Equipment 装备特别初始化_等级区间_最低品质(Prop_bascis 物品, int 等级, int 最高等级)
    {
        
        Equipment 装备 = (Equipment)物品;

        //初始化装备等级
        装备.lessgrade = bm.Xitos( UnityEngine.Random.Range(等级, 最高等级 + 1));
        //最低品质
        装备.qua = 装备.min_qua;
        //初始化主属性值
        装备 = 装备初始化主属性值(装备);
        //初始化额外属性
        装备 = 装备刷新额外属性(装备);

        return 装备;
    }

    private Equipment 装备特别初始化_等级区间(Prop_bascis 物品, int 等级, int 最高等级,int qua)
    {
        
        Equipment 装备 = (Equipment)物品;
        //初始化装备等级
        装备.lessgrade = bm.Xitos( UnityEngine.Random.Range(等级, 最高等级 + 1));
        //初始化品质
        装备.qua = bm.Xitos(qua);
        //初始化主属性值
        装备 = 装备初始化主属性值(装备);
        //初始化额外属性
        装备 = 装备刷新额外属性(装备);

        return 装备;
    }

    public Equipment 装备初始化主属性值(Equipment 装备)
    {     
        装备.head_attribute_num = bm.Xitos(返回装备主属性(装备));
        return 装备;
    }


    public int 返回装备主属性(Equipment 装备) {
        int num;
        if (bm.Xstoi(装备.lessgrade) <= 50)
        {
             num = (int)(bm.Xstoi(装备.lessgrade) * float.Parse(五十级比例[主属性表[装备.place]]) * float.Parse(颜色增幅[bm.Xstoi(装备.qua) + ""]) * (1 + float.Parse(强化增幅[bm.Xstoi(装备.lv) + ""]) / 100f));
        }
        else
        {
             num = (int)(bm.Xstoi(装备.lessgrade) * float.Parse(五十级比例[主属性表[装备.place]]) * float.Parse(颜色增幅[bm.Xstoi(装备.qua) + ""]) * (1 + float.Parse(强化增幅[bm.Xstoi(装备.lv) + ""]) / 100f));
        }
        if (num == 0)
            num = 1;
        return num;
    }

    public int 返回装备强化主属性(Equipment 装备)
    {
        int num;
        if (bm.Xstoi(装备.lessgrade) <= 50)
        {
            num = (int)(bm.Xstoi(装备.lessgrade) * float.Parse(五十级比例[主属性表[装备.place]]) * float.Parse(颜色增幅[bm.Xstoi(装备.qua) + ""]) * (1 + float.Parse(强化增幅[bm.Xstoi(装备.lv)+1 + ""]) / 100f));
        }
        else
        {
            num = (int)(bm.Xstoi(装备.lessgrade) * float.Parse(五十级比例[主属性表[装备.place]]) * float.Parse(颜色增幅[bm.Xstoi(装备.qua) + ""]) * (1 + float.Parse(强化增幅[bm.Xstoi(装备.lv)+1 + ""]) / 100f));
        }
        if (num == 0)
            num = 1;
        return num;
    }



    public Equipment 装备刷新额外属性(Equipment 装备) {
        装备.extar_attribute = new Dictionary<string, string>();
        if (bm.Xstoi(装备.qua) >= 1)
        {
            int 词条数 = bm.Xstoi(装备.qua) > 3 ? 3 : bm.Xstoi(装备.qua);//三目省代码
            for (int i = 0; i < 词条数; i++)
            {
                if (bm.Xstoi(装备.qua) <= 3)
                {
                    装备 = 装备随机添加词条(低级额外属性, 装备);
                }
                else
                {
                    装备 = 装备随机添加词条(高级额外属性, 装备);
                }
            }
        }

       return 装备;
    }


    public Equipment 装备随机添加词条(Dictionary<string, string> 字典,Equipment 装备) {
        
        int 随机值 =  UnityEngine.Random.Range(1, 字典.Count + 1);
        int index = 0;
            foreach (string 键 in 字典.Keys)
            {
                index++;
                if (随机值 == index)
                {
                    if (装备.extar_attribute.ContainsKey(键))
                    {
                        return 装备随机添加词条(字典,装备);
                    }
                    else
                    {
                        if (!低级属性.Contains(键))
                        {
                            int 值 = (int)(float.Parse(字典[键]) * float.Parse(颜色增幅[bm.Xstoi(装备.qua) + ""])* 0.75f);
                            装备.extar_attribute.Add(键, bm.Xftos(值<1?1:值));
                        }
                        else
                        {
                            int 值 = (int)(float.Parse(字典[键]) * float.Parse(颜色增幅[bm.Xstoi(装备.qua) + ""]) * bm.Xstoi(装备.lessgrade) * 0.75f);
                            装备.extar_attribute.Add(键, bm.Xftos(值<1?1:值));
                        }
                    }
                }
            }
        return 装备;
    }


    public Equipment 递归返回品质_开始(Equipment 装备) {
        装备.qua = 装备.min_qua;
        装备 = 递归返回品质(装备);
        return 装备;
    }


    private Equipment 递归返回品质(Equipment 装备) {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (bm.Xstoi(装备.qua) < bm.Xstoi(装备.max_qua))
        {
            if (gut.概率(6, 10))
            {
                return 装备;
            }
            else
            {
                装备.qua = bm.Xitos(bm.Xstoi(装备.qua) + 1);
                return 递归返回品质(装备);
            }
        }
        else {
            return 装备;
        }
    }


    public string 递归返回品质_返回加密字符串_开始(Equipment 装备)
    {
        装备.qua = 装备.min_qua;
        return 递归返回品质_返回加密字符串(装备);
    }

    private string 递归返回品质_返回加密字符串(Equipment 装备)
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        if (bm.Xstoi(装备.qua) < bm.Xstoi(装备.max_qua))
        {
            if (gut.概率(6, 10))
            {
                return 装备.qua;
            }
            else
            {
                装备.qua = bm.Xitos(bm.Xstoi(装备.qua) + 1);
                 return 递归返回品质_返回加密字符串(装备);
            }
        }
        else
        {
            return 装备.qua;
        }
    }

    public void 获取多个物品(Dictionary<Prop_bascis, int> 物品列表)
    {
        io io_ = io.GetInstance();
        role_Data myData = io_.load();
        foreach (Prop_bascis 物品 in 物品列表.Keys)
        {
            if (物品 == null|| 物品.name==null||物品.name.Equals("")) {
                continue;
            }
            if (物品.type.Equals("1") || 物品.type.Equals("2"))//加入材料背包
            {
                //判断
                if (myData.材料背包.ContainsKey(物品.name))
                    myData.材料背包[物品.name].num = bm.Xitos(bm.Xstoi(myData.材料背包[物品.name].num) + 物品列表[物品]);
                else
                {
                    myData.材料背包.Add(物品.name, 物品);
                    myData.材料背包[物品.name].num = bm.Xor(物品列表[物品] + "");
                }
            }
            else//加入装备背包 
            {
                //判断
                /*if (myData.装备背包.ContainsKey(物品.name))
                    myData.装备背包[物品.name].num = bm.Xitos(bm.Xstoi(myData.装备背包[物品.name].num) + 物品列表[物品]);
                else
                {
                    myData.装备背包.Add(物品.name, (Equipment)物品);
                    myData.装备背包[物品.name].num = bm.Xor(物品列表[物品] + "");
                }*/
                Equipment 装备 = (Equipment)物品;
                myData.装备背包.Add(bm.获取UID(), 装备);
            }
        }
        io_.save(myData);
        物品列表.Clear();
    }


    public void 获取缓存经验金钱(Dictionary<string, int> 经验金钱)
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        //这个集合去除经验
        if (经验金钱.ContainsKey("经验"))//经验值
        {
            gut.加经验值(经验金钱["经验"]);
            经验金钱.Remove("经验");
        }
        //经验去除了经验,可以直接用加金钱方法
        gut.加金钱(经验金钱);
        经验金钱.Clear();
    }


    public bool 获取多个物品并显示特效(Dictionary<Prop_bascis, int> 物品列表, string type)
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        List<Prop_bascis> 列表 = new List<Prop_bascis>();

        //效果
        foreach (Prop_bascis 物品 in 物品列表.Keys)
        {
            //获取物品检测,如果物品不存在则刹车
            if (物品 == null || 物品.name == null || 物品.name.Equals("")) {
                return false;
            }
            列表.Add(物品);
            cm.系统播报(物品, 物品.name, 物品列表[物品], "获得道具");
        }
        if (type.Equals("背包"))
            gut.生成获得提示(列表, "背包");
        else
            gut.生成获得提示(列表, "掉落");

        获取多个物品(物品列表);//数据
        return true;
    }


    public int 返回背包该物品的数量(string 物品名)
    {
        io io_ = io.GetInstance();
        role_Data myData = io_.load();
        Prop_bascis 物品 = 检索物品(物品名);
        if (物品 == null) {
            Debug.Log("没有写该物品!");
            return 0;
        }
        if (!PropMgr.材料表.ContainsKey(物品名))
        {
            /* if (!myData.装备背包.ContainsKey(物品名))
                 return 0;
             else
                 return bm.Xstoi(myData.装备背包[物品名].num);*/
            foreach (string key in myData.装备背包.Keys)
            {
                if (myData.装备背包[key].name.Equals(物品名))
                {
                    return 1;
                }
            }
            return 0;
        }
        else
        {
            if (!myData.材料背包.ContainsKey(物品名))
                return 0;
            else
                return bm.Xstoi(myData.材料背包[物品名].num);
        }
    }

    public string 返回金钱数量(string 名字)
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        io io_ = io.GetInstance();
        role_Data myData = io_.load();
        Dictionary<string, string> 金钱 = myData.金钱;
        //判断
        if (金钱.ContainsKey(名字))//判断背包里有没有该物品
        {
            return bm.Xstoi(金钱[名字])+"";
        }
        else
            return "0";


    }



    public string 检索装备背包(string 装备名字) {
        io io_ = io.GetInstance();
        role_Data myData = io_.load();
        string 键 = "";
        foreach (string key in myData.装备背包.Keys)
        {
            if (myData.装备背包[key].name.Equals(装备名字))
            {
                键 = key;
                return 键;
            }
        }
        return "";
    }

    /// <summary>
    /// 根据名字找到对象,同名装备装备会先指向先检索到的
    /// </summary>
    /// <param name="名字"></param>
    /// <param name="数量"></param>
    /// <returns></returns>
    public string 失去物品(string 名字, int 数量)
    {
        io io_ = io.GetInstance();
        Prop_bascis 物品 = 检索物品(名字);
        role_Data myData = io_.load();
        if (物品.type.Equals("3"))
        {
            string 键 = 检索装备背包(名字);
           
            //判断
            if (!键.Equals(""))//判断myData.装备背包里有没有该物品
            {
                    myData.装备背包.Remove(键);
                    io_.save(myData);
                    return "成功";

            }
            else
                return "没有该物品";
        }
        else
        {
            //判断
            if (myData.材料背包.ContainsKey(名字))//判断myData.材料背包里有没有该物品
            {
                if (bm.Xstoi(myData.材料背包[名字].num) >= 数量)
                {
                    myData.材料背包[名字].num = bm.Xitos(bm.Xstoi(myData.材料背包[名字].num) - 数量);
                    if (bm.Xstoi(myData.材料背包[名字].num) <= 0)
                        myData.材料背包.Remove(名字);
                    io_.save(myData);
                    return "成功";
                }
                else
                    return "数量不足";
            }
            else
                return "没有该物品";

        }


    }


    /// <summary>
    /// 根据UID找到对象,精准找到某一对象
    /// </summary>
    /// <param name="UID"></param>
    /// <returns></returns>
    public string 失去装备(string UID)
    {
        io io_ = io.GetInstance();
        role_Data myData = io_.load();
        if (myData.装备背包.ContainsKey(UID))
        {
            myData.装备背包.Remove(UID);
            io_.save(myData);
            return "成功";
        }
        else {
            return "没有该装备!";
        }


    }

    public void 背包CD道具持久化(string name)
    {
        myData = io_.load();
        if (myData.记录.ContainsKey("CD道具"))
        {
            myData.记录["CD道具"] = name;
        }
        else
        {
            myData.记录.Add("CD道具", name);
        }
        io_.save(myData);
        if (NameMgr.人物 != null)
        {
            combat cb = NameMgr.人物.GetComponent<combat>();
            cb.myData = myData;
        }
    }

    public string 失去金钱(string 名字, int 数量)
    {
        G_Util gut = NameMgr.画布.GetComponent<G_Util>();
        io io_ = io.GetInstance();
        role_Data myData = io_.load();
        Dictionary<string, string> 金钱 = myData.金钱;
        //判断
        if (金钱.ContainsKey(名字))//判断背包里有没有该物品
        {
            if (bm.Xstoi(金钱[名字]) >= 数量)
            {
                金钱[名字] = bm.Xitos(bm.Xstoi(金钱[名字]) - 数量);
                io_.save(myData);
                GameObject UI = GameObject.Find("UI").gameObject;
                cm.系统播报(new Prop_bascis(), 名字, 数量, "失去金钱");
                gut.刷新金钱UI(UI);
                return "成功";
            }
            else
                return "金钱不足";
        }
        else
            return "没有该货币";


    }

    public Dictionary<string, string> 提取装备属性(Equipment 装备)
    {
        /*Dictionary<string, string> 属性表 = new Dictionary<string, string>();
        if (bm.Xstoi(装备.atk) != 0)
            属性表.Add("攻击力", bm.Xstoi(装备.atk) + "");
        if (bm.Xstoi(装备.def) != 0)
            属性表.Add("防御力", bm.Xstoi(装备.def) + "");
        if (bm.Xstoi(装备.hp) != 0)
            属性表.Add("血量", bm.Xstoi(装备.hp) + "");
        if (bm.Xstoi(装备.hpr) != 0)
            属性表.Add("回血值", bm.Xstoi(装备.hpr) + "");
        if (bm.Xstoi(装备.atk_p) != 0)
            属性表.Add("攻击力加成", bm.Xstoi(装备.atk_p) + "%");
        if (bm.Xstoi(装备.def_p) != 0)
            属性表.Add("防御力加成", bm.Xstoi(装备.def_p) + "%");
        if (bm.Xstoi(装备.hp_p) != 0)
            属性表.Add("血量加成", bm.Xstoi(装备.hp_p) + "%");
        if (bm.Xstoi(装备.hpr_p) != 0)
            属性表.Add("回血值加成", bm.Xstoi(装备.hpr_p) + "%");
        if (bm.Xstoi(装备.aspd) != 0)
            属性表.Add("攻击速度", bm.Xstoi(装备.aspd) + "%");
        if (bm.Xstoi(装备.ms) != 0)
            属性表.Add("移动速度", bm.Xstoi(装备.ms) + "");
        if (bm.Xstoi(装备.cri) != 0)
            属性表.Add("暴击率", bm.Xstoi(装备.cri) + "%");
        if (bm.Xstoi(装备.cri_d) != 0)
            属性表.Add("暴伤加成", bm.Xstoi(装备.cri_d) + "%");
        if (bm.Xstoi(装备.harm) != 0)
            属性表.Add("固定伤害", bm.Xstoi(装备.harm) + "");
        if (bm.Xstoi(装备.harm_d) != 0)
            属性表.Add("固定减伤", bm.Xstoi(装备.harm_d) + "");
        if (bm.Xstoi(装备.harm_p) != 0)
            属性表.Add("伤害加成", bm.Xstoi(装备.harm_p) + "%");
        if (bm.Xstoi(装备.harm_p_d) != 0)
            属性表.Add("伤害减免", bm.Xstoi(装备.harm_p_d) + "%");
        if (bm.Xstoi(装备.vam) != 0)
            属性表.Add("固定吸血", bm.Xstoi(装备.vam) + "");
        if (bm.Xstoi(装备.vam_p) != 0)
            属性表.Add("吸血加成", bm.Xstoi(装备.vam_p) + "%");
        if (bm.Xstoi(装备.mon_p) != 0)
            属性表.Add("金钱加成", bm.Xstoi(装备.mon_p) + "%");
        if (bm.Xstoi(装备.exp_p) != 0)
            属性表.Add("经验加成", bm.Xstoi(装备.exp_p) + "%");
        return 属性表;*/

        Dictionary<string, string> 属性表 = new Dictionary<string, string>();
        //提取主属性
            属性表.Add(主属性表[装备.place], bm.Xitos(bm.Xstoi(装备.head_attribute_num)));
        //提取副属性
        if (!装备.next_attribute.Equals(""))
        {
            if (属性表.ContainsKey(装备.next_attribute))
            {
                属性表[装备.next_attribute] = bm.Xstoi(装备.next_num) + bm.Xitos(bm.Xstoi(属性表[装备.next_attribute]));
            }
            else
            {
                属性表.Add(装备.next_attribute, bm.Xitos(bm.Xstoi(装备.next_num)));
            }
        }
        //提取额外属性
        if (装备.extar_attribute != null && 装备.extar_attribute.Count > 0) {
            foreach (string 属性名 in 装备.extar_attribute.Keys)
            {
                if (属性表.ContainsKey(属性名))
                {
                    属性表[属性名] = bm.Xitos(bm.Xstoi(装备.extar_attribute[属性名]) + bm.Xstoi(属性表[属性名]));
                }
                else
                {
                    属性表.Add(属性名, bm.Xitos(bm.Xstoi(装备.extar_attribute[属性名])));
                }
            }
        }

        return 属性表;
    }

    public bool 检测物品是否满足(Dictionary<string, int> 物品集合)
    {

        role_Data myData = io.GetInstance().load();
        foreach (string 物品名 in 物品集合.Keys)
        {
            Prop_bascis 物品 = 检索物品(物品名);
            if (物品.type.Equals("3"))
            {
                if (检索装备背包(物品名).Equals(""))
                    return false;
            }
            else
            {
                if (!myData.材料背包.ContainsKey(物品名) || bm.Xstoi(myData.材料背包[物品名].num) < 物品集合[物品名])
                    return false;
            }
        }
        return true;

    }

    public Dictionary<string, string> 加载装备配置(combat cb)
    {
        Dictionary<string, string> 临时装备属性 = bm.返回空的战斗属性();
        myData = io.GetInstance().load();
        装备套装信息 = new Dictionary<string, int>();
        foreach (string 位置 in myData.装备槽.Keys)
        {
            Equipment 装备 = myData.装备槽[位置];
            if (装备 != null)
            {
                /* if (bm.Xstoi(装备.atk) != 0)
                     临时装备属性["攻击力"] = bm.Xitos(bm.Xstoi(装备.atk) + bm.Xstoi(临时装备属性["攻击力"]));
                 if (bm.Xstoi(装备.def) != 0)
                     临时装备属性["防御力"] = bm.Xitos(bm.Xstoi(装备.def) + bm.Xstoi(临时装备属性["防御力"]));
                 if (bm.Xstoi(装备.hp) != 0)
                     临时装备属性["血量"] = bm.Xitos(bm.Xstoi(装备.hp) + bm.Xstoi(临时装备属性["血量"]));
                 if (bm.Xstoi(装备.hpr) != 0)
                     临时装备属性["回血值"] = bm.Xitos(bm.Xstoi(装备.hpr) + bm.Xstoi(临时装备属性["回血值"]));
                 if (bm.Xstoi(装备.atk_p) != 0)
                     临时装备属性["攻击力加成"] = bm.Xitos(bm.Xstoi(装备.atk_p) + bm.Xstoi(临时装备属性["攻击力加成"]));
                 if (bm.Xstoi(装备.def_p) != 0)
                     临时装备属性["防御力加成"] = bm.Xitos(bm.Xstoi(装备.def_p) + bm.Xstoi(临时装备属性["防御力加成"]));
                 if (bm.Xstoi(装备.hp_p) != 0)
                     临时装备属性["血量加成"] = bm.Xitos(bm.Xstoi(装备.hp_p) + bm.Xstoi(临时装备属性["血量加成"]));
                 if (bm.Xstoi(装备.hpr_p) != 0)
                     临时装备属性["回血值加成"] = bm.Xitos(bm.Xstoi(装备.hpr_p) + bm.Xstoi(临时装备属性["回血值加成"]));
                 if (bm.Xstof(装备.aspd) != 0)
                     临时装备属性["攻击速度"] = bm.Xftos(bm.Xstof(装备.aspd) + bm.Xstof(临时装备属性["攻击速度"]));
                 if (bm.Xstoi(装备.ms) != 0)
                     临时装备属性["移动速度"] = bm.Xitos(bm.Xstoi(装备.ms) + bm.Xstoi(临时装备属性["移动速度"]));
                 if (bm.Xstoi(装备.cri) != 0)
                     临时装备属性["暴击率"] = bm.Xitos(bm.Xstoi(装备.cri) + bm.Xstoi(临时装备属性["暴击率"]));
                 if (bm.Xstoi(装备.cri_d) != 0)
                     临时装备属性["暴伤加成"] = bm.Xitos(bm.Xstoi(装备.cri_d) + bm.Xstoi(临时装备属性["暴伤加成"]));
                 if (bm.Xstoi(装备.harm) != 0)
                     临时装备属性["固定伤害"] = bm.Xitos(bm.Xstoi(装备.harm) + bm.Xstoi(临时装备属性["固定伤害"]));
                 if (bm.Xstoi(装备.harm_d) != 0)
                     临时装备属性["固定减伤"] = bm.Xitos(bm.Xstoi(装备.harm_d) + bm.Xstoi(临时装备属性["固定减伤"]));
                 if (bm.Xstoi(装备.harm_p) != 0)
                     临时装备属性["伤害加成"] = bm.Xitos(bm.Xstoi(装备.harm_p) + bm.Xstoi(临时装备属性["伤害加成"]));
                 if (bm.Xstoi(装备.harm_p_d) != 0)
                     临时装备属性["伤害减免"] = bm.Xitos(bm.Xstoi(装备.harm_p_d) + bm.Xstoi(临时装备属性["伤害减免"]));
                 if (bm.Xstoi(装备.vam) != 0)
                     临时装备属性["固定吸血"] = bm.Xitos(bm.Xstoi(装备.vam) + bm.Xstoi(临时装备属性["固定吸血"]));
                 if (bm.Xstoi(装备.vam_p) != 0)
                     临时装备属性["吸血加成"] = bm.Xitos(bm.Xstoi(装备.vam_p) + bm.Xstoi(临时装备属性["吸血加成"]));
                 if (bm.Xstoi(装备.mon_p) != 0)
                     临时装备属性["金钱加成"] = bm.Xitos(bm.Xstoi(装备.mon_p) + bm.Xstoi(临时装备属性["金钱加成"]));
                 if (bm.Xstoi(装备.exp_p) != 0)
                     临时装备属性["经验加成"] = bm.Xitos(bm.Xstoi(装备.exp_p) + bm.Xstoi(临时装备属性["经验加成"]));*/

                //提取主属性
                if (临时装备属性.ContainsKey(主属性表[装备.place]))
                {
                    临时装备属性[主属性表[装备.place]] = bm.Xitos(bm.Xstoi(装备.head_attribute_num) + bm.Xstoi(临时装备属性[主属性表[装备.place]]));
                }
                else
                {
                    临时装备属性.Add(主属性表[装备.place], bm.Xitos(bm.Xstoi(装备.head_attribute_num)));
                }
                //提取副属性
                if (!装备.next_attribute.Equals(""))
                {
                    if (临时装备属性.ContainsKey(装备.next_attribute))
                    {
                        临时装备属性[装备.next_attribute] = bm.Xitos(bm.Xstoi(装备.next_num) + bm.Xstoi(临时装备属性[装备.next_attribute]));
                    }
                    else
                    {
                        临时装备属性.Add(装备.next_attribute, bm.Xitos(bm.Xstoi(装备.next_num)));
                    }
                }
                    //提取额外属性
                    if (装备.extar_attribute != null && 装备.extar_attribute.Count > 0)
                {
                    //提取额外属性
                    foreach (string 属性名 in 装备.extar_attribute.Keys)
                    {
                        if (临时装备属性.ContainsKey(属性名))
                        {
                            临时装备属性[属性名] = bm.Xitos(bm.Xstoi(装备.extar_attribute[属性名]) + bm.Xstoi(临时装备属性[属性名]));
                        }
                        else
                        {
                            临时装备属性.Add(属性名, bm.Xitos(bm.Xstoi(装备.extar_attribute[属性名])));
                        }
                    }
                }


                if (!装备.tao.Equals(""))
                {
                    if (装备套装信息.ContainsKey(装备.tao))
                        装备套装信息[装备.tao]++;
                    else
                        装备套装信息.Add(装备.tao, 1);
                }
            }
        }


        foreach (string 套装名 in 装备套装信息.Keys)
        {
            if (装备套装信息[套装名] >= 2)
                加载套装配置(套装名, 装备套装信息[套装名], cb, 临时装备属性);
        }

        return 临时装备属性;
    }

    private void 加载套装配置(string 套装名, int 数量, combat cb, Dictionary<string, string> 临时装备属性)
    {

        suit_Data suData = PropMgr.套装表[套装名];
        suData.s_index = 数量 + "";
        Dictionary<string, string> 套装属性 = suData.激活套装();//生成属性
        PropMgr.套装表[套装名] = suData;//将赋了值的套装类更新
        foreach (string 属性名 in 套装属性.Keys)
        {
            if (属性名.Equals("血量"))
                临时装备属性["血量"] = bm.Xitos(bm.Xstoi(临时装备属性["血量"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("攻击力"))
                临时装备属性["攻击力"] = bm.Xitos(bm.Xstoi(临时装备属性["攻击力"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("防御力"))
                临时装备属性["防御力"] = bm.Xitos(bm.Xstoi(临时装备属性["防御力"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("回血值"))
                临时装备属性["回血值"] = bm.Xitos(bm.Xstoi(临时装备属性["回血值"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("暴击率"))
                临时装备属性["暴击率"] = bm.Xitos(bm.Xstoi(临时装备属性["暴击率"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("攻击速度"))
                临时装备属性["攻击速度"] = bm.Xitos(bm.Xstoi(临时装备属性["攻击速度"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("固定减伤"))
                临时装备属性["固定减伤"] = bm.Xitos(bm.Xstoi(临时装备属性["固定减伤"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("固定伤害"))
                临时装备属性["固定伤害"] = bm.Xitos(bm.Xstoi(临时装备属性["固定伤害"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("伤害减免"))
                临时装备属性["伤害减免"] = bm.Xitos(bm.Xstoi(临时装备属性["伤害减免"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("伤害加成"))
                临时装备属性["伤害加成"] = bm.Xitos(bm.Xstoi(临时装备属性["伤害加成"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("固定吸血"))
                临时装备属性["固定吸血"] = bm.Xitos(bm.Xstoi(临时装备属性["固定吸血"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("吸血加成"))
                临时装备属性["吸血加成"] = bm.Xitos(bm.Xstoi(临时装备属性["吸血加成"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("金钱加成"))
                临时装备属性["金钱加成"] = bm.Xitos(bm.Xstoi(临时装备属性["金钱加成"]) + bm.Xstoi(套装属性[属性名]));
            else if (属性名.Equals("经验加成"))
                临时装备属性["经验加成"] = bm.Xitos(bm.Xstoi(临时装备属性["经验加成"]) + bm.Xstoi(套装属性[属性名]));
        }

        //bm.加载属性(cb, 临时套装属性);//加载属性
    }

}
