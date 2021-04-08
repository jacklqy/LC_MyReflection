using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.DB.SqlServer
{
    /// <summary>
    /// 单例模式：整个进程中只有一个实例
    /// </summary>
    public sealed class Singleton
    {
        private static Singleton _Singleton = null;
        /// <summary>
        /// 1 构造函数私有化
        /// </summary>
        private Singleton()
        {
            Console.WriteLine("Singleton被构造啦");
        }
        /// <summary>
        /// 2 静态构造函数由CLR保障，在第一次调用Singleton之前调用
        /// </summary>
        static Singleton()
        {
            _Singleton = new Singleton();
        }
        /// <summary>
        /// 3 对外提供获取实例方法
        /// </summary>
        /// <returns></returns>
        public static Singleton GetInstance()
        {
            return _Singleton;
        }
    }
}
