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
namespace nhietdokhovacxin
{
    public partial class Form1 : Form
    {
        //Tao doi tuong myPLC
        PLCPi myPLC = new PLCPi();
        //Email myemail = new Email();
        GhiMySQL mysql = new GhiMySQL();
        public Form1()
        {
            InitializeComponent();
        }
        DataTable dt;
        DataTable dt1;
        DataTable dt2;
        static double Ketthuc = 0, Batdau = 0, thoigiancoi = 0;

        static string id1, id2, id3, id4, id5;//, id6, id7;// id10, id11, id12;
        static int thoigian, dem = 0;

        static byte[] canhbao_cao = { 0, 0, 0, 0, 0 };
        static byte[] canhbao_thap = { 0, 0, 0, 0, 0 };
        static byte CanhBaoMatDien = 0;
        static byte[] chot_cao = { 0, 0, 0, 0, 0 };
        static byte[] chot_thap = { 0, 0, 0, 0, 0 };
        static int baocoi = 0;
        static int reset1 = 0, reset2 = 0;

        static double qdcoi = 0;
        static bool qdgoi = false;
        static string ttkn_server = "", sodtgoi = "";
        static byte demmatketnoi_modbus = 0, ChotMatDien = 0, ChotBaoCoi = 0;

        static byte[] GhiOut = { 0, 0, 0, 0 };
        static bool[] DocIn = { false, false, false, false, false, false, false, false };
        static bool[] DocOut = { false, false, false, false, false, false, false, false };
        static string[] TenCB = { "", "", "", "", "", "" };
        static string ThoiGianAlarm = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                label22.Text = ConfigurationManager.AppSettings["tendonvi"];
                ttkn_server = mysql.Ketnoi_local();
                if (ttkn_server == "GOOD")
                {
                    label20.BackColor = Color.Green;
                    // truy van de lay id va ten cam bien  
                    dt = mysql.DocMySQL("idds18b20");
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        id1 = dt.Rows[0][1].ToString().Trim();
                        id2 = dt.Rows[1][1].ToString().Trim();
                        id3 = dt.Rows[2][1].ToString().Trim();
                        id4 = dt.Rows[3][1].ToString().Trim();
                        id5 = dt.Rows[4][1].ToString().Trim();
                        Console.WriteLine($"{id1}-{id2}-{id3}-{id4}-{id5}");

                        label11.Text = TenCB[0] = dt.Rows[0][2].ToString();
                        label12.Text = TenCB[1] = dt.Rows[1][2].ToString();
                        label13.Text = TenCB[2] = dt.Rows[2][2].ToString();
                        label14.Text = TenCB[3] = dt.Rows[3][2].ToString();
                        label15.Text = TenCB[4] = dt.Rows[4][2].ToString();
                        
                        label19.Text = TenCB[5] = dt.Rows[5][2].ToString();
                    }
                    else { ttkn_server = "BAD"; }
                    dt.Clear();
                    // doc thoi gian luu
                    dt = mysql.DocMySQL("thoigiancapnhat");
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        thoigian = (Convert.ToInt32(dt.Rows[0][0].ToString())) * 60000;//phut
                        thoigiancoi = Convert.ToInt32(dt.Rows[0][1].ToString());//giay
                    }
                    else { ttkn_server = "BAD"; }
                    dt.Clear();
                }
                else
                {
                    label20.BackColor = Color.Red;
                }
                #region ket noi 485
                myPLC.ModbusRTUMaster.ResponseTimeout = 1000;
                if (myPLC.ModbusRTUMaster.KetNoi("/dev/ttyUSB0", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
                {
                    demmatketnoi_modbus = 0;
                    Console.WriteLine("Ket noi modbus thanh cong");
                    label10.BackColor = Color.Green;
                    #region ghi modbus ngo ra xuat coi
                    myPLC.SetWord(GhiOut, 0, 0);
                    if (myPLC.ModbusRTUMaster.WriteHoldingRegisters(1, 1, 1, GhiOut) == true)
                    {
                        demmatketnoi_modbus = 0;
                        Console.WriteLine("ghi modbus ngo ra thanh cong");
                    }
                    else
                    {
                        demmatketnoi_modbus = 10;
                        Console.WriteLine("ghi modbus ngo ra that bai");
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
                            label20.BackColor = Color.Green;
                        }
                    }

                    if (demmatketnoi_modbus >= 10)
                    {
                        Console.WriteLine("mat ket noi. ket noi lai modbus");
                        label10.BackColor = Color.Red;
                        myPLC.ModbusRTUMaster.NgatKetNoi();
                        if (myPLC.ModbusRTUMaster.KetNoi("/dev/ttyUSB0", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
                        //if (caidat.mymodbus.KetNoi("COM1", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
                        {
                            Console.WriteLine("ket noi lai modbus ok");
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
        private void hienthi(string id, string TenCB, Label mylabel, int a, string thap, string cao, string sdtsms, string dcemail)
        {
            try
            {
                mylabel.Text = myPLC.DS18B20.DocNhietDo(id);
                if (mylabel.Text != "BAD" && !string.IsNullOrEmpty(mylabel.Text))
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
                        //cap nhat bang alarm
                        ttkn_server = mysql.insert_cmd("alarm", "now(),'" + TenCB + "','" + thap + "','" + mylabel.Text + "','" + cao + "','Vuot nguong cao'");
                        //gui e mai
                        sendemail(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nNguong thap: " + thap + ",\r\nNguong cao: " + cao + ",\r\nNhiet do hien tai: " + mylabel.Text + ",\r\nCanh bao: Vuot nguong cao", dcemail);
                        // gui sms
                        myPLC.SMS.GuiSMS(sdtsms, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nNguong thap: " + thap + ",\r\nNguong cao: " + cao + ",\r\nNhiet do hien tai: " + mylabel.Text + ",\r\nCanh bao: Vuot nguong cao");
                        Thread.Sleep(3000);
                    }
                    else if (canhbao_cao[a] == 0 && chot_cao[a] == 1)
                    {
                        //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        sendemail(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nNhiet do da ve binh thuong", dcemail);
                        // gui sms
                        myPLC.SMS.GuiSMS(sdtsms, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nNhiet do da ve binh thuong");
                        Thread.Sleep(3000);
                        chot_cao[a] = 0;
                    }
                    #endregion
                    #region canh bao nguong thap
                    if (canhbao_thap[a] == 1 && chot_thap[a] == 0)
                    {
                        //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        chot_thap[a] = 1;
                        //cap nhat bang alarm
                        ttkn_server = mysql.insert_cmd("alarm", "now(),'" + TenCB + "','" + thap + "','" + mylabel.Text + "','" + cao + "','Vuot nguong thap'");
                        //gui e mail
                        sendemail(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nNguong thap: " + thap + ",\r\nNguong cao: " + cao + ",\r\nNhiet do hien tai: " + mylabel.Text + ",\r\nCanh bao: Vuot nguong thap", dcemail);
                        // gui sms
                        myPLC.SMS.GuiSMS(sdtsms, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nNguong thap: " + thap + ",\r\nNguong cao: " + cao + ",\r\nNhiet do hien tai: " + mylabel.Text + ",\r\nCanh bao: Vuot nguong thap");
                        Thread.Sleep(3000);

                    }
                    else if (canhbao_thap[a] == 0 && chot_thap[a] == 1)
                    {
                        //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        sendemail(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nNhiet do da ve binh thuong", dcemail);
                        // gui sms
                        myPLC.SMS.GuiSMS(sdtsms, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nNhiet do da ve binh thuong");
                        Thread.Sleep(3000);
                        chot_thap[a] = 0;
                    }
                    #endregion
                }
            }
            catch { }
        }
        private void ghicsdl(Label mylabel1, Label mylabel2, Label mylabel3, Label mylabel4, Label mylabel5, Label mylabel7)
        {
            try
            {
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[0] + "','" + Convert.ToDouble(mylabel1.Text).ToString() + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[1] + "','" + Convert.ToDouble(mylabel2.Text).ToString() + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[2] + "','" + Convert.ToDouble(mylabel3.Text).ToString() + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[3] + "','" + Convert.ToDouble(mylabel4.Text).ToString() + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[4] + "','" + Convert.ToDouble(mylabel5.Text).ToString() + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[5] + "','" + mylabel7.Text + "'");
            }
            catch { ttkn_server = "BAD"; }
        }
        private void BaoMatDien(string TenCB, Label mylabel, string sdtsms, string dcemail)
        {
            try
            {
                #region doc ngo vao bao mat dien tu IO8
                if (myPLC.ModbusRTUMaster.ReadDiscreteInputContact(1, 0, 1, ref DocIn) == true)
                {
                    demmatketnoi_modbus = 0;
                    Console.WriteLine("doc input thanh cong{0}", DocIn[0]);
                }
                else
                {
                    demmatketnoi_modbus++;
                    if (demmatketnoi_modbus >= 10)
                    {
                        demmatketnoi_modbus = 10;
                    }
                    Console.WriteLine("doc input that bai");
                }
                #endregion
                if (DocIn[0] == false)// && ChotMatDien == 0)
                {
                    mylabel.Text = "ALARM";
                    mylabel.BackColor = Color.Red;
                    CanhBaoMatDien = 1;
                    if (ChotMatDien == 0)
                    {
                        //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        ttkn_server = mysql.insert_cmd("alarm", "now(),'" + TenCB + "','NULL','" + mylabel.Text + "','NULL','Canh Bao mat dien luoi.'");
                        sendemail(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nCanh bao mat dien luoi.", dcemail);
                        // gui sms
                        myPLC.SMS.GuiSMS(sdtsms, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nCanh bao mat dien luoi.");
                        Thread.Sleep(3000);
                        ChotMatDien = 1;
                    }
                }
                else if (DocIn[0] == true)// && ChotMatDien == 1)
                {
                    mylabel.Text = "GOOD";
                    mylabel.BackColor = Color.Green;
                    CanhBaoMatDien = 0;
                    if (ChotMatDien == 1)
                    {
                        //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        sendemail(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nDien luoi da co lai.", dcemail);
                        // gui sms
                        myPLC.SMS.GuiSMS(sdtsms, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "\r\n" + TenCB + "\r\nDien luoi da co lai.");
                        Thread.Sleep(3000);
                        ChotMatDien = 0;
                    }
                }

                #region ghi ngo ra IO8 bao coi
                baocoi = canhbao_cao[0] + canhbao_cao[1] + canhbao_cao[2] + canhbao_cao[3] + canhbao_cao[4] +
                    canhbao_thap[0] + canhbao_thap[1] + canhbao_thap[2] + canhbao_thap[3] + canhbao_thap[4] + CanhBaoMatDien;
                Console.WriteLine("Bat coi{0}", baocoi);
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
                        if (myPLC.ModbusRTUMaster.WriteHoldingRegisters(1, 1, 1, GhiOut) == true)
                        {
                            demmatketnoi_modbus = 0;
                            ChotBaoCoi = 1;
                            Console.WriteLine("ghi modbus ngo ra thanh cong");
                        }
                        else
                        {
                            demmatketnoi_modbus++;
                            if (demmatketnoi_modbus >= 10)
                            {
                                demmatketnoi_modbus = 10;
                            }
                            Console.WriteLine("ghi modbus ngo ra that bai");
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
                    if (myPLC.ModbusRTUMaster.WriteHoldingRegisters(1, 1, 1, GhiOut) == true)
                    {
                        demmatketnoi_modbus = 0;
                        ChotBaoCoi = 0;
                        Console.WriteLine("ghi modbus ngo ra thanh cong");
                    }
                    else
                    {
                        demmatketnoi_modbus++;
                        if (demmatketnoi_modbus >= 10)
                        {
                            demmatketnoi_modbus = 10;
                        }
                        Console.WriteLine("ghi modbus ngo ra that bai");
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
                //string strAlarm = "Location : " + location.Name + Environment.NewLine
                //     + "Alarm: " + alarm + Environment.NewLine
                //     + "Value: " + location.Value + Environment.NewLine
                //     + "Low Level: " + (location.LowLevel.HasValue ? location.LowLevel.Value.ToString() : "") + Environment.NewLine
                //     + "High Level: " + (location.HighLevel.HasValue ? location.HighLevel.Value.ToString() : "");
                Console.WriteLine($"EMAIL {noidung}");
                myPLC.Email.bodyEmail = noidung;
                myPLC.Email.TimeOut = 2000;
                myPLC.Email.SendEmail();
                Thread.Sleep(10);
                Console.WriteLine($"Gui Email");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi gửi Email : {ex.Message}");
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (dem < 6)
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
                if (ttkn_server == "GOOD")
                {
                    // truy van de lay id va ten cam bien  
                    dt = mysql.DocMySQL("idds18b20");
                    if (dt != null && dt.Rows.Count != 0)
                    {
                        id1 = dt.Rows[0][1].ToString().Trim();
                        id2 = dt.Rows[1][1].ToString().Trim();
                        id3 = dt.Rows[2][1].ToString().Trim();
                        id4 = dt.Rows[3][1].ToString().Trim();
                        id5 = dt.Rows[4][1].ToString().Trim();

                        label11.Text = TenCB[0] = dt.Rows[0][2].ToString();
                        label12.Text = TenCB[1] = dt.Rows[1][2].ToString();
                        label13.Text = TenCB[2] = dt.Rows[2][2].ToString();
                        label14.Text = TenCB[3] = dt.Rows[3][2].ToString();
                        label15.Text = TenCB[4] = dt.Rows[4][2].ToString();
                        label19.Text = TenCB[6] = dt.Rows[5][2].ToString();

                    }
                    else { ttkn_server = "BAD"; }
                    dt.Clear();
                    // doc thoi gian luu
                    dt2 = mysql.DocMySQL("gioihannhietdo");
                    if (dt2 != null && dt2.Rows.Count != 0)
                    {
                        //Console.WriteLine("hight={0};low={1},sdtsms={2},email={3}", dt2.Rows[0][0], dt2.Rows[0][1], dt2.Rows[0][2], dt2.Rows[0][4]);
                        if (dem == 1)
                            hienthi(id1, TenCB[0], label1, 0, dt2.Rows[0][1].ToString(), dt2.Rows[0][0].ToString(), dt2.Rows[0][2].ToString(), dt2.Rows[0][4].ToString());
                        else if (dem == 2)
                            hienthi(id2, TenCB[1], label2, 1, dt2.Rows[0][1].ToString(), dt2.Rows[0][0].ToString(), dt2.Rows[0][2].ToString(), dt2.Rows[0][4].ToString());
                        else if (dem == 3)
                            hienthi(id3, TenCB[2], label3, 2, dt2.Rows[0][1].ToString(), dt2.Rows[0][0].ToString(), dt2.Rows[0][2].ToString(), dt2.Rows[0][4].ToString());
                        else if (dem == 4)
                            hienthi(id4, TenCB[3], label4, 3, dt2.Rows[0][1].ToString(), dt2.Rows[0][0].ToString(), dt2.Rows[0][2].ToString(), dt2.Rows[0][4].ToString());
                        else if (dem == 5)
                            hienthi(id5, TenCB[4], label5, 4, dt2.Rows[0][1].ToString(), dt2.Rows[0][0].ToString(), dt2.Rows[0][2].ToString(), dt2.Rows[0][4].ToString());


                        BaoMatDien(TenCB[5], label9, dt2.Rows[0][2].ToString(), dt2.Rows[0][4].ToString());


                    }
                    else { ttkn_server = "BAD"; }
                }
                // cap nhat ia tri len web 
                ttkn_server = mysql.capnhatgiatriweb(label1.Text.Trim(), label2.Text.Trim(), label3.Text.Trim(), label4.Text.Trim(), label5.Text.Trim(), label9.Text.Trim());
            }
            catch { }
            timer1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            try
            {
                ghicsdl(label1, label2, label3, label4, label5, label9);
            }
            catch { }
            timer2.Enabled = true;

        }

    }
}
