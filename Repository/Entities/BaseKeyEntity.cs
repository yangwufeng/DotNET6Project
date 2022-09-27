

using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Repository.Entities
{
    /// <summary>
    /// model基类，只包含一个id
    /// </summary>
    public abstract class BaseKeyEntity<T>
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
}
