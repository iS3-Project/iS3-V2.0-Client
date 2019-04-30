using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using iS3.Core;

namespace iS3.Unity.EXE
    {
    public partial class UnityPanel : UserControl,IViewHolder
    {
        public U3DViewModel _u3dViewModel;
        public static UnityPanel main;
        public UnityPanel(Project prj, EngineeringMap eMap)
        {

            InitializeComponent();
            main = this;
            _u3dViewModel = new U3DViewModel(this);
            _u3dViewModel.prj = prj;
            _u3dViewModel.eMap = eMap;
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            //AsyncActiveWindow();
        }
        public string GetArguments()
        {
            return "-parentHWND " + panel1.Handle.ToInt32() + " " + Environment.CommandLine;
        }
        public void EnumChildWindows()
        {
            EnumChildWindows(panel1.Handle, WindowEnum, IntPtr.Zero);
        }
        [DllImport("User32.dll")]
        static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);
        [DllImport("user32.dll")]
        internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);


        private Process process;
        private IntPtr unityHWND = IntPtr.Zero;


        private const int WM_ACTIVATE = 0x0006;
        private readonly IntPtr WA_ACTIVE = new IntPtr(1);
        private readonly IntPtr WA_INACTIVE = new IntPtr(0);

        public IView view
        {
            get
            {
                return _u3dViewModel;
            }
        }

        private int WindowEnum(IntPtr hwnd, IntPtr lparam)
        {
            unityHWND = hwnd;
            ActivateUnityWindow();
            return 0;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            MoveWindow(unityHWND, 0, 0, panel1.Width, panel1.Height, true);
            ActivateUnityWindow();
        }

        // Close Unity application
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                process.CloseMainWindow();

                Thread.Sleep(1000);
                while (process.HasExited == false)
                    process.Kill();
            }
            catch (Exception)
            {

            }
        }

        public void ActivateUnityWindow()
        {
            SendMessage(unityHWND, WM_ACTIVATE, WA_ACTIVE, IntPtr.Zero);
        }

        public void DeactivateUnityWindow()
        {
            SendMessage(unityHWND, WM_ACTIVATE, WA_INACTIVE, IntPtr.Zero);
        }

        public void setCoord(string coord)
        {
            
        }

        async void AsyncActiveWindow()
        {
            //for (int i = 0; i < 5; i++)
            //{
            //    MoveWindow(unityHWND, 0, 0, panel1.Width, panel1.Height, true);
            //    ActivateUnityWindow();
            //    await Task.Delay(1000);
            //}
        }
    }
}

