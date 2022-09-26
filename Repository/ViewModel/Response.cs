using Common;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel
{
    /// <summary>
    /// 兼容PDA、和web 错误统一返回500，无数据也返回500
    /// 401 Token失效
    /// 200 成功，有数据，业务正常
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 默认返回操作成功
        /// </summary>
        public Response(bool status = true, string msg = "成功")
        {
            Code = status ? 200 : 500;
            Status = status;
            Message = msg;
        }


        public int Code { get; set; }

        public string Message { get; set; }

        public bool Status { get; set; }

        public long Count { get; set; }

        /// <summary>
        /// 登陆标识
        /// </summary>
        public string Token { get; set; }


        public dynamic Result { get; set; }


        /// <summary>
        /// 统一返回前端成功
        /// </summary>
        public Response Success(string msg = "业务数据操作成功!")
        {
            Code = 200;
            Message = msg;
            Status = true;
            return this;
        }

        /// <summary>
        /// 统一返回前端失败
        /// </summary>
        /// <param name="msg">失败消息</param>
        public Response Error(string msg = "业务数据操作失败")
        {
            Code = 500;
            Message = msg;
            Status = false;
            return this;
        }

        public string ToJson()
        {
            return JsonHelper.Instance.Serialize(this);
        }
    }

    public class Response<T> : Response
    {
        /// <summary>
        /// 回传的结果
        /// </summary>
        public new T? Result { get; set; }
    }
}

