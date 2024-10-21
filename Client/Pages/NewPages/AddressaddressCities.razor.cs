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
    public partial class AddressaddressCities
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> addressaddressCities;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> grid0;
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
                var result = await EspoDbNewService.GetAddressaddress_cities(filter: $@"(contains(ETag,""{search}"") or contains(city_id,""{search}"") or contains(city_name,""{search}"") or contains(state_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "address_state", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                addressaddressCities = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Addressaddress_cities" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddAddressAddressCity>("Add AddressAddressCity", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> args)
        {
            await DialogService.OpenAsync<EditAddressAddressCity>("Edit AddressAddressCity", new Dictionary<string, object> { {"city_id", args.Data.city_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AddressAddressCity addressAddressCity)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteAddressAddressCity(cityId:addressAddressCity.city_id);

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
                    Detail = $"Unable to delete AddressAddressCity"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportAddressaddress_citiesToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "address_state",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Addressaddress_cities");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportAddressaddress_citiesToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "address_state",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Addressaddress_cities");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.AddressAddressCity addressAddressCityChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.AddressAddressCity args)
        {
            addressAddressCityChild = args;
            var ContactscontactsResult = await EspoDbNewService.GetContactscontacts();
            if (ContactscontactsResult != null)
            {
                args.Contactscontacts = ContactscontactsResult.Value.ToList();
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

        protected async Task ContactscontactsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.AddressAddressCity data)
        {

            var dialogResult = await DialogService.OpenAsync<AddContactsContact>("Add Contactscontacts", new Dictionary<string, object> {  });
            await GetChildData(data);
            await ContactscontactsDataGrid.Reload();

        }

        protected async Task ContactscontactsRowSelect(EspoNew.Server.Models.EspoDbNew.ContactsContact args, EspoNew.Server.Models.EspoDbNew.AddressAddressCity data)
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

                    await GetChildData(addressAddressCityChild);

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