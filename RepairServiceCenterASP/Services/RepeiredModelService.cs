using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using RepairServiceCenterASP.Data;
using RepairServiceCenterASP.Models;

namespace RepairServiceCenterASP.Services
{
    public class RepairedModelService : ICachingModel<RepairedModel>
    {
        private RepairServiceCenterContext db;
        private IMemoryCache cache;
        private int rowsNumber = 50;
        private const int SECONDS = 272;
        int pageNum;

        public RepairedModelService(RepairServiceCenterContext db, IMemoryCache memoryCache)
        {
            this.db = db;
            cache = memoryCache;
        }

        public bool EditCache(RepairedModel entity)
        {
            try
            {
                var model = db.RepairedModels.Where(m => m.RepairedModelId == entity.RepairedModelId)
                                 .FirstOrDefault();
                if (model != null)
                {
                    model = entity;
                    cache.Set(entity.RepairedModelId, entity, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(SECONDS)
                    });
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
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

        public void RefreshCache(string cacheKey)
        {
            var repairedModels = db.RepairedModels.Take(rowsNumber).ToList();
            cache.Set(cacheKey, repairedModels,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(SECONDS)));
        }

        public ICollection<RepairedModel> ReadAllCache(string cacheKey, int count, int pageNum, int pageSize)
        {
            if (pageNum == this.pageNum && rowsNumber < count)
            {
                return ReadAllCache(cacheKey).Take(count).ToList();
            }
            else
            {
                var models = db.RepairedModels.Skip((pageNum - 1) * pageSize)
                                              .Take(rowsNumber)
                                              .ToList();
                cache.Set(cacheKey, models,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(SECONDS)));
                return models.Take(count).ToList();

            }
        }
    }
}
