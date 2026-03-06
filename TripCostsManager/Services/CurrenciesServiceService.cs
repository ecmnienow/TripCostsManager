using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TripCostsManager.Domain.Database.Services;
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Models;

namespace TripCostsManager.Services
{
    public class CurrenciesService
    {
        public CurrenciesService(CurrenciesDbService currenciesDbService)
        {
            this._currenciesDbService = currenciesDbService;
        }

        private CurrenciesDbService _currenciesDbService;

        public Task<CurrencyEntity[]> GetAllCurrenciesAsync()
        {
            var currenciesList = this._currenciesDbService.GetAll()
                .ToList();

            return Task.FromResult(currenciesList.ToArray());
        }

        public Task<CurrencyEntity[]> GetAllCurrenciesAsync(Expression<Func<CurrencyEntity, bool>> extraClause = null)
        {
            var newsListQuery = this._currenciesDbService.GetAll();

            if (extraClause != null)
                newsListQuery = newsListQuery.Where(extraClause);

            var newsList = newsListQuery.ToList();

            return Task.FromResult(newsListQuery.ToArray());
        }

        public Task<CurrencyEntity> GetCurrencyAsync(int id)
        {
            var currenciesList = this._currenciesDbService.GetAll()
                .Where(x => x.Id == id)
                .First();

            return Task.FromResult(currenciesList);
        }

        public async Task SaveCurrency(CurrencyModel model)
        {
            CurrencyEntity currency;
            if (model.CurrencyId > 0)
            {
                currency = await this.GetCurrencyAsync(model.CurrencyId);

                var propsModel = typeof(CurrencyModel).GetProperties();
                var entityType = typeof(CurrencyEntity);

                foreach (var prop in propsModel)
                {
                    var entityProp = entityType.GetProperty(prop.Name);
                    if (entityProp != null && entityProp.Name != "Id")
                    {
                        var value = prop.GetValue(model);

                        if (value?.ToString() == entityProp.GetValue(currency)?.ToString())
                            continue;

                        if (entityProp.PropertyType == typeof(DateTime))
                            entityProp.SetValue(currency, Convert.ToDateTime(value));
                        else if (entityProp.PropertyType == typeof(decimal))
                            entityProp.SetValue(currency, Convert.ToDecimal(value));
                        else
                            entityProp.SetValue(currency, value);
                    }
                    else if (prop.Name != "CurrencyId")
                        throw new Exception($"Propert \"{prop.Name}\" could not be found in database mapping entity");
                }

                await this.UpdateCurrencyAsync(currency);
            }
            else
            {
                currency = new CurrencyEntity()
                {
                    Name = model.Name,
                    CurrencySymbol = model.CurrencySymbol,
                    Value = 1m
                };

                var propsModel = typeof(CurrencyModel).GetProperties();
                var entityType = typeof(CurrencyEntity);

                foreach (var prop in propsModel)
                {
                    var entityProp = entityType.GetProperty(prop.Name);
                    if (entityProp != null && entityProp.Name != "Id")
                    {
                        var value = prop.GetValue(model);

                        if (value?.ToString() == entityProp.GetValue(currency)?.ToString())
                            continue;

                        else if (entityProp.PropertyType == typeof(decimal))
                            entityProp.SetValue(currency, Convert.ToDecimal(value));
                        else
                            entityProp.SetValue(currency, value);
                    }
                }

                await this.AddCurrencyAsync(currency);
            }
        }

        public Task UpdateCurrencyAsync(CurrencyEntity currency)
        {
            this._currenciesDbService.Save(currency);

            return Task.CompletedTask;
        }

        public Task AddCurrencyAsync(CurrencyEntity currency)
        {
            this._currenciesDbService.Save(currency);

            return Task.CompletedTask;
        }
    }
}
