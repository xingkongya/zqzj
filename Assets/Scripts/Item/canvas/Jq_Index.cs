using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jq_Index : MonoBehaviour
{
    private G_Util gut;

    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
    }

    // Start is called before the first frame update
    void Start()
    {
        int index = PlayerPrefs.GetInt("剧情");
        if (index != -1)
            gut.生成剧情画布();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
