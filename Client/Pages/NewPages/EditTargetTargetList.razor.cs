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
    public partial class EditTargetTargetList
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
        public string target_list_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            targetTargetList = await EspoDbNewService.GetTargetTargetListByTargetListId(targetListId:target_list_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.TargetTargetList targetTargetList;

        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateTargetTargetList(targetListId:target_list_id, targetTargetList);
                DialogService.Close(targetTargetList);
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