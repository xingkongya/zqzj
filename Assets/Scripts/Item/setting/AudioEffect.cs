using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    private io io_;

    private void Awake()
    {
        io_ = io.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        role_Data myData = io_.load();
        AudioSource 音频源 = gameObject.GetComponent<AudioSource>();
        if (myData.记录.ContainsKey("游戏音效"))
        {
            音频源.volume = int.Parse(myData.记录["游戏音效"]) / 100f;
        }
        else
        {
            音频源.mute = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
