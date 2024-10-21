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
    public partial class Accountsaccounts
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccounts;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.AccountsAccount> grid0;
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
                var result = await EspoDbNewService.GetAccountsaccounts(filter: $@"(contains(ETag,""{search}"") or contains(account_id,""{search}"") or contains(name,""{search}"") or contains(website,""{search}"") or contains(type,""{search}"") or contains(industry,""{search}"") or contains(sic_code,""{search}"") or contains(billing_address_street,""{search}"") or contains(billing_address_city,""{search}"") or contains(billing_address_state,""{search}"") or contains(billing_address_country,""{search}"") or contains(billing_address_postal_code,""{search}"") or contains(shipping_address_street,""{search}"") or contains(shipping_address_city,""{search}"") or contains(shipping_address_state,""{search}"") or contains(shipping_address_country,""{search}"") or contains(shipping_address_postal_code,""{search}"") or contains(description,""{search}"") or contains(campaign_id,""{search}"") or contains(assigned_employee_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "campaign,employee", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                accountsaccounts = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Accountsaccounts" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddAccountsAccount>("Add AccountsAccount", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.AccountsAccount> args)
        {
            await DialogService.OpenAsync<EditAccountsAccount>("Edit AccountsAccount", new Dictionary<string, object> { {"account_id", args.Data.account_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsAccount)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteAccountsAccount(accountId:accountsAccount.account_id);

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
                    Detail = $"Unable to delete AccountsAccount"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportAccountsaccountsToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "campaign,employee",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Accountsaccounts");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportAccountsaccountsToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "campaign,employee",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Accountsaccounts");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsAccountChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.AccountsAccount args)
        {
            accountsAccountChild = args;
            var Accountsaccount_contactsResult = await EspoDbNewService.GetAccountsaccount_contacts();
            if (Accountsaccount_contactsResult != null)
            {
                args.Accountsaccount_contacts = Accountsaccount_contactsResult.Value.ToList();
            }
            var Accountsaccount_documentsResult = await EspoDbNewService.GetAccountsaccount_documents();
            if (Accountsaccount_documentsResult != null)
            {
                args.Accountsaccount_documents = Accountsaccount_documentsResult.Value.ToList();
            }
            var CallscallsResult = await EspoDbNewService.GetCallscalls();
            if (CallscallsResult != null)
            {
                args.Callscalls = CallscallsResult.Value.ToList();
            }
            var Cases_casesResult = await EspoDbNewService.GetCases_cases();
            if (Cases_casesResult != null)
            {
                args.Cases_cases = Cases_casesResult.Value.ToList();
            }
            var ContactscontactsResult = await EspoDbNewService.GetContactscontacts();
            if (ContactscontactsResult != null)
            {
                args.Contactscontacts = ContactscontactsResult.Value.ToList();
            }
            var EmailemailsResult = await EspoDbNewService.GetEmailemails();
            if (EmailemailsResult != null)
            {
                args.Emailemails = EmailemailsResult.Value.ToList();
            }
            var MeetingsmeetingsResult = await EspoDbNewService.GetMeetingsmeetings();
            if (MeetingsmeetingsResult != null)
            {
                args.Meetingsmeetings = MeetingsmeetingsResult.Value.ToList();
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

        protected async Task Accountsaccount_contactsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {

            var dialogResult = await DialogService.OpenAsync<AddAccountsAccountContact>("Add Accountsaccount_contacts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Accountsaccount_contactsDataGrid.Reload();

        }

        protected async Task Accountsaccount_contactsRowSelect(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
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

                    await GetChildData(accountsAccountChild);

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
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument accountsAccountDocumentAccountsaccount_documents;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdAccountsaccount_documents;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> documentsdocumentsFordocumentIdAccountsaccount_documents;

        protected int accountsaccountsForaccountIdAccountsaccount_documentsCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdAccountsaccount_documentsValue;
        protected async Task accountsaccountsForaccountIdAccountsaccount_documentsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdAccountsaccount_documents = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdAccountsaccount_documentsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int documentsdocumentsFordocumentIdAccountsaccount_documentsCount;
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsdocumentsFordocumentIdAccountsaccount_documentsValue;
        protected async Task documentsdocumentsFordocumentIdAccountsaccount_documentsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetDocumentsdocuments(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(folder_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                documentsdocumentsFordocumentIdAccountsaccount_documents = result.Value.AsODataEnumerable();
                documentsdocumentsFordocumentIdAccountsaccount_documentsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load document" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> Accountsaccount_documentsDataGrid;

        protected async Task Accountsaccount_documentsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {

            var dialogResult = await DialogService.OpenAsync<AddAccountsAccountDocument>("Add Accountsaccount_documents", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Accountsaccount_documentsDataGrid.Reload();

        }

        protected async Task Accountsaccount_documentsRowSelect(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {
            var dialogResult = await DialogService.OpenAsync<EditAccountsAccountDocument>("Edit Accountsaccount_documents", new Dictionary<string, object> { {"account_document_id", args.account_document_id} });
            await GetChildData(data);
            await Accountsaccount_documentsDataGrid.Reload();
        }

        protected async Task Accountsaccount_documentsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument accountsAccountDocument)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteAccountsAccountDocument(accountDocumentId:accountsAccountDocument.account_document_id);

                    await GetChildData(accountsAccountChild);

                    if (deleteResult != null)
                    {
                        await Accountsaccount_documentsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete AccountsAccountDocument"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.CallsCall callsCallCallscalls;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdCallscalls;

        protected int accountsaccountsForaccountIdCallscallsCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdCallscallsValue;
        protected async Task accountsaccountsForaccountIdCallscallsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdCallscalls = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdCallscallsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.CallsCall> CallscallsDataGrid;

        protected async Task CallscallsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCallsCall>("Add Callscalls", new Dictionary<string, object> {  });
            await GetChildData(data);
            await CallscallsDataGrid.Reload();

        }

        protected async Task CallscallsRowSelect(EspoNew.Server.Models.EspoDbNew.CallsCall args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {
            var dialogResult = await DialogService.OpenAsync<EditCallsCall>("Edit Callscalls", new Dictionary<string, object> { {"call_id", args.call_id} });
            await GetChildData(data);
            await CallscallsDataGrid.Reload();
        }

        protected async Task CallscallsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.CallsCall callsCall)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteCallsCall(callId:callsCall.call_id);

                    await GetChildData(accountsAccountChild);

                    if (deleteResult != null)
                    {
                        await CallscallsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CallsCall"
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

        protected async Task Cases_casesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCasesCase>("Add Cases_cases", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Cases_casesDataGrid.Reload();

        }

        protected async Task Cases_casesRowSelect(EspoNew.Server.Models.EspoDbNew.CasesCase args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
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

                    await GetChildData(accountsAccountChild);

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
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactsContactContactscontacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> addressaddressCitiesForaddressCityIdContactscontacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressState> addressaddressStatesForaddressStateIdContactscontacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> addressaddressCountriesForaddressCountryIdContactscontacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdContactscontacts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> campaigncampaignsForcampaignIdContactscontacts;

        protected int addressaddressCitiesForaddressCityIdContactscontactsCount;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressCity addressaddressCitiesForaddressCityIdContactscontactsValue;
        protected async Task addressaddressCitiesForaddressCityIdContactscontactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAddressaddress_cities(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(state_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                addressaddressCitiesForaddressCityIdContactscontacts = result.Value.AsODataEnumerable();
                addressaddressCitiesForaddressCityIdContactscontactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load address_city" });
            }
        }

        protected int addressaddressStatesForaddressStateIdContactscontactsCount;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressState addressaddressStatesForaddressStateIdContactscontactsValue;
        protected async Task addressaddressStatesForaddressStateIdContactscontactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAddressaddress_states(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(country_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                addressaddressStatesForaddressStateIdContactscontacts = result.Value.AsODataEnumerable();
                addressaddressStatesForaddressStateIdContactscontactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load address_state" });
            }
        }

        protected int addressaddressCountriesForaddressCountryIdContactscontactsCount;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressaddressCountriesForaddressCountryIdContactscontactsValue;
        protected async Task addressaddressCountriesForaddressCountryIdContactscontactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAddressaddress_countries(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(country_name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                addressaddressCountriesForaddressCountryIdContactscontacts = result.Value.AsODataEnumerable();
                addressaddressCountriesForaddressCountryIdContactscontactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load address_country" });
            }
        }

        protected int accountsaccountsForaccountIdContactscontactsCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdContactscontactsValue;
        protected async Task accountsaccountsForaccountIdContactscontactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdContactscontacts = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdContactscontactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int campaigncampaignsForcampaignIdContactscontactsCount;
        protected EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaigncampaignsForcampaignIdContactscontactsValue;
        protected async Task campaigncampaignsForcampaignIdContactscontactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCampaigncampaigns(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(users_template_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                campaigncampaignsForcampaignIdContactscontacts = result.Value.AsODataEnumerable();
                campaigncampaignsForcampaignIdContactscontactsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load campaign" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.ContactsContact> ContactscontactsDataGrid;

        protected async Task ContactscontactsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {

            var dialogResult = await DialogService.OpenAsync<AddContactsContact>("Add Contactscontacts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await ContactscontactsDataGrid.Reload();

        }

        protected async Task ContactscontactsRowSelect(EspoNew.Server.Models.EspoDbNew.ContactsContact args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {
            var dialogResult = await DialogService.OpenAsync<EditContactsContact>("Edit Contactscontacts", new Dictionary<string, object> { {"contact_id", args.contact_id} });
            await GetChildData(data);
            await ContactscontactsDataGrid.Reload();
        }

        protected async Task ContactscontactsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContact contactsContact)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteContactsContact(contactId:contactsContact.contact_id);

                    await GetChildData(accountsAccountChild);

                    if (deleteResult != null)
                    {
                        await ContactscontactsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete ContactsContact"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.EmailEmail emailEmailEmailemails;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdEmailemails;

        protected int accountsaccountsForaccountIdEmailemailsCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdEmailemailsValue;
        protected async Task accountsaccountsForaccountIdEmailemailsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdEmailemails = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdEmailemailsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.EmailEmail> EmailemailsDataGrid;

        protected async Task EmailemailsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {

            var dialogResult = await DialogService.OpenAsync<AddEmailEmail>("Add Emailemails", new Dictionary<string, object> {  });
            await GetChildData(data);
            await EmailemailsDataGrid.Reload();

        }

        protected async Task EmailemailsRowSelect(EspoNew.Server.Models.EspoDbNew.EmailEmail args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {
            var dialogResult = await DialogService.OpenAsync<EditEmailEmail>("Edit Emailemails", new Dictionary<string, object> { {"email_id", args.email_id} });
            await GetChildData(data);
            await EmailemailsDataGrid.Reload();
        }

        protected async Task EmailemailsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.EmailEmail emailEmail)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteEmailEmail(emailId:emailEmail.email_id);

                    await GetChildData(accountsAccountChild);

                    if (deleteResult != null)
                    {
                        await EmailemailsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete EmailEmail"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsMeetingMeetingsmeetings;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountIdMeetingsmeetings;

        protected int accountsaccountsForaccountIdMeetingsmeetingsCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdMeetingsmeetingsValue;
        protected async Task accountsaccountsForaccountIdMeetingsmeetingsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountIdMeetingsmeetings = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdMeetingsmeetingsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> MeetingsmeetingsDataGrid;

        protected async Task MeetingsmeetingsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {

            var dialogResult = await DialogService.OpenAsync<AddMeetingsMeeting>("Add Meetingsmeetings", new Dictionary<string, object> {  });
            await GetChildData(data);
            await MeetingsmeetingsDataGrid.Reload();

        }

        protected async Task MeetingsmeetingsRowSelect(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {
            var dialogResult = await DialogService.OpenAsync<EditMeetingsMeeting>("Edit Meetingsmeetings", new Dictionary<string, object> { {"meeting_id", args.meeting_id} });
            await GetChildData(data);
            await MeetingsmeetingsDataGrid.Reload();
        }

        protected async Task MeetingsmeetingsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsMeeting)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteMeetingsMeeting(meetingId:meetingsMeeting.meeting_id);

                    await GetChildData(accountsAccountChild);

                    if (deleteResult != null)
                    {
                        await MeetingsmeetingsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete MeetingsMeeting"
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

        protected async Task OpportunitiesopportunitiesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {

            var dialogResult = await DialogService.OpenAsync<AddOpportunitiesOpportunity>("Add Opportunitiesopportunities", new Dictionary<string, object> {  });
            await GetChildData(data);
            await OpportunitiesopportunitiesDataGrid.Reload();

        }

        protected async Task OpportunitiesopportunitiesRowSelect(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
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

                    await GetChildData(accountsAccountChild);

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

        protected async Task TaskstasksAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
        {

            var dialogResult = await DialogService.OpenAsync<AddTasksTask>("Add Taskstasks", new Dictionary<string, object> {  });
            await GetChildData(data);
            await TaskstasksDataGrid.Reload();

        }

        protected async Task TaskstasksRowSelect(EspoNew.Server.Models.EspoDbNew.TasksTask args, EspoNew.Server.Models.EspoDbNew.AccountsAccount data)
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

                    await GetChildData(accountsAccountChild);

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