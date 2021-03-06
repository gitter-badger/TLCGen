﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TLCGen.Models.Enumerations;

namespace TLCGen.Models
{
    [Serializable]
    public class NaloopModel
    {
        #region Fields

        #endregion // Fields

        #region Properties

        public string FaseVan { get; set; }
        public string FaseNaar { get; set; }
        public NaloopTypeEnum Type { get; set; }
        public bool DetectieAfhankelijk { get; set; }

        [XmlArrayItem(ElementName = "NaloopDetectoren")]
        public List<NaloopDetectorModel> Detectoren { get; set; }

        [XmlArrayItem(ElementName = "NaloopTijden")]
        public List<NaloopTijdModel> Tijden { get; set; }

        #endregion // Properties

        #region Constructor

        public NaloopModel()
        {
            Detectoren = new List<NaloopDetectorModel>();
            Tijden = new List<NaloopTijdModel>();
        }

        #endregion // Constructor
    }
}
