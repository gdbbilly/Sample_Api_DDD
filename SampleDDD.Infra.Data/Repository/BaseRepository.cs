using SampleDDD.Domain.Entities;
using SampleDDD.Domain.Interfaces;
using SampleDDD.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace SampleDDD.Infra.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly SqlContext _sqlContext;

        public BaseRepository(SqlContext mySqlContext)
        {
            _sqlContext = mySqlContext;
        }

        public void Insert(TEntity obj)
        {
            _sqlContext.Set<TEntity>().Add(obj);
            _sqlContext.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _sqlContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _sqlContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _sqlContext.Set<TEntity>().Remove(Select(id));
            _sqlContext.SaveChanges();
        }

        public IList<TEntity> Select() =>
            _sqlContext.Set<TEntity>().ToList();

        public TEntity Select(int id) =>
            _sqlContext.Set<TEntity>().Find(id);

    }
}
