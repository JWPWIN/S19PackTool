using Microsoft.Win32;
using System.Collections;
using System.Windows.Forms.Design;

namespace S19PackToolPro
{
    public partial class Form1 : Form
    {
        //应用软件版本号
        public readonly string ApplicationVersion = "-V1.0-20251120";

        FileDataManger fileDataManger;

        public Form1()
        {
            InitializeComponent();

            //添加当前应用程序路径到win环境变量
            AddExePathToWinSystemEnvironmentPath();


            fileDataManger = new FileDataManger();
        }

        /// <summary>
        /// 添加当前应用路径至win系统环境路径 用于正确调用Dll文件
        /// </summary>
        private void AddExePathToWinSystemEnvironmentPath()
        {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey env = key.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment", true); //该项必须已存在
            // 从注册表读取系统环境变量Path值（%SystemRoot%系统变量不会被替换为C:\Windows）
            string pathStr = (string)env.GetValue("PATH", "", RegistryValueOptions.DoNotExpandEnvironmentNames);

            string curExeDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // 读取系统环境变量Path值（%SystemRoot%会自动转换为真实的C:\Windows）
            // string pathStr = System.Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

            //如果系统环境路径中未包含该路径，则添加当前exe路径至系统环境路径
            if (!pathStr.Contains(curExeDirectory))
            {
                pathStr += ";" + curExeDirectory;
                // 修改环境变量Path值
                Environment.SetEnvironmentVariable("Path", pathStr, EnvironmentVariableTarget.Machine);
            }
        }

        private void Btn_LoadAppFile_Click(object sender, EventArgs e)
        {
            fileDataManger.ParseAppFileData(TextOperation.ReadData());
            this.btn_LoadAppFile.BackColor = System.Drawing.Color.Green;
            //显示APP信息
            this.Text_AppStartAdr.Text = fileDataManger.appStartAddress;
            this.Text_AppLen.Text = fileDataManger.appLength;
            this.Text_AppProCode.Text = fileDataManger.appProCode;
            this.Text_AppVer.Text = fileDataManger.appVer;

        }

        private void btn_LoadBootFile_Click(object sender, EventArgs e)
        {
            fileDataManger.ParseBootFileData(TextOperation.ReadData());
            this.btn_LoadBootFile.BackColor = System.Drawing.Color.Green;
            //显示Boot信息
            this.Text_BootStartAdr.Text = fileDataManger.bootStartAddress;
            this.Text_BootLen.Text = fileDataManger.bootLength;
            this.Text_BootProCode.Text= fileDataManger.bootProCode;
            this.Text_BootVer.Text= fileDataManger.bootVer;

        }

        private void btn_IntegratedPkg_Click(object sender, EventArgs e)
        {
            fileDataManger.IntegratedPkg();
        }

        private void ComboBox_ChipSelect_IndexChange(object sender, EventArgs e)
        { 
            ChipType selcetChipType = (ChipType)(comboBox_SelectChip.SelectedIndex);

            if (selcetChipType != fileDataManger.selectChipType)
            {
                fileDataManger.selectChipType = selcetChipType;
                //选择芯片更新，重置数据
                fileDataManger.ResetFileData();
                this.btn_LoadAppFile.BackColor = Button.DefaultBackColor;
                this.btn_LoadBootFile.BackColor = Button.DefaultBackColor;
                this.Text_AppStartAdr.Text = string.Empty;
                this.Text_AppLen.Text = string.Empty;
                this.Text_AppProCode.Text = string.Empty;
                this.Text_AppVer.Text = string.Empty;
                this.Text_BootStartAdr.Text = string.Empty;
                this.Text_BootLen.Text = string.Empty;
                this.Text_BootProCode.Text = string.Empty;
                this.Text_BootVer.Text = string.Empty;
            }

        }

    }
}
