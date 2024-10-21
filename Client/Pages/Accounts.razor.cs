using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using EspoNew.Client.Pages.NewPages;
namespace EspoNew.Client.Pages
{
    public partial class Accounts
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
        protected EspoNew.Client.EspoDbNewService EspoDbNewService { get; set; }

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsAccounts;

        protected int accountsAccountsCount;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CallsCall> callsCalls;

        protected int callsCallsCount;

        public List<EspoNew.Server.Models.EspoDbNew.AccountsAccount> AccountsList { get; set; }
        public List<EspoNew.Server.Models.EspoDbNew.CallsCall> CallsList { get; set; }
        public List<EspoNew.Server.Models.EspoDbNew.ContactsContact> ContactsList {get; set;}
        public List<EspoNew.Server.Models.EspoDbNew.CasesCase> CasesList {get; set;}
        public List<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> DocumentsList {get; set;}
        public List<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> OpportunitiesList {get; set;}
        public List<EspoNew.Server.Models.EspoDbNew.TasksTask> TasksList {get; set;}
        public List<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> EmployeesList {get; set;}

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactsContacts;

        protected int contactsContactsCount;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> employeesEmployees;

        protected int employeesEmployeesCount;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CasesCase> casesCases;

        protected int casesCasesCount;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> opportunitiesOpportunities;

        protected int opportunitiesOpportunitiesCount;

        [Parameter]
        public int account_Id{get;set;}

        

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> accountsAccountDocuments;

        protected int accountsAccountDocumentsCount;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.TasksTask> tasksTasks;

        protected int tasksTasksCount;


        protected override async Task OnInitializedAsync()
        {
            /*var accountsResult = await EspoDbNewService.GetAccountsaccounts();
            accountsAccounts = accountsResult.Value.ToList();
            callsCalls = new List<EspoNew.Server.Models.EspoDbNew.CallsCall>();
            contactsContacts = new List<EspoNew.Server.Models.EspoDbNew.ContactsContact>();
            employeesEmployees = new List<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>();
            casesCases = new List<EspoNew.Server.Models.EspoDbNew.CasesCase>();
            opportunitiesOpportunities = new List<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>();
            StateHasChanged();*/

            await LoadChildTableData(account_Id);


        }

        private async Task LoadChildTableData(int account_Id){

            var callsResult = await EspoDbNewService.GetCallscalls(new Radzen.Query { Filter = $"account_id eq '{account_Id}'" });
            var contactsResult = await EspoDbNewService.GetContactscontacts(new Radzen.Query{Filter = $"account_id eq '{account_Id}'"});
            var casesResult = await EspoDbNewService.GetCases_cases(new Radzen.Query{Filter = $"account_id eq '{account_Id} '"});
            var opportunitiesResult= await EspoDbNewService.GetOpportunitiesopportunities(new Radzen.Query { Filter = $"account_id eq '{account_Id}'" });
            var documentsResult = await EspoDbNewService.GetAccountsaccount_documents(new Radzen.Query {Filter = $"account_id eq '{account_Id}'"});
            var tasksResult = await EspoDbNewService.GetTaskstasks(new Radzen.Query { Filter= $"account_id eq '{account_Id}'"});

            
            callsCalls = callsResult.Value.ToList();
            contactsContacts = contactsResult.Value.ToList();
            casesCases = casesResult.Value.ToList();
            opportunitiesOpportunities = opportunitiesResult.Value.ToList();
            accountsAccountDocuments = documentsResult.Value.ToList();
            tasksTasks = tasksResult.Value.ToList();


        }

        /*protected async Task accountsAccountsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(new Query { Top = args.Top, Skip = args.Skip, Filter = args.Filter, OrderBy = args.OrderBy });

                accountsAccounts = result.Value.AsODataEnumerable();
                accountsAccountsCount = result.Count;
            }
            catch (Exception)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
            }
        }*/


        protected async Task callsCallsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCallscalls(new Query { Top = args.Top, Skip = args.Skip, Filter = args.Filter, OrderBy = args.OrderBy });

                callsCalls = result.Value.AsODataEnumerable();
                callsCallsCount = result.Count;
            }
            catch (Exception)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
            }
        }


        protected async Task contactsContactsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(new Query { Top = args.Top, Skip = args.Skip, Filter = args.Filter, OrderBy = args.OrderBy });

                contactsContacts = result.Value.AsODataEnumerable();
                contactsContactsCount = result.Count;
            }
            catch (Exception)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
            }
        }

        protected async Task employeesEmployeesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetEmployeesemployees(new Query { Top = args.Top, Skip = args.Skip, Filter = args.Filter, OrderBy = args.OrderBy });

                employeesEmployees = result.Value.AsODataEnumerable();
                employeesEmployeesCount = result.Count;
            }
            catch (Exception)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
            }
        }

        protected async Task casesCasesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCases_cases(new Query { Top = args.Top, Skip = args.Skip, Filter = args.Filter, OrderBy = args.OrderBy });

                casesCases = result.Value.AsODataEnumerable();
                casesCasesCount = result.Count;
            }
            catch (Exception)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
            }
        }
        protected async Task opportunitiesOpportunitiesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetOpportunitiesopportunities(new Query { Top = args.Top, Skip = args.Skip, Filter = args.Filter, OrderBy = args.OrderBy });

                opportunitiesOpportunities = result.Value.AsODataEnumerable();
                opportunitiesOpportunitiesCount = result.Count;
            }
            catch (Exception)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
            }
        }



        /*protected async System.Threading.Tasks.Task DataGrid0RowSelect(EspoNew.Server.Models.EspoDbNew.AccountsAccount args)
        {
            var callsResult = await EspoDbNewService.GetCallscalls(new Radzen.Query { Filter = $"account_id eq '{args.account_id}'" });
            var contactsResult = await EspoDbNewService.GetContactscontacts(new Radzen.Query{Filter = $"account_id eq '{args.account_id}'"});
            var casesResult = await EspoDbNewService.GetCases_cases(new Radzen.Query{Filter = $"account_id eq '{args.account_id} '"});
            var opportunitiesResult= await EspoDbNewService.GetOpportunitiesopportunities(new Radzen.Query { Filter = $"account_id eq '{args.account_id}'" });

            
            callsCalls = callsResult.Value.ToList();
            contactsContacts = contactsResult.Value.ToList();
            casesCases = casesResult.Value.ToList();
            opportunitiesOpportunities = opportunitiesResult.Value.ToList();


        }*/


        protected async Task accountsAccountDocumentsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccount_documents(new Query { Top = args.Top, Skip = args.Skip, Filter = args.Filter, OrderBy = args.OrderBy });

                accountsAccountDocuments = result.Value.AsODataEnumerable();
                accountsAccountDocumentsCount = result.Count;
            }
            catch (Exception)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
            }
        }


        protected async Task tasksTasksLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetTaskstasks(new Query { Top = args.Top, Skip = args.Skip, Filter = args.Filter, OrderBy = args.OrderBy });

                tasksTasks = result.Value.AsODataEnumerable();
                tasksTasksCount = result.Count;
            }
            catch (Exception)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load" });
            }
        }


        protected async System.Threading.Tasks.Task DataGrid3RowSelect(EspoNew.Server.Models.EspoDbNew.ContactsContact args)
        {
            var employeesResult = await EspoDbNewService.GetEmployeesemployees(new Radzen.Query { Filter = $"contact_id eq '{args.contact_id}'" });
            employeesEmployees = employeesResult.Value.ToList();

        }



        protected async System.Threading.Tasks.Task CallsButton0Click(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddCallsCall>("Add Calls ");

        }
    }
}