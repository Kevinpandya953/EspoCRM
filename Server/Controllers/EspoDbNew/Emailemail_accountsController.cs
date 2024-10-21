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
    [Route("odata/EspoDbNew/Emailemail_accounts")]
    public partial class Emailemail_accountsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Emailemail_accountsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> GetEmailemail_accounts()
        {
            var items = this.context.Emailemail_accounts.AsQueryable<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>();
            this.OnEmailemail_accountsRead(ref items);

            return items;
        }

        partial void OnEmailemail_accountsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> items);

        partial void OnEmailEmailAccountGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Emailemail_accounts(email_account_id={email_account_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> GetEmailEmailAccount(string key)
        {
            var items = this.context.Emailemail_accounts.Where(i => i.email_account_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnEmailEmailAccountGet(ref result);

            return result;
        }
        partial void OnEmailEmailAccountDeleted(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);
        partial void OnAfterEmailEmailAccountDeleted(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);

        [HttpDelete("/odata/EspoDbNew/Emailemail_accounts(email_account_id={email_account_id})")]
        public IActionResult DeleteEmailEmailAccount(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Emailemail_accounts
                    .Where(i => i.email_account_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmailEmailAccountDeleted(item);
                this.context.Emailemail_accounts.Remove(item);
                this.context.SaveChanges();
                this.OnAfterEmailEmailAccountDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmailEmailAccountUpdated(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);
        partial void OnAfterEmailEmailAccountUpdated(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);

        [HttpPut("/odata/EspoDbNew/Emailemail_accounts(email_account_id={email_account_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEmailEmailAccount(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Emailemail_accounts
                    .Where(i => i.email_account_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmailEmailAccountUpdated(item);
                this.context.Emailemail_accounts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Emailemail_accounts.Where(i => i.email_account_id == Uri.UnescapeDataString(key));
                
                this.OnAfterEmailEmailAccountUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Emailemail_accounts(email_account_id={email_account_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEmailEmailAccount(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Emailemail_accounts
                    .Where(i => i.email_account_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.EmailEmailAccount>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnEmailEmailAccountUpdated(item);
                this.context.Emailemail_accounts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Emailemail_accounts.Where(i => i.email_account_id == Uri.UnescapeDataString(key));
                
                this.OnAfterEmailEmailAccountUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmailEmailAccountCreated(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);
        partial void OnAfterEmailEmailAccountCreated(EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.EmailEmailAccount item)
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

                this.OnEmailEmailAccountCreated(item);
                this.context.Emailemail_accounts.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Emailemail_accounts.Where(i => i.email_account_id == item.email_account_id);

                

                this.OnAfterEmailEmailAccountCreated(item);

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
