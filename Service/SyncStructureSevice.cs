using Repository.DAL;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// 用于表同步
    /// </summary>
    public class SyncStructureSevice
    {
        public static void FreeSqlSyncTable()
        {
            try
            {
                FreeSqlDAL.GetFreeSql().CodeFirst.SyncStructure(FreeSqlDAL.GetTypesByNameSpace());
            }
            catch (Exception ex )
            {
                throw;
            }
        }

        public static void SqlSugarDALSyncTable()
        {
            try
            {
                ///***批量创建表***/
                ////语法1：
                //Type[] types = Assembly
                //        .LoadFrom("XXX.dll")//如果 .dll报错，可以换成 xxx.exe 有些生成的是exe 
                //        .GetTypes().Where(it => it.FullName.Contains("OrmTest."))//命名空间过滤，当然你也可以写其他条件过滤
                //        .ToArray();//断点调试一下是不是需要的Type，不是需要的在进行过滤

                //db.CodeFirst.SetStringDefaultLength(200).InitTables(types);//根据types创建表

                //语法2：                
                Type[] types = typeof(BaseKeyEntity<>).Assembly.GetTypes()
            .Where(it => it.FullName.Contains("Repository.Entities."))//命名空间过滤，当然你也可以写其他条件过滤
            .ToArray();
                //排除实体基类
                types= types.Where(t => t.Name != "BaseKeyEntity`1" && t.Name != "BaseEntity`1").ToArray();
            SqlSugarDAL.DB.CodeFirst.SetStringDefaultLength(200).InitTables(types);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
