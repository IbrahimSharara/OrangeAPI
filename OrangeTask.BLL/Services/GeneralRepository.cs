using Microsoft.EntityFrameworkCore;
using OrangeTask.BLL.Interfaces;
using OrangeTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeTask.BLL.Services
{
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : BaseEntity
    {
        #region Injection
        private readonly OrangeTaskContext _context;
        private readonly DbSet<TEntity> _dbset;
        #endregion

        #region Constructor
        public GeneralRepository(OrangeTaskContext context)
        {
            this._context = context;
            _dbset = _context.Set<TEntity>();
        }
        #endregion

        #region Insert
        public TEntity Add(TEntity entity)
        {
            var returnEntity = _dbset.Add(entity).Entity;
            return returnEntity;
        }
        public void InsertAdd(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbset.Add(entity);
        }

        public void InsertAdd(IEnumerable<TEntity> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbset.AddRange(entity);
        }

        public async Task<bool> InsertAndSaveAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _dbset.AddAsync(entity);
            if (await _context.SaveChangesAsync() == 0) return false;
            return true;
        }

        public TEntity InsertAndSaveChanges(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity = _dbset.Add(entity).Entity;
            _context.SaveChanges();
            return entity;
        }

        public void InsertAndSaveChanges(IEnumerable<TEntity> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbset.AddRange(entity);
            _context.SaveChanges();
        }

        public async Task<TEntity> InsertAndSaveChangesAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity = (await _dbset.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task InsertAndSaveChangesAsync(IEnumerable<TEntity> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _dbset.AddRangeAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _dbset.AddAsync(entity);
            await _context.SaveChangesAsync();
            if (await _context.SaveChangesAsync() == 0)
            {
                return false;
            }
            return true;
        }

        public async Task InsertAsync(IEnumerable<TEntity> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _dbset.AddRangeAsync(entity);
        }
        #endregion

        #region Delete
        public void Delete(object id)
        {
            var entity = _dbset.Find(id);
            if (entity != null)
                _dbset.Remove(entity);
        }
        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbset.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _dbset.RemoveRange(entities);
        }
        public void DeleteAndSaveChanges(object id)
        {
            var entity = _dbset.Find(id);
            if (entity != null)
            {
                _dbset.Remove(entity);
                _context.SaveChanges();
            }
        }

        public void DeleteAndSaveChanges(IEnumerable<TEntity> entities)
        {
            if (entities != null)
            {
                _dbset.RemoveRange(entities);
                _context.SaveChanges();
            }
            else
                throw new ArgumentNullException(nameof(entities));
        }

        public void DeleteAndSaveChanges(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbset.Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteAndSaveChangesAsync(object id)
        {
            var entity = await _dbset.FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAndSaveChangesAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            _dbset.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAndSaveChangesAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Get Data

        public IQueryable<TEntity> GetAll()
        {
            return _dbset.AsQueryable();
        }

        public async Task<TEntity?> GetByIdAsync(object id) => await _dbset.FindAsync(id);
        public TEntity? GetById(object id) => _dbset.Find(id);

        //public async Task<IEnumerable<TEntity>> GetAllWithSpec(ISpecification<TEntity> spec)
        //{
        //    var s = await ApplySpecification(spec).ToListAsync();
        //    return s;
        //}

        //public async Task<TEntity> GetEntityWithSpec(ISpecification<TEntity> spec)
        //{
        //    return await ApplySpecification(spec).FirstOrDefaultAsync();
        //}
        #endregion

        #region Save changes
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAcync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Update
        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbset.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbset.UpdateRange(entity);
        }

        public async Task UpdateAndSaveChangesAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbset.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAndSaveChangesAsync(IEnumerable<TEntity> entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _dbset.UpdateRange(entity);
            await _context.SaveChangesAsync();
        }
        #endregion


        #region Helper
        //private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        //{
        //    var result = SpecificationEvaluator<TEntity>.GetQuery(spec, _context.Set<TEntity>());
        //    return result;
        //}
        #endregion
    }
}
