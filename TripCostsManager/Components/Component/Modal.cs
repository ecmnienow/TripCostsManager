using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace TripCostsManager.Components.Component
{
    public enum Buttons
    {
        YesNo,
        SaveCancel,
        SaveCancelCustom,
        OK
    }

    public partial class Modal : ComponentBase
    {
        [Parameter]
        public Func<string> Title { get; set; }

        [Parameter]
        public Func<Task> CustomAction { get; set; }

        [Parameter]
        public Func<Task> Save { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Buttons Buttons { get; set; }

        [Parameter]
        public string CustomActionTitle { get; set; }

        [Parameter]
        public string ExtraClass { get; set; }

        public string ModalDisplay { get; set; } = "none;";
        public string ModalClass { get; set; } = "";
        public bool ShowBackdrop { get; set; } = false;

        private ElementReference _element;

        public async Task AcceptHandler()
        {
            await Save.Invoke();
        }

        public void Close()
        {
            ModalDisplay = "none";
            ModalClass = "";
            ShowBackdrop = false;
            StateHasChanged();
        }

        public async Task CustomActionHandler()
        {
            await CustomAction.Invoke();
        }

        public void Open(string className = null)
        {
            ModalDisplay = "block;";
            ModalClass = "Show";
            ShowBackdrop = true;
            StateHasChanged();

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(100);

                if(!string.IsNullOrWhiteSpace(className))
                    await JSRuntime.InvokeVoidAsync("focusElement.setFocusByClassName", className);
                else
                    await JSRuntime.InvokeVoidAsync("focusElement.setFocus", _element);
            });
        }

        public void OnEsc(KeyboardEventArgs e)
        {
            if (e.Code == "Escape")
                Close();
        }
    }
}
