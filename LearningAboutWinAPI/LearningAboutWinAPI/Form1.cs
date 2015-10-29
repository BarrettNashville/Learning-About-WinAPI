using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace LearningAboutWinAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /* http://stackoverflow.com/questions/14108742/manipulating-the-simple-windows-calculator-using-win32-api-in-c/14111246#14111246 */

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var calcWindow = AutomationElement.RootElement.FindFirst(
 TreeScope.Children,
 new PropertyCondition(AutomationElement.NameProperty, "Calculator"));
                if (calcWindow == null) return;

                var sevenButton = calcWindow.FindFirst(TreeScope.Descendants,
                 new PropertyCondition(AutomationElement.NameProperty, "7"));

                var invokePattern = sevenButton.GetCurrentPattern(InvokePattern.Pattern)
                                   as InvokePattern;
                invokePattern.Invoke();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }

        // /* http://blogs.msdn.com/b/thottams/archive/2006/08/11/696013.aspx */
        //public struct STARTUPINFO
        //{
        //    public uint cb;
        //    public string lpReserved;
        //    public string lpDesktop;
        //    public string lpTitle;
        //    public uint dwX;
        //    public uint dwY;
        //    public uint dwXSize;
        //    public uint dwYSize;
        //    public uint dwXCountChars;
        //    public uint dwYCountChars;
        //    public uint dwFillAttribute;
        //    public uint dwFlags;
        //    public short wShowWindow;
        //    public short cbReserved2;
        //    public IntPtr lpReserved2;
        //    public IntPtr hStdInput;
        //    public IntPtr hStdOutput;
        //    public IntPtr hStdError;
        //}

        //public struct PROCESS_INFORMATION
        //{
        //    public IntPtr hProcess;
        //    public IntPtr hThread;
        //    public uint dwProcessId;
        //    public uint dwThreadId;
        //}

        //public struct SECURITY_ATTRIBUTES
        //{
        //    public int length;
        //    public IntPtr lpSecurityDescriptor;
        //    public bool bInheritHandle;
        //}

        //[DllImport("kernel32.dll")]
        //static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
        //                bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment,
        //                string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        ///* http://stackoverflow.com/questions/14962081/click-on-ok-button-of-message-box-using-winapi-in-c-sharp */
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        //// For Windows Mobile, replace user32.dll with coredll.dll
        //[DllImport("user32.dll", SetLastError = true)]
        //static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //[DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, [In] string lpClassName, [In] string lpWindowName);


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        /* Constants:
        //         * http://www.pinvoke.net/default.aspx/Constants.WM
        //         */
        //        const int WM_CLOSE = 0x10;

        //        STARTUPINFO si = new STARTUPINFO();
        //        PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
        //        CreateProcess("C:\\WINDOWS\\SYSTEM32\\Calc.exe", null, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero, null, ref si, out pi);


        //        IntPtr hwnd = FindWindow(null, "Calculator");
        //        //if (hwnd != 0)
        //        SendMessage(hwnd, WM_CLOSE, 0, IntPtr.Zero);

        //        IntPtr nineButton = FindWindowEx(hwnd, new IntPtr(0), "Button", null);

        //        // MessageBox.Show();
        //    }
        //    catch (Exception er)
        //    {
        //        MessageBox.Show(er.Message);
        //    }

        //}
    }
}
