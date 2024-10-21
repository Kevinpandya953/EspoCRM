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
    public partial class Opportunitiesopportunities
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> opportunitiesopportunities;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> grid0;
        protected int count;

        protected string search = "";

        protected async Task Search(ChangeEventArgs args)
        {
            search = $"{args.Value}";

            await grid0.GoToPage(0);

            await grid0.Reload();
        }

        protected async Task Grid0LoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetOpportunitiesopportunities(filter: $@"(contains(ETag,""{search}"") or contains(opportunity_id,""{search}"") or contains(name,""{search}"") or contains(stage,""{search}"") or contains(last_stage,""{search}"") or contains(lead_source,""{search}"") or contains(description,""{search}"") or contains(amount_currency,""{search}"") or contains(account_id,""{search}"") or contains(contact_id,""{search}"") or contains(campaign_id,""{search}"") or contains(assigned_employee_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "account,contact,campaign,employee", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                opportunitiesopportunities = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Opportunitiesopportunities" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddOpportunitiesOpportunity>("Add OpportunitiesOpportunity", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> args)
        {
            await DialogService.OpenAsync<EditOpportunitiesOpportunity>("Edit OpportunitiesOpportunity", new Dictionary<string, object> { {"opportunity_id", args.Data.opportunity_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunity)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteOpportunitiesOpportunity(opportunityId:opportunitiesOpportunity.opportunity_id);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete OpportunitiesOpportunity"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportOpportunitiesopportunitiesToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "account,contact,campaign,employee",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Opportunitiesopportunities");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportOpportunitiesopportunitiesToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "account,contact,campaign,employee",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Opportunitiesopportunities");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunityChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity args)
        {
            opportunitiesOpportunityChild = args;
            var Contactscontact_opportunitiesResult = await EspoDbNewService.GetContactscontact_opportunities();
            if (Contactscontact_opportunitiesResult != null)
            {
                args.Contactscontact_opportunities = Contactscontact_opportunitiesResult.Value.ToList();
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity contactsContactOpportunityContactscontact_opportunities;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdContactscontact_opportunities;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> opportunitiesopportunitiesForopportunityIdContactscontact_opportunities;

        protected int contactscontactsForcontactIdContactscontact_opportunitiesCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdContactscontact_opportunitiesValue;
        protected async Task contactscontactsForcontactIdContactscontact_opportunitiesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdContactscontact_opportunities = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdContactscontact_opportunitiesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected int opportunitiesopportunitiesForopportunityIdContactscontact_opportunitiesCount;
        protected EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesopportunitiesForopportunityIdContactscontact_opportunitiesValue;
        protected async Task opportunitiesopportunitiesForopportunityIdContactscontact_opportunitiesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetOpportunitiesopportunities(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                opportunitiesopportunitiesForopportunityIdContactscontact_opportunities = result.Value.AsODataEnumerable();
                opportunitiesopportunitiesForopportunityIdContactscontact_opportunitiesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load opportunity" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> Contactscontact_opportunitiesDataGrid;

        protected async Task Contactscontact_opportunitiesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity data)
        {

            var dialogResult = await DialogService.OpenAsync<AddContactsContactOpportunity>("Add Contactscontact_opportunities", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Contactscontact_opportunitiesDataGrid.Reload();

        }

        protected async Task Contactscontact_opportunitiesRowSelect(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity args, EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity data)
        {
            var dialogResult = await DialogService.OpenAsync<EditContactsContactOpportunity>("Edit Contactscontact_opportunities", new Dictionary<string, object> { {"contact_opportunity_id", args.contact_opportunity_id} });
            await GetChildData(data);
            await Contactscontact_opportunitiesDataGrid.Reload();
        }

        protected async Task Contactscontact_opportunitiesDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity contactsContactOpportunity)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteContactsContactOpportunity(contactOpportunityId:contactsContactOpportunity.contact_opportunity_id);

                    await GetChildData(opportunitiesOpportunityChild);

                    if (deleteResult != null)
                    {
                        await Contactscontact_opportunitiesDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete ContactsContactOpportunity"
                });
            }
        }
    }
}