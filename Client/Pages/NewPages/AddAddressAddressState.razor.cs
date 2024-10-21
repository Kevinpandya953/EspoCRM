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
    public partial class AddAddressAddressState
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
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressState addressAddressState;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> addressaddressCountriesForcountryId;


        protected int addressaddressCountriesForcountryIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressaddressCountriesForcountryIdValue;
        protected async Task addressaddressCountriesForcountryIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAddressaddress_countries(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(country_name, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                addressaddressCountriesForcountryId = result.Value.AsODataEnumerable();
                addressaddressCountriesForcountryIdCount = result.Count;

                if (!object.Equals(addressAddressState.country_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAddressaddress_countries(filter: $"country_id eq '{addressAddressState.country_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        addressaddressCountriesForcountryIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load address_country" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.CreateAddressAddressState(addressAddressState);
                DialogService.Close(addressAddressState);
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





        bool hascountry_idValue;

        [Parameter]
        public string country_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            addressAddressState = new EspoNew.Server.Models.EspoDbNew.AddressAddressState();

            hascountry_idValue = parameters.TryGetValue<string>("country_id", out var hascountry_idResult);

            if (hascountry_idValue)
            {
                addressAddressState.country_id = hascountry_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}