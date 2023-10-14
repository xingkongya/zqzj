using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuXi : MonoBehaviour
{
    public float speed = 0.03f;
    public float time = 0.6f;
    private bool index = true;
    private float T_time;

    // Start is called before the first frame update
    void Start()
    {
        T_time = time;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (index)
        {
            transform.localScale -= new Vector3(speed*2f * Time.deltaTime, speed * Time.deltaTime);
            if (T_time < 0) {
                index = false;
                T_time = time;
            }
        }
        else {
            transform.localScale += new Vector3(speed * 2f * Time.deltaTime, speed * Time.deltaTime);
            if (T_time < 0)
            {
                index = true;
                T_time = time;
            }
        }

        T_time -= Time.deltaTime;
    }
}
