using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesAllChild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        for (int i=0;i<gameObject.transform.childCount;i++) {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
