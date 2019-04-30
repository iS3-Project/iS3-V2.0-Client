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
using System.Windows.Shapes;
using iS3UnityLib.Model.Event;
using iS3UnityLib.Model.ObjModel;
using IS3.Core;
using Newtonsoft.Json;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TreeListView;

namespace iS3.Unity.EXE
{
    /// <summary>
    /// UnityLayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UnityLayerWindow :UserControl
    {
        public U3DViewModel viewModel;

        public UnityLayerWindow()
        {
            InitializeComponent();
        }
        public void UpdateBinding(LayerTreeNode node)
        {
            this.DataContext = node;
        }

        private void VisibilityBox_OnClick(object sender, RoutedEventArgs e)
        {
            MessageArgs arg = new MessageArgs();
            SetObjStateEvent _event = new SetObjStateEvent(UnityEventType.SetObjStateEvent);
            string s = (sender as CheckBox).Tag.ToString();
            _event.path = s;
            if ((sender as CheckBox).IsChecked == true)
            {
                _event.isVisible = true;
                foreachLayer(((sender as CheckBox).DataContext as LayerTreeNode), ModelLayerSetting.Visibility, true);
            }
            else
            {
                _event.isVisible = false;
                foreachLayer(((sender as CheckBox).DataContext as LayerTreeNode), ModelLayerSetting.Visibility, false);
            }
            
            arg.message = JsonConvert.SerializeObject(_event);
            viewModel.SendMessageListener(this, arg);
        }

        private void TransparentBox_OnClick(object sender, RoutedEventArgs e)
        {
            MessageArgs arg = new MessageArgs();
            
            string s = (sender as CheckBox).Tag.ToString();
            
            if ((sender as CheckBox).IsChecked == true)
            {
                AddTransparentEvent _event = new AddTransparentEvent(UnityEventType.AddTransparentEvent);
                _event.path = s;
                _event.add = true;
                foreachLayer(((sender as CheckBox).DataContext as LayerTreeNode), ModelLayerSetting.Transparent, true);
                arg.message = JsonConvert.SerializeObject(_event);
                viewModel.SendMessageListener(this, arg);
            }
            else
            {
                AddTransparentEvent _event = new AddTransparentEvent(UnityEventType.AddTransparentEvent);
                _event.path = s;
                _event.add = false;
                foreachLayer(((sender as CheckBox).DataContext as LayerTreeNode), ModelLayerSetting.Transparent, false);
                arg.message = JsonConvert.SerializeObject(_event);
                viewModel.SendMessageListener(this, arg);
            }
            //else
            //{
            //    SetRawStateEvent _event = new SetRawStateEvent(UnityEventType.SetRawStateEvent);
            //    _event.path = s;
            //    foreachLayer(((sender as CheckBox).DataContext as LayerTreeNode), ModelLayerSetting.Transparent, false);
            //    arg.message = JsonConvert.SerializeObject(_event);
            //    viewModel.SendMessageListener(this, arg);
            //}
        }
        public enum ModelLayerSetting
        {
            Visibility,Transparent
        }

        void foreachLayer(LayerTreeNode modelLayer, ModelLayerSetting modelLayerSetting, bool value)//循环设置
        {
            if (modelLayerSetting == ModelLayerSetting.Visibility)
            {
                modelLayer.Visibility = value;
            }
            else
            {
                modelLayer.Transparent = value;
            }

            foreach (var item in modelLayer.childNodes)
            {
                foreachLayer(item, modelLayerSetting, value);
            }
        }

        private void ShowObject(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            TreeListViewRow item = this.ContextMenu.GetClickedElement<TreeListViewRow>();
            MessageArgs arg=new MessageArgs();

            ZoomToObjEvent _event = new ZoomToObjEvent(UnityEventType.ZoomToObjEvent);
            
            _event.path = (item.DataContext as LayerTreeNode)?.path;
            string _str = JsonConvert.SerializeObject(_event);
            arg.message = _str;
            viewModel.SendMessageListener(this, arg);
        }

        private void ContextMenu_OnOpened(object sender, RoutedEventArgs e)
        {
            
            TreeListViewRow item = this.ContextMenu.GetClickedElement<TreeListViewRow>();
            if (item != null) 
            {
                LayerList.SelectedItem = (LayerTreeNode)item.DataContext;
            }

        }
    }
}
