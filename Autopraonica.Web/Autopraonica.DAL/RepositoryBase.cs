using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autopraonica.Model;
using System.Data.SqlTypes;

namespace Autopraonica.DAL
{
    public abstract class RepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected AutopraonicaDbContext DbContext { get; private set; }

        public IQueryable<TEntity> All
        {
            get
            {
                return DbContext.Set<TEntity>().AsQueryable();
            }
        }

        protected RepositoryBase(AutopraonicaDbContext db)
        {
            DbContext = db;
        }

        public TEntity Find(int id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity model)
        {
            model.DateCreated = DateTime.Now;
            DbContext.Set<TEntity>().Add(model);
        }

        public void Update(TEntity model)
        {
        }

        public void Delete(int id)
        {
            var obj = DbContext.Set<TEntity>().Find(id);
            DbContext.Set<TEntity>().Remove(obj);
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
