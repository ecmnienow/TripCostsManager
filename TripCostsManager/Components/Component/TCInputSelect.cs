using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TripCostsManager.Domain.Entities.Enums;

namespace TripCostsManager.Components.Component
{
    public partial class TCInputSelect
    {
        [Parameter]
        public EPurpose? Selected { get; set; }
    }
}
