using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdressMgr : BaseManager<AdressMgr>
{
   public static Dictionary<string,int> ��ͼ��=���ص�ͼ��();

    public static Dictionary<string, int> ���ص�ͼ��() {
        Dictionary<string, int> ��ͼ�� = new Dictionary<string, int>();
        ��ͼ��.Add("ľ��(��)",0);
        ��ͼ��.Add("��Դ��",0);
        ��ͼ��.Add("���С��",0);
        ��ͼ��.Add("С����",5);
        ��ͼ��.Add("��ڶ�", 15);
        ��ͼ��.Add("ɽ������", 30);
        ��ͼ��.Add("ʮ����", 50);
        ��ͼ��.Add("��ʯ̲", 200);
        ��ͼ��.Add("��ԭ", 70);
        ��ͼ��.Add("ج��֮ɭ", 100);
        ��ͼ��.Add("��ɳ��", 120);
        ��ͼ��.Add("�ٺ��ٵ�", 150);
        ��ͼ��.Add("�ٺ���", 0);
        ��ͼ��.Add("�ٺ��Ƕ�", 0);
        ��ͼ��.Add("�ٺ�����", 0);
        ��ͼ��.Add("�ٺ�����", 260);
        ��ͼ��.Add("�����", 450);
        ��ͼ��.Add("����", 0);

        return ��ͼ��;
    }




    public bool ���ս�����Ƿ���(string ����) {
        G_Util gut = NameMgr.����.GetComponent<G_Util>();
        int ����ս����= gut.��������ս����_�޵�λ();
        int ��ͽ���ս���� = 0;
        if (��ͼ��.ContainsKey(����)) {
            ��ͽ���ս���� = ��ͼ��[����];
        }

        if (����ս���� < ��ͽ���ս����) {
            return false;
        }

        return true;
    }
}
