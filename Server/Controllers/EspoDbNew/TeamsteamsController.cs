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
    [Route("odata/EspoDbNew/Teamsteams")]
    public partial class TeamsteamsController : ODataController
    {
        private EspoNew.Server.Data.EspoDbNewContext context;

        public TeamsteamsController(EspoNew.Server.Data.EspoDbNewContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<EspoNew.Server.Models.EspoDbNew.TeamsTeam> GetTeamsteams()
        {
            var items = this.context.Teamsteams.AsQueryable<EspoNew.Server.Models.EspoDbNew.TeamsTeam>();
            this.OnTeamsteamsRead(ref items);

            return items;
        }

        partial void OnTeamsteamsRead(ref IQueryable<EspoNew.Server.Models.EspoDbNew.TeamsTeam> items);

        partial void OnTeamsTeamGet(ref SingleResult<EspoNew.Server.Models.EspoDbNew.TeamsTeam> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/EspoDbNew/Teamsteams(team_id={team_id})")]
        public SingleResult<EspoNew.Server.Models.EspoDbNew.TeamsTeam> GetTeamsTeam(string key)
        {
            var items = this.context.Teamsteams.Where(i => i.team_id == Uri.UnescapeDataString(key));
            var result = SingleResult.Create(items);

            OnTeamsTeamGet(ref result);

            return result;
        }
        partial void OnTeamsTeamDeleted(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);
        partial void OnAfterTeamsTeamDeleted(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);

        [HttpDelete("/odata/EspoDbNew/Teamsteams(team_id={team_id})")]
        public IActionResult DeleteTeamsTeam(string key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Teamsteams
                    .Where(i => i.team_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TeamsTeam>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTeamsTeamDeleted(item);
                this.context.Teamsteams.Remove(item);
                this.context.SaveChanges();
                this.OnAfterTeamsTeamDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTeamsTeamUpdated(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);
        partial void OnAfterTeamsTeamUpdated(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);

        [HttpPut("/odata/EspoDbNew/Teamsteams(team_id={team_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutTeamsTeam(string key, [FromBody]EspoNew.Server.Models.EspoDbNew.TeamsTeam item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Teamsteams
                    .Where(i => i.team_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TeamsTeam>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnTeamsTeamUpdated(item);
                this.context.Teamsteams.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Teamsteams.Where(i => i.team_id == Uri.UnescapeDataString(key));
                
                this.OnAfterTeamsTeamUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/EspoDbNew/Teamsteams(team_id={team_id})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchTeamsTeam(string key, [FromBody]Delta<EspoNew.Server.Models.EspoDbNew.TeamsTeam> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Teamsteams
                    .Where(i => i.team_id == Uri.UnescapeDataString(key))
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<EspoNew.Server.Models.EspoDbNew.TeamsTeam>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnTeamsTeamUpdated(item);
                this.context.Teamsteams.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Teamsteams.Where(i => i.team_id == Uri.UnescapeDataString(key));
                
                this.OnAfterTeamsTeamUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnTeamsTeamCreated(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);
        partial void OnAfterTeamsTeamCreated(EspoNew.Server.Models.EspoDbNew.TeamsTeam item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] EspoNew.Server.Models.EspoDbNew.TeamsTeam item)
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

                this.OnTeamsTeamCreated(item);
                this.context.Teamsteams.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Teamsteams.Where(i => i.team_id == item.team_id);

                

                this.OnAfterTeamsTeamCreated(item);

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
