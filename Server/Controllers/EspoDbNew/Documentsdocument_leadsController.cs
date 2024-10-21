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
    [Route("odata/EspoDbNew/Documentsdocument_leads")]
    public partial class Documentsdocument_leadsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Documentsdocument_leadsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> GetDocumentsdocument_leads()
        {
            var items = this.context.Documentsdocument_leads.AsQueryable<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>();
            this.OnDocumentsdocument_leadsRead(ref items);

            return items;
        }

        partial void OnDocumentsdocument_leadsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> items);

        partial void OnDocumentsDocumentLeadGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Documentsdocument_leads(document_lead_id={document_lead_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> GetDocumentsDocumentLead(string key)
        {
            var items = this.context.Documentsdocument_leads.Where(i => i.document_lead_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnDocumentsDocumentLeadGet(ref result);

            return result;
        }
        partial void OnDocumentsDocumentLeadDeleted(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);
        partial void OnAfterDocumentsDocumentLeadDeleted(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);

        [HttpDelete("/odata/EspoDbNew/Documentsdocument_leads(document_lead_id={document_lead_id})")]
        public IActionResult DeleteDocumentsDocumentLead(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Documentsdocument_leads
                    .Where(i => i.document_lead_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDocumentsDocumentLeadDeleted(item);
                this.context.Documentsdocument_leads.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDocumentsDocumentLeadDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDocumentsDocumentLeadUpdated(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);
        partial void OnAfterDocumentsDocumentLeadUpdated(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);

        [HttpPut("/odata/EspoDbNew/Documentsdocument_leads(document_lead_id={document_lead_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDocumentsDocumentLead(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Documentsdocument_leads
                    .Where(i => i.document_lead_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDocumentsDocumentLeadUpdated(item);
                this.context.Documentsdocument_leads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Documentsdocument_leads.Where(i => i.document_lead_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "document,lead");
                this.OnAfterDocumentsDocumentLeadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Documentsdocument_leads(document_lead_id={document_lead_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDocumentsDocumentLead(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Documentsdocument_leads
                    .Where(i => i.document_lead_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDocumentsDocumentLeadUpdated(item);
                this.context.Documentsdocument_leads.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Documentsdocument_leads.Where(i => i.document_lead_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "document,lead");
                this.OnAfterDocumentsDocumentLeadUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDocumentsDocumentLeadCreated(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);
        partial void OnAfterDocumentsDocumentLeadCreated(EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.DocumentsDocumentLead item)
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

                this.OnDocumentsDocumentLeadCreated(item);
                this.context.Documentsdocument_leads.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Documentsdocument_leads.Where(i => i.document_lead_id == item.document_lead_id);

                Request.QueryString = Request.QueryString.Add("$expand", "document,lead");

                this.OnAfterDocumentsDocumentLeadCreated(item);

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
