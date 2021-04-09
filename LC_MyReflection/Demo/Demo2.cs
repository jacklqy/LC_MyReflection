using Ruanmou.DB.SqlServer;
using Ruanmou.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC_MyReflection.Demo
{
    public class Demo2
    {
        public static void Start()
        {
            {
                Console.WriteLine("*********************常规做法************************");
                People people = new People();
                people.Id = 123;
                people.Name = "jack";
                people.Description = "Hello Nice to meet you";
                Console.WriteLine("Id={0},Name={1},Description={2}", people.Id, people.Name, people.Description);
            }
            {
                Console.WriteLine("*********************Reflection************************");
                //1 反射get展示是有意义的--反射遍历，可以不用改代码
                //2 反射set感觉好像没啥用，但是
                Type type = typeof(People);
                object oPeople = Activator.CreateInstance(type);//dynamic动态类型或强制转换
                //遍历属性
                foreach (var prop in type.GetProperties())
                {
                    Console.WriteLine($"{type.Name}.{prop.Name}={prop.GetValue(oPeople)}");
                    if (prop.Name.Equals("Id"))
                        prop.SetValue(oPeople, 234);
                    else if (prop.Name.Equals("Name"))
                        prop.SetValue(oPeople, "rocs");
                    Console.WriteLine($"{type.Name}.{prop.Name}={prop.GetValue(oPeople)}");
                }
                //遍历字段
                foreach (var field in type.GetFields())
                {
                    Console.WriteLine($"{type.Name}.{field.Name}={field.GetValue(oPeople)}");
                    if (field.Name.Equals("Description"))
                        field.SetValue(oPeople, "Happy birthday!");
                    Console.WriteLine($"{type.Name}.{field.Name}={field.GetValue(oPeople)}");
                }
            }
            {
                SqlServerHelper helper = new SqlServerHelper();
                helper.Find<tb_log>(1);
                helper.Find<tb_error>(1);
            }
        }
    }
}
