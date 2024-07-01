namespace MediaManagement.Domain.Entities
{
    public class MediaCategory
    {
        public string Title { get; set; }
        public bool Status { get; set; }

        public List<Media> Medias { get; set; }
    }
}
