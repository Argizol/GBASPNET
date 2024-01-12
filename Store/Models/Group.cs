using NetStore.Models.Base;

namespace NetStore.Models
{
    public class Group : BaseModel 
    {      
        public virtual List<Product> Products { get; set; } = null!;
    }
}
