using Ruanmou.DB.Interface;
using Ruanmou.DB.SqlServer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace LC_MyReflection
{
    /// <summary>
    /// 性能对比测试
    /// </summary>
    public class Monitor
    {
        public static void Show()
        {
            Console.WriteLine("***********************Monitor*******************");
            long commonTime = 0;
            long reflectionTime = 0;

            //普通创建对象调用方法
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                for (int i = 0; i < 1000_000; i++)
                {
                    IDBHelper iDBHelper = new SqlServerHelper();
                    iDBHelper.Query();//测试的时候最好是构造函数和方法都是空的，只测试构造对象耗时，这样更准
                }
                stopwatch.Stop();
                commonTime = stopwatch.ElapsedMilliseconds;
            }

            //反射创建对象调用方法
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                //优化：减少重复加载
                Assembly assembly = Assembly.Load("Ruanmou.DB.SqlServer");//1动态加载
                Type type = assembly.GetType("Ruanmou.DB.SqlServer.SqlServerHelper");//2获取类型 完整类型名称
                
                for (int i = 0; i < 1000_000; i++)
                {
                    //Assembly assembly = Assembly.Load("Ruanmou.DB.SqlServer");//1动态加载
                    //Type type = assembly.GetType("Ruanmou.DB.SqlServer.SqlServerHelper");//2获取类型 完整类型名称

                    object oDBHelper = Activator.CreateInstance(type);//3创建对象
                    IDBHelper iDBHelper = oDBHelper as IDBHelper;//4类型转换 不报错，类型不对就返回null
                    iDBHelper.Query();//测试的时候最好是构造函数和方法都是空的，只测试构造对象耗时，这样更准
                }
                stopwatch.Stop();
                reflectionTime = stopwatch.ElapsedMilliseconds;
            }

            Console.WriteLine("commonTime={0};reflectionTime={1}", commonTime, reflectionTime);
        }
    }
}
