﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class EquipmentTypePropTemplate : BaseEntity<int>
    {
        private string code;

        [Column(Order = 2)]
        [MaxLength(50)]
        [Required]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string name;

        [Column(Order = 3)]
        [MaxLength(50)]
        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Column(Order = 4)]
        [Required]
        public int EquipmentTypeId { get; set; }


        private string description;

        [Column(Order = 5)]
        [MaxLength(200)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string propType;

        /// <summary>
        /// 监控地址、常规地址、自身属性；取枚举的name而不是index
        /// </summary>
        [Column(Order = 6)]
        [MaxLength(50)]
        [Required]
        public string PropType
        {
            get { return propType; }
            set { propType = value; }
        }

        private string dataType;

        /// <summary>
        /// 当前属性的数据类型
        /// </summary>
        [Column(Order = 7)]
        [MaxLength(50)]
        [Required]
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

        /// <summary>
        /// 固定地址
        /// </summary>
        [Column(Order = 8)]
        [MaxLength(50)]
        public string Address { get; set; }

        /// <summary>
        /// 比较值
        /// </summary>
        [Column(Order = 11)]
        [MaxLength(50)]
        public string MonitorCompareValue { get; set; }

        /// <summary>
        /// 正常输出
        /// </summary>
        [Column(Order = 12)]
        [MaxLength(50)]
        public string MonitorNormal { get; set; }

        /// <summary>
        /// 异常输出文本
        /// </summary>
        [Column(Order = 13)]
        [MaxLength(50)]
        public string MonitorFailure { get; set; }

        //逻辑外键
        public EquipmentType EquipmentType { get; set; }


    }
}
