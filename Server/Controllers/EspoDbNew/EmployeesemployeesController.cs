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
    [Route("odata/EspoDbNew/Employeesemployees")]
    public partial class EmployeesemployeesController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public EmployeesemployeesController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> GetEmployeesemployees()
        {
            var items = this.context.Employeesemployees.AsQueryable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>();
            this.OnEmployeesemployeesRead(ref items);

            return items;
        }

        partial void OnEmployeesemployeesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> items);

        partial void OnEmployeesEmployeeGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Employeesemployees(employee_id={employee_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> GetEmployeesEmployee(string key)
        {
            var items = this.context.Employeesemployees.Where(i => i.employee_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnEmployeesEmployeeGet(ref result);

            return result;
        }
        partial void OnEmployeesEmployeeDeleted(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);
        partial void OnAfterEmployeesEmployeeDeleted(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);

        [HttpDelete("/odata/EspoDbNew/Employeesemployees(employee_id={employee_id})")]
        public IActionResult DeleteEmployeesEmployee(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Employeesemployees
                    .Where(i => i.employee_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Accountsaccounts)
                    .Include(i => i.Documentsdocuments)
                    .Include(i => i.Opportunitiesopportunities)
                    .Include(i => i.Taskstasks)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmployeesEmployeeDeleted(item);
                this.context.Employeesemployees.Remove(item);
                this.context.SaveChanges();
                this.OnAfterEmployeesEmployeeDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmployeesEmployeeUpdated(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);
        partial void OnAfterEmployeesEmployeeUpdated(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);

        [HttpPut("/odata/EspoDbNew/Employeesemployees(employee_id={employee_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutEmployeesEmployee(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Employeesemployees
                    .Where(i => i.employee_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnEmployeesEmployeeUpdated(item);
                this.context.Employeesemployees.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Employeesemployees.Where(i => i.employee_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "contact");
                this.OnAfterEmployeesEmployeeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Employeesemployees(employee_id={employee_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchEmployeesEmployee(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Employeesemployees
                    .Where(i => i.employee_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.EmployeesEmployee>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnEmployeesEmployeeUpdated(item);
                this.context.Employeesemployees.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Employeesemployees.Where(i => i.employee_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "contact");
                this.OnAfterEmployeesEmployeeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnEmployeesEmployeeCreated(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);
        partial void OnAfterEmployeesEmployeeCreated(EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.EmployeesEmployee item)
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

                this.OnEmployeesEmployeeCreated(item);
                this.context.Employeesemployees.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Employeesemployees.Where(i => i.employee_id == item.employee_id);

                Request.QueryString = Request.QueryString.Add("$expand", "contact");

                this.OnAfterEmployeesEmployeeCreated(item);

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
