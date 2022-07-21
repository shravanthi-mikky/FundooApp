using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class NoteController : ControllerBase
    {
        INoteBL noteBL;
        UserContext context;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBL noteBL, UserContext context, IDistributedCache distributedCache)
        {
            this.noteBL = noteBL;
            this.context = context;
            this.distributedCache = distributedCache;
        }
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddNotes(NoteModel addnote)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "id").Value);

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
        [HttpGet("UserId")]
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

        [HttpGet("RedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NodeList";
            string serializedNotesList;
            var NotesList = new List<NoteEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = await context.Notes.ToListAsync();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
        [Authorize]
        [HttpPut("IsPin")]
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
        [HttpPut("IsTrash")]
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
        [HttpPut("IsArchive")]
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
        [Authorize]
        [HttpPut("Color")]
        public IActionResult Color(long noteid, string color)
        {
            try
            {
                var result = noteBL.Color(noteid, color);
                if (result != null)
                {
                    return this.Ok(new { message = "Color is changed ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Unable to change color" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut("Upload")]
        public IActionResult UploadImage(long noteid, IFormFile img)
        {
            try
            {
                var result = noteBL.UploadImage(noteid, img);
                if (result != null)
                {
                    return this.Ok(new { message = "uploaded ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Not uploaded" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
