using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Configuration;
using Sino.Bll;
using Sino.Model;
using System.Diagnostics;
using SinoSoft.Common;

namespace FileTools
{
    public partial class UploadForm : BaseForm
    {
        public List<string> list = new List<string>();
        public Sino.Model.Attachment at = new Attachment();
        /// <summary>
        /// 上传文件夹路径
        /// </summary>
        public string Url;
        public DateTime startDate;
        public List<FileInfo> FilePaths=new List<FileInfo>();
        /// <summary>
        /// 文件夹名
        /// </summary>
        public string FolderName;
        public int listNumber = 0;
        public int init = 0;
        public int initchild = 0;
        public int initp = 0;
        public int num = 0, pi = 0, cc = 0;
        public UploadForm()
        {            
            InitializeComponent();
            LoadListView();
            this.FormClosed += (sender, e) =>
            {
                Application.Exit();
                System.Diagnostics.Process pro = System.Diagnostics.Process.GetCurrentProcess();
                pro.Kill();
            };
        }
        /// <summary>  
        /// 初始化上传列表  
        /// </summary>  
        void LoadListView()
        {
            listView1.View = View.Details;
            listView1.CheckBoxes = true;
            listView1.GridLines = true;
            listView1.Columns.Add("序号", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("文件名", 150, HorizontalAlignment.Center);
            listView1.Columns.Add("大小", 150, HorizontalAlignment.Center);
            listView1.Columns.Add("原始大小", 150, HorizontalAlignment.Center);
            listView1.Columns.Add("文件路径", 150, HorizontalAlignment.Center);
        }

        /// <summary>  
        /// 存储目录  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }

        private void UploadForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = ConfigurationSettings.AppSettings["SavePath"].ToString();//存放的路径 固定写死
            textBox1.Enabled = false;
            button2.Enabled = false;
            GetDictory();
        }

        private void GetDictory()
        {
            comboBox1.DisplayMember = "PRO_CONTENT";
            comboBox1.ValueMember = "ID";
            DataTable dt = new DICTIONARYBLL().GetDictionary("档案类别").Tables[0];
            comboBox1.DataSource = dt;

            comboBox2.DisplayMember = "PRO_CONTENT";
            comboBox2.ValueMember = "ID";
            DataTable dt1 = new DICTIONARYBLL().GetDictionary("语种").Tables[0];
            comboBox2.DataSource = dt1;

            comboBox3.DisplayMember = "PRO_CONTENT";
            comboBox3.ValueMember = "ID";
            DataTable dt2 = new DICTIONARYBLL().GetDictionary("领域").Tables[0];
            comboBox3.DataSource = dt2;

            int Year = DateTime.Now.Year;
            List<FileyearInfo> listy = new List<FileyearInfo>();
            for (int i = Year - 10; i < Year + 10; i++)
            {
                FileyearInfo yi = new FileyearInfo();
                yi.name = i + "";
                yi.year = i;
                listy.Add(yi);
            }
            comboBox4.DisplayMember = "name";
            comboBox4.ValueMember = "Year";
            comboBox4.DataSource = listy;
            comboBox4.SelectedValue = Year;
        }

