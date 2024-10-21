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
    public partial class Leadsleads
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.LeadsLead> leadsleads;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.LeadsLead> grid0;
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
                var result = await EspoDbNewService.GetLeadsleads(filter: $@"(contains(ETag,""{search}"") or contains(lead_id,""{search}"") or contains(salutation_name,""{search}"") or contains(first_name,""{search}"") or contains(last_name,""{search}"") or contains(title,""{search}"") or contains(status,""{search}"") or contains(source,""{search}"") or contains(industry,""{search}"") or contains(website,""{search}"") or contains(address_street,""{search}"") or contains(address_city,""{search}"") or contains(address_state,""{search}"") or contains(address_country,""{search}"") or contains(address_postal_code,""{search}"") or contains(description,""{search}"") or contains(account_name,""{search}"") or contains(middle_name,""{search}"") or contains(opportunity_amount_currency,""{search}"") or contains(assigned_employee_id,""{search}"") or contains(campaign_id,""{search}"") or contains(created_account_id,""{search}"") or contains(created_contact_id,""{search}"") or contains(created_opportunity_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "campaign", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                leadsleads = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Leadsleads" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddLeadsLead>("Add LeadsLead", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.LeadsLead> args)
        {
            await DialogService.OpenAsync<EditLeadsLead>("Edit LeadsLead", new Dictionary<string, object> { {"lead_id", args.Data.lead_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.LeadsLead leadsLead)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteLeadsLead(leadId:leadsLead.lead_id);

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
                    Detail = $"Unable to delete LeadsLead"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportLeadsleadsToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "campaign",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Leadsleads");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportLeadsleadsToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "campaign",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Leadsleads");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.LeadsLead leadsLeadChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.LeadsLead args)
        {
            leadsLeadChild = args;
            var Callscall_leadsResult = await EspoDbNewService.GetCallscall_leads();
            if (Callscall_leadsResult != null)
            {
                args.Callscall_leads = Callscall_leadsResult.Value.ToList();
            }
            var Cases_casesResult = await EspoDbNewService.GetCases_cases();
            if (Cases_casesResult != null)
            {
                args.Cases_cases = Cases_casesResult.Value.ToList();
            }
            var Documentsdocument_leadsResult = await EspoDbNewService.GetDocumentsdocument_leads();
            if (Documentsdocument_leadsResult != null)
            {
                args.Documentsdocument_leads = Documentsdocument_leadsResult.Value.ToList();
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

        protected async Task Callscall_leadsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.LeadsLead data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCallsCallLead>("Add Callscall_leads", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Callscall_leadsDataGrid.Reload();

        }

        protected async Task Callscall_leadsRowSelect(EspoNew.Server.Models.EspoDbNew.CallsCallLead args, EspoNew.Server.Models.EspoDbNew.LeadsLead data)
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

                    await GetChildData(leadsLeadChild);

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
        protected EspoNew.Server.Models.EspoDbNew.CasesCase casesCaseCases_cases;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdCases_cases;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.LeadsLead> leadsleadsForleadIdCases_cases;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdCases_cases;

        protected int accountsaccountsForaccountIdCases_casesCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdCases_casesValue;
        protected async Task accountsaccountsForaccountIdCases_casesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdCases_cases = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdCases_casesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int leadsleadsForleadIdCases_casesCount;
        protected EspoNew.Server.Models.EspoDbNew.LeadsLead leadsleadsForleadIdCases_casesValue;
        protected async Task leadsleadsForleadIdCases_casesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetLeadsleads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(created_opportunity_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                leadsleadsForleadIdCases_cases = result.Value.AsODataEnumerable();
                leadsleadsForleadIdCases_casesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load lead" });
            }
        }

        protected int contactscontactsForcontactIdCases_casesCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdCases_casesValue;
        protected async Task contactscontactsForcontactIdCases_casesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdCases_cases = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdCases_casesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.CasesCase> Cases_casesDataGrid;

        protected async Task Cases_casesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.LeadsLead data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCasesCase>("Add Cases_cases", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Cases_casesDataGrid.Reload();

        }

        protected async Task Cases_casesRowSelect(EspoNew.Server.Models.EspoDbNew.CasesCase args, EspoNew.Server.Models.EspoDbNew.LeadsLead data)
        {
            var dialogResult = await DialogService.OpenAsync<EditCasesCase>("Edit Cases_cases", new Dictionary<string, object> { {"case_id", args.case_id} });
            await GetChildData(data);
            await Cases_casesDataGrid.Reload();
        }

        protected async Task Cases_casesDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CasesCase casesCase)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteCasesCase(caseId:casesCase.case_id);

                    await GetChildData(leadsLeadChild);

                    if (deleteResult != null)
                    {
                        await Cases_casesDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CasesCase"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead documentsDocumentLeadDocumentsdocument_leads;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> documentsdocumentsFordocumentIdDocumentsdocument_leads;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.LeadsLead> leadsleadsForleadIdDocumentsdocument_leads;

        protected int documentsdocumentsFordocumentIdDocumentsdocument_leadsCount;
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsdocumentsFordocumentIdDocumentsdocument_leadsValue;
        protected async Task documentsdocumentsFordocumentIdDocumentsdocument_leadsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetDocumentsdocuments(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(folder_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                documentsdocumentsFordocumentIdDocumentsdocument_leads = result.Value.AsODataEnumerable();
                documentsdocumentsFordocumentIdDocumentsdocument_leadsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load document" });
            }
        }

        protected int leadsleadsForleadIdDocumentsdocument_leadsCount;
        protected EspoNew.Server.Models.EspoDbNew.LeadsLead leadsleadsForleadIdDocumentsdocument_leadsValue;
        protected async Task leadsleadsForleadIdDocumentsdocument_leadsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetLeadsleads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(created_opportunity_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                leadsleadsForleadIdDocumentsdocument_leads = result.Value.AsODataEnumerable();
                leadsleadsForleadIdDocumentsdocument_leadsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load lead" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> Documentsdocument_leadsDataGrid;

        protected async Task Documentsdocument_leadsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.LeadsLead data)
        {

            var dialogResult = await DialogService.OpenAsync<AddDocumentsDocumentLead>("Add Documentsdocument_leads", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Documentsdocument_leadsDataGrid.Reload();

        }

        protected async Task Documentsdocument_leadsRowSelect(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead args, EspoNew.Server.Models.EspoDbNew.LeadsLead data)
        {
            var dialogResult = await DialogService.OpenAsync<EditDocumentsDocumentLead>("Edit Documentsdocument_leads", new Dictionary<string, object> { {"document_lead_id", args.document_lead_id} });
            await GetChildData(data);
            await Documentsdocument_leadsDataGrid.Reload();
        }

        protected async Task Documentsdocument_leadsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead documentsDocumentLead)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteDocumentsDocumentLead(documentLeadId:documentsDocumentLead.document_lead_id);

                    await GetChildData(leadsLeadChild);

                    if (deleteResult != null)
                    {
                        await Documentsdocument_leadsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete DocumentsDocumentLead"
                });
            }
        }
    }
}