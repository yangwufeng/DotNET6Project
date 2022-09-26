using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base
{
    public class UserService : BaseService
    {
        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string CheckUser(UserModel model)
        {
            try
            {
                var user = DB.Queryable<User>().First(t => t.Name == model.Account && t.Password == model.Password);
                if (user == null)
                {
                    return Response.Error("未找到用户信息").ToJson();
                }
                user.LoginTime = DateTime.Now;
                DB.Updateable(user); 
                return Response.Success().ToJson();
            }
            catch (Exception ex)
            {
                return Response.Error("登录接口出现异常" + ex.Message).ToJson();
            }
        }



        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string OutLogin(UserModel user)
        {
            try
            {
                if (!DB.Queryable<User>().Any(t => t.Name == user.Account && t.Password == user.Password))
                {
                    return Response.Error("未找到用户信息").ToJson();
                }
                return Response.Success().ToJson();
            }
            catch (Exception ex)
            {
                return Response.Error("未找到用户信息" + ex.Message).ToJson();
            }
        }
    }
}
