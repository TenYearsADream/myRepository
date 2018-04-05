using System;

namespace Sino.Model
{
    /// <summary>
	/// ST_GROUP:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class GROUP
	{
		public GROUP()
		{}
		#region Model
		private string _groupid;
		private string _group_name;
		private string _memo;
		private string _parent_gid;
		private int _type=0 ;
		private int _sort=0 ;
		private int _region_id;
		private string _department_id;
		/// <summary>
		/// Id
		/// </summary>
		public string GROUPID
		{
			set{ _groupid=value;}
			get{return _groupid;}
		}
		/// <summary>
		/// 名称
		/// </summary>
		public string GROUP_NAME
		{
			set{ _group_name=value;}
			get{return _group_name;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string MEMO
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 父节点Id
		/// </summary>
		public string PARENT_GID
		{
			set{ _parent_gid=value;}
			get{return _parent_gid;}
		}
		/// <summary>
		/// 节点类型（0表示行政区，1表示部门，2表示岗位）
		/// </summary>
		public int TYPE
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 同层节点顺序
		/// </summary>
		public int SORT
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 行政区Id
		/// </summary>
		public int REGION_ID
		{
			set{ _region_id=value;}
			get{return _region_id;}
		}
		/// <summary>
		/// 部门ID
		/// </summary>
		public string DEPARTMENT_ID
		{
			set{ _department_id=value;}
			get{return _department_id;}
		}
		#endregion Model

	}
}
