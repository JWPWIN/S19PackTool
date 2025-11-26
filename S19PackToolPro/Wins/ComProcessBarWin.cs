using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace S19PackToolPro.Wins
{
    public partial class ComProcessBarWin : Form
    {
        public ComProcessBarWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 异步调用任务进度条，进度条值每秒+1(最大100s)，当任务完成后将设为100，结束进度条
        /// 每调用一次该异步函数，将执行一次进度条事件，要与外部异步耗时进程配合使用，外部任务执行完成后可将进度条值设置为100，结束该进程
        /// </summary>
        public async Task TaskProcessTipAsync(string taskName)
        {
            this.Show();
            this.Text = taskName;
            this.TopMost = true;
            this.TaskProgressBar.Value = 0;
            this.ConsumeTimelabel.Text = "当前耗时:" + 0.ToString() + "s";

            while (this.TaskProgressBar.Value < 100)
            {
                await Task.Delay(1000);
                this.TaskProgressBar.Value++;
                //因为存在异步函数操作同一value，因此需要判断value小于100时显示消耗时间
                if (this.TaskProgressBar.Value < 100)
                {
                    this.ConsumeTimelabel.Text = "当前耗时:" + this.TaskProgressBar.Value.ToString() + "s";
                }
            }

            await Task.Delay(1000);

            this.Hide();
        }

        /// <summary>
        /// 设置任务进度条值
        /// </summary>
        public void SetTaskProcessBarCurValue(int curValue)
        {
            this.TaskProgressBar.Value = curValue;
        }

    }
}
