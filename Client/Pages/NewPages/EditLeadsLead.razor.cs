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
    public partial class EditLeadsLead
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
        public string lead_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            leadsLead = await EspoDbNewService.GetLeadsLeadByLeadId(leadId:lead_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.LeadsLead leadsLead;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> campaigncampaignsForcampaignId;


        protected int campaigncampaignsForcampaignIdCount;
        protected EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaigncampaignsForcampaignIdValue;
        protected async Task campaigncampaignsForcampaignIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCampaigncampaigns(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(users_template_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                campaigncampaignsForcampaignId = result.Value.AsODataEnumerable();
                campaigncampaignsForcampaignIdCount = result.Count;

                if (!object.Equals(leadsLead.campaign_id, null))
                {
                    var valueResult = await EspoDbNewService.GetCampaigncampaigns(filter: $"campaign_id eq '{leadsLead.campaign_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        campaigncampaignsForcampaignIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load campaign" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateLeadsLead(leadId:lead_id, leadsLead);
                DialogService.Close(leadsLead);
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





        bool hascampaign_idValue;

        [Parameter]
        public string campaign_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            leadsLead = new EspoNew.Server.Models.EspoDbNew.LeadsLead();

            hascampaign_idValue = parameters.TryGetValue<string>("campaign_id", out var hascampaign_idResult);

            if (hascampaign_idValue)
            {
                leadsLead.campaign_id = hascampaign_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}