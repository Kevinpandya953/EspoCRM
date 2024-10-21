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
    [Route("odata/EspoDbNew/Opportunitiesopportunities")]
    public partial class OpportunitiesopportunitiesController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public OpportunitiesopportunitiesController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> GetOpportunitiesopportunities()
        {
            var items = this.context.Opportunitiesopportunities.AsQueryable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>();
            this.OnOpportunitiesopportunitiesRead(ref items);

            return items;
        }

        partial void OnOpportunitiesopportunitiesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> items);

        partial void OnOpportunitiesOpportunityGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Opportunitiesopportunities(opportunity_id={opportunity_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> GetOpportunitiesOpportunity(string key)
        {
            var items = this.context.Opportunitiesopportunities.Where(i => i.opportunity_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnOpportunitiesOpportunityGet(ref result);

            return result;
        }
        partial void OnOpportunitiesOpportunityDeleted(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);
        partial void OnAfterOpportunitiesOpportunityDeleted(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);

        [HttpDelete("/odata/EspoDbNew/Opportunitiesopportunities(opportunity_id={opportunity_id})")]
        public IActionResult DeleteOpportunitiesOpportunity(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Opportunitiesopportunities
                    .Where(i => i.opportunity_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Contactscontact_opportunities)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnOpportunitiesOpportunityDeleted(item);
                this.context.Opportunitiesopportunities.Remove(item);
                this.context.SaveChanges();
                this.OnAfterOpportunitiesOpportunityDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnOpportunitiesOpportunityUpdated(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);
        partial void OnAfterOpportunitiesOpportunityUpdated(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);

        [HttpPut("/odata/EspoDbNew/Opportunitiesopportunities(opportunity_id={opportunity_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutOpportunitiesOpportunity(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Opportunitiesopportunities
                    .Where(i => i.opportunity_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnOpportunitiesOpportunityUpdated(item);
                this.context.Opportunitiesopportunities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Opportunitiesopportunities.Where(i => i.opportunity_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,employee,campaign,contact");
                this.OnAfterOpportunitiesOpportunityUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Opportunitiesopportunities(opportunity_id={opportunity_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchOpportunitiesOpportunity(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Opportunitiesopportunities
                    .Where(i => i.opportunity_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnOpportunitiesOpportunityUpdated(item);
                this.context.Opportunitiesopportunities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Opportunitiesopportunities.Where(i => i.opportunity_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,employee,campaign,contact");
                this.OnAfterOpportunitiesOpportunityUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnOpportunitiesOpportunityCreated(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);
        partial void OnAfterOpportunitiesOpportunityCreated(EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.OpportunitiesOpportunity item)
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

                this.OnOpportunitiesOpportunityCreated(item);
                this.context.Opportunitiesopportunities.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Opportunitiesopportunities.Where(i => i.opportunity_id == item.opportunity_id);

                Request.QueryString = Request.QueryString.Add("$expand", "account,employee,campaign,contact");

                this.OnAfterOpportunitiesOpportunityCreated(item);

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
