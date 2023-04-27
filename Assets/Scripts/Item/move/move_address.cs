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
        ScenceName = gameObject.transform.Find("Text").GetComponent<Text>().text;
        ut.跳转场景(ScenceName);
    }


   
}
