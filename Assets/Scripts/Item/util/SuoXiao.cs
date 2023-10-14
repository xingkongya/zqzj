using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuoXiao : MonoBehaviour
{

    public float speed = 1.2f;
    public float time = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameObject.CompareTag("boss"))
            speed = 2.4f;
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            transform.localScale -= new Vector3(speed * Time.deltaTime, speed * Time.deltaTime);
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(1,1,1);
                time = 1f;
            }
        }
    }
}
