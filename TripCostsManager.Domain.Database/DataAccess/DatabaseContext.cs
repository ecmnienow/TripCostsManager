using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripCostsManager.Domain.Entities.Interfaces;
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Domain.Database.Interfaces;

namespace TripCostsManager.Domain.Database.DataAccess
{
    public class DatabaseContext : DbContext, IDataAccess
    {
        #region Constructor

#if DEBUG
        public DatabaseContext() : base("Data Source=DESKTOP-7S103UN\\SQLEXPRESS;Initial Catalog=TripCostsManager_DEV;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true")
        {
            this.Database.CommandTimeout = 300;
            this.Database.CreateIfNotExists();

            this.Configuration.LazyLoadingEnabled = true;
        }
#else
        public DatabaseContext() : base("Data Source=DESKTOP-7S103UN\\SQLEXPRESS;Initial Catalog=TripCostsManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true")
        {
            this.Database.CommandTimeout = 300;
            this.Database.CreateIfNotExists();

            this.Configuration.LazyLoadingEnabled = true;
        }
#endif

        //public DatabaseContext() : base("TripCostsManager")
        //{
        //    /*
        //     create login [IIS APPPool\DefaultAPPPool] from windows;
        //     exec sp_addsrvrolemember [IIS APPPool\DefaultAPPPool], @rolename = sysadmin
        //     */

        //    this.Database.CreateIfNotExists();

        //    this.Configuration.LazyLoadingEnabled = true;
        //}

        #endregion

        #region Private Method

        private void Attach<T>(T entity) where T : BaseEntity
        {
            if (typeof(T) == typeof(RecordEntity))
                RecordSet.Attach((RecordEntity)Convert.ChangeType(entity, typeof(T)));
            else if (typeof(T) == typeof(ImageEntity))
                ImagesSet.Attach((ImageEntity)Convert.ChangeType(entity, typeof(T)));
            else
                throw new NotImplementedException();
        }

        #endregion

        #region Public Methods

        public IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            return Set<T>();
        }

        public T Get<T>(int id) where T : BaseEntity
        {
            return Set<T>().Find(id);
        }

        //public T Save<T>(T entity) where T : BaseEntity
        //{
        //    if (entity.Id > 0)
        //    {
        //        entity.Modification = DateTime.Now;
        //        Entry(entity).CurrentValues.SetValues(entity);

        //        return entity;
        //    }

        //    return Set<T>().Add(entity);
        //}

        public T Save<T>(T entity) where T : BaseEntity
        {
            if (entity.Id > 0)
            {
                if (Entry(entity).State == EntityState.Detached)
                    this.Attach(entity);

                entity.Modification = DateTime.Now;
                Entry(entity).CurrentValues.SetValues(entity);
                Entry(entity).State = EntityState.Modified;

                return entity;
            }

            return Set<T>().Add(entity);
        }

        //public void Delete<T>(int id) where T : BaseEntity
        //{
        //    var entity = this.Get<T>(id);
        //    Set<T>().Remove(entity);
        //}

        public void Delete<T>(int id) where T : BaseEntity
        {
            var entity = this.Get<T>(id);

            if (Entry(entity).State == EntityState.Detached)
                this.Attach(entity);

            Set<T>().Remove(entity);
        }

        //public void Delete<T>(T entity) where T : BaseEntity
        //{
        //    Set<T>().Remove(entity);
        //}

        public void Delete<T>(T entity) where T : BaseEntity
        {
            if (Entry(entity).State == EntityState.Detached)
                this.Attach(entity);

            Set<T>().Remove(entity);
        }

        public IDbTransaction GetTransaction()
        {
            var transaction = this.Database.BeginTransaction();
            return transaction.UnderlyingTransaction;
        }

        public void SetUnchanged<T>(T entity) where T : BaseEntity
        {
            Entry(entity).State = EntityState.Unchanged;
        }

        #endregion

        #region DbSets

        public IDbSet<ImageEntity> ImagesSet { get; set; }
        public IDbSet<RecordEntity> RecordSet { get; set; }

        #endregion

        #region Overriden Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Data.Entity.Database.SetInitializer<DatabaseContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}

