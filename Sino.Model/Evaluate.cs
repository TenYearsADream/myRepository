using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sino.Model
{
    public partial class Evaluate
    {
        public int ID { get; set; }
        public Nullable<int> Marks { get; set; }
        public string Remarks { get; set; }
        public string UserName { get; set; }
        public string OperateTime { get; set; }
        public string KeyWords { get; set; }
        public Nullable<int> Fid { get; set; }
    }

}
