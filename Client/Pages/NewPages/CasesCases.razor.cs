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
    public partial class CasesCases
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CasesCase> casesCases;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.CasesCase> grid0;
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
                var result = await EspoDbNewService.GetCases_cases(filter: $@"(contains(ETag,""{search}"") or contains(case_id,""{search}"") or contains(name,""{search}"") or contains(status,""{search}"") or contains(priority,""{search}"") or contains(type,""{search}"") or contains(description,""{search}"") or contains(account_id,""{search}"") or contains(lead_id,""{search}"") or contains(contact_id,""{search}"") or contains(inbound_email_id,""{search}"") or contains(assigned_employee_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "account,lead,contact", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                casesCases = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Cases_cases" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddCasesCase>("Add CasesCase", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.CasesCase> args)
        {
            await DialogService.OpenAsync<EditCasesCase>("Edit CasesCase", new Dictionary<string, object> { {"case_id", args.Data.case_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CasesCase casesCase)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteCasesCase(caseId:casesCase.case_id);

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
                    Detail = $"Unable to delete CasesCase"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportCases_casesToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "account,lead,contact",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Cases_cases");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportCases_casesToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "account,lead,contact",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Cases_cases");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.CasesCase casesCaseChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.CasesCase args)
        {
            casesCaseChild = args;
            var Casescase_contactsResult = await EspoDbNewService.GetCasescase_contacts();
            if (Casescase_contactsResult != null)
            {
                args.Casescase_contacts = Casescase_contactsResult.Value.ToList();
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.CasesCaseContact casesCaseContactCasescase_contacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CasesCase> casesCasesForcaseIdCasescase_contacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdCasescase_contacts;

        protected int casesCasesForcaseIdCasescase_contactsCount;
        protected EspoNew.Server.Models.EspoDbNew.CasesCase casesCasesForcaseIdCasescase_contactsValue;
        protected async Task casesCasesForcaseIdCasescase_contactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCases_cases(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                casesCasesForcaseIdCasescase_contacts = result.Value.AsODataEnumerable();
                casesCasesForcaseIdCasescase_contactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load _case" });
            }
        }

        protected int contactscontactsForcontactIdCasescase_contactsCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdCasescase_contactsValue;
        protected async Task contactscontactsForcontactIdCasescase_contactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdCasescase_contacts = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdCasescase_contactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> Casescase_contactsDataGrid;

        protected async Task Casescase_contactsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CasesCase data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCasesCaseContact>("Add Casescase_contacts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Casescase_contactsDataGrid.Reload();

        }

        protected async Task Casescase_contactsRowSelect(EspoNew.Server.Models.EspoDbNew.CasesCaseContact args, EspoNew.Server.Models.EspoDbNew.CasesCase data)
        {
            var dialogResult = await DialogService.OpenAsync<EditCasesCaseContact>("Edit Casescase_contacts", new Dictionary<string, object> { {"case_contact_id", args.case_contact_id} });
            await GetChildData(data);
            await Casescase_contactsDataGrid.Reload();
        }

        protected async Task Casescase_contactsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CasesCaseContact casesCaseContact)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteCasesCaseContact(caseContactId:casesCaseContact.case_contact_id);

                    await GetChildData(casesCaseChild);

                    if (deleteResult != null)
                    {
                        await Casescase_contactsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CasesCaseContact"
                });
            }
        }
    }
}