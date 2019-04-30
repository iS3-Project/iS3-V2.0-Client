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
    public partial class LineChart : UserControl,IBaseView
    {
        public LineChart()
        {
            InitializeComponent();
            Loaded += LineChart_Loaded;
        }

        public string ViewName { get { return "LineChart"; } }

        public string ViewID { get { return "LineChart"; } }

        public bool DefaultShow { get { return false; } }

        public bool SetData(params object[] objs)
        {
            Web.InvokeScript("jsPushData",
                "未来一周气温变化", 
                new string[2] { "最高气温", "最低气温" },
                new string[7] {"周一", "周二", "周三", "周四", "周五", "周六", "周日"},
                "'{value} °C'",
                new List<object>()
                {
                    new 
                    {
                        name= "最高气温",
                        type ="line",
                        data=new int[7]{11, 11, 15, 13, 12, 13, 10 },
                    },
                    new
                    {
                        name= "最低气温",
                        type ="line",
                        data=new int[7]{1, -2, 2, 5, 3, 2, 0},
                    }
                }
                );
            return true;
        }

        private void LineChart_Loaded(object sender, RoutedEventArgs e)
        {
            Web.ObjectForScripting = new WebAdapter();
            // Host.Children.Add(Web);
            Web.Navigate(new Uri(Directory.GetCurrentDirectory() + "/controls/echarts/LineChart.html"));
            //SetData();
            Web.InvokeScript("Type", 1);
        }
    }

}
