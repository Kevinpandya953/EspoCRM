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
    public partial class AddContactsContactMeeting
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
        protected EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting contactsContactMeeting;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactId;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> meetingsmeetingsFormeetingId;


        protected int contactscontactsForcontactIdCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdValue;
        protected async Task contactscontactsForcontactIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactId = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdCount = result.Count;

                if (!object.Equals(contactsContactMeeting.contact_id, null))
                {
                    var valueResult = await EspoDbNewService.GetContactscontacts(filter: $"contact_id eq '{contactsContactMeeting.contact_id}'");
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

        protected int meetingsmeetingsFormeetingIdCount;
        protected EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsmeetingsFormeetingIdValue;
        protected async Task meetingsmeetingsFormeetingIdLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetMeetingsmeetings(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                meetingsmeetingsFormeetingId = result.Value.AsODataEnumerable();
                meetingsmeetingsFormeetingIdCount = result.Count;

                if (!object.Equals(contactsContactMeeting.meeting_id, null))
                {
                    var valueResult = await EspoDbNewService.GetMeetingsmeetings(filter: $"meeting_id eq '{contactsContactMeeting.meeting_id}'");
                    var firstItem = valueResult.Value.FirstOrDefault();
                    if (firstItem != null)
                    {
                        meetingsmeetingsFormeetingIdValue = firstItem;
                    }
                }

            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load meeting" });
            }
        }
        protected async Task FormSubmit()
        {
            try
            {
                await EspoDbNewService.CreateContactsContactMeeting(contactsContactMeeting);
                DialogService.Close(contactsContactMeeting);
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

        bool hasmeeting_idValue;

        [Parameter]
        public string meeting_id { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            contactsContactMeeting = new EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting();

            hascontact_idValue = parameters.TryGetValue<string>("contact_id", out var hascontact_idResult);

            if (hascontact_idValue)
            {
                contactsContactMeeting.contact_id = hascontact_idResult;
            }

            hasmeeting_idValue = parameters.TryGetValue<string>("meeting_id", out var hasmeeting_idResult);

            if (hasmeeting_idValue)
            {
                contactsContactMeeting.meeting_id = hasmeeting_idResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}