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
    public partial class Index : ComponentBase
    {
        private async Task LoadRecords()
        {
            if (this.RecordsList.Any())
                return;

            var newsList = (await RecordsService.GetAllRecordsAsync())
                    .ToList();

            foreach (var news in newsList)
                this.RecordsList.Add(news);
        }

        private static List<RecordEntity> _recordList { get; } = new List<RecordEntity>();
        public List<RecordEntity> RecordsList
        {
            get { return _recordList; }
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

            this.RecordsList.Clear();
            await LoadRecords();
        }

        [Parameter]
        public Action<MouseEventArgs, RecordEntity> OnAddRecordClick { get; set; }


        #region Constructor

        public Index()
        {
            AlertModalContext = new EditContext(AlertModel);
            RecordModelContext = new EditContext(RecordModel);
        }

        #endregion

        #region Attributes and Properties

        [Parameter]
        //public List<RecordEntity> RecordsList { get; set; }

        public Modal Modal { get; set; }
        public Modal ModalAlert { get; set; }

        public AlertModel AlertModel { get; set; } = new AlertModel();
        public RecordModel RecordModel { get; set; } = new RecordModel();

        public EditContext AlertModalContext { get; set; }
        public EditContext RecordModelContext { get; set; }




        private RecordEntity _model = null;
        [Parameter]
        public RecordEntity Model
        {
            get { return this._model; }
            set
            {
                this._model = value;

                //this._model.Id = this._model.Id;
                //this._model.Title = this._model.Title ?? string.Empty;
                //this._model.Description = this._model.Description ?? string.Empty;
            }
        }

        #endregion

        #region Private Methods

        //private async Task AddRecord(MouseEventArgs e, RecordEntity newsEntity)
        private async Task AddRecord(RecordEntity recordEntity)
        {
            try
            {
                this.RecordModelContext.ClearContext();
            }
            catch (Exception ex)
            {
                await jsRuntime.InvokeVoidAsync("console.error", ex.ToString());
            }

            this.RecordModel.Clear();
            //this.RecordModel.Image = newsEntity.Image;

            Modal.Open();
        }

        private async Task EditRecord(MouseEventArgs e, RecordEntity recordEntity)
        {
            if (e.Button != 0)
                return;

            this.RecordModel.Load(recordEntity);
            RecordModelContext.Validate();

            Modal.Open();
        }

        private async Task SaveRecord()
        {
            if (!RecordModelContext.Validate())
                return;

            this.RecordModel.Title = this.RecordModel.Title.Trim();

            try
            {
                //var ext = Path.GetExtension(this.RecordModel.Image).ToLower();
                //if (ext.IndexOf('?') > 0)
                //    ext = ext.Substring(0, ext.IndexOf('?'));

                //if ((ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".bmp" || ext == ".png") &&
                //    Path.GetFileNameWithoutExtension(this.RecordModel.Image).Length != 32)
                //{
                //    string customName;
                //    using (var md5 = MD5.Create())
                //    {
                //        var bytes = Encoding.UTF8.GetBytes(this.RecordModel.RecordId.ToString());
                //        customName = BitConverter.ToString(md5.ComputeHash(bytes));
                //        customName = customName.Replace("-", "").ToLower();
                //    }
                //}

                await RecordsService.SaveRecord(this.RecordModel);

                await Update();

                Modal.Close();
            }
            catch (Exception ex)
            {
                ShowAlert("Erro", "Não foi possível cadastrar o registro \"" + this.RecordModel.Title + "\".<br/><br/>" + ex.GetInnerExceptionMessage());
            }
        }

        private async Task Update(int id)
        {
            var updatedNews = await RecordsService.GetRecordAsync(id);

            var index = this.RecordsList.IndexOf(this.RecordsList.First(x => x.Id == id));
            this.RecordsList.RemoveAt(index);
            this.RecordsList.Insert(index, updatedNews);
        }

        private async Task Update(bool reload = false)
        {
            List<RecordEntity> updatedList;

            //if (reload)
            updatedList = (await RecordsService.GetAllRecordsAsync()).ToList();
            //else
            //{
            //    var currentNewsIds = this.RecordsList.Select(y => y.Id).ToArray();
            //    updatedList = (await RecordsService.GetAllNewsAsync2(extraClause: x => currentNewsIds.Contains(x.Id))).ToList();
            //}

            this.RecordsList.Clear();
            this.RecordsList.AddRange(updatedList);

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

        //public async void OnHandleAddRecordClick(MouseEventArgs args)
        //{
        //    //OnAddRecordClick?.Invoke(args, this.Model);
        //    var n = new RecordEntity()
        //    {
        //        MarketName = string.Empty,
        //        Title = string.Empty,
        //        Description = string.Empty
        //    };

        //    await AddRecord(args, n);
        //}

        public async Task OnHandleAddRecordClick()
        {
            //OnAddRecordClick?.Invoke(args, this.Model);
            var n = new RecordEntity()
            {
                MarketName = string.Empty,
                Title = string.Empty,
                Description = string.Empty
            };

            await AddRecord(n);
        }
    }
}
