﻿using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FreeSql.Internal.GlobalFilter;

namespace Service.Base
{
    public class UserService : BaseService
    {
        public string GetTokenUser(string token)
        {
            try
            {
                var user = DB.Select<User>().Where(t => t.Token == token).First();
                if (user == null)
                {
                    return Response.Error("未找到用户信息").ToJson();
                }
                return Response.Success().ToJson();
            }
            catch (Exception ex)
            {
                return Response.Error("登录接口出现异常" + ex.Message).ToJson();
            }
        }


        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string CheckUser(UserModel model)
        {
            try
            {
                var user = DB.Select<User>().Where(t => t.Name == model.Account && t.Password == model.Password).First();
                if (user == null)
                {
                    return Response.Error("未找到用户信息").ToJson();
                }
                user.LoginTime = DateTime.Now;
                user.Token = Guid.NewGuid().ToString();
                DB.Update<User>().SetSource(user).ExecuteAffrows();
                Response.Result = user;
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
        public string OutLogin(UserModel model)
        {
            try
            {
                var user = DB.Select<User>().Where(t => t.Name == model.Account && t.Password == model.Password).ToOne();
                if (user == null)
                {
                    return Response.Error("未找到用户信息").ToJson();
                }
                user.Token = "";
                DB.Update<User>(user);
                return Response.Success().ToJson();
            }
            catch (Exception ex)
            {
                return Response.Error("未找到用户信息" + ex.Message).ToJson();
            }
        }
    }
}
