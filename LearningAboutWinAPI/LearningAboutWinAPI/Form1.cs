using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        [DllImport("user32.dll")]
        public static extern int MessageBox(int h, string m, string c, int type);

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox(0, "API Message Box", "API Demo", 0);
        }
    }
}
