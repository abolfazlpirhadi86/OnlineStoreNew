using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Entity
{
    public abstract class BaseEntity<T> : BaseEntity
    {
        [Column("ID")]
        [Display(Name = "شناسه")]
        public T Id { get; set; }
    }

    public abstract class BaseEntity
    {

    }
}
