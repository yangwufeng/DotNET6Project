using DAL.FreeSqlOrm;
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
        public static void SyncTable()
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

    }
}
