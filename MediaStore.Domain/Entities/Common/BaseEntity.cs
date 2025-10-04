namespace MediaStore.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = new DateTime(2025,08,15);
        public DateTime LastModifiedDate { get; set; } = new DateTime(2025, 08, 15);
        public bool IsDeleted { get; set; }
    }
}
