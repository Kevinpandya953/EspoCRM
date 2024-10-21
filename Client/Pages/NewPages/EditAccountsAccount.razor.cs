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
    public partial class EditAccountsAccount
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
        public string account_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            accountsAccount = await EspoDbNewService.GetAccountsAccountByAccountId(accountId:account_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsAccount;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> campaigncampaignsForcampaignId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> employeesemployeesForassignedEmployeeId;


        protected int campaigncampaignsForcampaignIdCount;
        protected EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaigncampaignsForcampaignIdValue;
        protected async Task campaigncampaignsForcampaignIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCampaigncampaigns(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(users_template_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                campaigncampaignsForcampaignId = result.Value.AsODataEnumerable();
                campaigncampaignsForcampaignIdCount = result.Count;

                if (!object.Equals(accountsAccount.campaign_id, null))
                {
                    var valueResult = await EspoDbNewService.GetCampaigncampaigns(filter: $"campaign_id eq '{accountsAccount.campaign_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        campaigncampaignsForcampaignIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load campaign" });
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

                if (!object.Equals(accountsAccount.assigned_employee_id, null))
                {
                    var valueResult = await EspoDbNewService.GetEmployeesemployees(filter: $"employee_id eq '{accountsAccount.assigned_employee_id}'");
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
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateAccountsAccount(accountId:account_id, accountsAccount);
                DialogService.Close(accountsAccount);
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





        bool hasassigned_employee_idValue;

        [Parameter]
        public string assigned_employee_id { get; set; }

        bool hascampaign_idValue;

        [Parameter]
        public string campaign_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            accountsAccount = new EspoNew.Server.Models.EspoDbNew.AccountsAccount();

            hasassigned_employee_idValue = parameters.TryGetValue<string>("assigned_employee_id", out var hasassigned_employee_idResult);

            if (hasassigned_employee_idValue)
            {
                accountsAccount.assigned_employee_id = hasassigned_employee_idResult;
            }

            hascampaign_idValue = parameters.TryGetValue<string>("campaign_id", out var hascampaign_idResult);

            if (hascampaign_idValue)
            {
                accountsAccount.campaign_id = hascampaign_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}