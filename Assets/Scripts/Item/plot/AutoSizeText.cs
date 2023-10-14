using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSizeText : MonoBehaviour
{
    //Text�����
    public float maxWidth = 700;
    private Text text;//Text��TMP������
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponentInChildren<Text>();
        text.text = "";
    }

    void Update()
    {
        TextAreaChange();
    }

    /// <summary>
    /// �ı�����Ӧ��Textê������Ϊ����
    /// </summary>
    /// <param name="text">�ı�(Text��TMP������)</param>
    /// <param name="parent">�ı�������</param>
    /// <param name="maxWith">�����(�������ֵ��չ�߶�)</param>
    public void TextAreaChange()
    {
        RectTransform rect = text.rectTransform;
        // ��ȡText��Size
        Vector2 oldSize = rect.rect.size;
        Vector2 parentSize = rectTransform.rect.size;

        //Text�Ŀ��
        float width = text.preferredWidth < maxWidth ? text.preferredWidth : maxWidth;

        //������Ŀ��
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parentSize.x + width - oldSize.x);

        //���������ÿ������ø�
        float height = text.preferredHeight;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentSize.y + height - oldSize.y);
    }
}