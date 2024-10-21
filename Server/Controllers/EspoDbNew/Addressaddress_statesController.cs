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
    [Route("odata/EspoDbNew/Addressaddress_states")]
    public partial class Addressaddress_statesController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Addressaddress_statesController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressState> GetAddressaddress_states()
        {
            var items = this.context.Addressaddress_states.AsQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressState>();
            this.OnAddressaddress_statesRead(ref items);

            return items;
        }

        partial void OnAddressaddress_statesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressState> items);

        partial void OnAddressAddressStateGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.AddressAddressState> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Addressaddress_states(state_id={state_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.AddressAddressState> GetAddressAddressState(string key)
        {
            var items = this.context.Addressaddress_states.Where(i => i.state_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAddressAddressStateGet(ref result);

            return result;
        }
        partial void OnAddressAddressStateDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);
        partial void OnAfterAddressAddressStateDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);

        [HttpDelete("/odata/EspoDbNew/Addressaddress_states(state_id={state_id})")]
        public IActionResult DeleteAddressAddressState(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Addressaddress_states
                    .Where(i => i.state_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Addressaddress_cities)
                    .Include(i => i.Contactscontacts)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AddressAddressState>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAddressAddressStateDeleted(item);
                this.context.Addressaddress_states.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAddressAddressStateDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAddressAddressStateUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);
        partial void OnAfterAddressAddressStateUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);

        [HttpPut("/odata/EspoDbNew/Addressaddress_states(state_id={state_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAddressAddressState(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.AddressAddressState item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Addressaddress_states
                    .Where(i => i.state_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AddressAddressState>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAddressAddressStateUpdated(item);
                this.context.Addressaddress_states.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Addressaddress_states.Where(i => i.state_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "address_country");
                this.OnAfterAddressAddressStateUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Addressaddress_states(state_id={state_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAddressAddressState(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.AddressAddressState> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Addressaddress_states
                    .Where(i => i.state_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AddressAddressState>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAddressAddressStateUpdated(item);
                this.context.Addressaddress_states.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Addressaddress_states.Where(i => i.state_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "address_country");
                this.OnAfterAddressAddressStateUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAddressAddressStateCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);
        partial void OnAfterAddressAddressStateCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressState item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.AddressAddressState item)
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

                this.OnAddressAddressStateCreated(item);
                this.context.Addressaddress_states.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Addressaddress_states.Where(i => i.state_id == item.state_id);

                Request.QueryString = Request.QueryString.Add("$expand", "address_country");

                this.OnAfterAddressAddressStateCreated(item);

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
