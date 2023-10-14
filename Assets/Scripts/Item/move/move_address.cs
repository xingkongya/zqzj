using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class move_address : MonoBehaviour
{
    //引用
    private string ScenceName;
    private io io_;
    

    //自定义
    G_Util ut;

    private void Awake()
    {
        io_ = io.GetInstance();
    }

    private void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        ut = NameMgr.画布.GetComponent<G_Util>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 移动() {
        GameObject 地图 = DataMgr.GetInstance().本地对象["UI"].transform.Find("战斗页").transform.Find("地图").gameObject;
        if (地图.activeSelf)
            地图.SetActive(false);
        ScenceName = gameObject.transform.Find("Text").GetComponent<Text>().text;
        if (AdressMgr.GetInstance().检测战斗力是否达标(ScenceName))
        {
            ut.跳转场景(ScenceName);
        }
        else {
            ut.生成对话框("战斗力最低需要" + AdressMgr.地图表[ScenceName] + "点才能进入该地图!", 0, 0, "进图失败");

        }

    }


    private void OnDisable()
    {
        if (!(gameObject.name.Equals("up") || gameObject.name.Equals("down") || gameObject.name.Equals("left") || gameObject.name.Equals("right"))) {
            Destroy(gameObject);
        }
    }

}
