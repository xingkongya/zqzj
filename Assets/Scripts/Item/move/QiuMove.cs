using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiuMove : MonoBehaviour
{
    float timer = 0.3f;
    Transform bagTf;
    // Start is called before the first frame update
    void Start()
    {
        bagTf = GameObject.Find("bag_bg_pic").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) { 
        gameObject.transform.position=  Vector3.MoveTowards(transform.position, bagTf.position, 10);//移动动画模拟   
        }

        if (gameObject.transform.position == bagTf.position||timer<-1)
            Destroy(gameObject);
    }
}
