using System;
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
        public RecordsService(CustomGraphsDbService customGraphsDbService, RecordsDbService recordsDbService)
        {
            this._customGraphsDbService = customGraphsDbService;
            this._recordsDbService = recordsDbService;
        }

        private CustomGraphsDbService _customGraphsDbService;
        private RecordsDbService _recordsDbService;

        public Task<RecordEntity[]> GetAllRecordsAsync()
        {
            var recordsList = this._recordsDbService.GetAll()
                .OrderByDescending(x => x.DateTime)
                .ToList();

            return Task.FromResult(recordsList.ToArray());
        }

        public Task<RecordEntity[]> GetAllRecordsAsync(Expression<Func<RecordEntity, bool>> extraClause = null)
        {
            var recordsListQuery = this._recordsDbService.GetAll();

            if (extraClause != null)
                recordsListQuery = recordsListQuery.Where(extraClause);

            var newsList = recordsListQuery.ToList();

            return Task.FromResult(recordsListQuery.ToArray());
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
                record.CurrencyId = model.Currency.Id;
                record.ItemTypeId = model.ItemType.Id;

                var propsModel = typeof(RecordModel).GetProperties();
                var entityType = typeof(RecordEntity);

                foreach (var prop in propsModel)
                {
                    var entityProp = entityType.GetProperty(prop.Name);
                    if (entityProp != null && entityProp.Name != "Id")
                    {
                        var value = prop.GetValue(model);

                        if (value?.ToString() == entityProp.GetValue(record)?.ToString())
                            continue;

                        if (entityProp.PropertyType == typeof(DateTime))
                            entityProp.SetValue(record, Convert.ToDateTime(value));
                        else if (entityProp.PropertyType == typeof(decimal))
                            entityProp.SetValue(record, Convert.ToDecimal(value));
                        else
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
                    MarketName = model.MarketName,
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

        public Task DeleteRecordAsync(int recordId)
        {
            this._recordsDbService.Delete(recordId);

            return Task.CompletedTask;
        }

        public Task<CustomGraphEntity[]> GetAllCustomGraphs()
        {
            var result = this._customGraphsDbService.GetAll();

            return Task.FromResult(result.ToArray());
        }

        public Task<ReportRowModel[]> GetCustomGraphData(string script)
        {
            //var customGraph = this._customGraphsDbService.GetAll().First(x => x.Title == "Alimentacao");

            //var result = this._recordsDbService.ExecuteSqlCommand<DietReportModel>(customGraph.Script);
            var result = this._recordsDbService.ExecuteSqlCommand<ReportRowModel>(script);

            return Task.FromResult(result.ToArray());
        }
    }
}
