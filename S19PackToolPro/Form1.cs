using System.Windows.Forms.Design;

namespace S19PackToolPro
{
    public partial class Form1 : Form
    {
        FileDataManger fileDataManger;

        public Form1()
        {
            InitializeComponent();

            fileDataManger = new FileDataManger();
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
