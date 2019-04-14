using SharingService.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SharingService.Data.Service
{
    public interface IRepository<TModel>
        where TModel : AbstractEntity
    {
        Task<List<TModel>> AllAsync();
        Task<List<TModel>> FilterAsync(Expression<Func<TModel, bool>> filter);
        Task<TModel> GetByIdAsync(int id);
        Task<List<TModel>> GetByIdsAsync(List<int> ids);
        Task<TModel> SaveAsync(TModel item);
        Task DeleteAsync(int id);
        Task DeleteRangeAsync(List<int> idsToDelete);
        Task DeleteAllAsync();
    }
}
