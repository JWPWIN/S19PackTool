namespace S19PackToolPro.Wins
{
    partial class ComProcessBarWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TaskProgressBar = new ProgressBar();
            ConsumeTimelabel = new Label();
            SuspendLayout();
            // 
            // TaskProgressBar
            // 
            TaskProgressBar.Location = new Point(39, 72);
            TaskProgressBar.Maximum = 101;
            TaskProgressBar.Name = "TaskProgressBar";
            TaskProgressBar.Size = new Size(251, 23);
            TaskProgressBar.TabIndex = 0;
            // 
            // ConsumeTimelabel
            // 
            ConsumeTimelabel.AutoSize = true;
            ConsumeTimelabel.Location = new Point(39, 52);
            ConsumeTimelabel.Name = "ConsumeTimelabel";
            ConsumeTimelabel.Size = new Size(43, 17);
            ConsumeTimelabel.TabIndex = 1;
            ConsumeTimelabel.Text = "";
            // 
            // ComProcessBarWin
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 200);
            Controls.Add(ConsumeTimelabel);
            Controls.Add(TaskProgressBar);
            Name = "ComProcessBarWin";
            Text = "当前任务进度";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar TaskProgressBar;
        private Label ConsumeTimelabel;
    }
}