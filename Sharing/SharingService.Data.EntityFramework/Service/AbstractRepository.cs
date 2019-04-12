using Microsoft.EntityFrameworkCore;
using SharingService.Data.Model;
using SharingService.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SharingService.Data.EntityFramework.Service
{
    public abstract class AbstractRepository<TModel> : IRepository<TModel>
        where TModel : AbstractEntity
    {
        protected readonly SharingServiceContext Context;

        protected AbstractRepository(SharingServiceContext context)
        {
            Context = context;
        }

        protected DbSet<TModel> GetDbSet()
        {
            return Context.Set<TModel>();
        }

        protected abstract void CopyProperties(TModel source, TModel destination);

        public virtual Task<List<TModel>> All()
        {
            return GetDbSet().ToListAsync();
        }

        public virtual Task<TModel> GetById(int id)
        {
            return GetDbSet().Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public virtual Task<List<TModel>> GetByIds(List<int> ids)
        {
            return GetDbSet().Where(p => ids.Contains(p.Id)).ToListAsync();
        }

        public virtual async Task Save(TModel item)
        {
            var toSave = item;
            if (item.Id > 0)
            {
                toSave = await GetById(item.Id);
                CopyProperties(item, toSave);
            }
            else
            {
                await GetDbSet().AddAsync(toSave);
            }

            await Context.SaveChangesAsync();
        }

        public virtual async Task Edit(TModel item)
        {
            if (item.Id > 0)
            {
                var toSave = await GetById(item.Id);
                CopyProperties(item, toSave);
            }

            await Context.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            var item = await GetById(id);
            if (item != null)
            {
                GetDbSet().Remove(item);
            }

            await Context.SaveChangesAsync();
        }

        public virtual async Task Delete(List<int> ids)
        {
            var items = await GetByIds(ids);
            if (items != null && items.Count > 0)
            {
                GetDbSet().RemoveRange(items);
            }

            await Context.SaveChangesAsync();
        }

        public virtual Task<List<TModel>> Filter(Expression<Func<TModel, bool>> filter)
        {
            if (filter == null)
            {
                return Task.FromResult(new List<TModel>());
            }

            var queryable = GetDbSet().AsQueryable();
            queryable = queryable.Where(filter);
            return queryable.ToListAsync();
        }
    }
}
