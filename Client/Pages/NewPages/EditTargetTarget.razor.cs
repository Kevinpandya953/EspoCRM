using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace EspoNew.Client.Pages.NewPages
{
    public partial class EditTargetTarget
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }
        [Inject]
        public EspoDbNewService EspoDbNewService { get; set; }

        [Parameter]
        public string target_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            targetTarget = await EspoDbNewService.GetTargetTargetByTargetId(targetId:target_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.TargetTarget targetTarget;

        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateTargetTarget(targetId:target_id, targetTarget);
                DialogService.Close(targetTarget);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}