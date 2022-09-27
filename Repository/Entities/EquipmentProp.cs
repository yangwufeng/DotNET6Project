

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class EquipmentProp : BaseEntity<int>
    {

        private int equipmentId;

        [Column(Order = 2)]
        [Required]
        public int EquipmentId
        {
            get { return equipmentId; }
            set { equipmentId = value; }
        }

        private int equipmentTypePropTemplateId;

        [Column(Order = 3)]
        [Required]
        public int EquipmentTypePropTemplateId
        {
            get { return equipmentTypePropTemplateId; }
            set { equipmentTypePropTemplateId = value; }
        }

        private string equipmentTypePropTemplateCode;

        [Column(Order = 4)]
        [MaxLength(50)]
        [Required]
        public string EquipmentTypePropTemplateCode
        {
            get { return equipmentTypePropTemplateCode; }
            set { equipmentTypePropTemplateCode = value; }
        }

        private string address;

        [Column(Order = 6)]
        [MaxLength(50)]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string value;

        [Column(Order = 7)]
        [MaxLength(50)]
        public string Value
        {
            //在此处做一个去除空格的处理，不再在PLC读写里进行
            get { return value?.Replace("\0", "").Trim(); }
            set { this.value = value; }
        }

        private string remark;

        [Column(Order = 8)]
        [MaxLength(200)]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }


        /// <summary>
        /// 逻辑外键--设备实体
        /// </summary>
        public Equipment Equipment { get; set; }

        /// <summary>
        /// 额外对应属性模板，方便读取模板属性
        /// </summary>
        public EquipmentTypePropTemplate EquipmentTypePropTemplate { get; set; }

        public int ServerHandle { get; set; }
    }
}
