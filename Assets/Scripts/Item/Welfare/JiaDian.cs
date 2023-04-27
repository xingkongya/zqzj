using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiaDian : MonoBehaviour
{

    public G_Util gut;



    private void Awake()
    {
        gut = NameMgr.画布.GetComponent<G_Util>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void 删除自己()
    {
        gut.删除对象(gameObject);
    }
}
