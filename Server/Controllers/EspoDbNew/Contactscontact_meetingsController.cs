using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EspoNew.Server.Controllers.EspoDbNew
{
    [Route("odata/EspoDbNew/Contactscontact_meetings")]
    public partial class Contactscontact_meetingsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Contactscontact_meetingsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> GetContactscontact_meetings()
        {
            var items = this.context.Contactscontact_meetings.AsQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>();
            this.OnContactscontact_meetingsRead(ref items);

            return items;
        }

        partial void OnContactscontact_meetingsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> items);

        partial void OnContactsContactMeetingGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Contactscontact_meetings(contact_meeting_id={contact_meeting_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> GetContactsContactMeeting(string key)
        {
            var items = this.context.Contactscontact_meetings.Where(i => i.contact_meeting_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnContactsContactMeetingGet(ref result);

            return result;
        }
        partial void OnContactsContactMeetingDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);
        partial void OnAfterContactsContactMeetingDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);

        [HttpDelete("/odata/EspoDbNew/Contactscontact_meetings(contact_meeting_id={contact_meeting_id})")]
        public IActionResult DeleteContactsContactMeeting(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Contactscontact_meetings
                    .Where(i => i.contact_meeting_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactsContactMeetingDeleted(item);
                this.context.Contactscontact_meetings.Remove(item);
                this.context.SaveChanges();
                this.OnAfterContactsContactMeetingDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactsContactMeetingUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);
        partial void OnAfterContactsContactMeetingUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);

        [HttpPut("/odata/EspoDbNew/Contactscontact_meetings(contact_meeting_id={contact_meeting_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutContactsContactMeeting(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Contactscontact_meetings
                    .Where(i => i.contact_meeting_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactsContactMeetingUpdated(item);
                this.context.Contactscontact_meetings.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontact_meetings.Where(i => i.contact_meeting_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "contact,meeting");
                this.OnAfterContactsContactMeetingUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Contactscontact_meetings(contact_meeting_id={contact_meeting_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchContactsContactMeeting(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Contactscontact_meetings
                    .Where(i => i.contact_meeting_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnContactsContactMeetingUpdated(item);
                this.context.Contactscontact_meetings.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontact_meetings.Where(i => i.contact_meeting_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "contact,meeting");
                this.OnAfterContactsContactMeetingUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactsContactMeetingCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);
        partial void OnAfterContactsContactMeetingCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.ContactsContactMeeting item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnContactsContactMeetingCreated(item);
                this.context.Contactscontact_meetings.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontact_meetings.Where(i => i.contact_meeting_id == item.contact_meeting_id);

                Request.QueryString = Request.QueryString.Add("$expand", "contact,meeting");

                this.OnAfterContactsContactMeetingCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
