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
    [Route("odata/EspoDbNew/Addressaddress_countries")]
    public partial class Addressaddress_countriesController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public Addressaddress_countriesController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> GetAddressaddress_countries()
        {
            var items = this.context.Addressaddress_countries.AsQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>();
            this.OnAddressaddress_countriesRead(ref items);

            return items;
        }

        partial void OnAddressaddress_countriesRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> items);

        partial void OnAddressAddressCountryGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Addressaddress_countries(country_id={country_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> GetAddressAddressCountry(string key)
        {
            var items = this.context.Addressaddress_countries.Where(i => i.country_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnAddressAddressCountryGet(ref result);

            return result;
        }
        partial void OnAddressAddressCountryDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);
        partial void OnAfterAddressAddressCountryDeleted(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);

        [HttpDelete("/odata/EspoDbNew/Addressaddress_countries(country_id={country_id})")]
        public IActionResult DeleteAddressAddressCountry(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Addressaddress_countries
                    .Where(i => i.country_id == Uri.UnescapeDataString(key))
                    .Include(i => i.Addressaddress_states)
                    .Include(i => i.Contactscontacts)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAddressAddressCountryDeleted(item);
                this.context.Addressaddress_countries.Remove(item);
                this.context.SaveChanges();
                this.OnAfterAddressAddressCountryDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAddressAddressCountryUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);
        partial void OnAfterAddressAddressCountryUpdated(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);

        [HttpPut("/odata/EspoDbNew/Addressaddress_countries(country_id={country_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutAddressAddressCountry(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Addressaddress_countries
                    .Where(i => i.country_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnAddressAddressCountryUpdated(item);
                this.context.Addressaddress_countries.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Addressaddress_countries.Where(i => i.country_id == Uri.UnescapeDataString(key));
                
                this.OnAfterAddressAddressCountryUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Addressaddress_countries(country_id={country_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchAddressAddressCountry(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Addressaddress_countries
                    .Where(i => i.country_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.AddressAddressCountry>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnAddressAddressCountryUpdated(item);
                this.context.Addressaddress_countries.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Addressaddress_countries.Where(i => i.country_id == Uri.UnescapeDataString(key));
                
                this.OnAfterAddressAddressCountryUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnAddressAddressCountryCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);
        partial void OnAfterAddressAddressCountryCreated(EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.AddressAddressCountry item)
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

                this.OnAddressAddressCountryCreated(item);
                this.context.Addressaddress_countries.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Addressaddress_countries.Where(i => i.country_id == item.country_id);

                

                this.OnAfterAddressAddressCountryCreated(item);

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
