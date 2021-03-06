﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TLCGen.Extensions;
using TLCGen.ViewModels;
using TLCGen.ViewModels.Enums;

namespace TLCGen.Views
{
    /// <summary>
    /// Interaction logic for ConflictMatrixDataGrid.xaml
    /// </summary>
    public partial class ConflictMatrixDataGrid : UserControl
    {
        private DataGridRow _PreviousRow = null;
        private DataGridColumn _PreviousCol = null;
        private Style styleSelectedC;
        private Style styleSelectedR;

        // Using a DependencyProperty as the backing store for ConflictMatrix.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ConflictMatrixProperty =
            DependencyProperty.Register("ConflictMatrix", typeof(Array), typeof(ConflictMatrixDataGrid), new PropertyMetadata(default(Array)));

        public Array ConflictMatrix
        {
            get { return (Array)GetValue(ConflictMatrixProperty); }
            set { SetValue(ConflictMatrixProperty, value); }
        }

        public object GridSelectedItem
        {
            get { return (object)GetValue(GridSelectedItemProperty); }
            set { SetValue(GridSelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridSelectedItemProperty =
            DependencyProperty.Register("GridSelectedItem", typeof(object), typeof(ConflictMatrixDataGrid), new PropertyMetadata(null));

        public ConflictMatrixDataGrid()
        {
            InitializeComponent();

            if (!DesignMode.IsInDesignMode)
            {
                styleSelectedC = new Style();
                styleSelectedC.Setters.Add(new Setter(Border.PaddingProperty, new Thickness(3, 3, 3, 3)));
                styleSelectedC.Setters.Add(new Setter(Grid.BackgroundProperty, new SolidColorBrush(Colors.Beige)));
                styleSelectedC.Setters.Add(new Setter(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Stretch));
                styleSelectedC.Setters.Add(new Setter(TextBox.ForegroundProperty, new SolidColorBrush(Colors.Blue)));
                styleSelectedC.Setters.Add(new Setter(TextBox.PaddingProperty, new Thickness(5, 3, 5, 3)));
                styleSelectedC.Setters.Add(new Setter(TextBox.HorizontalAlignmentProperty, HorizontalAlignment.Center));

                styleSelectedR = new Style();
                styleSelectedR.Setters.Add(new Setter(Border.PaddingProperty, new Thickness(2, 3, 2, 3)));
                styleSelectedR.Setters.Add(new Setter(Grid.BackgroundProperty, new SolidColorBrush(Colors.Beige)));
                styleSelectedC.Setters.Add(new Setter(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Stretch));
                styleSelectedR.Setters.Add(new Setter(TextBox.ForegroundProperty, new SolidColorBrush(Colors.Blue)));
                styleSelectedR.Setters.Add(new Setter(TextBox.PaddingProperty, new Thickness(3, 3, 3, 3)));
                styleSelectedR.Setters.Add(new Setter(TextBox.HorizontalAlignmentProperty, HorizontalAlignment.Center));
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (dg != null)
            {
                if (_PreviousRow != null) _PreviousRow.HeaderStyle = null;
                if (_PreviousCol != null) _PreviousCol.HeaderStyle = null;

                if (e.AddedCells != null && e.AddedCells.Count > 0)
                {
                    var row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromItem(e.AddedCells[0].Item);
                    _PreviousRow = row;
                    var col = e.AddedCells[0].Column as DataGridColumn;
                    _PreviousCol = col;
                    col.HeaderStyle = styleSelectedC;
                    row.HeaderStyle = styleSelectedR;
                }
            editing = false;
            }
        }

        //private void DataGrid2D_Loaded(object sender, RoutedEventArgs e)
        //{
        //    IDisposable d = null;
        //
        //    try
        //    {
        //        d = Dispatcher.DisableProcessing();
        //        Dispatcher.BeginInvoke((Action)(() =>
        //        {
        //            DataGrid dg = sender as DataGrid;
        //            if (dg != null)
        //            {
        //                int count = dg.Columns.Count;
        //                for (int i = 0; i < count; ++i)
        //                {
        //                    dg.GetCell(i, i).IsEnabled = false;
        //                    dg.GetCell(i, i).Background = System.Windows.Media.Brushes.Gray;
        //                }
        //            }
        //        }));
        //    }
        //    finally
        //    {
        //        d.Dispose();
        //    }
        //}

        bool editing = false;
        private void DataGrid2D_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            editing = false;
        }

        private void DataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var dg = sender as DataGrid;
            
            if (dg != null && !dg.IsReadOnly && !editing && 
                !(e.Key == Key.Up ||
                e.Key == Key.Down ||
                e.Key == Key.Left ||
                e.Key == Key.Right ||
                e.Key == Key.Tab ||
                e.Key == Key.Enter ||
                e.Key == Key.Escape
                ))
            {
                if (dg.SelectedCells?.Count > 0)
                {
                    var row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromItem(dg.SelectedCells[0].Item);
                    _PreviousRow = row;
                    var col = dg.SelectedCells[0].Column as DataGridColumn;
                    _PreviousCol = col;
                    DataGridCell cell = dg.GetCell(row.GetIndex(), col.DisplayIndex);
                    dg.BeginEdit();
                }
            }
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            editing = true;
        }
    }

