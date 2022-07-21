using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        ICollabBL collab;
        public CollabController(ICollabBL collab)
        {
            this.collab = collab;
        }
        [HttpPost("Add")]
        public IActionResult AddCollab(CollabModel collabModel)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "id").Value);
                var result = collab.AddCollab(collabModel.noteid, userid, collabModel.email);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Collaborator Added Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to add" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpDelete("Remove")]
        public IActionResult Remove(CollabModelCollabId collabModelCollabId)
        {
            try
            {
                if (collab.Remove(collabModelCollabId.collabid))
                {
                    return this.Ok(new { Success = true, message = "Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to Delete" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("NoteId")]
        public IEnumerable<CollabEntity> GetAllByNoteID(CollabModelNoteId collabModelNoteId)
        {
            try
            {
                return collab.GetAllByNoteID(collabModelNoteId.noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
