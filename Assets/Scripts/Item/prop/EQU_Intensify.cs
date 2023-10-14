using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EQU_Intensify : MonoBehaviour
{
    public string Key;
    private Equipment װ��;
    private io IO;
    private G_Util gut;
    private basicMgr bm;
    private PropMgr pm;
    public Dictionary<string, string> ǿ���ɹ��� = new Dictionary<string, string>() { { "0", "100" }, { "1", "95" }, { "2", "90" }, { "3", "80" }, { "4", "70" }, { "5", "60" }, { "6", "45" }, { "7", "30" }, { "8", "25" }, { "9", "20" }, { "10", "15" }, { "11", "12" }, { "12", "9" }, { "13", "5" }, { "14", "1" } };
    private  Dictionary<string, string> ������� = new Dictionary<string, string>() { { "0", "��ɫ����" }, { "1", "��ɫ����" }, { "2", "��ɫ����" }, { "3", "��ɫ����" }, { "4", "��ɫ����" }, { "5", "��ɫ����" } };

    private void Awake()
    {
        gut = NameMgr.����.GetComponent<G_Util>();
        IO = io.GetInstance();
        bm = basicMgr.GetInstance();
        pm = PropMgr.GetInstance();
    }


    public void ˢ��װ����Ϣ() {
        role_Data myData = IO.load();
        if (myData.װ������.ContainsKey(Key) || myData.װ����.ContainsKey(Key))
        {
            if (myData.װ������.ContainsKey(Key))
            {
                װ�� = myData.װ������[Key];
            }
            else
            {
                װ�� = myData.װ����[Key];
            }


        }
        else
        {
            gut.���ɾ����("װ������!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ˢ��װ����Ϣ();
        ˢ��װ��ǿ����Ϣ();
        ˢ���ı���Ϣ();
    }



    public void ˢ��װ��ǿ����Ϣ() {
        Text �����ı� = gameObject.transform.Find("�ı�/����").GetComponent<Text>();
        Text ǿ���ı� = �����ı�.transform.Find("ǿ���ȼ�").GetComponent<Text>();
        �����ı�.text = װ��.name;
        ǿ���ı�.text = "+" + bm.Xstoi(װ��.lv);
        �����ı�.color = bm.ת����ɫ(bm.Xstoi(װ��.qua));
        ǿ���ı�.color = bm.ת����ɫ(bm.Xstoi(װ��.qua));
    }


    public void ˢ���ı���Ϣ()
    {
        Text ���������ı� = gameObject.transform.Find("�ı�/����/����").GetComponent<Text>();
        Text ���������ı� = gameObject.transform.Find("�ı�/����/����").GetComponent<Text>();
        Text �ɹ����ı� = gameObject.transform.Find("�ı�/�ɹ���/����").GetComponent<Text>();
        Text ʧ�ܳͷ��ı� = gameObject.transform.Find("�ı�/ʧ�ܳͷ�/����").GetComponent<Text>();
        Text ��ǰ�����ı� = gameObject.transform.Find("�ı�/��ǰ����/�ı�/��ֵ").GetComponent<Text>();
        Text ǿ�������ı� = gameObject.transform.Find("�ı�/ǿ������/�ı�/��ֵ").GetComponent<Text>();
        ���������ı�.text = �������[bm.Xstoi(װ��.qua) + ""] + "";
        ���������ı�.text = "<color=red>1</color>/"+pm.���ر�������Ʒ������(�������[bm.Xstoi(װ��.qua) + ""]) + "";
        ��ǰ�����ı�.text = PropMgr.GetInstance().�����Ա�[װ��.place] + "+"+pm.����װ��������(װ��);
        ǿ�������ı�.text = PropMgr.GetInstance().�����Ա�[װ��.place] + "+" + pm.����װ��ǿ��������(װ��);
        if (bm.Xstoi(װ��.lv) < int.Parse(PropMgr.GetInstance().��ɫ�ȼ�����[bm.Xstoi(װ��.qua) + ""])) {
            �ɹ����ı�.text = ǿ���ɹ���[bm.Xstoi(װ��.lv) + ""]+"%";
        }
        else {
            �ɹ����ı�.text = "ǿ��������";
        }
        if (bm.Xstoi(װ��.lv) < 5)
        {
            ʧ�ܳͷ��ı�.text = "��";
        }
        else if (bm.Xstoi(װ��.lv) < 8)
        {
            ʧ�ܳͷ��ı�.text = "С���ʵ�һ��";

        }
        else if (bm.Xstoi(װ��.lv) < 12)
        {
            ʧ�ܳͷ��ı�.text = "���ʵ�һ��";

        }
        else if (bm.Xstoi(װ��.lv) < 15)
        {
            ʧ�ܳͷ��ı�.text = "��һ��";

        }
    }



    public void ǿ����ť() {
        //����߼��㷵��true,˵��ǿ���ɹ�,��ˢ����ͼ��
        if (ǿ��_Control()) {
            gut.���ɾ����("ǿ���ɹ�!");
            ˢ��װ����Ϣ();
            ˢ��װ��ǿ����Ϣ();
            ˢ���ı���Ϣ();
            if (NameMgr.����!=null)
                NameMgr.����.GetComponent<Bag>().��ʼ������();
        }
    }

    public void һ����1��ť()
    {
        //����߼��㷵��true,˵��ǿ���ɹ�,��ˢ����ͼ��
        if (һ��_Control())
        {
            gut.���ɾ����("ǿ���ɹ�!");
        }
        ˢ��װ����Ϣ();
        ˢ��װ��ǿ����Ϣ();
        ˢ���ı���Ϣ();
        if (NameMgr.���� != null)
            NameMgr.����.GetComponent<Bag>().��ʼ������();

}




    private bool ǿ��_Control() {
        string ������ = �������[bm.Xstoi(װ��.qua) + ""];
        if (pm.���ر�������Ʒ������(������) > 1)
        {
            if (bm.Xstoi(װ��.lv)<int.Parse( PropMgr.GetInstance().��ɫ�ȼ�����[bm.Xstoi(װ��.qua)+""])) {
                string ���ؽ�� = pm.ʧȥ��Ʒ(������, 1);
                if (���ؽ��.Equals("�ɹ�"))
                {
                    int �ɹ��� = int.Parse(ǿ���ɹ���[bm.Xstoi(װ��.lv) + ""]);
                    if (gut.����(�ɹ���, 100))
                    {
                        ǿ��_Dao();
                        return true;
                    }
                    else
                    {
                        gut.���ɾ����("ǿ��ʧ��!");
                        return false;
                    }
                }
                else
                {
                    gut.���ɾ����("���ϲ���");
                    return false;
                }
            }
            else {
                gut.���ɾ����("ǿ��������");
                return false;
            }
        }
        else
        {
            gut.���ɾ����("���ϲ���");
            return false;
        }
    }

    private bool һ��_Control()
    {
        int ��ǰ�ȼ� = bm.Xstoi(װ��.lv);
        string ������ = �������[bm.Xstoi(װ��.qua) + ""];
        int num = pm.���ر�������Ʒ������(������) > 20 ? 20 : pm.���ر�������Ʒ������(������);
        for (int i=0;i< num;i++)
        {
           
                if (bm.Xstoi(װ��.lv) < int.Parse(PropMgr.GetInstance().��ɫ�ȼ�����[bm.Xstoi(װ��.qua) + ""]))
                {
                    string ���ؽ�� = pm.ʧȥ��Ʒ(������, 1);
                    if (���ؽ��.Equals("�ɹ�"))
                    {
                        int �ɹ��� = int.Parse(ǿ���ɹ���[bm.Xstoi(װ��.lv) + ""]);
                        if (gut.����(�ɹ���, 100))
                        {
                            ǿ��_Dao();
                        if (bm.Xstoi(װ��.lv)>��ǰ�ȼ�) {
                            return true;
                        }
                        }
                        else
                        {
                        ǿ��ʧ���ж�_Control();
                            continue;
                        }
                    }
                    else
                    {
                    gut.���ɾ����("���ϲ���");
                    return false;
                    }
                }
                else
                {
                gut.���ɾ����("ǿ��������");
                return false;
            }

        }
        gut.���ɾ����("ǿ��ʧ��!");
        return false;
    }





        private void ǿ��_Dao() {
        role_Data myData = IO.load();
        װ��.lv = bm.Xitos(bm.Xstoi(װ��.lv) + 1);
        װ��= pm.װ����ʼ��������ֵ(װ��);
        װ��.islock = "1";
        if (myData.װ������.ContainsKey(Key))
        {    
            myData.װ������[Key]=װ��;
        }
        else
        {
            myData.װ����[Key] = װ��;
        }
        IO.save(myData);
    }

    private void ǿ��ʧ���ж�_Control()
    {
        if (bm.Xstoi(װ��.lv) >= 5 && bm.Xstoi(װ��.lv) < 8)
        {
            if (gut.����(30, 100))
            {
                ǿ��ʧ��_Dao();
            }
        }
        else if (bm.Xstoi(װ��.lv) < 12)
        {
            if (gut.����(70, 100))
            {
                ǿ��ʧ��_Dao();
            }
        }
        else if (bm.Xstoi(װ��.lv) < 15) {
            ǿ��ʧ��_Dao();
        }


    }

    private void ǿ��ʧ��_Dao() {
        role_Data myData = IO.load();
        װ��.lv = bm.Xitos(bm.Xstoi(װ��.lv) - 1);
        װ�� = pm.װ����ʼ��������ֵ(װ��);
        if (myData.װ������.ContainsKey(Key))
        {
            myData.װ������[Key] = װ��;
        }
        else
        {
            myData.װ����[Key] = װ��;
        }
        IO.save(myData);
    }



    public void ɾ���Լ�() {
        Destroy(gameObject);
    }

}
