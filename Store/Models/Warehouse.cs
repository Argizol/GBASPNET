using NetStore.Models.Base;

namespace NetStore.Models
{
    public class Warehouse : BaseModel
    {
        public virtual List<Product> Products { get; set; } = null!;
        public int Count { get; set; }
    }
}
