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
    [Route("odata/EspoDbNew/Campaigncampaigns")]
    public partial class CampaigncampaignsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public CampaigncampaignsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> GetCampaigncampaigns()
        {
            var items = this.context.Campaigncampaigns.AsQueryable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>();
            this.OnCampaigncampaignsRead(ref items);

            return items;
        }

        partial void OnCampaigncampaignsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> items);

        partial void OnCampaignCampaignGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Campaigncampaigns(campaign_id={campaign_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> GetCampaignCampaign(string key)
        {
            var items = this.context.Campaigncampaigns.Where(i => i.campaign_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnCampaignCampaignGet(ref result);

            return result;
        }
        partial void OnCampaignCampaignDeleted(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);
        partial void OnAfterCampaignCampaignDeleted(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);

        [HttpDelete("/odata/EspoDbNew/Campaigncampaigns(campaign_id={campaign_id})")]
        public IActionResult DeleteCampaignCampaign(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Campaigncampaigns
                    .Where(i => i.campaign_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Accountsaccounts)
                    .Include(i => i.Contactscontacts)
                    .Include(i => i.Leadsleads)
                    .Include(i => i.Opportunitiesopportunities)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCampaignCampaignDeleted(item);
                this.context.Campaigncampaigns.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCampaignCampaignDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCampaignCampaignUpdated(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);
        partial void OnAfterCampaignCampaignUpdated(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);

        [HttpPut("/odata/EspoDbNew/Campaigncampaigns(campaign_id={campaign_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCampaignCampaign(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.CampaignCampaign item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Campaigncampaigns
                    .Where(i => i.campaign_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCampaignCampaignUpdated(item);
                this.context.Campaigncampaigns.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Campaigncampaigns.Where(i => i.campaign_id == Uri.UnescapeDataString(key));
                
                this.OnAfterCampaignCampaignUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Campaigncampaigns(campaign_id={campaign_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCampaignCampaign(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.CampaignCampaign> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Campaigncampaigns
                    .Where(i => i.campaign_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CampaignCampaign>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCampaignCampaignUpdated(item);
                this.context.Campaigncampaigns.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Campaigncampaigns.Where(i => i.campaign_id == Uri.UnescapeDataString(key));
                
                this.OnAfterCampaignCampaignUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCampaignCampaignCreated(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);
        partial void OnAfterCampaignCampaignCreated(EspoNew.Server.Models.EspoDbNew.CampaignCampaign item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.CampaignCampaign item)
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

                this.OnCampaignCampaignCreated(item);
                this.context.Campaigncampaigns.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Campaigncampaigns.Where(i => i.campaign_id == item.campaign_id);

                

                this.OnAfterCampaignCampaignCreated(item);

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
