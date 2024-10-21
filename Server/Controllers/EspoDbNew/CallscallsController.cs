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
    [Route("odata/EspoDbNew/Callscalls")]
    public partial class CallscallsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public CallscallsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.CallsCall> GetCallscalls()
        {
            var items = this.context.Callscalls.AsQueryable<EspoNew.Server.Models.EspoDbNew.CallsCall>();
            this.OnCallscallsRead(ref items);

            return items;
        }

        partial void OnCallscallsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CallsCall> items);

        partial void OnCallsCallGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.CallsCall> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Callscalls(call_id={call_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.CallsCall> GetCallsCall(string key)
        {
            var items = this.context.Callscalls.Where(i => i.call_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnCallsCallGet(ref result);

            return result;
        }
        partial void OnCallsCallDeleted(EspoNew.Server.Models.EspoDbNew.CallsCall item);
        partial void OnAfterCallsCallDeleted(EspoNew.Server.Models.EspoDbNew.CallsCall item);

        [HttpDelete("/odata/EspoDbNew/Callscalls(call_id={call_id})")]
        public IActionResult DeleteCallsCall(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Callscalls
                    .Where(i => i.call_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Callscall_contacts)
                    .Include(i => i.Callscall_leads)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CallsCall>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCallsCallDeleted(item);
                this.context.Callscalls.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCallsCallDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCallsCallUpdated(EspoNew.Server.Models.EspoDbNew.CallsCall item);
        partial void OnAfterCallsCallUpdated(EspoNew.Server.Models.EspoDbNew.CallsCall item);

        [HttpPut("/odata/EspoDbNew/Callscalls(call_id={call_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCallsCall(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.CallsCall item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Callscalls
                    .Where(i => i.call_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CallsCall>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCallsCallUpdated(item);
                this.context.Callscalls.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Callscalls.Where(i => i.call_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account");
                this.OnAfterCallsCallUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Callscalls(call_id={call_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCallsCall(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.CallsCall> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Callscalls
                    .Where(i => i.call_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CallsCall>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCallsCallUpdated(item);
                this.context.Callscalls.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Callscalls.Where(i => i.call_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account");
                this.OnAfterCallsCallUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCallsCallCreated(EspoNew.Server.Models.EspoDbNew.CallsCall item);
        partial void OnAfterCallsCallCreated(EspoNew.Server.Models.EspoDbNew.CallsCall item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.CallsCall item)
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

                this.OnCallsCallCreated(item);
                this.context.Callscalls.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Callscalls.Where(i => i.call_id == item.call_id);

                Request.QueryString = Request.QueryString.Add("$expand", "account");

                this.OnAfterCallsCallCreated(item);

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
