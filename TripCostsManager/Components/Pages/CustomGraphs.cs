using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TripCostsManager.Components.Component;
using TripCostsManager.Domain.Database.Services;
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Models;

namespace TripCostsManager.Components.Pages
{
    public partial class CustomGraphs : ComponentBase
    {
        #region Constructor

        public CustomGraphs()
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

        [Parameter]
        public Action<MouseEventArgs, ItemTypeEntity> OnAddItemTypeClick { get; set; }



        private static List<CustomGraphEntity> _customGraphsList { get; } = new List<CustomGraphEntity>();
        public List<CustomGraphEntity> CustomGraphsList
        {
            get { return _customGraphsList; }
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
        
        private async Task LoadCustomGraphs()
        {
            if (this.CustomGraphsList.Any())
                return;

            var customGraphsList = (await RecordsService.GetAllCustomGraphs())
                    .ToList();

            foreach (var news in customGraphsList)
                this.CustomGraphsList.Add(news);
        }
        
        private List<ReportRowModel> GetCustomGraphData(string script)
        {
            var customGraphsList = RecordsService.GetCustomGraphData(script).Result
                   .ToList();

            //var customGraphsList = (await RecordsService.GetCustomGraphData(script))
            //        .ToList();

            //foreach (var news in customGraphsList)
            //    this.CustomGraphsList.Add(news);
            return customGraphsList;
        }

        private void ShowAlert(string title, string message)
        {
            this.AlertModel = new AlertModel();
            this.AlertModel.Title = title;
            this.AlertModel.Message = message;

            ModalAlert.Open();
        }

        #endregion

        #region Public Methods

        public async Task OnHandleAddItemTypeClick()
        {
            var n = new ItemTypeEntity()
            {
                Name = string.Empty
            };

            await AddItemType(n);
        }

        #endregion

        #region Overridden Methods

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

            this.CustomGraphsList.Clear();
            await LoadCustomGraphs();
        }

        #endregion
    }
}
