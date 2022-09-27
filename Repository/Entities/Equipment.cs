using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using SqlSugar;

namespace Repository.Entities
{
    public class Equipment : BaseEntity<int>
    {
        /// <summary>
        /// 设备编码，唯一且有规律易识别
        /// </summary>
        [Column(Order = 2)]
        [MaxLength(50)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 设备名
        /// </summary>
        [Column(Order = 3)]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 关联到设备类型Id
        /// </summary>
        [Column(Order = 4)]
        [Required]
        public int EquipmentTypeId { get; set; }

        /// <summary>
        /// 此处写到这台设备对应的IP，一般为PLC的IP
        /// </summary>
        [Column(Order = 5)]
        [MaxLength(20)]
        public string IP { get; set; }

        /// <summary>
        /// 连接名
        /// </summary>
        public string ConnectName { get; set; }

        /// <summary>
        /// 所在区域，出于调度目的或其他划分，比如：为兼容转轨堆垛机设定，正常情况下与巷道相同，转轨情况下对应虚拟划分巷道
        /// </summary>
        [Column(Order = 8)]
        [MaxLength(50)]
        public string DestinationArea { get; set; }

        /// <summary>
        /// 关联的LED IP
        /// </summary>
        [Column(Order = 6)]
        [MaxLength(20)]
        public string LEDIP { get; set; }

        /// <summary>
        /// 关联的扫码器IP
        /// </summary>
        [Column(Order = 7)]
        [MaxLength(20)]
        public string ScanIP { get; set; }

        /// <summary>
        /// 所在巷道，没有填0
        /// </summary>
        [Column(Order = 9)]
        [Required]
        public int RoadWay { get; set; }

        /// <summary>
        /// 通常用于站台和口，自身地址
        /// </summary>
        [Column(Order = 11)]
        [MaxLength(50)]
        public string SelfAddress { get; set; }

        /// <summary>
        /// 通常用于站台和口，回退地址
        /// </summary>
        [Column(Order = 12)]
        [MaxLength(50)]
        public string BackAddress { get; set; }

        /// <summary>
        /// 通常用于站台和口，前进地址，如果配置了则以配置优先，如果没有配置则由路由获取
        /// </summary>
        [Column(Order = 13)]
        [MaxLength(50)]
        public string GoAddress { get; set; }

        /// <summary>
        /// 通常用于站台和口，电控接入接出站台索引，从501到532;当发送口时，同时发送排索引；
        /// </summary>
        [Column(Order = 14)]
        public int StationIndex { get; set; }

        /// <summary>
        /// 通常用于站台和口，货叉排索引，没有填0
        /// </summary>
        [Column(Order = 15)]
        [Required]
        public int RowIndex1 { get; set; }

        /// <summary>
        /// 通常用于站台和口，货叉排索引，没有填0
        /// </summary>
        [Column(Order = 16)]
        public int RowIndex2 { get; set; }

        /// <summary>
        /// 列索引，此值对应到X，一般与Y相同，指定501等站台索引
        /// 对于堆垛机接出与接入站台，要求必须配置此项，此项用于判断堆垛机是否可以到达站台所在位置
        /// </summary>
        [Column(Order = 17)]
        [Required]
        public int ColumnIndex { get; set; }

        /// <summary>
        /// 针对堆垛机，所在的层索引，此值对应到Y，一般与X相同，指定501等站台索引
        /// </summary>
        [Column(Order = 18)]
        [Required]
        public int LayerIndex { get; set; }

        /// <summary>
        /// 站台组（双叉会用到）
        /// </summary>
        [Column(Order = 20)]
        [MaxLength(50)]
        public string StationGroup { get; set; }

        /// <summary>
        /// 站台组索引（双叉时，小的对应1号货叉，大的对应2号货叉，当存在多个时可以定义为12 45，则表示12可以同时放，45可以同时放，即相差不为1表示相邻）
        /// </summary>
        [Column(Order = 21)]
        [Required]
        public int StationGroupIndex { get; set; }

        /// <summary>
        /// 用于界面显示进行站台分组的
        /// </summary>
        [Column(Order = 26)]
        [MaxLength(50)]
        public string StationGroupUIText { get; set; }


        /// <summary>
        /// 货叉1可用站台索引
        /// </summary>
        [Column(Order = 23)]
        [MaxLength(50)]
        public string Fork1AvailableStation { get; set; }

        /// <summary>
        /// 货叉1可抵达最小列
        /// </summary>
        [Column(Order = 24)]
        public int Fork1MinColumn { get; set; }

        /// <summary>
        /// 货叉1可抵达最大列
        /// </summary>
        [Column(Order = 25)]
        public int Fork1MaxColumn { get; set; }


        /// <summary>
        /// 货叉2可用站台索引
        /// </summary>
        [Column(Order = 26)]
        [MaxLength(50)]
        public string Fork2AvailableStation { get; set; }

        /// <summary>
        /// 货叉2可抵达最小列
        /// </summary>
        [Column(Order = 27)]
        public int Fork2MinColumn { get; set; }

        /// <summary>
        /// 货叉2可抵达最大列
        /// </summary>
        [Column(Order = 28)]
        public int Fork2MaxColumn { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column(Order = 29)]
        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// 维护规则Id
        /// </summary>
        [Column(Order = 32)]
        public int? EquipmentMaintainRuleId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column(Order = 33)]
        [Required]
        public bool Disable { get; set; }

        /// <summary>
        /// 设备所属仓库
        /// </summary>
        [Column(Order = 34)]
        [Required]
        public string WarehouseCode { get; set; }



        /// <summary>
        /// 逻辑外键实体-设备类型
        /// </summary>
        public EquipmentType EquipmentType { get; set; }

        private List<EquipmentProp> equipmentProps = new List<EquipmentProp>();

        /// <summary>
        /// 逻辑外键实体-设备属性
        /// </summary>
        public List<EquipmentProp> EquipmentProps
        {
            get { return equipmentProps; }
            set
            {
                equipmentProps = value;
            }
        }

        /// <summary>
        /// 获取设备属性
        /// </summary>
        /// <param name="key">设备属性code </param>
        /// <returns></returns>
        [NotMapped]
        public EquipmentProp this[string key]
        {
            get
            {
                return equipmentProps?.Find(t => t.EquipmentTypePropTemplateCode == key);
            }
        }

    }
}
