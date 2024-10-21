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
    [Route("odata/EspoDbNew/Cases_cases")]
    public partial class Cases_casesController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Cases_casesController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.CasesCase> GetCases_cases()
        {
            var items = this.context.Cases_cases.AsQueryable<EspoNew.Server.Models.EspoDbNew.CasesCase>();
            this.OnCases_casesRead(ref items);

            return items;
        }

        partial void OnCases_casesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CasesCase> items);

        partial void OnCasesCaseGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.CasesCase> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Cases_cases(case_id={case_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.CasesCase> GetCasesCase(string key)
        {
            var items = this.context.Cases_cases.Where(i => i.case_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnCasesCaseGet(ref result);

            return result;
        }
        partial void OnCasesCaseDeleted(EspoNew.Server.Models.EspoDbNew.CasesCase item);
        partial void OnAfterCasesCaseDeleted(EspoNew.Server.Models.EspoDbNew.CasesCase item);

        [HttpDelete("/odata/EspoDbNew/Cases_cases(case_id={case_id})")]
        public IActionResult DeleteCasesCase(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Cases_cases
                    .Where(i => i.case_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Casescase_contacts)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CasesCase>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCasesCaseDeleted(item);
                this.context.Cases_cases.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCasesCaseDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCasesCaseUpdated(EspoNew.Server.Models.EspoDbNew.CasesCase item);
        partial void OnAfterCasesCaseUpdated(EspoNew.Server.Models.EspoDbNew.CasesCase item);

        [HttpPut("/odata/EspoDbNew/Cases_cases(case_id={case_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCasesCase(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.CasesCase item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Cases_cases
                    .Where(i => i.case_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CasesCase>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCasesCaseUpdated(item);
                this.context.Cases_cases.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cases_cases.Where(i => i.case_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,contact,lead");
                this.OnAfterCasesCaseUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Cases_cases(case_id={case_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCasesCase(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.CasesCase> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Cases_cases
                    .Where(i => i.case_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CasesCase>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCasesCaseUpdated(item);
                this.context.Cases_cases.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cases_cases.Where(i => i.case_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,contact,lead");
                this.OnAfterCasesCaseUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCasesCaseCreated(EspoNew.Server.Models.EspoDbNew.CasesCase item);
        partial void OnAfterCasesCaseCreated(EspoNew.Server.Models.EspoDbNew.CasesCase item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.CasesCase item)
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

                this.OnCasesCaseCreated(item);
                this.context.Cases_cases.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cases_cases.Where(i => i.case_id == item.case_id);

                Request.QueryString = Request.QueryString.Add("$expand", "account,contact,lead");

                this.OnAfterCasesCaseCreated(item);

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
