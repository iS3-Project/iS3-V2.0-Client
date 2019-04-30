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
    /// DemoControl.xaml 的交互逻辑
    /// </summary>
    public partial class DemoControl :UserControl, IBaseView
    {
        public  string ViewID { get { return "demoControl"; } }
        public  string ViewName { get { return "这是默认在中间的窗口"; } }
        public  bool DefaultShow { get { return true; } }

        public ViewLocation ViewPos
        {
            get { return ViewLocation.Bottom; }
        }

        public DemoControl()
        {
            InitializeComponent();
            Loaded += DemoControl_Loaded;
        }

        private void DemoControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public bool SetData(params object[] objs)
        {
            throw new NotImplementedException();
        }
    }
}
