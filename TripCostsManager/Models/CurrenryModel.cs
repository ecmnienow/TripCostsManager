using System;
using TripCostsManager.Domain.Entities.Entities;

namespace TripCostsManager.Models
{
    public class CurrencyModel
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string CurrencySymbol { get; set; }
        public string Value { get; set; }

        public void Clear()
        {
            var props = typeof(CurrencyModel).GetProperties();
            foreach (var prop in props)
                prop.SetValue(this, null, null);
        }

        public void Load(CurrencyEntity entity)
        {
            this.Clear();

            this.CurrencyId = entity.Id;

            var propsModel = typeof(CurrencyModel).GetProperties();
            var entityType = typeof(CurrencyEntity);

            foreach (var prop in propsModel)
            {
                var entityProp = entityType.GetProperty(prop.Name);
                if (entityProp != null)
                {
                    var value = entityProp.GetValue(entity);

                    if (value == null)
                        continue;
                    else if (entityProp.PropertyType == typeof(decimal))
                        prop.SetValue(this, Convert.ToDecimal(value).ToString("0.0000"));
                    else
                        prop.SetValue(this, value);
                }
            }
        }
    }
}
