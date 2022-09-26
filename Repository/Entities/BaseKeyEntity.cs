

using Org.BouncyCastle.Asn1.X509;
using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Repository.Entities
{
    /// <summary>
    /// model基类，只包含一个id
    /// </summary>
    public abstract class BaseKeyEntity<T>
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public T Id { get; set; }
    }
}
