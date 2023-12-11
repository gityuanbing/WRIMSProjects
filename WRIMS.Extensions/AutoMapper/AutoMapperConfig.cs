using System;
using System.IO;
using System.Reflection;
using AutoMapper;

namespace WRIMS.AutoMapper
{
    /// <summary>
    /// 静态全局 AutoMapper 配置文件
    /// </summary>
    public partial class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                var basePath = AppContext.BaseDirectory;
                var extensionsPath = Path.Combine(basePath, "WRIMS.Extensions.dll");
                //var modelPath = Path.Combine(basePath, "WRIMS.Model.dll");
                cfg.AddMaps(Assembly.LoadFrom(extensionsPath));
                //cfg.AddMaps(Assembly.LoadFrom(modelPath));

            });
        }
    }
}
