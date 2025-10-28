using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

public class FileDataManger
{
    public string appFileString;//app程序包总字符串
    public string appStartAddress;//app起始地址
    public string appLength;//app长度
    public List<string> appDataField;//app数据段字符串 每个元素代表一行数据

    public string bootFileString;//boot程序包总字符串
    public string bootStartAddress;//boot起始地址
    public string bootLength;//boot长度


    public FileDataManger()
    {
        appFileString = string.Empty;
        appStartAddress = string.Empty;
        appLength = string.Empty;
        bootFileString = string.Empty;
        bootStartAddress = string.Empty;
        bootLength = string.Empty;
        appDataField = new List<string>();
    }

    /// <summary>
    /// 解析并记录APP信息
    /// </summary>
    /// <param name="appString">APP S19/Hex信息</param>
    public void ParseAppFileData(string appString)
    {
        appFileString = appString;

        //解析app软件包数据
        string[] sArray = appFileString.Split("\r\n");//按行分割数据

        //获取App起始地址及长度
        appStartAddress = sArray[0].Substring(4, 8);
        char[] lenStr = { sArray[1][66], sArray[1][67],
                       sArray[1][64], sArray[1][65],
                       sArray[1][62], sArray[1][63],
                       sArray[1][60], sArray[1][61],
                       };
        appLength = new string(lenStr);

        //获取App S3 数据段数据
        foreach (var item in sArray)
        {
            //仅保留S3字段的数据
            if (item.Contains("S325"))
            {
                //type count address  data                                                             checksum
                //S3   25    A0038000 00C0038059B37CFA20800380F8130880960A41C969F5BE36A954E51356AB1AEC 44
                //仅取data字段
                string dataStr = item.Remove(0, 12);
                dataStr = dataStr.Remove(64, 2);
                appDataField.Add(dataStr);
            }
        }

    }

    /// <summary>
    /// 解析并记录BOOT信息
    /// </summary>
    /// <param name="bootString">APP S19/Hex信息</param>
    public void ParseBootFileData(string bootString)
    {
        bootFileString = bootString;

        //解析boot软件包数据
        string[] sArray = bootFileString.Split("\r\n");//按行分割数据

        //获取App起始地址及长度
        bootStartAddress = sArray[0].Substring(4, 8);
        bootLength = "";
    }


    [DllImport("CrcAndHashLibDll.dll")]
    extern unsafe static uint GetCrc32(char* buffer, uint len);
    [DllImport("CrcAndHashLibDll.dll")]
    extern unsafe static char* Add(char* a, char* b);

    /// <summary>
    /// 将导入的APP和BOOT合成一体包
    /// </summary>
    public void IntegratedPkg()
    {
        if (appFileString == string.Empty || bootFileString == string.Empty)
        {
            MessageBox.Show("APP/BOOT程序包加载异常,请先加载程序");
            return;
        }

        //typedef struct AppValidityInfo
        //{
        //    u32 selfCrc;            /* Self calibration of structure */
        //    u32 flag;               /* Structure flag */
        //    u32 selfSize;           /* Structure size */
        //    u32 validFlag;          /* 0x32F1D9CC: valid APP ,  other：invalid APP */
        //    u32 AppCrc;
        //    u08 AppHash[32];        /* Record APP hash value */
        //    u08 reserved[12];       /* Reserved (supplier available) */
        //}AppValidityInfo;

        //设置APP验证信息
        string AppValidityInfo = "";
        //在传递字符串时，将字符所在的内存固化，
        //并取出字符数组的指针
        unsafe
        {
            //在传递字符串时，将字符所在的内存固化，

            //并取出字符数组的指针

            char a = '0';
            char b = '1';
            char *tmp = Add(&a, &b);
            int num = (int)*tmp;

            MessageBox.Show(num.ToString());





        }
    }

}

