using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Config
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// 
        /// </summary>
        public static Logging Logging { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static string AllowedHosts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static SqlSugarConnectionStrings SqlSugarConnectionStrings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static FreeSqlConnectionStrings FreeSqlConnectionStrings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static RedisConnectionStrings RedisConnectionStrings { get; set; }
    }
    public class LogLevel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Default { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Microsoft { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Lifetime { get; set; }
    }

    public class Logging
    {
        /// <summary>
        /// 
        /// </summary>
        public LogLevel LogLevel { get; set; }
    }

    public class SqlSugarConnectionStrings
    {
        /// <summary>
        /// 
        /// </summary>
        public string DBMySql { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DBSqlServer { get; set; }
    }

    public class FreeSqlConnectionStrings
    {
        /// <summary>
        /// 
        /// </summary>
        public string DBMySql { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DBSqlServer { get; set; }
    }

    public class RedisConnectionStrings
    {
        /// <summary>
        /// 
        /// </summary>
        public string RedisIP { get; set; }
    }



}