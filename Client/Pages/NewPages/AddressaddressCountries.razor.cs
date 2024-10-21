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
    public partial class AddressaddressCountries
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> addressaddressCountries;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> grid0;
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
                var result = await EspoDbNewService.GetAddressaddress_countries(filter: $@"(contains(ETag,""{search}"") or contains(country_id,""{search}"") or contains(country_name,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                addressaddressCountries = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Addressaddress_countries" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddAddressAddressCountry>("Add AddressAddressCountry", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> args)
        {
            await DialogService.OpenAsync<EditAddressAddressCountry>("Edit AddressAddressCountry", new Dictionary<string, object> { {"country_id", args.Data.country_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressAddressCountry)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteAddressAddressCountry(countryId:addressAddressCountry.country_id);

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
                    Detail = $"Unable to delete AddressAddressCountry"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportAddressaddress_countriesToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Addressaddress_countries");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportAddressaddress_countriesToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Addressaddress_countries");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressAddressCountryChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry args)
        {
            addressAddressCountryChild = args;
            var Addressaddress_statesResult = await EspoDbNewService.GetAddressaddress_states();
            if (Addressaddress_statesResult != null)
            {
                args.Addressaddress_states = Addressaddress_statesResult.Value.ToList();
            }
            var ContactscontactsResult = await EspoDbNewService.GetContactscontacts();
            if (ContactscontactsResult != null)
            {
                args.Contactscontacts = ContactscontactsResult.Value.ToList();
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressState addressAddressStateAddressaddress_states;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> addressaddressCountriesForcountryIdAddressaddress_states;

        protected int addressaddressCountriesForcountryIdAddressaddress_statesCount;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressaddressCountriesForcountryIdAddressaddress_statesValue;
        protected async Task addressaddressCountriesForcountryIdAddressaddress_statesLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAddressaddress_countries(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(country_name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                addressaddressCountriesForcountryIdAddressaddress_states = result.Value.AsODataEnumerable();
                addressaddressCountriesForcountryIdAddressaddress_statesCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load address_country" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.AddressAddressState> Addressaddress_statesDataGrid;

        protected async Task Addressaddress_statesAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AddressAddressCountry data)
        {

            var dialogResult = await DialogService.OpenAsync<AddAddressAddressState>("Add Addressaddress_states", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Addressaddress_statesDataGrid.Reload();

        }

        protected async Task Addressaddress_statesRowSelect(EspoNew.Server.Models.EspoDbNew.AddressAddressState args, EspoNew.Server.Models.EspoDbNew.AddressAddressCountry data)
        {
            var dialogResult = await DialogService.OpenAsync<EditAddressAddressState>("Edit Addressaddress_states", new Dictionary<string, object> { {"state_id", args.state_id} });
            await GetChildData(data);
            await Addressaddress_statesDataGrid.Reload();
        }

        protected async Task Addressaddress_statesDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AddressAddressState addressAddressState)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteAddressAddressState(stateId:addressAddressState.state_id);

                    await GetChildData(addressAddressCountryChild);

                    if (deleteResult != null)
                    {
                        await Addressaddress_statesDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete AddressAddressState"
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

        protected async Task ContactscontactsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AddressAddressCountry data)
        {

            var dialogResult = await DialogService.OpenAsync<AddContactsContact>("Add Contactscontacts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await ContactscontactsDataGrid.Reload();

        }

        protected async Task ContactscontactsRowSelect(EspoNew.Server.Models.EspoDbNew.ContactsContact args, EspoNew.Server.Models.EspoDbNew.AddressAddressCountry data)
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

                    await GetChildData(addressAddressCountryChild);

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
    }
}