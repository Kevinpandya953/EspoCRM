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
    [Route("odata/EspoDbNew/Addressaddress_cities")]
    public partial class Addressaddress_citiesController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Addressaddress_citiesController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> GetAddressaddress_cities()
        {
            var items = this.context.Addressaddress_cities.AsQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>();
            this.OnAddressaddress_citiesRead(ref items);

            return items;
        }

        partial void OnAddressaddress_citiesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> items);

        partial void OnAddressAddressCityGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Addressaddress_cities(city_id={city_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> GetAddressAddressCity(string key)
        {
            var items = this.context.Addressaddress_cities.Where(i => i.city_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAddressAddressCityGet(ref result);

            return result;
        }
        partial void OnAddressAddressCityDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);
        partial void OnAfterAddressAddressCityDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);

        [HttpDelete("/odata/EspoDbNew/Addressaddress_cities(city_id={city_id})")]
        public IActionResult DeleteAddressAddressCity(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Addressaddress_cities
                    .Where(i => i.city_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Contactscontacts)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAddressAddressCityDeleted(item);
                this.context.Addressaddress_cities.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAddressAddressCityDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAddressAddressCityUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);
        partial void OnAfterAddressAddressCityUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);

        [HttpPut("/odata/EspoDbNew/Addressaddress_cities(city_id={city_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAddressAddressCity(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.AddressAddressCity item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Addressaddress_cities
                    .Where(i => i.city_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAddressAddressCityUpdated(item);
                this.context.Addressaddress_cities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Addressaddress_cities.Where(i => i.city_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "address_state");
                this.OnAfterAddressAddressCityUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Addressaddress_cities(city_id={city_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAddressAddressCity(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.AddressAddressCity> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Addressaddress_cities
                    .Where(i => i.city_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AddressAddressCity>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAddressAddressCityUpdated(item);
                this.context.Addressaddress_cities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Addressaddress_cities.Where(i => i.city_id == Uri.UnescapeDataString(key));
                Request.QueryString = Request.QueryString.Add("$expand", "address_state");
                this.OnAfterAddressAddressCityUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAddressAddressCityCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);
        partial void OnAfterAddressAddressCityCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressCity item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.AddressAddressCity item)
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

                this.OnAddressAddressCityCreated(item);
                this.context.Addressaddress_cities.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Addressaddress_cities.Where(i => i.city_id == item.city_id);

                Request.QueryString = Request.QueryString.Add("$expand", "address_state");

                this.OnAfterAddressAddressCityCreated(item);

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
