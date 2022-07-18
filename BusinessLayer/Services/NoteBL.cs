using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL iNoteRL;
        public NoteBL(INoteRL iNoteRL)
        {
            this.iNoteRL = iNoteRL;
        }

        public NoteEntity AddNote(NoteModel node, long UserId)
        {
            try
            {
                return this.iNoteRL.AddNote(node, UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeleteNote(long noteid)
        {
            try
            {
                return this.iNoteRL.DeleteNote(noteid);

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public NoteEntity UpdateNotes(NoteModel notes, long Noteid)
        {
            try
            {
                return this.iNoteRL.UpdateNotes(notes, Noteid);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public NoteEntity IsPinOrNot(long noteid)
        {
            try
            {
                return this.iNoteRL.IsPinOrNot(noteid);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NoteEntity IsArchiveOrNot(long noteid)
        {
            try
            {
                return this.iNoteRL.IsArchiveOrNot(noteid);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NoteEntity IsTrashOrNot(long noteid)
        {
            try
            {
                return this.iNoteRL.IsTrashOrNot(noteid);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid)
        {
            try
            {
                return this.iNoteRL.GetAllNotesbyuserid(userid);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<NoteEntity> GetAllNotes()
        {
            try
            {
                return this.iNoteRL.GetAllNotes();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public NoteEntity Color(long noteid, string color)
        {
            try
            {
                return this.iNoteRL.Color(noteid,color);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public NoteEntity UploadImage(long noteid, IFormFile img)
        {
            try
            {
                return this.iNoteRL.UploadImage(noteid, img);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
