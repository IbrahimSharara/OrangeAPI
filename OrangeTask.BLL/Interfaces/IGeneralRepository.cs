using OrangeTask.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeTask.BLL.Interfaces
{
    public interface IGeneralRepository <TEntity> where TEntity : BaseEntity
    {
        #region Retrive data
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(object id);
        TEntity? GetById(object id);

        #endregion

        #region Update
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entity);
        Task UpdateAndSaveChangesAsync(TEntity entity);
        Task UpdateAndSaveChangesAsync(IEnumerable<TEntity> entity);

        #endregion

        #region Delete
        void Delete(object id);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        Task DeleteAndSaveChangesAsync(object id);
        Task DeleteAndSaveChangesAsync(IEnumerable<TEntity> entities);
        Task DeleteAndSaveChangesAsync(TEntity entity);
        void DeleteAndSaveChanges(object id);
        void DeleteAndSaveChanges(IEnumerable<TEntity> entities);
        void DeleteAndSaveChanges(TEntity entity);
        #endregion

        #region Insert
        TEntity Add(TEntity entity);
        Task<TEntity> InsertAndSaveChangesAsync(TEntity entity);
        Task InsertAndSaveChangesAsync(IEnumerable<TEntity> entity);
        Task<bool> InsertAndSaveAsync(TEntity entity);
        TEntity InsertAndSaveChanges(TEntity entity);
        void InsertAndSaveChanges(IEnumerable<TEntity> entity);
        void InsertAdd(TEntity entity);
        Task<bool> InsertAsync(TEntity entity);
        void InsertAdd(IEnumerable<TEntity> entity);
        Task InsertAsync(IEnumerable<TEntity> entity);

        #endregion

        #region Save changes
        Task SaveChangesAcync();
        void SaveChanges();
        #endregion
    }
}
