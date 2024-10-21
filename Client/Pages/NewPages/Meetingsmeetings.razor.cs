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
    public partial class Meetingsmeetings
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

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> meetingsmeetings;

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> grid0;
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
                var result = await EspoDbNewService.GetMeetingsmeetings(filter: $@"(contains(ETag,""{search}"") or contains(meeting_id,""{search}"") or contains(name,""{search}"") or contains(status,""{search}"") or contains(description,""{search}"") or contains(parent_id,""{search}"") or contains(parent_type,""{search}"") or contains(account_id,""{search}"") or contains(assigned_employee_id,""{search}"")) and {(string.IsNullOrEmpty(args.Filter)? "true" : args.Filter)}", expand: "account", orderby: $"{args.OrderBy}", top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null);
                meetingsmeetings = result.Value.AsODataEnumerable();
                count = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load Meetingsmeetings" });
            }
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddMeetingsMeeting>("Add MeetingsMeeting", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> args)
        {
            await DialogService.OpenAsync<EditMeetingsMeeting>("Edit MeetingsMeeting", new Dictionary<string, object> { {"meeting_id", args.Data.meeting_id} });
            await grid0.Reload();
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsMeeting)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteMeetingsMeeting(meetingId:meetingsMeeting.meeting_id);

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
                    Detail = $"Unable to delete MeetingsMeeting"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await EspoDbNewService.ExportMeetingsmeetingsToCSV(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "account",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Meetingsmeetings");
            }

            if (args == null || args.Value == "xlsx")
            {
                await EspoDbNewService.ExportMeetingsmeetingsToExcel(new Query
{
    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
    OrderBy = $"{grid0.Query.OrderBy}",
    Expand = "account",
    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
}, "Meetingsmeetings");
            }
        }

        protected EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsMeetingChild;
        protected async Task GetChildData(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting args)
        {
            meetingsMeetingChild = args;
            var Contactscontact_meetingsResult = await EspoDbNewService.GetContactscontact_meetings();
            if (Contactscontact_meetingsResult != null)
            {
                args.Contactscontact_meetings = Contactscontact_meetingsResult.Value.ToList();
            }
        }
        protected EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting contactsContactMeetingContactscontact_meetings;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> contactscontactsForcontactIdContactscontact_meetings;

        protected IEnumerable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> meetingsmeetingsFormeetingIdContactscontact_meetings;

        protected int contactscontactsForcontactIdContactscontact_meetingsCount;
        protected EspoNew.Server.Models.EspoDbNew.ContactsContact contactscontactsForcontactIdContactscontact_meetingsValue;
        protected async Task contactscontactsForcontactIdContactscontact_meetingsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetContactscontacts(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                contactscontactsForcontactIdContactscontact_meetings = result.Value.AsODataEnumerable();
                contactscontactsForcontactIdContactscontact_meetingsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load contact" });
            }
        }

        protected int meetingsmeetingsFormeetingIdContactscontact_meetingsCount;
        protected EspoNew.Server.Models.EspoDbNew.MeetingsMeeting meetingsmeetingsFormeetingIdContactscontact_meetingsValue;
        protected async Task meetingsmeetingsFormeetingIdContactscontact_meetingsLoadData(LoadDataArgs args)
        {
            try
            {
                var result = await EspoDbNewService.GetMeetingsmeetings(top: args.Top, skip: args.Skip, count:args.Top != null && args.Skip != null, filter: $"contains(assigned_employee_id, '{(!string.IsNullOrEmpty(args.Filter) ? args.Filter : "")}')", orderby: $"{args.OrderBy}");
                meetingsmeetingsFormeetingIdContactscontact_meetings = result.Value.AsODataEnumerable();
                meetingsmeetingsFormeetingIdContactscontact_meetingsCount = result.Count;
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to load meeting" });
            }
        }

        protected RadzenDataGrid<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> Contactscontact_meetingsDataGrid;

        protected async Task Contactscontact_meetingsAddButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.MeetingsMeeting data)
        {

            var dialogResult = await DialogService.OpenAsync<AddContactsContactMeeting>("Add Contactscontact_meetings", new Dictionary<string, object> {  });
            await GetChildData(data);
            await Contactscontact_meetingsDataGrid.Reload();

        }

        protected async Task Contactscontact_meetingsRowSelect(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting args, EspoNew.Server.Models.EspoDbNew.MeetingsMeeting data)
        {
            var dialogResult = await DialogService.OpenAsync<EditContactsContactMeeting>("Edit Contactscontact_meetings", new Dictionary<string, object> { {"contact_meeting_id", args.contact_meeting_id} });
            await GetChildData(data);
            await Contactscontact_meetingsDataGrid.Reload();
        }

        protected async Task Contactscontact_meetingsDeleteButtonClick(MouseEventArgs args, EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting contactsContactMeeting)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await EspoDbNewService.DeleteContactsContactMeeting(contactMeetingId:contactsContactMeeting.contact_meeting_id);

                    await GetChildData(meetingsMeetingChild);

                    if (deleteResult != null)
                    {
                        await Contactscontact_meetingsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete ContactsContactMeeting"
                });
            }
        }
    }
}