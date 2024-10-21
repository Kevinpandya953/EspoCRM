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
    [Route("odata/EspoDbNew/Documentsdocuments")]
    public partial class DocumentsdocumentsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public DocumentsdocumentsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> GetDocumentsdocuments()
        {
            var items = this.context.Documentsdocuments.AsQueryable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>();
            this.OnDocumentsdocumentsRead(ref items);

            return items;
        }

        partial void OnDocumentsdocumentsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> items);

        partial void OnDocumentsDocumentGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Documentsdocuments(document_id={document_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> GetDocumentsDocument(string key)
        {
            var items = this.context.Documentsdocuments.Where(i => i.document_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnDocumentsDocumentGet(ref result);

            return result;
        }
        partial void OnDocumentsDocumentDeleted(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);
        partial void OnAfterDocumentsDocumentDeleted(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);

        [HttpDelete("/odata/EspoDbNew/Documentsdocuments(document_id={document_id})")]
        public IActionResult DeleteDocumentsDocument(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Documentsdocuments
                    .Where(i => i.document_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Accountsaccount_documents)
                    .Include(i => i.Contactscontact_documents)
                    .Include(i => i.Documentsdocument_leads)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDocumentsDocumentDeleted(item);
                this.context.Documentsdocuments.Remove(item);
                this.context.SaveChanges();
                this.OnAfterDocumentsDocumentDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDocumentsDocumentUpdated(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);
        partial void OnAfterDocumentsDocumentUpdated(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);

        [HttpPut("/odata/EspoDbNew/Documentsdocuments(document_id={document_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutDocumentsDocument(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.DocumentsDocument item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Documentsdocuments
                    .Where(i => i.document_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnDocumentsDocumentUpdated(item);
                this.context.Documentsdocuments.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Documentsdocuments.Where(i => i.document_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "employee");
                this.OnAfterDocumentsDocumentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Documentsdocuments(document_id={document_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchDocumentsDocument(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.DocumentsDocument> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Documentsdocuments
                    .Where(i => i.document_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.DocumentsDocument>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnDocumentsDocumentUpdated(item);
                this.context.Documentsdocuments.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Documentsdocuments.Where(i => i.document_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "employee");
                this.OnAfterDocumentsDocumentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnDocumentsDocumentCreated(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);
        partial void OnAfterDocumentsDocumentCreated(EspoNew.Server.Models.EspoDbNew.DocumentsDocument item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.DocumentsDocument item)
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

                this.OnDocumentsDocumentCreated(item);
                this.context.Documentsdocuments.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Documentsdocuments.Where(i => i.document_id == item.document_id);

                Request.QueryString = Request.QueryString.Add("$expand", "employee");

                this.OnAfterDocumentsDocumentCreated(item);

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
