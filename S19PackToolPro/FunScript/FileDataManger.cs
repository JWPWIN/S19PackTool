using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

public enum ChipType
{ 
    Tc334,
    Ti280039,
}

public class FileDataManger
{
    //app内容
    public string appFileString;//app程序包总字符串
    public string appStartAddress;//app起始地址
    public string appLength;//app长度
    public List<string> appDataField;//app数据段字符串 每个元素代表一行数据
    public string appDataFieldStr;//app数据段总字符串
    public byte[] appDataHex;//app数据段Hex格式数据
    public string appProCode;//APP项目代码
    public string appVer;//APP内部版本号
    public List<string> hvAppExportLineStr;//导出HvAPP的内容
    public string hvAppStartAdrInFile;//hvapp文件存储开始地址
    //boot内容
    public string bootFileString;//boot程序包总字符串
    public string bootStartAddress;//boot起始地址
    public string bootLength;//boot长度
    public string bootProCode;//Boot项目代码
    public string bootVer;//Boot内部版本号
    //选择芯片类型
    public ChipType selectChipType;//选择芯片类型

    public FileDataManger()
    {
        ResetFileData(); //初始化重置所有数据
    }

    public void ResetFileData()
    {
        appFileString = string.Empty;
        appStartAddress = string.Empty;
        appLength = string.Empty;
        bootFileString = string.Empty;
        bootStartAddress = string.Empty;
        bootLength = string.Empty;
        appDataField = new List<string>();
        hvAppExportLineStr = new List<string>();
        appDataFieldStr = string.Empty;
        appProCode = string.Empty;
        appVer = string.Empty;
        bootProCode = string.Empty;
        bootVer = string.Empty;
        hvAppStartAdrInFile = string.Empty;
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

        //Tc334-Lv App程序读取
        if (selectChipType == ChipType.Tc334)
        {
            foreach (var item in sArray)
            {
                //获取App起始地址及长度
                if (item.Contains("A0038000"))
                {
                    appStartAddress = item.Substring(4, 8);
                }

                if (item.Contains("A0038020"))
                {
                    char[] lenStr = { item[66], item[67],
                       item[64], item[65],
                       item[62], item[63],
                       item[60], item[61],
                       };
                    appLength = new string(lenStr);
                }


                //获取APP项目信息：AppInfo版本结构体信息为 App起始地址+0x400
                if (item.Contains("A0038420"))
                {
                    //获取项目号
                    appProCode = "FE" + item.Substring(49, 1) + item.Substring(51, 1) + item.Substring(53, 1) + item.Substring(55, 1);
                    //获取App版本号
                    appVer = "V" + item.Substring(74, 2) + item.Substring(72, 2)
                             + "F" + item.Substring(70, 2) + item.Substring(68, 2);

                    break;
                }
            }

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
        //280039-HV App程序读取
        else if (selectChipType == ChipType.Ti280039)
        {
            string _hvAppStartAdrInFile = string.Empty;
            string _hvAppSize = string.Empty;
            uint _ulAppAdr = 0;
            uint _ulhvAppStartAdrInFile = 0;
            uint _ulhvAppEndAdrInFile = 0;
            List<string> _tmpHvAppExportLineStr = new List<string>();

            //读取程序包数据

            foreach (var item in sArray)
            {
                //获取Hv app起始地址及大小
                if (item.Contains("A00380C0"))
                {
                    string fileOffset = item.Substring(50, 2) + item.Substring(48, 2) + item.Substring(46, 2) + item.Substring(44, 2);
                    appStartAddress = item.Substring(26, 2) + item.Substring(24, 2) + item.Substring(22, 2) + item.Substring(20, 2);
                    uint _hvAppStartAdd = (uint)(Int32.Parse("A0038000", System.Globalization.NumberStyles.HexNumber));
                    uint _appOffset = (uint)(Int32.Parse(fileOffset, System.Globalization.NumberStyles.HexNumber));
                    uint _appendAppDataAdd_L1 = _hvAppStartAdd + _appOffset;
                    _hvAppStartAdrInFile = _appendAppDataAdd_L1.ToString("X2");
                    _hvAppSize = item.Substring(34, 2) + item.Substring(32, 2) + item.Substring(30, 2) + item.Substring(28, 2);
                    appLength = _hvAppSize;

                    _ulhvAppStartAdrInFile = (uint)(Int32.Parse(_hvAppStartAdrInFile, System.Globalization.NumberStyles.HexNumber));
                    _ulhvAppEndAdrInFile = _ulhvAppStartAdrInFile + (uint)(Int32.Parse(_hvAppSize, System.Globalization.NumberStyles.HexNumber));

                    hvAppStartAdrInFile = _ulhvAppStartAdrInFile.ToString("X2");
                }

                //仅保留S3字段的数据
                if ((item.Contains("S325")))
                {
                    _ulAppAdr = (uint)(Int32.Parse(item.Substring(4, 8), System.Globalization.NumberStyles.HexNumber));
                    if ((_ulAppAdr >= _ulhvAppStartAdrInFile) && (_ulAppAdr < _ulhvAppEndAdrInFile))//取HvApp存储地址程序
                    {
                        _tmpHvAppExportLineStr.Add(item);
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

            //修改hvApp数据的地址/数据段双字节排列/checksum
            uint _ulLastAdr = 0;
            foreach (var item in _tmpHvAppExportLineStr)
            {
                string _lineData = item.Substring(0, 4);//S325 + 双字节地址 + 双字节数据段 + checksum

                //写入转换后的地址
                uint _ulRawAdr = (uint)(Int32.Parse(item.Substring(4, 8), System.Globalization.NumberStyles.HexNumber));
                uint _ulRetAdr = _ulRawAdr - _ulhvAppStartAdrInFile + 0x00090000;

                if (_ulLastAdr == 0)
                {
                    _ulLastAdr = _ulRetAdr;
                }
                else
                {
                    _ulRetAdr = (_ulRetAdr - _ulLastAdr) / 2 + _ulLastAdr;
                }

                //在HvAppStartAddress + 0x200取HvApp项目号和版本信息
                if (_ulRetAdr == 0x00090000 + 0x210)
                {
                    appVer = item.Substring(74,2) + item.Substring(72, 2) + item.Substring(70, 2) + item.Substring(68, 2);
                    appProCode = ConverAsciiToString(HexStringToByteArray(item.Substring(44, 24)));
                }

                _lineData += _ulRetAdr.ToString("X8");

                //写入转换后的数据段，数据段两两Byte交换前后顺序
                for (int i = 0; i < 32; i++)
                {
                    _lineData += item.Substring(12 + 2 * i, 2);
                }

                //写入转换后的校验和CHKSUM = 0xFF - （（字节计数 + 地址 + 数据和 ）&0xFF）
                uint _checksum = 0x25;
                _checksum += (_ulRetAdr & 0xFF) + (_ulRetAdr >> 8 & 0xFF) + (_ulRetAdr >> 16 & 0xFF) + (_ulRetAdr >> 24 & 0xFF);
                for (int i = 0; i < 32; i++)
                {
                    _checksum += (uint)(Int32.Parse(_lineData.Substring((12 + i * 2), 2), System.Globalization.NumberStyles.HexNumber));
                }
                _checksum &= 0xFF;
                _checksum = (byte)(0xFF - _checksum);
                _lineData += _checksum.ToString("X2") + "\r\n";

                hvAppExportLineStr.Add(_lineData);
            }
        }
        else
        { }
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

        //获取boot起始地址及长度
        bootStartAddress = sArray[0].Substring(4, 8);

        //获取boot项目信息：

        if (selectChipType == ChipType.Ti280039)
        {
            //AppInfo版本结构体信息为 boot起始地址0x00080000+0x200(Ti280039-Boot)
            foreach (var item in sArray)
            {
                if (item.Substring(4, 8) == (0x00080000 + 0x200).ToString("X8"))
                {
                    bootLength = item.Substring(50, 2) + item.Substring(48, 2) + item.Substring(46, 2) + item.Substring(44, 2);
                }

                if (item.Substring(4,8) == (0x00080000 + 0x210).ToString("X8"))
                {
                    bootVer = item.Substring(74, 2) + item.Substring(72, 2) + item.Substring(70, 2) + item.Substring(68, 2);
                    bootProCode = ConverAsciiToString(HexStringToByteArray(item.Substring(44, 24)));
                    break;
                }
            }
        }
        else
        {
            //AppInfo版本结构体信息为 boot起始地址A0108000+0x400(Tc334-Boot)
            foreach (var item in sArray)
            {
                //获取App起始地址及长度
                if (item.Contains("A0108000"))
                {
                    bootStartAddress = item.Substring(4, 8);
                }

                if (item.Contains("A0108400"))
                {
                    //读取boot长度
                    bootLength = item.Substring(50, 2) + item.Substring(48, 2) + item.Substring(46, 2) + item.Substring(44, 2);
                }

                if (item.Contains("A0108420"))
                {
                    //获取项目号
                    bootProCode = "FE" + item.Substring(49, 1) + item.Substring(51, 1) + item.Substring(53, 1) + item.Substring(55, 1);
                    //获取App版本号
                    bootVer = "V" + item.Substring(74, 2) + item.Substring(72, 2)
                             + "B" + item.Substring(70, 2) + item.Substring(68, 2);

                    break;
                }
            }
        }

    }

    [DllImport("CrcAndHashLibDll.dll")]
    extern unsafe static uint GetCrc32(byte* buffer, uint len);

    [DllImport("CrcAndHashLibDll.dll")]
    extern unsafe static uint GetCrc32ForForValidityInfo(byte* buffer, uint len);

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
            MessageBox.Show("APP/BOOT程序包加载异常,请先加载程序");
            return;
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
        if (selectChipType == ChipType.Ti280039)
        {
            selfSize = 0x20;
        }
        uint validFlag = 0x32F1D9CC;
        byte[] reserved = new byte[12];

        //S3数据段字符串转换为Hex数据
        appDataHex = HexStringToByteArray(appDataFieldStr);

        //计算App数据的CRC和Hash
        uint AppCrc;
        byte[] AppHash = new byte[32];
        unsafe
        {

            if (selectChipType == ChipType.Ti280039)
            {
                //计算APP的CRC值,AppCrc 只要计算APP-S3段的数据域校验
                fixed (byte* pCSArray = &appDataHex[0])
                {
                    AppCrc = GetCrc32(pCSArray, (uint)appDataHex.Count());
                }

            }
            else
            {
                //计算APP的CRC值,AppCrc 只要计算APP-S3段的数据域校验
                fixed (byte* pCSArray = &appDataHex[0])
                {
                    AppCrc = GetCrc32ForForValidityInfo(pCSArray, (uint)appDataHex.Count());
                }

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
        byte[] selfCrcCheckData = new byte[60];
        uint selfCrc;
        if (selectChipType == ChipType.Ti280039)
        {
            byte[] _selfCrcCheckData = { (byte)(flag&0xFF),(byte)(flag>>8&0xFF),(byte)(flag>>16&0xFF),(byte)(flag>>24&0xFF),
                                    (byte)(selfSize),0,0,0,
                                    (byte)(validFlag&0xFF),(byte)(validFlag>>8&0xFF),(byte)(validFlag>>16&0xFF),(byte)(validFlag>>24&0xFF),
                                    (byte)(AppCrc&0xFF),(byte)(AppCrc>>8&0xFF),(byte)(AppCrc>>16&0xFF),(byte)(AppCrc>>24&0xFF),
                                    0x68,0x54,0x73, 0x69,0x69,0x20,0x20,0x73,0x61,0x76,0x69,0x6C,0x20,0x64,0x70,0x41,0x2E,0x70,
                                    0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0, 0x0,0x0,0x0,0x0,0x0,
                                    0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0, 0x0,0x0,0x0,0x0,0x0,
                                    };


            for (int i = 0; i < selfCrcCheckData.Count(); i++)
            {
                selfCrcCheckData[i] = _selfCrcCheckData[i];
            }

            //280049数据域需要切换双字节计算
            string _tmpStr = string.Empty;
            foreach (var item in selfCrcCheckData)
            {
                _tmpStr += item.ToString("X2");
            }

            string _dbByteStr = string.Empty;
            for (int i = 0; i < 30; i++)
            {
                _dbByteStr += _tmpStr.Substring(2 + 4 * i, 2);
                _dbByteStr += _tmpStr.Substring(2 + 4 * i - 2, 2);
            }
            byte[] _tmpinfo = HexStringToByteArray(_dbByteStr);

            unsafe
            {
                //计算selfCrc值
                fixed (byte* pCSArray = &_tmpinfo[0])
                {
                    selfCrc = GetCrc32(pCSArray, (uint)_tmpinfo.Count());
                }
            }
        }
        else//使用334方式计算selfcrc
        {
            byte[] _selfCrcCheckData = { (byte)(flag&0xFF),(byte)(flag>>8&0xFF),(byte)(flag>>16&0xFF),(byte)(flag>>24&0xFF),
                                     (byte)(selfSize),0,0,0,
                                    (byte)(validFlag&0xFF),(byte)(validFlag>>8&0xFF),(byte)(validFlag>>16&0xFF),(byte)(validFlag>>24&0xFF),
                                    (byte)(AppCrc&0xFF),(byte)(AppCrc>>8&0xFF),(byte)(AppCrc>>16&0xFF),(byte)(AppCrc>>24&0xFF),
                                    AppHash[0], AppHash[1], AppHash[2], AppHash[3], AppHash[4], AppHash[5], AppHash[6], AppHash[7],
                                    AppHash[8], AppHash[9], AppHash[10], AppHash[11], AppHash[12], AppHash[13], AppHash[14], AppHash[15],
                                    AppHash[16], AppHash[17], AppHash[18], AppHash[19], AppHash[20], AppHash[21], AppHash[22], AppHash[23],
                                    AppHash[24], AppHash[25], AppHash[26], AppHash[27], AppHash[28], AppHash[29], AppHash[30],AppHash[31],
                                    0x49,0x73,0x20,0x76, 0x61,0x6c,0x69,0x64, 0x20,0x41,0x70,0x70
                                    };

            for (int i = 0; i < selfCrcCheckData.Count(); i++)
            {
                selfCrcCheckData[i] = _selfCrcCheckData[i];
            }

            unsafe
            {
                //计算selfCrc值
                fixed (byte* pCSArray = &selfCrcCheckData[0])
                {
                    selfCrc = GetCrc32(pCSArray, (uint)selfCrcCheckData.Count());
                }
            }

        }

        byte[] selfCrcArr = { (byte)(selfCrc & 0xFF), (byte)(selfCrc >> 8 & 0xFF), (byte)(selfCrc >> 16 & 0xFF), (byte)(selfCrc >> 24 & 0xFF) };

        //合并完整的AppValidityInfo信息
        byte[] AppValidityInfo = new byte[selfCrcArr.Length + selfCrcCheckData.Length];

        Array.Copy(selfCrcArr, 0, AppValidityInfo, 0, selfCrcArr.Length);
        Array.Copy(selfCrcCheckData, 0, AppValidityInfo, selfCrcArr.Length, selfCrcCheckData.Length);


        //合并APP及BOOT程序包
        string AppAndBoot_2In1_Str = string.Empty;
        if (selectChipType == ChipType.Ti280039)
        {
            string _tmpStr1 = string.Empty;
            foreach (var item in hvAppExportLineStr)
            {
                _tmpStr1 += item;
            }
            AppAndBoot_2In1_Str += _tmpStr1;
        }
        else
        {
            AppAndBoot_2In1_Str += appFileString;
        }

        AppAndBoot_2In1_Str += bootFileString;

        uint _appStartAdd = (uint)(Int32.Parse(appStartAddress, System.Globalization.NumberStyles.HexNumber));
        uint _appLen = (uint)(Int32.Parse(appLength, System.Globalization.NumberStyles.HexNumber));
        uint _appendAppDataAdd_L1 = _appStartAdd + _appLen;
        if (selectChipType == ChipType.Ti280039)
        {
            _appendAppDataAdd_L1 = _appStartAdd + _appLen/2;
        };

        //校验和CHKSUM = 0xFF - （（字节计数 + 地址 + 数据和 ）&0xFF）
        uint _tmp = 0x25;
        _tmp += (_appendAppDataAdd_L1 & 0xFF) + (_appendAppDataAdd_L1 >> 8 & 0xFF) + (_appendAppDataAdd_L1 >> 16 & 0xFF) + (_appendAppDataAdd_L1 >> 24 & 0xFF);
        string _tmpData = string.Empty;
        for (int i = 0; i < 32; i++)
        {
            _tmpData += AppValidityInfo[i].ToString("X2");
            _tmp += AppValidityInfo[i];
        }

        //280049数据域需要切换双字节
        if (selectChipType == ChipType.Ti280039)
        {
            string _doubleByteStr = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                _doubleByteStr += _tmpData.Substring(2 + 4 * i, 2);
                _doubleByteStr += _tmpData.Substring(2 + 4 * i - 2, 2);
            }

            _tmpData = _doubleByteStr;
        }

        _tmp &= 0xFF;

        byte checksum_line1 = (byte)(0xFF - _tmp);
        AppAndBoot_2In1_Str += "S325" + _appendAppDataAdd_L1.ToString("X8") + _tmpData + checksum_line1.ToString("X2") + "\r\n";

        _tmp = 0x25;
        uint _appendAppDataAdd_L2 = _appendAppDataAdd_L1;
        if (selectChipType == ChipType.Ti280039)
        {
            _appendAppDataAdd_L2 += 0x10;
        }
        else
        {
            _appendAppDataAdd_L2 += 0x20;
        }

        _tmp += (_appendAppDataAdd_L2 & 0xFF) + (_appendAppDataAdd_L2 >> 8 & 0xFF) + (_appendAppDataAdd_L2 >> 16 & 0xFF) + (_appendAppDataAdd_L2 >> 24 & 0xFF);
        _tmpData = string.Empty;
        for (int i = 32; i < 64; i++)
        {
            _tmpData += AppValidityInfo[i].ToString("X2");
            _tmp += AppValidityInfo[i];
        }

        //280049数据域需要切换双字节
        if (selectChipType == ChipType.Ti280039)
        {
            string _doubleByteStr = string.Empty;
            for (int i = 0; i < 16; i++)
            {
                _doubleByteStr += _tmpData.Substring(2 + 4 * i, 2);
                _doubleByteStr += _tmpData.Substring(2 + 4 * i - 2, 2);
            }

            _tmpData = _doubleByteStr;
        }

        _tmp &= 0xFF;
        byte checksum_line2 = (byte)(0xFF - _tmp);
        AppAndBoot_2In1_Str += "S325" + _appendAppDataAdd_L2.ToString("X8") + _tmpData + checksum_line2.ToString("X2") + "\r\n";

        string exportFileName = string.Empty;
        if (selectChipType == ChipType.Ti280039)
        {
            exportFileName = "TI280039";
        }
        else
        {
            exportFileName = "TC334";
        }
        exportFileName = exportFileName + "_" + appProCode + "-" + appVer + "_" + bootProCode + "-" + bootVer + "_";
        string systemTime = System.DateTime.Now.ToString("yyyyMMdd-HHmmss");
        exportFileName += systemTime;

        TextOperation.WriteData(exportFileName, FileType.S19,AppAndBoot_2In1_Str);

        MessageBox.Show("一体包生成成功");
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

    /// <summary>
    /// ascii码转字符
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string ConverAsciiToString(byte[] data)
    {
        string result = string.Empty;
        try
        {
            foreach (byte b in data)
            {
                int a = int.Parse(b.ToString());   //现将字符串转成int类型
                if (a >= 0 && a <= 255)     //若不在这个范围内，则不是字符
                {
                    char c = (char)a;   //利用类型强转得到字符
                    result += c;
                }
            }
        }
        catch (Exception ex)
        {
        }
        return result;
    }

}

