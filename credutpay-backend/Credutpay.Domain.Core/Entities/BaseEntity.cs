namespace Credutpay.Domain.Core.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public BaseEntity()
        {
            Id: Guid.NewGuid().ToString();
        }

        public void GenerateId() => Id = Guid.NewGuid().ToString();

        public void UpdateTimestamp() => UpdatedAt = DateTime.Now;

        public void Activate()
        {
            IsDeleted = false;
            UpdateTimestamp();
        }

        public void Disable()
        {
            IsDeleted = true;
            UpdateTimestamp();
        }
    }
}
