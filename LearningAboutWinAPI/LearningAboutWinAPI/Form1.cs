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

        // Structs for CreateProcess()
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

        // CreateProcess()
        [DllImport("kernel32.dll")]
        static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes,
                        bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment,
                        string lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        // FindWindow()
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // FindWindowEx()
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        // SendMessage()
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, StringBuilder lParam);

        // SendMessage() for button click
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, IntPtr lParam);

        // GetWindowText()
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        // GetWindowTextLength()
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        //RedrawWindow()
        /* https://msdn.microsoft.com/en-us/library/windows/desktop/dd162911(v=vs.85).aspx */
        [DllImport("user32.dll")]
        public static extern Boolean RedrawWindow(IntPtr hWnd, IntPtr lpRectUpdate, IntPtr hrgnUpdate, UInt32 flags);

        // WinAPI RECT struct
        public struct Rect
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        };

        // RedrawWindow() flags
        public enum RedrawWindowFlags
        {
            RDW_INVALIDATE = 0x0001,
            RDW_INTERNALPAINT = 0x0002,
            RDW_ERASE = 0x0004,

            RDW_VALIDATE = 0x0008,
            RDW_NOINTERNALPAINT = 0x0010,
            RDW_NOERASE = 0x0020,

            RDW_NOCHILDREN = 0x0040,
            RDW_ALLCHILDREN = 0x0080,

            RDW_UPDATENOW = 0x0100,
            RDW_ERASENOW = 0x0200,

            RDW_FRAME = 0x0400,
            RDW_NOFRAME = 0x0800
        };

        public static IntPtr NULL = (IntPtr)0;

        // GetComboBoxInfo()
        /* http://www.cyotek.com/blog/getting-the-hwnd-of-the-edit-component-within-a-combobox-control */
        [DllImport("user32.dll")]
        public static extern bool GetComboBoxInfo(IntPtr hWnd, ref COMBOBOXINFO pcbi);

        // Structs for GetComboBoxInfo()
        [StructLayout(LayoutKind.Sequential)]
        public struct COMBOBOXINFO
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public int stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                /* ATTEMPT TO OPEN APP */
                STARTUPINFO si = new STARTUPINFO();
                PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
                CreateProcess(@"MathApp.exe", null, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero,
                    null, ref si, out pi);
                /* SUCCESSFULLY OPENED APP */
                
                //evidently, you've got to sleep for a litle bit before the FindWindow() method will find the new window
                Thread.Sleep(100);
                /* ATTEMP TO RETRIEVE HWND OF APP */
                var win = FindWindow(null, "Math App");
                //MessageBox.Show(win.ToString("X8"));
                /* SUCCESSFULLY RETRIEVED HWND OF APP */

                /* ATTEMPT TO RETRIEVE HWND OF ANSWER LABEL */
                var lbl = FindWindowEx(win, IntPtr.Zero, null, "Answer");
                //MessageBox.Show(lbl.ToString("X8"));
                /* SUCCESSFULLY RETRIEVED HWND OF ANSWER LABEL */

                /* ATTEMPT TO RETRIEVE TEXT OF ANSWER LABEL */
                const int WM_GETTEXT = 0x000D;
                StringBuilder getText = new StringBuilder(256);  // or length from call with GETTEXTLENGTH
                SendMessage(lbl, WM_GETTEXT, getText.Capacity, getText);
                //MessageBox.Show(getText.ToString());
                /* SUCCESSFULLY RETRIEVED TEXT OF ANSWER LABEL */

                /* ATTEMPT TO SET NEW TEXT OF ANSWER LABEL */
                const int WM_SETTEXT = 0x000C;
                StringBuilder setText = new StringBuilder(256);
                setText.Append("New Answer");
                SendMessage(lbl, WM_SETTEXT, setText.MaxCapacity, setText);

                StringBuilder getNewText = new StringBuilder(256);
                SendMessage(lbl, WM_GETTEXT, getNewText.Capacity, getNewText);
                //MessageBox.Show(getNewText.ToString());
                /* SUCCESSFULLY SET AND RETRIEVED NEW TEXT OF ANSWER LABEL */

                /* ATTEMPT TO REDRAW WINDOW */
                RedrawWindow(win, NULL, NULL, (UInt32)(RedrawWindowFlags.RDW_FRAME | RedrawWindowFlags.RDW_INVALIDATE | RedrawWindowFlags.RDW_UPDATENOW | RedrawWindowFlags.RDW_ALLCHILDREN));
                /* SUCCESSFULLY REDREW WINDOW */

                /* ATTEMPT TO RETRIEVE HWND OF FIRST TEXTBOX */
                var num1 = FindWindowEx(win, IntPtr.Zero, null, "Enter First Number");
                //MessageBox.Show(num1.ToString("X8"));
                /* SUCCESSFULLY RETRIEVED HWND OF FIRST TEXTBOX */

                /* ATTEMPT TO RETRIEVE HWND OF SECOND TEXTBOX */
                var num2 = FindWindowEx(win, IntPtr.Zero, null, "Enter Second Number");
                //MessageBox.Show(num2.ToString("X8"));
                /* SUCCESSFULLY RETRIEVED HWND OF SECOND TEXTBOX */

                /* ATTEMPT TO RETRIEVE HWND OF CALCULATE BUTTON*/
                var btn = FindWindowEx(win, IntPtr.Zero, null, "Calculate");
                //MessageBox.Show(btn.ToString("X8"));
                /* SUCCESSFULLY RETRIEVED HWND OF CALCULATE BUTTON */



                /* ATTEMPT TO SET FIRST NUMBER to 6 */
                StringBuilder setNum1 = new StringBuilder(256);
                setNum1.Append("6");
                SendMessage(num1, WM_SETTEXT, setNum1.MaxCapacity, setNum1);
                /* SUCCESSFULLY SET FIRST NUMBER TO 6 */

                /* ATTEMPT TO SET SECOND NUMBER to 0 */
                StringBuilder setNum2 = new StringBuilder(256);
                setNum2.Append("0");
                SendMessage(num2, WM_SETTEXT, setNum2.MaxCapacity, setNum2);
                /* SUCCESSFULLY SET SECOND NUMBER TO 0 */

                /* ATTEMPT TO CLICK CALCULATE BUTTON */
                /* http://stackoverflow.com/questions/14962081/click-on-ok-button-of-message-box-using-winapi-in-c-sharp */
                const int WM_LBUTTONDOWN = 0x0201;
                const int WM_LBUTTONUP = 0x0202;
                SendMessage(btn, WM_LBUTTONDOWN, 0, IntPtr.Zero);
                SendMessage(btn, WM_LBUTTONUP, 0, IntPtr.Zero);
                /* SUCCESSFULLY CLICKED CACLULATE BUTTON */

                /* ATTEMPT TO REDRAW WINDOW */
                RedrawWindow(win, NULL, NULL, (UInt32)(RedrawWindowFlags.RDW_FRAME | RedrawWindowFlags.RDW_INVALIDATE | RedrawWindowFlags.RDW_UPDATENOW | RedrawWindowFlags.RDW_ALLCHILDREN));
                /* SUCCESSFULLY REDREW WINDOW */

                /* ATTEMPT TO RETRIEVE HWND OF DROPDOWN BOX */
                COMBOBOXINFO info;
                info = new COMBOBOXINFO();
                info.cbSize = Marshal.SizeOf(info);

                //GetComboBoxInfo(win, ref info);


                //var op = FindWindowEx(info.hwndList, IntPtr.Zero, null, null);
                //MessageBox.Show(op.ToString("X8"));


            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

    }
}
