namespace Common.Entity
{
    public abstract class AuditableBaseEntityWithSoftRemove<T> : AuditableBaseEntity<T>
    {
        public bool IsRemoved { get; set; }
    }
}
