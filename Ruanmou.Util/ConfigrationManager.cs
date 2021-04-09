using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Ruanmou.Util
{
    /// <summary>
    /// 约定：固定读取更目录下面的appsetting.json
    /// </summary>
    public class ConfigrationManager
    {
        //有了IOC在去注入--容器单列
        static ConfigrationManager()
        {
            //需要NuGet引入Microsoft.Extensions.Configuration/Microsoft.Extensions.Configuration.FileExtensions/Microsoft.Extensions.Configuration.Json
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsetting.json");
            IConfigurationRoot configuration = builder.Build();
            _SqlConnectionString = configuration["ConnectionString"];

        }

        private static string _SqlConnectionString = null;
        public static string SqlConnectionString
        {
            get
            {
                return _SqlConnectionString;
            }
        }

    }
}
