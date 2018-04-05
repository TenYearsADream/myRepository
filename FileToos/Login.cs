using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Sino.Common;

namespace FileTools
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TxtPassword.Text) && !string.IsNullOrWhiteSpace(TxtUserLog.Text))
            {
                try
                {
                    Sino.Bll.USERBLL UserBLL = new Sino.Bll.USERBLL();
                    if (UserBLL.GetIsLog(TxtUserLog.Text, StrUtil.Md5(TxtPassword.Text)))
                    {
                        BaseForm.UserName = TxtUserLog.Text;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                MessageBox.Show("请输入用户名与密码！", "提示", MessageBoxButtons.OK);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            notifyIcon1.Icon = new Icon("icon.ico");
            notifyIcon1.Visible = false;
            notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            this.SizeChanged += new System.EventHandler(this.Login_SizeChanged);
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
                this.Activate();
                this.notifyIcon1.Visible = false;
                this.ShowInTaskbar = true;
            }
        }

        private void Login_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;
            }
        }
    }
} 