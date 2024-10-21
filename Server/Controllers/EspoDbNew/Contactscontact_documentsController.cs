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
    [Route("odata/EspoDbNew/Contactscontact_documents")]
    public partial class Contactscontact_documentsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Contactscontact_documentsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> GetContactscontact_documents()
        {
            var items = this.context.Contactscontact_documents.AsQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>();
            this.OnContactscontact_documentsRead(ref items);

            return items;
        }

        partial void OnContactscontact_documentsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> items);

        partial void OnContactsContactDocumentGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Contactscontact_documents(contact_document_id={contact_document_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> GetContactsContactDocument(string key)
        {
            var items = this.context.Contactscontact_documents.Where(i => i.contact_document_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnContactsContactDocumentGet(ref result);

            return result;
        }
        partial void OnContactsContactDocumentDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);
        partial void OnAfterContactsContactDocumentDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);

        [HttpDelete("/odata/EspoDbNew/Contactscontact_documents(contact_document_id={contact_document_id})")]
        public IActionResult DeleteContactsContactDocument(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Contactscontact_documents
                    .Where(i => i.contact_document_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactsContactDocumentDeleted(item);
                this.context.Contactscontact_documents.Remove(item);
                this.context.SaveChanges();
                this.OnAfterContactsContactDocumentDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactsContactDocumentUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);
        partial void OnAfterContactsContactDocumentUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);

        [HttpPut("/odata/EspoDbNew/Contactscontact_documents(contact_document_id={contact_document_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutContactsContactDocument(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Contactscontact_documents
                    .Where(i => i.contact_document_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactsContactDocumentUpdated(item);
                this.context.Contactscontact_documents.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontact_documents.Where(i => i.contact_document_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "contact,document");
                this.OnAfterContactsContactDocumentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Contactscontact_documents(contact_document_id={contact_document_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchContactsContactDocument(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Contactscontact_documents
                    .Where(i => i.contact_document_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContactDocument>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnContactsContactDocumentUpdated(item);
                this.context.Contactscontact_documents.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontact_documents.Where(i => i.contact_document_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "contact,document");
                this.OnAfterContactsContactDocumentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactsContactDocumentCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);
        partial void OnAfterContactsContactDocumentCreated(EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.ContactsContactDocument item)
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

                this.OnContactsContactDocumentCreated(item);
                this.context.Contactscontact_documents.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontact_documents.Where(i => i.contact_document_id == item.contact_document_id);

                Request.QueryString = Request.QueryString.Add("$expand", "contact,document");

                this.OnAfterContactsContactDocumentCreated(item);

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
