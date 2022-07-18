using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        public NoteEntity AddNote(NoteModel node, long UserId);
        public bool DeleteNote(long noteid);
        public NoteEntity UpdateNotes(NoteModel notes, long Noteid);
        public NoteEntity IsPinOrNot(long noteid);
        public NoteEntity IsArchiveOrNot(long noteid);
        public NoteEntity IsTrashOrNot(long noteid);
        public IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid);
        public IEnumerable<NoteEntity> GetAllNotes();
        public NoteEntity Color(long noteid, string color);
        public NoteEntity UploadImage(long noteid, IFormFile img);
    }
}
