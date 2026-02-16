using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TripCostsManager.Components.Layout
{
    public partial class TCInputBase<T> : InputBase<T>
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public string ExtraClass { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public Expression<Func<T>> OnValidation { get; set; }

        [Parameter]
        public Func<T, Task> OnEnter { get; set; }

        public async Task OnEnterHandle(KeyboardEventArgs e)
        {
            if (OnEnter != null && (e.Code == "Enter" || e.Code == "NumpadEnter"))
                await OnEnter(CurrentValue);
        }

        //protected async Task OnInputChange(ChangeEventArgs args)
        //{
        //    InputValue = (T)Convert.ChangeType(args.Value, typeof(T));
        //    await ValueChanged.InvokeAsync(InputValue);
        //    //EditContext.NotifyFieldChanged(new FieldIdentifier(EditContext.Model, /*Id ??*/ Label ?? string.Empty));
        //}

        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            result = (T)Convert.ChangeType(value, typeof(T));
            validationErrorMessage = null;
            return true;
        }
    }
}
