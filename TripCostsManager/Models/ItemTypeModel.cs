using System;
using System.Collections.Generic;
using TripCostsManager.Domain.Entities.Entities;

namespace TripCostsManager.Models
{
    public class ItemTypeModel
    {
        public int ItemTypeId { get; set; }
        public string Name { get; set; }

        public void Clear()
        {
            var props = typeof(ItemTypeModel).GetProperties();
            foreach (var prop in props)
                prop.SetValue(this, null, null);
        }

        public void Load(ItemTypeEntity entity)
        {
            this.Clear();

            this.ItemTypeId = entity.Id;

            var propsModel = typeof(ItemTypeModel).GetProperties();
            var entityType = typeof(ItemTypeEntity);

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
