using Common.Config;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Index.Strtree;
using Repository;
using Repository.ViewModel;
using Service;
using Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        public LoginController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public string GetTokenUser(string token)
        {
            return _userService.GetTokenUser(token);
        }

        /// <summary>
        /// /登录接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public string Login(UserModel user)
        {
            return _userService.CheckUser(user);
        }

        [HttpPost]
        public string OutLogin(UserModel user)
        {
            return _userService.OutLogin(user);
        }
    }
}
