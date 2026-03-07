using TripCostsManager.Domain.Database.Interfaces;
using TripCostsManager.Domain.Entities.Entities;

namespace TripCostsManager.Domain.Database.Services
{
    public class CustomGraphsDbService : BaseService<CustomGraphEntity>
    {
        #region Constructor

        public CustomGraphsDbService(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        #endregion

        #region Public Methods

        public IQueryable<CustomGraphEntity> GetAll()
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
