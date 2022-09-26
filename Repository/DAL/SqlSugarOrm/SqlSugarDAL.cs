using Common.Config;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class SqlSugarDAL
    {
        /// <summary>
        /// 静态单例处理
        /// </summary>
        public static SqlSugarScope DB = new SqlSugarScope(new ConnectionConfig()
        {
            ConnectionString = ConnectionStrings.SqlSugarConnectionStrings.DBMySql,//连接符字串
            DbType = DbType.MySql,//数据库类型
            IsAutoCloseConnection = true,//不设成true要手动close
            ConfigureExternalServices = new ConfigureExternalServices
            {
                //注意:  这儿AOP设置不能少
                EntityService = (c, p) =>
                {
                    // int?  decimal?这种 isnullable=true 不支持string
                    if (c.PropertyType.IsGenericType &&
                    c.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        p.IsNullable = true;
                    }

                    //高版C#写法 支持string?和string  
                    //if (new NullabilityInfoContext()
                    //.Create(c).WriteState is NullabilityState.Nullable)
                    //{
                    //    p.IsNullable = true;
                    //}
                }
            },
            MoreSettings = new ConnMoreSettings()
            {
                IsWithNoLockQuery = true//全局 With(nolock) 
            },
        }, db =>
        {
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);//输出sql,查看执行sql
            };
        });
        /// <summary>
        /// 启用单例
        /// </summary>
        //public static SqlSugarScope db = new SqlSugarScope(new ConnectionConfig()
        //{
        //    ConnectionString = "Server=.xxxxx",//连接符字串
        //    DbType = DbType.SqlServer,//数据库类型
        //    IsAutoCloseConnection = true //不设成true要手动close
        //}, db =>
        //{
        //    db.Aop.OnLogExecuting = (sql, pars) =>
        //    {
        //        Console.WriteLine(sql);//输出sql,查看执行sql
        //    };
        //});

        public SqlSugarDAL()
        {
            if (DB == null)
            {
                lock (typeof(SqlSugarDAL))
                {
                    if (DB == null)
                    {
                        DB = new SqlSugarScope(new ConnectionConfig()
                        {
                            ConnectionString = ConnectionStrings.SqlSugarConnectionStrings.DBMySql,//连接符字串
                            DbType = DbType.MySql,//数据库类型
                            IsAutoCloseConnection = true, //不设成true要手动close
                            MoreSettings = new ConnMoreSettings()
                            {
                                IsWithNoLockQuery = true//全局 With(nolock) 
                            },
                        }, db =>
                        {
                            db.Aop.OnLogExecuting = (sql, pars) =>
                            {
                                Console.WriteLine(sql);//输出sql,查看执行sql
                            };
                        });
                    }
                }
            }
        }

    }
}
