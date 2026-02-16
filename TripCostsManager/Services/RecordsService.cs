using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TripCostsManager.Domain.Database.Services;
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Models;

namespace TripCostsManager.Services
{
    public class RecordsService
    {
        public RecordsService(RecordsDbService recordsDbService)
        {
            this._recordsDbService = recordsDbService;
        }

        private RecordsDbService _recordsDbService;

        public Task<RecordEntity[]> GetAllRecordsAsync()
        {
            var recordsList = this._recordsDbService.GetAll()
                .ToList();

            return Task.FromResult(recordsList.ToArray());
        }

        public Task<RecordEntity[]> GetAllRecordsAsync(Expression<Func<RecordEntity, bool>> extraClause = null)
        {
            var newsListQuery = this._recordsDbService.GetAll();

            if (extraClause != null)
                newsListQuery = newsListQuery.Where(extraClause);

            var newsList = newsListQuery.ToList();

            return Task.FromResult(newsListQuery.ToArray());
        }

        public Task<RecordEntity> GetRecordAsync(int id)
        {
            var recordsList = this._recordsDbService.GetAll()
                .Where(x => x.Id == id)
                .First();

            return Task.FromResult(recordsList);
        }

        public async Task SaveRecord(RecordModel model)
        {
            RecordEntity record;
            if (model.RecordId > 0)
            {
                record = await this.GetRecordAsync(model.RecordId);

                var propsModel = typeof(RecordModel).GetProperties();
                var entityType = typeof(RecordEntity);

                foreach (var prop in propsModel)
                {
                    var entityProp = entityType.GetProperty(prop.Name);
                    if (entityProp != null && entityProp.Name != "Id")
                    {
                        var value = prop.GetValue(model);
                        if (value?.ToString() != entityProp.GetValue(record)?.ToString())
                            entityProp.SetValue(record, value);
                    }
                    else if (prop.Name != "RecordId")
                        throw new Exception($"Propert \"{prop.Name}\" could not be found in database mapping entity");
                }

                await this.UpdateRecordAsync(record);
            }
            else
            {
                record = new RecordEntity()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Modification = DateTime.Now
                };

                var propsModel = typeof(RecordModel).GetProperties();
                var entityType = typeof(RecordEntity);

                foreach (var prop in propsModel)
                {
                    var entityProp = entityType.GetProperty(prop.Name);
                    if (entityProp != null && entityProp.Name != "Id")
                    {
                        var value = prop.GetValue(model);
                        if (value?.ToString() != entityProp.GetValue(record)?.ToString())
                        {
                            if (entityProp.PropertyType == typeof(DateTime))
                                entityProp.SetValue(record, Convert.ToDateTime(value));
                            else if (entityProp.PropertyType == typeof(decimal))
                                entityProp.SetValue(record, Convert.ToDecimal(value));
                            else
                                entityProp.SetValue(record, value);
                        }
                    }
                }

                await this.AddRecordAsync(record);
            }
        }

        public Task UpdateRecordAsync(RecordEntity record)
        {
            this._recordsDbService.Save(record);

            return Task.CompletedTask;
        }
        public Task AddRecordAsync(RecordEntity record)
        {
            this._recordsDbService.Save(record);

            return Task.CompletedTask;
        }
    }
}
