using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiaDian : MonoBehaviour
{

    public G_Util gut;
    private TianFuMgr tfm;
    TianFu 铜币1;
    TianFu 铜币2;
    TianFu 铜币3;
    TianFu 铜币4;
    TianFu 铜币5;
    TianFu 铜币6;
    TianFu 铜币7;
    TianFu 铜币8;
    TianFu 仙晶1;
    TianFu 仙晶2;
    TianFu 仙晶3;
    TianFu 仙晶4;
    TianFu 仙晶5;
    TianFu 仙晶6;
    TianFu 仙晶7;
    TianFu 仙晶8;



    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
        tfm = TianFuMgr.GetInstance();
        初始化天赋加点图标();
    }



    public void 初始化天赋加点图标() {
        GameObject 铜币_1 = gameObject.transform.Find("铜币项/Panel/天赋1/图标").gameObject;
        GameObject 铜币_2 = gameObject.transform.Find("铜币项/Panel/天赋2/图标").gameObject;
        GameObject 铜币_3 = gameObject.transform.Find("铜币项/Panel/天赋3/图标").gameObject;
        GameObject 铜币_4 = gameObject.transform.Find("铜币项/Panel/天赋4/图标").gameObject;
        GameObject 铜币_5 = gameObject.transform.Find("铜币项/Panel/天赋5/图标").gameObject;
        GameObject 铜币_6 = gameObject.transform.Find("铜币项/Panel/天赋6/图标").gameObject;
        GameObject 铜币_7 = gameObject.transform.Find("铜币项/Panel/天赋7/图标").gameObject;
        GameObject 铜币_8 = gameObject.transform.Find("铜币项/Panel/天赋8/图标").gameObject;
        GameObject 仙晶_1 = gameObject.transform.Find("仙晶项/Panel/天赋1/图标").gameObject;
        GameObject 仙晶_2 = gameObject.transform.Find("仙晶项/Panel/天赋2/图标").gameObject;
        GameObject 仙晶_3 = gameObject.transform.Find("仙晶项/Panel/天赋3/图标").gameObject;
        GameObject 仙晶_4 = gameObject.transform.Find("仙晶项/Panel/天赋4/图标").gameObject;
        GameObject 仙晶_5 = gameObject.transform.Find("仙晶项/Panel/天赋5/图标").gameObject;
        GameObject 仙晶_6 = gameObject.transform.Find("仙晶项/Panel/天赋6/图标").gameObject;
        GameObject 仙晶_7 = gameObject.transform.Find("仙晶项/Panel/天赋7/图标").gameObject;
        GameObject 仙晶_8 = gameObject.transform.Find("仙晶项/Panel/天赋8/图标").gameObject;
        GameObject 黑钻_1 = gameObject.transform.Find("黑钻项/Panel/天赋1").gameObject;
        GameObject 黑钻_2 = gameObject.transform.Find("黑钻项/Panel/天赋2").gameObject;
        GameObject 黑钻_3 = gameObject.transform.Find("黑钻项/Panel/天赋3").gameObject;
        GameObject 黑钻_4 = gameObject.transform.Find("黑钻项/Panel/天赋4").gameObject;

         铜币1 = tfm.返回天赋("强化骨骼");
         铜币2 = tfm.返回天赋("强化皮肤");
         铜币3 = tfm.返回天赋("强化血液");
         铜币4 = tfm.返回天赋("强化呼吸");
         铜币5= tfm.返回天赋("九牛二虎");
         铜币6 = tfm.返回天赋("琉璃金身");
         铜币7 = tfm.返回天赋("蛮牛之体");
         铜币8 = tfm.返回天赋("枯木逢春");
         仙晶1 = tfm.返回天赋("锋锐");
         仙晶2 = tfm.返回天赋("坚固");
         仙晶3 = tfm.返回天赋("爆裂");
         仙晶4 = tfm.返回天赋("饮血");
         仙晶5 = tfm.返回天赋("势不可挡");
         仙晶6 = tfm.返回天赋("不动如山");
         仙晶7 = tfm.返回天赋("雷霆万钧");
         仙晶8 = tfm.返回天赋("蹑影追风");


        tfm.天赋加载图标(铜币_1, 铜币1);
        tfm.天赋加载图标(铜币_2, 铜币2);
        tfm.天赋加载图标(铜币_3, 铜币3);
        tfm.天赋加载图标(铜币_4, 铜币4);
        tfm.天赋加载图标(铜币_5, 铜币5);
        tfm.天赋加载图标(铜币_6, 铜币6);
        tfm.天赋加载图标(铜币_7, 铜币7);
        tfm.天赋加载图标(铜币_8, 铜币8);
        tfm.天赋加载图标(仙晶_1, 仙晶1);
        tfm.天赋加载图标(仙晶_2, 仙晶2);
        tfm.天赋加载图标(仙晶_3, 仙晶3);
        tfm.天赋加载图标(仙晶_4, 仙晶4);
        tfm.天赋加载图标(仙晶_5, 仙晶5);
        tfm.天赋加载图标(仙晶_6, 仙晶6);
        tfm.天赋加载图标(仙晶_7, 仙晶7);
        tfm.天赋加载图标(仙晶_8, 仙晶8);

    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 生成铜币1天赋() {
        gut.生成天赋框(gameObject,铜币1);
    }

    public void 生成铜币2天赋()
    {
        gut.生成天赋框(gameObject, 铜币2);
    }

    public void 生成铜币3天赋()
    {
        gut.生成天赋框(gameObject, 铜币3);
    }
    public void 生成铜币4天赋()
    {
        gut.生成天赋框(gameObject, 铜币4);
    }

    public void 生成铜币5天赋()
    {
        gut.生成天赋框(gameObject, 铜币5);
    }

    public void 生成铜币6天赋()
    {
        gut.生成天赋框(gameObject, 铜币6);
    }

    public void 生成铜币7天赋()
    {
        gut.生成天赋框(gameObject, 铜币7);
    }
    public void 生成铜币8天赋()
    {
        gut.生成天赋框(gameObject, 铜币8);
    }

    public void 生成仙晶1天赋()
    {
        gut.生成天赋框(gameObject, 仙晶1);
    }

    public void 生成仙晶2天赋()
    {
        gut.生成天赋框(gameObject, 仙晶2);
    }

    public void 生成仙晶3天赋()
    {
        gut.生成天赋框(gameObject, 仙晶3);
    }
    public void 生成仙晶4天赋()
    {
        gut.生成天赋框(gameObject, 仙晶4);
    }

    public void 生成仙晶5天赋()
    {
        gut.生成天赋框(gameObject, 仙晶5);
    }

    public void 生成仙晶6天赋()
    {
        gut.生成天赋框(gameObject, 仙晶6);
    }

    public void 生成仙晶7天赋()
    {
        gut.生成天赋框(gameObject, 仙晶7);
    }
    public void 生成仙晶8天赋()
    {
        gut.生成天赋框(gameObject, 仙晶8);
    }


    public void 删除自己()
    {
        gut.删除对象(gameObject);
    }
}
