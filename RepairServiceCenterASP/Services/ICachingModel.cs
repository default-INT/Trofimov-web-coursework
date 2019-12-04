using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepairServiceCenterASP.Services
{
    public interface ICachingModel<T>
    {
        ICollection<T> ReadAllCache(string cacheKey);
        void RefreshCache(string cacheKey);
        bool CreateCache(T entity);
        T ReadCache(string cacheKey, int id);
        bool EditCache(T entity);
    }
}
