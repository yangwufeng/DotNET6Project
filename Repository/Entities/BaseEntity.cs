using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    /// <summary>
    /// 扩展基类，包含created等，created自动写入
    /// </summary>
    public abstract class BaseEntity<T> : BaseKeyEntity<T>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(Order = 40)]
        public DateTime? Created { get; set; }

        [Column(Order = 41)]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// 对于更新时间，不自动写入，允许为null
        /// </summary>
        [Column(Order = 42)]
        public DateTime? Updated { get; set; }

        [Column(Order = 43)]
        public string? UpdatedBy { get; set; }

    }
}
