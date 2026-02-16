using System;
using System.Linq;
using TripCostsManager.Domain.Database.Interfaces;
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Domain.Entities.Enums;

namespace TripCostsManager.Domain.Database.Services
{
    public class ServiceImage : BaseService<ImageEntity>
    {
        #region Constructor

        public ServiceImage(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        #endregion

        #region Public Methods

        public IQueryable<ImageEntity> GetAll(int taskId)
        {
            return this.GetAll()
                .Where(x => x.Record.Id == taskId)
                .OrderByDescending(x => x.Name);
        }

        #endregion

        public ImageEntity GetByTask(int taskId)
        {
            return this.GetAll()
                .FirstOrDefault(x => x.Record.Id == taskId);
        }

        public override void Save(ImageEntity entity, bool commit = true)
        {
            //entity.Task.Images = null;
            //base.SetUnchanged(entity.Task);
            base.Save(entity, commit);
        }
    }
}
