using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.InteropServices;
using UnityEditor;

public class basicMgr : BaseManager<basicMgr>
{
    private Color nowColor;
    public GameObject 对象;
    public bool isOver = false;
    Dictionary<string, AssetBundle> 加载集 = new Dictionary<string, AssetBundle>();
    private NameMgr nm = NameMgr.GetInstance();
    public DateTime 网络时间;
    public long 时间戳;




    public Dictionary<string, string> 返回空的战斗属性()
    {
        Dictionary<string, string> 属性 = new Dictionary<string, string>();
        属性.Add("攻击力", Xor("0"));
        属性.Add("防御力", Xor("0"));
        属性.Add("血量", Xor("0"));
        属性.Add("回血值", Xor("0"));
        属性.Add("攻击力加成", Xor("0"));
        属性.Add("防御力加成", Xor("0"));
        属性.Add("血量加成", Xor("0"));
        属性.Add("回血值加成", Xor("0"));
        属性.Add("移动速度", Xor("0"));
        属性.Add("攻击速度", Xor("0"));
        属性.Add("暴击率", Xor("0"));
        属性.Add("暴伤加成", Xor("0"));
        属性.Add("固定伤害", Xor("0"));
        属性.Add("固定减伤", Xor("0"));
        属性.Add("伤害加成", Xor("0"));
        属性.Add("伤害减免", Xor("0"));
        属性.Add("固定吸血", Xor("0"));
        属性.Add("吸血加成", Xor("0"));
        属性.Add("金钱加成", Xor("0"));
        属性.Add("经验加成", Xor("0"));
        属性.Add("稀有怪概率", Xor("0"));
        属性.Add("爆率", Xor("0"));

        return 属性;
    }





    public Color 转换颜色(int qua)
    {
        switch (qua)
        {
            case -1://纯黑
                ColorUtility.TryParseHtmlString("#000000", out nowColor);
                break;
            case 0://暖灰色
                ColorUtility.TryParseHtmlString("#9F7575", out nowColor);
                break;
            case 1://绿色32AD47
                ColorUtility.TryParseHtmlString("#008C18", out nowColor);
                break;
            case 2://蓝色
                ColorUtility.TryParseHtmlString("#1060F6", out nowColor);
                break;
            case 3://紫色
                ColorUtility.TryParseHtmlString("#FF40E8", out nowColor);
                break;
            case 4://金色
                ColorUtility.TryParseHtmlString("#FF7F00", out nowColor);
                break;
            case 5://红色
                ColorUtility.TryParseHtmlString("#FF4D40", out nowColor);
                break;
        }



        return nowColor;
    }


    public string 返回颜色代码(int qua)
    {
        string color = "";
        switch (qua)
        {
            case 0://暖灰色
                color = "#9F7575";
                break;
            case 1://绿色
                color = "#32AD47";
                break;
            case 2://蓝色
                color = "#1060F6";
                break;
            case 3://紫色
                color = "#FF40E8";
                break;
            case 4://金色
                color = "#FF7F00";
                break;
            case 5://红色
                color = "#FF4D40";
                break;
        }
        return color;
    }

