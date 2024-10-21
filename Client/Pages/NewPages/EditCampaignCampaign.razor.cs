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
    public partial class EditCampaignCampaign
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
        public string campaign_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            campaignCampaign = await EspoDbNewService.GetCampaignCampaignByCampaignId(campaignId:campaign_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaignCampaign;

        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateCampaignCampaign(campaignId:campaign_id, campaignCampaign);
                DialogService.Close(campaignCampaign);
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