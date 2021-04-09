using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.Model
{
    /// <summary>
    /// 数据库表tb_log
    /// </summary>
    public class tb_log : BaseModel
    {
        public int mqpathid { get; set; }
        public string mqpath { get; set; }
        public string methodname { get; set; }
        public string info { get; set; }
        public DateTime createtime { get; set; }
    }
}
