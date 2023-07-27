using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using PLCPiProject;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Reflection;
using System.IO.Ports;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

namespace nhietdokhovacxin
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();

        GhiMySQL mysql = new GhiMySQL();

        public Form1()
        {
            InitializeComponent();
        }
        DataTable dt;
        static double Ketthuc = 0, Batdau = 0, thoigiancoi = 0;

        static int thoigian, dem = 0;

        static byte[] canhbao_cao = { 0, 0, 0, 0 };
        static byte[] canhbao_thap = { 0, 0, 0, 0 };
        static byte CanhBaoMatDien = 0;
        static byte[] chot_cao = { 0, 0, 0, 0 };
        static byte[] chot_thap = { 0, 0, 0, 0 };
        static int baocoi = 0;

        static double qdcoi = 0;

        static string ttkn_server = "";
        static byte demmatketnoi_modbus = 0, ChotMatDien = 0, ChotBaoCoi = 0;

        static byte[] docHolding = { 0, 0 };
        static byte[] GhiOut = { 0, 0, 0, 0 };
        static bool[] DocIn = { false, false, false, false, false, false, false, false };
        static bool[] DocOut = { false, false, false, false, false, false, false, false };
        static string[] TenCB = { "", "", "", "", "" };
        static string idDS18B20 = "";

        static string NoiDung = "";

        //add new
        static double hightLevel = 8, lowLevel = 2;
        static string sdtSms = null, email = null;
        static byte[] docHoldingNhietDo = { 0, 0, 0, 0, 0, 0 };
        static double nhietKenh1 = 0, nhietKenh2 = 0, nhietKenh3 = 0, nhietKenh4 = 0;
        static double nhietKenh1New = 0, nhietKenh2New = 0, nhietKenh3New = 0, nhietKenh4New = 0;
        static double giaTriVotLo = 10;
        static double offset1 = 0, offset2 = 0, offset3 = 0, offset4 = 0;
        static byte idPLC = 2, idNhietDo = 1;

        #region Cong update 2020/11/10
        static string conStringLocal = "Server =localhost; Database = cauhinh ; Port=3306;User ID=root ;Password=100100;charset=utf8";

        private DataTable ExecuteQuery(string query, object[] parametter = null, [CallerMemberName] string memberCall = "", [CallerFilePath] string memberClass = "", [CallerLineNumber] int memberLine = 0)
        {
            DataTable res = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(conStringLocal))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                //trường hợp câu query là store procedure
                //set object giá trị tham số truyền vào, nếu khác null thì tiến hành add các giá trị cho tham số
                if (parametter != null)
                {
                    int i = 0;
                    string[] arrayParas = query.Split(' ');
                    foreach (string item in arrayParas)
                    {
                        if (item.Contains("@"))
                        {
                            if (parametter[i] != null)
                            {
                                command.Parameters.AddWithValue(item, parametter[i]);
                            }
                            else
                            {
                                command.Parameters.AddWithValue(item, DBNull.Value);
                            }
                            i++;
                        }

                    }
                }
                //chạy câu query fill vào DataTable
                MySqlDataAdapter adaptor = new MySqlDataAdapter(command);
                adaptor.Fill(res);

                //đóng kết nối hủy đối tượng connection đến DB server
                connection.Close();
                connection.Dispose();

                return res;
            }
        }

        public string ReadText(string PathFile)
        {
            try
            {
                FileStream fs = new FileStream(PathFile, FileMode.Open, FileAccess.Read, FileShare.None);
                System.IO.StreamReader sr = new System.IO.StreamReader(fs);
                string text = sr.ReadToEnd().Trim();
                sr.Close();
                fs.Close();
                return text;
            }
            catch { return "NULL"; }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                label22.Text = ConfigurationManager.AppSettings["tendonvi"];
                ttkn_server = mysql.Ketnoi_local();
                if (ttkn_server == "GOOD")
                {
                    #region dng bo gio
                    string TimeNow = mysql.DongBoThoiGian();

                    if (TimeNow != "" && TimeNow != null)
                    {
                        myPLC.ThoiGian.CaiDat(Convert.ToDateTime(TimeNow).ToString("dd-MM-yyyy HH:mm:ss"));
                        Console.WriteLine("Data_dongbo bo thanh cong " + Convert.ToDateTime(TimeNow).ToString("dd-MM-yyyy HH:mm:ss"));
                        Debug.WriteLine("Data_dongbo bo thanh cong " + Convert.ToDateTime(TimeNow).ToString("dd-MM-yyyy HH:mm:ss"));
                    }
                    else
                    {
                        Console.WriteLine("Dong bo loi");
                        Debug.WriteLine("Dong bo loi");
                    }
                    #endregion
                    label20.BackColor = Color.Green;
                }
                else
                {
                    label20.BackColor = Color.Red;
                }

                #region Cong update 2020/11/10. Đọc các giá trị cài đặt như ngưỡng cao thấp, thông tin gửi alarm. Read DB Local get infomation
                // truy van de lay id va ten cam bien  
                dt = ExecuteQuery($"select * from idds18b20");
                if (dt != null && dt.Rows.Count != 0)
                {
                    label11.Text = TenCB[0] = dt.Rows[0][2].ToString();
                    label12.Text = TenCB[1] = dt.Rows[1][2].ToString();
                    label13.Text = TenCB[2] = dt.Rows[2][2].ToString();
                    label14.Text = TenCB[3] = dt.Rows[3][2].ToString();
                    label5.Text = TenCB[4] = dt.Rows[4][2].ToString();

                    //offset
                    offset1 = Convert.ToDouble(dt.Rows[0][3].ToString());
                    offset2 = Convert.ToDouble(dt.Rows[0][3].ToString());
                    offset3 = Convert.ToDouble(dt.Rows[0][3].ToString());
                    offset4 = Convert.ToDouble(dt.Rows[0][3].ToString());
                }
                else
                {
                    ttkn_server = "BAD";
                    Restart();
                }
                dt.Clear();
                // doc thoi gian luu
                //dt = mysql.DocMySQL("thoigiancapnhat");
                dt = ExecuteQuery($"select * from thoigiancapnhat");
                if (dt != null && dt.Rows.Count != 0)
                {
                    thoigian = (Convert.ToInt32(dt.Rows[0][0].ToString())) * 60000;//phut
                    thoigiancoi = Convert.ToInt32(dt.Rows[0][1].ToString());//giay
                }
                else
                {
                    ttkn_server = "BAD";
                    Restart();
                }
                dt.Clear();

                //đọc ngưỡng cảnh báo
                dt = ExecuteQuery($"select * from gioihannhietdo");
                if (dt != null && dt.Rows.Count != 0)
                {
                    hightLevel = Convert.ToDouble(dt.Rows[0][0].ToString());
                    lowLevel = Convert.ToDouble(dt.Rows[0][1].ToString());
                    sdtSms = dt.Rows[0][2].ToString();
                    email = dt.Rows[0][4].ToString();
                }
                else
                {
                    Console.WriteLine("đọc DB local lỗi!");

                    Restart();
                }

                //đọc ngưỡng cảnh báo
                //doc gia tri offset
                //CHINH CHO NAY
                //string DataParametter = ReadText("./Files/Offset.txt").Trim();
                //string DataParametter = ReadText("/home/pi/Files/Offset.txt").Trim();
                //offset1 = Convert.ToDouble(DataParametter.Split('|')[0]);
                //offset2 = Convert.ToDouble(DataParametter.Split('|')[1]);
                //offset3 = Convert.ToDouble(DataParametter.Split('|')[2]);
                //offset4 = Convert.ToDouble(DataParametter.Split('|')[3]);

                Console.WriteLine(offset1.ToString() + "|" + offset2.ToString() + "|" + offset3.ToString() + "|" + offset4.ToString());
                #endregion

                #region ket noi 485
                myPLC.ModbusRTUMaster.ResponseTimeout = 1000;
                if (myPLC.ModbusRTUMaster.KetNoi("/dev/ttyUSB0", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
                //if (myPLC.ModbusRTUMaster.KetNoi("COM11", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
                {
                    demmatketnoi_modbus = 0;
                    Console.WriteLine("Ket noi modbus thanh cong");
                    //Debug.WriteLine("Ket noi modbus thanh cong");
                    label10.BackColor = Color.Green;
                    #region ghi modbus ngo ra xuat coi
                    myPLC.SetWord(GhiOut, 0, 0);
                    if (myPLC.ModbusRTUMaster.WriteHoldingRegisters(idPLC, 1, 1, GhiOut) == true)
                    {
                        demmatketnoi_modbus = 0;
                        Console.WriteLine("ghi modbus ngo ra thanh cong");
                        //Debug.WriteLine("ghi modbus ngo ra thanh cong");
                    }
                    else
                    {
                        demmatketnoi_modbus = 10;
                        Console.WriteLine("ghi modbus ngo ra that bai");
                        //Debug.WriteLine("ghi modbus ngo ra that bai");
                    }
                    Thread.Sleep(1000);
                    #endregion

                    //update 20201013
                    #region doc nhiet do tu 485
                    if (myPLC.ModbusRTUMaster.ReadHoldingRegisters(idNhietDo, 0, 3, ref docHoldingNhietDo) == true)
                    {
                        demmatketnoi_modbus = 0;

                        nhietKenh1 = nhietKenh1New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 0)) / 10, 2);
                        nhietKenh2 = nhietKenh2New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 2)) / 10, 2);
                        nhietKenh3 = nhietKenh3New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 4)) / 10, 2);

                        Console.WriteLine("doc nhiet do ban dau thanh cong{0}", nhietKenh1.ToString());
                        //Debug.WriteLine("doc nhiet do ban dau thanh cong{0}", nhietKenh1.ToString());
                    }
                    else
                    {
                        demmatketnoi_modbus++;
                        if (demmatketnoi_modbus >= 10)
                        {
                            demmatketnoi_modbus = 10;
                        }
                        Console.WriteLine("doc nhiet do ban dau that bai");
                        //Debug.WriteLine("doc nhiet do ban dau that bai");
                    }
                    #endregion
                }
                else
                {
                    demmatketnoi_modbus = 10;
                    label10.BackColor = Color.Red;
                }
                #endregion

                myPLC.SMS.Port_USB3G = "/dev/ttyUSB1";
                myPLC.SMS.Khoitao();
            }
            catch { }
            var ct1 = Task.Factory.StartNew(() => reconnect_server());
            timer2.Interval = thoigian;
            timer2.Enabled = true;
            timer1.Enabled = true;
        }

        public void Restart()
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = "sudo";
            proc.StartInfo.Arguments = "reboot";
            proc.Start();
            proc.Close();
            proc.Dispose();
        }

        #region task kiem tra ket noi den csdl server
        protected void reconnect_server()
        {
            while (true)
            {
                try
                {
                    if (ttkn_server != "GOOD")
                    {
                        Console.WriteLine("Mat ket noi DB server. ket noi laij...");
                        label20.BackColor = Color.Red;
                        mysql.Ngatketnoi_local();
                        ttkn_server = mysql.Ketnoi_local();
                        if (ttkn_server == "GOOD")
                        {
                            Console.WriteLine("Ket noi lai DB server thanh cong");
                            Debug.WriteLine("Ket noi lai DB server thanh cong");
                            label20.BackColor = Color.Green;
                        }
                    }

                    if (demmatketnoi_modbus >= 10)
                    {
                        Console.WriteLine("mat ket noi. ket noi lai modbus");
                        Debug.WriteLine("mat ket noi. ket noi lai modbus");
                        label10.BackColor = Color.Red;
                        myPLC.ModbusRTUMaster.NgatKetNoi();
                        if (myPLC.ModbusRTUMaster.KetNoi("/dev/ttyUSB0", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
                        //if (caidat.mymodbus.KetNoi("COM1", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
                        {
                            Console.WriteLine("ket noi lai modbus ok");
                            Debug.WriteLine("ket noi lai modbus ok");
                            label10.BackColor = Color.Green;
                            demmatketnoi_modbus = 0;
                        }
                    }

                }
                catch
                {
                    // caidat.ttkn_server = "BAD";
                }
                Thread.Sleep(2000);
            }
        }
        #endregion
        private void hienthi(string tempValue, string TenCB, Label mylabel, int a, string thap, string cao, string sdtsms, string dcemail)
        {
            try
            {
                if (tempValue != "-0.1")
                {
                    mylabel.Text = tempValue;
                    if (!string.IsNullOrEmpty(mylabel.Text))
                    {
                        // so sanh khi khong co alarm
                        #region xet dieu kien de dua ra tin hieu canh bao
                        if (Convert.ToDouble(mylabel.Text) > (Convert.ToDouble(cao) + 0.2))
                        {
                            canhbao_cao[a] = 1;
                        }
                        else if (Convert.ToDouble(mylabel.Text) < (Convert.ToDouble(cao) - 0.2))
                        {
                            canhbao_cao[a] = 0;
                        }

                        if (Convert.ToDouble(mylabel.Text) < (Convert.ToDouble(thap) - 0.2))
                        {
                            canhbao_thap[a] = 1;
                        }
                        else if (Convert.ToDouble(mylabel.Text) > (Convert.ToDouble(thap) + 0.2))
                        {
                            canhbao_thap[a] = 0;
                        }

                        if (canhbao_cao[a] == 1 || canhbao_thap[a] == 1)
                        {
                            mylabel.BackColor = Color.Red;
                        }
                        else
                        {
                            mylabel.BackColor = Color.GreenYellow;
                        }
                        #endregion
                        #region canh bao nguong cao
                        if (canhbao_cao[a] == 1 && chot_cao[a] == 0)
                        {
                            //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                            chot_cao[a] = 1;
                            //gui e mai
                            NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNguong thap: " + thap + ". \nNguong cao: " + cao + ". \nNhiet do hien tai: " + mylabel.Text + ". \nCanh bao: Vuot nguong cao";
                            sendemail(NoiDung, dcemail);

                            for (int j = 0; j < sdtsms.Split(',').Length; j++)
                            {
                                Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                                Thread.Sleep(2000);
                            }
                            //cap nhat bang alarm
                            ttkn_server = mysql.insert_cmd("alarm", "now(),'" + TenCB + "','" + thap + "','" + mylabel.Text + "','" + cao + "','Vuot nguong cao'");
                        }
                        else if (canhbao_cao[a] == 0 && chot_cao[a] == 1)
                        {
                            NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNhiet do da ve binh thuong";
                            //gui email
                            sendemail(NoiDung, dcemail);

                            for (int j = 0; j < sdtsms.Split(',').Length; j++)
                            {
                                Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                                Thread.Sleep(2000);
                            }
                            chot_cao[a] = 0;
                            ttkn_server = mysql.insert_cmd("alarm", "now(),'" + TenCB + "','" + thap + "','" + mylabel.Text + "','" + cao + "','Nhiet do da ve binh thuong'");
                        }
                        #endregion
                        #region canh bao nguong thap
                        if (canhbao_thap[a] == 1 && chot_thap[a] == 0)
                        {
                            chot_thap[a] = 1;
                            //cap nhat bang alarm
                            NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNguong thap: " + thap + ". \nNguong cao: " + cao + ". \nNhiet do hien tai: " + mylabel.Text + ". \nCanh bao: Vuot nguong thap";
                            //gui e mail
                            sendemail(NoiDung, dcemail);

                            for (int j = 0; j < sdtsms.Split(',').Length; j++)
                            {
                                //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                                Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                                Thread.Sleep(2000);
                            }
                            ttkn_server = mysql.insert_cmd("alarm", "now(),'" + TenCB + "','" + thap + "','" + mylabel.Text + "','" + cao + "','Vuot nguong thap'");
                        }
                        else if (canhbao_thap[a] == 0 && chot_thap[a] == 1)
                        {
                            NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNhiet do da ve binh thuong";
                            sendemail(NoiDung, dcemail);

                            for (int j = 0; j < sdtsms.Split(',').Length; j++)
                            {
                                //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                                Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                                Thread.Sleep(2000);
                            }
                            chot_thap[a] = 0;
                            ttkn_server = mysql.insert_cmd("alarm", "now(),'" + TenCB + "','" + thap + "','" + mylabel.Text + "','" + cao + "','Nhiet do da ve binh thuong'");
                        }
                        #endregion
                    }
                }
                else
                {
                    mylabel.Text = "BAD";
                }
            }
            catch { }
        }
        private void ghicsdl(string cb1, string cb2, string cb3, string cb4, Label mylabel4)
        {
            try
            {
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[0] + "','" + cb1 + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[1] + "','" + cb2 + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[2] + "','" + cb3 + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[4] + "','" + cb4 + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[3] + "','" + mylabel4.Text + "'");
                Console.WriteLine("ghi bao cao");
                //Debug.WriteLine("ghi bao cao");
            }
            catch { ttkn_server = "BAD"; }
        }
        private void BaoMatDien(string TenCB, Label mylabel, string sdtsms, string dcemail)
        {
            try
            {
                #region doc ngo vao bao mat dien tu PLC
                if (myPLC.ModbusRTUMaster.ReadHoldingRegisters(idPLC, 0, 1, ref docHolding) == true)
                {
                    demmatketnoi_modbus = 0;
                    Console.WriteLine("doc input thanh cong{0}", myPLC.GetUshortAt(docHolding, 0));
                    //Debug.WriteLine("doc input thanh cong{0}", myPLC.GetUshortAt(docHolding, 0));
                }
                else
                {
                    demmatketnoi_modbus++;
                    if (demmatketnoi_modbus >= 10)
                    {
                        demmatketnoi_modbus = 10;
                    }
                    Console.WriteLine("doc input that bai");
                    //Debug.WriteLine("doc input that bai");
                }
                #endregion
                if (myPLC.GetUshortAt(docHolding, 0) == 1)// && ChotMatDien == 0)
                {
                    mylabel.Text = "ALARM";
                    mylabel.BackColor = Color.Red;
                    CanhBaoMatDien = 1;
                    if (ChotMatDien == 0)
                    {
                        NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nCanh bao mat dien luoi.";
                        sendemail(NoiDung, dcemail);
                        // gui sms
                        //myPLC.SMS.GuiSMS(sdtsms, NoiDung);
                        for (int j = 0; j < sdtsms.Split(',').Length; j++)
                        {
                            //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                            Console.WriteLine($"gui SMS: {myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung)}");
                            Thread.Sleep(2000);
                        }
                        //Thread.Sleep(3000);
                        ChotMatDien = 1;
                        ttkn_server = mysql.insert_cmd("alarm", "now(),'" + TenCB + "','NULL','" + mylabel.Text + "','NULL','Canh Bao mat dien luoi'");
                    }
                }
                else //if (DocIn[0] == true)// && ChotMatDien == 1)
                {
                    mylabel.Text = "GOOD";
                    mylabel.BackColor = Color.Green;
                    CanhBaoMatDien = 0;
                    if (ChotMatDien == 1)
                    {
                        NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nDien luoi da co lai.";
                        sendemail(NoiDung, dcemail);

                        for (int j = 0; j < sdtsms.Split(',').Length; j++)
                        {
                            //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                            Console.WriteLine($"gui SMS: {myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung)}");
                            Thread.Sleep(2000);
                        }
                        ChotMatDien = 0;
                        ttkn_server = mysql.insert_cmd("alarm", "now(),'" + TenCB + "','NULL','" + mylabel.Text + "','NULL','Dien luoi da co lai'");
                    }
                }

                #region ghi ngo ra PLC bao coi
                baocoi = canhbao_cao[0] + canhbao_cao[1] + canhbao_cao[2] +
                    canhbao_thap[0] + canhbao_thap[1] + canhbao_thap[2] + CanhBaoMatDien;
                Console.WriteLine("Bat coi{0}", baocoi);
                //Debug.WriteLine("Bat coi{0}", baocoi);
                if (baocoi > 0)
                {
                    if (qdcoi == 0)
                    {
                        Batdau = DateTime.Now.TimeOfDay.Seconds;
                        qdcoi = 1;
                    }
                    Ketthuc = DateTime.Now.TimeOfDay.Seconds;
                    if ((Ketthuc - Batdau) >= thoigiancoi && ChotBaoCoi == 0)
                    {
                        #region ghi modbus ngo ra xuat coi
                        myPLC.SetWord(GhiOut, 0, 1);
                        if (myPLC.ModbusRTUMaster.WriteHoldingRegisters(idPLC, 1, 1, GhiOut) == true)
                        {
                            demmatketnoi_modbus = 0;
                            ChotBaoCoi = 1;
                            Console.WriteLine("ghi modbus ngo ra thanh cong");
                            //Debug.WriteLine("ghi modbus ngo ra thanh cong");
                        }
                        else
                        {
                            demmatketnoi_modbus++;
                            if (demmatketnoi_modbus >= 10)
                            {
                                demmatketnoi_modbus = 10;
                            }
                            Console.WriteLine("ghi modbus ngo ra that bai");
                            //Debug.WriteLine("ghi modbus ngo ra that bai");
                        }
                        #endregion
                    }
                    else if ((Ketthuc - Batdau) < 0)
                    {
                        Batdau = DateTime.Now.TimeOfDay.Minutes;
                    }

                }
                else if (baocoi == 0 && ChotBaoCoi == 1)
                {
                    #region ghi modbus ngo ra xuat coi
                    myPLC.SetWord(GhiOut, 0, 0);
                    if (myPLC.ModbusRTUMaster.WriteHoldingRegisters(idPLC, 1, 1, GhiOut) == true)
                    {
                        demmatketnoi_modbus = 0;
                        ChotBaoCoi = 0;
                        Console.WriteLine("ghi modbus ngo ra thanh cong");
                        //Debug.WriteLine("ghi modbus ngo ra thanh cong");
                    }
                    else
                    {
                        demmatketnoi_modbus++;
                        if (demmatketnoi_modbus >= 10)
                        {
                            demmatketnoi_modbus = 10;
                        }
                        Console.WriteLine("ghi modbus ngo ra that bai");
                        //Debug.WriteLine("ghi modbus ngo ra that bai");
                    }
                    #endregion
                    qdcoi = 0;
                }
                #endregion
            }
            catch { }
        }

        private void sendemail(string noidung, string EmailTo)
        {
            try
            {
                myPLC.Email.CredentialEmail = "giamsat.canhbao@gmail.com";
                myPLC.Email.CredentialPass = "1@3$5^7*";

                myPLC.Email.emailTo = EmailTo;
                myPLC.Email.subjectEmail = "Alarm";
                Console.WriteLine($"EMAIL {noidung}");
                //Debug.WriteLine($"EMAIL {noidung}");
                myPLC.Email.bodyEmail = noidung;
                myPLC.Email.TimeOut = 2000;
                myPLC.Email.SendEmail();
                Thread.Sleep(10);
                Console.WriteLine($"Gui Email thanh cong");
                //Debug.WriteLine($"Gui Email thanh cong");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi gửi Email : {ex.Message}");
                //Debug.WriteLine($"Lỗi gửi Email : {ex.Message}");
            }
        }

        private void DocNhietDo()
        {
            #region doc nhiet do tu 485
            if (myPLC.ModbusRTUMaster.ReadHoldingRegisters(idNhietDo, 0, 3, ref docHoldingNhietDo) == true)
            {
                demmatketnoi_modbus = 0;
                nhietKenh1New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 0)) / 10, 2) + offset1;
                nhietKenh2New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 2)) / 10, 2) + offset2;
                //nhietKenh3New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 4)) / 10, 2) + offset3;
                nhietKenh3New = 3;

                if (nhietKenh1New <= nhietKenh1 + giaTriVotLo && nhietKenh1New >= nhietKenh1 + giaTriVotLo)
                {
                    nhietKenh1 = nhietKenh1New;
                }

                if (nhietKenh2New <= nhietKenh2 + giaTriVotLo && nhietKenh2New >= nhietKenh2 + giaTriVotLo)
                {
                    nhietKenh2 = nhietKenh2New;
                }

                if (nhietKenh3New <= nhietKenh3 + giaTriVotLo && nhietKenh3New >= nhietKenh3 + giaTriVotLo)
                {
                    nhietKenh3 = nhietKenh3New;
                }
                //Debug.WriteLine($"doc nhiet do thanh cong {nhietKenh1.ToString()}|{nhietKenh2.ToString()}|{nhietKenh3.ToString()}");
            }
            else
            {
                demmatketnoi_modbus++;
                if (demmatketnoi_modbus >= 10)
                {
                    demmatketnoi_modbus = 10;
                }
                Console.WriteLine("doc nhiet do that bai");
                //Debug.WriteLine("doc nhiet do that bai");
            }
            #endregion

            #region đọc từ DS18B20
            //var t4 = myPLC.DS18B20.DocNhietDo(idDS18B20);
            //if (t4 != "BAD" && !string.IsNullOrEmpty(t4))
            //{
            //    nhietKenh4New = double.TryParse(t4, out double value) ? value : nhietKenh4New;
            //    if (nhietKenh4New <= nhietKenh4 + giaTriVotLo && nhietKenh4New >= nhietKenh4 - giaTriVotLo)
            //    {
            //        nhietKenh4 = nhietKenh4New;
            //    }
            //}
            #endregion

            Console.WriteLine($"doc nhiet do thanh cong {nhietKenh1.ToString()}|{nhietKenh2.ToString()}|{nhietKenh3.ToString()}|{nhietKenh4.ToString()}" +
                    $"//trước ofset {nhietKenh1 - offset1}|{nhietKenh2 - offset2}");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (dem < 4)
            {
                dem++;
            }
            else
            {
                dem = 1;
            }
            try
            {
                // lay các so lieu
                //Console.WriteLine("hight={0};low={1},sdtsms={2},email={3}", dt2.Rows[0][0], dt2.Rows[0][1], dt2.Rows[0][2], dt2.Rows[0][4]);
                if (dem == 1)
                {
                    DocNhietDo();
                    hienthi(nhietKenh1.ToString(), TenCB[0], label1, 0, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi(nhietKenh2.ToString(), TenCB[1], label2, 1, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi(nhietKenh3.ToString(), TenCB[2], label3, 2, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi(nhietKenh4.ToString(), TenCB[4], label6, 3, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                else if (dem == 2)
                {
                    BaoMatDien(TenCB[3], label4, sdtSms, email);
                }
                else if (dem == 3)
                {
                    DocNhietDo();
                    hienthi(nhietKenh1.ToString(), TenCB[0], label1, 0, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi(nhietKenh2.ToString(), TenCB[1], label2, 1, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi(nhietKenh3.ToString(), TenCB[2], label3, 2, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi(nhietKenh4.ToString(), TenCB[4], label6, 3, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                // cap nhat ia tri len web 
                ttkn_server = mysql.capnhatgiatriweb(nhietKenh1.ToString(), nhietKenh2.ToString(), nhietKenh3.ToString(), nhietKenh4.ToString(), label4.Text.Trim());
            }
            catch { }
            timer1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            try
            {
                ghicsdl(nhietKenh1.ToString(), nhietKenh2.ToString(), nhietKenh3.ToString(), nhietKenh4.ToString(), label4);
            }
            catch { }
            timer2.Enabled = true;

        }

    }
}
