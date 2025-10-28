using System;
using System.Collections.Generic;

public class FileDataManger
{ 
    public string appFileString;//app程序包总字符串
    public string appStartAddress;//app起始地址
    public string appLength;//app长度
    public List<string> appDataField;//app数据段字符串 每个元素代表一行数据

    public string bootFileString;


    public FileDataManger()
    {
        appFileString = string.Empty;
        bootFileString = string.Empty;
        appDataField = new List<string>();
    }

    public void ParseAppFileData(string appString)
    {
        appFileString = appString;

        //解析app软件包数据
        string[] sArray = appFileString.Split("\r\n");//按行分割数据
        foreach (var item in sArray)
        {
            appDataField.Add(item);
        }

        foreach (var item in appDataField)
        {
            if (item == null) continue;

            //去除非S3字段的数据
            if (!item.Contains("S325"))
            { 
                appDataField.Remove(item);
            }
            //type count address  data                                                             checksum
            //S3   25    A0038000 00C0038059B37CFA20800380F8130880960A41C969F5BE36A954E51356AB1AEC 44
            //仅取data字段
            item.Remove(1,12);//去除type count address字段
            //item.Remove(64,2);//去除checksum字段
        }


    }
}

