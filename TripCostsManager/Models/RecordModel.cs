using System.Collections.Generic;
using TripCostsManager.Domain.Entities.Entities;

namespace TripCostsManager.Models
{
    public class RecordModel
    {
        public int RecordId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<ImageEntity> Images { get; set; }

        public void Clear()
        {
            var props = typeof(RecordModel).GetProperties();
            foreach (var prop in props)
                prop.SetValue(this, null, null);
        }

        public void Load(RecordEntity entity)
        {
            this.Clear();

            this.RecordId = entity.Id;

            var propsModel = typeof(RecordModel).GetProperties();
            var entityType = typeof(RecordEntity);

            foreach (var prop in propsModel)
            {
                var entityProp = entityType.GetProperty(prop.Name);
                if (entityProp != null)
                {
                    var value = entityProp.GetValue(entity);
                    prop.SetValue(this, value);
                }
            }
        }
    }
}
