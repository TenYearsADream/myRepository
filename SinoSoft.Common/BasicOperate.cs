using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SinoSoft.Common
{
    /// <summary>
    /// 基本数据操作。
    /// </summary>
    /// <!--
    /// 创建人  : Zhouyinglu
    /// 创建时间: 2011-02-26
    /// -->
    public class BasicOperate
    {
        /// <summary>
        /// 转换泛型集合为数组。
        /// </summary>
        /// <param name="collection">泛型集合。</param>
        /// <returns>数组。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static T[] ConvertCollectionToArray<T>(IList<T> collection)
        {
            T[] array = null;
            if (null != collection)
            {
                array = new T[collection.Count];
                collection.CopyTo(array, 0);
            }
            return array;
        }

        /// <summary>
        /// 将一个泛型集合连接成字符串。
        /// </summary>
        /// <typeparam name="T">泛型。</typeparam>
        /// <param name="separator">分割符。</param>
        /// <param name="values">泛型集合。</param>
        /// <returns></returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static string Join<T>(string separator, IList<T> values)
        {
            T[] arrT = ConvertCollectionToArray(values);
            return Join<T>(separator, arrT);
        }

        /// <summary>
        /// 将一个泛型数组连接成字符串。
        /// </summary>
        /// <typeparam name="T">泛型。</typeparam>
        /// <param name="separator">分割符。</param>
        /// <param name="values">泛型数组。</param>
        /// <returns></returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static string Join<T>(string separator, T[] values)
        {
            string result = string.Empty;
            string s = string.IsNullOrEmpty(separator) ? "," : separator;
            if (values != null)
            {
                foreach (T t in values)
                {
                    result += t.ToString() + s;
                }
                result = result.TrimEnd(s.ToCharArray());
            }
            return result;
        }

        /// <summary>
        /// 将一个泛型数组连接成字符串。
        /// </summary>
        /// <typeparam name="T">泛型。</typeparam>
        /// <param name="separator">分割符。</param>
        /// <param name="values">泛型数组。</param>
        /// <returns></returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static IList<long> Split(char separator, string values)
        {
            IList<long> result = null;
            if (!string.IsNullOrEmpty(values))
            {
                result = new List<long>();
                foreach (string t in values.Split(separator))
                {
                    result.Add(GetLong(t));
                }
            }
            return result;
        }

        /// <summary>
        /// 对两个泛型集合求交。
        /// </summary>
        /// <typeparam name="T">泛型。</typeparam>
        /// <param name="A">求交前泛型集合。</param>
        /// <param name="B">求交前泛型集合。</param>
        /// <returns>求并后泛型集合。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static IList<T> Intersection<T>(IList<T> A, IList<T> B)
        {
            if (A == null || B == null)
                return null;
            IList<T> C = new List<T>();
            foreach (T a in A)
            {
                foreach (T b in B)
                {
                    if (a.Equals(b))
                    {
                        C.Add(a);
                    }
                }
            }
            return C;
        }

        /// <summary>
        /// 对两个泛型数组求交。
        /// </summary>
        /// <typeparam name="T">泛型。</typeparam>
        /// <param name="arrA">求交前泛型数组。</param>
        /// <param name="arrB">求交前泛型数组。</param>
        /// <returns>求并后泛型数组。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static IList<T> Intersection<T>(T[] arrA, T[] arrB)
        {
            if (arrA == null || arrB == null)
                return null;
            IList<T> C = new List<T>();
            foreach (T a in arrA)
            {
                foreach (T b in arrA)
                {
                    if (a.Equals(b))
                    {
                        C.Add(a);
                    }
                }
            }
            return C;
        }

        /// <summary>
        /// 判断对象是否包含于指定数组。
        /// </summary>
        /// <typeparam name="T">泛型。</typeparam>
        /// <param name="array">数组。</param>
        /// <param name="t">泛型对象。</param>
        /// <returns></returns>
        public static bool Contains<T>(T[] array, T t)
        {
            if (array == null)
                return false; ;
            IList<T> C = new List<T>();
            foreach (T a in array)
            {
                C.Add(a);
            }
            return C.Contains(t);
        }

        /// <summary>
        /// 获取新的32位字符串形式的Guid。
        /// </summary>
        /// <returns>32位字符串形式的Guid。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 将空字符转化成默认字符串。
        /// </summary>
        /// <param name="strOld">原字符串（可能为空）</param>
        /// <param name="strNew">默认字符串</param>
        /// <returns>不为空的字符串</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-12-15
        /// -->
        public static string StringNvl(string strOld, string strNew)
        {
            return string.IsNullOrEmpty(strOld) ? strNew : strOld;
        }

        #region 获取日期

        /// <summary>
        /// 当前月有多少天
        /// </summary>
        /// <param name="y"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static int GetMonthDay(int y, int m)
        {
            int mnext;
            int ynext;
            if (m < 12)
            {
                mnext = m + 1;
                ynext = y;
            }
            else
            {
                mnext = 1;
                ynext = y + 1;
            }
            DateTime dt1 = System.Convert.ToDateTime(y + "-" + m + "-1");
            DateTime dt2 = System.Convert.ToDateTime(ynext + "-" + mnext + "-1");
            TimeSpan diff = dt2 - dt1;
            return diff.Days;
        }

        /// <summary>
        /// 得到一年中的某周的起始日和截止日
        ///   nYear
        /// 周数 nNumWeek
        /// 周始 out dtWeekStart
        /// 周终 out dtWeekeEnd
        /// </summary>
        /// <param name="nYear">年</param>
        /// <param name="nNumWeek">年</param>
        /// <param name="dtWeekStart"></param>
        /// <param name="dtWeekeEnd"></param>
        public static void GetWeek(int nYear, int nNumWeek, out   DateTime dtWeekStart, out   DateTime dtWeekeEnd)
        {
            DateTime dt = new DateTime(nYear, 1, 1);
            dt = dt + new TimeSpan((nNumWeek - 1) * 7, 0, 0, 0);
            dtWeekStart = dt.AddDays(-(int)dt.DayOfWeek + (int)DayOfWeek.Monday);
            dtWeekeEnd = dt.AddDays((int)DayOfWeek.Saturday - (int)dt.DayOfWeek + 1);
        }

        /// <summary>
        /// 求某年有多少周
        /// 返回 int
        /// </summary>
        /// <param name="strYear"></param>
        /// <returns>int</returns>
        public static int GetYearWeekCount(int strYear)
        {
            DateTime fDt = BasicOperate.GetDateTime(strYear.ToString() + "-01-01");
            int k = Convert.ToInt32(fDt.DayOfWeek);//得到该年的第一天是周几 
            if (k == 1)
            {
                int countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 1;
                return countWeek;

            }
            else
            {
                int countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 2;
                return countWeek;
            }
        }

        /// <summary>
        /// 求当前日期是一年的中第几周
        /// </summary>
        /// <param name="curDay">日期</param>
        /// <returns></returns>
        public static int GetWeekByDate(DateTime curDay)
        {
            int firstdayofweek = Convert.ToInt32(Convert.ToDateTime(curDay.Year.ToString() + "- " + "1-1 ").DayOfWeek);

            int days = curDay.DayOfYear;
            int daysOutOneWeek = days - (7 - firstdayofweek);

            if (daysOutOneWeek <= 0)
            {
                return 1;
            }
            else
            {
                int weeks = daysOutOneWeek / 7;
                if (daysOutOneWeek % 7 != 0)
                    weeks++;

                return weeks + 1;
            }
        }

        /// <summary>
        /// 当天与该周星期一相差的天数
        /// </summary>
        /// <param name="dw">指定天数</param>
        /// <!--
        /// 创建人  : MaGuoFu
        /// 创建时间: 2009-03-03
        /// -->
        public static int GetModdaDNum(System.DayOfWeek dw)
        {
            int weeknow = Convert.ToInt32(dw);
            int moddayNum = (-1) * weeknow + 1;
            return moddayNum;
        }

        /// <summary>
        /// 当天与该周星期日相差的天数
        /// </summary>
        /// <param name="dw">指定天数</param>
        /// <!--
        /// 创建人  : MaGuoFu
        /// 创建时间: 2009-03-03
        /// -->
        public static int GetSundayNum(System.DayOfWeek dw)
        {
            int weeknow = Convert.ToInt32(dw);
            int sundayNum = 7 - weeknow;
            return sundayNum;
        }

        /// <summary>
        /// 该周星期一的日期
        /// </summary>
        /// <param name="dt">日期</param>
        /// <!--
        /// 创建人  : MaGuoFu
        /// 创建时间: 2009-03-03
        /// -->
        public static string GetWeekMonday(DateTime dt)
        {
            int weeknow = Convert.ToInt32(dt.DayOfWeek);
            int moddayNum = (-1) * weeknow + 1;
            string weekMonday = dt.AddDays(moddayNum).Date.ToString("yyyy-MM-dd");
            return weekMonday;
        }

        /// <summary>
        /// 该周星期日的日期
        /// </summary>
        /// <param name="dt">日期</param>
        /// <!--
        /// 创建人  : MaGuoFu
        /// 创建时间: 2009-03-03
        /// -->
        public static string GetWeekSunday(DateTime dt)
        {
            int weeknow = Convert.ToInt32(dt.DayOfWeek);
            int sundayNum = 7 - weeknow;
            string weekSunday = dt.AddDays(sundayNum).Date.ToString("yyyy-MM-dd");
            return weekSunday;
        }

        /// <summary>
        /// 本周是本年第几周
        /// </summary>
        /// <param name="dw">指定天数</param>
        /// <!--
        /// 创建人  : MaGuoFu
        /// 创建时间: 2009-03-03
        /// -->
        public static int GetWeekNum(DateTime dete)
        {
            int weeknow = Convert.ToInt32(dete.DayOfWeek);//今天星期几
            int daydiff = (-1) * (weeknow + 1);//今日与上周末的天数差
            int days = dete.AddDays(daydiff).DayOfYear;//上周末是本年第几天
            int weeks = days / 7;
            if (days % 7 != 0)
            {
                weeks++;
            }
            return (weeks + 2);
        }
        #endregion

        #region 数据类型转换

        #region String类型

        /// <summary>
        /// 将时间类型转换为字符串（最大最小值转为空，其它按照配置格式转换）。
        /// </summary>
        /// <param name="value">时间值。</param>
        /// <returns>转换后的字符串。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2007-1-20
        /// -->
        public static string GetString(DateTime value)
        {
            return GetString(value, "yyyy-MM-dd");
        }

        /// <summary>
        /// 将时间类型转换为字符串（最大最小值转为空，其它按照配置格式转换）。
        /// </summary>
        /// <param name="value">时间值。</param>
        /// <param name="format">时间转换格式。</param>
        /// <returns>转换后的字符串。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2007-1-20
        /// -->
        public static string GetString(DateTime value, string format)
        {
            if ((value == DateTime.MinValue) || (value == DateTime.MaxValue))
            {
                return string.Empty;
            }
            else
            {
                return value.ToString(format);
            }
        }

        /// <summary>
        /// 获取数据库中的字符串
        /// </summary>
        /// <param name="obj">object类型的值</param>
        /// <returns>字符串</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static string GetString(object obj)
        {
            return GetString(obj, true, string.Empty);
        }

        /// <summary>
        /// 获取数据库中的字符串。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <returns>字符串。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static string GetString(object obj, bool ifConvert)
        {
            return GetString(obj, ifConvert, string.Empty);
        }

        /// <summary>
        /// 获取数据库中的字符串。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <param name="defaultValue">默认值(obj为空，或转换失败时返回值)。</param>
        /// <returns>字符串。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static string GetString(object obj, bool ifConvert, string defaultValue)
        {
            string result = defaultValue;
            if ((null != obj) && (DBNull.Value != obj))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToString(obj);
                    }
                    catch
                    {
                        result = defaultValue;
                    }
                }
                else
                {
                    result = obj as string;
                    if (null == result)
                    {
                        result = defaultValue;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Decimal类型

        /// <summary>
        /// 将字符类型转换为数字类型。
        /// </summary>
        /// <param name="value">字符类型值。</param>
        /// <returns>转换后的数字。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2007-1-24
        /// -->
        public static decimal GetDecimal(string value)
        {
            decimal result = decimal.Zero;
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim();
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        result = decimal.Parse(value);
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取数据库中的数值类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <returns>数值类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static decimal GetDecimal(object obj)
        {
            return GetDecimal(obj, true, decimal.Zero);
        }

        /// <summary>
        /// 获取数据库中的数值类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <returns>数值类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static decimal GetDecimal(object obj, bool ifConvert)
        {
            return GetDecimal(obj, ifConvert, decimal.Zero);
        }

        /// <summary>
        /// 获取数据库中的数值类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <param name="defaultValue">默认值(obj为空，或转换失败时返回值)。</param>
        /// <returns>数值类型数据。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static decimal GetDecimal(object obj, bool ifConvert, decimal defaultValue)
        {
            decimal result = defaultValue;
            if ((null != obj) && (DBNull.Value != obj))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToDecimal(obj);
                    }
                    catch
                    {
                        result = defaultValue;
                    }
                }
                else
                {
                    if (obj is decimal)
                    {
                        result = (decimal)obj;
                    }
                }
            }
            return result;
        }

        #endregion

        #region DateTime类型

        /// <summary>
        /// 获取数据库中的时间类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <returns>时间类型数据(默认为最小值)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static DateTime GetDateTime(object obj)
        {
            return GetDateTime(obj, true, DateTime.MinValue);
        }

        /// <summary>
        /// 获取数据库中的时间类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <returns>时间类型数据(默认为最小值)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static DateTime GetDateTime(object obj, bool ifConvert)
        {
            return GetDateTime(obj, ifConvert, DateTime.MinValue);
        }

        /// <summary>
        /// 获取数据库中的时间类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <param name="defaultValue">默认值(obj为空，或转换失败时返回值)。</param>
        /// <returns>时间类型数据。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static DateTime GetDateTime(object obj, bool ifConvert, DateTime defaultValue)
        {
            DateTime result = defaultValue;
            if ((null != obj) && (DBNull.Value != obj))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToDateTime(obj);
                    }
                    catch
                    {
                        result = defaultValue;
                    }
                }
                else
                {
                    if (obj is DateTime)
                    {
                        result = (DateTime)obj;
                    }
                }
            }
            return result;
        }

        #endregion

        #region 可为空的DateTime类型

        /// <summary>
        /// 获取数据库中的时间类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <returns>时间类型数据(默认为null)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2007-10-25
        /// -->
        public static Nullable<DateTime> GetNullableDateTime(object obj)
        {
            return GetNullableDateTime(obj, true, null);
        }

        /// <summary>
        /// 获取数据库中的时间类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <returns>时间类型数据(默认为最小值)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2007-10-25
        /// -->
        public static Nullable<DateTime> GetNullableDateTime(object obj, bool ifConvert)
        {
            return GetNullableDateTime(obj, ifConvert, null);
        }

        /// <summary>
        /// 获取数据库中的时间类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <param name="defaultValue">默认值(obj为空，或转换失败时返回值)。</param>
        /// <returns>时间类型数据。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2007-10-25
        /// -->
        public static Nullable<DateTime> GetNullableDateTime(object obj, bool ifConvert, Nullable<DateTime> defaultValue)
        {
            Nullable<DateTime> result = defaultValue;
            if ((null != obj) && (DBNull.Value != obj))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToDateTime(obj);
                    }
                    catch
                    {
                        result = defaultValue;
                    }
                }
                else
                {
                    if (obj is DateTime)
                    {
                        result = (DateTime)obj;
                    }
                }
            }
            return result;
        }

        #endregion

        #region int类型

        /// <summary>
        /// 获取数据库中的Int类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <returns>Int类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static int GetInt(object obj)
        {
            return GetInt(obj, true, 0);
        }

        /// <summary>
        /// 获取数据库中的Int类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <returns>Int类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static int GetInt(object obj, bool ifConvert)
        {
            return GetInt(obj, ifConvert, 0);
        }

        /// <summary>
        /// 获取数据库中的Int类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <param name="defaultValue">默认值(obj为空，或转换失败时返回值)。</param>
        /// <returns>Int类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static int GetInt(object obj, bool ifConvert, int defaultValue)
        {
            int result = defaultValue;
            if ((null != obj) && (DBNull.Value != obj))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToInt32(obj);
                    }
                    catch
                    {
                        result = defaultValue;
                    }
                }
                else
                {
                    if (obj is decimal)
                    {
                        try
                        {
                            result = decimal.ToInt32((decimal)obj);
                        }
                        catch
                        {
                            result = defaultValue;
                        }
                    }
                }
            }
            return result;
        }

        #endregion

        #region long类型

        /// <summary>
        /// 获取数据库中的Long类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <returns>Long类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static long GetLong(object obj)
        {
            return GetLong(obj, true, 0);
        }

        /// <summary>
        /// 获取数据库中的Long类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <returns>Long类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static long GetLong(object obj, bool ifConvert)
        {
            return GetLong(obj, ifConvert, 0);
        }

        /// <summary>
        /// 获取数据库中的Long类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <param name="defaultValue">默认值(obj为空，或转换失败时返回值)。</param>
        /// <returns>Long类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static long GetLong(object obj, bool ifConvert, long defaultValue)
        {
            long result = defaultValue;
            if ((null != obj) && (DBNull.Value != obj))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToInt64(obj);
                    }
                    catch
                    {
                        result = defaultValue;
                    }
                }
                else
                {
                    if (obj is decimal)
                    {
                        try
                        {
                            result = decimal.ToInt64((decimal)obj);
                        }
                        catch
                        {
                            result = defaultValue;
                        }
                    }
                }
            }
            return result;
        }

        #endregion

        #region float类型

        /// <summary>
        /// 获取数据库中的float类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <returns>float类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static float GetFloat(object obj)
        {
            return GetFloat(obj, true, 0);
        }

        /// <summary>
        /// 获取数据库中的float类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <returns>float类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static float GetFloat(object obj, bool ifConvert)
        {
            return GetFloat(obj, ifConvert, 0);
        }

        /// <summary>
        /// 获取数据库中的float类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <param name="defaultValue">默认值(obj为空，或转换失败时返回值)。</param>
        /// <returns>float类型数据。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static float GetFloat(object obj, bool ifConvert, float defaultValue)
        {
            float result = defaultValue;
            if ((null != obj) && (DBNull.Value != obj))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToSingle(obj);
                    }
                    catch
                    {
                        result = defaultValue;
                    }
                }
                else
                {
                    if (obj is decimal)
                    {
                        try
                        {
                            result = decimal.ToSingle((decimal)obj);
                        }
                        catch
                        {
                            result = defaultValue;
                        }
                    }
                }
            }
            return result;
        }

        #endregion

        #region double类型

        /// <summary>
        /// 获取数据库中的double类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <returns>double类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static double GetDouble(object obj)
        {
            return GetDouble(obj, true, 0);
        }

        /// <summary>
        /// 获取数据库中的double类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <returns>double类型数据(默认为0)。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static double GetDouble(object obj, bool ifConvert)
        {
            return GetDouble(obj, ifConvert, 0);
        }

        /// <summary>
        /// 获取数据库中的double类型数据。
        /// </summary>
        /// <param name="obj">object类型的值。</param>
        /// <param name="ifConvert">如果为 <c>true</c>，则强制转换类型。</param>
        /// <param name="defaultValue">默认值(obj为空，或转换失败时返回值)。</param>
        /// <returns>double类型数据。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static double GetDouble(object obj, bool ifConvert, double defaultValue)
        {
            double result = defaultValue;
            if ((null != obj) && (DBNull.Value != obj))
            {
                if (ifConvert)
                {
                    try
                    {
                        result = Convert.ToDouble(obj);
                    }
                    catch
                    {
                        result = defaultValue;
                    }
                }
                else
                {
                    if (obj is decimal)
                    {
                        try
                        {
                            result = decimal.ToDouble((decimal)obj);
                        }
                        catch
                        {
                            result = defaultValue;
                        }
                    }
                }
            }
            return result;
        }

        #endregion

        /// <summary>
        /// 获取数据库中的Char型(Y,N)表示bool型值数据。
        /// </summary>
        /// <param name="obj">object类型值。</param>
        /// <returns>如果为'Y'，返回true；其它返回false。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static bool GetYesNo(object obj)
        {
            bool result = false;
            if (GetString(obj) == "Y")
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 获取数据库中的Char型表示bool型值数据。
        /// </summary>
        /// <param name="obj">object类型值。</param>
        /// <returns>如果为'1'，返回true；其它返回false。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static bool GetCharBool(object obj)
        {
            bool result = false;
            if (GetString(obj) == "1")
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 获取数据库中的number型表示布尔类型数据。
        /// </summary>
        /// <param name="obj">object类型值。</param>
        /// <returns>如果为1，返回true；其它返回false。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static bool GetNumberBool(object obj)
        {
            if (decimal.One == GetDecimal(obj))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 获取数据库中的二进制类型数据
        /// </summary>
        /// <param name="obj">object类型值</param>
        /// <returns>非二进制或空，返回null</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static byte[] GetRaw(object obj)
        {
            if ((null == obj) || (DBNull.Value == obj))
            {
                return null;
            }
            else
            {
                byte[] bytes = obj as byte[];
                return bytes;
            }
        }

        #endregion

        #region 设置写入数据库数据格式

        /// <summary>
        /// 设置数据库中number类型形式的布尔类型数据
        /// </summary>
        /// <param name="value">bool类型值</param>
        /// <returns>如果为false返回0，true返回1</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static int SetNumberBool(bool value)
        {
            if (value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 设置数据库中char类型形式(Y,N)的布尔类型数据
        /// </summary>
        /// <param name="value">bool类型值</param>
        /// <returns>如果为false返回'N'，true返回'Y'</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static char SetYesNo(bool value)
        {
            if (value)
            {
                return 'Y';
            }
            else
            {
                return 'N';
            }
        }

        /// <summary>
        /// 设置数据库中char类型形式的布尔类型数据
        /// </summary>
        /// <param name="value">bool类型值</param>
        /// <returns>如果为false返回'0'，true返回'1'</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static char SetCharBool(bool value)
        {
            if (value)
            {
                return '1';
            }
            else
            {
                return '0';
            }
        }

        /// <summary>
        /// 设置时间类型的值，将时间最大及最小值转为DBNull。
        /// </summary>
        /// <param name="value">时间类型值。</param>
        /// <returns>转换后的时间值。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static object SetDateTime(DateTime value)
        {
            if ((value == DateTime.MinValue) || (value == DateTime.MaxValue))
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        #endregion

        /// <summary>
        /// 处理Sql中输入值的特殊字符。
        /// </summary>
        /// <param name="value">Sql中连接的值。</param>
        /// <returns>处理特殊字符后的值。</returns>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static string HandleString(string value)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                result = value.Trim();
                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Replace("'", string.Empty);
                }
            }
            return result;
        }

        /// <summary>
        /// 处理查询中使用Or关系的条件。
        /// </summary>
        /// <param name="builder">连接查询条件的StringBuilder。</param>
        /// <param name="fieldName">需要查询的字段名。</param>
        /// <param name="content">查询的内容。</param>
        /// <!--
        /// 创建人  : ZhouYingLu
        /// 创建时间: 2009-02-26
        /// -->
        public static void HandleOrQuery(StringBuilder builder, string fieldName, string content)
        {
            if ((null != builder) && (!string.IsNullOrEmpty(fieldName)) && (!string.IsNullOrEmpty(content)))
            {
                string[] value = content.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (value.Length > 0)
                {
                    builder.Append('(');
                    foreach (string s in value)
                    {
                        builder.Append(fieldName);
                        builder.Append(" like '%");
                        builder.Append(HandleString(s));
                        builder.Append("%'");
                        builder.Append(" or ");
                    }
                    builder.Remove(builder.Length - 4, 4);
                    builder.Append(')');
                }
            }
        }

        #region Stream 和 byte[] 之间的转换

        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 将 Stream 写入文件
        /// </summary>
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);

            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 从文件读取 Stream
        /// </summary>
        public static Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        #endregion

        #region 获取ASCII码在 0~255内的字符
        /// <summary>
        /// 函数 截取部分字符串
        /// 排除汉字部分
        /// </summary>
        /// <param name="PartName"></param>
        /// <returns></returns>
        private static string GetPart(string value)
        {
            StringBuilder sb = new StringBuilder();
            char[] chrs = value.ToCharArray();
            foreach (char chr in chrs)
            {
                if ((int)chr > 0 && (int)chr < 255)
                {
                    sb.Append(chr);
                }
            }
            return sb.ToString().Trim();
        }
        /// <summary>
        /// char与ascii码转换
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int getascii(char c)
        {
            return (int)c;
        }

        /// <summary>
        /// 获取记录总数SQL语句
        /// </summary>
        /// <param name="_safeSql">SQL查询语句</param>
        /// <returns>记录总数SQL语句</returns>
        public static string CreateCountingSql(string _safeSql)
        {
            return string.Format(" SELECT COUNT(1) AS RecordCount FROM ({0}) AS T ", _safeSql);
        }

        /// <summary>
        /// 获取分页SQL语句，排序字段需要构成唯一记录
        /// </summary>
        /// <param name="_recordCount">记录总数</param>
        /// <param name="_pageSize">每页记录数</param>
        /// <param name="_pageIndex">当前页数</param>
        /// <param name="_safeSql">SQL查询语句</param>
        /// <param name="_orderField">排序字段，多个则用“,”隔开</param>
        /// <returns>分页SQL语句</returns>
        public static string CreatePagingSql(int _recordCount, int _pageSize, int _pageIndex, string _safeSql, string _orderField)
        {
            //重新组合排序字段，防止有错误
            string[] arrStrOrders = _orderField.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sbOriginalOrder = new StringBuilder(); //原排序字段
            StringBuilder sbReverseOrder = new StringBuilder(); //与原排序字段相反，用于分页
            for (int i = 0; i < arrStrOrders.Length; i++)
            {
                arrStrOrders[i] = arrStrOrders[i].Trim();  //去除前后空格
                if (i != 0)
                {
                    sbOriginalOrder.Append(", ");
                    sbReverseOrder.Append(", ");
                }
                sbOriginalOrder.Append(arrStrOrders[i]);

                int index = arrStrOrders[i].IndexOf(" "); //判断是否有升降标识
                if (index > 0)
                {
                    //替换升降标识，分页所需
                    bool flag = arrStrOrders[i].IndexOf(" DESC", StringComparison.OrdinalIgnoreCase) != -1;
                    sbReverseOrder.AppendFormat("{0} {1}", arrStrOrders[i].Remove(index), flag ? "ASC" : "DESC");
                }
                else
                {
                    sbReverseOrder.AppendFormat("{0} DESC", arrStrOrders[i]);
                }
            }

            //计算总页数
            _pageSize = _pageSize == 0 ? _recordCount : _pageSize;
            int pageCount = (_recordCount + _pageSize - 1) / _pageSize;

            //检查当前页数
            if (_pageIndex < 1)
            {
                _pageIndex = 1;
            }
            else if (_pageIndex > pageCount)
            {
                _pageIndex = pageCount;
            }

            StringBuilder sbSql = new StringBuilder();
            //第一页时，直接使用TOP n，而不进行分页查询
            if (_pageIndex == 1)
            {
                sbSql.AppendFormat(" SELECT TOP {0} * ", _pageSize);
                sbSql.AppendFormat(" FROM ({0}) AS T ", _safeSql);
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
            }
            //最后一页时，减少一个TOP
            else if (_pageIndex == pageCount)
            {
                sbSql.Append(" SELECT * FROM ");
                sbSql.Append(" ( ");
                sbSql.AppendFormat(" SELECT TOP {0} * ", _recordCount - _pageSize * (_pageIndex - 1));
                sbSql.AppendFormat(" FROM ({0}) AS T ", _safeSql);
                sbSql.AppendFormat(" ORDER BY {0} ", sbReverseOrder.ToString());
                sbSql.Append(" ) AS T ");
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
            }
            //前半页数时的分页
            else if (_pageIndex <= (pageCount / 2 + pageCount % 2) + 1)
            {
                sbSql.Append(" SELECT * FROM ");
                sbSql.Append(" ( ");
                sbSql.AppendFormat(" SELECT TOP {0} * FROM ", _pageSize);
                sbSql.Append(" ( ");
                sbSql.AppendFormat(" SELECT TOP {0} * ", _pageSize * _pageIndex);
                sbSql.AppendFormat(" FROM ({0}) AS T ", _safeSql);
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
                sbSql.Append(" ) AS T ");
                sbSql.AppendFormat(" ORDER BY {0} ", sbReverseOrder.ToString());
                sbSql.Append(" ) AS T ");
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
            }
            //后半页数时的分页
            else
            {
                sbSql.AppendFormat(" SELECT TOP {0} * FROM ", _pageSize);
                sbSql.Append(" ( ");
                sbSql.AppendFormat(" SELECT TOP {0} * ", ((_recordCount % _pageSize) + _pageSize * (pageCount - _pageIndex) + 1));
                sbSql.AppendFormat(" FROM ({0}) AS T ", _safeSql);
                sbSql.AppendFormat(" ORDER BY {0} ", sbReverseOrder.ToString());
                sbSql.Append(" ) AS T ");
                sbSql.AppendFormat(" ORDER BY {0} ", sbOriginalOrder.ToString());
            }
            return sbSql.ToString();
        }


        #endregion
    }
}
