using Ruanmou.DB.Interface;
using System;

namespace Ruanmou.DB.MySql
{
    public class MySqlHelper : IDBHelper
    {
        public MySqlHelper()
        {
            Console.WriteLine("{0}被构造", this.GetType().Name);
        }
        public void Query()
        {
            Console.WriteLine("{0}.Query", this.GetType().Name);
        }
    }
}
