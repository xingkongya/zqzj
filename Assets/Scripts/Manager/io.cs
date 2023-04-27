using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class io : BaseManager<io>
{

    public string DataPath
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android)
                return Application.persistentDataPath + @"/MyData.json";//"/storage/emulated/0/cxData.json";//移动端根目录,删除了app也能找得到数据
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                return Application.dataPath + "/Raw/AssetsBundles/";
            else if (Application.platform == RuntimePlatform.OSXPlayer)
                return Application.dataPath + "/Resources/Data/AssetsBundles/";
            else
                return Application.persistentDataPath + @"/MyData.json";//打包后Resources文件夹不能存储文件，Application.persistentDataPath为移动端可读写目录
        }
    }


    /// <summary>
    /// /存储处理好的数据
    /// </summary>
    /// <param name="myData"></param>
    /// <returns></returns>
    public bool save(role_Data myData) {
        StreamWriter sw=null;
        try
        {
            //找到当前路径
            FileInfo file = new FileInfo(DataPath);
            //判断有没有文件，有则打开文件，，没有创建后打开文件
            sw = file.CreateText();
            //ToJson接口将你的列表类传进去，，并自动转换为string类型
            string json = JsonMapper.ToJson(myData);
            //将转换好的字符串存进文件，
            sw.WriteLine(json);
            return true;
        }
        //存储异常
        catch (Exception e){
            Debug.Log("存储失败!"+e);
            return false;
        }
        finally {
            //注意释放资源
            sw.Close();
            sw.Dispose();

        }
    
    }




    /// <summary>
    /// 读取存档
    /// </summary>
    /// <returns></returns>
    public role_Data load()
    {
        role_Data myData=new role_Data();
        StreamReader streamReader=null;
        try
        {
            streamReader = new StreamReader(DataPath);
            string str = streamReader.ReadToEnd();
            myData = JsonMapper.ToObject<role_Data>(str);
            return myData;
        }
        //读取异常
        catch
        {
            Debug.Log("读取失败!");
            return null;
        }
        finally
        {
            streamReader.Close();
            streamReader.Dispose();
        }
    }


    }
