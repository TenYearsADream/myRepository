using System;

namespace Sino.Model
{
    /// <summary>
    /// ST_USER:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class USER
    {
        public USER()
        { }
        #region Model
        private string _userid;
        private string _login_name;
        private string _user_name;
        private string _password;
        private string _invalid;
        private int _orderbyid;
        private string _mobile;
        private string _email;
        private string _depart_id;
        private string _department;
        private int _sex;
        private string _remark;
        private int _user_level;
        private string _workno;
        /// <summary>
        /// 
        /// 
        /// 用户Id
        /// </summary>
        public string USERID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 登陆名
        /// </summary>
        public string LOGIN_NAME
        {
            set { _login_name = value; }
            get { return _login_name; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string USER_NAME
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string PASSWORD
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 是否可用 Y不可用
        /// </summary>
        public string INVALID
        {
            set { _invalid = value; }
            get { return _invalid; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int ORDERBYID
        {
            set { _orderbyid = value; }
            get { return _orderbyid; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string MOBILE
        {
            set { _mobile = value; }
            get { return _mobile; }
        }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string EMAIL
        {
            set { _email = value; }
            get { return _email; }
        }

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DEPART_ID
        {
            set { _depart_id = value; }
            get { return _depart_id; }
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DEPARTMENT
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// 性别 0男 1女
        /// </summary>
        public int SEX
        {
            set { _sex = value; }
            get { return _sex; }
        }


        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK
        {
            set { _remark = value; }
            get { return _remark; }
        }

        /// <summary>
        /// 员工层次
        /// </summary>
        public int USER_LEVEL
        {
            set { _user_level = value; }
            get { return _user_level; }
        }

        /// <summary>
        /// 工号
        /// </summary>
        public string WORKNO
        {
            set { _workno = value; }
            get { return _workno; }
        }
        #endregion Model

    }
}

