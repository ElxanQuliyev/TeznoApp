

namespace Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public ushort InStock { get; set; }
        public decimal Discount { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? SKU { get; set; }
        public string Barcode { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSlider { get; set; }
        public bool IsWeek { get; set; }
        public bool IsMonth { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
