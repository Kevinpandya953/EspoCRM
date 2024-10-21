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
    public partial class EditAddressAddressCity
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
        public string city_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            addressAddressCity = await EspoDbNewService.GetAddressAddressCityByCityId(cityId:city_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressCity addressAddressCity;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressState> addressaddressStatesForstateId;


        protected int addressaddressStatesForstateIdCount;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressState addressaddressStatesForstateIdValue;
        protected async Task addressaddressStatesForstateIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetAddressaddress_states(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(country_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                addressaddressStatesForstateId = result.Value.AsODataEnumerable();
                addressaddressStatesForstateIdCount = result.Count;

                if (!object.Equals(addressAddressCity.state_id, null))
                {
                    var valueResult = await EspoDbNewService.GetAddressaddress_states(filter: $"state_id eq '{addressAddressCity.state_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        addressaddressStatesForstateIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load address_state" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateAddressAddressCity(cityId:city_id, addressAddressCity);
                DialogService.Close(addressAddressCity);
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





        bool hasstate_idValue;

        [Parameter]
        public string state_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            addressAddressCity = new EspoNew.Server.Models.EspoDbNew.AddressAddressCity();

            hasstate_idValue = parameters.TryGetValue<string>("state_id", out var hasstate_idResult);

            if (hasstate_idValue)
            {
                addressAddressCity.state_id = hasstate_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}