using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using System.Globalization;
using System.Collections;

using iS3.Core;
using System.Reflection;

namespace iS3.Desktop
{
    public class ObjectValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return ObjectHelper.ObjectToString(value, true);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary>
    /// Interaction logic for IS3DataGrid.xaml
    /// </summary>
    /// 
    public partial class IS3DataGrid : UserControl
    {
        public EventHandler<ObjSelectionChangedEventArgs>
            objSelectionChangedTrigger;
        protected int _maxColWith = 300;

        public IS3DataGrid()
        {
            InitializeComponent();
        }
        public void ObjSelectionChangedListener(object sender,ObjSelectionChangedEventArgs e)
        {
            if (LastObjList == null) return;
            //if (e.addedObjs != null)
            //{
            //    foreach (string key in e.addedObjs.Keys)
            //    {
            //        foreach (DGObject obj in e.addedObjs[key])
            //        {
            //            foreach (var data in LastObjList)
            //            {
            //                if ((data as DGObject).Name == obj.Name)
            //                {
            //                    (data as DGObject).IsSelected = true;
            //                }
            //            }
            //        }
            //    }
            //}
            //if (e.removedObjs != null)
            //{
            //    foreach (string key in e.removedObjs.Keys)
            //    {
            //        foreach (DGObject obj in e.removedObjs[key])
            //        {
            //            foreach (var data in LastObjList)
            //            {
            //                if ((data as DGObject).Name == obj.Name)
            //                {
            //                    (data as DGObject).IsSelected = false;
            //                }
            //            }
            //        }
            //    }
            //}

        }
        IEnumerable LastObjList = null;
        public  void DGObjectsSelectionChangedListener(object sender, DGObjectsSelectionChangedEventArgs e)
        {
            DGObjects objs=e.addedObjs;
            string domain = objs.parent.name;
            string objtype = objs.definition.Type;

            //获取对应属性数据
            Type objType = ObjectHelper.GetType(objs.parent.name, objs.definition.Type);
            Type _t = typeof(ObjectHelper);
            MethodInfo mi = _t.GetMethod("Convert").MakeGenericMethod(objType);
            LastObjList = mi.Invoke(null, new object[] { objs.objContainer }) as IEnumerable;
            DGObjectDataGrid.ItemsSource = LastObjList;
        }
        private void DGObjectDataGrid_AutoGeneratingColumn(object sender,
            DataGridAutoGeneratingColumnEventArgs e)
        {
            // "Graphics" and "Attributes" are used internally.
            if (e.PropertyName == "Graphics" ||
                e.PropertyName == "Attributes" ||
                e.PropertyName == "IsSelected" ||
                e.PropertyName == "OBJECTID" ||
                e.PropertyName == "SHAPE" ||
                e.PropertyName == "Shape" ||
                e.PropertyName == "SHAPE_Length" ||
                e.PropertyName == "Shape_Length" ||
                e.PropertyName == "SHAPE_Area" ||
                e.PropertyName == "Shape_Area"
                )
            {
                e.Cancel = true;
                return;
            }

            //DataGridTextColumn tcol = e.Column as DataGridTextColumn;
            //if (tcol == null)
            //    return;

            //// Does the column data type contain the ICollection interface?
            //// If yes, we need the CollectionValueConverter to display data.
            //if (typeof(ICollection).IsAssignableFrom(e.PropertyType))
            //{
            //    Binding binding = tcol.Binding as Binding;
            //    binding.Converter = _objectConverter;
            //}
            //// Is the column data class type other than String?
            //// If yes, we need the ClassValueConverter to display data.
            //else if (e.PropertyType.IsClass && e.PropertyType.Name != "String")
            //{
            //    Binding binding = tcol.Binding as Binding;
            //    binding.Converter = _objectConverter;
            //}
        }

        private void DGObjectDataGrid_AutoGeneratedColumns(object sender,
            EventArgs e)
        {
            if (DGObjectDataGrid.Columns.Count == 0)
                return;

            try
            {
                DataGridColumn col =
                    DGObjectDataGrid.Columns.FirstOrDefault(
                    c => c.Header.ToString() == "ID");
                if (col != null)
                    col.DisplayIndex = 0;

                col = DGObjectDataGrid.Columns.FirstOrDefault(
                    c => c.Header.ToString() == "Name");
                if (col != null)
                    col.DisplayIndex = 1;

                col = DGObjectDataGrid.Columns.FirstOrDefault(
                    c => c.Header.ToString() == "FullName");
                if (col != null)
                    col.DisplayIndex = 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            foreach (DataGridColumn _iter in DGObjectDataGrid.Columns)
            {
                //_iter.MaxWidth = 300;
            }
        }
        DGObject _lastObj = null;
        private void DGObjectDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGObjectDataGrid.IsKeyboardFocusWithin == false)
                return;
            List<DGObject> addedObjs = new List<DGObject>();
            List<DGObject> removedObjs = new List<DGObject>();
            DGObject selectOne = DGObjectDataGrid.SelectedItem as DGObject;
            //select the selected one
            if ((_lastObj != null) && (_lastObj.Name == selectOne.Name)) return;
            addedObjs.Add(selectOne);
            if (_lastObj != null)
            {
                removedObjs.Add(_lastObj);
            }
            if (objSelectionChangedTrigger != null)
            {
                Dictionary<string, IEnumerable<DGObject>> addedObjsDict = null;
                Dictionary<string, IEnumerable<DGObject>> removedObjsDict = null;
                if (addedObjs.Count > 0)
                {
                    addedObjsDict = new Dictionary<string, IEnumerable<DGObject>>();
                    addedObjsDict[selectOne.parent.definition.Name] = addedObjs;
                }
                if (removedObjs.Count > 0)
                {
                    removedObjsDict = new Dictionary<string, IEnumerable<DGObject>>();
                    removedObjsDict[_lastObj.parent.definition.Name] = removedObjs;
                }
                ObjSelectionChangedEventArgs args =
                    new ObjSelectionChangedEventArgs();
                args.addedObjs = addedObjsDict;
                args.removedObjs = removedObjsDict;
                objSelectionChangedTrigger(this, args);
            }
            _lastObj = selectOne;
        }

    }
}
