using System;

namespace Sino.Model
{
    /// <summary>
    /// ST_USER:ʵ����(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// �û�Id
        /// </summary>
        public string USERID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// ��½��
        /// </summary>
        public string LOGIN_NAME
        {
            set { _login_name = value; }
            get { return _login_name; }
        }
        /// <summary>
        /// �û���
        /// </summary>
        public string USER_NAME
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string PASSWORD
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// �Ƿ���� Y������
        /// </summary>
        public string INVALID
        {
            set { _invalid = value; }
            get { return _invalid; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int ORDERBYID
        {
            set { _orderbyid = value; }
            get { return _orderbyid; }
        }
        /// <summary>
        /// �ֻ���
        /// </summary>
        public string MOBILE
        {
            set { _mobile = value; }
            get { return _mobile; }
        }

        /// <summary>
        /// �����ʼ�
        /// </summary>
        public string EMAIL
        {
            set { _email = value; }
            get { return _email; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public string DEPART_ID
        {
            set { _depart_id = value; }
            get { return _depart_id; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string DEPARTMENT
        {
            set { _department = value; }
            get { return _department; }
        }
        /// <summary>
        /// �Ա� 0�� 1Ů
        /// </summary>
        public int SEX
        {
            set { _sex = value; }
            get { return _sex; }
        }


        /// <summary>
        /// ��ע
        /// </summary>
        public string REMARK
        {
            set { _remark = value; }
            get { return _remark; }
        }

        /// <summary>
        /// Ա�����
        /// </summary>
        public int USER_LEVEL
        {
            set { _user_level = value; }
            get { return _user_level; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string WORKNO
        {
            set { _workno = value; }
            get { return _workno; }
        }
        #endregion Model

    }
}