    public class SynchronisatieTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item is SynchronisatieViewModel)
            {
                SynchronisatieViewModel sync = item as SynchronisatieViewModel;
                switch (sync.DisplayType)
                {
                    case ViewModels.Enums.SynchronisatieTypeEnum.Conflict:
                        return ConflictTemplate;
                    case ViewModels.Enums.SynchronisatieTypeEnum.GarantieConflict:
                        return GarantieConflictTemplate;
                    case ViewModels.Enums.SynchronisatieTypeEnum.Naloop:
                        return NaloopTemplate;
                    case ViewModels.Enums.SynchronisatieTypeEnum.Gelijkstart:
                        return GelijkstartTemplate;
                    case ViewModels.Enums.SynchronisatieTypeEnum.Voorstart:
                        return VoorstartTemplate;
                }
            }
            return null;
        }

        public DataTemplate ConflictTemplate { get; set; }
        public DataTemplate GarantieConflictTemplate { get; set; }
        public DataTemplate NaloopTemplate { get; set; }
        public DataTemplate GelijkstartTemplate { get; set; }
        public DataTemplate VoorstartTemplate { get; set; }
    }

    public class SynchronisatieEditingTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item is SynchronisatieViewModel)
            {
                SynchronisatieViewModel sync = item as SynchronisatieViewModel;
                switch (sync.DisplayType)
                {
                    case ViewModels.Enums.SynchronisatieTypeEnum.Conflict:
                        return ConflictTemplate;
                    case ViewModels.Enums.SynchronisatieTypeEnum.GarantieConflict:
                        return GarantieConflictTemplate;
                    case ViewModels.Enums.SynchronisatieTypeEnum.Naloop:
                        return NaloopTemplate;
                    case ViewModels.Enums.SynchronisatieTypeEnum.Gelijkstart:
                        return GelijkstartTemplate;
                    case ViewModels.Enums.SynchronisatieTypeEnum.Voorstart:
                        return VoorstartTemplate;
                }
            }
            return null;
        }

        public DataTemplate ConflictTemplate { get; set; }
        public DataTemplate GarantieConflictTemplate { get; set; }
        public DataTemplate NaloopTemplate { get; set; }
        public DataTemplate GelijkstartTemplate { get; set; }
        public DataTemplate VoorstartTemplate { get; set; }
    }

    public class SynchCheckBoxVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values == null || values.Count() != 2)
                throw new NotImplementedException();

            var DisplayType = (SynchronisatieTypeEnum)values[0];
            var IsEnabled = (bool)values[1];

            switch(DisplayType)
            {
                case SynchronisatieTypeEnum.Conflict:
                case SynchronisatieTypeEnum.GarantieConflict:
                    return Visibility.Hidden;
                default:
                    return IsEnabled ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ViewModelValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SynchronisatieTypeEnum e = (SynchronisatieTypeEnum)parameter;
            switch(e)
            {
                case SynchronisatieTypeEnum.Conflict:
                case SynchronisatieTypeEnum.GarantieConflict:
                    if (value is string)
                        return value as string;
                    else
                        return "";
                case SynchronisatieTypeEnum.Gelijkstart:
                case SynchronisatieTypeEnum.Voorstart:
                case SynchronisatieTypeEnum.Naloop:
                    if (value is bool)
                        return (bool)value;
                    else
                        return false;
                default:
                    throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
