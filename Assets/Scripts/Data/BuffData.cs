using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffData 
{
    public string buff����;
    public string buff������������;//������������Ϊ�ֵ�
    public float buff����������ֵ;
    public int ����;
    public int ������;
    public float ����ʱ��;
    public bool �Ƿ���Ե���;

    public BuffData(string buff����,string buff������������, float buff����������ֵ,int ����, int ������, float ����ʱ��,bool �Ƿ���Ե���) {
        this.buff���� = buff����;
        this.buff������������ = buff������������;
        this.buff����������ֵ = buff����������ֵ;
        this.���� = ����;
        this.������ = ������;
        this.����ʱ�� = ����ʱ��;
        this.�Ƿ���Ե��� = �Ƿ���Ե���;
    }
}
