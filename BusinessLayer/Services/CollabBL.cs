using BusinessLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        ICollabRL iCollabRL;
        public CollabBL(ICollabRL iCollabRL)
        {
            this.iCollabRL = iCollabRL;
        }
        public CollabEntity AddCollab(long noteid, long userid, string email)
        {
            try
            {
                return this.iCollabRL.AddCollab(noteid, userid, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Remove(long collabid)
        {
            try
            {
                return this.iCollabRL.Remove(collabid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<CollabEntity> GetAllByNoteID(long noteid)
        {
            try
            {
                return this.iCollabRL.GetAllByNoteID(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
