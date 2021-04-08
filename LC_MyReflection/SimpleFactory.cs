using Microsoft.Extensions.Configuration;
using Ruanmou.DB.Interface;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LC_MyReflection
{
    public class SimpleFactory
    {
        private static string _IDBHelperConfig = null;
        /// <summary>
        /// 静态构造函数：由CLR保证，在第一次使用这个类之前，自动被调用且只调用一次.
        /// </summary>
        static SimpleFactory()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsetting.json", optional: true, reloadOnChange: false)
                .Build();
            _IDBHelperConfig = config["IDBHelperConfig"];
        }
        public static IDBHelper CreateInstance()
        {
            var dllName = _IDBHelperConfig.Split(',')[0];
            var className= _IDBHelperConfig.Split(',')[1];

            //Assembly assembly1 = Assembly.Load("Ruanmou.DB.MySql");//1动态加载
            //Type type = assembly1.GetType("Ruanmou.DB.MySql.MySqlHelper");//2获取类型 完整类型名称

            Assembly assembly1 = Assembly.Load(dllName);//1动态加载
            Type type = assembly1.GetType(className);//2获取类型 完整类型名称
            object oDBHelper = Activator.CreateInstance(type);//3创建对象
            IDBHelper iDBHelper = oDBHelper as IDBHelper;//4类型转换 不报错，类型不对就返回null
            return iDBHelper;
        }
    }
}
