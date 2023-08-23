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
using System.Runtime.CompilerServices;
using System.IO;

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

        static string id1, id2, id3, id4, id5, id6, id7, id8, id9, id10, id11;
        static int thoigian = 60000, dem = 0;

        static byte[] canhbao_cao = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static byte[] canhbao_thap = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static byte CanhBaoMatDien = 0;
        static byte[] chot_cao = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static byte[] chot_thap = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        static int baocoi = 0;
        static int reset1 = 0, reset2 = 0;

        static double qdcoi = 0;
        static bool qdgoi = false;
        static string ttkn_server = "", sodtgoi = "";
        static byte demmatketnoi_modbus = 0, ChotMatDien = 0, ChotBaoCoi = 0;

        static byte[] docHolding = { 0, 0 };
        static bool[] GhiOut = { false, false, false, false, false, false, false, false };
        static bool[] DocIn = { false, false, false, false, false, false, false, false };
        static bool[] DocOut = { false, false, false, false, false, false, false, false };
        static string[] TenCB = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        static string ThoiGianAlarm = "";

        static double offset1 = 0, offset2 = 0, offset3 = 0;

        #region Cong update 2020/11/10
        //add new
        static double hightLevel = 8, lowLevel = 2;
        static double hightLevelTu1 = 5, lowLevelTu1 = -25;
        static string sdtSms = null, email = null;
        static byte[] docHoldingNhietDo = { 0, 0, 0, 0, 0, 0 };
        static double nhietKenh12 = 0, nhietKenh13 = 0, nhietKenh14 = 0;
        static double nhietKenh12New = 0, nhietKenh13New = 0, nhietKenh14New = 0;
        static double giaTriVotLo = 10;
        static string NoiDung = "";

        //static string pathFile = "./Files/";
        static string pathFile = "/home/pi/Files/";

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
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("MOI CAP NHAT 16/12/2020");
                label22.Text = ConfigurationManager.AppSettings["tendonvi"];

                mysql.ChuoiKetnoiMySQL_local = ConfigurationManager.AppSettings["conStr"];

                ttkn_server = mysql.Ketnoi_local();
                if (ttkn_server == "GOOD")
                {
                    #region dng bo gio
                    string TimeNow = mysql.DongBoThoiGian();

                    if (TimeNow != "" && TimeNow != null)
                    {
                        myPLC.ThoiGian.CaiDat(Convert.ToDateTime(TimeNow).ToString("dd-MM-yyyy HH:mm:ss"));
                        Console.WriteLine("Data_dongbo bo thanh cong " + Convert.ToDateTime(TimeNow).ToString("dd-MM-yyyy HH:mm:ss"));
                    }
                    else
                        Console.WriteLine("Dong bo loi");
                    #endregion

                    label20.BackColor = Color.Green;

                }
                else
                {
                    label20.BackColor = Color.Red;
                }

                #region Cong update 2020/11/10. Đọc các giá trị cài đặt như ngưỡng cao thấp, thông tin gửi alarm

                #region truy van de lay id va ten cam bien  
                //dt = ExecuteQuery($"select * from idds18b20");
                //if (dt != null && dt.Rows.Count != 0)
                //{
                //    id1 = dt.Rows[0][1].ToString().Trim();
                //    id2 = dt.Rows[1][1].ToString().Trim();
                //    id3 = dt.Rows[2][1].ToString().Trim();
                //    id4 = dt.Rows[3][1].ToString().Trim();
                //    id5 = dt.Rows[4][1].ToString().Trim();
                //    id6 = dt.Rows[5][1].ToString().Trim();
                //    id7 = dt.Rows[6][1].ToString().Trim();
                //    id8 = dt.Rows[7][1].ToString().Trim();
                //    id9 = dt.Rows[8][1].ToString().Trim();
                //    id10 = dt.Rows[9][1].ToString().Trim();
                //    id11 = dt.Rows[10][1].ToString().Trim();

                //    label11.Text = TenCB[0] = dt.Rows[0][2].ToString();
                //    label12.Text = TenCB[1] = dt.Rows[1][2].ToString();
                //    label13.Text = TenCB[2] = dt.Rows[2][2].ToString();
                //    label14.Text = TenCB[3] = dt.Rows[3][2].ToString();
                //    label15.Text = TenCB[4] = dt.Rows[4][2].ToString();
                //    label16.Text = TenCB[5] = dt.Rows[5][2].ToString();
                //    label17.Text = TenCB[6] = dt.Rows[6][2].ToString();
                //    label18.Text = TenCB[7] = dt.Rows[7][2].ToString();
                //    label28.Text = TenCB[8] = dt.Rows[8][2].ToString();
                //    label29.Text = TenCB[9] = dt.Rows[9][2].ToString();
                //    label30.Text = TenCB[10] = dt.Rows[10][2].ToString();
                //    labCb12Value.Text = TenCB[11] = dt.Rows[11][2].ToString();
                //    labCb13Value.Text = TenCB[12] = dt.Rows[12][2].ToString();
                //    labCb14Value.Text = TenCB[13] = dt.Rows[13][2].ToString();

                //    label19.Text = TenCB[14] = dt.Rows[14][2].ToString();
                //}
                //else { ttkn_server = "BAD"; }
                //dt.Clear();
                //// doc thoi gian luu
                //dt = ExecuteQuery($"select * from thoigiancapnhat");
                //if (dt != null && dt.Rows.Count != 0)
                //{
                //    thoigian = (Convert.ToInt32(dt.Rows[0][0].ToString())) * 60000;//phut
                //    thoigiancoi = Convert.ToInt32(dt.Rows[0][1].ToString());//giay
                //}
                //else { ttkn_server = "BAD"; }
                //dt.Clear();

                //dt = ExecuteQuery($"select * from gioihannhietdo");
                //if (dt != null && dt.Rows.Count != 0)
                //{
                //    hightLevel = Convert.ToDouble(dt.Rows[0]["muccao"].ToString());
                //    lowLevel = Convert.ToDouble(dt.Rows[0]["mucthap"].ToString());
                //    sdtSms = dt.Rows[0]["sdtsms"].ToString();
                //    email = dt.Rows[0]["email"].ToString();

                //    hightLevelTu1 = Convert.ToDouble(dt.Rows[0]["caoamsau"].ToString());
                //    lowLevelTu1 = Convert.ToDouble(dt.Rows[0]["thapamsau"].ToString());
                //}
                #endregion

                #region dung textFile
                //CHINH CHO NAY
                //string[] lines = System.IO.File.ReadAllLines("./Files/idds18b20.txt");
                string[] lines = System.IO.File.ReadAllLines($"{pathFile}idds18b20.txt");
                label11.Text = TenCB[0] = lines[0].Split('|')[1];
                label12.Text = TenCB[1] = lines[1].Split('|')[1];
                label13.Text = TenCB[2] = lines[2].Split('|')[1];
                label14.Text = TenCB[3] = lines[3].Split('|')[1];
                label15.Text = TenCB[4] = lines[4].Split('|')[1];
                label16.Text = TenCB[5] = lines[5].Split('|')[1];
                label17.Text = TenCB[6] = lines[6].Split('|')[1];
                label18.Text = TenCB[7] = lines[7].Split('|')[1];
                label28.Text = TenCB[8] = lines[8].Split('|')[1];
                label29.Text = TenCB[9] = lines[9].Split('|')[1];
                label30.Text = TenCB[10] = lines[10].Split('|')[1];
                labCb12.Text = TenCB[11] = lines[11].Split('|')[1];
                labCb13.Text = TenCB[12] = lines[12].Split('|')[1];
                labCb14.Text = TenCB[13] = lines[13].Split('|')[1];
                label19.Text = TenCB[14] = lines[14].Split('|')[1];

                id1 = lines[0].Split('|')[2].Trim();
                id2 = lines[1].Split('|')[2].Trim();
                id3 = lines[2].Split('|')[2].Trim();
                id4 = lines[3].Split('|')[2].Trim();
                id5 = lines[4].Split('|')[2].Trim();
                id6 = lines[5].Split('|')[2].Trim();
                id7 = lines[6].Split('|')[2].Trim();
                id8 = lines[7].Split('|')[2].Trim();
                id9 = lines[8].Split('|')[2].Trim();
                id10 = lines[9].Split('|')[2].Trim();
                id11 = lines[10].Split('|')[2].Trim();

                Console.WriteLine("Ten CB:" + TenCB[0] + "|" + TenCB[1] + "|" + TenCB[2] + "|" + TenCB[3] + "|" + TenCB[4] + "|" + TenCB[5] + "|"
                    + TenCB[6] + "|" + TenCB[7] + "|" + TenCB[8] + "|" + TenCB[9] + "|" + TenCB[10] + "|"
                    + TenCB[11] + "|" + TenCB[12] + "|" + TenCB[13] + "|" + TenCB[14]);

                Console.WriteLine($"Kenh1:{myPLC.DS18B20.DocNhietDo(id1)}");

                //CHINH CHO NAY
                //string DataParametter = ReadText("./Files/ThoiGianCapNhat.txt").Trim();
                string DataParametter = ReadText($"{pathFile}ThoiGianCapNhat.txt").Trim();
                thoigian = (Convert.ToInt32(DataParametter.Split('|')[0])) * 60000;//phut
                thoigiancoi = Convert.ToInt32(DataParametter.Split('|')[1]);//giay

                Console.WriteLine("Thoi Gian cap nhat/coi:" + thoigian + "|" + thoigiancoi);

                //CHINH CHO NAY
                //DataParametter = ReadText("./Files/GioiHanNhietDo.txt").Trim();
                DataParametter = ReadText($"{pathFile}GioiHanNhietDo.txt").Trim();
                hightLevel = Convert.ToDouble(DataParametter.Split('|')[0]);
                lowLevel = Convert.ToDouble(DataParametter.Split('|')[1]);
                sdtSms = DataParametter.Split('|')[2];
                email = DataParametter.Split('|')[3];

                hightLevelTu1 = Convert.ToDouble(DataParametter.Split('|')[4]);
                lowLevelTu1 = Convert.ToDouble(DataParametter.Split('|')[5]);

                Console.WriteLine("Hight:{0}|Low:{1}|Hight1:{2}|Low:{3}|SDT:{4}|Email:{5}",hightLevel,lowLevel,hightLevelTu1,lowLevelTu1,sdtSms,email);

                //doc gia tri offset
                //CHINH CHO NAY
                //DataParametter = ReadText("./Files/Offset.txt").Trim();
                DataParametter = ReadText($"{pathFile}Offset.txt").Trim();
                offset1 = Convert.ToDouble(DataParametter.Split('|')[0]);
                offset2 = Convert.ToDouble(DataParametter.Split('|')[1]);
                offset3 = Convert.ToDouble(DataParametter.Split('|')[2]);
                Console.WriteLine("Offset1:{0}|Offset2:{1}|Offset3:{2}",offset1,offset2,offset3);
                #endregion
                #endregion


                #region ket noi 485
                myPLC.ModbusRTUMaster.ResponseTimeout = 1000;
                if (myPLC.ModbusRTUMaster.KetNoi("/dev/ttyUSB0", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
                //if (myPLC.ModbusRTUMaster.KetNoi("COM2", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One) == true)
                {
                    demmatketnoi_modbus = 0;
                    Console.WriteLine("Ket noi modbus thanh cong");
                    label10.BackColor = Color.Green;
                    #region ghi modbus ngo ra xuat coi
                    GhiOut[0] = false;
                    if (myPLC.ModbusRTUMaster.WriteMultipleCoils(1, 0, 1, GhiOut) == true)
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

                    //update 20201013
                    #region doc nhiet do tu 485 ban dau
                    if (myPLC.ModbusRTUMaster.ReadHoldingRegisters(4, 0, 3, ref docHoldingNhietDo) == true)
                    {
                        demmatketnoi_modbus = 0;

                        nhietKenh12 = nhietKenh12New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 0)) / 10, 2) + offset1;
                        nhietKenh13 = nhietKenh13New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 2)) / 10, 2) + offset2;
                        nhietKenh14 = nhietKenh14New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 4)) / 10, 2) + offset3;

                        Console.WriteLine("doc nhiet do ban dau thanh cong {0}/{1}/{2}", nhietKenh12.ToString(), nhietKenh13.ToString(), nhietKenh14.ToString());
                    }
                    else
                    {
                        demmatketnoi_modbus++;
                        if (demmatketnoi_modbus >= 10)
                        {
                            demmatketnoi_modbus = 10;
                        }
                        Console.WriteLine("doc nhiet do ban dau that bai");
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
            catch { Console.WriteLine("PageLoad Catch"); }
            var ct1 = Task.Factory.StartNew(() => reconnect_server());
            timer2.Interval = thoigian;
            timer2.Enabled = true;
            timer1.Enabled = true;
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

        public void WriteText(string Text, string PathFile)
        {
            FileStream fs = new FileStream(PathFile, FileMode.Create, FileAccess.Write, FileShare.None);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
            sw.WriteLine(Text);
            sw.Flush();
            sw.Close();
            fs.Close();
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
                        NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNguong thap: " + thap + ". \nNguong cao: " + cao + ". \nNhiet do hien tai: " + mylabel.Text + ". \nCanh bao: Vuot nguong cao";
                        sendemail(NoiDung, dcemail);
                        // gui sms
                        //myPLC.SMS.GuiSMS(sdtsms, NoiDung);
                        //Thread.Sleep(3000);
                        for (int j = 0; j < sdtsms.Split(',').Length; j++)
                        {
                            //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                            Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                            Thread.Sleep(2000);
                        }
                    }
                    else if (canhbao_cao[a] == 0 && chot_cao[a] == 1)
                    {
                        //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNhiet do da ve binh thuong";
                        //gui email
                        sendemail(NoiDung, dcemail);
                        // gui sms
                        //myPLC.SMS.GuiSMS(sdtsms, NoiDung);
                        //Thread.Sleep(3000);
                        for (int j = 0; j < sdtsms.Split(',').Length; j++)
                        {
                            //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                            Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                            Thread.Sleep(2000);
                        }
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
                        NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNguong thap: " + thap + ". \nNguong cao: " + cao + ". \nNhiet do hien tai: " + mylabel.Text + ". \nCanh bao: Vuot nguong thap";
                        //gui e mail
                        sendemail(NoiDung, dcemail);
                        // gui sms
                        //myPLC.SMS.GuiSMS(sdtsms, NoiDung);
                        //Thread.Sleep(3000);
                        for (int j = 0; j < sdtsms.Split(',').Length; j++)
                        {
                            //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                            Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                            Thread.Sleep(2000);
                        }

                    }
                    else if (canhbao_thap[a] == 0 && chot_thap[a] == 1)
                    {
                        //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNhiet do da ve binh thuong";
                        //gui email
                        sendemail(NoiDung, dcemail);
                        // gui sms
                        //myPLC.SMS.GuiSMS(sdtsms, NoiDung);
                        //Thread.Sleep(3000);
                        for (int j = 0; j < sdtsms.Split(',').Length; j++)
                        {
                            //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                            Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                            Thread.Sleep(2000);
                        }
                        chot_thap[a] = 0;
                    }
                    #endregion
                }
            }
            catch { }
        }
        private void hienthi1(string tempValue, string TenCB, Label mylabel, int a, string thap, string cao, string sdtsms, string dcemail)
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
                            Console.WriteLine("canh bao vuot nguong cao modbus");
                            //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                            chot_cao[a] = 1;
                            //cap nhat bang alarm
                            ttkn_server = mysql.insert_cmd("alarm", "now(),'" + TenCB + "','" + thap + "','" + mylabel.Text + "','" + cao + "','Vuot nguong cao'");
                            //gui e mai
                            NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNguong thap: " + thap + ". \nNguong cao: " + cao + ". \nNhiet do hien tai: " + mylabel.Text + ". \nCanh bao: Vuot nguong cao";
                            sendemail(NoiDung, dcemail);
                            // gui sms
                            //myPLC.SMS.GuiSMS(sdtsms, NoiDung);
                            //Thread.Sleep(3000);
                            for (int j = 0; j < sdtsms.Split(',').Length; j++)
                            {
                                //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                                Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                                Thread.Sleep(2000);
                            }
                        }
                        else if (canhbao_cao[a] == 0 && chot_cao[a] == 1)
                        {
                            //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                            NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNhiet do da ve binh thuong";
                            //gui email
                            sendemail(NoiDung, dcemail);
                            // gui sms
                            //myPLC.SMS.GuiSMS(sdtsms, NoiDung);
                            //Thread.Sleep(3000);
                            for (int j = 0; j < sdtsms.Split(',').Length; j++)
                            {
                                //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                                Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                                Thread.Sleep(2000);
                            }
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

                            NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNguong thap: " + thap + ". \nNguong cao: " + cao + ". \nNhiet do hien tai: " + mylabel.Text + ". \nCanh bao: Vuot nguong thap";
                            //gui e mail
                            sendemail(NoiDung, dcemail);
                            // gui sms
                            //myPLC.SMS.GuiSMS(sdtsms, NoiDung);
                            //Thread.Sleep(3000);
                            for (int j = 0; j < sdtsms.Split(',').Length; j++)
                            {
                                //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                                Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                                Thread.Sleep(2000);
                            }
                        }
                        else if (canhbao_thap[a] == 0 && chot_thap[a] == 1)
                        {
                            //ThoiGianAlarm = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                            NoiDung = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ". \n" + TenCB + ". \nNhiet do da ve binh thuong";
                            sendemail(NoiDung, dcemail);
                            // gui sms
                            //myPLC.SMS.GuiSMS(sdtsms, NoiDung);
                            //Thread.Sleep(3000);
                            for (int j = 0; j < sdtsms.Split(',').Length; j++)
                            {
                                //Parametter.ModbusRTU.SMS.GuiSMS(Parametter.sms.Split(',')[j], Parametter.Name[index] + ": High Alarm");
                                Console.WriteLine(myPLC.SMS.GuiSMS(sdtsms.Split(',')[j], NoiDung));
                                Thread.Sleep(2000);
                            }
                            chot_thap[a] = 0;
                        }
                        #endregion
                    }
                }
                else
                {
                    mylabel.Text = "BAD";
                }

                Console.WriteLine($"Trang thai canh bao: {chot_thap[11]}|{chot_thap[12]}|{chot_thap[13]}");
            }
            catch { }
        }
        private void DocNhietDo()
        {
            #region doc nhiet do tu 485
            if (myPLC.ModbusRTUMaster.ReadHoldingRegisters(4, 0, 3, ref docHoldingNhietDo) == true)
            {
                demmatketnoi_modbus = 0;

                //sua chỗ này. sẽ kiểm tra nếu nhiệt độ vọt lố thì sẽ ko cho nhảy
                //nhietKenh1 = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 0)) / 10, 2);
                //nhietKenh2 = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 2)) / 10, 2);
                //nhietKenh3 = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 4)) / 10, 2);

                nhietKenh12New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 0)) / 10, 2) + offset1;
                nhietKenh13New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 2)) / 10, 2) + offset2;
                nhietKenh14New = Math.Round(Convert.ToDouble(myPLC.GetShortAt(docHoldingNhietDo, 4)) / 10, 2) + offset3;

                if (nhietKenh12New <= nhietKenh12 + giaTriVotLo)
                {
                    nhietKenh12 = nhietKenh12New;
                }

                if (nhietKenh13New <= nhietKenh13 + giaTriVotLo)
                {
                    nhietKenh13 = nhietKenh13New;
                }

                if (nhietKenh14New <= nhietKenh14 + giaTriVotLo)
                {
                    nhietKenh14 = nhietKenh14New;
                }

                Console.WriteLine($"doc nhiet do thanh cong {nhietKenh12.ToString()}|{nhietKenh13.ToString()}|{nhietKenh14.ToString()}");
            }
            else
            {
                demmatketnoi_modbus++;
                if (demmatketnoi_modbus >= 10)
                {
                    demmatketnoi_modbus = 10;
                }
                Console.WriteLine("doc nhiet do that bai");
            }
            #endregion
        }
        private void ghicsdl(Label mylabel1, Label mylabel2, Label mylabel3, Label mylabel4, Label mylabel5, Label mylabel6, Label mylabel7,
            Label mylabel8, Label mylabel25, Label mylabel26, Label mylabel27, Label cb12, Label cb13, Label cb14, Label mylabel9)
        {
            try
            {
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[0] + "','" + mylabel1.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[1] + "','" + mylabel2.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[2] + "','" + mylabel3.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[3] + "','" + mylabel4.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[4] + "','" + mylabel5.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[5] + "','" + mylabel6.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[6] + "','" + mylabel7.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[7] + "','" + mylabel8.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[8] + "','" + mylabel25.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[9] + "','" + mylabel26.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[10] + "','" + mylabel27.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[11] + "','" + cb12.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[12] + "','" + cb13.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[13] + "','" + cb14.Text + "'");
                ttkn_server = mysql.insert_cmd("xuatbaocao", "now(),'" + TenCB[14] + "','" + mylabel9.Text + "'");
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
                baocoi = canhbao_cao[0] + canhbao_cao[1] + canhbao_cao[2] + canhbao_cao[3] + canhbao_cao[4] + canhbao_cao[5] + canhbao_cao[6] + canhbao_cao[7] + canhbao_cao[8] + canhbao_cao[9] + canhbao_cao[10] + canhbao_cao[11] + canhbao_cao[12] + canhbao_cao[13] +
                    canhbao_thap[0] + canhbao_thap[1] + canhbao_thap[2] + canhbao_thap[3] + canhbao_thap[4] + canhbao_thap[5] + canhbao_thap[6] + canhbao_thap[7] + canhbao_thap[8] + canhbao_thap[9] + canhbao_thap[10]
                    + canhbao_thap[11] + canhbao_thap[12] + canhbao_thap[13] + CanhBaoMatDien;
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
                        GhiOut[0] = true;
                        if (myPLC.ModbusRTUMaster.WriteMultipleCoils(1, 0, 1, GhiOut) == true)
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
                    GhiOut[0] = false;
                    if (myPLC.ModbusRTUMaster.WriteMultipleCoils(1, 0, 1, GhiOut) == true)
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
            if (dem < 12)
            {
                dem++;
            }
            else
            {
                dem = 1;
            }

             try
            {
                DocNhietDo();

                if (dem == 1)
                {
                    //hienthi(id1, TenCB[0], label1, 0, dt2.Rows[0][1].ToString(), dt2.Rows[0][0].ToString(), dt2.Rows[0][2].ToString(), dt2.Rows[0][4].ToString());
                    //tu 1 am sau nen dung 1 nguong rieng
                    hienthi(id1, TenCB[0], label1, 0, lowLevelTu1.ToString(), hightLevelTu1.ToString(), sdtSms, email);//tu 1 dung nguong canh bao  rieng

                    hienthi1(nhietKenh12.ToString(), TenCB[11], labCb12Value, 11, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                else if (dem == 2)
                {
                    hienthi(id2, TenCB[1], label2, 1, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);

                    hienthi1(nhietKenh13.ToString(), TenCB[12], labCb13Value, 12, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                else if (dem == 3)
                {
                    hienthi(id3, TenCB[2], label3, 2, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);

                    hienthi1(nhietKenh14.ToString(), TenCB[13], labCb14Value, 13, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                else if (dem == 4)
                {
                    hienthi(id4, TenCB[3], label4, 3, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);

                    hienthi1(nhietKenh12.ToString(), TenCB[11], labCb12Value, 11, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                else if (dem == 5)
                {
                    hienthi(id5, TenCB[4], label5, 4, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi1(nhietKenh13.ToString(), TenCB[12], labCb13Value, 12, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                else if (dem == 6)
                {
                    hienthi(id6, TenCB[5], label6, 5, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi1(nhietKenh14.ToString(), TenCB[13], labCb14Value, 13, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                else if (dem == 7)
                {
                    hienthi(id7, TenCB[6], label7, 6, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi1(nhietKenh12.ToString(), TenCB[11], labCb12Value, 11, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                else if (dem == 8)
                {
                    hienthi(id8, TenCB[7], label8, 7, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi1(nhietKenh13.ToString(), TenCB[12], labCb13Value, 12, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                //
                else if (dem == 9)
                {
                    hienthi(id9, TenCB[8], label25, 8, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi1(nhietKenh14.ToString(), TenCB[13], labCb14Value, 13, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                else if (dem == 10)
                {
                    hienthi(id10, TenCB[9], label26, 9, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi1(nhietKenh12.ToString(), TenCB[11], labCb12Value, 11, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }
                else if (dem == 11)
                {
                    hienthi(id11, TenCB[10], label27, 10, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                    hienthi1(nhietKenh13.ToString(), TenCB[12], labCb13Value, 12, lowLevel.ToString(), hightLevel.ToString(), sdtSms, email);
                }

                BaoMatDien(TenCB[14], label9, sdtSms, email);

                // cap nhat ia tri len web 
                ttkn_server = mysql.capnhatgiatriweb(label1.Text.Trim(), label2.Text.Trim(), label3.Text.Trim(), label4.Text.Trim(), label5.Text.Trim(), label6.Text.Trim(), label7.Text.Trim(),
                    label8.Text.Trim(), label25.Text.Trim(), label26.Text.Trim(), label27.Text.Trim(), labCb12Value.Text.Trim(), labCb13Value.Text.Trim(), labCb14Value.Text.Trim(), label9.Text.Trim());
            }
            catch { }
            timer1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            try
            {
                ghicsdl(label1, label2, label3, label4, label5, label6, label7, label8, label25, label26, label27, labCb12Value, labCb13Value, labCb14Value, label9);
            }
            catch { }
            timer2.Enabled = true;

        }

    }
}
