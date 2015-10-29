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

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, int wParam, string lParam);

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process[] notepads = Process.GetProcessesByName("notepad");
                if (notepads.Length == 0) return;
                if (notepads[0] != null)
                {
                    IntPtr child = FindWindowEx(notepads[0].MainWindowHandle, new IntPtr(0), "Edit", null);
                    SendMessage(child, 0x000C, 0, textBox1.Text);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }
    }
}
