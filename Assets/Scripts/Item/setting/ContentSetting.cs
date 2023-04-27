using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.childCount == 1)
            gameObject.GetComponent<RectTransform>().sizeDelta = gameObject.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
