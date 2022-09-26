using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class User : BaseEntity<long>
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        public string Code { get; set; }

        public string Name { get; set; }
        public string Account { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string? Role { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? LoginTime { get; set; }

        public string?  Token { get; set; }
    }
}
