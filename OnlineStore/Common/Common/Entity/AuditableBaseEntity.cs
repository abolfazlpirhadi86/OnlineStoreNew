using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.Entity
{
    [Serializable]
    public abstract class AuditableBaseEntity<T> : BaseEntity<T>
    {
        [Column(TypeName = "varchar(50)")]
        [Display(Name = "ایجاد شده توسط")]
        public string? CreatedBy { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Display(Name = "به‌روزرسانی شده توسط")]
        public string? UpdatedBy { get; set; }

        [Display(Name = "تاریخ به‌روزرسانی")]
        public DateTime UpdatedAt { get; set; }
    }
}
