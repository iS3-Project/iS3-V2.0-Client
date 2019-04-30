using iS3.Core;
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

namespace iS3.Controls
{
    /// <summary>
    /// DemoControlHidden.xaml 的交互逻辑
    /// </summary>
    public partial class DemoControlHidden : UserControl,IBaseView
    {
        public EventHandler<ObjSelectionChangedEventArgs> objSelectionChangedTrigger;
        public DemoControlHidden()
        {
            InitializeComponent();
            Loaded += DemoControlHidden_Loaded;
        }

        private void DemoControlHidden_Loaded(object sender, RoutedEventArgs e)
        {
            Globals.mainframe.dgObjectsSelectionChangedTriggler += DGObjectsSelectionChangedListener;
            objSelectionChangedTrigger += Globals.mainframe.objSelectionChangedListener;
        }
        public void DGObjectsSelectionChangedListener(object serder,DGObjectsSelectionChangedEventArgs e)
        {
            listLB.ItemsSource = e.addedObjs.values;
        }

        public bool SetData(params object[] objs)
        {
            return true;
        }

        public string ViewName { get { return "这是左边的默认不显示的"; } }

        public string ViewID
        {
            get { return "DemoControlHidden"; }
        }

        public bool DefaultShow { get { return false; } }

        public ViewLocation ViewPos { get { return ViewLocation.Left; } }

        DGObject _lastObj;

        private void listLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DGObject selectOne = listLB.SelectedItem as DGObject;
            List<DGObject> addedObjs = new List<DGObject>();
            List<DGObject> removedObjs = new List<DGObject>();
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
