using Ruanmou.DB.Interface;
using Ruanmou.Model;
using Ruanmou.Util;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace Ruanmou.DB.SqlServer
{
    public class SqlServerHelper : IDBHelper
    {
        public SqlServerHelper()
        {
            //Console.WriteLine("{0}被构造", this.GetType().Name);
        }
        public void Query()
        {
            //Console.WriteLine("{0}.Query", this.GetType().Name);
        }

        /// <summary>
        /// 普通版查询方式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public tb_log Find(long id)
        {
            string sql = $"select * from tb_log where id={id}";
            using (SqlConnection connection = new SqlConnection(ConfigrationManager.SqlConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                //command.Parameters.AddRange(paraArray);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine(reader[1]);
                }
                //else
                //{
                //    return null;
                //}
            }
            return null;
        }
        /// <summary>
        /// 泛型版查询方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(long id) where T : BaseModel
        {
            Type type = typeof(T);
            string sql = $"select {string.Join(",", type.GetProperties().Select(p => $"[{p.Name}]"))} from [{type.Name}] where id={id}";
            object oObject = Activator.CreateInstance(type);
            using (SqlConnection connection = new SqlConnection(ConfigrationManager.SqlConnectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    foreach (var prop in type.GetProperties())
                    {
                        prop.SetValue(oObject, reader[prop.Name] is DBNull ? null : reader[prop.Name]);//数据库字段为null的情况会报错
                    }
                    //foreach (var prop in type.GetProperties())
                    //{
                    //    Console.WriteLine($"{type.Name}.{prop.Name}={prop.GetValue(oPeople)}");
                    //    if (prop.Name.Equals("Id"))
                    //        prop.SetValue(oPeople, 234);
                    //    else if (prop.Name.Equals("Name"))
                    //        prop.SetValue(oPeople, "rocs");
                    //    Console.WriteLine($"{type.Name}.{prop.Name}={prop.GetValue(oPeople)}");
                    //}
                    return (T)oObject;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
