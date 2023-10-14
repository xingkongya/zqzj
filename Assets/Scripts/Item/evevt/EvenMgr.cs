using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EvenMgr : BaseManager<EvenMgr>
{
    private basicMgr bm = basicMgr.GetInstance();
    public static Event_UI eu = DataMgr.GetInstance().���ض���["UI"].transform.Find("ս��ҳ/�¼�����").GetComponent<Event_UI>();
    public  Dictionary<string, Action> �¼���= �����¼���();



    public static Dictionary<string, Action> �����¼���() {
        Dictionary<string, Action> �¼��� = new Dictionary<string, Action>();
        �¼���.Add("����֮Լ", EvenMgr.eu.����֮Լ_UI);
        �¼���.Add("ɽ��Сʦ��", EvenMgr.eu.ɽ��Сʦ��_UI);
        �¼���.Add("��ó���", EvenMgr.eu.��ó���_UI);
        �¼���.Add("����ְҵ", EvenMgr.eu.����ְҵ_UI);

        return �¼���;
    }


    public void �¼�ģ��(string str,UnityAction �¼�,GameObject ���ݸ�����)
    {
        GameObject �¼�ʵ��;
        GameObject �¼�ģ�� = Resources.Load<GameObject>("�/�¼�����ģ��");
        �¼�ʵ�� =GameObject.Instantiate(�¼�ģ��);
        �¼�ʵ��.transform.Find("�ı�/����/Text").GetComponent<Text>().text = str;
        �¼�ʵ��.transform.SetParent(���ݸ�����.transform, false);
        GameObject ��ť = �¼�ʵ��.transform.Find("button/ǰ��").gameObject;
        bm.Banding(��ť, �¼�);
    }


    public void �¼�����(string �¼���) {
        �¼���[�¼���]();
    }


    public void �浵����¼�(string �¼�����,string ʣ��ʱ��) {
        G_Util gut = NameMgr.����.GetComponent<G_Util>();
        gut.�����¼���();
        role_Data myData = NameMgr.IO.load();
        myData.�¼���.Add(�¼�����, ʣ��ʱ��);
        NameMgr.IO.save(myData);

        //UI
        �����¼�ͼ�������(true);
    }

    public void �浵�Ƴ��¼�(string �¼�����)
    {
        role_Data myData = NameMgr.IO.load();
        if (myData.�¼���.ContainsKey(�¼�����))
        {
            myData.�¼���.Remove(�¼�����);
            NameMgr.IO.save(myData);

            //UI
            if (myData.�¼���.Count == 0) {
                �����¼�ͼ�������(false);
            }
        }
       
    }


    public void �����¼�ͼ�������(bool isActive) {
        DataMgr.GetInstance().���ض���["UI"].transform.Find("ս��ҳ/event").gameObject.SetActive(isActive);
    }
}
