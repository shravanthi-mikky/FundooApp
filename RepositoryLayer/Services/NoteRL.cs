using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        UserContext userContext;
        private readonly IConfiguration config;
        public NoteRL(UserContext userContext, IConfiguration config)
        {
            this.userContext = userContext;
            this.config = config;
        }
        
        public NoteEntity AddNote(NoteModel node, long UserId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = node.Title;
                noteEntity.Note = node.Note;
                noteEntity.Color = node.Color;
                noteEntity.Image = node.Image;
                noteEntity.IsArchive = node.IsArchive;
                noteEntity.IsPin = node.IsPin;
                noteEntity.IsTrash = node.IsTrash;
                noteEntity.UserId = UserId;
                noteEntity.Createat = node.Createat;
                noteEntity.Modifiedat = node.Modifiedat;
                userContext.Notes.Add(noteEntity);
                int result = userContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteNote(long noteid)
        {
            try
            {
                var result = this.userContext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                userContext.Remove(result);
                int deletednote = this.userContext.SaveChanges();
                if (deletednote > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity UpdateNotes(NoteModel notes, long noteid)
        {
            try
            {
                NoteEntity result = userContext.Notes.Where(e => e.NoteID == noteid).FirstOrDefault();
                if (result != null)
                {
                    //NoteEntity noteEntity = new NoteEntity();
                    result.Title = notes.Title;
                    result.Note = notes.Note;
                    result.Color = notes.Color;
                    result.Image = notes.Image;
                    result.IsArchive = notes.IsArchive;
                    result.IsPin = notes.IsPin;
                    result.IsTrash = notes.IsTrash;
                    userContext.Notes.Update(result);
                    userContext.SaveChanges();
                    return result;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NoteEntity> GetAllNotes()
        {
            return userContext.Notes.ToList();
        }
        public IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid)
        {
            return userContext.Notes.Where(n => n.UserId == userid).ToList();
        }
        public NoteEntity IsPinOrNot(long noteid)
        {
            try
            {
                NoteEntity result = this.userContext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (result.IsPin == true)
                {
                    result.IsPin = false;
                    this.userContext.SaveChanges();
                    return result;
                }
                result.IsPin = true;
                this.userContext.SaveChanges();
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity IsArchiveOrNot(long noteid)
        {
            try
            {
                NoteEntity result = this.userContext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (result.IsArchive == true)
                {
                    result.IsArchive = false;
                    this.userContext.SaveChanges();
                    return result;
                }
                result.IsArchive = true;
                this.userContext.SaveChanges();
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity IsTrashOrNot(long noteid)
        {
            try
            {
                NoteEntity result = this.userContext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (result.IsTrash == true)
                {
                    result.IsTrash = false;
                    this.userContext.SaveChanges();
                    return result;
                }
                result.IsTrash = true;
                this.userContext.SaveChanges();
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
