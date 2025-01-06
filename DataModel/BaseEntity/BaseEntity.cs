namespace DataModel.BaseEntity
{
    public class BaseEntity<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; } 

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; }

        public DateTime DeletedDate { get; set; }

        public string CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
