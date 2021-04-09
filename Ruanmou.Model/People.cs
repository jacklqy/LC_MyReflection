using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.Model
{
    public class People
    {
        public People()
        {
            Console.WriteLine("{0}被创建", this.GetType().Name);
        }
        //属性
        public int Id { get; set; }
        //属性
        public string Name { get; set; }
        //字段
        public string Description;
    }
}
