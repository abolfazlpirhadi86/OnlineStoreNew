using Common.Entity;

namespace MediaManagement.Domain.Entities
{
    public class Media : AuditableBaseEntity<int>
    {
        public int FileName { get; set; }
        public int MediaCategoryId { get; set; }
        public int MediaTypeId { get; set; }
        public int MediaFormatId { get; set; }
        public int Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        public MediaCategory MediaCategory { get; set; }
        public MediaType MediaType { get; set; }
        public MediaFormat MediaFormat { get; set; }
    }
}
