using Microsoft.Win32;
using S19PackToolPro.Wins;
using System.Collections;
using System.Windows.Forms.Design;

namespace S19PackToolPro
{
    public partial class Form1 : Form
    {
        //应用软件版本号
        public readonly string ApplicationVersion = "-V2.0-20260310";

        FileDataManger fileDataManger;

        ComProcessBarWin comProcessBarWin;

        public Form1()
        {
            InitializeComponent();

            //初始化数据管理器
            fileDataManger = new FileDataManger();

            //初始化进度条窗口
            comProcessBarWin = new ComProcessBarWin();
            comProcessBarWin.Hide();

            //初始化读取需要支持的CBF项目列表
            bool readCfgRes = fileDataManger.InitPkgProInfoList();
            if (readCfgRes == false) System.Environment.Exit(0);//如果读取配置失败则立即退出程序
            string[] proList = new string[fileDataManger.pkgInfoCfgList.Count];
            for (int i = 0; i < fileDataManger.pkgInfoCfgList.Count; i++)
            {
                proList[i] = fileDataManger.pkgInfoCfgList[i];
            }
            comboBox_SelectPkgInfo.Items.AddRange(proList);
            comboBox_SelectPkgInfo.SelectedIndex = 0;
            UpdateCurPkgCfgInfo();//更新打包配置信息

            //添加当前应用程序路径到win环境变量
            //AddExePathToWinSystemEnvironmentPath();
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
                    // 异步修改环境变量Path值
                    Task task1 = ReWriteSysEnvPathAsync(pathStr);
                    Task task2 = comProcessBarWin.TaskProcessTipAsync("正在加载当前路径到系统环境变量...");
                    Task.WhenAll(task1, task2);
                }
                else if (MsgBoxResult == DialogResult.No)//如果对话框的返回值是NO（按"N"按钮）
                {
                    //退出程序
                    System.Environment.Exit(0);
                }
                else { }
            }
        }

        /// <summary>
        /// 重写系统环境变量 该函数比较耗时 因此异步调用
        /// </summary>
        /// <param name="sysEnvPath">系统环境变量</param>
        /// <returns>None</returns>
        private async Task ReWriteSysEnvPathAsync(string sysEnvPath)
        {
            this.Enabled = false;

            await Task.Run(() => { Environment.SetEnvironmentVariable("Path", sysEnvPath, EnvironmentVariableTarget.Machine); });

            comProcessBarWin.SetTaskProcessBarCurValue(100);

            this.Enabled = true;
        }

        private void Btn_LoadAppFile_Click(object sender, EventArgs e)
        {
            string appData = TextOperation.ReadData();
            if (appData == string.Empty) return;//未读取到软件数据退出

            Task task1 = PhaseAppDataAsync(appData);
            Task task2 = comProcessBarWin.TaskProcessTipAsync("正在解析APP数据...");
            Task.WhenAll(task1, task2);
        }

        /// <summary>
        /// 解析APP数据 该函数比较耗时 因此异步调用
        /// </summary>
        private async Task PhaseAppDataAsync(string appData)
        {
            this.Enabled = false;

            await Task.Run(() => { fileDataManger.ParseAppFileData(appData); });

            //任务完成，同步更新进度条到100结束任务进度窗口
            comProcessBarWin.SetTaskProcessBarCurValue(100);

            //数据解析完成后更新UI
            this.btn_LoadAppFile.BackColor = System.Drawing.Color.Green;
            //显示APP信息
            this.Text_AppStartAdr.Text = fileDataManger.appStartAddress;
            this.Text_AppLen.Text = fileDataManger.appLength;
            this.Text_AppProCode.Text = fileDataManger.appProCode;
            this.Text_AppVer.Text = fileDataManger.appVer;

            this.Enabled = true;
        }

        private void btn_LoadBootFile_Click(object sender, EventArgs e)
        {
            string bootData = TextOperation.ReadData();
            if (bootData == string.Empty) return;//未读取到软件数据退出
            fileDataManger.ParseBootFileData(bootData);

            this.btn_LoadBootFile.BackColor = System.Drawing.Color.Green;
            //显示Boot信息
            this.Text_BootStartAdr.Text = fileDataManger.bootStartAddress;
            this.Text_BootLen.Text = fileDataManger.bootLength;
            this.Text_BootProCode.Text = fileDataManger.bootProCode;
            this.Text_BootVer.Text = fileDataManger.bootVer;

        }

        private void btn_IntegratedPkg_Click(object sender, EventArgs e)
        {
            fileDataManger.IntegratedPkg();
        }

        private void ComboBox_SelectPkgInfo_IndexChange(object sender, EventArgs e)
        {
            UpdateCurPkgCfgInfo();//更新打包配置信息

            //选择芯片更新，重置数据 更新UI
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

        /// <summary>
        /// 更新当前打包配置信息 更新配置数据和更新UI
        /// </summary>
        private void UpdateCurPkgCfgInfo()
        { 
            //解析打包信息配置字符串
            string[] pkgInfo = comboBox_SelectPkgInfo.Text.Split("@");

            //打包配置信息长度不正确 退出
            if (pkgInfo.Count() != 4)
            {
                MessageBox.Show("打包配置信息长度异常!");
                return;
            }

            string proCode = pkgInfo[0];
            string chipType = pkgInfo[1];
            string appAdd = pkgInfo[2];
            string bootAdd = pkgInfo[3];

            //更新打包数据信息
            fileDataManger.cfgProCode = proCode;

            if (chipType == "TC334")
                fileDataManger.cfgChipType = ChipType.Tc334;
            else if (chipType == "TI280039")
                fileDataManger.cfgChipType = ChipType.Ti280039;
            else fileDataManger.cfgChipType = ChipType.Tc334;

            fileDataManger.cfgAppAdd = appAdd;
            fileDataManger.cfgBootAdd = bootAdd;

            //更新UI
            label_ProCode.Text = proCode;
            label_ChipType.Text = chipType;
            label_AppAdd.Text = appAdd;
            label_BootAdd.Text = bootAdd;
        }
    }
}
