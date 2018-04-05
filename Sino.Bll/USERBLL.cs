using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sino.Model;
using Sino.Dal;
using System.Data;
using Sino.DAL;

namespace Sino.Bll
{
    public class USERBLL
    {

        /// <summary>
        /// 获取是否登录成功
        /// </summary>
        /// <param name="LogName">登录名</param>
        /// <param name="LogPassword">登录密码</param>
        /// <returns></returns>
        public bool GetIsLog(string UserName, string LogPassword)
        {
            if (!string.IsNullOrWhiteSpace(UserName)&&!string.IsNullOrWhiteSpace(LogPassword))
            {
                try
                {
                    string StrSql = "SELECT COUNT(1) FROM ST_USER WHERE (USER_NAME='" + UserName + "' OR LOGIN_NAME='" + UserName + "') AND PASSWORD='" + LogPassword + "'";
                    return (int)SqlHelper.ExecuteScalar(StrSql) > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户登录名获取用户
        /// </summary>
        /// <param name="LogName"></param>
        /// <returns></returns>
        public USER GetModel(string userName)
        {
            try
            {
                USER UserInfo = new USER();
                UserInfo.USER_NAME = userName;
                return SqlHelper.GetModels<USER>(UserInfo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}