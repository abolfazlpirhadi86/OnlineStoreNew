using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
{
    public interface IBaseEntity { }
    public abstract class BaseEntity<T> : IBaseEntity
    {

        [Column("ID")]
        public virtual T Id { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<long>
    {

    }
}
