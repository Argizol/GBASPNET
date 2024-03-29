﻿using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using NetStore.Abstraction;
using NetStore.Models;
using NetStore.Models.DTO;
using System.Text;

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
            using var context = new StoreContext();
            var entityGroup = context.Groups.FirstOrDefault(x => x.Name.ToLower() == group.Name.ToLower());
            if (entityGroup is null)
            {
                entityGroup = _mapper.Map<Group>(group);
                context.Groups.Add(entityGroup);
                context.SaveChanges();
                _cache.Remove("groups");
            }
            return entityGroup.GroupId;
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

                var getgroups = context.Groups.Select(x => _mapper.Map<DTOGroup>(x)).ToList();

                return getgroups;
            }
        }

        public string GetGroupsCSV()
        {
            var sb = new StringBuilder();
            var groups = GetGroups();

            foreach (var group in groups)
            {
                sb.AppendLine($"{group.GroupId},{group.Name},{group.Description}");
            }

            return sb.ToString();
        }

        public string GetСacheStatCSV()
        {
            var curCache = _cache.GetCurrentStatistics();
            var sb = new StringBuilder();
            sb.AppendLine($"CurrentEntryCount, {curCache.CurrentEntryCount.ToString()}")
              .AppendLine($"CurrentEstimatedSize, {curCache.CurrentEstimatedSize.ToString()}")
              .AppendLine($"TotalHits, {curCache.TotalHits.ToString()}")
              .AppendLine($"TotalMisses, {curCache.TotalMisses.ToString()}");
            return sb.ToString();
        }
    }
}
