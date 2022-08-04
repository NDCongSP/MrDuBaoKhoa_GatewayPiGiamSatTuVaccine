using PLCPiProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSMS
{
    public partial class Form1 : Form
    {
        PLCPi myPLC = new PLCPi();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myPLC.SMS.Port_USB3G = "/dev/ttyUSB1";
            myPLC.SMS.Khoitao();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < txtPhoneNum.Text.Trim().Split(',').Length; j++)
            {
                //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                Console.WriteLine(myPLC.SMS.GuiSMS(txtPhoneNum.Text.Trim().Split(',')[j], txtContent.Text));
                Thread.Sleep(2000);
            }
        }
    }
}
