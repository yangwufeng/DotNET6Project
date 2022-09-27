

using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class Location : BaseEntity<int>
    {
        /// <summary>
        /// 库位编码，一般情况下请保证上下游一致
        /// </summary>
        [Column(Order = 2)]
        [MaxLength(50)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 当wcs库位与上游库位不一致时，这个一般为关联的上游库位
        /// 请尽量使wcs库位与上游库位一致；
        /// </summary>
        [Column(Order = 3)]
        [MaxLength(50)]
        public string LinkCode { get; set; }

        [Column(Order = 4)]
        [Required]
        public int Row { get; set; }

        [Column(Order = 5)]
        [Required]
        public int Column { get; set; }

        [Column(Order = 6)]
        [Required]
        public int Layer { get; set; }

        /// <summary>
        /// 一般行列层即可定位到库位，某些特殊情况下，会定位到格，比如一个库位作为叠盘库位划分格子
        /// 当此值不为0时，表示当前库位叠盘
        /// </summary>
        [Column(Order = 7)]
        [Required]
        public int Grid { get; set; }


        /// <summary>
        /// 堆垛机标记，与之相等，取rowindex1，反之取rowindex2
        /// </summary>
        [Column(Order = 8)]
        [MaxLength(50)]
        [Required]
        public string SRMCode { get; set; }

        /// <summary>
        /// 行索引1，2=左2,1=左，3=右1，4=右2
        /// </summary>
        [Column(Order = 9)]
        [Required]
        public int RowIndex1 { get; set; }

        /// <summary>
        /// 行索引2，当转轨时，堆垛机位于另一侧则采用rowindex2
        /// </summary>
        [Column(Order = 10)]
        public int RowIndex2 { get; set; }

        /// <summary>
        /// 默认情况下，destinationArea可以与巷道一致，当有转轨堆垛机的时候，用来标识两个虚拟的巷道;当出现上下层等情况，按照实际情况划分
        /// </summary>
        [Column(Order = 11)]
        [MaxLength(50)]
        public string DestinationArea { get; set; }

        /// <summary>
        /// 默认情况下，只要物理轨道没有切割，则为同一个巷道
        /// </summary>
        [Column(Order = 12)]
        [Required]
        public int Roadway { get; set; }

        /// <summary>
        /// 使用type来划分库位类型，只要宽度或高度不一致，则为新的库位类型
        /// </summary>
        [Column(Order = 13)]
        [MaxLength(50)]
        public string Type { get; set; }

        /// <summary>
        /// 一个库位只能放一个托盘，如果放多个托盘，则是多个库位
        /// </summary>
        [Column(Order = 14)]
        [MaxLength(50)]
        public string ContainerCode { get; set; }

        [Column(Order = 15)]
        [Required]
        public int Status { get; set; }

        [Column(Order = 16)]
        [MaxLength(50)]
        public string ZoneCode { get; set; }

        [Column(Order = 17)]
        [MaxLength(50)]
        [Required]
        public string WarehouseCode { get; set; }
    }
}