    public Color 转换光球颜色(int qua, bool 是外环)
    {
        if (是外环)
        {
            switch (qua)
            {
                case 0://白色
                    nowColor = new Color(255f / 255f, 255 / 255f, 255 / 255f, 80 / 255f);
                    break;
                case 1://绿色
                    nowColor = new Color(134 / 255f, 224 / 255f, 35 / 255f, 80 / 255f);
                    break;
                case 2://蓝色
                    nowColor = new Color(42 / 255f, 210 / 255f, 217 / 255f, 80 / 255f);
                    break;
                case 3://紫色
                    nowColor = new Color(236 / 255f, 103 / 255f, 245 / 255f, 80 / 255f);
                    break;
                case 4://金色
                    nowColor = new Color(217 / 255f, 210 / 255f, 42 / 255f, 80 / 255f);
                    break;
                case 5://红色
                    nowColor = new Color(250 / 255f, 82 / 255f, 70 / 255f, 80 / 255f);
                    break;
            }
        }
        else
        {
            switch (qua)
            {
                case 0://白色
                    nowColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 150 / 255f);
                    break;
                case 1://绿色
                    nowColor = new Color(134 / 255f, 224 / 255f, 35 / 255f, 150 / 255f);
                    break;
                case 2://蓝色
                    nowColor = new Color(42 / 255f, 210 / 255f, 217 / 255f, 150 / 255f);
                    break;
                case 3://紫色
                    nowColor = new Color(236 / 255f, 103 / 255f, 245 / 255f, 150 / 255f);
                    break;
                case 4://金色
                    nowColor = new Color(217 / 255f, 210 / 255f, 42 / 255f, 150 / 255f);
                    break;
                case 5://红色
                    nowColor = new Color(255 / 255f, 106 / 255f, 96 / 255f, 120 / 255f);
                    break;
            }
        }
        return nowColor;
    }


    /// <summary>
    /// 给按钮自动绑定点击事件
    /// </summary>
    public void Banding(GameObject 按钮, UnityAction 事件)
    {
        按钮.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            事件();
        });
    }


    public Color 改变透明度(GameObject 对象, float 透明值)
    {
        Color 对象颜色;
        if (对象.GetComponent<Image>() == null)
            对象颜色 = 对象.GetComponent<Text>().color;
        else
            对象颜色 = 对象.GetComponent<Image>().color;
        nowColor = new Color(对象颜色.r, 对象颜色.g, 对象颜色.b, 透明值 / 255f);
        return nowColor;
    }

    /// <summary>
    /// ab包路径
    /// </summary>
    public string AppContentPath
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android)
                return Application.streamingAssetsPath + "/AssetsBundles/";
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                return Application.dataPath + "/Raw/AssetsBundles/";
            else if (Application.platform == RuntimePlatform.OSXPlayer)
                return Application.dataPath + "/Resources/Data/AssetsBundles/";
            else
                return Application.dataPath + "/StreamingAssets/AssetsBundles/";
        }
    }

    /// <summary>
    /// 加载AB包预设资源方法
    /// </summary>
    /// <param name="prefabsName">AB预设的名称</param>
    /// <returns>返回加载到的AB包资源预设物体</returns>
    public IEnumerator LoadABPrefabs(string prefabsName, UnityAction<GameObject, Dictionary<int, object>> action, Dictionary<int, object> 参数集)
    {
        UnityWebRequest request;
        string strABPath = AppContentPath + prefabsName;
        if (Application.platform == RuntimePlatform.Android)//是安卓
        {
            request = UnityWebRequestAssetBundle.GetAssetBundle(strABPath);
            yield return request.SendWebRequest();
            if (request.isHttpError)
            {
                Debug.LogError(GetType() + "/ERROR/" + request.error);
            }
            else
            {
                if (加载集.ContainsKey(prefabsName))//判断是不是第一次加载
                    对象 = 加载集[prefabsName].LoadAsset(prefabsName) as GameObject;
                else
                {
                    AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
                    对象 = ab.LoadAsset(prefabsName) as GameObject;
                    加载集.Add(prefabsName, ab);
                }
                action(对象, 参数集);
            }
        }
        else
        {//不是安卓
            if (加载集.ContainsKey(prefabsName))//判断是不是第一次加载
                对象 = 加载集[prefabsName].LoadAsset(prefabsName) as GameObject;
            else
            {
                AssetBundle bundle = AssetBundle.LoadFromFile(strABPath);
                对象 = bundle.LoadAsset(prefabsName) as GameObject;
                加载集.Add(prefabsName, bundle);
            }
            action(对象, 参数集);

        }
        if (!NameMgr.对象池.ContainsKey(对象.name))
        {
            NameMgr.对象池.Add(对象.name, 对象);
        }
        isOver = true;
    }

    public Vector3 GetScreenPoint(Vector3 v3)
    {
        Camera camera = NameMgr.Camera;
        return camera.WorldToScreenPoint(v3);
    }



    public Vector3 GetUIToWordPos(Vector3 uiObj)
    {
        Camera camera = NameMgr.Camera;
        Vector3 ptScreen = RectTransformUtility.WorldToScreenPoint(camera, uiObj);
        ptScreen.z = 0;
        ptScreen.z = Mathf.Abs(camera.transform.position.z - uiObj.z);
        Vector3 ptWorld = camera.ScreenToWorldPoint(ptScreen);
        return ptWorld;
    }


    /// <summary>
    /// 文件读取路径
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public string LoadFile(string filePath)
    {
        string url = Application.streamingAssetsPath + "/" + filePath;
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);

            request.SendWebRequest();//读取数据
            while (true)

            {

                if (request.downloadHandler.isDone)//是否读取完数据

                {

                    return request.downloadHandler.text;

                }

            }
        }
        else//目前只支持安卓和unity测试
            return File.ReadAllText(url);
    }



    //返回一个子sprite
    public  Sprite GetChildSprite(string name, int num)
    {

        string path = "图片" + "/" + name;//图片路径名称

        //UnityEngine.Object[] sprites = AssetDatabase.LoadAllAssetsAtPath(path);
        UnityEngine.Object[] sprites = Resources.LoadAll<Sprite>(path);


        Sprite output_sprite = (Sprite)sprites[num];

        Debug.Log(output_sprite.name);

        return output_sprite;
    }




    /// <summary>
    /// 携程模拟物体移动
    /// </summary>
    /// <param name="tr"></param>
    /// <param name="pos"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator MoveTo(Transform tr, Vector3 pos, float time)
    {
        float t = 0;
        Vector3 startPos = tr.position;
        while (true)
        {
            t += Time.deltaTime;
            float a = t / time;
            if (tr == null)
                yield break;
            tr.position = Vector3.Lerp(startPos, pos, a);
            if (a >= 1.0f)
                break;
            yield return null;
        }
        GameObject.Destroy(tr.gameObject);
    }


    public Vector3 世界坐标转屏幕坐标(Vector3 坐标)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(坐标);
        return screenPos;
    }

    public List<GameObject> 查找所有同名对象(string 名字)
    {
        List<GameObject> 对象组 = new List<GameObject>();
        while (GameObject.Find(名字))
        {
            GameObject 对象 = GameObject.Find(名字);
            对象.name = "加入对象组中";
            对象组.Add(对象);
        }
        return 对象组;
    }


    public string[] 字符串分割(string 字符串, char 分割符)
    {
        if (字符串 == null)
            return null;
        string[] 字符组 = 字符串.Split(分割符);
        if (字符组.Length == 2)
            return 字符组;
        else
            return null;

    }



    /// <summary>
    /// 加密/解密
    /// </summary>
    /// <returns></returns>
    public string Xor(string input)
    {
        string miyao = "rgbuerbiurbqdljgopwe";
        if (input != null)
        {
            char[] chars = input.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)(chars[i] ^ miyao[i]);
            }
            return new string(chars);
        }
        return "";
    }


    public float Xftof(int input)
    {
        return float.Parse(Xor(input + ""));
    }

    public int Xstoi(string input)
    {
        if (input == null || input.Equals(""))
        {
            Debug.Log("传入空数据");
            return 0;
        }
        int result;
        if (int.TryParse(Xor(input), out result))
        {
            return result;
        }
        else
        {
            float r2;
            if (float.TryParse(Xor(input), out r2))//判断是不是浮点数
            {
                return (int)r2;
            }
            else
            {
                int r3;
                if (int.TryParse(Xor(Xor(input)), out r3))//判断是不是传递来的是明文
                {
                    Debug.Log("注意!这个是明文!!->" + input);
                    return r3;
                }
                else
                {
                    Debug.Log("错误!错误!数据遭修改!!->" + input);
                    return 0;
                }
            }
        }
    }

    public float Xstof(string input)
    {
        if (input == null || input.Equals(""))
        {
            Debug.Log("传入空数据");
            return 0;
        }
        float result;
        if (float.TryParse(Xor(input), out result))
        {
            return result;
        }
        else
        {
            float result1;
            if (float.TryParse(Xor(Xor(input)), out result1))
            {
                Debug.Log("注意!这个是明文!!->" + input);
                return result1;
            }
            else
            {
                Debug.Log("错误!错误!数据遭修改!!->" + input);
                return 0;
            }
        }
    }

    public string Xitos(int input)
    {
        return Xor(input + "");
    }

    public string Xftos(float input)
    {
        return Xor(input + "");
    }


    public Equipment 装备加密(Equipment eq)
    {
        eq.qua = Xor(eq.qua);
        eq.xing = Xor(eq.xing);
        eq.lessgrade = Xor(eq.lessgrade);
        eq.price = Xstoi(eq.qua) < 4 ? Xitos(Xstoi(eq.lessgrade) * (Xstoi(eq.qua) + 1) * (Xstoi(eq.qua) + 1)) : Xitos(Xstoi(eq.lessgrade) * (Xstoi(eq.qua) + 1) * (Xstoi(eq.qua) + 1) * 5);
        eq.num = Xor(eq.num);
        eq.atk = Xor(eq.atk);
        eq.def = Xor(eq.def);
        eq.hp = Xor(eq.hp);
        eq.hpr = Xor(eq.hpr);
        eq.atk_p = Xor(eq.atk_p);
        eq.def_p = Xor(eq.def_p);
        eq.hp_p = Xor(eq.hp_p);
        eq.hpr_p = Xor(eq.hpr_p);
        eq.aspd = Xor(eq.aspd);
        eq.ms = Xor(eq.ms);
        eq.cri = Xor(eq.cri);
        eq.cri_d = Xor(eq.cri_d);
        eq.harm = Xor(eq.harm);
        eq.harm_d = Xor(eq.harm_d);
        eq.harm_p = Xor(eq.harm_p);
        eq.harm_p_d = Xor(eq.harm_p_d);
        eq.vam = Xor(eq.vam);
        eq.vam_p = Xor(eq.vam_p);
        eq.mon_p = Xor(eq.mon_p);
        eq.exp_p = Xor(eq.exp_p);
        return eq;
    }

    public Prop_bascis 道具加密(Prop_bascis pb)
    {
        pb.qua = Xor(pb.qua);
        pb.xing = Xor(pb.xing);
        pb.cd = Xor(pb.cd);
        pb.price = Xor(pb.price);
        pb.num = Xor(pb.num);
        return pb;
    }

    public SkillData 技能加密(SkillData sd)
    {
        sd.lessgrade = Xor(sd.lessgrade);
        sd.qua = Xor(sd.qua);
        sd.xing = Xor(sd.xing);
        sd.cd = Xor(sd.cd);
        sd.atknum = Xor(sd.atknum);
        return sd;
    }

    public Pet_Data 宠物加密(Pet_Data pd)
    {
        pd.grade = Xor(pd.grade);
        pd.xing = Xor(pd.xing);
        pd.qua = Xor(pd.qua);
        pd.cc = Xor(pd.cc);
        pd.max_cc = Xor(pd.max_cc);
        pd.ini_atk = Xor(pd.ini_atk);
        pd.ini_def = Xor(pd.ini_def);
        pd.ini_hp = Xor(pd.ini_hp);
        pd.ini_hpr = Xor(pd.ini_hpr);
        pd.ini_aspd = Xor(pd.ini_aspd);
        pd.ini_cri = Xor(pd.ini_cri);
        pd.ram_atk = Xor(pd.ram_atk);
        pd.ram_def = Xor(pd.ram_def);
        pd.ram_hp = Xor(pd.ram_hp);
        pd.ram_hpr = Xor(pd.ram_hpr);
        pd.qua_atk = Xor(pd.qua_atk);
        pd.qua_def = Xor(pd.qua_def);
        pd.qua_hp = Xor(pd.qua_hp);
        pd.qua_hpr = Xor(pd.qua_hpr);
        pd.qua_cri = Xor(pd.qua_cri);
        pd.harm = Xor(pd.harm);
        pd.harm_p = Xor(pd.harm_p);
        pd.vam = Xor(pd.vam);
        pd.vam_p = Xor(pd.vam_p);

        return pd;
    }


    public string 获取对象地址(object o) // 获取引用类型的内存地址方法    
    {
        GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

        IntPtr addr = GCHandle.ToIntPtr(h);

        return "0x" + addr.ToString("X");
    }


    public string 获取UID() // 唯一识别码(单一计算机)
    {
        return Guid.NewGuid().ToString();
    }

    #region 获取网络时间

    /// <summary>
    /// 获取当前网络时间
    /// </summary>
    /// <returns></returns>
    public IEnumerator GetWebTime()
    {
        // 获取时间地址
        string url = "https://www.baidu.com"; // 百度 //http://www.beijing-time.org/"; // 北京时间
        Debug.Log("开始获取服务器时间... 获取地址是: " + url);

        网络时间 = new DateTime(2000, 1, 1);
        时间戳 = 0;

        DateTime _webNowTime = DateTime.Now;
        // 获取时间
        UnityWebRequest WebRequest = new UnityWebRequest(url);

        // 等待请求完成
        yield return WebRequest.SendWebRequest();

        //网页加载完成  并且下载过程中没有错误   string.IsNullOrEmpty 判断字符串是否是null 或者是" ",如果是返回true
        //WebRequest.error  下载过程中如果出现下载错误  会返回错误信息 如果下载没有完成那么将会阻塞到下载完成
        if (WebRequest.isDone && string.IsNullOrEmpty(WebRequest.error))
        {
            // 将返回值存为字典
            Dictionary<string, string> resHeaders = WebRequest.GetResponseHeaders();
            string key = "DATE";
            string value = null;
            // 获取key为"DATE" 的 Value值
            if (resHeaders != null && resHeaders.ContainsKey(key))
            {
                resHeaders.TryGetValue(key, out value);
            }

            if (value == null)
            {
                Debug.LogError("没有获取到key为DATE对应的Value值...");
                yield break;
            }

            // 取到了value，则进行转换为本地时间
            _webNowTime = FormattingGMT(value);
            网络时间 = _webNowTime;
            Debug.Log(value + " ，转换后的网络时间：" + _webNowTime);

            // 转换成时间戳TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local)
            TimeSpan cha = (_webNowTime - TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local));
            long t = (long)cha.TotalSeconds;
            时间戳 = t;
            Debug.Log("网络时间转时间戳：" + t);
        }
    }

    /// <summary>
    /// GMT(格林威治时间)时间转成本地时间
    /// </summary>
    /// <param name="gmt">字符串形式的GMT时间</param>
    /// <returns></returns>
    private DateTime FormattingGMT(string gmt)
    {
        DateTime dt = DateTime.MinValue;
        try
        {
            string pattern = "";
            if (gmt.IndexOf("+0") != -1)
            {
                gmt = gmt.Replace("GMT", "");
                pattern = "ddd, dd MMM yyyy HH':'mm':'ss zzz";
            }

            if (gmt.ToUpper().IndexOf("GMT") != -1)
            {
                pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
            }

            if (pattern != "")
            {
                dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.AdjustToUniversal);
                dt = dt.ToLocalTime();
            }
            else
            {
                dt = Convert.ToDateTime(gmt);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.LogError("时间转换错误...");
        }
        return dt;
    }
    #endregion


}
