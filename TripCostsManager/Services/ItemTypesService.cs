using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TripCostsManager.Components.Pages;
using TripCostsManager.Domain.Database.Services;
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Models;

namespace TripCostsManager.Services
{
    public class ItemTypesService
    {
        public ItemTypesService(ItemTypesDbService itemTypesDbService)
        {
            this._itemTypesDbService = itemTypesDbService;
        }

        private ItemTypesDbService _itemTypesDbService;

        public Task<ItemTypeEntity[]> GetAllItemTypesAsync()
        {
            var itemTypesList = this._itemTypesDbService.GetAll()
                .ToList();

            return Task.FromResult(itemTypesList.ToArray());
        }

        public Task<ItemTypeEntity[]> GetAllItemTypesAsync(Expression<Func<ItemTypeEntity, bool>> extraClause = null)
        {
            var newsListQuery = this._itemTypesDbService.GetAll();

            if (extraClause != null)
                newsListQuery = newsListQuery.Where(extraClause);

            var newsList = newsListQuery.ToList();

            return Task.FromResult(newsListQuery.ToArray());
        }

        public Task<ItemTypeEntity> GetItemTypeAsync(int id)
        {
            var itemTypesList = this._itemTypesDbService.GetAll()
                .Where(x => x.Id == id)
                .First();

            return Task.FromResult(itemTypesList);
        }

        public async Task SaveItemType(ItemTypeModel model)
        {
            ItemTypeEntity itemType;
            if (model.ItemTypeId > 0)
            {
                itemType = await this.GetItemTypeAsync(model.ItemTypeId);

                var propsModel = typeof(ItemTypeModel).GetProperties();
                var entityType = typeof(ItemTypeEntity);

                foreach (var prop in propsModel)
                {
                    var entityProp = entityType.GetProperty(prop.Name);
                    if (entityProp != null && entityProp.Name != "Id")
                    {
                        var value = prop.GetValue(model);

                        if (value?.ToString() == entityProp.GetValue(itemType)?.ToString())
                            continue;

                        if (entityProp.PropertyType == typeof(DateTime))
                            entityProp.SetValue(itemType, Convert.ToDateTime(value));
                        else if (entityProp.PropertyType == typeof(decimal))
                            entityProp.SetValue(itemType, Convert.ToDecimal(value));
                        else
                            entityProp.SetValue(itemType, value);
                    }
                    else if (prop.Name != "ItemTypeId")
                        throw new Exception($"Propert \"{prop.Name}\" could not be found in database mapping entity");
                }

                await this.UpdateItemTypeAsync(itemType);
            }
            else
            {
                itemType = new ItemTypeEntity()
                {
                    Name = model.Name
                };

                var propsModel = typeof(ItemTypeModel).GetProperties();
                var entityType = typeof(ItemTypeEntity);

                foreach (var prop in propsModel)
                {
                    var entityProp = entityType.GetProperty(prop.Name);
                    if (entityProp != null && entityProp.Name != "Id")
                    {
                        var value = prop.GetValue(model);
                        if (value?.ToString() != entityProp.GetValue(itemType)?.ToString())
                            entityProp.SetValue(itemType, value);
                    }
                }

                await this.AddItemTypeAsync(itemType);
            }
        }

        public Task UpdateItemTypeAsync(ItemTypeEntity itemType)
        {
            this._itemTypesDbService.Save(itemType);

            return Task.CompletedTask;
        }

        public Task AddItemTypeAsync(ItemTypeEntity itemType)
        {
            this._itemTypesDbService.Save(itemType);

            return Task.CompletedTask;
        }
    }
}
