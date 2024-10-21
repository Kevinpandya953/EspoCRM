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
    [Route("odata/EspoDbNew/Meetingsmeetings")]
    public partial class MeetingsmeetingsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public MeetingsmeetingsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> GetMeetingsmeetings()
        {
            var items = this.context.Meetingsmeetings.AsQueryable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>();
            this.OnMeetingsmeetingsRead(ref items);

            return items;
        }

        partial void OnMeetingsmeetingsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> items);

        partial void OnMeetingsMeetingGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Meetingsmeetings(meeting_id={meeting_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> GetMeetingsMeeting(string key)
        {
            var items = this.context.Meetingsmeetings.Where(i => i.meeting_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnMeetingsMeetingGet(ref result);

            return result;
        }
        partial void OnMeetingsMeetingDeleted(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);
        partial void OnAfterMeetingsMeetingDeleted(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);

        [HttpDelete("/odata/EspoDbNew/Meetingsmeetings(meeting_id={meeting_id})")]
        public IActionResult DeleteMeetingsMeeting(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Meetingsmeetings
                    .Where(i => i.meeting_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Contactscontact_meetings)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMeetingsMeetingDeleted(item);
                this.context.Meetingsmeetings.Remove(item);
                this.context.SaveChanges();
                this.OnAfterMeetingsMeetingDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMeetingsMeetingUpdated(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);
        partial void OnAfterMeetingsMeetingUpdated(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);

        [HttpPut("/odata/EspoDbNew/Meetingsmeetings(meeting_id={meeting_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutMeetingsMeeting(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Meetingsmeetings
                    .Where(i => i.meeting_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnMeetingsMeetingUpdated(item);
                this.context.Meetingsmeetings.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Meetingsmeetings.Where(i => i.meeting_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account");
                this.OnAfterMeetingsMeetingUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Meetingsmeetings(meeting_id={meeting_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchMeetingsMeeting(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Meetingsmeetings
                    .Where(i => i.meeting_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.MeetingsMeeting>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnMeetingsMeetingUpdated(item);
                this.context.Meetingsmeetings.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Meetingsmeetings.Where(i => i.meeting_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account");
                this.OnAfterMeetingsMeetingUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnMeetingsMeetingCreated(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);
        partial void OnAfterMeetingsMeetingCreated(EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.MeetingsMeeting item)
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

                this.OnMeetingsMeetingCreated(item);
                this.context.Meetingsmeetings.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Meetingsmeetings.Where(i => i.meeting_id == item.meeting_id);

                Request.QueryString = Request.QueryString.Add("$expand", "account");

                this.OnAfterMeetingsMeetingCreated(item);

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
