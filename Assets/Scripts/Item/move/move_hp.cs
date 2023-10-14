using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move_hp : MonoBehaviour
{
    private float timer =0;
    private Color nowColor;
    float 速度 = 16;


    void Start()
    {
        Text 文本 = gameObject.transform.GetComponent<Text>();
        if (gameObject.name.Equals("回血"))
        {
            ColorUtility.TryParseHtmlString("#3A8E3B", out nowColor);
            string 回血字 = 文本.text;
            文本.text = "+" + 回血字;
        }
        else if (gameObject.name.Equals("宠物"))
        {
            ColorUtility.TryParseHtmlString("#FA10DA", out nowColor);
              string 扣血字 = 文本.text;
            文本.text = "-" + 扣血字;
        }
        else if (gameObject.name.Equals("暴击"))
        {
            ColorUtility.TryParseHtmlString("#FFEA09", out nowColor);
            文本.fontSize = 35;
            GameObject 伤害图标 = gameObject.transform.Find("Image").gameObject;
            伤害图标.gameObject.SetActive(true);
            Sprite 暴击图标 = Resources.Load<Sprite>("图标/暴击2图标");
            伤害图标.GetComponent<Image>().sprite = 暴击图标;
            ColorUtility.TryParseHtmlString("#FA7C10", out nowColor);
            伤害图标.GetComponent<Image>().color = nowColor;
            string 扣血字 = 文本.text;
            文本.text = "-" + 扣血字;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#FA0F0F", out nowColor);
            string 扣血字 = 文本.text;
            文本.text = "-" + 扣血字;
        }



        文本.color = nowColor;
        //1秒后销毁
            Destroy(this.gameObject, 0.6f);

    }

    // Update is called once per frame
    void Update()
    {
        //变大效果
        gameObject.transform.localScale += new Vector3(Time.deltaTime * 1.5f, Time.deltaTime * 1.5f, 0);

        //移动效果
        timer += Time.deltaTime;
        if (timer < 0.5f)
        {
            if (gameObject.name.Equals("扣血"))
                transform.Translate(Vector3.right * Time.deltaTime * 5f);
            else if (gameObject.name.Equals("回血"))
                transform.Translate(Vector3.right * Time.deltaTime * 20f);
            else if (gameObject.name.Equals("暴击"))
                transform.Translate(Vector3.left * Time.deltaTime * 15f);
            else if (gameObject.name.Equals("技能"))
                transform.Translate(Vector3.left * Time.deltaTime * 5f);
            else
                transform.Translate(Vector3.right * Time.deltaTime * 5f);


        }
        if (timer > 0.3f)
        {
            if (gameObject.name.Equals("暴击"))
                速度 = 40f;
            else if (gameObject.name.Equals("技能"))
                速度 = 40f;
            if (gameObject.transform.parent.CompareTag("人物"))
                transform.Translate(Vector3.down * Time.deltaTime * 速度);
            else
                transform.Translate(Vector3.up * Time.deltaTime * 速度);
        }



    }





    private void OnDisable()
    {
        Destroy(gameObject);
    }

}
