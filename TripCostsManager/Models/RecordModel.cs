using System;
using System.Collections.Generic;
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Domain.Entities.Enums;

namespace TripCostsManager.Models
{
    public class RecordModel
    {
        public int RecordId { get; set; }
        public string MarketName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EPurpose Purpose { get; set; }
        public string Price { get; set; }
        public string DateTime { get; set; }
        public ItemTypeEntity ItemType { get; set; }
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
                //if (prop.Name.EndsWith("Id"))
                //    continue;

                var entityProp = entityType.GetProperty(prop.Name);
                if (entityProp != null)
                {
                    var value = entityProp.GetValue(entity);

                    if (value == null)
                        continue;
                    else if (entityProp.PropertyType == typeof(decimal))
                        prop.SetValue(this, Convert.ToDecimal(value).ToString("#,##0.00"));
                    else if (entityProp.PropertyType == typeof(DateTime))
                        prop.SetValue(this, Convert.ToDateTime(value).ToString("dd/MM/yyyy HH:mm:ss"));
                    else
                        prop.SetValue(this, value);
                }
            }
        }
    }
}
