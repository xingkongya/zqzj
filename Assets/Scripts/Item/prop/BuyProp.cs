using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyProp : MonoBehaviour
{
    private basicMgr bm;
    private G_Util gut;
    private GameObject ͭ�Ұ�ť;
    private GameObject ��Ұ�ť;
    private GameObject �ɾ���ť;
    private GameObject ���갴ť;


    private void Awake()
    {
        bm = basicMgr.GetInstance();
        gut = NameMgr.����.GetComponent<G_Util>();
        ͭ�Ұ�ť = gameObject.transform.Find("Panel").Find("�̳�ҳ/ѡ��/ͭ���̵�").gameObject;
        ��Ұ�ť = gameObject.transform.Find("Panel").Find("�̳�ҳ/ѡ��/����̵�").gameObject;
        �ɾ���ť = gameObject.transform.Find("Panel").Find("�̳�ҳ/ѡ��/�ɾ��̵�").gameObject;
        ���갴ť = gameObject.transform.Find("Panel").Find("�̳�ҳ/ѡ��/�����̵�").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ���ͭ��ѡ��()
    {
        bm.�ı���ɫ(ͭ�Ұ�ť.GetComponent<Image>(), "FFFFFF");
        bm.�ı���ɫ(��Ұ�ť.GetComponent<Image>(), "A4A0A0");
        bm.�ı���ɫ(�ɾ���ť.GetComponent<Image>(), "A4A0A0");
        bm.�ı���ɫ(���갴ť.GetComponent<Image>(), "A4A0A0");
        gut.��ʼ��ͭ���̳�();
    }


   

    public void ������ѡ��()
    {
        bm.�ı���ɫ(ͭ�Ұ�ť.GetComponent<Image>(), "A4A0A0");
        bm.�ı���ɫ(��Ұ�ť.GetComponent<Image>(), "FFFFFF");
        bm.�ı���ɫ(�ɾ���ť.GetComponent<Image>(), "A4A0A0");
        bm.�ı���ɫ(���갴ť.GetComponent<Image>(), "A4A0A0");
        gut.��ʼ������̳�();
    }




    public void ����ɾ�ѡ��()
    {
        bm.�ı���ɫ(ͭ�Ұ�ť.GetComponent<Image>(), "A4A0A0");
        bm.�ı���ɫ(��Ұ�ť.GetComponent<Image>(), "A4A0A0");
        bm.�ı���ɫ(�ɾ���ť.GetComponent<Image>(), "FFFFFF");
        bm.�ı���ɫ(���갴ť.GetComponent<Image>(), "A4A0A0");
        gut.��ʼ���ɾ��̳�();
    }



    public void �������ѡ��()
    {
        bm.�ı���ɫ(ͭ�Ұ�ť.GetComponent<Image>(), "A4A0A0");
        bm.�ı���ɫ(��Ұ�ť.GetComponent<Image>(), "A4A0A0");
        bm.�ı���ɫ(�ɾ���ť.GetComponent<Image>(), "A4A0A0");
        bm.�ı���ɫ(���갴ť.GetComponent<Image>(), "FFFFFF");
        gut.��ʼ�������̳�();
    }


}
