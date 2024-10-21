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
    public partial class AddContactsContactOpportunity
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
        protected EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity contactsContactOpportunity;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> opportunitiesopportunitiesForopportunityId;


        protected int contactscontactsForcontactIdCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdValue;
        protected async Task contactscontactsForcontactIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactId = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdCount = result.Count;

                if (!object.Equals(contactsContactOpportunity.contact_id, null))
                {
                    var valueResult = await EspoDbNewService.GetContactscontacts(filter: $"contact_id eq '{contactsContactOpportunity.contact_id}'");
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

        protected int opportunitiesopportunitiesForopportunityIdCount;
        protected EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity opportunitiesopportunitiesForopportunityIdValue;
        protected async Task opportunitiesopportunitiesForopportunityIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetOpportunitiesopportunities(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                opportunitiesopportunitiesForopportunityId = result.Value.AsODataEnumerable();
                opportunitiesopportunitiesForopportunityIdCount = result.Count;

                if (!object.Equals(contactsContactOpportunity.opportunity_id, null))
                {
                    var valueResult = await EspoDbNewService.GetOpportunitiesopportunities(filter: $"opportunity_id eq '{contactsContactOpportunity.opportunity_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        opportunitiesopportunitiesForopportunityIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load opportunity" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.CreateContactsContactOpportunity(contactsContactOpportunity);
                DialogService.Close(contactsContactOpportunity);
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





        bool hascontact_idValue;

        [Parameter]
        public string contact_id { get; set; }

        bool hasopportunity_idValue;

        [Parameter]
        public string opportunity_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            contactsContactOpportunity = new EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity();

            hascontact_idValue = parameters.TryGetValue<string>("contact_id", out var hascontact_idResult);

            if (hascontact_idValue)
            {
                contactsContactOpportunity.contact_id = hascontact_idResult;
            }

            hasopportunity_idValue = parameters.TryGetValue<string>("opportunity_id", out var hasopportunity_idResult);

            if (hasopportunity_idValue)
            {
                contactsContactOpportunity.opportunity_id = hasopportunity_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}