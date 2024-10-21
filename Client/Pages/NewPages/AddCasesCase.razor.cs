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
    public partial class AddCasesCase
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

        protected override async Task OnInitializedAsync()
        {
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.CasesCase casesCase;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.LeadsLead> leadsleadsForleadId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactId;


        protected int accountsaccountsForaccountIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdValue;
        protected async Task accountsaccountsForaccountIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountId = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdCount = result.Count;

                if (!object.Equals(casesCase.account_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAccountsaccounts(filter: $"account_id eq '{casesCase.account_id}'");
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

        protected int leadsleadsForleadIdCount;
        protected EspoNew.Server.Models.EspoDbNew.LeadsLead leadsleadsForleadIdValue;
        protected async Task leadsleadsForleadIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetLeadsleads(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(created_opportunity_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                leadsleadsForleadId = result.Value.AsODataEnumerable();
                leadsleadsForleadIdCount = result.Count;

                if (!object.Equals(casesCase.lead_id, null))
                {
                    var valueResult = await EspoDbNewService.GetLeadsleads(filter: $"lead_id eq '{casesCase.lead_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        leadsleadsForleadIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load lead" });
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

                if (!object.Equals(casesCase.contact_id, null))
                {
                    var valueResult = await EspoDbNewService.GetContactscontacts(filter: $"contact_id eq '{casesCase.contact_id}'");
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
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.CreateCasesCase(casesCase);
                DialogService.Close(casesCase);
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

        bool hascontact_idValue;

        [Parameter]
        public string contact_id { get; set; }

        bool haslead_idValue;

        [Parameter]
        public string lead_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            casesCase = new EspoNew.Server.Models.EspoDbNew.CasesCase();

            hasaccount_idValue = parameters.TryGetValue<string>("account_id", out var hasaccount_idResult);

            if (hasaccount_idValue)
            {
                casesCase.account_id = hasaccount_idResult;
            }

            hascontact_idValue = parameters.TryGetValue<string>("contact_id", out var hascontact_idResult);

            if (hascontact_idValue)
            {
                casesCase.contact_id = hascontact_idResult;
            }

            haslead_idValue = parameters.TryGetValue<string>("lead_id", out var haslead_idResult);

            if (haslead_idValue)
            {
                casesCase.lead_id = haslead_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}