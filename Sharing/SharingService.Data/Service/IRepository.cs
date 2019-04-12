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
        Task<List<TModel>> All();
        Task<List<TModel>> Filter(Expression<Func<TModel, bool>> filter);
        Task<TModel> GetById(int id);
        Task<List<TModel>> GetByIds(List<int> ids);
        Task Save(TModel item);
        Task Edit(TModel item);
        Task Delete(int id);
    }
}
