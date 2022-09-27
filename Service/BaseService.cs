using Common;
using Repository.DAL;
using Repository.ViewModel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public abstract class BaseService
    {
        protected SqlSugarScope SqlSugarDB = SqlSugarDAL.DB;

        protected IFreeSql DB = FreeSqlDAL.DB;
        public Response Response { get; set; } = new Response();

    }
}
