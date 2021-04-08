using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.DB.SqlServer
{
    public class ReflectionTest
    {
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public ReflectionTest()
        {
            Console.WriteLine("这里是{0}无参数构造函数", this.GetType().Name);
        }
        /// <summary>
        /// 有参数构造函数
        /// </summary>
        /// <param name="name"></param>
        public ReflectionTest(string name)
        {
            Console.WriteLine("这里是{0}有参数构造函数", this.GetType().Name);
        }
        /// <summary>
        /// 有参数构造函数
        /// </summary>
        /// <param name="id"></param>
        public ReflectionTest(int id)
        {
            Console.WriteLine("这里是{0}有参数构造函数", this.GetType().Name);
        }
    }
}
