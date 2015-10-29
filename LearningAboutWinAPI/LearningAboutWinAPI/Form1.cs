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

namespace LearningAboutWinAPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* http://stackoverflow.com/questions/523405/how-to-send-text-to-notepad-in-c-win32 
         * and
         * http://stackoverflow.com/questions/6604070/write-text-to-notepad-with-c-win32 */

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        extern static IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, [In] string lpClassName, [In] string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        extern static int SendMessageGetTextLength(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, int lParam);

        const int WM_GETTEXTLENGTH = 0x000E;

        const int EM_SETSEL = 0x00B1;

        const int EM_REPLACESEL = 0x00C2;

        public void testAppendText(string text)
        {
            Process[] notepads = Process.GetProcessesByName("notepad");
            if (notepads.Length == 0) return;
            if (notepads[0] != null)
            {
                IntPtr editBox = FindWindowEx(notepads[0].MainWindowHandle, new IntPtr(0), "Edit", null);
                int length = SendMessageGetTextLength(editBox, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                SendMessage(editBox, EM_SETSEL, length, length);
                SendMessage(editBox, EM_REPLACESEL, 1, text);
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                testAppendText(textBox1.Text);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }
    }
}
