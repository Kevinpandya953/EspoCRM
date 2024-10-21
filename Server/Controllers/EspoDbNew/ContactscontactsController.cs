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
    [Route("odata/EspoDbNew/Contactscontacts")]
    public partial class ContactscontactsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public ContactscontactsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.ContactsContact> GetContactscontacts()
        {
            var items = this.context.Contactscontacts.AsQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContact>();
            this.OnContactscontactsRead(ref items);

            return items;
        }

        partial void OnContactscontactsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.ContactsContact> items);

        partial void OnContactsContactGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.ContactsContact> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Contactscontacts(contact_id={contact_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.ContactsContact> GetContactsContact(string key)
        {
            var items = this.context.Contactscontacts.Where(i => i.contact_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnContactsContactGet(ref result);

            return result;
        }
        partial void OnContactsContactDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContact item);
        partial void OnAfterContactsContactDeleted(EspoNew.Server.Models.EspoDbNew.ContactsContact item);

        [HttpDelete("/odata/EspoDbNew/Contactscontacts(contact_id={contact_id})")]
        public IActionResult DeleteContactsContact(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Contactscontacts
                    .Where(i => i.contact_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Accountsaccount_contacts)
                    .Include(i => i.Callscall_contacts)
                    .Include(i => i.Cases_cases)
                    .Include(i => i.Casescase_contacts)
                    .Include(i => i.Contactscontact_documents)
                    .Include(i => i.Contactscontact_meetings)
                    .Include(i => i.Contactscontact_opportunities)
                    .Include(i => i.Employeesemployees)
                    .Include(i => i.Opportunitiesopportunities)
                    .Include(i => i.Taskstasks)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContact>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactsContactDeleted(item);
                this.context.Contactscontacts.Remove(item);
                this.context.SaveChanges();
                this.OnAfterContactsContactDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactsContactUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContact item);
        partial void OnAfterContactsContactUpdated(EspoNew.Server.Models.EspoDbNew.ContactsContact item);

        [HttpPut("/odata/EspoDbNew/Contactscontacts(contact_id={contact_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutContactsContact(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.ContactsContact item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Contactscontacts
                    .Where(i => i.contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContact>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnContactsContactUpdated(item);
                this.context.Contactscontacts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontacts.Where(i => i.contact_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,address_city,address_country,address_state,campaign");
                this.OnAfterContactsContactUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Contactscontacts(contact_id={contact_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchContactsContact(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.ContactsContact> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Contactscontacts
                    .Where(i => i.contact_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.ContactsContact>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnContactsContactUpdated(item);
                this.context.Contactscontacts.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontacts.Where(i => i.contact_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "account,address_city,address_country,address_state,campaign");
                this.OnAfterContactsContactUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnContactsContactCreated(EspoNew.Server.Models.EspoDbNew.ContactsContact item);
        partial void OnAfterContactsContactCreated(EspoNew.Server.Models.EspoDbNew.ContactsContact item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.ContactsContact item)
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

                this.OnContactsContactCreated(item);
                this.context.Contactscontacts.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Contactscontacts.Where(i => i.contact_id == item.contact_id);

                Request.QueryString = Request.QueryString.Add("$expand", "account,address_city,address_country,address_state,campaign");

                this.OnAfterContactsContactCreated(item);

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
