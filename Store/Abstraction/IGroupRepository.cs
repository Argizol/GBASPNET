using NetStore.Models.DTO;

namespace NetStore.Abstraction
{
    public interface IGroupRepository
    {
        public int AddGroup(DTOGroup group);

        public IEnumerable<DTOGroup> GetGroups();
    }
}
