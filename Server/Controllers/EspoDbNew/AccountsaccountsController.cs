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
    [Route("odata/EspoDbNew/Accountsaccounts")]
    public partial class AccountsaccountsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public AccountsaccountsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> GetAccountsaccounts()
        {
            var items = this.context.Accountsaccounts.AsQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccount>();
            this.OnAccountsaccountsRead(ref items);

            return items;
        }

        partial void OnAccountsaccountsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AccountsAccount> items);

        partial void OnAccountsAccountGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.AccountsAccount> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Accountsaccounts(account_id={account_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.AccountsAccount> GetAccountsAccount(string key)
        {
            var items = this.context.Accountsaccounts.Where(i => i.account_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAccountsAccountGet(ref result);

            return result;
        }
        partial void OnAccountsAccountDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);
        partial void OnAfterAccountsAccountDeleted(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);

        [HttpDelete("/odata/EspoDbNew/Accountsaccounts(account_id={account_id})")]
        public IActionResult DeleteAccountsAccount(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Accountsaccounts
                    .Where(i => i.account_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Accountsaccount_contacts)
                    .Include(i => i.Accountsaccount_documents)
                    .Include(i => i.Callscalls)
                    .Include(i => i.Cases_cases)
                    .Include(i => i.Contactscontacts)
                    .Include(i => i.Emailemails)
                    .Include(i => i.Meetingsmeetings)
                    .Include(i => i.Opportunitiesopportunities)
                    .Include(i => i.Taskstasks)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AccountsAccount>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAccountsAccountDeleted(item);
                this.context.Accountsaccounts.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAccountsAccountDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAccountsAccountUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);
        partial void OnAfterAccountsAccountUpdated(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);

        [HttpPut("/odata/EspoDbNew/Accountsaccounts(account_id={account_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAccountsAccount(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.AccountsAccount item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Accountsaccounts
                    .Where(i => i.account_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AccountsAccount>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAccountsAccountUpdated(item);
                this.context.Accountsaccounts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Accountsaccounts.Where(i => i.account_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "employee,campaign");
                this.OnAfterAccountsAccountUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Accountsaccounts(account_id={account_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAccountsAccount(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.AccountsAccount> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Accountsaccounts
                    .Where(i => i.account_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AccountsAccount>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAccountsAccountUpdated(item);
                this.context.Accountsaccounts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Accountsaccounts.Where(i => i.account_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "employee,campaign");
                this.OnAfterAccountsAccountUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAccountsAccountCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);
        partial void OnAfterAccountsAccountCreated(EspoNew.Server.Models.EspoDbNew.AccountsAccount item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.AccountsAccount item)
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

                this.OnAccountsAccountCreated(item);
                this.context.Accountsaccounts.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Accountsaccounts.Where(i => i.account_id == item.account_id);

                Request.QueryString = Request.QueryString.Add("$expand", "employee,campaign");

                this.OnAfterAccountsAccountCreated(item);

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
