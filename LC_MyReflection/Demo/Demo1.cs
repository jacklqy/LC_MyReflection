using Ruanmou.DB.Interface;
using Ruanmou.DB.MySql;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LC_MyReflection.Demo
{
    public class Demo1
    {
        public static void Start()
        {
            {
                //常规做法：引用类库，然后实例化对象，在调用方法
                IDBHelper dBHelper = new MySqlHelper();
                dBHelper.Query();

                //假如MySqlHelper要换成SqlServerHelper就必须修改现有代码，重新编译发布
            }
            Console.WriteLine("*******************Reflection*******************");
            {
                //反射
                //1 动态加载 一个完整dll名称，不需要后缀，会从exe所在路径查找
                Assembly assembly1 = Assembly.Load("Ruanmou.DB.MySql");
                ////2 完整路径
                //Assembly assembly2 = Assembly.LoadFile(@"G:\github\LC_MyReflection\LC_MyReflection\bin\Debug\netcoreapp3.1\Ruanmou.DB.MySql.dll");
                ////当前路径
                //Assembly assembly3 = Assembly.LoadFrom("Ruanmou.DB.MySql.dll");
                //Assembly assembly4 = Assembly.LoadFrom(@"G:\github\LC_MyReflection\LC_MyReflection\bin\Debug\netcoreapp3.1\Ruanmou.DB.MySql.dll");

                foreach (var type in assembly1.GetTypes())
                {
                    Console.WriteLine(type.Name);
                    foreach (var method in type.GetMethods())
                    {
                        Console.WriteLine(method.Name);
                    }
                }
            }
            Console.WriteLine("*******************Reflection*******************");
            {
                Assembly assembly1 = Assembly.Load("Ruanmou.DB.MySql");//1动态加载
                Type type = assembly1.GetType("Ruanmou.DB.MySql.MySqlHelper");//2获取类型 完整类型名称
                object oDBHelper = Activator.CreateInstance(type);//3创建对象

                //不能直接Query 为啥？ 实际上oDBHelper是有Query方法的，只是因为编译器不认可。C#是一种强类型语言，静态语言，编译时就确定好类型保证安全。
                //oDBHelper.Query();

                //dynamic动态类型，编译器不检查，运行时才检查
                //dynamic oDBHelper2 = Activator.CreateInstance(type);
                //oDBHelper2.Query();

                IDBHelper iDBHelper = oDBHelper as IDBHelper;//4类型转换 不报错，类型不对就返回null
                iDBHelper?.Query();//5方法调用
            }
            Console.WriteLine("*******************Reflection+Factory*******************");
            {
                //如果你觉得代码很复杂，封装一下！
                IDBHelper iDBHelper = SimpleFactory.CreateInstance();
                iDBHelper?.Query();//5方法调用

                //程序可配置可扩展=反射的动态加载和动态创建对象+配置文件结合

                //程序可配置，通过修改配置文件就可以自动切换
                //1 没有写死类型，而是通过配置文件执行，反射创建的
                //2 实现类必须是事先已有的，而且在目录下面，才可以
                //程序可扩展
                //1 完全不修改原有代码，只是增加新的实现，copy，修改配置文件，就可以支持新功能
            }
            Console.WriteLine("******************Reflection多构造函数创建实例********************");
            {
                Assembly assembly1 = Assembly.Load("Ruanmou.DB.SqlServer");//1动态加载
                Type type = assembly1.GetType("Ruanmou.DB.SqlServer.ReflectionTest");//2获取类型 完整类型名称

                //遍历构造函数
                foreach (ConstructorInfo ctor in type.GetConstructors())
                {
                    Console.WriteLine(ctor.Name);
                    foreach (var parameter in ctor.GetParameters())//构造函数参数
                    {
                        Console.WriteLine(parameter.ParameterType);//构造函数参数类型
                    }
                }

                object oDBHelper1 = Activator.CreateInstance(type);//3创建对象，调用无参数构造函数
                object oDBHelper2 = Activator.CreateInstance(type, new object[] { 123 });//3创建对象，调用有参数构造函数，且类型为int的构造函数
                object oDBHelper3 = Activator.CreateInstance(type, new object[] { "jack" });//3创建对象，调用有参数构造函数，且类型为string的构造函数
            }
            Console.WriteLine("******************Reflection反射破坏单列********************");
            {
                //Singleton singleton1 = Singleton.GetInstance();
                //Singleton singleton2 = Singleton.GetInstance();
                //Console.WriteLine(object.ReferenceEquals(singleton1, singleton2));//true

                //反射破坏单列--就是反射调用私有构造函数
                Assembly assembly1 = Assembly.Load("Ruanmou.DB.SqlServer");//1动态加载
                Type type = assembly1.GetType("Ruanmou.DB.SqlServer.Singleton");//2获取类型 完整类型名称
                object singletonA = Activator.CreateInstance(type, true);//调用私有构造函数
                object singletonB = Activator.CreateInstance(type, true);//调用私有构造函数
                object singletonC = Activator.CreateInstance(type, true);//调用私有构造函数
                Console.WriteLine(object.ReferenceEquals(singletonA, singletonB));//false
                Console.WriteLine(object.ReferenceEquals(singletonA, singletonC));//false
            }
            Console.WriteLine("******************Reflection操作泛型类********************");
            {
                Assembly assembly1 = Assembly.Load("Ruanmou.DB.SqlServer");

                //Type type = assembly1.GetType("Ruanmou.DB.SqlServer.GenericClass");
                //object oGeneric = Activator.CreateInstance(type);

                Type type = assembly1.GetType("Ruanmou.DB.SqlServer.GenericClass`3");//占位符+泛型类型个数
                Type typeMake = type.MakeGenericType(new Type[] { typeof(string), typeof(int), typeof(DateTime) });
                object oGeneric = Activator.CreateInstance(typeMake);
            }
            Console.WriteLine("******************Reflection操作泛型方法********************");
            {
                Assembly assembly1 = Assembly.Load("Ruanmou.DB.SqlServer");
                Type type = assembly1.GetType("Ruanmou.DB.SqlServer.GenericMethod");
                object oGeneric = Activator.CreateInstance(type);
            }
            Console.WriteLine("******************Reflection调用方法********************");
            {
                //如果反射创建对象之后，知道方法名称，怎么不做类型转换，直接调用方法？
                Assembly assembly1 = Assembly.Load("Ruanmou.DB.SqlServer");//1动态加载
                Type type = assembly1.GetType("Ruanmou.DB.SqlServer.ReflectionTest");//2获取类型 完整类型名称
                object oDBHelper1 = Activator.CreateInstance(type);//3创建对象，调用无参数构造函数
                                                                   //Show方法调用
                foreach (var methodInfo in type.GetMethods())//遍历全部方法
                {
                    Console.WriteLine(methodInfo.Name);
                    foreach (var parameter in methodInfo.GetParameters())
                    {
                        Console.WriteLine($"{parameter.Name}{parameter.ParameterType}");
                    }
                }
                {
                    //不带参数方法调用
                    MethodInfo method1 = type.GetMethod("Show1");
                    method1.Invoke(oDBHelper1, null);
                }
                {
                    //带参数方法调用
                    MethodInfo method2 = type.GetMethod("Show2");
                    method2.Invoke(oDBHelper1, new object[] { 123 });
                }
                {
                    //重载方法调用
                    //1、无参数方法调用
                    MethodInfo method1 = type.GetMethod("Show3", new Type[] { });
                    method1.Invoke(oDBHelper1, null);
                    //2、有参数方法调用，int
                    MethodInfo method2 = type.GetMethod("Show3", new Type[] { typeof(int) });
                    method2.Invoke(oDBHelper1, new object[] { 123 });
                    //3、有参数方法调用，string
                    MethodInfo method3 = type.GetMethod("Show3", new Type[] { typeof(string) });
                    method3.Invoke(oDBHelper1, new object[] { "jack" });
                    //4、有参数方法调用，string,int
                    MethodInfo method4 = type.GetMethod("Show3", new Type[] { typeof(string), typeof(int) });
                    method4.Invoke(oDBHelper1, new object[] { "jack", 123 });
                    //5、有参数方法调用，int,string
                    MethodInfo method5 = type.GetMethod("Show3", new Type[] { typeof(int), typeof(string) });
                    method5.Invoke(oDBHelper1, new object[] { 123, "jack" });
                }
                {
                    //静态方法调用
                    MethodInfo method2 = type.GetMethod("Show5");
                    method2.Invoke(oDBHelper1, new object[] { "jack" });//静态方法实例也可不要
                    method2.Invoke(null, new object[] { "jack" });//静态方法实例也可不要
                }
                {
                    //私有方法调用(突破访问限制) 单元测试私有方法等...
                    MethodInfo method = type.GetMethod("Show4", BindingFlags.Instance | BindingFlags.NonPublic);
                    method.Invoke(oDBHelper1, new object[] { "jack" });
                }
                {
                    //泛型方法调用
                    Assembly assembly2 = Assembly.Load("Ruanmou.DB.SqlServer");
                    Type type2 = assembly2.GetType("Ruanmou.DB.SqlServer.GenericMethod");
                    object oGeneric = Activator.CreateInstance(type2);
                    //foreach (MethodInfo item in type2.GetMethods())
                    //{
                    //    Console.WriteLine(item.Name);
                    //}
                    MethodInfo method = type2.GetMethod("Show");
                    var methodNew = method.MakeGenericMethod(new Type[] { typeof(string), typeof(int), typeof(DateTime) });
                    methodNew.Invoke(oGeneric, new object[] { "jack", 123, DateTime.Now });
                }
                {
                    //泛型类+泛型方法调用
                    Assembly assembly2 = Assembly.Load("Ruanmou.DB.SqlServer");
                    Type type2 = assembly2.GetType("Ruanmou.DB.SqlServer.GenericDouble`1");//占位符+泛型类型个数
                    Type typeMake = type2.MakeGenericType(new Type[] { typeof(int) });
                    object oGeneric = Activator.CreateInstance(typeMake);
                    MethodInfo method = typeMake.GetMethod("Show").MakeGenericMethod(new Type[] { typeof(string), typeof(DateTime) });
                    method.Invoke(oGeneric, new object[] { 123, "jack", DateTime.Now });//这里类型必须和泛型类和泛型方法类型一致。
                }
                //反射创建了对象实例---有方法的名称---反射调用方法
                //假如给你一个dll名称、类型名称、方法名称，我们就能调用方法了，那有什么应用场景呢？
                //MVC就是靠的就是这一招，比如MVC请求地址：http://localhost/Home/Index 经过路由解析--调用HomeControl--Index方法 -》肯定是靠反射
                //MVC在启动时会先加载--扫描全部dll--找到全部controller--存起来--请求来的时候用Controller来匹配的---dll+类型名称---调用方法
                //1 MVC局限性---Action方法不能重载---反射是无法区分的---只能通过HttpMethod+特性httpget/httppost
                //2 AOP--反射调用方法可以在前后加入逻辑
            }
        }
    }
}
