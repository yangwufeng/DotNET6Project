using Common;
using DAL.SqlSugarOrm;
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
        protected SqlSugarScope DB = SqlSugarDAL.DB;
        public Response Response { get; set; } = new Response();

    }
}
