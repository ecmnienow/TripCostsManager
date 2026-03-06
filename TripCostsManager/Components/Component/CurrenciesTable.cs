using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using TripCostsManager.Domain.Entities.Entities;

namespace TripCostsManager.Components.Component
{
    public partial class CurrenciesTable : ComponentBase
    {
        //private static List<RecordEntity> _recordList { get; } = new List<RecordEntity>();
        private static List<CurrencyEntity> _dataSource;

        [Parameter]
        public List<CurrencyEntity> DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }

        [Parameter]
        public Action<MouseEventArgs, CurrencyEntity> OnClickEdit { get; set; }
    }
}
