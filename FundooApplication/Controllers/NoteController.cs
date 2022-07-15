using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        INoteBL noteBL;
        UserContext context;
        public NoteController(INoteBL noteBL, UserContext context)
        {
            this.noteBL = noteBL;
            this.context = context;
        }
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddNotes(NoteModel addnote)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "id").Value);
                //long userid = User.FindFirst(ClaimTypes.UserId).Value;

                var result = noteBL.AddNote(addnote, userid);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Note Added Successfully", result=result});
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to add note" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult DeleteNotes(long noteid)
        {
            try
            {
                if (noteBL.DeleteNote(noteid))
                {
                    return this.Ok(new { Success = true, message = "Note Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to delete note" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("Update")]
        public IActionResult updateNotes(NoteModel addnote, long noteid)
        {
            try
            {
                //long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "Id").Value);
                var result = noteBL.UpdateNotes(addnote, noteid);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Note Updated Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to Update note" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("ByUser")]
        public IEnumerable<NoteEntity> GetAllNotesbyuser(long userid)
        {
            try
            {
                return noteBL.GetAllNotesbyuserid(userid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("AllNotes")]
        public IEnumerable<NoteEntity> GetAllNote()
        {
            try
            {
                return noteBL.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("Pin")]
        public IActionResult Ispinornot(long noteid)
        {
            try
            {
                var result = noteBL.IsPinOrNot(noteid);
                if (result != null)
                {
                    return this.Ok(new { message = "Note unPinned ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Note Pinned Successfully" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("Trash")]
        public IActionResult Istrashornot(long noteid)
        {
            try
            {
                var result = noteBL.IsTrashOrNot(noteid);
                if (result != null)
                {
                    return this.Ok(new { message = "Note Restored", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Note is in Trash" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("Archive")]
        public IActionResult IsArchiveOrNot(long noteid)
        {
            try
            {
                var result = noteBL.IsArchiveOrNot(noteid);
                if (result != null)
                {
                    return this.Ok(new { message = "Note Unarchived ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Note Archived Successfully" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
