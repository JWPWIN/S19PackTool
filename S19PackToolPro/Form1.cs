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

            this.textBox1.Text = fileDataManger.appDataField[0];

        }

    }
}
