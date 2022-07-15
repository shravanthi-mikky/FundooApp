﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public NoteEntity AddNote(NoteModel node, long UserId);
        public bool DeleteNote(long noteid);
        public NoteEntity UpdateNotes(NoteModel notes, long Noteid);
        public NoteEntity IsPinOrNot(long noteid);
        public NoteEntity IsArchiveOrNot(long noteid);
        public NoteEntity IsTrashOrNot(long noteid);
        public IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid);
        public IEnumerable<NoteEntity> GetAllNotes();
    }
}