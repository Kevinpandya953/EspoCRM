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
    public partial class Emailemails
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmailEmail> emailemails;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.EmailEmail> grid0;
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
                var result = await EspoDbNewService.GetEmailemails(filter: $@"(contains(ETag,""{search}"") or contains(email_id,""{search}"") or contains(name,""{search}"") or contains(from_string,""{search}"") or contains(reply_to_string,""{search}"") or contains(address_name_map,""{search}"") or contains(message_id,""{search}"") or contains(message_id_internal,""{search}"") or contains(body_plain,""{search}"") or contains(body,""{search}"") or contains(status,""{search}"") or contains(ics_contents,""{search}"") or contains(ics_event_uid,""{search}"") or contains(from_email_address_id,""{search}"") or contains(parent_id,""{search}"") or contains(parent_type,""{search}"") or contains(sent_by_id,""{search}"") or contains(assigned_employee_id,""{search}"") or contains(replied_id,""{search}"") or contains(created_event_id,""{search}"") or contains(created_event_type,""{search}"") or contains(group_folder_id,""{search}"") or contains(account_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "account", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                emailemails = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Emailemails" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddEmailEmail>("Add EmailEmail", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.EmailEmail> args)
        {
            await DialogService.OpenAsync<EditEmailEmail>("Edit EmailEmail", new Dictionary<string, object> { {"email_id", args.Data.email_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.EmailEmail emailEmail)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteEmailEmail(emailId:emailEmail.email_id);

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
                    Detail = $"Unable to delete EmailEmail"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportEmailemailsToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "account",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Emailemails");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportEmailemailsToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "account",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Emailemails");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.EmailEmail emailEmailChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.EmailEmail args)
        {
            emailEmailChild = args;
            var TaskstasksResult = await EspoDbNewService.GetTaskstasks();
            if (TaskstasksResult != null)
            {
                args.Taskstasks = TaskstasksResult.Value.ToList();
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

        protected async Task TaskstasksAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.EmailEmail data)
        {

            var dialogResult = await DialogService.OpenAsync<AddTasksTask>("Add Taskstasks", new Dictionary<string, object> {  });
            await GetChildData(data);
            await TaskstasksDataGrid.Reload();

        }

        protected async Task TaskstasksRowSelect(EspoNew.Server.Models.EspoDbNew.TasksTask args, EspoNew.Server.Models.EspoDbNew.EmailEmail data)
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

                    await GetChildData(emailEmailChild);

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