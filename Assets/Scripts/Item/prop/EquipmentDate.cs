using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDate : MonoBehaviour
{
    public string Key;
    private G_Util gut;
    private io IO;
    private basicMgr bm;
    private PropMgr pm;
    private DataMgr dm;



    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        IO = io.GetInstance();
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
        dm = DataMgr.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 生成强化界面() {
        gut.生成强化界面(Key);
    }


    public void 穿戴or卸下() {
        role_Data myData = IO.load();
        //判断key是穿戴装备还是卸下装备,这里是穿戴装备
        if (!pm.主属性表.ContainsKey(Key))
        {
            Equipment 装备 = myData.装备背包[Key];

            if (bm.Xstoi(myData.等级) < bm.Xstoi(装备.lessgrade))
            {
                gut.生成警告框("等级不足");
                return;
            }

            //数据写入-装备
            if (myData.装备槽[装备.place] != null)//判断原先位置是否有装备,有则取下
                pm.获取物品(myData.装备槽[装备.place]);
            myData = IO.load();//更新数据
            myData.装备槽[装备.place] = 装备;
            IO.save(myData);
            pm.失去装备(Key);

            gut.生成警告框("使用成功");
            if (NameMgr.背包 != null)
                NameMgr.背包.GetComponent<Bag>().初始化背包();
        }
        //卸下
        else {
            //数据写入-装备
            pm.获取物品(myData.装备槽[Key]);
            myData = IO.load();//更新数据
            myData.装备槽[Key] = null;
            IO.save(myData);

            //改变UI
            if (dm.本地对象.ContainsKey("属性面板")&& dm.本地对象["属性面板"]!=null) {
                dm.本地对象["属性面板"].GetComponent<Role_Panel>().ini_panel();
                gut.检索装备栏生成流光();

                //刷新属性
                gut.面板属性刷新(dm.本地对象["属性面板"]);
            }
        }
       
        if (!NameMgr.画布.CompareTag("幻界"))
            DataMgr.GetInstance().本地对象["主角"].GetComponent<combat>().人物属性刷新();
        

        Destroy(gameObject);
    }

}
