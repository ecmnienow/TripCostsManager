using System.Linq;
using TripCostsManager.Domain.Database.Interfaces;
using TripCostsManager.Domain.Entities.Entities;

namespace TripCostsManager.Domain.Database.Services
{
    public class BaseService<T> where T : BaseEntity
    {
        #region Constructor

        public BaseService(IDataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        #endregion

        #region Private Fields

        private IDataAccess _dataAccess;

        #endregion

        #region Virtual Methods

        public virtual IQueryable<T> GetAll()
        {
            return this._dataAccess.GetAll<T>();
        }

        public virtual T Get(int id)
        {
            return this._dataAccess.Get<T>(id);
        }

        public virtual void Save(T entity, bool commit = true)
        {
            this._dataAccess.Save(entity);
            if (commit)
                this._dataAccess.SaveChanges();
        }

        public virtual void Delete(int id, bool commit = true)
        {
            this._dataAccess.Delete<T>(id);
            if (commit)
                this._dataAccess.SaveChanges();
        }

        public virtual void Delete(T entity, bool commit = true)
        {
            this._dataAccess.Delete(entity);
            if (commit)
                this._dataAccess.SaveChanges();
        }

        public virtual void SetUnchanged(BaseEntity entity)
        {
            this._dataAccess.SetUnchanged(entity);
        }

        #endregion
    }
}
