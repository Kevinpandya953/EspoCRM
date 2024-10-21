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
    [Route("odata/EspoDbNew/Targettarget_lists")]
    public partial class Targettarget_listsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Targettarget_listsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.TargetTargetList> GetTargettarget_lists()
        {
            var items = this.context.Targettarget_lists.AsQueryable<EspoNew.Server.Models.EspoDbNew.TargetTargetList>();
            this.OnTargettarget_listsRead(ref items);

            return items;
        }

        partial void OnTargettarget_listsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TargetTargetList> items);

        partial void OnTargetTargetListGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.TargetTargetList> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Targettarget_lists(target_list_id={target_list_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.TargetTargetList> GetTargetTargetList(string key)
        {
            var items = this.context.Targettarget_lists.Where(i => i.target_list_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnTargetTargetListGet(ref result);

            return result;
        }
        partial void OnTargetTargetListDeleted(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);
        partial void OnAfterTargetTargetListDeleted(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);

        [HttpDelete("/odata/EspoDbNew/Targettarget_lists(target_list_id={target_list_id})")]
        public IActionResult DeleteTargetTargetList(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Targettarget_lists
                    .Where(i => i.target_list_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TargetTargetList>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTargetTargetListDeleted(item);
                this.context.Targettarget_lists.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTargetTargetListDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTargetTargetListUpdated(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);
        partial void OnAfterTargetTargetListUpdated(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);

        [HttpPut("/odata/EspoDbNew/Targettarget_lists(target_list_id={target_list_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTargetTargetList(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.TargetTargetList item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Targettarget_lists
                    .Where(i => i.target_list_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TargetTargetList>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTargetTargetListUpdated(item);
                this.context.Targettarget_lists.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Targettarget_lists.Where(i => i.target_list_id == Uri.UnescapeDataString(key));
                
                this.OnAfterTargetTargetListUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Targettarget_lists(target_list_id={target_list_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTargetTargetList(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.TargetTargetList> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Targettarget_lists
                    .Where(i => i.target_list_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TargetTargetList>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTargetTargetListUpdated(item);
                this.context.Targettarget_lists.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Targettarget_lists.Where(i => i.target_list_id == Uri.UnescapeDataString(key));
                
                this.OnAfterTargetTargetListUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTargetTargetListCreated(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);
        partial void OnAfterTargetTargetListCreated(EspoNew.Server.Models.EspoDbNew.TargetTargetList item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.TargetTargetList item)
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

                this.OnTargetTargetListCreated(item);
                this.context.Targettarget_lists.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Targettarget_lists.Where(i => i.target_list_id == item.target_list_id);

                

                this.OnAfterTargetTargetListCreated(item);

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
