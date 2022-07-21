﻿using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICollabRL
    {
        public CollabEntity AddCollab(long noteid, long userid, string email);
        public bool Remove(long collabid);
        IEnumerable<CollabEntity> GetAllByNoteID(long noteid);
    }
}
