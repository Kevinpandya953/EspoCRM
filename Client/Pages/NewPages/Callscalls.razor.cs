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
    public partial class Callscalls
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CallsCall> callscalls;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.CallsCall> grid0;
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
                var result = await EspoDbNewService.GetCallscalls(filter: $@"(contains(ETag,""{search}"") or contains(call_id,""{search}"") or contains(name,""{search}"") or contains(status,""{search}"") or contains(direction,""{search}"") or contains(description,""{search}"") or contains(parent_id,""{search}"") or contains(parent_type,""{search}"") or contains(account_id,""{search}"") or contains(assigned_employee_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "account", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                callscalls = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Callscalls" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddCallsCall>("Add CallsCall", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.CallsCall> args)
        {
            await DialogService.OpenAsync<EditCallsCall>("Edit CallsCall", new Dictionary<string, object> { {"call_id", args.Data.call_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CallsCall callsCall)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteCallsCall(callId:callsCall.call_id);

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
                    Detail = $"Unable to delete CallsCall"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportCallscallsToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "account",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Callscalls");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportCallscallsToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "account",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Callscalls");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.CallsCall callsCallChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.CallsCall args)
        {
            callsCallChild = args;
            var Callscall_contactsResult = await EspoDbNewService.GetCallscall_contacts();
            if (Callscall_contactsResult != null)
            {
                args.Callscall_contacts = Callscall_contactsResult.Value.ToList();
            }
            var Callscall_leadsResult = await EspoDbNewService.GetCallscall_leads();
            if (Callscall_leadsResult != null)
            {
                args.Callscall_leads = Callscall_leadsResult.Value.ToList();
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.CallsCallContact callsCallContactCallscall_contacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CallsCall> callscallsForcallIdCallscall_contacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdCallscall_contacts;

        protected int callscallsForcallIdCallscall_contactsCount;
        protected EspoNew.Server.Models.EspoDbNew.CallsCall callscallsForcallIdCallscall_contactsValue;
        protected async Task callscallsForcallIdCallscall_contactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCallscalls(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                callscallsForcallIdCallscall_contacts = result.Value.AsODataEnumerable();
                callscallsForcallIdCallscall_contactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load call" });
            }
        }

        protected int contactscontactsForcontactIdCallscall_contactsCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdCallscall_contactsValue;
        protected async Task contactscontactsForcontactIdCallscall_contactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdCallscall_contacts = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdCallscall_contactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.CallsCallContact> Callscall_contactsDataGrid;

        protected async Task Callscall_contactsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CallsCall data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCallsCallContact>("Add Callscall_contacts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Callscall_contactsDataGrid.Reload();

        }

        protected async Task Callscall_contactsRowSelect(EspoNew.Server.Models.EspoDbNew.CallsCallContact args, EspoNew.Server.Models.EspoDbNew.CallsCall data)
        {
            var dialogResult = await DialogService.OpenAsync<EditCallsCallContact>("Edit Callscall_contacts", new Dictionary<string, object> { {"call_contact_id", args.call_contact_id} });
            await GetChildData(data);
            await Callscall_contactsDataGrid.Reload();
        }

        protected async Task Callscall_contactsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CallsCallContact callsCallContact)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteCallsCallContact(callContactId:callsCallContact.call_contact_id);

                    await GetChildData(callsCallChild);

                    if (deleteResult != null)
                    {
                        await Callscall_contactsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CallsCallContact"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.CallsCallLead callsCallLeadCallscall_leads;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CallsCall> callscallsForcallIdCallscall_leads;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.LeadsLead> leadsleadsForleadIdCallscall_leads;

        protected int callscallsForcallIdCallscall_leadsCount;
        protected EspoNew.Server.Models.EspoDbNew.CallsCall callscallsForcallIdCallscall_leadsValue;
        protected async Task callscallsForcallIdCallscall_leadsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCallscalls(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                callscallsForcallIdCallscall_leads = result.Value.AsODataEnumerable();
                callscallsForcallIdCallscall_leadsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load call" });
            }
        }

        protected int leadsleadsForleadIdCallscall_leadsCount;
        protected EspoNew.Server.Models.EspoDbNew.LeadsLead leadsleadsForleadIdCallscall_leadsValue;
        protected async Task leadsleadsForleadIdCallscall_leadsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetLeadsleads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(created_opportunity_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                leadsleadsForleadIdCallscall_leads = result.Value.AsODataEnumerable();
                leadsleadsForleadIdCallscall_leadsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load lead" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.CallsCallLead> Callscall_leadsDataGrid;

        protected async Task Callscall_leadsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CallsCall data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCallsCallLead>("Add Callscall_leads", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Callscall_leadsDataGrid.Reload();

        }

        protected async Task Callscall_leadsRowSelect(EspoNew.Server.Models.EspoDbNew.CallsCallLead args, EspoNew.Server.Models.EspoDbNew.CallsCall data)
        {
            var dialogResult = await DialogService.OpenAsync<EditCallsCallLead>("Edit Callscall_leads", new Dictionary<string, object> { {"call_lead_id", args.call_lead_id} });
            await GetChildData(data);
            await Callscall_leadsDataGrid.Reload();
        }

        protected async Task Callscall_leadsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CallsCallLead callsCallLead)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteCallsCallLead(callLeadId:callsCallLead.call_lead_id);

                    await GetChildData(callsCallChild);

                    if (deleteResult != null)
                    {
                        await Callscall_leadsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CallsCallLead"
                });
            }
        }
    }
}