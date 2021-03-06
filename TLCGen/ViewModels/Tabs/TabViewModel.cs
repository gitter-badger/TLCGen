﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLCGen.Models;
using TLCGen.Plugins;

namespace TLCGen.ViewModels
{
    public abstract class TLCGenTabItemViewModel : ViewModelBase, ITLCGenTabItem
    {
        #region Fields

        protected ControllerModel _Controller;
        protected bool _IsEnabled;

        #endregion // Fields

        #region Properties

        public abstract string DisplayName { get; }
        public virtual bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                _IsEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        public virtual System.Windows.DataTemplate ContentDataTemplate { get { return null; } }

        public ControllerModel Controller
        {
            get { return _Controller; }
            set { _Controller = value; }
        }

        public virtual bool SelectedPreview()
        {
            return true;
        }

        public virtual void Selected()
        {
        }

        public virtual bool DeselectedPreview()
        {
            return true;
        }

        public virtual void Deselected()
        {
        }

        public virtual string GetPluginName()
        {
            return "Non named tab";
        }

        public virtual bool CanBeEnabled()
        {
            return true;
        }

        #endregion // Properties

        #region Constructor

        public TLCGenTabItemViewModel(ControllerModel controller)
        {
            _Controller = controller;
        }

        #endregion // Constructor
    }
}
