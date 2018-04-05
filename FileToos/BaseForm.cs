using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileTools
{
    public partial class BaseForm : Form
    {
        #region 传参参数定义
        /// <summary>
        /// 登陆名
        /// </summary>
        public static string UserName { get; set; }
        public int ihight, iwidth;
        #endregion               

        public BaseForm()
        {
            InitializeComponent();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowException(Exception ex)
        {
            Type T = ex.GetType();
            MessageBox.Show("异常：" + ex.Message, T.Name + "异常");

        }
    }
}
