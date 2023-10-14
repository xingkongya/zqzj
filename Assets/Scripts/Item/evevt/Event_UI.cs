using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_UI : MonoBehaviour
{
    private basicMgr bm;
    GameObject �¼�ʵ��;
    private Dictionary<string, string> �¼��б�;
    private GameObject ���ݸ�����;
    private GameObject ͼ�길����;
    private G_Util gut;
    private io io_;

    private void Awake()
    {
        io_ = io.GetInstance();
        bm = basicMgr.GetInstance();
        ���ݸ����� = gameObject.transform.Find("�¼���Ϣ���/�¼�����").gameObject;
        ͼ�길���� = gameObject.transform.Find("�¼���Ϣ���/ͷ������/Scroll View/Viewport/Content").gameObject;
        gut = NameMgr.����.GetComponent<G_Util>();
    }



    public void OnEnable()
    {
        gut.����������(ͼ�길����, 3);
        gut.����������(���ݸ�����, 3);
        role_Data myData = NameMgr.IO.load();
        �¼��б� = myData.�¼���;
        int i = �¼��б�.Count-1;
        foreach (string ActName in �¼��б�.Keys)
        {
            gut.���ɻ��(ͼ�길����, ActName, i,"�¼�");
            i--;
        }
    }




  

    public void ����¼�ͼ��()
    {
        gut.����������(���ݸ�����, 3);
        string ActName = GameObject.FindGameObjectWithTag("ѡ��").transform.Find("����").GetComponent<Text>().text;
        EvenMgr.GetInstance().�¼�����(ActName);
    }


    public void ����֮Լ_UI() {
        GameObject ����֮Լ = Resources.Load<GameObject>("�/����֮Լ") as GameObject;
        �¼�ʵ�� = Instantiate(����֮Լ);
        �¼�ʵ��.transform.SetParent(���ݸ�����.transform, false);
    }

    public void ɽ��Сʦ��_UI()
    {
        GameObject ɽ��Сʦ�� = Resources.Load<GameObject>("�/ɽ��Сʦ��") as GameObject;
        �¼�ʵ�� = Instantiate(ɽ��Сʦ��);
        �¼�ʵ��.transform.SetParent(���ݸ�����.transform, false);
    }

    public void ��ó���_UI()
    {
        EvenMgr.GetInstance().�¼�ģ��("������<color=blue>��ԭ</color>����,�ƺ�������ʲô����������������÷����...��ȥ������", ��ת����_��ԭ, ���ݸ�����);
    }

    public void ����ְҵ_UI()
    {
        EvenMgr.GetInstance().�¼�ģ��("����ǩ��ֽ,ǰ��<color=blue>�ٺ���</color>��Ѱ�����ܸ��׶��ݵ���,�����Ƿ��б�ǿ�ķ���", ��ת����_�ٺ���, ���ݸ�����);
    }

    public void ��ת����_��ԭ() {
        gut.�ƶ�("��ԭ");
        �����Լ�();
    }

    public void ��ת����_�ٺ���()
    {
        gut.�ƶ�("�ٺ���");
        �����Լ�();
    }


    public void �����Լ�() {
        gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
