using Store.Models.Base;

namespace Store.Models
{
    public class Group : BaseModel 
    {
      
        public virtual List<Product> Products { get; set; } = null!;
    }
}
