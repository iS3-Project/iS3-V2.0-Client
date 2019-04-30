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
using System.Collections.Specialized;
using System.Configuration;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using iS3.Core.Service;
using System.Net;
using System.IO;

namespace iS3.Desktop
{
    /// <summary>
    /// UserLoginPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserLoginPage : UserControl
    {
        private XDocument xml = XDocument.Load(Runtime.configurationPath);

        public UserLoginPage()
        {
            InitializeComponent();

            //读取xml配置到界面
            InitialLogin();
        }
        void InitialLogin()
        {
            DBAddress_TB.Text = xml.Root.Element("ipaddress").Value;
            DBPort_TB.Text = xml.Root.Element("portID").Value;
            ServiceConfig.IP = xml.Root.Element("ipaddress").Value;
            ServiceConfig.PortNum = xml.Root.Element("portID").Value;
        }

        //登陆验证
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginNameTB.Text;
            string password = LoginPasswordTB.Password;

            Globals.oAuthClient = new Core.OAuthClient();
            string token = await Globals.oAuthClient.Call_WebAPI_By_Resource_Owner_Password_Credentials_Grant(username, password);
            if (token != string.Empty)
            {
                App app = Application.Current as App;
                IS3MainWindow mw = (IS3MainWindow)app.MainWindow;
                mw.SwitchToProjectListPage();
            }
            else
            {
                MessageBox.Show("账号或密码错误");
            }
        }
        #region  控件切换逻辑
        private void DBConfig_Click(object sender, RoutedEventArgs e)
        {
            LoginElement.Visibility = Visibility.Hidden;
            DBConfigElement.Visibility = Visibility.Visible;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            hideUI();
        }

        private void Commit_Click(object sender, RoutedEventArgs e)
        {
            xml.Root.Element("ipaddress").SetValue(DBAddress_TB.Text);
            xml.Root.Element("portID").SetValue(DBPort_TB.Text);
            ServiceConfig.IP = xml.Root.Element("ipaddress").Value;
            ServiceConfig.PortNum = xml.Root.Element("portID").Value;
            xml.Save(Runtime.configurationPath);

            hideUI();
        }
        void hideUI()
        {
            LoginElement.Visibility = Visibility.Visible;
            DBConfigElement.Visibility = Visibility.Hidden;
        }
        #endregion
    }
}