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
    [Route("odata/EspoDbNew/Leadsleads")]
    public partial class LeadsleadsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public LeadsleadsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.LeadsLead> GetLeadsleads()
        {
            var items = this.context.Leadsleads.AsQueryable<EspoNew.Server.Models.EspoDbNew.LeadsLead>();
            this.OnLeadsleadsRead(ref items);

            return items;
        }

        partial void OnLeadsleadsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.LeadsLead> items);

        partial void OnLeadsLeadGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.LeadsLead> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Leadsleads(lead_id={lead_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.LeadsLead> GetLeadsLead(string key)
        {
            var items = this.context.Leadsleads.Where(i => i.lead_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnLeadsLeadGet(ref result);

            return result;
        }
        partial void OnLeadsLeadDeleted(EspoNew.Server.Models.EspoDbNew.LeadsLead item);
        partial void OnAfterLeadsLeadDeleted(EspoNew.Server.Models.EspoDbNew.LeadsLead item);

        [HttpDelete("/odata/EspoDbNew/Leadsleads(lead_id={lead_id})")]
        public IActionResult DeleteLeadsLead(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Leadsleads
                    .Where(i => i.lead_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Callscall_leads)
                    .Include(i => i.Cases_cases)
                    .Include(i => i.Documentsdocument_leads)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.LeadsLead>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLeadsLeadDeleted(item);
                this.context.Leadsleads.Remove(item);
                this.context.SaveChanges();
                this.OnAfterLeadsLeadDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLeadsLeadUpdated(EspoNew.Server.Models.EspoDbNew.LeadsLead item);
        partial void OnAfterLeadsLeadUpdated(EspoNew.Server.Models.EspoDbNew.LeadsLead item);

        [HttpPut("/odata/EspoDbNew/Leadsleads(lead_id={lead_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutLeadsLead(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.LeadsLead item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Leadsleads
                    .Where(i => i.lead_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.LeadsLead>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnLeadsLeadUpdated(item);
                this.context.Leadsleads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Leadsleads.Where(i => i.lead_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "campaign");
                this.OnAfterLeadsLeadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Leadsleads(lead_id={lead_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchLeadsLead(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.LeadsLead> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Leadsleads
                    .Where(i => i.lead_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.LeadsLead>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnLeadsLeadUpdated(item);
                this.context.Leadsleads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Leadsleads.Where(i => i.lead_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "campaign");
                this.OnAfterLeadsLeadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnLeadsLeadCreated(EspoNew.Server.Models.EspoDbNew.LeadsLead item);
        partial void OnAfterLeadsLeadCreated(EspoNew.Server.Models.EspoDbNew.LeadsLead item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.LeadsLead item)
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

                this.OnLeadsLeadCreated(item);
                this.context.Leadsleads.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Leadsleads.Where(i => i.lead_id == item.lead_id);

                Request.QueryString = Request.QueryString.Add("$expand", "campaign");

                this.OnAfterLeadsLeadCreated(item);

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
