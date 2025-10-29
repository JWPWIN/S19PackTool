using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

public class FileDataManger
{
    public string appFileString;//app程序包总字符串
    public string appStartAddress;//app起始地址
    public string appLength;//app长度
    public List<string> appDataField;//app数据段字符串 每个元素代表一行数据
    public string appDataFieldStr;//app数据段总字符串
    public byte[] appDataHex;//app数据段Hex格式数据

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
        appDataFieldStr = string.Empty;
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
                dataStr.Replace("\r\n", "");//去除换行符
                appDataFieldStr += dataStr;
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
    extern unsafe static uint GetCrc32(byte* buffer, uint len);
    [DllImport("CrcAndHashLibDll.dll")]
    extern unsafe static byte* Add(byte* a, byte* b);
    [DllImport("CrcAndHashLibDll.dll")]
    extern unsafe static void Sha256_Init();
    [DllImport("CrcAndHashLibDll.dll")]
    extern unsafe static void Sha256_PushData(byte* pData, uint ulDataLen);
    [DllImport("CrcAndHashLibDll.dll")]
    extern unsafe static byte* Sha256_GetShaValue();


    /// <summary>
    /// 将导入的APP和BOOT合成一体包
    /// </summary>
    public void IntegratedPkg()
    {
        if (appFileString == string.Empty || bootFileString == string.Empty)
        {
            //MessageBox.Show("APP/BOOT程序包加载异常,请先加载程序");
            //return;
        }

        //typedef struct AppValidityInfo
        //{
        //    u32 selfCrc;            /* Self calibration of structure */
        //    u32 flag;--               /* Structure flag */
        //    u32 selfSize;--           /* Structure size */
        //    u32 validFlag;--          /* 0x32F1D9CC: valid APP ,  other：invalid APP */
        //    u32 AppCrc;
        //    u08 AppHash[32];--        /* Record APP hash value */
        //    u08 reserved[12];--       /* Reserved (supplier available) */
        //}AppValidityInfo;

        //设置APP验证信息
        uint flag = 0xE92CD298;
        uint selfSize = 0x40;
        uint validFlag = 0x32F1D9CC;
        byte[] reserved = new byte[12];

        //S3数据段字符串转换为Hex数据
        appDataHex = HexStringToByteArray(appDataFieldStr);

        //计算App数据的CRC和Hash
        uint AppCrc;
        byte[] AppHash = new byte[32];
        unsafe
        {
            //计算APP的CRC值,AppCrc 只要计算APP-S3段的数据域校验
            fixed (byte* pCSArray = &appDataHex[0])
            {
                AppCrc = GetCrc32(pCSArray, (uint)appDataHex.Count());
            }

            //计算APP的Hash值,AppHash[32] 计算（ APP起始地址4Byte+APP长度4Byte）+ APP-S3段的数据域
            string hashStr = appStartAddress + appLength + appDataFieldStr;
            byte[] hashHexArr= HexStringToByteArray(hashStr);
            fixed (byte* pCSArray = &hashHexArr[0])
            {
                Sha256_Init();
                Sha256_PushData(pCSArray, (uint)hashHexArr.Count());
                byte* hashData = Sha256_GetShaValue();

                for (uint i = 0; i < 32; i++)
                {
                    AppHash[i] = hashData[i];
                }
            }
        }

        //typedef struct AppValidityInfo
        //{
        //    u32 selfCrc;            /* Self calibration of structure */
        //    u32 flag;--               /* Structure flag */
        //    u32 selfSize;--           /* Structure size */
        //    u32 validFlag;--          /* 0x32F1D9CC: valid APP ,  other：invalid APP */
        //    u32 AppCrc;--
        //    u08 AppHash[32];--        /* Record APP hash value */
        //    u08 reserved[12];--       /* Reserved (supplier available) */
        //}AppValidityInfo;

        //计算selfCrc
        byte[] selfCrcCheckData = { (byte)(flag&0xFF),(byte)(flag>>8&0xFF),(byte)(flag>>16&0xFF),(byte)(flag>>24&0xFF),
                                     0,0,0,(byte)(selfSize),
                                    (byte)(validFlag&0xFF),(byte)(validFlag>>8&0xFF),(byte)(validFlag>>16&0xFF),(byte)(validFlag>>24&0xFF),
                                    (byte)(AppCrc&0xFF),(byte)(AppCrc>>8&0xFF),(byte)(AppCrc>>16&0xFF),(byte)(AppCrc>>24&0xFF),
                                    AppHash[0], AppHash[1], AppHash[2], AppHash[3], AppHash[4], AppHash[5], AppHash[6], AppHash[7], 
                                    AppHash[8], AppHash[9], AppHash[10], AppHash[11], AppHash[12], AppHash[13], AppHash[14], AppHash[15],
                                    AppHash[16], AppHash[17], AppHash[18], AppHash[19], AppHash[20], AppHash[21], AppHash[22], AppHash[23],
                                    AppHash[24], AppHash[25], AppHash[26], AppHash[27], AppHash[28], AppHash[29], AppHash[30],AppHash[31],
                                    0,0,0,0, 0,0,0,0, 0,0,0,0
                                    };

        uint selfCrc;
        unsafe
        {
            //计算selfCrc值
            fixed (byte* pCSArray = &selfCrcCheckData[0])
            {
                selfCrc = GetCrc32(pCSArray, (uint)selfCrcCheckData.Count());
            }
        }
        byte[] selfCrcArr = { (byte)(selfCrc & 0xFF), (byte)(selfCrc >> 8 & 0xFF), (byte)(selfCrc >> 16 & 0xFF), (byte)(selfCrc >> 24 & 0xFF) };

        //合并完整的AppValidityInfo信息
        byte[] AppValidityInfo = new byte[selfCrcArr.Length + selfCrcCheckData.Length];

        Array.Copy(selfCrcArr, 0, AppValidityInfo, 0, selfCrcArr.Length);
        Array.Copy(selfCrcCheckData, 0, AppValidityInfo, selfCrcArr.Length, selfCrcCheckData.Length);



        MessageBox.Show(selfCrc.ToString("X2"));


}

    /// <summary>
    /// 16进制字符串转字符数组
    /// </summary>
    /// <param name="_str">字符串</param>
    /// <param name="encode">编码格式</param>
    /// <returns></returns>
    private byte[] HexStringToByteArray(string hexString)
    {
        List<byte> byteBuffer = new List<byte>();

        for (int i = 0; i < hexString.Length; i += 2)
        {
            string twoHexChars = hexString.Substring(i, 2); // 每次取两位
            byte value = Convert.ToByte(twoHexChars, 16); // 转换为字节
            byteBuffer.Add(value);
        }

        byte[] byteArray = byteBuffer.ToArray(); // 转换为字节数组

        return byteArray;
    }

}

