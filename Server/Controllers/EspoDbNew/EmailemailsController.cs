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
    [Route("odata/EspoDbNew/Emailemails")]
    public partial class EmailemailsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public EmailemailsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.EmailEmail> GetEmailemails()
        {
            var items = this.context.Emailemails.AsQueryable<EspoNew.Server.Models.EspoDbNew.EmailEmail>();
            this.OnEmailemailsRead(ref items);

            return items;
        }

        partial void OnEmailemailsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.EmailEmail> items);

        partial void OnEmailEmailGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.EmailEmail> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Emailemails(email_id={email_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.EmailEmail> GetEmailEmail(string key)
        {
            var items = this.context.Emailemails.Where(i => i.email_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnEmailEmailGet(ref result);

            return result;
        }
        partial void OnEmailEmailDeleted(EspoNew.Server.Models.EspoDbNew.EmailEmail item);
        partial void OnAfterEmailEmailDeleted(EspoNew.Server.Models.EspoDbNew.EmailEmail item);

        [HttpDelete("/odata/EspoDbNew/Emailemails(email_id={email_id})")]
        public IActionResult DeleteEmailEmail(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Emailemails
                    .Where(i => i.email_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Taskstasks)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.EmailEmail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmailEmailDeleted(item);
                this.context.Emailemails.Remove(item);
                this.context.SaveChanges();
                this.OnAfterEmailEmailDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmailEmailUpdated(EspoNew.Server.Models.EspoDbNew.EmailEmail item);
        partial void OnAfterEmailEmailUpdated(EspoNew.Server.Models.EspoDbNew.EmailEmail item);

        [HttpPut("/odata/EspoDbNew/Emailemails(email_id={email_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEmailEmail(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.EmailEmail item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Emailemails
                    .Where(i => i.email_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.EmailEmail>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmailEmailUpdated(item);
                this.context.Emailemails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Emailemails.Where(i => i.email_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account");
                this.OnAfterEmailEmailUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Emailemails(email_id={email_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEmailEmail(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.EmailEmail> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Emailemails
                    .Where(i => i.email_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.EmailEmail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnEmailEmailUpdated(item);
                this.context.Emailemails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Emailemails.Where(i => i.email_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account");
                this.OnAfterEmailEmailUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmailEmailCreated(EspoNew.Server.Models.EspoDbNew.EmailEmail item);
        partial void OnAfterEmailEmailCreated(EspoNew.Server.Models.EspoDbNew.EmailEmail item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.EmailEmail item)
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

                this.OnEmailEmailCreated(item);
                this.context.Emailemails.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Emailemails.Where(i => i.email_id == item.email_id);

                Request.QueryString = Request.QueryString.Add("$expand", "account");

                this.OnAfterEmailEmailCreated(item);

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
