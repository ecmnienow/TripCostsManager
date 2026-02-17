using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TripCostsManager.Components.Component;
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Models;

namespace TripCostsManager.Components.Pages
{
    public partial class ItemType : ComponentBase
    {
        private async Task LoadItemTypes()
        {
            if (this.ItemTypesList.Any())
                return;

            var newsList = (await ItemTypesService.GetAllItemTypesAsync())
                    .ToList();

            foreach (var news in newsList)
                this.ItemTypesList.Add(news);
        }

        private static List<ItemTypeEntity> _itemTypeList { get; } = new List<ItemTypeEntity>();
        public List<ItemTypeEntity> ItemTypesList
        {
            get { return _itemTypeList; }
        }

        protected override async Task OnParametersSetAsync()
        {
            var regex = new Regex("http[s]{0,1}:\\/\\/localhost:[0-9]*?\\/");
            if (!regex.IsMatch(NavigationManager.Uri) &&
                !NavigationManager.Uri.EndsWith("/Favorites") &&
                !NavigationManager.Uri.EndsWith("/Search") &&
                !NavigationManager.Uri.EndsWith("/games") &&
                !NavigationManager.Uri.EndsWith("/history/1") &&
                !NavigationManager.Uri.EndsWith("/history/7") &&
                !NavigationManager.Uri.EndsWith("/history/30") &&
                !NavigationManager.Uri.EndsWith("/Logs"))
            {
                try
                {
                    await jsRuntime.InvokeVoidAsync("console.info", $"Currenmt URL: {NavigationManager.Uri}");
                }
                catch { }

                return;
            }

            this.ItemTypesList.Clear();
            await LoadItemTypes();
        }

        [Parameter]
        public Action<MouseEventArgs, ItemTypeEntity> OnAddItemTypeClick { get; set; }


        #region Constructor

        public ItemType()
        {
            AlertModalContext = new EditContext(AlertModel);
            ItemTypeModelContext = new EditContext(ItemTypeModel);
        }

        #endregion

        #region Attributes and Properties

        public Modal Modal { get; set; }
        public Modal ModalAlert { get; set; }

        public AlertModel AlertModel { get; set; } = new AlertModel();
        public ItemTypeModel ItemTypeModel { get; set; } = new ItemTypeModel();

        public EditContext AlertModalContext { get; set; }
        public EditContext ItemTypeModelContext { get; set; }




        private ItemTypeEntity _model = null;
        [Parameter]
        public ItemTypeEntity Model
        {
            get { return this._model; }
            set { this._model = value; }
        }

        #endregion

        #region Private Methods

        private async Task AddItemType(ItemTypeEntity itemTypeEntity)
        {
            try
            {
                this.ItemTypeModelContext.ClearContext();
            }
            catch (Exception ex)
            {
                await jsRuntime.InvokeVoidAsync("console.error", ex.ToString());
            }

            this.ItemTypeModel.Clear();

            Modal.Open();
        }

        private async Task EditItemType(MouseEventArgs e, ItemTypeEntity itemTypeEntity)
        {
            if (e.Button != 0)
                return;

            this.ItemTypeModel.Load(itemTypeEntity);
            ItemTypeModelContext.Validate();

            Modal.Open();
        }

        private async Task SaveItemType()
        {
            if (!ItemTypeModelContext.Validate())
                return;

            this.ItemTypeModel.Name = this.ItemTypeModel.Name.Trim();

            try
            {
                await ItemTypesService.SaveItemType(this.ItemTypeModel);

                await Update();

                Modal.Close();
            }
            catch (Exception ex)
            {
                ShowAlert("Erro", "Não foi possível cadastrar o tio de item \"" + this.ItemTypeModel.Name + "\".<br/><br/>" + ex.GetInnerExceptionMessage());
            }
        }

        private async Task Update(int id)
        {
            var updatedNews = await ItemTypesService.GetItemTypeAsync(id);

            var index = this.ItemTypesList.IndexOf(this.ItemTypesList.First(x => x.Id == id));
            this.ItemTypesList.RemoveAt(index);
            this.ItemTypesList.Insert(index, updatedNews);
        }

        private async Task Update(bool reload = false)
        {
            List<ItemTypeEntity> updatedList = updatedList = (await ItemTypesService.GetAllItemTypesAsync()).ToList();

            this.ItemTypesList.Clear();
            this.ItemTypesList.AddRange(updatedList);

            StateHasChanged();
        }

        private void ShowAlert(string title, string message)
        {
            this.AlertModel = new AlertModel();
            this.AlertModel.Title = title;
            this.AlertModel.Message = message;

            ModalAlert.Open();
        }

        #endregion

        public async Task OnHandleAddItemTypeClick()
        {
            var n = new ItemTypeEntity()
            {
                Name = string.Empty
            };

            await AddItemType(n);
        }
    }
}
