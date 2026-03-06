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
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Models;

namespace TripCostsManager.Components.Pages
{
    public partial class Currency : ComponentBase
    {
        #region Constructor

        public Currency()
        {
            AlertModalContext = new EditContext(AlertModel);
            CurrencyModelContext = new EditContext(CurrencyModel);
        }

        #endregion

        #region Attributes and Properties

        public Modal Modal { get; set; }
        public Modal ModalAlert { get; set; }

        public AlertModel AlertModel { get; set; } = new AlertModel();
        public CurrencyModel CurrencyModel { get; set; } = new CurrencyModel();

        public EditContext AlertModalContext { get; set; }
        public EditContext CurrencyModelContext { get; set; }



        private CurrencyEntity _model = null;
        [Parameter]
        public CurrencyEntity Model
        {
            get { return this._model; }
            set { this._model = value; }
        }

        [Parameter]
        public Action<MouseEventArgs, CurrencyEntity> OnAddCurrencyClick { get; set; }



        private static List<CurrencyEntity> _currencyList { get; } = new List<CurrencyEntity>();
        public List<CurrencyEntity> CurrenciesList
        {
            get { return _currencyList; }
        }

        #endregion

        #region Private Methods

        private async Task AddCurrency(CurrencyEntity currencyEntity)
        {
            try
            {
                this.CurrencyModelContext.ClearContext();
            }
            catch (Exception ex)
            {
                await jsRuntime.InvokeVoidAsync("console.error", ex.ToString());
            }

            this.CurrencyModel.Clear();

            Modal.Open();
        }

        private async Task EditCurrency(MouseEventArgs e, CurrencyEntity currencyEntity)
        {
            if (e.Button != 0)
                return;

            this.CurrencyModel.Load(currencyEntity);
            CurrencyModelContext.Validate();

            Modal.Open();
        }

        private async Task SaveCurrency()
        {
            if (!CurrencyModelContext.Validate())
                return;

            this.CurrencyModel.Name = this.CurrencyModel.Name.Trim();

            try
            {
                await CurrenciesService.SaveCurrency(this.CurrencyModel);

                await Update();

                Modal.Close();
            }
            catch (Exception ex)
            {
                ShowAlert("Erro", "Não foi possível cadastrar o tio de item \"" + this.CurrencyModel.Name + "\".<br/><br/>" + ex.GetInnerExceptionMessage());
            }
        }

        private async Task Update(int id)
        {
            var updatedNews = await CurrenciesService.GetCurrencyAsync(id);

            var index = this.CurrenciesList.IndexOf(this.CurrenciesList.First(x => x.Id == id));
            this.CurrenciesList.RemoveAt(index);
            this.CurrenciesList.Insert(index, updatedNews);
        }

        private async Task Update(bool reload = false)
        {
            List<CurrencyEntity> updatedList = updatedList = (await CurrenciesService.GetAllCurrenciesAsync()).ToList();

            this.CurrenciesList.Clear();
            this.CurrenciesList.AddRange(updatedList);

            StateHasChanged();
        }
        
        private async Task LoadCurrencies()
        {
            if (this.CurrenciesList.Any())
                return;

            var newsList = (await CurrenciesService.GetAllCurrenciesAsync())
                    .ToList();

            foreach (var news in newsList)
                this.CurrenciesList.Add(news);
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

        public async Task OnHandleAddCurrencyClick()
        {
            var n = new CurrencyEntity()
            {
                Name = string.Empty,
                CurrencySymbol = string.Empty,
                Value = 1
            };

            await AddCurrency(n);
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

            this.CurrenciesList.Clear();
            await LoadCurrencies();
        }

        #endregion
    }
}
