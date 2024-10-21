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
    [Route("odata/EspoDbNew/Accountsaccount_contacts")]
    public partial class Accountsaccount_contactsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Accountsaccount_contactsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> GetAccountsaccount_contacts()
        {
            var items = this.context.Accountsaccount_contacts.AsQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>();
            this.OnAccountsaccount_contactsRead(ref items);

            return items;
        }

        partial void OnAccountsaccount_contactsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> items);

        partial void OnAccountsAccountContactGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Accountsaccount_contacts(account_contact_id={account_contact_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> GetAccountsAccountContact(string key)
        {
            var items = this.context.Accountsaccount_contacts.Where(i => i.account_contact_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAccountsAccountContactGet(ref result);

            return result;
        }
        partial void OnAccountsAccountContactDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);
        partial void OnAfterAccountsAccountContactDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);

        [HttpDelete("/odata/EspoDbNew/Accountsaccount_contacts(account_contact_id={account_contact_id})")]
        public IActionResult DeleteAccountsAccountContact(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Accountsaccount_contacts
                    .Where(i => i.account_contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAccountsAccountContactDeleted(item);
                this.context.Accountsaccount_contacts.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAccountsAccountContactDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAccountsAccountContactUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);
        partial void OnAfterAccountsAccountContactUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);

        [HttpPut("/odata/EspoDbNew/Accountsaccount_contacts(account_contact_id={account_contact_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAccountsAccountContact(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Accountsaccount_contacts
                    .Where(i => i.account_contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAccountsAccountContactUpdated(item);
                this.context.Accountsaccount_contacts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Accountsaccount_contacts.Where(i => i.account_contact_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,contact");
                this.OnAfterAccountsAccountContactUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Accountsaccount_contacts(account_contact_id={account_contact_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAccountsAccountContact(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Accountsaccount_contacts
                    .Where(i => i.account_contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AccountsAccountContact>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAccountsAccountContactUpdated(item);
                this.context.Accountsaccount_contacts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Accountsaccount_contacts.Where(i => i.account_contact_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,contact");
                this.OnAfterAccountsAccountContactUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAccountsAccountContactCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);
        partial void OnAfterAccountsAccountContactCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.AccountsAccountContact item)
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

                this.OnAccountsAccountContactCreated(item);
                this.context.Accountsaccount_contacts.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Accountsaccount_contacts.Where(i => i.account_contact_id == item.account_contact_id);

                Request.QueryString = Request.QueryString.Add("$expand", "account,contact");

                this.OnAfterAccountsAccountContactCreated(item);

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
