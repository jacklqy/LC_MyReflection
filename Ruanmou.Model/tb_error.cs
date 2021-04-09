using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.Model
{
    public class tb_error : BaseModel
    {
        public int mqpathid { get; set; }
        public string mqpath { get; set; }
        public string methodname { get; set; }

        public string info { get; set; }

        public DateTime createtime { get; set; }

    }
}
