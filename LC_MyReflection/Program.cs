using Ruanmou.DB.Interface;
using Ruanmou.DB.MySql;
using Ruanmou.DB.SqlServer;
using System;
using System.Reflection;

namespace LC_MyReflection
{
    /// <summary>
    /// 1、dll-IL-metadata-反射；
    /// 2、反射加载dll，读取module、类、方法、特性；
    /// 3、反射创建对象，反射+简单工厂+配置文件；
    /// 4、反射调用实例方法、静态方法、重载方法、私有方法、调用泛型方法；
    /// 5、反射字段和属性，分别获取值和设置值；
    /// 6、反射的好处和局限；
    /// 
    /// 反射反射，程序员的快乐
    /// 反射是无处不在的，MVC-Asp.Net-ORM-IOC-AOP 几乎所有的框架都离不开反射
    /// 
    /// 反编译工具不是用的反射，是一个逆向工程
    /// IL：也是一种面向对象的语言，只不过是不太好阅读
    /// metadata：数据清单，描述DLL/exe里面的各种信息
    /// 
    /// 反射Reflection：是.Net Framework提供的一个帮助类库，可以读取并使用metadata
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
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
                    //2 完整路径
                    Assembly assembly2 = Assembly.LoadFile(@"G:\github\LC_MyReflection\LC_MyReflection\bin\Debug\netcoreapp3.1\Ruanmou.DB.MySql.dll");
                    //当前路径
                    Assembly assembly3 = Assembly.LoadFrom("Ruanmou.DB.MySql.dll");
                    Assembly assembly4 = Assembly.LoadFrom(@"G:\github\LC_MyReflection\LC_MyReflection\bin\Debug\netcoreapp3.1\Ruanmou.DB.MySql.dll");

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
                    object singletonA = Activator.CreateInstance(type,true);//调用私有构造函数
                    object singletonB = Activator.CreateInstance(type, true);//调用私有构造函数
                    object singletonC = Activator.CreateInstance(type, true);//调用私有构造函数
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
                    //todo...
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
