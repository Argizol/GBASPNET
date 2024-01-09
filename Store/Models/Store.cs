using Store.Models.Base;

namespace Store.Models
{
    public class Store : BaseModel
    {
        public int Count { get; set; }
        public int ProductID { get; set; }
    }
}
