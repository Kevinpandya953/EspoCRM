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
    [Route("odata/EspoDbNew/Accountsaccount_documents")]
    public partial class Accountsaccount_documentsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Accountsaccount_documentsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> GetAccountsaccount_documents()
        {
            var items = this.context.Accountsaccount_documents.AsQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>();
            this.OnAccountsaccount_documentsRead(ref items);

            return items;
        }

        partial void OnAccountsaccount_documentsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> items);

        partial void OnAccountsAccountDocumentGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Accountsaccount_documents(account_document_id={account_document_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> GetAccountsAccountDocument(string key)
        {
            var items = this.context.Accountsaccount_documents.Where(i => i.account_document_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAccountsAccountDocumentGet(ref result);

            return result;
        }
        partial void OnAccountsAccountDocumentDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);
        partial void OnAfterAccountsAccountDocumentDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);

        [HttpDelete("/odata/EspoDbNew/Accountsaccount_documents(account_document_id={account_document_id})")]
        public IActionResult DeleteAccountsAccountDocument(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Accountsaccount_documents
                    .Where(i => i.account_document_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAccountsAccountDocumentDeleted(item);
                this.context.Accountsaccount_documents.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAccountsAccountDocumentDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAccountsAccountDocumentUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);
        partial void OnAfterAccountsAccountDocumentUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);

        [HttpPut("/odata/EspoDbNew/Accountsaccount_documents(account_document_id={account_document_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAccountsAccountDocument(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Accountsaccount_documents
                    .Where(i => i.account_document_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAccountsAccountDocumentUpdated(item);
                this.context.Accountsaccount_documents.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Accountsaccount_documents.Where(i => i.account_document_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,document");
                this.OnAfterAccountsAccountDocumentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Accountsaccount_documents(account_document_id={account_document_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAccountsAccountDocument(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Accountsaccount_documents
                    .Where(i => i.account_document_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAccountsAccountDocumentUpdated(item);
                this.context.Accountsaccount_documents.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Accountsaccount_documents.Where(i => i.account_document_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,document");
                this.OnAfterAccountsAccountDocumentUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAccountsAccountDocumentCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);
        partial void OnAfterAccountsAccountDocumentCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.AccountsAccountDocument item)
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

                this.OnAccountsAccountDocumentCreated(item);
                this.context.Accountsaccount_documents.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Accountsaccount_documents.Where(i => i.account_document_id == item.account_document_id);

                Request.QueryString = Request.QueryString.Add("$expand", "account,document");

                this.OnAfterAccountsAccountDocumentCreated(item);

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
