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
    [Route("odata/EspoDbNew/Taskstasks")]
    public partial class TaskstasksController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public TaskstasksController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.TasksTask> GetTaskstasks()
        {
            var items = this.context.Taskstasks.AsQueryable<EspoNew.Server.Models.EspoDbNew.TasksTask>();
            this.OnTaskstasksRead(ref items);

            return items;
        }

        partial void OnTaskstasksRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TasksTask> items);

        partial void OnTasksTaskGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.TasksTask> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Taskstasks(task_id={task_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.TasksTask> GetTasksTask(string key)
        {
            var items = this.context.Taskstasks.Where(i => i.task_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnTasksTaskGet(ref result);

            return result;
        }
        partial void OnTasksTaskDeleted(EspoNew.Server.Models.EspoDbNew.TasksTask item);
        partial void OnAfterTasksTaskDeleted(EspoNew.Server.Models.EspoDbNew.TasksTask item);

        [HttpDelete("/odata/EspoDbNew/Taskstasks(task_id={task_id})")]
        public IActionResult DeleteTasksTask(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Taskstasks
                    .Where(i => i.task_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TasksTask>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTasksTaskDeleted(item);
                this.context.Taskstasks.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTasksTaskDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTasksTaskUpdated(EspoNew.Server.Models.EspoDbNew.TasksTask item);
        partial void OnAfterTasksTaskUpdated(EspoNew.Server.Models.EspoDbNew.TasksTask item);

        [HttpPut("/odata/EspoDbNew/Taskstasks(task_id={task_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTasksTask(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.TasksTask item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Taskstasks
                    .Where(i => i.task_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TasksTask>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTasksTaskUpdated(item);
                this.context.Taskstasks.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Taskstasks.Where(i => i.task_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,employee,contact,email");
                this.OnAfterTasksTaskUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Taskstasks(task_id={task_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTasksTask(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.TasksTask> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Taskstasks
                    .Where(i => i.task_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TasksTask>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTasksTaskUpdated(item);
                this.context.Taskstasks.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Taskstasks.Where(i => i.task_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,employee,contact,email");
                this.OnAfterTasksTaskUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTasksTaskCreated(EspoNew.Server.Models.EspoDbNew.TasksTask item);
        partial void OnAfterTasksTaskCreated(EspoNew.Server.Models.EspoDbNew.TasksTask item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.TasksTask item)
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

                this.OnTasksTaskCreated(item);
                this.context.Taskstasks.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Taskstasks.Where(i => i.task_id == item.task_id);

                Request.QueryString = Request.QueryString.Add("$expand", "account,employee,contact,email");

                this.OnAfterTasksTaskCreated(item);

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
