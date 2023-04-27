using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DazaoItem : MonoBehaviour
{
    private RectTransform rt;
    private PropMgr pm;
    public Dictionary<string, int> 需求材料=new Dictionary<string, int>();


    private void Awake()
    {
        pm = PropMgr.GetInstance();
        rt = gameObject.GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        刷新打造状态();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rt.anchoredPosition.x != 0)
            rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y);
    }

    public void 删除自己() {
        Destroy(gameObject);

    }

    public void 刷新打造状态()
    {
        if (!pm.检测物品是否满足(需求材料))
        {
            Color nowColor;
            ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
            GameObject 打造按钮 = gameObject.transform.Find("button/打造").gameObject;
            打造按钮.GetComponent<Image>().color = nowColor;
            nowColor = basicMgr.GetInstance().改变透明度(打造按钮, 80.0f);
            打造按钮.GetComponent<Image>().color = nowColor;
        }
    }


}
