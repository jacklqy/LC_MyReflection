using Ruanmou.DB.Interface;
using System;

namespace Ruanmou.DB.SqlServer
{
    public class SqlServerHelper : IDBHelper
    {
        public SqlServerHelper()
        {
            Console.WriteLine("{0}被构造", this.GetType().Name);
        }
        public void Query()
        {
            Console.WriteLine("{0}.Query", this.GetType().Name);
        }
    }
}
