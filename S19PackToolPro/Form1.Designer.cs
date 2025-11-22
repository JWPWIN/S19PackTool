namespace S19PackToolPro
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_LoadAppFile = new Button();
            AppInfo = new GroupBox();
            label7 = new Label();
            Text_AppVer = new TextBox();
            label5 = new Label();
            Text_AppProCode = new TextBox();
            label2 = new Label();
            Text_AppLen = new TextBox();
            label1 = new Label();
            Text_AppStartAdr = new TextBox();
            BootInfo = new GroupBox();
            label8 = new Label();
            Text_BootVer = new TextBox();
            label6 = new Label();
            Text_BootProCode = new TextBox();
            label3 = new Label();
            Text_BootLen = new TextBox();
            label4 = new Label();
            Text_BootStartAdr = new TextBox();
            btn_LoadBootFile = new Button();
            btn_IntegratedPkg = new Button();
            comboBox_SelectChip = new ComboBox();
            label9 = new Label();
            AppInfo.SuspendLayout();
            BootInfo.SuspendLayout();
            SuspendLayout();
            // 
            // btn_LoadAppFile
            // 
            btn_LoadAppFile.Location = new Point(35, 71);
            btn_LoadAppFile.Name = "btn_LoadAppFile";
            btn_LoadAppFile.Size = new Size(133, 33);
            btn_LoadAppFile.TabIndex = 0;
            btn_LoadAppFile.Text = "加载App文件";
            btn_LoadAppFile.UseVisualStyleBackColor = true;
            btn_LoadAppFile.Click += Btn_LoadAppFile_Click;
            // 
            // AppInfo
            // 
            AppInfo.Controls.Add(label7);
            AppInfo.Controls.Add(Text_AppVer);
            AppInfo.Controls.Add(label5);
            AppInfo.Controls.Add(Text_AppProCode);
            AppInfo.Controls.Add(label2);
            AppInfo.Controls.Add(Text_AppLen);
            AppInfo.Controls.Add(label1);
            AppInfo.Controls.Add(Text_AppStartAdr);
            AppInfo.Location = new Point(35, 119);
            AppInfo.Name = "AppInfo";
            AppInfo.Size = new Size(218, 240);
            AppInfo.TabIndex = 1;
            AppInfo.TabStop = false;
            AppInfo.Text = "App信息";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(11, 54);
            label7.Name = "label7";
            label7.Size = new Size(68, 17);
            label7.TabIndex = 7;
            label7.Text = "App版本号";
            // 
            // Text_AppVer
            // 
            Text_AppVer.Location = new Point(91, 51);
            Text_AppVer.Name = "Text_AppVer";
            Text_AppVer.Size = new Size(100, 23);
            Text_AppVer.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 25);
            label5.Name = "label5";
            label5.Size = new Size(68, 17);
            label5.TabIndex = 5;
            label5.Text = "App项目号";
            // 
            // Text_AppProCode
            // 
            Text_AppProCode.Location = new Point(91, 22);
            Text_AppProCode.Name = "Text_AppProCode";
            Text_AppProCode.Size = new Size(100, 23);
            Text_AppProCode.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 116);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 3;
            label2.Text = "App长度";
            // 
            // Text_AppLen
            // 
            Text_AppLen.Location = new Point(91, 113);
            Text_AppLen.Name = "Text_AppLen";
            Text_AppLen.Size = new Size(100, 23);
            Text_AppLen.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 87);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 1;
            label1.Text = "App起始地址";
            // 
            // Text_AppStartAdr
            // 
            Text_AppStartAdr.Location = new Point(91, 84);
            Text_AppStartAdr.Name = "Text_AppStartAdr";
            Text_AppStartAdr.Size = new Size(100, 23);
            Text_AppStartAdr.TabIndex = 0;
            // 
            // BootInfo
            // 
            BootInfo.Controls.Add(label8);
            BootInfo.Controls.Add(Text_BootVer);
            BootInfo.Controls.Add(label6);
            BootInfo.Controls.Add(Text_BootProCode);
            BootInfo.Controls.Add(label3);
            BootInfo.Controls.Add(Text_BootLen);
            BootInfo.Controls.Add(label4);
            BootInfo.Controls.Add(Text_BootStartAdr);
            BootInfo.Location = new Point(311, 119);
            BootInfo.Name = "BootInfo";
            BootInfo.Size = new Size(218, 240);
            BootInfo.TabIndex = 2;
            BootInfo.TabStop = false;
            BootInfo.Text = "Boot信息";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 54);
            label8.Name = "label8";
            label8.Size = new Size(72, 17);
            label8.TabIndex = 7;
            label8.Text = "Boot版本号";
            // 
            // Text_BootVer
            // 
            Text_BootVer.Location = new Point(90, 51);
            Text_BootVer.Name = "Text_BootVer";
            Text_BootVer.Size = new Size(100, 23);
            Text_BootVer.TabIndex = 6;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 25);
            label6.Name = "label6";
            label6.Size = new Size(72, 17);
            label6.TabIndex = 5;
            label6.Text = "Boot项目号";
            // 
            // Text_BootProCode
            // 
            Text_BootProCode.Location = new Point(90, 22);
            Text_BootProCode.Name = "Text_BootProCode";
            Text_BootProCode.Size = new Size(100, 23);
            Text_BootProCode.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 113);
            label3.Name = "label3";
            label3.Size = new Size(60, 17);
            label3.TabIndex = 3;
            label3.Text = "Boot长度";
            // 
            // Text_BootLen
            // 
            Text_BootLen.Location = new Point(90, 110);
            Text_BootLen.Name = "Text_BootLen";
            Text_BootLen.Size = new Size(100, 23);
            Text_BootLen.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 84);
            label4.Name = "label4";
            label4.Size = new Size(84, 17);
            label4.TabIndex = 1;
            label4.Text = "Boot起始地址";
            // 
            // Text_BootStartAdr
            // 
            Text_BootStartAdr.Location = new Point(90, 81);
            Text_BootStartAdr.Name = "Text_BootStartAdr";
            Text_BootStartAdr.Size = new Size(100, 23);
            Text_BootStartAdr.TabIndex = 0;
            // 
            // btn_LoadBootFile
            // 
            btn_LoadBootFile.Location = new Point(311, 71);
            btn_LoadBootFile.Name = "btn_LoadBootFile";
            btn_LoadBootFile.Size = new Size(133, 33);
            btn_LoadBootFile.TabIndex = 3;
            btn_LoadBootFile.Text = "加载Boot文件";
            btn_LoadBootFile.UseVisualStyleBackColor = true;
            btn_LoadBootFile.Click += btn_LoadBootFile_Click;
            // 
            // btn_IntegratedPkg
            // 
            btn_IntegratedPkg.Location = new Point(35, 402);
            btn_IntegratedPkg.Name = "btn_IntegratedPkg";
            btn_IntegratedPkg.Size = new Size(133, 38);
            btn_IntegratedPkg.TabIndex = 4;
            btn_IntegratedPkg.Text = "合成一体包";
            btn_IntegratedPkg.UseVisualStyleBackColor = true;
            btn_IntegratedPkg.Click += btn_IntegratedPkg_Click;
            // 
            // comboBox_SelectChip
            // 
            comboBox_SelectChip.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_SelectChip.FormattingEnabled = true;
            comboBox_SelectChip.Location = new Point(109, 17);
            comboBox_SelectChip.Name = "comboBox_SelectChip";
            comboBox_SelectChip.Size = new Size(409, 25);
            comboBox_SelectChip.Items.Add("Chip_Tc334-Adr_AppA0038000/BootA0108000-单字节");
            comboBox_SelectChip.Items.Add("Chip_280039-Adr_App00090000/Boot00080000-双字节");
            comboBox_SelectChip.Items.Add("Chip_280039-Adr_App00090000/Boot00080000-双字节-D01-DCDC");
            comboBox_SelectChip.SelectedIndex = 0;
            comboBox_SelectChip.TabIndex = 5;
            comboBox_SelectChip.SelectedIndexChanged += ComboBox_ChipSelect_IndexChange;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(35, 20);
            label9.Name = "label9";
            label9.Size = new Size(56, 17);
            label9.TabIndex = 6;
            label9.Text = "芯片类型";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label9);
            Controls.Add(comboBox_SelectChip);
            Controls.Add(btn_IntegratedPkg);
            Controls.Add(btn_LoadBootFile);
            Controls.Add(BootInfo);
            Controls.Add(AppInfo);
            Controls.Add(btn_LoadAppFile);
            Name = "Xr2In1PkgTool";
            Text = Name + ApplicationVersion;
            AppInfo.ResumeLayout(false);
            AppInfo.PerformLayout();
            BootInfo.ResumeLayout(false);
            BootInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_LoadAppFile;
        private GroupBox AppInfo;
        private Label label2;
        private TextBox Text_AppLen;
        private Label label1;
        private TextBox Text_AppStartAdr;
        private GroupBox BootInfo;
        private Label label3;
        private TextBox Text_BootLen;
        private Label label4;
        private TextBox Text_BootStartAdr;
        private Button btn_LoadBootFile;
        private Button btn_IntegratedPkg;
        private Label label7;
        private TextBox Text_AppVer;
        private Label label5;
        private TextBox Text_AppProCode;
        private Label label6;
        private TextBox Text_BootProCode;
        private Label label8;
        private TextBox Text_BootVer;
        private ComboBox comboBox_SelectChip;
        private Label label9;
    }
}
