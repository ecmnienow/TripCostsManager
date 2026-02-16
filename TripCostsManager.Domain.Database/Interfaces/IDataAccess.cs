using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCostsManager.Domain.Entities.Entities;

namespace TripCostsManager.Domain.Database.Interfaces
{
    public interface IDataAccess
    {
        IQueryable<T> GetAll<T>() where T : BaseEntity;
        T Get<T>(int id) where T : BaseEntity;
        T Save<T>(T entity) where T : BaseEntity;
        void Delete<T>(int id) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        IDbTransaction GetTransaction();
        int SaveChanges();
        void SetUnchanged<T>(T entity) where T : BaseEntity;
    }
}
