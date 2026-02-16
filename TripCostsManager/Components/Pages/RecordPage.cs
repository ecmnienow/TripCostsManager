//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Forms;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.JSInterop;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using TripCostsManager.Components.Component;
//using TripCostsManager.Domain.Entities.Entities;
//using TripCostsManager.Models;
//using TripCostsManager.Services;

//namespace TripCostsManager.Components.Pages
//{
//    public partial class RecordPage : ComponentBase
//    {
//        #region Constructor

//        public RecordPage()
//        {
//            AlertModalContext = new EditContext(AlertModel);
//            RecordModelContext = new EditContext(RecordModel);
//        }

//        #endregion

//        #region Attributes and Properties

//        [Parameter]
//        public List<RecordEntity> RecordsList { get; set; }

//        public Modal Modal { get; set; }
//        public Modal ModalAlert { get; set; }

//        public AlertModel AlertModel { get; set; } = new AlertModel();
//        public RecordModel RecordModel { get; set; } = new RecordModel();

//        public EditContext AlertModalContext { get; set; }
//        public EditContext RecordModelContext { get; set; }

//        #endregion

//        #region Private Methods

//        private async Task AddGame(MouseEventArgs e, RecordEntity recordEntity)
//        {
//            try
//            {
//                this.RecordModelContext.ClearContext();
//            }
//            catch (Exception ex)
//            {
//                await jsRuntime.InvokeVoidAsync("console.error", ex.ToString());
//            }

//            this.RecordModel.Clear();
//            this.RecordModel.Images = recordEntity.Images;

//            Modal.Open();
//        }

//        private async Task EditGame(MouseEventArgs e, RecordEntity gameEntity)
//        {
//            if (e.Button != 0)
//                return;

//            this.RecordModel.Load(gameEntity);
//            RecordModelContext.Validate();

//            Modal.Open();
//        }

//        private async Task SaveGame()
//        {
//            this.RecordModel.Title = this.RecordModel.Title.Trim();

//            if (!RecordModelContext.Validate())
//                return;

//            try
//            {
//                //var ext = Path.GetExtension(this.RecordModel.Image).ToLower();
//                //if (ext.IndexOf('?') > 0)
//                //    ext = ext.Substring(0, ext.IndexOf('?'));

//                //if ((ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".bmp" || ext == ".png") &&
//                //    Path.GetFileNameWithoutExtension(this.RecordModel.Image).Length != 32)
//                //{
//                //    string customName;
//                //    using (var md5 = MD5.Create())
//                //    {
//                //        var bytes = Encoding.UTF8.GetBytes(this.RecordModel.RecordId.ToString());
//                //        customName = BitConverter.ToString(md5.ComputeHash(bytes));
//                //        customName = customName.Replace("-", "").ToLower();
//                //    }
//                //}

//                await RecordsService.SaveRecord(this.RecordModel);

//                await Update();

//                Modal.Close();
//            }
//            catch (Exception ex)
//            {
//                ShowAlert("Erro", "Não foi possível cadastrar o jogo \"" + this.RecordModel.Title + "\".<br/><br/>" + ex.GetInnerExceptionMessage());
//            }
//            ;
//        }

//        private async Task Update(int id)
//        {
//            var updatedNews = await RecordsService.GetRecordAsync(id);

//            var index = this.RecordsList.IndexOf(this.RecordsList.First(x => x.Id == id));
//            this.RecordsList.RemoveAt(index);
//            this.RecordsList.Insert(index, updatedNews);
//        }

//        private async Task Update(bool reload = false)
//        {
//            List<RecordEntity> updatedList;

//            //if (reload)
//                updatedList = (await RecordsService.GetAllRecordsAsync()).ToList();
//            //else
//            //{
//            //    var currentNewsIds = this.RecordsList.Select(y => y.Id).ToArray();
//            //    updatedList = (await RecordsService.GetAllNewsAsync2(extraClause: x => currentNewsIds.Contains(x.Id))).ToList();
//            //}

//            this.RecordsList.Clear();
//            this.RecordsList.AddRange(updatedList);
//        }

//        private void ShowAlert(string title, string message)
//        {
//            this.AlertModel = new AlertModel();
//            this.AlertModel.Title = title;
//            this.AlertModel.Message = message;

//            ModalAlert.Open();
//        }

//        #endregion
//    }
//}
