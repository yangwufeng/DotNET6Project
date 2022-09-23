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
        public string CheckUser(UserModel user)
        {
            return Response.Success().Serialize();

            try
            {
                if (!DB.Queryable<User>().Any(t => t.Name == user.Account && t.Password == user.Password))
                {
                    return Response.Error("未找到用户信息").Serialize();
                }

                //DB.Updateable<User>(t => t.id) user.Account
                return Response.Success().Serialize();
            }
            catch (Exception ex)
            {
                return Response.Error("未找到用户信息" + ex.Message).Serialize();
            }
        }



        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string OutLogin(UserModel user)
        {
            try
            {
                if (!DB.Queryable<User>().Any(t => t.Name == user.Account && t.Password == user.Password))
                {
                    return Response.Error("未找到用户信息").Serialize();
                }
                return Response.Success().Serialize();
            }
            catch (Exception ex)
            {
                return Response.Error("未找到用户信息" + ex.Message).Serialize();
            }
        }
    }
}
