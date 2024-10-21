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
    [Route("odata/EspoDbNew/Callscall_contacts")]
    public partial class Callscall_contactsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Callscall_contactsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.CallsCallContact> GetCallscall_contacts()
        {
            var items = this.context.Callscall_contacts.AsQueryable<EspoNew.Server.Models.EspoDbNew.CallsCallContact>();
            this.OnCallscall_contactsRead(ref items);

            return items;
        }

        partial void OnCallscall_contactsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCallContact> items);

        partial void OnCallsCallContactGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.CallsCallContact> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Callscall_contacts(call_contact_id={call_contact_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.CallsCallContact> GetCallsCallContact(string key)
        {
            var items = this.context.Callscall_contacts.Where(i => i.call_contact_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnCallsCallContactGet(ref result);

            return result;
        }
        partial void OnCallsCallContactDeleted(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);
        partial void OnAfterCallsCallContactDeleted(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);

        [HttpDelete("/odata/EspoDbNew/Callscall_contacts(call_contact_id={call_contact_id})")]
        public IActionResult DeleteCallsCallContact(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Callscall_contacts
                    .Where(i => i.call_contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CallsCallContact>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCallsCallContactDeleted(item);
                this.context.Callscall_contacts.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCallsCallContactDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCallsCallContactUpdated(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);
        partial void OnAfterCallsCallContactUpdated(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);

        [HttpPut("/odata/EspoDbNew/Callscall_contacts(call_contact_id={call_contact_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCallsCallContact(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.CallsCallContact item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Callscall_contacts
                    .Where(i => i.call_contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CallsCallContact>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCallsCallContactUpdated(item);
                this.context.Callscall_contacts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Callscall_contacts.Where(i => i.call_contact_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "call,contact");
                this.OnAfterCallsCallContactUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Callscall_contacts(call_contact_id={call_contact_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCallsCallContact(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.CallsCallContact> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Callscall_contacts
                    .Where(i => i.call_contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CallsCallContact>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCallsCallContactUpdated(item);
                this.context.Callscall_contacts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Callscall_contacts.Where(i => i.call_contact_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "call,contact");
                this.OnAfterCallsCallContactUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCallsCallContactCreated(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);
        partial void OnAfterCallsCallContactCreated(EspoNew.Server.Models.EspoDbNew.CallsCallContact item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.CallsCallContact item)
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

                this.OnCallsCallContactCreated(item);
                this.context.Callscall_contacts.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Callscall_contacts.Where(i => i.call_contact_id == item.call_contact_id);

                Request.QueryString = Request.QueryString.Add("$expand", "call,contact");

                this.OnAfterCallsCallContactCreated(item);

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
