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
    [Route("odata/EspoDbNew/Contactscontact_opportunities")]
    public partial class Contactscontact_opportunitiesController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Contactscontact_opportunitiesController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> GetContactscontact_opportunities()
        {
            var items = this.context.Contactscontact_opportunities.AsQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>();
            this.OnContactscontact_opportunitiesRead(ref items);

            return items;
        }

        partial void OnContactscontact_opportunitiesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> items);

        partial void OnContactsContactOpportunityGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Contactscontact_opportunities(contact_opportunity_id={contact_opportunity_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> GetContactsContactOpportunity(string key)
        {
            var items = this.context.Contactscontact_opportunities.Where(i => i.contact_opportunity_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnContactsContactOpportunityGet(ref result);

            return result;
        }
        partial void OnContactsContactOpportunityDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);
        partial void OnAfterContactsContactOpportunityDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);

        [HttpDelete("/odata/EspoDbNew/Contactscontact_opportunities(contact_opportunity_id={contact_opportunity_id})")]
        public IActionResult DeleteContactsContactOpportunity(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Contactscontact_opportunities
                    .Where(i => i.contact_opportunity_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactsContactOpportunityDeleted(item);
                this.context.Contactscontact_opportunities.Remove(item);
                this.context.SaveChanges();
                this.OnAfterContactsContactOpportunityDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactsContactOpportunityUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);
        partial void OnAfterContactsContactOpportunityUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);

        [HttpPut("/odata/EspoDbNew/Contactscontact_opportunities(contact_opportunity_id={contact_opportunity_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutContactsContactOpportunity(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Contactscontact_opportunities
                    .Where(i => i.contact_opportunity_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactsContactOpportunityUpdated(item);
                this.context.Contactscontact_opportunities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontact_opportunities.Where(i => i.contact_opportunity_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "contact,opportunity");
                this.OnAfterContactsContactOpportunityUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Contactscontact_opportunities(contact_opportunity_id={contact_opportunity_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchContactsContactOpportunity(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Contactscontact_opportunities
                    .Where(i => i.contact_opportunity_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnContactsContactOpportunityUpdated(item);
                this.context.Contactscontact_opportunities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontact_opportunities.Where(i => i.contact_opportunity_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "contact,opportunity");
                this.OnAfterContactsContactOpportunityUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactsContactOpportunityCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);
        partial void OnAfterContactsContactOpportunityCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.ContactsContactOpportunity item)
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

                this.OnContactsContactOpportunityCreated(item);
                this.context.Contactscontact_opportunities.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontact_opportunities.Where(i => i.contact_opportunity_id == item.contact_opportunity_id);

                Request.QueryString = Request.QueryString.Add("$expand", "contact,opportunity");

                this.OnAfterContactsContactOpportunityCreated(item);

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
