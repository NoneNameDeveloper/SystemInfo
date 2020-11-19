using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Fonts;
using System.Threading;
using System.Runtime.InteropServices;

namespace SystemInfo
{
    public partial class Form1 : MetroForm
    {
        private float cpu;
        private float ram;
        private float installedMemory;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]

        private class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLength;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));
            }

        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GlobalMemoryStatusEx([In, Out] MEMORYSTATUSEX lpBuffer);
        public Form1()
        {
            InitializeComponent();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
               string key = string.Empty;

               switch (metroComboBox1.SelectedItem.ToString())
               {
                   case "Процессор":
                       key = "Win32_Processor";
                       break;
                   case "Видеокарта":
                       key = "Win32_VideoController";
                       break;
                   case "Чипсет":
                       key = "Win32_IDEController";
                       break;
                   case "Батарея":
                       key = "Win32_Battery";
                       break;
                   case "Биос":
                       key = "Win32_BIOS";
                       break;
                   case "Оперативная память":
                       key = "Win32_PhysicalMemory";
                       break;
                   case "Кэш":
                       key = "Win32_CacheMemory";
                       break;
                   case "USB":
                       key = "Win32_USBController";
                       break;
                   case "Диск":
                       key = "Win32_LogicalDisk";
                       break;
                   case "Логические диски":
                       key = "Win32_LogicalDisk";
                       break;
                   case "Клавиатура":
                       key = "Win32_Keyboard";
                       break;
                   case "Сеть":
                       key = "Win32_NetworkAdapter";
                       break;
                   case "Пользователи":
                       key = "Win32_Account";
                       break;
                   default:
                       key = "Win32_Processor";
                       break;
               }

                GetMainInfo.GetHardWareInfo(key, listView1);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MEMORYSTATUSEX mEMORYSTATUSEX = new MEMORYSTATUSEX();

            if (GlobalMemoryStatusEx(mEMORYSTATUSEX))
            {
                installedMemory = mEMORYSTATUSEX.ullTotalPhys;
            }
            timer1.Interval = 1000;

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cpu = performanceCPU.NextValue();
            ram = performanceRAM.NextValue();

            chart1.Series["CPU"].Points.AddY(cpu);
            chart1.Series["RAM"].Points.AddY(ram);

        }
    }
}
