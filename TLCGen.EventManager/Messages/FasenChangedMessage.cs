﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLCGen.Models;

namespace TLCGen.Messaging.Messages
{
    public class FasenChangedMessage
    {
        public List<FaseCyclusModel> Fasen { get; private set; }
        public List<FaseCyclusModel> AddedFasen { get; private set; }
        public List<FaseCyclusModel> RemovedFasen { get; private set; }

        public FasenChangedMessage(List<FaseCyclusModel> fasenlist,
                                   List<FaseCyclusModel> added,
                                   List<FaseCyclusModel> removed)
        {
            Fasen = fasenlist;
            AddedFasen = added;
            RemovedFasen = removed;
        }
    }
}
