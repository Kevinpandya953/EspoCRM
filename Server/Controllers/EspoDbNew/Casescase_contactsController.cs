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
    [Route("odata/EspoDbNew/Casescase_contacts")]
    public partial class Casescase_contactsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Casescase_contactsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> GetCasescase_contacts()
        {
            var items = this.context.Casescase_contacts.AsQueryable<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>();
            this.OnCasescase_contactsRead(ref items);

            return items;
        }

        partial void OnCasescase_contactsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> items);

        partial void OnCasesCaseContactGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Casescase_contacts(case_contact_id={case_contact_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> GetCasesCaseContact(string key)
        {
            var items = this.context.Casescase_contacts.Where(i => i.case_contact_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnCasesCaseContactGet(ref result);

            return result;
        }
        partial void OnCasesCaseContactDeleted(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);
        partial void OnAfterCasesCaseContactDeleted(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);

        [HttpDelete("/odata/EspoDbNew/Casescase_contacts(case_contact_id={case_contact_id})")]
        public IActionResult DeleteCasesCaseContact(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Casescase_contacts
                    .Where(i => i.case_contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCasesCaseContactDeleted(item);
                this.context.Casescase_contacts.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCasesCaseContactDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCasesCaseContactUpdated(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);
        partial void OnAfterCasesCaseContactUpdated(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);

        [HttpPut("/odata/EspoDbNew/Casescase_contacts(case_contact_id={case_contact_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCasesCaseContact(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.CasesCaseContact item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Casescase_contacts
                    .Where(i => i.case_contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCasesCaseContactUpdated(item);
                this.context.Casescase_contacts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Casescase_contacts.Where(i => i.case_contact_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "_case,contact");
                this.OnAfterCasesCaseContactUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Casescase_contacts(case_contact_id={case_contact_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCasesCaseContact(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.CasesCaseContact> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Casescase_contacts
                    .Where(i => i.case_contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.CasesCaseContact>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCasesCaseContactUpdated(item);
                this.context.Casescase_contacts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Casescase_contacts.Where(i => i.case_contact_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "_case,contact");
                this.OnAfterCasesCaseContactUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCasesCaseContactCreated(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);
        partial void OnAfterCasesCaseContactCreated(EspoNew.Server.Models.EspoDbNew.CasesCaseContact item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.CasesCaseContact item)
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

                this.OnCasesCaseContactCreated(item);
                this.context.Casescase_contacts.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Casescase_contacts.Where(i => i.case_contact_id == item.case_contact_id);

                Request.QueryString = Request.QueryString.Add("$expand", "_case,contact");

                this.OnAfterCasesCaseContactCreated(item);

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
