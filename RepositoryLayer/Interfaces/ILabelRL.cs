﻿using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public LabelEntity Addlabel(long noteid, long userid, string label);
        public IEnumerable<LabelEntity> GetlabelsByNoteid(long noteid, long userid);
        public bool RemoveLabel(long userID, string labelName);
        public IEnumerable<LabelEntity> RenameLabel(long userID, string oldLabelName, string labelName);

    }
}