        /// <summary>  
        /// 打开上传文件夹 
        /// </summary>  
        /// <param name="sender"></param>  
        /// /// <param name="e"></param>  
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dilog = new FolderBrowserDialog();
            dilog.Description = "请选择文件夹";
            if (dilog.ShowDialog() == DialogResult.OK || dilog.ShowDialog() == DialogResult.Yes)
            {
                list = new List<string>();
                listView1.Items.Clear();
                textBoxFile.Text = "";
                init = 0;
                initchild = 0;
                listNumber = 0;
                if (textBoxFile.Text != dilog.SelectedPath)
                {
                    textBoxFile.Text = dilog.SelectedPath;
                    Url = textBoxFile.Text;
                    DirectoryInfo theFolder = new DirectoryInfo(textBoxFile.Text);
                    try
                    {
                        this.label8.Text = "文件总计：" + GetFileList(theFolder) + "个文件";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("遍历文件夹内容异常.异常原因：文件太多或文件访问不被允许。" + ex.Message);
                    }
                }
                else
                {

                }
            }
        }
        private int GetFileList(DirectoryInfo theFolder)
        {
            FileInfo[] allFile = theFolder.GetFiles().Where(c => c.Extension.Contains("pdf") || c.Extension.Contains("doc") || c.Extension.Contains("ppt") || c.Extension.Contains("xls")).ToArray();
            foreach (FileInfo fi in allFile)
            {
                listNumber++;
                ListViewItem lvi = new ListViewItem();
                lvi.Checked = true;
                lvi.Text = listNumber + "";
                lvi.Tag = fi.FullName;
                lvi.SubItems.Add(Path.GetFileNameWithoutExtension(fi.FullName));
                if (fi.Length / (1024 * 1024) < 1)
                {
                    lvi.SubItems.Add((fi.Length / 1024).ToString() + "KB");
                }
                else
                {
                    lvi.SubItems.Add((fi.Length / (1024 * 1024)).ToString() + "M");
                }
                lvi.SubItems.Add(fi.Length.ToString());
                lvi.SubItems.Add(Path.GetDirectoryName(fi.FullName));
                listView1.Items.Add(lvi);
                init++;
                //把文件路经存放到FileList中
                FilePaths.Add(fi);
            }
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                GetFileList(NextFolder);
            }
            return init;
        }

        public delegate void DeleFile(int position);
        /// <summary>  
        /// 文件上传  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Equals(""))
            {
                MessageBox.Show("请先设置存储目录..");
            }
            else if (this.textBoxFile.Text.Trim().Equals(""))
            {
                MessageBox.Show("请选择文件夹..");
            }
            else
            {
                //button3.Enabled = false;
                at.dType = comboBox1.Text;
                at.language = comboBox2.Text;
                at.area = comboBox3.Text;
                at.OrderBy = this.txtOrderBy.Text;
                //at.Contents = txtRemarks.Text;
                at.KeyWords = txtKeyWord.Text;
                at.year = int.Parse(this.comboBox4.SelectedValue.ToString());
                initchild = 0;
                Thread th = new Thread(new ThreadStart(DoFileUpload));                
                th.Start();
            }
        }
        private void Thread_timer_method(object o)
        {
            label1.Text = Convert.ToString((++num));
            pi++;
            if (pi < 100)
            {
                progressBar1.Value = pi;
            }
            else
            {
                pi = 1;
            }
            System.Threading.Thread.Sleep(3000);
        }
        private void DoFileUpload()
        {
            DirectoryInfo theFolder = new DirectoryInfo(Url);
            //获取年
            string Year = DateTime.Now.Year.ToString();
            //获取月
            string Month = DateTime.Now.Month.ToString();
            //获取日
            string Day = DateTime.Now.Day.ToString();
            string hm = DateTime.Now.Hour.ToString() + "" + DateTime.Now.Minute.ToString();
            string timestamp = Year + Month + Day + hm;

            string day = DateTime.Now.Day.ToString();
            string strDirectory = ConfigurationSettings.AppSettings["SavePath"].ToString();//存放的路径 固定写死  参见配置文件
            string DictoryFileName = Year + @"\" + Month + @"\" + day + @"\";
            string LogDirectory = strDirectory + "Log";//日志文件
            //定义名称为当天日期的文件夹的路径
            string path = strDirectory + @"\" + DictoryFileName;
            
            int NumSuccess = 0;
            int NumError = 0;
            int NumAll = FilePaths.Count();
            startDate = DateTime.Now;//开始导入数据时间
            foreach (FileInfo fi in FilePaths)
            {
                //虚拟路径
                string tempVirtualPath = fi.FullName.Replace(Url.Remove(Url.LastIndexOf('\\')), string.Empty).Remove(0, 1);
                at.VirtualPath = tempVirtualPath.Remove(tempVirtualPath.LastIndexOf('\\'));
                try
                {
                    bool ConvertResult = false;
                    string destFileName = path + at.VirtualPath + "\\" + fi.Name.Remove(fi.Name.LastIndexOf('.')) + ".pdf";
                    //创建文件夹
                    if (!Directory.Exists(path + at.VirtualPath))
                    {
                        Directory.CreateDirectory(path + at.VirtualPath);
                    }
                    if (".pdf".Contains(fi.Extension))
                    {
                        try
                        {
                            File.Copy(fi.FullName, destFileName, true);
                            ConvertResult = true;
                        }
                        catch (Exception ex)
                        {
                            ConvertResult = false;
                        }
                    }
                    else if (".doc.docx".Contains(fi.Extension))
                    {
                        ConvertResult = FileConversion.WordToPDF(fi.FullName, destFileName);
                    }
                    else if (".xls.xlsx".Contains(fi.Extension))
                    {
                        ConvertResult = FileConversion.ExcelToPDF(fi.FullName, destFileName);
                    }
                    else if (".ppt.pptx".Contains(fi.Extension))
                    {
                        ConvertResult = FileConversion.PowerPointToPDF(fi.FullName, destFileName);
                    }
                    //判断文件复制、转换是否成功
                    if (ConvertResult)
                    {
                        //写入数据库
                        at.Suffix = fi.Extension;
                        at.FileName = fi.Name.Remove(fi.Name.LastIndexOf('.'));
                        at.CreateDate = DateTime.Now;
                        at.BateSize = decimal.Round(decimal.Round(fi.Length) / (1024 * 1024), 2);                        
                        at.BatchNo = timestamp;
                        at.FtpPath = path.Replace(@"E:\", string.Empty);
                        at.FtpName = fi.Name;
                        at.CreateBy = BaseForm.UserName;
                        at.filetag = "0";
                        if (new Sino.Bll.AttachmentBLL().Add(at))
                        {                            
                            //删除源文件
                            File.Delete(fi.FullName);
                            //kill进程
                            KillProcess("WINWORD");
                            KillProcess("EXCEL");
                            NumSuccess++;
                        }
                    }
                    else
                    {
                        NumError++;
                        string Text = "【PDF转换失败】：" + fi.FullName + "\r\n【操作时间】：" + DateTime.Now + "\r\n【操作人】：" + BaseForm.UserName + "\r\n===========================================";
                        WritfLogText(LogDirectory, timestamp, Text);
                    }
                    //更新归档状态和进度条更新
                    this.Invoke(new Action<int, int>((numSuccess, numError) =>
                    {
                        progressBar1.Value = (numSuccess + numError) * 100 / NumAll;
                        label1.Text = "成功导入：" + numSuccess + "个文件\r\n\r\n失败：" + NumError + "个文件";
                    }), NumSuccess, NumError);
                }
                catch
                { }
            }
            //批量加入关键字数据
            if (!string.IsNullOrWhiteSpace(this.txtKeyWord.Text.Trim()))
            {
                try
                {
                    DataTable dtAtt = new AttachmentBLL().GetListID(startDate, UserName);
                    DataTable dtEva = new DataTable();
                    dtEva.Columns.Add("UserName", typeof(string));
                    dtEva.Columns.Add("OperateTime", typeof(string));
                    dtEva.Columns.Add("UserName", typeof(string));
                    dtEva.Columns.Add("Fid", typeof(int));
                    if (dtAtt != null)
                    {
                        foreach (DataRow row in dtAtt.Rows)
                        {
                            DataRow rowEva = dtEva.NewRow();
                            rowEva["UserName"] = UserName;
                            rowEva["Fid"] = int.Parse(row["ID"].ToString());
                            rowEva["KeyWords"] = this.txtKeyWord.Text.Trim(); ;
                            rowEva["OperateTime"] = DateTime.Now.ToShortDateString();
                            dtEva.Rows.Add(rowEva);
                        }
                    }
                    new EvaluateBLL().InserDataTable(dtEva, "BT_Evaluate");
                }
                catch
                { 
                
                }
            }
            this.Invoke(new Action(() =>
            {
                MessageBox.Show("上传文件成功！", "提示", MessageBoxButtons.OK);
            }));
        }

        private void WritfLogText(string LogDirectory, string timestamp, string Text)
        {

            DirectoryInfo dir = new DirectoryInfo(LogDirectory);
            dir.Create();//自行判断一下是否存在。 
            string textLogFile = LogDirectory + "\\Log" + timestamp + ".txt";
            if (File.Exists(textLogFile))
            {
                System.IO.File.AppendAllText(textLogFile, "\r\n" + Text);
            }
            else
            {
                Process.Start("notepad", textLogFile);
                System.IO.File.AppendAllText(textLogFile, Text + ";\r\n");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //刷新
            list = new List<string>();
            listView1.Items.Clear();
            textBoxFile.Text = "";
            this.label1.Text = "";
            init = 0;
            initchild = 0;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            FileTimeInfo fi = GetLatestFileTimeInfo(ConfigurationSettings.AppSettings["SavePath"].ToString() + @"Log", ".txt");
            if (fi != null)
            {
                System.Diagnostics.Process.Start("notepad.exe", fi.FileName);
            }
            else
            {
                MessageBox.Show("文件夹中没有产生日志文件！", "提示", MessageBoxButtons.OK);
            }
        }
        //获取最近创建的文件名和创建时间 
        //如果没有指定类型的文件，返回null 
        static FileTimeInfo GetLatestFileTimeInfo(string dir, string ext)
        {
            List<FileTimeInfo> list = new List<FileTimeInfo>();
            DirectoryInfo d = new DirectoryInfo(dir);
            foreach (FileInfo fi in d.GetFiles())
            {
                if (fi.Extension.ToUpper() == ext.ToUpper())
                {
                    list.Add(new FileTimeInfo()
                    {
                        FileName = fi.FullName,
                        FileCreateTime = fi.CreationTime
                    });
                }
            }
            var qry = from x in list
                      orderby x.FileCreateTime
                      select x;
            return qry.LastOrDefault();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pi++;
            if (pi < 100)
            {
                progressBar1.Value = pi;
            }
            else
            {
                pi = 1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            KillProcess("WINWORD");//EXCEL
            KillProcess("EXCEL");
            MessageBox.Show("WINWORD、EXECL进程关闭成功！", "提示", MessageBoxButtons.OK);
        }

        private void KillProcess(string strName)
        {
            Process[] ps = Process.GetProcesses();
            foreach (Process item in ps)
            {
                if (item.ProcessName == strName)
                {
                    item.Kill();
                }
            }
        }
    }
    //自定义一个类 

    public class FileTimeInfo
    {
        public string FileName;  //文件名 
        public DateTime FileCreateTime; //创建时间 
    }

    //自定义一个类 

    public class FileyearInfo
    {
        public int year { get; set; }//文件名 
        public string name { get; set; }//创建时间 
    }
}