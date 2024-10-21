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
    public partial class Contactscontacts
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontacts;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.ContactsContact> grid0;
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
                var result = await EspoDbNewService.GetContactscontacts(filter: $@"(contains(ETag,""{search}"") or contains(contact_id,""{search}"") or contains(salutation_name,""{search}"") or contains(first_name,""{search}"") or contains(last_name,""{search}"") or contains(description,""{search}"") or contains(address_street,""{search}"") or contains(address_city_id,""{search}"") or contains(address_state_id,""{search}"") or contains(address_country_id,""{search}"") or contains(address_postal_code,""{search}"") or contains(middle_name,""{search}"") or contains(account_id,""{search}"") or contains(campaign_id,""{search}"") or contains(assigned_employee_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "address_city,address_state,address_country,account,campaign", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                contactscontacts = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Contactscontacts" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddContactsContact>("Add ContactsContact", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.ContactsContact> args)
        {
            await DialogService.OpenAsync<EditContactsContact>("Edit ContactsContact", new Dictionary<string, object> { {"contact_id", args.Data.contact_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact contactsContact)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteContactsContact(contactId:contactsContact.contact_id);

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
                    Detail = $"Unable to delete ContactsContact"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportContactscontactsToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "address_city,address_state,address_country,account,campaign",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Contactscontacts");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportContactscontactsToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "address_city,address_state,address_country,account,campaign",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Contactscontacts");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactsContactChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.ContactsContact args)
        {
            contactsContactChild = args;
            var Accountsaccount_contactsResult = await EspoDbNewService.GetAccountsaccount_contacts();
            if (Accountsaccount_contactsResult != null)
            {
                args.Accountsaccount_contacts = Accountsaccount_contactsResult.Value.ToList();
            }
            var Callscall_contactsResult = await EspoDbNewService.GetCallscall_contacts();
            if (Callscall_contactsResult != null)
            {
                args.Callscall_contacts = Callscall_contactsResult.Value.ToList();
            }
            var Cases_casesResult = await EspoDbNewService.GetCases_cases();
            if (Cases_casesResult != null)
            {
                args.Cases_cases = Cases_casesResult.Value.ToList();
            }
            var Casescase_contactsResult = await EspoDbNewService.GetCasescase_contacts();
            if (Casescase_contactsResult != null)
            {
                args.Casescase_contacts = Casescase_contactsResult.Value.ToList();
            }
            var Contactscontact_documentsResult = await EspoDbNewService.GetContactscontact_documents();
            if (Contactscontact_documentsResult != null)
            {
                args.Contactscontact_documents = Contactscontact_documentsResult.Value.ToList();
            }
            var Contactscontact_meetingsResult = await EspoDbNewService.GetContactscontact_meetings();
            if (Contactscontact_meetingsResult != null)
            {
                args.Contactscontact_meetings = Contactscontact_meetingsResult.Value.ToList();
            }
            var Contactscontact_opportunitiesResult = await EspoDbNewService.GetContactscontact_opportunities();
            if (Contactscontact_opportunitiesResult != null)
            {
                args.Contactscontact_opportunities = Contactscontact_opportunitiesResult.Value.ToList();
            }
            var EmployeesemployeesResult = await EspoDbNewService.GetEmployeesemployees();
            if (EmployeesemployeesResult != null)
            {
                args.Employeesemployees = EmployeesemployeesResult.Value.ToList();
            }
            var OpportunitiesopportunitiesResult = await EspoDbNewService.GetOpportunitiesopportunities();
            if (OpportunitiesopportunitiesResult != null)
            {
                args.Opportunitiesopportunities = OpportunitiesopportunitiesResult.Value.ToList();
            }
            var TaskstasksResult = await EspoDbNewService.GetTaskstasks();
            if (TaskstasksResult != null)
            {
                args.Taskstasks = TaskstasksResult.Value.ToList();
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccountContact accountsAccountContactAccountsaccount_contacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdAccountsaccount_contacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdAccountsaccount_contacts;

        protected int accountsaccountsForaccountIdAccountsaccount_contactsCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdAccountsaccount_contactsValue;
        protected async Task accountsaccountsForaccountIdAccountsaccount_contactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdAccountsaccount_contacts = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdAccountsaccount_contactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int contactscontactsForcontactIdAccountsaccount_contactsCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdAccountsaccount_contactsValue;
        protected async Task contactscontactsForcontactIdAccountsaccount_contactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdAccountsaccount_contacts = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdAccountsaccount_contactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> Accountsaccount_contactsDataGrid;

        protected async Task Accountsaccount_contactsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddAccountsAccountContact>("Add Accountsaccount_contacts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Accountsaccount_contactsDataGrid.Reload();

        }

        protected async Task Accountsaccount_contactsRowSelect(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {
            var dialogResult = await DialogService.OpenAsync<EditAccountsAccountContact>("Edit Accountsaccount_contacts", new Dictionary<string, object> { {"account_contact_id", args.account_contact_id} });
            await GetChildData(data);
            await Accountsaccount_contactsDataGrid.Reload();
        }

        protected async Task Accountsaccount_contactsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccountContact accountsAccountContact)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteAccountsAccountContact(accountContactId:accountsAccountContact.account_contact_id);

                    await GetChildData(contactsContactChild);

                    if (deleteResult != null)
                    {
                        await Accountsaccount_contactsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete AccountsAccountContact"
                });
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

        protected async Task Callscall_contactsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCallsCallContact>("Add Callscall_contacts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Callscall_contactsDataGrid.Reload();

        }

        protected async Task Callscall_contactsRowSelect(EspoNew.Server.Models.EspoDbNew.CallsCallContact args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
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

                    await GetChildData(contactsContactChild);

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

        protected async Task Cases_casesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCasesCase>("Add Cases_cases", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Cases_casesDataGrid.Reload();

        }

        protected async Task Cases_casesRowSelect(EspoNew.Server.Models.EspoDbNew.CasesCase args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
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

                    await GetChildData(contactsContactChild);

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

        protected async Task Casescase_contactsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCasesCaseContact>("Add Casescase_contacts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Casescase_contactsDataGrid.Reload();

        }

        protected async Task Casescase_contactsRowSelect(EspoNew.Server.Models.EspoDbNew.CasesCaseContact args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
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

                    await GetChildData(contactsContactChild);

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
        protected EspoNew.Server.Models.EspoDbNew.ContactsContactDocument contactsContactDocumentContactscontact_documents;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdContactscontact_documents;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> documentsdocumentsFordocumentIdContactscontact_documents;

        protected int contactscontactsForcontactIdContactscontact_documentsCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdContactscontact_documentsValue;
        protected async Task contactscontactsForcontactIdContactscontact_documentsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdContactscontact_documents = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdContactscontact_documentsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected int documentsdocumentsFordocumentIdContactscontact_documentsCount;
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsdocumentsFordocumentIdContactscontact_documentsValue;
        protected async Task documentsdocumentsFordocumentIdContactscontact_documentsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetDocumentsdocuments(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(folder_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                documentsdocumentsFordocumentIdContactscontact_documents = result.Value.AsODataEnumerable();
                documentsdocumentsFordocumentIdContactscontact_documentsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load document" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> Contactscontact_documentsDataGrid;

        protected async Task Contactscontact_documentsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddContactsContactDocument>("Add Contactscontact_documents", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Contactscontact_documentsDataGrid.Reload();

        }

        protected async Task Contactscontact_documentsRowSelect(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {
            var dialogResult = await DialogService.OpenAsync<EditContactsContactDocument>("Edit Contactscontact_documents", new Dictionary<string, object> { {"contact_document_id", args.contact_document_id} });
            await GetChildData(data);
            await Contactscontact_documentsDataGrid.Reload();
        }

        protected async Task Contactscontact_documentsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContactDocument contactsContactDocument)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteContactsContactDocument(contactDocumentId:contactsContactDocument.contact_document_id);

                    await GetChildData(contactsContactChild);

                    if (deleteResult != null)
                    {
                        await Contactscontact_documentsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete ContactsContactDocument"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting contactsContactMeetingContactscontact_meetings;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdContactscontact_meetings;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> meetingsmeetingsFormeetingIdContactscontact_meetings;

        protected int contactscontactsForcontactIdContactscontact_meetingsCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdContactscontact_meetingsValue;
        protected async Task contactscontactsForcontactIdContactscontact_meetingsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdContactscontact_meetings = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdContactscontact_meetingsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected int meetingsmeetingsFormeetingIdContactscontact_meetingsCount;
        protected EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsmeetingsFormeetingIdContactscontact_meetingsValue;
        protected async Task meetingsmeetingsFormeetingIdContactscontact_meetingsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetMeetingsmeetings(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                meetingsmeetingsFormeetingIdContactscontact_meetings = result.Value.AsODataEnumerable();
                meetingsmeetingsFormeetingIdContactscontact_meetingsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load meeting" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> Contactscontact_meetingsDataGrid;

        protected async Task Contactscontact_meetingsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddContactsContactMeeting>("Add Contactscontact_meetings", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Contactscontact_meetingsDataGrid.Reload();

        }

        protected async Task Contactscontact_meetingsRowSelect(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {
            var dialogResult = await DialogService.OpenAsync<EditContactsContactMeeting>("Edit Contactscontact_meetings", new Dictionary<string, object> { {"contact_meeting_id", args.contact_meeting_id} });
            await GetChildData(data);
            await Contactscontact_meetingsDataGrid.Reload();
        }

        protected async Task Contactscontact_meetingsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting contactsContactMeeting)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteContactsContactMeeting(contactMeetingId:contactsContactMeeting.contact_meeting_id);

                    await GetChildData(contactsContactChild);

                    if (deleteResult != null)
                    {
                        await Contactscontact_meetingsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete ContactsContactMeeting"
                });
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

        protected async Task Contactscontact_opportunitiesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddContactsContactOpportunity>("Add Contactscontact_opportunities", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Contactscontact_opportunitiesDataGrid.Reload();

        }

        protected async Task Contactscontact_opportunitiesRowSelect(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
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

                    await GetChildData(contactsContactChild);

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
        protected EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesEmployeeEmployeesemployees;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdEmployeesemployees;

        protected int contactscontactsForcontactIdEmployeesemployeesCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdEmployeesemployeesValue;
        protected async Task contactscontactsForcontactIdEmployeesemployeesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdEmployeesemployees = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdEmployeesemployeesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> EmployeesemployeesDataGrid;

        protected async Task EmployeesemployeesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddEmployeesEmployee>("Add Employeesemployees", new Dictionary<string, object> {  });
            await GetChildData(data);
            await EmployeesemployeesDataGrid.Reload();

        }

        protected async Task EmployeesemployeesRowSelect(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {
            var dialogResult = await DialogService.OpenAsync<EditEmployeesEmployee>("Edit Employeesemployees", new Dictionary<string, object> { {"employee_id", args.employee_id} });
            await GetChildData(data);
            await EmployeesemployeesDataGrid.Reload();
        }

        protected async Task EmployeesemployeesDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesEmployee)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteEmployeesEmployee(employeeId:employeesEmployee.employee_id);

                    await GetChildData(contactsContactChild);

                    if (deleteResult != null)
                    {
                        await EmployeesemployeesDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete EmployeesEmployee"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunityOpportunitiesopportunities;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdOpportunitiesopportunities;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdOpportunitiesopportunities;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> campaigncampaignsForcampaignIdOpportunitiesopportunities;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> employeesemployeesForassignedEmployeeIdOpportunitiesopportunities;

        protected int accountsaccountsForaccountIdOpportunitiesopportunitiesCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdOpportunitiesopportunitiesValue;
        protected async Task accountsaccountsForaccountIdOpportunitiesopportunitiesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdOpportunitiesopportunities = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdOpportunitiesopportunitiesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int contactscontactsForcontactIdOpportunitiesopportunitiesCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdOpportunitiesopportunitiesValue;
        protected async Task contactscontactsForcontactIdOpportunitiesopportunitiesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdOpportunitiesopportunities = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdOpportunitiesopportunitiesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected int campaigncampaignsForcampaignIdOpportunitiesopportunitiesCount;
        protected EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaigncampaignsForcampaignIdOpportunitiesopportunitiesValue;
        protected async Task campaigncampaignsForcampaignIdOpportunitiesopportunitiesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCampaigncampaigns(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(users_template_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                campaigncampaignsForcampaignIdOpportunitiesopportunities = result.Value.AsODataEnumerable();
                campaigncampaignsForcampaignIdOpportunitiesopportunitiesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load campaign" });
            }
        }

        protected int employeesemployeesForassignedEmployeeIdOpportunitiesopportunitiesCount;
        protected EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesemployeesForassignedEmployeeIdOpportunitiesopportunitiesValue;
        protected async Task employeesemployeesForassignedEmployeeIdOpportunitiesopportunitiesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetEmployeesemployees(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(layout_set_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                employeesemployeesForassignedEmployeeIdOpportunitiesopportunities = result.Value.AsODataEnumerable();
                employeesemployeesForassignedEmployeeIdOpportunitiesopportunitiesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load employee" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> OpportunitiesopportunitiesDataGrid;

        protected async Task OpportunitiesopportunitiesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddOpportunitiesOpportunity>("Add Opportunitiesopportunities", new Dictionary<string, object> {  });
            await GetChildData(data);
            await OpportunitiesopportunitiesDataGrid.Reload();

        }

        protected async Task OpportunitiesopportunitiesRowSelect(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {
            var dialogResult = await DialogService.OpenAsync<EditOpportunitiesOpportunity>("Edit Opportunitiesopportunities", new Dictionary<string, object> { {"opportunity_id", args.opportunity_id} });
            await GetChildData(data);
            await OpportunitiesopportunitiesDataGrid.Reload();
        }

        protected async Task OpportunitiesopportunitiesDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesOpportunity)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteOpportunitiesOpportunity(opportunityId:opportunitiesOpportunity.opportunity_id);

                    await GetChildData(contactsContactChild);

                    if (deleteResult != null)
                    {
                        await OpportunitiesopportunitiesDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete OpportunitiesOpportunity"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.TasksTask tasksTaskTaskstasks;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdTaskstasks;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdTaskstasks;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> employeesemployeesForassignedEmployeeIdTaskstasks;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmailEmail> emailemailsForemailIdTaskstasks;

        protected int accountsaccountsForaccountIdTaskstasksCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdTaskstasksValue;
        protected async Task accountsaccountsForaccountIdTaskstasksLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdTaskstasks = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdTaskstasksCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int contactscontactsForcontactIdTaskstasksCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdTaskstasksValue;
        protected async Task contactscontactsForcontactIdTaskstasksLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdTaskstasks = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdTaskstasksCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected int employeesemployeesForassignedEmployeeIdTaskstasksCount;
        protected EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesemployeesForassignedEmployeeIdTaskstasksValue;
        protected async Task employeesemployeesForassignedEmployeeIdTaskstasksLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetEmployeesemployees(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(layout_set_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                employeesemployeesForassignedEmployeeIdTaskstasks = result.Value.AsODataEnumerable();
                employeesemployeesForassignedEmployeeIdTaskstasksCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load employee" });
            }
        }

        protected int emailemailsForemailIdTaskstasksCount;
        protected EspoNew.Server.Models.EspoDbNew.EmailEmail emailemailsForemailIdTaskstasksValue;
        protected async Task emailemailsForemailIdTaskstasksLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetEmailemails(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(account_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                emailemailsForemailIdTaskstasks = result.Value.AsODataEnumerable();
                emailemailsForemailIdTaskstasksCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load email" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.TasksTask> TaskstasksDataGrid;

        protected async Task TaskstasksAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {

            var dialogResult = await DialogService.OpenAsync<AddTasksTask>("Add Taskstasks", new Dictionary<string, object> {  });
            await GetChildData(data);
            await TaskstasksDataGrid.Reload();

        }

        protected async Task TaskstasksRowSelect(EspoNew.Server.Models.EspoDbNew.TasksTask args, EspoNew.Server.Models.EspoDbNew.ContactsContact data)
        {
            var dialogResult = await DialogService.OpenAsync<EditTasksTask>("Edit Taskstasks", new Dictionary<string, object> { {"task_id", args.task_id} });
            await GetChildData(data);
            await TaskstasksDataGrid.Reload();
        }

        protected async Task TaskstasksDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.TasksTask tasksTask)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteTasksTask(taskId:tasksTask.task_id);

                    await GetChildData(contactsContactChild);

                    if (deleteResult != null)
                    {
                        await TaskstasksDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete TasksTask"
                });
            }
        }
    }
}