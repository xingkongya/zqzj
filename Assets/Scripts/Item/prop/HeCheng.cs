using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeCheng : MonoBehaviour
{
    public string Key;
    private Prop_bascis ��Ʒ;
    private io IO;
    private G_Util gut;
    private basicMgr bm;
    private PropMgr pm;
    private Slider ����;


    private void Awake()
    {
        gut = NameMgr.����.GetComponent<G_Util>();
        IO = io.GetInstance();
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
        ���� = gameObject.transform.Find("button/Slider").GetComponent<Slider>();
    }


    public void ˢ���������ֵ()
    {
        int MaxNum = pm.���ر�������Ʒ������(pm.��Ʒ�ϳɱ�[��Ʒ.name]["�ϳ�����"]) / int.Parse(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]);
        int MaxEls;
        if (pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"].Equals("ͭ��"))
            MaxEls = int.Parse(pm.���ؽ�Ǯ����(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"])) / int.Parse(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]);
        else
            MaxEls = pm.���ر�������Ʒ������(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]) / int.Parse(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]);

        if (MaxEls > MaxNum)
        {
            ����.maxValue = MaxNum;
        }
        else {
            ����.maxValue = MaxEls;
        }

        if (����.maxValue != 0) {
            //��ʼ��valueֵΪ1
            ����.value = 1;
        }
    }


    public void ˢ�ºϳ���Ϣ()
    {
        role_Data myData = IO.load();
        if (myData.���ϱ���.ContainsKey(Key) )
        {
            ��Ʒ = myData.���ϱ���[Key];

            Text �����ı� = gameObject.transform.Find("�ı�/����").GetComponent<Text>();
            Text �����ı� = �����ı�.transform.Find("����").GetComponent<Text>();
            �����ı�.text = ��Ʒ.name;
            �����ı�.text = "X" + bm.Xstoi(��Ʒ.num);
            �����ı�.color = bm.ת����ɫ(bm.Xstoi(��Ʒ.qua));
            �����ı�.color = bm.ת����ɫ(bm.Xstoi(��Ʒ.qua));
        }
        else
        {
            gut.���ɾ����("��Ʒ����!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ˢ�ºϳ���Ϣ();
        ˢ���������ֵ();
        ˢ�ºϳ�������Ϣ();
    }



    public void ˢ�ºϳ�������Ϣ()
    {
        if (���� != null && ����.gameObject.activeSelf) {
            Text �����ı� = gameObject.transform.Find("�ı�/����/����").GetComponent<Text>();
            Text ���������ı� = gameObject.transform.Find("�ı�/��������/����").GetComponent<Text>();
            Text ����ı� = gameObject.transform.Find("�ı�/���/����").GetComponent<Text>();
            int num = (int)����.value;

            �����ı�.text = pm.��Ʒ�ϳɱ�[��Ʒ.name]["�ϳ�����"] + "(" + int.Parse(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]) * num + "/" + pm.���ر�������Ʒ������(pm.��Ʒ�ϳɱ�[��Ʒ.name]["�ϳ�����"]) + ")";
            if (pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"].Equals("ͭ��"))
            {
                ���������ı�.text = pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"] + "(" + int.Parse(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]) * num + "/" + pm.���ؽ�Ǯ����(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]) + ")";
            }
            else
            {
                ���������ı�.text = pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"] + "(" + int.Parse(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]) * num + "/" + pm.���ر�������Ʒ������(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]) + ")";
            }
            ����ı�.text = pm.��Ʒ�ϳɱ�[��Ʒ.name]["�ϳɻ��"] + "X" + num;
        }
    }




    public void �ϳɰ�ť()
    {
        //����߼��㷵��true,˵��ǿ���ɹ�,��ˢ����ͼ��
        if (�ϳ�_Control())
        {
            gut.���ɾ����("�ϳɳɹ�!");
            ˢ�ºϳ���Ϣ();
            ˢ���������ֵ();
            ˢ�ºϳ�������Ϣ();
            if (NameMgr.���� != null)
                NameMgr.����.GetComponent<Bag>().��ʼ������();
        }
    }



    public void ��һ() {
        if (����.value < ����.maxValue) {
            ����.value += 1;
        }
    }

    public void ��һ()
    {
        if (����.value > 0)
        {
            ����.value -= 1;
        }
    }


    public void ����() {
        if (����.value != ����.maxValue)
        {
            ����.value = ����.maxValue;
        }
    }



    private bool �ϳ�_Control()
    {
        int num = (int)����.value;
        string ����ֵ = pm.ʧȥ��Ʒ(pm.��Ʒ�ϳɱ�[��Ʒ.name]["�ϳ�����"], int.Parse(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]) * num);
        if (����ֵ.Equals("�ɹ�"))
        {
            string ����ֵ_����;
            if (pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"].Equals("ͭ��"))
            {
                 ����ֵ_���� = pm.ʧȥ��Ǯ(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"],int.Parse(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"])*num);
            }
            else
            {
                 ����ֵ_���� = pm.ʧȥ��Ʒ(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"], int.Parse(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]) * num);
            }

            if (!����ֵ_����.Equals("�ɹ�"))
            {
                pm.��ȡ��Ʒ(pm.��Ʒ�ϳɱ�[��Ʒ.name]["�ϳ�����"], int.Parse(pm.��Ʒ�ϳɱ�[��Ʒ.name]["��������"]) * num);
                gut.���ɾ����("�ϳ�ʧ��!");
                return false;
            }

            pm.��ȡ��Ʒ(pm.��Ʒ�ϳɱ�[��Ʒ.name]["�ϳɻ��"], num);
            return true;
        }
        else {
            gut.���ɾ����("�ϳ�ʧ��!");
            return false;
        }
    }

   






   
   


    public void ɾ���Լ�()
    {
        Destroy(gameObject);
    }
}
