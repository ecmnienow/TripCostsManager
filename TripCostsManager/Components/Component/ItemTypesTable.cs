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
    public partial class ItemTypesTable : ComponentBase
    {
        //private static List<RecordEntity> _recordList { get; } = new List<RecordEntity>();
        private static List<ItemTypeEntity> _dataSource;

        [Parameter]
        public List<ItemTypeEntity> DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        [Parameter]
        public Action<MouseEventArgs, ItemTypeEntity> OnClickEdit { get; set; }
    }
}
