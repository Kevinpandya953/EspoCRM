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
    [Route("odata/EspoDbNew/Callscall_leads")]
    public partial class Callscall_leadsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Callscall_leadsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.CallsCallLead> GetCallscall_leads()
        {
            var items = this.context.Callscall_leads.AsQueryable<EspoNew.Server.Models.EspoDbNew.CallsCallLead>();
            this.OnCallscall_leadsRead(ref items);

            return items;
        }

        partial void OnCallscall_leadsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCallLead> items);

        partial void OnCallsCallLeadGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.CallsCallLead> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Callscall_leads(call_lead_id={call_lead_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.CallsCallLead> GetCallsCallLead(string key)
        {
            var items = this.context.Callscall_leads.Where(i => i.call_lead_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnCallsCallLeadGet(ref result);

            return result;
        }
        partial void OnCallsCallLeadDeleted(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);
        partial void OnAfterCallsCallLeadDeleted(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);

        [HttpDelete("/odata/EspoDbNew/Callscall_leads(call_lead_id={call_lead_id})")]
        public IActionResult DeleteCallsCallLead(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Callscall_leads
                    .Where(i => i.call_lead_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CallsCallLead>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCallsCallLeadDeleted(item);
                this.context.Callscall_leads.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCallsCallLeadDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCallsCallLeadUpdated(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);
        partial void OnAfterCallsCallLeadUpdated(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);

        [HttpPut("/odata/EspoDbNew/Callscall_leads(call_lead_id={call_lead_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCallsCallLead(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.CallsCallLead item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Callscall_leads
                    .Where(i => i.call_lead_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CallsCallLead>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCallsCallLeadUpdated(item);
                this.context.Callscall_leads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Callscall_leads.Where(i => i.call_lead_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "call,lead");
                this.OnAfterCallsCallLeadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Callscall_leads(call_lead_id={call_lead_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCallsCallLead(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.CallsCallLead> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Callscall_leads
                    .Where(i => i.call_lead_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CallsCallLead>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCallsCallLeadUpdated(item);
                this.context.Callscall_leads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Callscall_leads.Where(i => i.call_lead_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "call,lead");
                this.OnAfterCallsCallLeadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCallsCallLeadCreated(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);
        partial void OnAfterCallsCallLeadCreated(EspoNew.Server.Models.EspoDbNew.CallsCallLead item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.CallsCallLead item)
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

                this.OnCallsCallLeadCreated(item);
                this.context.Callscall_leads.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Callscall_leads.Where(i => i.call_lead_id == item.call_lead_id);

                Request.QueryString = Request.QueryString.Add("$expand", "call,lead");

                this.OnAfterCallsCallLeadCreated(item);

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
