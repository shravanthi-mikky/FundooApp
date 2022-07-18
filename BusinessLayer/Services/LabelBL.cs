using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL:ILabelBL
    {
        ILabelRL iLabelRL;
        public LabelBL(ILabelRL iLabelRL)
        {
            this.iLabelRL = iLabelRL;
        }

        public LabelEntity Addlabel(long noteid, long userid, string labels)
        {
            try
            {
                return this.iLabelRL.Addlabel(noteid, userid, labels);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> GetlabelsByNoteid(long noteid, long userid)
        {
            try
            {
                return this.iLabelRL.GetlabelsByNoteid(noteid, userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool RemoveLabel(long userID, string labelName)
        {
            try
            {
                return this.iLabelRL.RemoveLabel(userID, labelName);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public IEnumerable<LabelEntity> RenameLabel(long userID, string oldLabelName, string labelName)
        {
            try
            {
                return this.iLabelRL.RenameLabel(userID, oldLabelName, labelName);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
