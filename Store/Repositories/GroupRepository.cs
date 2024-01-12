using NetStore.Models.DTO;
using NetStore.Models;
using AutoMapper;
using NetStore.Abstraction;

namespace NetStore.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMapper _mapper;

        public GroupRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public int AddGroup(DTOGroup group)
        {
            using (var context = new StoreContext())
            {
                var entityGroup = context.Groups.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
                if (entityGroup is null)
                {
                    entityGroup = _mapper.Map<Group>(group);
                    context.Groups.Add(entityGroup);
                    context.SaveChanges();
                }
                return entityGroup.Id;
            }
        }

        public IEnumerable<DTOGroup> GetGroups()
        {
            using (var context = new StoreContext())
            {
                var groups = context.Groups.Select(x => _mapper.Map<DTOGroup>(x)).ToList();

                return groups;
            }
        }
    }
}
