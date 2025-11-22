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

            fileDataManger = new FileDataManger();

            //添加当前应用程序路径到win环境变量
            AddExePathToWinSystemEnvironmentPath();
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
            // 读取系统环境变量Path值（%SystemRoot%会自动转换为真实的C:\Windows）
            // string pathStr = System.Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.Machine);

            //获取当前exe程序所在文件目录路径
            string curExeDirectory = AppDomain.CurrentDomain.BaseDirectory;

            //如果系统环境路径中未包含该路径，则添加当前exe路径至系统环境路径
            if (!pathStr.Contains(curExeDirectory))
            {
                //在没有添加环境变量的情况下 显示是否添加环境变量的对话框
                DialogResult MsgBoxResult;//设置对话框的返回值
                MsgBoxResult = MessageBox.Show("未添加当前路径到系统环境变量，请确认是否添加",//对话框的显示内容 
                "添加系统环境变量",//对话框的标题 
                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)//如果对话框的返回值是YES（按"Y"按钮）
                {
                    //添加当前路径到环境变量
                    pathStr += ";" + curExeDirectory;
                    // 修改环境变量Path值
                    Environment.SetEnvironmentVariable("Path", pathStr, EnvironmentVariableTarget.Machine);
                }
                if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）
                {
                    //退出程序
                    System.Environment.Exit(0);
                }
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
