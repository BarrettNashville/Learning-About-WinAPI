﻿using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;


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

        // SendMessage() for CB_SETCURSEL
        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

        // GetWindow()
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        // enum for GetWindow()
        enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }


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


        const int WM_SETTEXT = 0x000C;
        const int WM_GETTEXT = 0x000D;
        const UInt32 CB_SETCURSEL = 0x14E;
        const int BM_CLICK = 0x00F5;
        const int WM_SYSCOMMAND = 0X0112;
        const int SC_CLOSE = 0XF060;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // STEP 1 - Open app and get hwnd of app
                STARTUPINFO si = new STARTUPINFO();
                PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
                CreateProcess(@"MathApp.exe", null, IntPtr.Zero, IntPtr.Zero, false, 0, IntPtr.Zero,
                    null, ref si, out pi);
                Thread.Sleep(100);
                var win = FindWindow(null, "Math App");

                // STEP 2 - Enter 6 in the first text box and 0 in the second
                var num1 = FindWindowEx(win, IntPtr.Zero, null, "Enter First Number");
                StringBuilder setNum1 = new StringBuilder(256);
                setNum1.Append("6");
                SendMessage(num1, WM_SETTEXT, setNum1.MaxCapacity, setNum1);

                var num2 = FindWindowEx(win, IntPtr.Zero, null, "Enter Second Number");
                StringBuilder setNum2 = new StringBuilder(256);
                setNum2.Append("0");
                SendMessage(num2, WM_SETTEXT, setNum2.MaxCapacity, setNum2);

                // STEP 3 - Select Divide in the Combo Box
                /* http://stackoverflow.com/questions/7376435/how-to-interact-with-a-form-using-handle-c-sharp */
                var ch = GetWindow(win, (uint)GetWindow_Cmd.GW_CHILD);
                var ch2 = GetWindow(ch, (uint)GetWindow_Cmd.GW_HWNDNEXT);
                var ch3 = GetWindow(ch2, (uint)GetWindow_Cmd.GW_HWNDNEXT);
                SendMessage(ch3, CB_SETCURSEL, (Int32)3, 0);

                // STEP 4 - Click the Equals Button (but grab the hwnd of answer label first)
                var lbl = FindWindowEx(win, IntPtr.Zero, null, "Answer");
                var btn = FindWindowEx(win, IntPtr.Zero, null, "Calculate");
                SendMessage(btn, BM_CLICK, 0, IntPtr.Zero);

                // STEP 5 - Capture the error message
                StringBuilder getText = new StringBuilder(256);  // or length from call with GETTEXTLENGTH
                SendMessage(lbl, WM_GETTEXT, getText.Capacity, getText);
                MessageBox.Show(getText.ToString());

                // STEP 6 - Enter 3 in the second text box
                setNum2.Clear();
                setNum2.Append("3");
                SendMessage(num2, WM_SETTEXT, setNum2.MaxCapacity, setNum2);

                // STEP 7 - Click the equals button (and redraw window)
                SendMessage(btn, BM_CLICK, 0, IntPtr.Zero);
                RedrawWindow(win, NULL, NULL, (UInt32)(RedrawWindowFlags.RDW_FRAME | RedrawWindowFlags.RDW_INVALIDATE | RedrawWindowFlags.RDW_UPDATENOW | RedrawWindowFlags.RDW_ALLCHILDREN));

                // STEP 8 - Store the answer to memory
                StringBuilder getNewText = new StringBuilder(256);  // or length from call with GETTEXTLENGTH
                SendMessage(lbl, WM_GETTEXT, getNewText.Capacity, getNewText);

                // STEP 9 - Close the window
                SendMessage(win, WM_SYSCOMMAND, SC_CLOSE, 0);

                // STEP 10 - Write the result to the console
                MessageBox.Show(getNewText.ToString());

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

    }
}
