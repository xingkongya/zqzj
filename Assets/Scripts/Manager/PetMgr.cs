using System;
using Random = System.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMgr : BaseManager<PetMgr>
{
    private basicMgr bm = basicMgr.GetInstance();
    private io io_ = io.GetInstance();

    public Pet_Data 宠物资质初始化(Pet_Data 宠物)
    {
        Random 随机类 = new Random(Guid.NewGuid().GetHashCode());
        宠物.ram_atk = bm.Xitos(随机类.Next(bm.Xstoi(宠物.qua_atk) / 10, bm.Xstoi(宠物.qua_atk) + 1));
        宠物.ram_def = bm.Xitos(随机类.Next(bm.Xstoi(宠物.qua_def) / 10, bm.Xstoi(宠物.qua_def) + 1));
        宠物.ram_hp = bm.Xitos(随机类.Next(bm.Xstoi(宠物.qua_hp) / 10, bm.Xstoi(宠物.qua_hp) + 1));
        宠物.ram_hpr = bm.Xitos(随机类.Next(bm.Xstoi(宠物.qua_hpr) / 10, bm.Xstoi(宠物.qua_hpr) + 1));
        宠物 = 初始化宠物技能(宠物);
        return 宠物;
    }


    public Pet_Data 宠物洗练(Pet_Data 宠物)
    {
        Random 随机类 = new Random(Guid.NewGuid().GetHashCode());
        if (宠物.锁.Count != 0)
        {
            if (!宠物.锁.Contains("攻击"))
            {
                宠物.ram_atk = bm.Xitos(随机类.Next(bm.Xstoi(宠物.qua_atk) / 10, bm.Xstoi(宠物.qua_atk) + 1));
            }
            if (!宠物.锁.Contains("防御"))
            {
                宠物.ram_def = bm.Xitos(随机类.Next(bm.Xstoi(宠物.qua_def) / 10, bm.Xstoi(宠物.qua_def) + 1));
            }
            if (!宠物.锁.Contains("血量"))
            {
                宠物.ram_hp = bm.Xitos(随机类.Next(bm.Xstoi(宠物.qua_hp) / 10, bm.Xstoi(宠物.qua_hp) + 1));
            }
            if (!宠物.锁.Contains("回血"))
            {
                宠物.ram_hpr = bm.Xitos(随机类.Next(bm.Xstoi(宠物.qua_hpr) / 10, bm.Xstoi(宠物.qua_hpr) + 1));
            }

        }
        else
        {
            宠物 = 宠物资质初始化(宠物);
        }
        return 宠物;
    }


    public Pet_Data 获取唯一ID(Pet_Data 宠物)
    {
        宠物.id = bm.获取UID();
        return 宠物;
    }


    public Pet_Data 初始化宠物技能(Pet_Data 宠物)
    {
        if (!宠物.skilllist_str.Equals(""))
        {
            string[] 技能数组 = 宠物.skilllist_str.Split('-');
            foreach (string str in 技能数组)
            {
                宠物.skilllist.Add(str);
            }
        }
        return 宠物;
    }

    public Pet_Data 返回宠物(string UID)
    {
        role_Data myData = io_.load();
        foreach (Pet_Data 宠物 in myData.宠物栏)
        {
            if (宠物.id.Equals(UID))
            {
                return 宠物;
            }
        }
        return null;
    }

    public Pet_Data 宠物初始满资质(Pet_Data 宠物)
    {
        宠物.ram_atk = 宠物.qua_atk;
        宠物.ram_def = 宠物.qua_def;
        宠物.ram_hp = 宠物.qua_hp;
        宠物.ram_hpr = 宠物.qua_hpr;
        宠物 = 初始化宠物技能(宠物);
        return 宠物;
    }

    public Pet_Data 宠物升级(Pet_Data pd)
    {
        pd.grade = bm.Xor(bm.Xstoi(pd.grade) + 1 + "");
        return pd;

    }

    public Pet_Data 宠物成长提升(Pet_Data pd)
    {
        pd.cc = bm.Xor(bm.Xstoi(pd.cc) + 1 + "");
        return pd;
    }

    public bool 删除宠物(string UID)
    {
        role_Data myData = io_.load();
        for (int i = 0; i < myData.宠物栏.Count; i++)
        {
            if (myData.宠物栏[i].id.Equals(UID))
            {
                myData.宠物栏.RemoveAt(i);
                io_.save(myData);
                return true;
            }
        }
        return false;

    }


    public void 宠物全部满血()
    {
        role_Data myData = io_.load();
        foreach (Pet_Data 宠物 in myData.宠物栏)
        {
            宠物.state = "";
        }
        io_.save(myData);
    }

    public role_Data 存档宠物属性覆盖(role_Data myData, Pet_Data 宠物)
    {
        for (int i = 0; i < myData.宠物栏.Count; i++)
        {
            if (myData.宠物栏[i].id.Equals(宠物.id))
                myData.宠物栏[i] = 宠物;
        }
        return myData;

    }

    public Pet_Data 宠物信息更新(Pet_Data OldPet)
    {
        Pet_Data NewPet = PropMgr.宠物表[OldPet.name];
        NewPet.ram_atk = OldPet.ram_atk;
        NewPet.ram_def = OldPet.ram_def;
        NewPet.ram_hp = OldPet.ram_hp;
        NewPet.ram_hpr = OldPet.ram_hpr;
        return NewPet;
    }
}
