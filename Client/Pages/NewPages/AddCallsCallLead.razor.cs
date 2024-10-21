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
    public partial class AddCallsCallLead
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

        protected override async Task OnInitializedAsync()
        {
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.CallsCallLead callsCallLead;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CallsCall> callscallsForcallId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.LeadsLead> leadsleadsForleadId;


        protected int callscallsForcallIdCount;
        protected EspoNew.Server.Models.EspoDbNew.CallsCall callscallsForcallIdValue;
        protected async Task callscallsForcallIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCallscalls(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                callscallsForcallId = result.Value.AsODataEnumerable();
                callscallsForcallIdCount = result.Count;

                if (!object.Equals(callsCallLead.call_id, null))
                {
                    var valueResult = await EspoDbNewService.GetCallscalls(filter: $"call_id eq '{callsCallLead.call_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        callscallsForcallIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load call" });
            }
        }

        protected int leadsleadsForleadIdCount;
        protected EspoNew.Server.Models.EspoDbNew.LeadsLead leadsleadsForleadIdValue;
        protected async Task leadsleadsForleadIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetLeadsleads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(created_opportunity_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                leadsleadsForleadId = result.Value.AsODataEnumerable();
                leadsleadsForleadIdCount = result.Count;

                if (!object.Equals(callsCallLead.lead_id, null))
                {
                    var valueResult = await EspoDbNewService.GetLeadsleads(filter: $"lead_id eq '{callsCallLead.lead_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        leadsleadsForleadIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load lead" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.CreateCallsCallLead(callsCallLead);
                DialogService.Close(callsCallLead);
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





        bool hascall_idValue;

        [Parameter]
        public string call_id { get; set; }

        bool haslead_idValue;

        [Parameter]
        public string lead_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            callsCallLead = new EspoNew.Server.Models.EspoDbNew.CallsCallLead();

            hascall_idValue = parameters.TryGetValue<string>("call_id", out var hascall_idResult);

            if (hascall_idValue)
            {
                callsCallLead.call_id = hascall_idResult;
            }

            haslead_idValue = parameters.TryGetValue<string>("lead_id", out var haslead_idResult);

            if (haslead_idValue)
            {
                callsCallLead.lead_id = haslead_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}