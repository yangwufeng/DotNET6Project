using Common.Config;
using FreeSql.Aop;
using FreeSql.DataAnnotations;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class FreeSqlDAL
    {
        /// <summary>
        /// 数据库字符串连接
        /// </summary>
        public static IFreeSql DB { get; set; }

        /// <summary>
        /// 单例处理
        /// </summary>
        /// <returns></returns>
        public static IFreeSql GetFreeSql()
        {
            if (DB == null)
            {
                lock (typeof(FreeSqlDAL))
                {
                    if (DB == null)
                    {
                        DB = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.MySql, ConnectionStrings.FreeSqlConnectionStrings.DBMySql)
                .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                .Build(); //请务必定义成 Singleton 单例模式
                        DB.Aop.AuditValue += Aop_AuditValue;
                        DB.Aop.CurdAfter += Aop_CurdAfter;
                    }
                    return DB;
                }
            }
            return DB;
        }


        /// <summary>
        /// 记录超时Sql
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Aop_CurdAfter(object sender, CurdAfterEventArgs e)
        {
            Console.WriteLine($"SQL语句：{e.Sql}");
        }

        /// <summary>
        /// 插入更新时，自动赋值created和updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Aop_AuditValue(object sender, AuditValueEventArgs e)
        {
            if (e.AuditValueType == AuditValueType.Insert)
            {
                if (e.Property.Name == "Created")
                {
                    e.Value = DateTime.Now;
                }
            }
            if (e.AuditValueType == AuditValueType.Update)
            {
                if (e.Property.Name == "Updated")
                {
                    e.Value = DateTime.Now;
                }
            }
            if (e.AuditValueType == AuditValueType.InsertOrUpdate)
            {
                if (e.Property.Name == "Created" && e.Value == null)
                {
                    e.Value = DateTime.Now;
                }
                if (e.Property.Name == "Updated")
                {
                    e.Value = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// 静态单列
        /// </summary>
        //public static IFreeSql Db => new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.MySql, ConnectionStrings.FreeSqlConnectionStrings.DBMySql)
        //          .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
        //          .Build();

        //public static Type[] GetTypesByTableAttribute()
        //{
        //    try
        //    {
        //        List<Type> tableAssembies = new List<Type>();
        //        foreach (Type type in Assembly.GetAssembly(typeof(BaseKeyEntity<>)).GetExportedTypes())
        //            foreach (Attribute attribute in type.GetCustomAttributes())
        //                if (attribute is TableAttribute tableAttribute)
        //                    if (tableAttribute.DisableSyncStructure == false)
        //                        tableAssembies.Add(type);
        //        return tableAssembies.ToArray();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        public static Type[] GetTypesByNameSpace()
        {
            List<Type> tableAssembies = new List<Type>();
            List<string> entitiesFullName = new List<string>()
    {
        "Repository.Entities",
    };
            foreach (Type type in Assembly.GetAssembly(typeof(BaseKeyEntity<>)).GetExportedTypes())
                foreach (var fullname in entitiesFullName)
                    if (type.FullName.StartsWith(fullname) && type.IsClass)
                        tableAssembies.Add(type);
            return tableAssembies.Where(t => t.Name != "BaseKeyEntity`1" && t.Name != "BaseEntity`1").ToArray();

        }
    }
}

