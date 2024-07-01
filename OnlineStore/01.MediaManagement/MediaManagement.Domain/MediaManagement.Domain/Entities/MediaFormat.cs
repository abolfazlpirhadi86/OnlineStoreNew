using Common.Entity;

namespace MediaManagement.Domain.Entities
{
    public class MediaFormat : BaseEntity<int>
    {
        public string Format { get; set; }
        public int MediaTypeId { get; set; }

        public MediaType MediaType { get; set; }
    }
}
