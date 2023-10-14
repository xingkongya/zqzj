using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag_PropOption : MonoBehaviour
{
    private RectTransform rt;
    private PropMgr pm;
    private io io_;
    public string 名字;
    public string UID;
    public string 地址;
    private void Awake()
    {
        //rt = gameObject.GetComponent<RectTransform>();
        pm = PropMgr.GetInstance();
        io_ = io.GetInstance();
        //basicMgr.GetInstance().Banding(gameObject, 点击物品项);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (rt.anchoredPosition.x != 0)
            rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y);*/
    }

    public void 点击物品项()
    {
        Color nowColor;
        if (GameObject.FindGameObjectsWithTag("未选中").Length > 0)
        {
            GameObject[] 未选中栏 = GameObject.FindGameObjectsWithTag("未选中");
            foreach (GameObject 道具项 in 未选中栏)
            {
                ColorUtility.TryParseHtmlString("#FFFFFF", out nowColor);
                道具项.GetComponent<Image>().color = nowColor;
            }
        }
        
              
       
        //点击变色
        ColorUtility.TryParseHtmlString("#DBCDCD", out nowColor);
        gameObject.GetComponent<Image>().color = nowColor;

    }

    public void 点击锁() {
        role_Data myData = io_.load();
        Image 锁图标 = gameObject.transform.Find("锁").GetComponent<Image>();
        if (PropMgr.装备表.ContainsKey(名字))
        {
            if (myData.装备背包[UID].islock.Equals("0"))
            {
                myData.装备背包[UID].islock = "1";
                锁图标.sprite = Resources.Load("图标/关锁图标", typeof(Sprite)) as Sprite;
            }
            else
            {
                myData.装备背包[UID].islock = "0";
                锁图标.sprite = Resources.Load("图标/开锁图标", typeof(Sprite)) as Sprite;
            }
        }
        else {
            if (myData.材料背包[名字].islock.Equals("0"))
            {
                myData.材料背包[名字].islock = "1";
                锁图标.sprite = Resources.Load("图标/关锁图标", typeof(Sprite)) as Sprite;
            }
            else
            {
                myData.材料背包[名字].islock = "0";
                锁图标.sprite = Resources.Load("图标/开锁图标", typeof(Sprite)) as Sprite;
            }
        }
        io_.save(myData);
    }
}
