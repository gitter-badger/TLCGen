﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLCGen.Models.Enumerations;
using TLCGen.Models;
using TLCGen.DataAccess;

namespace TLCGen.ViewModels
{
    public class DetectorViewModel : ViewModelBase, IComparable
    {
        #region Fields

        private DetectorModel _Detector;
        private FaseCyclusViewModel _FaseVM;
        private ControllerViewModel _ControllerVM;

        #endregion // Fields

        #region Properties

        public DetectorModel Detector
        {
            get { return _Detector; }
        }

        public FaseCyclusViewModel FaseVM
        {
            get { return _FaseVM; }
            set { _FaseVM = value; }
        }

        public string FaseCyclus
        {
            get { return FaseVM?.Naam; }
        }

        public string Naam
        {
            get { return _Detector.Naam; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && _ControllerVM.IsElementNaamUnique(value))
                {
                    _Detector.Naam = value;
                }
                OnMonitoredPropertyChanged("Naam", _ControllerVM);
            }
        }

        public string Define
        {
            get { return _Detector.Define; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && _ControllerVM.IsElementDefineUnique(value))
                {
                    string oldname = _Detector.Define;
                    _Detector.Naam = value.Replace(SettingsProvider.GetDetectorDefinePrefix(), "");
                    _Detector.Define = value;
                }
                OnMonitoredPropertyChanged("Define", _ControllerVM);
                OnMonitoredPropertyChanged("Naam", _ControllerVM);
            }
        }

        public DetectorTypeEnum Type
        {
            get { return _Detector.Type; }
            set
            {
                _Detector.Type = value;
                OnMonitoredPropertyChanged("Type", _ControllerVM);
                FaseVM?.UpdateHasKopmax();
            }
        }

        public int? TDB
        {
            get { return _Detector.TDB; }
            set
            {
                if (value == null || value >= 0)
                    _Detector.TDB = value;
                OnMonitoredPropertyChanged("TDB", _ControllerVM);
            }
        }

        public int? TDH
        {
            get { return _Detector.TDH; }
            set
            {
                if (value == null || value >= 0)
                    _Detector.TDH = value;
                OnMonitoredPropertyChanged("TDH", _ControllerVM);
            }
        }

        public int? TOG
        {
            get { return _Detector.TOG; }
            set
            {
                if (value == null || value >= 0)
                    _Detector.TOG = value;
                OnMonitoredPropertyChanged("TOG", _ControllerVM);
            }
        }

        public int? TBG
        {
            get { return _Detector.TBG; }
            set
            {
                if (value == null || value >= 0)
                    _Detector.TBG = value;
                OnMonitoredPropertyChanged("TBG", _ControllerVM);
            }
        }

        public DetectorAanvraagTypeEnum Aanvraag
        {
            get { return _Detector.Aanvraag; }
            set
            {
                _Detector.Aanvraag = value;
                OnMonitoredPropertyChanged("Aanvraag", _ControllerVM);
            }
        }

        public DetectorVerlengenTypeEnum Verlengen
        {
            get { return _Detector.Verlengen; }
            set
            {
                _Detector.Verlengen = value;
                OnMonitoredPropertyChanged("Verlengen", _ControllerVM);
            }
        }

        public int Q1
        {
            get { return _Detector.Simulatie.Q1; }
            set
            {
                _Detector.Simulatie.Q1 = value;
                OnMonitoredPropertyChanged("Q1", _ControllerVM);
            }
        }

        public int Q2
        {
            get { return _Detector.Simulatie.Q2; }
            set
            {
                _Detector.Simulatie.Q2 = value;
                OnMonitoredPropertyChanged("Q2", _ControllerVM);
            }
        }

        public int Q3
        {
            get { return _Detector.Simulatie.Q3; }
            set
            {
                _Detector.Simulatie.Q3 = value;
                OnMonitoredPropertyChanged("Q3", _ControllerVM);
            }
        }

        public int Q4
        {
            get { return _Detector.Simulatie.Q4; }
            set
            {
                _Detector.Simulatie.Q4 = value;
                OnMonitoredPropertyChanged("Q4", _ControllerVM);
            }
        }

        public int Stopline
        {
            get { return _Detector.Simulatie.Stopline; }
            set
            {
                _Detector.Simulatie.Stopline = value;
                OnMonitoredPropertyChanged("Stopline", _ControllerVM);
            }
        }

        public string FCNr
        {
            get { return _Detector.Simulatie.FCNr; }
            set
            {
                _Detector.Simulatie.FCNr = value;
                OnMonitoredPropertyChanged("FCNr", _ControllerVM);
            }
        }

        public bool IsLooseDetector
        {
            get
            {
                return FaseVM == null;
            }
        }

        #endregion // Properties

        #region IComparable

        public int CompareTo(object obj)
        {
            DetectorViewModel fcvm = obj as DetectorViewModel;
            if (fcvm == null)
                throw new NotImplementedException();
            else
            {
                string myName = Naam;
                string hisName = fcvm.Naam;
                if (myName.Length < hisName.Length) myName = myName.PadLeft(hisName.Length, '0');
                else if (hisName.Length < myName.Length) hisName = hisName.PadLeft(myName.Length, '0');
                return myName.CompareTo(hisName);
            }
        }

        #endregion // IComparable

        #region Constructor

        public DetectorViewModel(ControllerViewModel controllervm, DetectorModel detector)
        {
            _ControllerVM = controllervm;
            _Detector = detector;

        }

        #endregion // Constructor
    }
}
