using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPropMGR : BaseManager<BuyPropMGR>
{
    public static Dictionary<string, Dictionary<string, string>> ͭ���̳ǹ���� = ����ͭ���̳�();
    public static Dictionary<string, Dictionary<string, string>> ����̳ǹ���� = ���ؽ���̳�();
    public static Dictionary<string, Dictionary<string, string>> �ɾ��̳ǹ���� = �����ɾ��̳�();
    public static Dictionary<string, Dictionary<string, string>> �����̳ǹ���� = ���غ����̳�();
    public Dictionary<string, Dictionary<string, string>> �������� = new Dictionary<string, Dictionary<string, string>>();


    public static Dictionary<string, Dictionary<string, string>> ����ͭ���̳�() {
        Dictionary<string, Dictionary<string, string>> ����� = new Dictionary<string, Dictionary<string, string>>();
        //����ֻ��һ��-ͭ��
        �����.Add("С����", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "5000" }, { "����", "����" } });
        �����.Add("�󻹵�", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "50000" }, { "����", "����" } });
        �����.Add("Ѫ����", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "5000" }, { "����", "����" } });
        �����.Add("������", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "50000" }, { "����", "����" } });
        �����.Add("��Ƥ", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "5000" }, { "����", "����" } });
        �����.Add("�޹�", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "5000" }, { "����", "����" } });
        �����.Add("����", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "500" }, { "����", "����" } });
        �����.Add("��ζ���", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "2000" }, { "����", "����" } });
        �����.Add("�谮��ʳ", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "5000" }, { "����", "����" } });
        �����.Add("��ʥ����", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "30000" }, { "����", "����" } });
        �����.Add("����ɳ����", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "10000"}, { "����", "����" } });
        �����.Add("����ɳ����", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "10000" }, { "����", "����" } });
        �����.Add("��Ե���", new Dictionary<string, string>() {{"����","ͭ��"}, { "�۸�", "20000" }, { "����", "����" } });
        return �����;
    }


    public static Dictionary<string, Dictionary<string, string>> ���ؽ���̳�()
    {
        Dictionary<string, Dictionary<string, string>> ����� = new Dictionary<string, Dictionary<string, string>>();
        //����ֻ��һ��-���
        �����.Add("1000�ɾ���", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "5" }, { "����", "1" } });
        �����.Add("��Ԫ¶���(С)", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "2" }, { "����", "����" } });
        �����.Add("����֮��", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "2" }, { "����", "����" } });
        �����.Add("ͨ�������", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "1" }, { "����", "����" } });
        �����.Add("��ʥ����", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "1" }, { "����", "����" } });
        �����.Add("��֥", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "1" }, { "����", "����" } });
        �����.Add("������ʯ", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "1" }, { "����", "����" } });
        �����.Add("������Ϣ", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "5"}, { "����", "����" } });
        �����.Add("������Ϣ", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "10" }, { "����", "����" } });
        �����.Add("ͨ����װ���", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "200" }, { "����", "1" } });
        �����.Add("(�ķ�)<ͨ���ص�>", new Dictionary<string, string>() {{"����","���"}, { "�۸�", "500" }, { "����", "����" } });
        return �����;
    }

    public static Dictionary<string, Dictionary<string, string>> �����ɾ��̳�()
    {
        Dictionary<string, Dictionary<string, string>> ����� = new Dictionary<string, Dictionary<string, string>>();
        //����ֻ��һ��-�ɾ�
        �����.Add("��ʦ��", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "500" }, { "����", "����" } });
        �����.Add("��������ȯ", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "100" }, { "����", "����" } });
        �����.Add("��Ԫ¶", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "50" }, { "����", "����" } });
        �����.Add("������Ϣ", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "399" }, { "����", "����" } });
        �����.Add("������Ϣ", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "999" }, { "����", "����" } });
        �����.Add("����ɳ����", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "50" }, { "����", "����" } });
        �����.Add("����ɳ����", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "50" }, { "����", "����" } });
        �����.Add("��Ե���", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "100" }, { "����", "����" } });
        �����.Add("���＼�������", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "300" }, { "����", "����" } });
        �����.Add("����-������", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "500" }, { "����", "10" } });
        �����.Add("�׻�-������", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "500" }, { "����", "10" } });
        �����.Add("��ȸ-������", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "500" }, { "����", "10" } });
        �����.Add("����-������", new Dictionary<string, string>() {{"����","�ɾ�"}, { "�۸�", "500" }, { "����", "10" } });
        return �����;
    }


    public static Dictionary<string, Dictionary<string, string>> ���غ����̳�()
    {
        Dictionary<string, Dictionary<string, string>> ����� = new Dictionary<string, Dictionary<string, string>>();
        //����ֻ��һ��-�ɾ�
        �����.Add("С��֮��", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "50" }, { "����", "1" } });
        �����.Add("����֮��", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "50" }, { "����", "1" } });
        �����.Add("С��֮��", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "50" }, { "����", "1" } });
        �����.Add("���֮��", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "50" }, { "����", "1" } });
        �����.Add("������ʯ", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "1" }, { "����", "����" } });
        �����.Add("��ɫ����", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "1" }, { "����", "����" } });
        �����.Add("��ɫ����", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "2" }, { "����", "����" } });
        �����.Add("��ɫ����", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "5" }, { "����", "����" } });
        �����.Add("��Ƥ���(С)", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "2" }, { "����", "����" } });
        �����.Add("�޹����(С)", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "2" }, { "����", "����" } });
        �����.Add("�������", new Dictionary<string, string>() {{"����","����"}, { "�۸�", "50" }, { "����", "1" } });
        return �����;
    }


}
