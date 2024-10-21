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
    public partial class Employeesemployees
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> employeesemployees;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> grid0;
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
                var result = await EspoDbNewService.GetEmployeesemployees(filter: $@"(contains(ETag,""{search}"") or contains(employee_id,""{search}"") or contains(employee_name,""{search}"") or contains(type,""{search}"") or contains(password,""{search}"") or contains(auth_method,""{search}"") or contains(api_key,""{search}"") or contains(salutation_name,""{search}"") or contains(first_name,""{search}"") or contains(last_name,""{search}"") or contains(title,""{search}"") or contains(avatar_color,""{search}"") or contains(gender,""{search}"") or contains(delete_id,""{search}"") or contains(middle_name,""{search}"") or contains(default_team_id,""{search}"") or contains(contact_id,""{search}"") or contains(avatar_id,""{search}"") or contains(dashboard_template_id,""{search}"") or contains(working_time_calendar_id,""{search}"") or contains(layout_set_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "contact", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                employeesemployees = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Employeesemployees" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddEmployeesEmployee>("Add EmployeesEmployee", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> args)
        {
            await DialogService.OpenAsync<EditEmployeesEmployee>("Edit EmployeesEmployee", new Dictionary<string, object> { {"employee_id", args.Data.employee_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesEmployee)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteEmployeesEmployee(employeeId:employeesEmployee.employee_id);

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
                    Detail = $"Unable to delete EmployeesEmployee"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportEmployeesemployeesToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "contact",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Employeesemployees");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportEmployeesemployeesToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "contact",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Employeesemployees");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesEmployeeChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee args)
        {
            employeesEmployeeChild = args;
            var AccountsaccountsResult = await EspoDbNewService.GetAccountsaccounts();
            if (AccountsaccountsResult != null)
            {
                args.Accountsaccounts = AccountsaccountsResult.Value.ToList();
            }
            var DocumentsdocumentsResult = await EspoDbNewService.GetDocumentsdocuments();
            if (DocumentsdocumentsResult != null)
            {
                args.Documentsdocuments = DocumentsdocumentsResult.Value.ToList();
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
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsAccountAccountsaccounts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> campaigncampaignsForcampaignIdAccountsaccounts;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> employeesemployeesForassignedEmployeeIdAccountsaccounts;

        protected int campaigncampaignsForcampaignIdAccountsaccountsCount;
        protected EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaigncampaignsForcampaignIdAccountsaccountsValue;
        protected async Task campaigncampaignsForcampaignIdAccountsaccountsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCampaigncampaigns(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(users_template_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                campaigncampaignsForcampaignIdAccountsaccounts = result.Value.AsODataEnumerable();
                campaigncampaignsForcampaignIdAccountsaccountsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load campaign" });
            }
        }

        protected int employeesemployeesForassignedEmployeeIdAccountsaccountsCount;
        protected EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesemployeesForassignedEmployeeIdAccountsaccountsValue;
        protected async Task employeesemployeesForassignedEmployeeIdAccountsaccountsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetEmployeesemployees(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(layout_set_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                employeesemployeesForassignedEmployeeIdAccountsaccounts = result.Value.AsODataEnumerable();
                employeesemployeesForassignedEmployeeIdAccountsaccountsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load employee" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.AccountsAccount> AccountsaccountsDataGrid;

        protected async Task AccountsaccountsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee data)
        {

            var dialogResult = await DialogService.OpenAsync<AddAccountsAccount>("Add Accountsaccounts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await AccountsaccountsDataGrid.Reload();

        }

        protected async Task AccountsaccountsRowSelect(EspoNew.Server.Models.EspoDbNew.AccountsAccount args, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee data)
        {
            var dialogResult = await DialogService.OpenAsync<EditAccountsAccount>("Edit Accountsaccounts", new Dictionary<string, object> { {"account_id", args.account_id} });
            await GetChildData(data);
            await AccountsaccountsDataGrid.Reload();
        }

        protected async Task AccountsaccountsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsAccount)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteAccountsAccount(accountId:accountsAccount.account_id);

                    await GetChildData(employeesEmployeeChild);

                    if (deleteResult != null)
                    {
                        await AccountsaccountsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete AccountsAccount"
                });
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsDocumentDocumentsdocuments;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> employeesemployeesForassignedEmployeeIdDocumentsdocuments;

        protected int employeesemployeesForassignedEmployeeIdDocumentsdocumentsCount;
        protected EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesemployeesForassignedEmployeeIdDocumentsdocumentsValue;
        protected async Task employeesemployeesForassignedEmployeeIdDocumentsdocumentsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetEmployeesemployees(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(layout_set_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                employeesemployeesForassignedEmployeeIdDocumentsdocuments = result.Value.AsODataEnumerable();
                employeesemployeesForassignedEmployeeIdDocumentsdocumentsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load employee" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> DocumentsdocumentsDataGrid;

        protected async Task DocumentsdocumentsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee data)
        {

            var dialogResult = await DialogService.OpenAsync<AddDocumentsDocument>("Add Documentsdocuments", new Dictionary<string, object> {  });
            await GetChildData(data);
            await DocumentsdocumentsDataGrid.Reload();

        }

        protected async Task DocumentsdocumentsRowSelect(EspoNew.Server.Models.EspoDbNew.DocumentsDocument args, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee data)
        {
            var dialogResult = await DialogService.OpenAsync<EditDocumentsDocument>("Edit Documentsdocuments", new Dictionary<string, object> { {"document_id", args.document_id} });
            await GetChildData(data);
            await DocumentsdocumentsDataGrid.Reload();
        }

        protected async Task DocumentsdocumentsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.DocumentsDocument documentsDocument)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteDocumentsDocument(documentId:documentsDocument.document_id);

                    await GetChildData(employeesEmployeeChild);

                    if (deleteResult != null)
                    {
                        await DocumentsdocumentsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete DocumentsDocument"
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

        protected async Task OpportunitiesopportunitiesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee data)
        {

            var dialogResult = await DialogService.OpenAsync<AddOpportunitiesOpportunity>("Add Opportunitiesopportunities", new Dictionary<string, object> {  });
            await GetChildData(data);
            await OpportunitiesopportunitiesDataGrid.Reload();

        }

        protected async Task OpportunitiesopportunitiesRowSelect(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity args, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee data)
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

                    await GetChildData(employeesEmployeeChild);

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

        protected async Task TaskstasksAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee data)
        {

            var dialogResult = await DialogService.OpenAsync<AddTasksTask>("Add Taskstasks", new Dictionary<string, object> {  });
            await GetChildData(data);
            await TaskstasksDataGrid.Reload();

        }

        protected async Task TaskstasksRowSelect(EspoNew.Server.Models.EspoDbNew.TasksTask args, EspoNew.Server.Models.EspoDbNew.EmployeesEmployee data)
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

                    await GetChildData(employeesEmployeeChild);

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