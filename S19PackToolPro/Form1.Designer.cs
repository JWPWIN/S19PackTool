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
            label2 = new Label();
            Text_AppLen = new TextBox();
            label1 = new Label();
            Text_AppStartAdr = new TextBox();
            BootInfo = new GroupBox();
            label3 = new Label();
            Text_BootLen = new TextBox();
            label4 = new Label();
            Text_BootStartAdr = new TextBox();
            btn_LoadBootFile = new Button();
            btn_IntegratedPkg = new Button();
            AppInfo.SuspendLayout();
            BootInfo.SuspendLayout();
            SuspendLayout();
            // 
            // btn_LoadAppFile
            // 
            btn_LoadAppFile.Location = new Point(40, 21);
            btn_LoadAppFile.Name = "btn_LoadAppFile";
            btn_LoadAppFile.Size = new Size(133, 33);
            btn_LoadAppFile.TabIndex = 0;
            btn_LoadAppFile.Text = "加载App文件";
            btn_LoadAppFile.UseVisualStyleBackColor = true;
            btn_LoadAppFile.Click += Btn_LoadAppFile_Click;
            // 
            // AppInfo
            // 
            AppInfo.Controls.Add(label2);
            AppInfo.Controls.Add(Text_AppLen);
            AppInfo.Controls.Add(label1);
            AppInfo.Controls.Add(Text_AppStartAdr);
            AppInfo.Location = new Point(40, 69);
            AppInfo.Name = "AppInfo";
            AppInfo.Size = new Size(218, 240);
            AppInfo.TabIndex = 1;
            AppInfo.TabStop = false;
            AppInfo.Text = "App信息";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 63);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 3;
            label2.Text = "App长度";
            // 
            // Text_AppLen
            // 
            Text_AppLen.Location = new Point(86, 60);
            Text_AppLen.Name = "Text_AppLen";
            Text_AppLen.Size = new Size(100, 23);
            Text_AppLen.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 34);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 1;
            label1.Text = "App起始地址";
            // 
            // Text_AppStartAdr
            // 
            Text_AppStartAdr.Location = new Point(86, 31);
            Text_AppStartAdr.Name = "Text_AppStartAdr";
            Text_AppStartAdr.Size = new Size(100, 23);
            Text_AppStartAdr.TabIndex = 0;
            // 
            // BootInfo
            // 
            BootInfo.Controls.Add(label3);
            BootInfo.Controls.Add(Text_BootLen);
            BootInfo.Controls.Add(label4);
            BootInfo.Controls.Add(Text_BootStartAdr);
            BootInfo.Location = new Point(316, 69);
            BootInfo.Name = "BootInfo";
            BootInfo.Size = new Size(218, 240);
            BootInfo.TabIndex = 2;
            BootInfo.TabStop = false;
            BootInfo.Text = "Boot信息";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 63);
            label3.Name = "label3";
            label3.Size = new Size(60, 17);
            label3.TabIndex = 3;
            label3.Text = "Boot长度";
            // 
            // Text_BootLen
            // 
            Text_BootLen.Location = new Point(86, 60);
            Text_BootLen.Name = "Text_BootLen";
            Text_BootLen.Size = new Size(100, 23);
            Text_BootLen.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 34);
            label4.Name = "label4";
            label4.Size = new Size(84, 17);
            label4.TabIndex = 1;
            label4.Text = "Boot起始地址";
            // 
            // Text_BootStartAdr
            // 
            Text_BootStartAdr.Location = new Point(86, 31);
            Text_BootStartAdr.Name = "Text_BootStartAdr";
            Text_BootStartAdr.Size = new Size(100, 23);
            Text_BootStartAdr.TabIndex = 0;
            // 
            // btn_LoadBootFile
            // 
            btn_LoadBootFile.Location = new Point(316, 21);
            btn_LoadBootFile.Name = "btn_LoadBootFile";
            btn_LoadBootFile.Size = new Size(133, 33);
            btn_LoadBootFile.TabIndex = 3;
            btn_LoadBootFile.Text = "加载Boot文件";
            btn_LoadBootFile.UseVisualStyleBackColor = true;
            btn_LoadBootFile.Click += btn_LoadBootFile_Click;
            // 
            // btn_IntegratedPkg
            // 
            btn_IntegratedPkg.Location = new Point(40, 352);
            btn_IntegratedPkg.Name = "btn_IntegratedPkg";
            btn_IntegratedPkg.Size = new Size(133, 38);
            btn_IntegratedPkg.TabIndex = 4;
            btn_IntegratedPkg.Text = "合成一体包";
            btn_IntegratedPkg.UseVisualStyleBackColor = true;
            btn_IntegratedPkg.Click += btn_IntegratedPkg_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btn_IntegratedPkg);
            Controls.Add(btn_LoadBootFile);
            Controls.Add(BootInfo);
            Controls.Add(AppInfo);
            Controls.Add(btn_LoadAppFile);
            Name = "Form1";
            Text = "Form1";
            AppInfo.ResumeLayout(false);
            AppInfo.PerformLayout();
            BootInfo.ResumeLayout(false);
            BootInfo.PerformLayout();
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
    }
}
