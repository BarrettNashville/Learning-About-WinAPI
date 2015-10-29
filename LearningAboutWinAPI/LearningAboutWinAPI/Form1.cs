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
using System.Threading;
using System.Windows.Automation;
using WinAPI;

namespace LearningAboutWinAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /* http://blogs.msdn.com/b/thottams/archive/2006/08/11/696013.aspx */
        public struct STARTUPINFO
        {
            public uint cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        public struct SECURITY_ATTRIBUTES
        {
            public int length;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [DllImport("kernel32.dll")]
        static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
                        bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment,
                        string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);


        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                STARTUPINFO si = new STARTUPINFO();
                PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
                CreateProcess(@"C:\WINDOWS\SYSTEM32\Calc.exe", null, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero,
                    null, ref si, out pi);


                // Find a calculator window
                var win = WindowSearch.GetWindowByText("calc", "", false, true);
                //var win = WindowSearch.GetWindowByText("notepad++", "", false, true);

                // Set it foreground
                WindowManipulation.SetForegroundWindow(win);

                Thread.Sleep(500);
                
                /* Key Constants:
                 * https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx
                 */

                const int aKey = 0x41;
                const int cKey = 0x43;

                MouseKeyboard.PressKey(KeyConstants.VK_NUMPAD6);
                MouseKeyboard.PressKey(KeyConstants.VK_DIVIDE);
                MouseKeyboard.PressKey(KeyConstants.VK_NUMPAD0);
                MouseKeyboard.PressKey(KeyConstants.VK_RETURN);

                Thread.Sleep(500);

                MouseKeyboard.PressKey(cKey, false, false, true);

                listBox1.Items.Insert(0, Clipboard.GetText());

                Clipboard.Clear();


            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

    }
}
