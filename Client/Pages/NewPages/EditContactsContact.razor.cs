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
    public partial class EditContactsContact
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
        public string contact_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            contactsContact = await EspoDbNewService.GetContactsContactByContactId(contactId:contact_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactsContact;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> addressaddressCitiesForaddressCityId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressState> addressaddressStatesForaddressStateId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> addressaddressCountriesForaddressCountryId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> accountsaccountsForaccountId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> campaigncampaignsForcampaignId;


        protected int addressaddressCitiesForaddressCityIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressCity addressaddressCitiesForaddressCityIdValue;
        protected async Task addressaddressCitiesForaddressCityIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAddressaddress_cities(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(state_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                addressaddressCitiesForaddressCityId = result.Value.AsODataEnumerable();
                addressaddressCitiesForaddressCityIdCount = result.Count;

                if (!object.Equals(contactsContact.address_city_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAddressaddress_cities(filter: $"city_id eq '{contactsContact.address_city_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        addressaddressCitiesForaddressCityIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load address_city" });
            }
        }

        protected int addressaddressStatesForaddressStateIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressState addressaddressStatesForaddressStateIdValue;
        protected async Task addressaddressStatesForaddressStateIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAddressaddress_states(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(country_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                addressaddressStatesForaddressStateId = result.Value.AsODataEnumerable();
                addressaddressStatesForaddressStateIdCount = result.Count;

                if (!object.Equals(contactsContact.address_state_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAddressaddress_states(filter: $"state_id eq '{contactsContact.address_state_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        addressaddressStatesForaddressStateIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load address_state" });
            }
        }

        protected int addressaddressCountriesForaddressCountryIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressaddressCountriesForaddressCountryIdValue;
        protected async Task addressaddressCountriesForaddressCountryIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAddressaddress_countries(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(country_name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                addressaddressCountriesForaddressCountryId = result.Value.AsODataEnumerable();
                addressaddressCountriesForaddressCountryIdCount = result.Count;

                if (!object.Equals(contactsContact.address_country_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAddressaddress_countries(filter: $"country_id eq '{contactsContact.address_country_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        addressaddressCountriesForaddressCountryIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load address_country" });
            }
        }

        protected int accountsaccountsForaccountIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AccountsAccount accountsaccountsForaccountIdValue;
        protected async Task accountsaccountsForaccountIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAccountsaccounts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                accountsaccountsForaccountId = result.Value.AsODataEnumerable();
                accountsaccountsForaccountIdCount = result.Count;

                if (!object.Equals(contactsContact.account_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAccountsaccounts(filter: $"account_id eq '{contactsContact.account_id}'");
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

        protected int campaigncampaignsForcampaignIdCount;
        protected EspoNew.Server.Models.EspoDbNew.CampaignCampaign campaigncampaignsForcampaignIdValue;
        protected async Task campaigncampaignsForcampaignIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetCampaigncampaigns(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(users_template_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                campaigncampaignsForcampaignId = result.Value.AsODataEnumerable();
                campaigncampaignsForcampaignIdCount = result.Count;

                if (!object.Equals(contactsContact.campaign_id, null))
                {
                    var valueResult = await EspoDbNewService.GetCampaigncampaigns(filter: $"campaign_id eq '{contactsContact.campaign_id}'");
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
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateContactsContact(contactId:contact_id, contactsContact);
                DialogService.Close(contactsContact);
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

        bool hasaddress_city_idValue;

        [Parameter]
        public string address_city_id { get; set; }

        bool hasaddress_country_idValue;

        [Parameter]
        public string address_country_id { get; set; }

        bool hasaddress_state_idValue;

        [Parameter]
        public string address_state_id { get; set; }

        bool hascampaign_idValue;

        [Parameter]
        public string campaign_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            contactsContact = new EspoNew.Server.Models.EspoDbNew.ContactsContact();

            hasaccount_idValue = parameters.TryGetValue<string>("account_id", out var hasaccount_idResult);

            if (hasaccount_idValue)
            {
                contactsContact.account_id = hasaccount_idResult;
            }

            hasaddress_city_idValue = parameters.TryGetValue<string>("address_city_id", out var hasaddress_city_idResult);

            if (hasaddress_city_idValue)
            {
                contactsContact.address_city_id = hasaddress_city_idResult;
            }

            hasaddress_country_idValue = parameters.TryGetValue<string>("address_country_id", out var hasaddress_country_idResult);

            if (hasaddress_country_idValue)
            {
                contactsContact.address_country_id = hasaddress_country_idResult;
            }

            hasaddress_state_idValue = parameters.TryGetValue<string>("address_state_id", out var hasaddress_state_idResult);

            if (hasaddress_state_idValue)
            {
                contactsContact.address_state_id = hasaddress_state_idResult;
            }

            hascampaign_idValue = parameters.TryGetValue<string>("campaign_id", out var hascampaign_idResult);

            if (hascampaign_idValue)
            {
                contactsContact.campaign_id = hascampaign_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}