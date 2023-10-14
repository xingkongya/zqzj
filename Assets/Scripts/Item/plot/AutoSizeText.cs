using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoSizeText : MonoBehaviour
{
    //Text最大宽度
    public float maxWidth = 700;
    private Text text;//Text和TMP都可以
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
    /// 文本自适应，Text锚点设置为拉伸
    /// </summary>
    /// <param name="text">文本(Text和TMP都可以)</param>
    /// <param name="parent">文本父物体</param>
    /// <param name="maxWith">最大宽度(超过这个值扩展高度)</param>
    public void TextAreaChange()
    {
        RectTransform rect = text.rectTransform;
        // 获取Text的Size
        Vector2 oldSize = rect.rect.size;
        Vector2 parentSize = rectTransform.rect.size;

        //Text的宽度
        float width = text.preferredWidth < maxWidth ? text.preferredWidth : maxWidth;

        //父物体的宽度
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parentSize.x + width - oldSize.x);

        //必须先设置宽再设置高
        float height = text.preferredHeight;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentSize.y + height - oldSize.y);
    }
}