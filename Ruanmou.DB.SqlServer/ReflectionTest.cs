using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.DB.SqlServer
{
    public class ReflectionTest
    {
        #region Identity
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
        #endregion


        #region Method
        /// <summary>
        /// 无参数方法
        /// </summary>
        public void Show1()
        {
            Console.WriteLine("无参数方法：这里是{0}的Show1", this.GetType());
        }
        /// <summary>
        /// 有参数方法
        /// </summary>
        /// <param name="id"></param>
        public void Show2(int id)
        {
            Console.WriteLine("有参数方法：这里是{0}的Show2", this.GetType());
        }
        /// <summary>
        /// 重载方法一
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void Show3(int id, string name)
        {
            Console.WriteLine("重载方法一：这里是{0}的Show3_1", this.GetType());
        }
        /// <summary>
        /// 重载方法二
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        public void Show3(string name, int id)
        {
            Console.WriteLine("重载方法二：这里是{0}的Show3_2", this.GetType());
        }
        /// <summary>
        /// 重载方法三
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void Show3(int id)
        {
            Console.WriteLine("重载方法三：这里是{0}的Show3_3", this.GetType());
        }
        /// <summary>
        /// 重载方法四
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void Show3(string name)
        {
            Console.WriteLine("重载方法四：这里是{0}的Show3_4", this.GetType());
        }
        /// <summary>
        /// 重载方法五
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public void Show3()
        {
            Console.WriteLine("重载方法五：这里是{0}的Show3_5", this.GetType());
        }
        /// <summary>
        /// 私有方法
        /// </summary>
        private void Show4(string name)
        {
            Console.WriteLine("私有方法：这里是{0}的Show4", this.GetType());
        }
        /// <summary>
        /// 静态方法
        /// </summary>
        public static void Show5(string name)
        {
            Console.WriteLine("静态方法：这里是{0}的Show5", typeof(ReflectionTest));
        }
        #endregion

    }
}
