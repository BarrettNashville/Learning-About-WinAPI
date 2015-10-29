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

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }

        [DllImport("kernel32", SetLastError = true)]
        public static extern void GetSystemInfo(ref SYSTEM_INFO lpSystemInfo);



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SYSTEM_INFO pSI = new SYSTEM_INFO();
                GetSystemInfo(ref pSI);

                listBox1.Items.Insert(0, "Number of Processors: " + pSI.dwNumberOfProcessors.ToString());
                listBox1.Items.Insert(1, "Processor Type: " + pSI.dwProcessorType.ToString());
                listBox1.Items.Insert(2, "Page Size: " + pSI.dwPageSize);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }
    }
}
