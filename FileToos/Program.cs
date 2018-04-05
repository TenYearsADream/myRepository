using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileTools
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            int i = 0;
            foreach (Process vProcess in Process.GetProcesses())
            {
                if (vProcess.ProcessName == "FileTools")
                {
                    i++;
                    if (i >= 2)
                    {
                        MessageBox.Show("对不起！你已经运行了本系统不可同时运行相同系统！", "提示");
                        return;
                    }
                }
            }
            Login log = new Login();
            Sunisoft.IrisSkin.SkinEngine skin = new Sunisoft.IrisSkin.SkinEngine((System.ComponentModel.Component)log);
            skin.SkinFile = "MSN.ssk"; //指定皮肤文件
            skin.TitleFont = new System.Drawing.Font("微软雅黑", 10F);// 指定标题栏的Font。
            Application.Run(log);
            //登录用户
            if (log.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                UploadForm bf = new UploadForm();
                Sunisoft.IrisSkin.SkinEngine skin1 = new Sunisoft.IrisSkin.SkinEngine((System.ComponentModel.Component)bf);
                skin1.SkinFile = "MSN.ssk"; //指定皮肤文件
                skin1.TitleFont = new System.Drawing.Font("微软雅黑", 10F);// 指定标题栏的Font。
                Application.Run(bf);
            }
        }
    }
}