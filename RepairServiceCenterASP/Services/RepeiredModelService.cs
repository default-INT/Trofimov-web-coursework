using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RepairServiceCenterASP.Data;
using RepairServiceCenterASP.Models;

namespace RepairServiceCenterASP.Services
{
    public class RepeiredModelService : ICachingModel<RepairedModel>
    {
        private RepairServiceCenterContext db;
        private IMemoryCache cache;
        private int rowsNumber = 50;
        private const int SECONDS = 272;

        public RepeiredModelService(RepairServiceCenterContext db, IMemoryCache memoryCache)
        {
            this.db = db;
            cache = memoryCache;
        }

        public bool CreateCache(RepairedModel entity)
        {
            try
            {
                db.RepairedModels.Add(entity);
                int n = db.SaveChanges();
                if (n > 0)
                {
                    cache.Set(entity.RepairedModelId, entity, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(SECONDS)
                    });
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public RepairedModel ReadCache(string cacheKey, int id)
        {
            ICollection<RepairedModel> repairedModels = null;
            if (!cache.TryGetValue(cacheKey, out repairedModels))
            {
                var repairedModel = db.RepairedModels.Where(r => r.RepairedModelId == id).FirstOrDefault();
                cache.Set(repairedModel.RepairedModelId, repairedModel, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(SECONDS)
                });
            }
            return repairedModels.Where(r => r.RepairedModelId == id).FirstOrDefault();
        }

        public ICollection<RepairedModel> ReadAllCache(string cacheKey)
        {
            ICollection<RepairedModel> repairedModels = null;
            if (!cache.TryGetValue(cacheKey, out repairedModels))
            {
                repairedModels = db.RepairedModels.Take(rowsNumber).ToList();
                if (repairedModels != null)
                {
                    cache.Set(cacheKey, repairedModels,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(SECONDS)));
                }
            }
            return repairedModels;
        }
    }
}
