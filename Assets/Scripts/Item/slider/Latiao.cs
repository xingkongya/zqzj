using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Latiao : MonoBehaviour
{
    public string ��Ʒ����;
    public string UID;
    public int ����;
    public int �������;
    public string ����;
    public string ����;
    private GameObject ͼ��;
    private Slider ����;
    private Text �����ı�;
    private Text �����ı�;
    private GameObject ȷ����ť;
    private GameObject ȡ����ť;
    private GameObject ��һ��ť;
    private GameObject ��һ��ť;
    private GameObject ������ť;
    private G_Util gut;
    private PropMgr pm;
    private basicMgr bm;


    private void Awake()
    {
        ͼ�� = gameObject.transform.Find("Panel/ͼ��").gameObject;
        ���� = gameObject.transform.Find("Slider").GetComponent<Slider>();
        �����ı� = gameObject.transform.Find("����").GetComponent<Text>();
        �����ı� = gameObject.transform.Find("����").GetComponent<Text>();
        ȷ����ť = gameObject.transform.Find("button/ȷ��").gameObject;
        ȡ����ť = gameObject.transform.Find("button/ȡ��").gameObject;
        ��һ��ť = gameObject.transform.Find("button/��һ").gameObject;
        ��һ��ť = gameObject.transform.Find("button/��һ").gameObject;
        ������ť = gameObject.transform.Find("button/����").gameObject;
        gut = NameMgr.����.GetComponent<G_Util>();
        pm = PropMgr.GetInstance();
        bm = basicMgr.GetInstance();
    }


    // Start is called before the first frame update
    void Start()
    {
        //��������
        �����ı�.text = ��Ʒ����;
        �����ı�.color = bm.ת����ɫ(bm.Xstoi( pm.������Ʒ(��Ʒ����).qua));

        //����ͼ��
        Prop_bascis ��Ʒ = pm.������Ʒ(��Ʒ����);
        if (��Ʒ!=null&&!��Ʒ.name.Equals(""))
        {
            gut.��ʼ��ͼ��(ͼ��.GetComponent<Image>(), ��Ʒ);
        }

        //�����������
        ����.maxValue = �������;

        //���¼�
        bm.Banding(ȡ����ť, ȡ��);
        bm.Banding(ȷ����ť, ȷ��);
        bm.Banding(��һ��ť, ���ּ�һ);
        bm.Banding(��һ��ť, ���ּ�һ);
        bm.Banding(������ť, ���ּ���);

        //��ʼ��
        //��ʼ�������ı���ɫ
        if (����.Equals("����") || ����.Equals("ʹ�õ���"))
        {
            �����ı�.color = bm.ת����ɫ(5);
        }
        else if (����.Equals("����"))
        {
            �����ı�.color = bm.ת����ɫ(1);
        }
        //��ʼ��valueֵΪ1
        ����.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ȷ��() {
        ���� = gameObject.transform.Find("Slider").GetComponent<Slider>();
        int ��ǰ���� = (int)����.value;
        //����
        if (����.Equals("����")) {
            string ����ֵ = pm.ʧȥ��Ǯ(����, ��ǰ���� * ����);
            if (����ֵ.Equals("�ɹ�"))
            {
                pm.��ȡ��Ʒ(��Ʒ����, ��ǰ����);
            }
            else {
                gut.���ɾ����(����ֵ);
                return;
            }
            gut.���ɾ����("����ɹ�!");
            ȡ��();
        }
        //����
        else if (����.Equals("����")) {
            string ����ֵ;
            if (PropMgr.���ϱ�.ContainsKey(��Ʒ����))
            {
                ����ֵ = pm.ʧȥ��Ʒ(��Ʒ����, ��ǰ����);
            }
            else {
                ����ֵ = pm.ʧȥװ��(UID);
            }
            if (����ֵ.Equals("�ɹ�"))
            {
                Dictionary<string, int> ��Ǯ = new Dictionary<string, int>();
                ��Ǯ.Add(����, ���� * ��ǰ����);
                gut.�ӽ�Ǯ(��Ǯ);
                gut.���ɻ�ÿ�("ͭ��", ���� * ��ǰ����);
            }
            else
            {
                gut.���ɾ����(����ֵ);
                return;
            }
            gut.���ɾ����("���۳ɹ�!");
            ȡ��();
            if (DataMgr.GetInstance().���ض���.ContainsKey("����")) {
                DataMgr.GetInstance().���ض���["����"].GetComponent<Bag>().��ʼ������();
            }
        }
        //ʹ�õ���
        else if (����.Equals("ʹ�õ���")) {
            ȡ��();
            string ����״̬ = gut.ʹ�õ���(��Ʒ����, ��ǰ����);
                if (!����״̬.Equals("ʹ�óɹ�"))
                {
                    gut.���ɾ����(����״̬);
                    return;
                }
                else
                {
                    gut.���ɾ����("ʹ�óɹ�");
                    return;
                }
          
        }
    }


    public void ʵʱ��������() {
        if (����!=null&&gameObject.activeSelf) {

            gameObject.transform.Find("����").GetComponent<Text>().text = "("+����.value + "/"+����.maxValue+")";
            if (����.Equals("����"))
            {
                �����ı�.text = "-" + ���� * (int)����.value + ����;
            }
            else if(����.Equals("����"))
            {
                �����ı�.text = "+" + ���� * (int)����.value + ����;
            }
            else if (����.Equals("ʹ�õ���"))
            {
                �����ı�.text = "ʹ��"+��Ʒ����+"��" +  (int)����.value;
            }
        }
    }


    public void ���ּ�һ() {
        if (����.value < ����.maxValue)
        {
            ����.value++;
        }
    }

    public void ���ּ�һ()
    {
        if (����.value >= 1)
        {
            ����.value--;
        }
    }

    public void ���ּ���()
    {
        ����.value=����.maxValue;
    }

    public void ȡ��() {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
