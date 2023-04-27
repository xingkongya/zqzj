using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaoLuoItem : MonoBehaviour
{
    public int index;
    public int all;
    private RectTransform rt;

    private void Awake()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (rt.anchoredPosition.x != 0)
        {
            if (all <= 3)
                rt.anchoredPosition = new Vector2((index % 3) * 150 - 150, 0);
            else if (all <= 9)
            {
                if (all % 3 == 0 && (all / 3) % 2 != 0)
                    rt.anchoredPosition = new Vector2((index % 3) * 150 - 150, (index / 3) * (-120) + 120);
                else
                    rt.anchoredPosition = new Vector2((index % 3) * 150 - 150, (index / 3) * (-120) + 60);
            }
            else {

                rt.anchoredPosition = new Vector2((index % 3) * 150 - 150, (index / 3) * (-120) + 180);
            }
        }

    }


    // Update is called once per frame
    void Update()
    {

    }
}
