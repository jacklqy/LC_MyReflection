using System;
using System.Collections.Generic;
using System.Text;

namespace Ruanmou.DB.SqlServer
{
    /// <summary>
    /// 泛型类测试
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="W"></typeparam>
    /// <typeparam name="X"></typeparam>
    public class GenericClass<T, W, X>
    {
        public void Show(T t, W w, X x)
        {
            Console.WriteLine("t.type={0},w.type={1},x.type={2}", t.GetType().Name, w.GetType().Name, x.GetType().Name);
        }
    }

    /// <summary>
    /// 泛型方法测试
    /// </summary>
    public class GenericMethod
    {
        public void Show<T, W, X>(T t, W w, X x)
        {
            Console.WriteLine("t.type={0},w.type={1},x.type={2}", t.GetType().Name, w.GetType().Name, x.GetType().Name);
        }
    }
    /// <summary>
    /// 泛型类和泛型方法测试
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericDouble<T>
    {
        public void Show<W, X>(T t, W w, X x)
        {
            Console.WriteLine("t.type={0},w.type={1},x.type={2}", t.GetType().Name, w.GetType().Name, x.GetType().Name);
        }
    }
}
