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
            comboBox_SelectPkgInfo = new ComboBox();
            label9 = new Label();
            groupBox1 = new GroupBox();
            label_ChipType = new Label();
            label12 = new Label();
            label_ProCode = new Label();
            label10 = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            label_AppAdd = new Label();
            label13 = new Label();
            label_BootAdd = new Label();
            label14 = new Label();
            AppInfo.SuspendLayout();
            BootInfo.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // btn_LoadAppFile
            // 
            btn_LoadAppFile.Location = new Point(9, 22);
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
            AppInfo.Location = new Point(9, 70);
            AppInfo.Name = "AppInfo";
            AppInfo.Size = new Size(218, 149);
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
            BootInfo.Location = new Point(285, 70);
            BootInfo.Name = "BootInfo";
            BootInfo.Size = new Size(218, 149);
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
            btn_LoadBootFile.Location = new Point(285, 22);
            btn_LoadBootFile.Name = "btn_LoadBootFile";
            btn_LoadBootFile.Size = new Size(133, 33);
            btn_LoadBootFile.TabIndex = 3;
            btn_LoadBootFile.Text = "加载Boot文件";
            btn_LoadBootFile.UseVisualStyleBackColor = true;
            btn_LoadBootFile.Click += btn_LoadBootFile_Click;
            // 
            // btn_IntegratedPkg
            // 
            btn_IntegratedPkg.Location = new Point(6, 20);
            btn_IntegratedPkg.Name = "btn_IntegratedPkg";
            btn_IntegratedPkg.Size = new Size(118, 31);
            btn_IntegratedPkg.TabIndex = 4;
            btn_IntegratedPkg.Text = "合成一体包";
            btn_IntegratedPkg.UseVisualStyleBackColor = true;
            btn_IntegratedPkg.Click += btn_IntegratedPkg_Click;
            // 
            // comboBox_SelectPkgInfo
            // 
            comboBox_SelectPkgInfo.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_SelectPkgInfo.FormattingEnabled = true;
            comboBox_SelectPkgInfo.Location = new Point(93, 22);
            comboBox_SelectPkgInfo.Name = "comboBox_SelectPkgInfo";
            comboBox_SelectPkgInfo.Size = new Size(409, 25);
            comboBox_SelectPkgInfo.TabIndex = 5;
            comboBox_SelectPkgInfo.SelectedIndexChanged += ComboBox_SelectPkgInfo_IndexChange;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft YaHei UI", 9F);
            label9.Location = new Point(7, 25);
            label9.Name = "label9";
            label9.Size = new Size(80, 17);
            label9.TabIndex = 6;
            label9.Text = "选择打包信息";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label_BootAdd);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(label_AppAdd);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label_ChipType);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label_ProCode);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(comboBox_SelectPkgInfo);
            groupBox1.Controls.Add(label9);
            groupBox1.Location = new Point(35, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(660, 100);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "第一步.选择打包信息";
            // 
            // label_ChipType
            // 
            label_ChipType.AutoSize = true;
            label_ChipType.Location = new Point(240, 69);
            label_ChipType.Name = "label_ChipType";
            label_ChipType.Size = new Size(72, 17);
            label_ChipType.TabIndex = 10;
            label_ChipType.Text = "XXXXXXXX";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(166, 69);
            label12.Name = "label12";
            label12.Size = new Size(68, 17);
            label12.TabIndex = 9;
            label12.Text = "当前芯片：";
            // 
            // label_ProCode
            // 
            label_ProCode.AutoSize = true;
            label_ProCode.Location = new Point(93, 69);
            label_ProCode.Name = "label_ProCode";
            label_ProCode.Size = new Size(49, 17);
            label_ProCode.TabIndex = 8;
            label_ProCode.Text = "FE0000";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(11, 69);
            label10.Name = "label10";
            label10.Size = new Size(80, 17);
            label10.TabIndex = 7;
            label10.Text = "当前项目号：";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btn_LoadBootFile);
            groupBox2.Controls.Add(btn_LoadAppFile);
            groupBox2.Controls.Add(AppInfo);
            groupBox2.Controls.Add(BootInfo);
            groupBox2.Location = new Point(35, 132);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(660, 242);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "第二步.加载打包文件";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btn_IntegratedPkg);
            groupBox3.Location = new Point(35, 390);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(660, 57);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "第三步.合成一体包";
            // 
            // label_AppAdd
            // 
            label_AppAdd.AutoSize = true;
            label_AppAdd.Location = new Point(405, 69);
            label_AppAdd.Name = "label_AppAdd";
            label_AppAdd.Size = new Size(72, 17);
            label_AppAdd.TabIndex = 12;
            label_AppAdd.Text = "XXXXXXXX";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(331, 69);
            label13.Name = "label13";
            label13.Size = new Size(68, 17);
            label13.TabIndex = 11;
            label13.Text = "App地址：";
            // 
            // label_BootAdd
            // 
            label_BootAdd.AutoSize = true;
            label_BootAdd.Location = new Point(569, 69);
            label_BootAdd.Name = "label_BootAdd";
            label_BootAdd.Size = new Size(72, 17);
            label_BootAdd.TabIndex = 14;
            label_BootAdd.Text = "XXXXXXXX";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(495, 69);
            label14.Name = "label14";
            label14.Size = new Size(72, 17);
            label14.TabIndex = 13;
            label14.Text = "Boot地址：";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "XR一体包打包工具" + ApplicationVersion;
            AppInfo.ResumeLayout(false);
            AppInfo.PerformLayout();
            BootInfo.ResumeLayout(false);
            BootInfo.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
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
        private ComboBox comboBox_SelectPkgInfo;
        private Label label9;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label10;
        private Label label_ProCode;
        private Label label_ChipType;
        private Label label12;
        private Label label_AppAdd;
        private Label label13;
        private Label label_BootAdd;
        private Label label14;
    }
}
