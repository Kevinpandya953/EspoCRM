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
    public partial class EditAddressAddressCountry
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
        public string country_id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            addressAddressCountry = await EspoDbNewService.GetAddressAddressCountryByCountryId(countryId:country_id);
        }
        protected bool errorVisible;
        protected EspoNew.Server.Models.EspoDbNew.AddressAddressCountry addressAddressCountry;

        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.UpdateAddressAddressCountry(countryId:country_id, addressAddressCountry);
                DialogService.Close(addressAddressCountry);
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
    }
}