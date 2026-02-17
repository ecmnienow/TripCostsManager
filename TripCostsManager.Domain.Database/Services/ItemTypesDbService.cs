using System;
using System.Linq;
using System.Net.NetworkInformation;
using TripCostsManager.Domain.Database.Interfaces;
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Domain.Entities.Enums;

namespace TripCostsManager.Domain.Database.Services
{
    public class ItemTypesDbService : BaseService<ItemTypeEntity>
    {
        #region Constructor

        public ItemTypesDbService(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        #endregion

        #region Public Methods

        public IQueryable<ItemTypeEntity> GetAll()
        {
            return base.GetAll();
        }

        //public IQueryable<RecordEntity> GetAll(bool onlyActive)
        //{
        //    return this.GetAll()
        //        .Where(x => x.Status != EStatus.Deleted)
        //        .OrderByDescending(x => x.Creation);
        //}

        //public void CloseTask(int id)
        //{
        //    var entity = this.Get(id);
        //    entity.Status = EStatus.Done;
        //    entity.Conclusion = DateTime.Now;

        //    this.Save(entity);
        //}

        //public void OpenTask(int id)
        //{
        //    var entity = this.Get(id);
        //    entity.Status = EStatus.Open;
        //    entity.Conclusion = null;

        //    this.Save(entity);
        //}

        //public void DeleteTask(int id)
        //{
        //    var entity = this.Get(id);
        //    entity.Status = EStatus.Deleted;
        //    entity.Exclusion = DateTime.Now;

        //    this.Save(entity);
        //}

        #endregion
    }
}
