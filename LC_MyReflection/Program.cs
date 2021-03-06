using LC_MyReflection.Demo;
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
    /// 
    /// 
    /// 反射优点：动态、热启动
    /// 反射缺点：1、使用麻烦；2、避开编译器检查；3、性能问题（没有想象的夸张，要正确看待）
    ///                        100w次循环创建对象    普通方法 41ms
    ///                                              反射     6512ms
    ///                        -----但是，换个角度分析下，100次循环，反射耗时0.65ms，也就是说，反射基本不会影响到你的程序性能，除非你循环太多反射了。               
    ///                        缓存优化，把dll加载和类型获取 只执行一次
    ///                        100w次循环创建对象    普通方法 48ms
    ///                                              反射     103ms
    ///                                              反射影响是不是更小了，是的。
    ///                        MVC-Asp.Net-ORM-IOC-AOP都在使用反射，几乎都有缓存
    ///                        MVC&&ORM启动慢，完成很多初始化，反射的那些东西，后面就运行快了。
    ///         缓存---反射优化：事先将dll通过反射加载出来，然后创建对象可以放入静态字典内。                       
    /// -----------------------这才是使用反射的正确姿势！！！                        
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //1、反射创建对象和调用方法
                //Demo1.Start();

                Console.WriteLine("-----------------------------------------------------");

                //2、反射创建对象和操作属性
                //Demo2.Start();

                //3、反射创建对象和特性、接口、事件、委托等...

                Console.WriteLine("-----------------------------------------------------");

                //3、性能测试
                Monitor.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
