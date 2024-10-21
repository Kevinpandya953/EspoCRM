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
    [Route("odata/EspoDbNew/Targettargets")]
    public partial class TargettargetsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public TargettargetsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.TargetTarget> GetTargettargets()
        {
            var items = this.context.Targettargets.AsQueryable<EspoNew.Server.Models.EspoDbNew.TargetTarget>();
            this.OnTargettargetsRead(ref items);

            return items;
        }

        partial void OnTargettargetsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TargetTarget> items);

        partial void OnTargetTargetGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.TargetTarget> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Targettargets(target_id={target_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.TargetTarget> GetTargetTarget(string key)
        {
            var items = this.context.Targettargets.Where(i => i.target_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnTargetTargetGet(ref result);

            return result;
        }
        partial void OnTargetTargetDeleted(EspoNew.Server.Models.EspoDbNew.TargetTarget item);
        partial void OnAfterTargetTargetDeleted(EspoNew.Server.Models.EspoDbNew.TargetTarget item);

        [HttpDelete("/odata/EspoDbNew/Targettargets(target_id={target_id})")]
        public IActionResult DeleteTargetTarget(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Targettargets
                    .Where(i => i.target_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TargetTarget>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTargetTargetDeleted(item);
                this.context.Targettargets.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTargetTargetDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTargetTargetUpdated(EspoNew.Server.Models.EspoDbNew.TargetTarget item);
        partial void OnAfterTargetTargetUpdated(EspoNew.Server.Models.EspoDbNew.TargetTarget item);

        [HttpPut("/odata/EspoDbNew/Targettargets(target_id={target_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTargetTarget(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.TargetTarget item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Targettargets
                    .Where(i => i.target_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TargetTarget>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTargetTargetUpdated(item);
                this.context.Targettargets.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Targettargets.Where(i => i.target_id == Uri.UnescapeDataString(key));
                
                this.OnAfterTargetTargetUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Targettargets(target_id={target_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTargetTarget(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.TargetTarget> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Targettargets
                    .Where(i => i.target_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TargetTarget>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTargetTargetUpdated(item);
                this.context.Targettargets.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Targettargets.Where(i => i.target_id == Uri.UnescapeDataString(key));
                
                this.OnAfterTargetTargetUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTargetTargetCreated(EspoNew.Server.Models.EspoDbNew.TargetTarget item);
        partial void OnAfterTargetTargetCreated(EspoNew.Server.Models.EspoDbNew.TargetTarget item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.TargetTarget item)
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

                this.OnTargetTargetCreated(item);
                this.context.Targettargets.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Targettargets.Where(i => i.target_id == item.target_id);

                

                this.OnAfterTargetTargetCreated(item);

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
