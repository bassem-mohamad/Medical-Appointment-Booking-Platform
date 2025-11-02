using Api_Project.Context;
using Api_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Project.Repository
{
    public class GenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApiDbContext _context;
        public GenericRepository(ApiDbContext _context)
        {
            this._context = _context;
        }

        public  IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public TEntity? GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        //public void Delete(TEntity entity)
        //{
        //    _context.Set<TEntity>().Remove(entity);
        //}
        public void Delete(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Set<TEntity>().Update(entity);
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
