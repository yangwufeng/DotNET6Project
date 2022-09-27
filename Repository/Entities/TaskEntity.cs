using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using Org.BouncyCastle.Crypto;

namespace Repository.Entities
{

    /// <summary>
    /// 任务
    /// </summary>
    [Table("task")]
    public class TaskEntity : BaseEntity<int>
    {
        /// <summary>
        /// 上游的前置任务号在wcs中的内部任务号
        /// </summary>
        [Column(Order = 2)]
        public int PreTaskId { get; set; }

        /// <summary>
        /// 上游任务号
        /// </summary>
        [Column(Order = 3)]
        [MaxLength(50)]
        public string RemoteTaskNo { get; set; }

        /// <summary>
        /// 上游的前置任务号
        /// </summary>
        [Column(Order = 4)]
        [MaxLength(50)]
        public string PreRemoteTaskNo { get; set; }

        /// <summary>
        /// 优先级，约定越大优先级越高
        /// </summary>
        [Column(Order = 5)]
        [Required]
        public int Priority { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        [Column(Order = 6)]
        [Required]
        public int TaskType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Column(Order = 7)]
        [Required]
        public int TaskStatus { get; set; }

        /// <summary>
        /// 容器编码
        /// </summary>
        [Column(Order = 8)]
        [MaxLength(50)]
        public string ContainerCode { get; set; }

        /// <summary>
        /// 从库位
        /// </summary>
        [Column(Order = 9)]
        [MaxLength(50)]
        public string FromLocationCode { get; set; }

        /// <summary>
        /// 去向库位
        /// </summary>
        [Column(Order = 10)]
        [MaxLength(50)]
        public string ToLocationCode { get; set; }

        /// <summary>
        /// 入口port，值为入口的设备code，应用的场景为立库外接输送线，需到达指定入口入库
        /// </summary>
        [Column(Order = 11)]
        [MaxLength(50)]
        public string FromPort { get; set; }

        /// <summary>
        /// 出库口、拣选口编码，实际制定最终达到的口
        /// </summary>
        [Column(Order = 12)]
        [MaxLength(50)]
        public string ToPort { get; set; }

        /// <summary>
        /// 用于实时记录位置的变量，设备的code，此值可能滞后
        /// </summary>
        [Column(Order = 13)]
        [MaxLength(20)]
        public string CurrentEquipmentCode { get; set; }

        /// <summary>
        /// 某些特殊情况下，可能要区分任务运行的区域
        /// </summary>
        [Column(Order = 14)]
        [MaxLength(20)]
        public string DestinationArea { get; set; }

        /// <summary>
        /// 阶段标记，对应到阶段枚举
        /// </summary>
        [Column(Order = 15)]
        [Required]
        public int Stage { get; set; }

        /// <summary>
        /// 双叉时，用于记录任务在哪个货叉上
        /// 1,表示任务在货叉1上；2，表示任务在货叉2上；
        /// </summary>
        [Column(Order = 16)]
        public int ForkFlag { get; set; }

        /// <summary>
        /// 是否空出（默认false)
        /// </summary>
        [Column(Order = 17)]
        [Required]
        public int IsEmptyOut { get; set; }

        /// <summary>
        /// 是否重入
        /// </summary>
        [Column(Order = 18)]
        [Required]
        public int IsDoubleIn { get; set; }

        /// <summary>
        /// 是否取货错误
        /// </summary>
        [Column(Order = 19)]
        [Required]
        public int IsForkError { get; set; }

        /// <summary>
        /// 发生重入时的库位，默认为空
        /// </summary>
        [Column(Order = 20)]
        [MaxLength(50)]
        public string DoubleInLocationCode { get; set; }

        /// <summary>
        /// 任务重发标志,1表示任务重发
        /// </summary>
        [Column(Order = 21)]
        public int SendAgain { get; set; }

        /// <summary>
        /// 当前任务是否已反馈
        /// 请在任务创建时就标定是否需要反馈
        /// </summary>
        [Column(Order = 22)]
        [Required]
        public int CommitFlag { get; set; }

        private string reqLength;

        /// <summary>
        /// 携带货物的长度，获取时转成int后返回string
        /// </summary>
        [Column(Order = 23)]
        [MaxLength(20)]
        public string ReqLength
        {
            get
            {
                if (int.TryParse(reqLength, out var length))
                {
                    return length.ToString();
                }
                else
                {
                    return "0";
                }
            }
            set { reqLength = value; }
        }

        private string reqWeight;

        /// <summary>
        /// 携带货物的重量，获取时转成int后返回string
        /// </summary>
        [Column(Order = 24)]
        [MaxLength(20)]
        public string ReqWeight
        {
            get
            {
                if (int.TryParse(reqWeight, out var weight))
                {
                    return weight.ToString();
                }
                else
                {
                    return "0";
                }
            }
            set { reqWeight = value; }
        }

        private string reqHeight;

        /// <summary>
        /// 携带货物的高度，获取时转成int后返回string
        /// </summary>
        [Column(Order = 25)]
        [MaxLength(20)]
        public string ReqHeight
        {
            get
            {
                if (int.TryParse(reqHeight, out var height))
                {
                    return height.ToString();
                }
                else
                {
                    return "0";
                }
            }
            set { reqHeight = value; }
        }

        private string reqWidth;

        /// <summary>
        /// 携带货物的宽度，获取时转成int后返回string
        /// </summary>
        [Column(Order = 26)]
        [MaxLength(20)]
        public string ReqWidth
        {
            get
            {
                if (int.TryParse(reqWidth, out var width))
                {
                    return width.ToString();
                }
                else
                {
                    return "0";
                }
            }
            set { reqWidth = value; }
        }

        /// <summary>
        /// 货物类型
        /// </summary>
        [Column(Order = 27)]
        public int GoodsType { get; set; }

        /// <summary>
        /// 平台，WMS或WCS
        /// </summary>
        [Column(Order = 27)]
        [MaxLength(50)]
        public string Platform { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [Column(Order = 28)]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column(Order = 28)]
        [MaxLength(50)]
        public string WarehouseCode { get; set; }

        //private int deleted;

        ///// <summary>
        ///// 任务是否删除，默认0,1为删除，实际我们不标记此任务是否已经被删除，如果删除，则物理删除，记录日志
        ///// </summary>
        //public int Deleted
        //{
        //    get { return deleted; }
        //    set { deleted = value;  }
        //}



        [NotMapped]
        public Location FromLocation { get; set; }

        [NotMapped]
        public Location ToLocation { get; set; }

        [NotMapped]
        public Equipment ToPortEquipment { get; set; }

        [NotMapped]
        public Equipment FromPortEquipment { get; set; }

        [NotMapped]
        public Equipment CurrentEquipment { get; set; }

        [NotMapped]
        public Location DoubleInLocation { get; set; }

        /// <summary>
        /// 货叉1，路由配置的可出站台
        /// </summary>
        [NotMapped]
        public List<Equipment> AvailableForkStation1 { get; set; } = new List<Equipment>();

        /// <summary>
        /// 货叉2，路由配置的可出站台
        /// </summary>
        [NotMapped]
        public List<Equipment> AvailableForkStation2 { get; set; } = new List<Equipment>();
    }
}
