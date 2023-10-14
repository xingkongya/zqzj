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
        gut = NameMgr.����.GetComponent<G_Util>();
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

    public void ����ǿ������() {
        gut.����ǿ������(Key);
    }


    public void ����orж��() {
        role_Data myData = IO.load();
        //�ж�key�Ǵ���װ������ж��װ��,�����Ǵ���װ��
        if (!pm.�����Ա�.ContainsKey(Key))
        {
            Equipment װ�� = myData.װ������[Key];

            if (bm.Xstoi(myData.�ȼ�) < bm.Xstoi(װ��.lessgrade))
            {
                gut.���ɾ����("�ȼ�����");
                return;
            }

            //����д��-װ��
            if (myData.װ����[װ��.place] != null)//�ж�ԭ��λ���Ƿ���װ��,����ȡ��
                pm.��ȡ��Ʒ(myData.װ����[װ��.place]);
            myData = IO.load();//��������
            myData.װ����[װ��.place] = װ��;
            IO.save(myData);
            pm.ʧȥװ��(Key);

            gut.���ɾ����("ʹ�óɹ�");
            if (NameMgr.���� != null)
                NameMgr.����.GetComponent<Bag>().��ʼ������();
        }
        //ж��
        else {
            //����д��-װ��
            pm.��ȡ��Ʒ(myData.װ����[Key]);
            myData = IO.load();//��������
            myData.װ����[Key] = null;
            IO.save(myData);

            //�ı�UI
            if (dm.���ض���.ContainsKey("�������")&& dm.���ض���["�������"]!=null) {
                dm.���ض���["�������"].GetComponent<Role_Panel>().ini_panel();
                gut.����װ������������();

                //ˢ������
                gut.�������ˢ��(dm.���ض���["�������"]);
            }
        }
       
        if (!NameMgr.����.CompareTag("�ý�"))
            DataMgr.GetInstance().���ض���["����"].GetComponent<combat>().��������ˢ��();
        

        Destroy(gameObject);
    }

}
