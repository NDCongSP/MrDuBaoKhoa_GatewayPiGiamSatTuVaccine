using PLCPiProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestKetNoiNhietDo
{
    public partial class Form1 : Form
    {
        PLCPi myPLC = new PLCPi();

        static byte[] docHoldingNhietDo = { 0, 0, 0, 0, 0, 0 };
        static double nhietKenh12 = 0, nhietKenh13 = 0, nhietKenh14 = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myPLC.ModbusRTUMaster.ResponseTimeout = 1000;
            if (myPLC.ModbusRTUMaster.KetNoi("/dev/ttyUSB0", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
            //if (myPLC.ModbusRTUMaster.KetNoi("COM2", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
            {

                //update 20201013
                #region doc nhiet do tu 485 ban dau
                if (myPLC.ModbusRTUMaster.ReadHoldingRegisters(2, 0, 3, ref docHoldingNhietDo) == true)
                {

                    nhietKenh12 = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 0)) / 10, 2);
                    nhietKenh13 = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 2)) / 10, 2);
                    nhietKenh14 = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 4)) / 10, 2);

                    Console.WriteLine("doc nhiet do ban dau thanh cong {0}/{1}/{2}", nhietKenh12.ToString(), nhietKenh13.ToString(), nhietKenh14.ToString());
                    label1.BackColor = Color.Green;
                }
                else
                {
                    Console.WriteLine("doc nhiet do ban dau that bai");
                    label1.BackColor = Color.Red;
                }
                #endregion

            }
            else
            {

                label1.BackColor = Color.Red;
            }

            System.Timers.Timer nTimer = new System.Timers.Timer();
            nTimer.Interval = 1000;
            nTimer.Elapsed += (s, o) =>
            {
                nTimer.Enabled = false;

                label2.Invoke(new Action(()=> {
                    label2.Text = DocNhietDo();
                }));

                nTimer.Enabled = true;
            };
            nTimer.Enabled = true;
        }

        private string DocNhietDo()
        {
            #region doc nhiet do tu 485
            if (myPLC.ModbusRTUMaster.ReadHoldingRegisters(2, 0, 3, ref docHoldingNhietDo) == true)
            {
                nhietKenh12 = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 0)) / 10, 2);
                nhietKenh13 = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 2)) / 10, 2);
                nhietKenh14 = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 4)) / 10, 2);

                
                Console.WriteLine($"doc nhiet do thanh cong {nhietKenh12.ToString()}|{nhietKenh13.ToString()}|{nhietKenh14.ToString()}");
            }
            else
            {
                Console.WriteLine("doc nhiet do that bai");
            }

            return $"doc nhiet do {nhietKenh12.ToString()}|{nhietKenh13.ToString()}|{nhietKenh14.ToString()}";
            #endregion
        }
    }
}
