using Elastic.Apm.Config;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        private readonly UserContext context;
        public LabelRL(UserContext context)
        {
            this.context = context;
        }
        public LabelEntity Addlabel(long noteid, long userid, string label)
        {
            try
            {
                LabelEntity Entity = new LabelEntity();
                Entity.LabelName = label;
                Entity.Userid = userid;
                Entity.Noteid = noteid;
                this.context.Label.Add(Entity);
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return Entity;
                }
                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<LabelEntity> GetlabelsByNoteid(long noteid, long userid)
        {
            return context.Label.Where(e => e.Noteid == noteid && e.Userid == userid).ToList();
        }
        public bool RemoveLabel(long userID, string labelName)
        {
            try
            {
                var result = this.context.Label.FirstOrDefault(x => x.Userid == userID && x.LabelName == labelName);
                if (result != null)
                {
                    context.Remove(result);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> RenameLabel(long userID, string oldLabelName, string labelName)
        {
            IEnumerable<LabelEntity> labels;
            labels = context.Label.Where(x => x.Userid == userID && x.LabelName == oldLabelName).ToList();
            if (labels != null)
            {
                foreach (var newlabel in labels)
                {
                    newlabel.LabelName = labelName;
                }
                context.SaveChanges();
                return labels;
            }
            return null;
        }
    }
}
