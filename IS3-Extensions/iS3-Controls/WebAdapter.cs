using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DemoControls
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisible(true)]//给予权限并设置可见
    public class WebAdapter
    {
        public void ShowMsg(string Msg)
        {
            Console.WriteLine(Msg);
            MessageBox.Show(Msg);
        }
    }
}
