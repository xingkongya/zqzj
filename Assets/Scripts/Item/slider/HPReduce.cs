using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPReduce : MonoBehaviour
{
    private int MaxVal;
    private int Val;
    private RectTransform Fill;
    private RectTransform BgFill;
    private GameObject boss图片;
    private int boss血量=0;

    private void Awake()
    {
        MaxVal = (int)gameObject.GetComponent<Slider>().maxValue;
        Val = (int)gameObject.GetComponent<Slider>().value;
        Fill = gameObject.transform.Find("边框遮罩/Fill Area/Fill").gameObject.GetComponent<RectTransform>();
        BgFill = gameObject.transform.Find("边框遮罩/Fill Area/Fill_BG").gameObject.GetComponent<RectTransform>();
        if(gameObject.CompareTag("boss"))
            boss图片 = gameObject.transform.Find("图片").gameObject;

    }

    // Start is called before the first frame update
    void Start()
    {
        Fill.SetAsLastSibling();//血条颜色在最下层
        BgFill.anchorMax = Fill.anchorMax;//同步渐隐层数据
    }

    public IEnumerator ReduceHPEffect() {
        BgFill.anchorMax = Fill.anchorMax;//同步渐隐层数据
        yield return new WaitForSeconds(0.3f);
        if (BgFill == null || Fill == null)
            yield break;
        BgFill.anchorMax = Fill.anchorMax;//同步渐隐层数据
    }


    public void AddHPEffect() {
        BgFill.anchorMax = Fill.anchorMax;//同步渐隐层数据
    }

    public void Boss血条数值改变() {
        int nowHp=(int)gameObject.GetComponent<Slider>().value;
        if(gameObject.activeSelf&& nowHp<boss血量)
            StartCoroutine(图片变暗());
        gameObject.transform.Find("Text").GetComponent<Text>().text = gameObject.GetComponent<Slider>().value+"";
        boss血量 = nowHp;
    }

    public IEnumerator 图片变暗() {
        Color nowColor;
        ColorUtility.TryParseHtmlString("#DBDBDB", out nowColor);//暗色
        boss图片.GetComponent<Image>().color = nowColor;
        yield return new WaitForSeconds(0.1f);
        ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);//暗色
        boss图片.GetComponent<Image>().color = nowColor;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
