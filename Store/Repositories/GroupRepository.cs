﻿using NetStore.Models.DTO;
using NetStore.Models;
using AutoMapper;
using NetStore.Abstraction;
using Microsoft.Extensions.Caching.Memory;

namespace NetStore.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GroupRepository(IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _cache = cache;
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
                    _cache.Remove("groups");
                }
                return entityGroup.Id;
            }
        }

        public IEnumerable<DTOGroup> GetGroups()
        {
            using (var context = new StoreContext())
            {
                if (_cache.TryGetValue("products", out List<DTOGroup> groups))
                {
                    return groups;
                }

                _cache.Set("products", groups, TimeSpan.FromMinutes(30));
                groups = context.Groups.Select(x => _mapper.Map<DTOGroup>(x)).ToList();

                return groups;
            }
        }
    }
}