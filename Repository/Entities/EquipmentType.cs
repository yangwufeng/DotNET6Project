using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class EquipmentType : BaseEntity<int>
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

        private string description;

        [Column(Order = 4)]
        [MaxLength(200)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public List<EquipmentTypePropTemplate> EquipmentTypePropTemplates { get; set; }
    }
}