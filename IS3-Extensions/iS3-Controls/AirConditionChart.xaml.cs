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

using iS3.Core;
using System.Security.Permissions;
using System.IO;

namespace DemoControls
{
    /// <summary>
    /// DemoChart.xaml 的交互逻辑
    /// </summary>
    public partial class AirConditionChart : UserControl,IBaseView
    {
        public AirConditionChart()
        {
            InitializeComponent();
            Loaded += AirConditionChart_Loaded;
        }

        public string ViewName { get { return "AirConditionChart"; } }

        public string ViewID { get { return "AirConditionChart"; } }

        public bool DefaultShow { get { return false; } }

        public bool SetData(params object[] objs)
        {
            return true;
        }

        private void AirConditionChart_Loaded(object sender, RoutedEventArgs e)
        {
            Web.ObjectForScripting = new WebAdapter();
            // Host.Children.Add(Web);
            Web.Navigate(new Uri(Directory.GetCurrentDirectory() + "/controls/echarts/AirConditionChart.html"));
        }
    }

}
