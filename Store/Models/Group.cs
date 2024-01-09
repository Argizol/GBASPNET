using Store.Models.Base;

namespace Store.Models
{
    public class Group : BaseModel
    {
        public virtual List<Group> Groups { get; set; } = new List<Group>();
    }
}
