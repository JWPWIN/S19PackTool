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

        }

        private void btn_LoadBootFile_Click(object sender, EventArgs e)
        {
            fileDataManger.ParseBootFileData(TextOperation.ReadData());
            this.btn_LoadBootFile.BackColor = System.Drawing.Color.Green;
            //显示Boot信息
            this.Text_BootStartAdr.Text = fileDataManger.bootStartAddress;
            this.Text_BootLen.Text = fileDataManger.bootLength;


        }

        private void btn_IntegratedPkg_Click(object sender, EventArgs e)
        {
            fileDataManger.IntegratedPkg();
        }

    }
}
