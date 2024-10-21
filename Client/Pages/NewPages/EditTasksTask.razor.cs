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
    public partial class EditTasksTask
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

        [Parameter]
        public string task_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            tasksTask = await EspoDbNewService.GetTasksTaskByTaskId(taskId:task_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.TasksTask tasksTask;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> employeesemployeesForassignedEmployeeId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmailEmail> emailemailsForemailId;


        protected int accountsaccountsForaccountIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdValue;
        protected async Task accountsaccountsForaccountIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountId = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdCount = result.Count;

                if (!object.Equals(tasksTask.account_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAccountsaccounts(filter: $"account_id eq '{tasksTask.account_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        accountsaccountsForaccountIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load account" });
            }
        }

        protected int contactscontactsForcontactIdCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdValue;
        protected async Task contactscontactsForcontactIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactId = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdCount = result.Count;

                if (!object.Equals(tasksTask.contact_id, null))
                {
                    var valueResult = await EspoDbNewService.GetContactscontacts(filter: $"contact_id eq '{tasksTask.contact_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        contactscontactsForcontactIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected int employeesemployeesForassignedEmployeeIdCount;
        protected EspoNew.Server.Models.EspoDbNew.EmployeesEmployee employeesemployeesForassignedEmployeeIdValue;
        protected async Task employeesemployeesForassignedEmployeeIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetEmployeesemployees(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(layout_set_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                employeesemployeesForassignedEmployeeId = result.Value.AsODataEnumerable();
                employeesemployeesForassignedEmployeeIdCount = result.Count;

                if (!object.Equals(tasksTask.assigned_employee_id, null))
                {
                    var valueResult = await EspoDbNewService.GetEmployeesemployees(filter: $"employee_id eq '{tasksTask.assigned_employee_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        employeesemployeesForassignedEmployeeIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load employee" });
            }
        }

        protected int emailemailsForemailIdCount;
        protected EspoNew.Server.Models.EspoDbNew.EmailEmail emailemailsForemailIdValue;
        protected async Task emailemailsForemailIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetEmailemails(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(account_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                emailemailsForemailId = result.Value.AsODataEnumerable();
                emailemailsForemailIdCount = result.Count;

                if (!object.Equals(tasksTask.email_id, null))
                {
                    var valueResult = await EspoDbNewService.GetEmailemails(filter: $"email_id eq '{tasksTask.email_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        emailemailsForemailIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load email" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateTasksTask(taskId:task_id, tasksTask);
                DialogService.Close(tasksTask);
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





        bool hasaccount_idValue;

        [Parameter]
        public string account_id { get; set; }

        bool hasassigned_employee_idValue;

        [Parameter]
        public string assigned_employee_id { get; set; }

        bool hascontact_idValue;

        [Parameter]
        public string contact_id { get; set; }

        bool hasemail_idValue;

        [Parameter]
        public string email_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            tasksTask = new EspoNew.Server.Models.EspoDbNew.TasksTask();

            hasaccount_idValue = parameters.TryGetValue<string>("account_id", out var hasaccount_idResult);

            if (hasaccount_idValue)
            {
                tasksTask.account_id = hasaccount_idResult;
            }

            hasassigned_employee_idValue = parameters.TryGetValue<string>("assigned_employee_id", out var hasassigned_employee_idResult);

            if (hasassigned_employee_idValue)
            {
                tasksTask.assigned_employee_id = hasassigned_employee_idResult;
            }

            hascontact_idValue = parameters.TryGetValue<string>("contact_id", out var hascontact_idResult);

            if (hascontact_idValue)
            {
                tasksTask.contact_id = hascontact_idResult;
            }

            hasemail_idValue = parameters.TryGetValue<string>("email_id", out var hasemail_idResult);

            if (hasemail_idValue)
            {
                tasksTask.email_id = hasemail_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}