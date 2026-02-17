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
using TripCostsManager.Domain.Entities.Entities;
using TripCostsManager.Models;

namespace TripCostsManager.Components.Component
{
    public partial class RecordsTable : ComponentBase
    {
        //private static List<RecordEntity> _recordList { get; } = new List<RecordEntity>();
        private static List<RecordEntity> _dataSource;

        [Parameter]
        public List<RecordEntity> DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        [Parameter]
        public Action<MouseEventArgs, RecordEntity> OnClickEdit { get; set; }

        [Parameter]
        public Action<MouseEventArgs, RecordEntity> OnClickDelete { get; set; }
    }
}
