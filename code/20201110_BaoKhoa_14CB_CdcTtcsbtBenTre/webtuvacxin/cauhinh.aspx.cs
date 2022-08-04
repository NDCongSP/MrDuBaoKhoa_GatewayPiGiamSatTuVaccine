using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Diagnostics;

public partial class cauhinh : System.Web.UI.Page
{
    MySqlConnection Con;
    MySqlCommand com;
    MySqlDataAdapter da;
    DataTable dt;
    string conStringLocal = "Server =localhost; Database = cauhinh ; Port=3306;User ID=root ;Password=100100;charset=utf8";

    //string pathFile = @"F:\0.Cong Profile\Documents\1.Others\Learn\ThamKhao\MyDocument\BaoKhoa\20201110_BaoKhoa_14CB_CdcTtcsbtBenTre\nhietdokhovacxin\nhietdokhovacxin\bin\Debug\Files\";
    string pathFile = "/home/pi/Files/";

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
    public int ExecuteNonQuery(string query, object[] parametter = null, [CallerMemberName] string memberCall = "", [CallerFilePath] string memberClass = "", [CallerLineNumber] int memberLine = 0)
    {
        int res = 0;

        try
        {
            using (MySqlConnection connection = new MySqlConnection(conStringLocal))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

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

                res = command.ExecuteNonQuery();

                connection.Close();
                connection.Dispose();
            }
        }
        catch { }
        return res;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Khoa"] == "Mo")
        {
            if (!IsPostBack)
            {
                Label10.Text = ConfigurationManager.AppSettings["tendonvi"];

                //Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoacdcbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
                //Con.Open();
                //// truy van de lay cac thong so hien len label,textbox
                //string data = "SELECT * FROM idds18b20";
                //com = new MySqlCommand(data, Con);
                //com.ExecuteNonQuery();
                //da = new MySqlDataAdapter(com);
                //dt = new DataTable();
                //da.Fill(dt);
                //Label1.Text = dt.Rows[0][0].ToString();
                //Label2.Text = dt.Rows[1][0].ToString();
                //Label3.Text = dt.Rows[2][0].ToString();
                //Label4.Text = dt.Rows[3][0].ToString();
                //Label5.Text = dt.Rows[4][0].ToString();
                //Label6.Text = dt.Rows[5][0].ToString();
                //Label7.Text = dt.Rows[6][0].ToString();
                //Label8.Text = dt.Rows[7][0].ToString();
                //Label9.Text = dt.Rows[8][0].ToString();
                //Label11.Text = dt.Rows[9][0].ToString();
                //Label12.Text = dt.Rows[10][0].ToString();
                //Label13.Text = dt.Rows[11][0].ToString();
                //Label14.Text = dt.Rows[12][0].ToString();
                //Label15.Text = dt.Rows[13][0].ToString();

                //TextBox1.Text = dt.Rows[0][1].ToString();
                //TextBox2.Text = dt.Rows[1][1].ToString();
                //TextBox3.Text = dt.Rows[2][1].ToString();
                //TextBox4.Text = dt.Rows[3][1].ToString();
                //TextBox5.Text = dt.Rows[4][1].ToString();
                //TextBox6.Text = dt.Rows[5][1].ToString();
                //TextBox7.Text = dt.Rows[6][1].ToString();
                //TextBox8.Text = dt.Rows[7][1].ToString();
                //TextBox9.Text = dt.Rows[8][1].ToString();
                //TextBox24.Text = dt.Rows[9][1].ToString();
                //TextBox29.Text = dt.Rows[10][1].ToString();

                //TextBox10.Text = dt.Rows[0][2].ToString();
                //TextBox11.Text = dt.Rows[1][2].ToString();
                //TextBox12.Text = dt.Rows[2][2].ToString();
                //TextBox13.Text = dt.Rows[3][2].ToString();
                //TextBox14.Text = dt.Rows[4][2].ToString();
                //TextBox15.Text = dt.Rows[5][2].ToString();
                //TextBox16.Text = dt.Rows[6][2].ToString();
                //TextBox17.Text = dt.Rows[7][2].ToString();
                //TextBox18.Text = dt.Rows[8][2].ToString();
                //TextBox28.Text = dt.Rows[9][2].ToString();
                //TextBox31.Text = dt.Rows[10][2].ToString();

                //TextBox35.Text = dt.Rows[11][2].ToString();
                //TextBox37.Text = dt.Rows[12][2].ToString();
                //TextBox39.Text = dt.Rows[13][2].ToString();
                //// truy van de lay thoi gian hien len text box
                //string data1 = "SELECT * FROM thoigiancapnhat";
                //com = new MySqlCommand(data1, Con);
                //com.ExecuteNonQuery();
                //da = new MySqlDataAdapter(com);
                //dt = new DataTable();
                //da.Fill(dt);
                //TextBox22.Text = dt.Rows[0][0].ToString();
                //TextBox30.Text = dt.Rows[0][1].ToString();
                //// truy van de lay cac du lieu trong bang
                //string data2 = "SELECT * FROM gioihannhietdo";
                //com = new MySqlCommand(data2, Con);
                //com.ExecuteNonQuery();
                //da = new MySqlDataAdapter(com);
                //dt = new DataTable();
                //da.Fill(dt);
                //TextBox23.Text = dt.Rows[0][2].ToString();
                //TextBox25.Text = dt.Rows[0][4].ToString();
                //TextBox26.Text = dt.Rows[0][0].ToString();
                //TextBox27.Text = dt.Rows[0][1].ToString();
                //TextBox32.Text = dt.Rows[0][5].ToString();
                //TextBox33.Text = dt.Rows[0][6].ToString();
                //Con.Close();

                //string[] lines = System.IO.File.ReadAllLines(pathFile + "idds18b20.txt");
                //Label1.Text = TextBox10.Text = lines[0].Split('|')[1];
                //Label2.Text = TextBox11.Text = lines[1].Split('|')[1];
                //Label3.Text = TextBox12.Text = lines[2].Split('|')[1];
                //Label4.Text = TextBox1.Text = lines[3].Split('|')[1];

                //CHINH CHO NAY
                string[] line = System.IO.File.ReadAllLines(pathFile + "idds18b20.txt");
                TextBox22.Text = line[0].Split('|')[1];

                Label1.Text = line[0].Split('|')[0];
                Label2.Text = line[1].Split('|')[0];
                Label3.Text = line[2].Split('|')[0];
                Label4.Text = line[3].Split('|')[0];
                Label5.Text = line[4].Split('|')[0];
                Label6.Text = line[5].Split('|')[0];
                Label7.Text = line[6].Split('|')[0];
                Label8.Text = line[7].Split('|')[0];
                Label9.Text = line[8].Split('|')[0];
                Label11.Text = line[9].Split('|')[0];
                Label12.Text = line[10].Split('|')[0];
                Label13.Text = line[11].Split('|')[0];
                Label14.Text = line[12].Split('|')[0];
                Label15.Text = line[13].Split('|')[0];

                TextBox1.Text = line[0].Split('|')[2];
                TextBox2.Text = line[1].Split('|')[2];
                TextBox3.Text = line[2].Split('|')[2];
                TextBox4.Text = line[3].Split('|')[2];
                TextBox5.Text = line[4].Split('|')[2];
                TextBox6.Text = line[5].Split('|')[2];
                TextBox7.Text = line[6].Split('|')[2];
                TextBox8.Text = line[7].Split('|')[2];
                TextBox9.Text = line[8].Split('|')[2];
                TextBox24.Text = line[9].Split('|')[2];
                TextBox29.Text = line[10].Split('|')[2];

                TextBox10.Text = line[0].Split('|')[1];
                TextBox11.Text = line[1].Split('|')[1];
                TextBox12.Text = line[2].Split('|')[1];
                TextBox13.Text = line[3].Split('|')[1];
                TextBox14.Text = line[4].Split('|')[1];
                TextBox15.Text = line[5].Split('|')[1];
                TextBox16.Text = line[6].Split('|')[1];
                TextBox17.Text = line[7].Split('|')[1];
                TextBox18.Text = line[8].Split('|')[1];
                TextBox28.Text = line[9].Split('|')[1];
                TextBox31.Text = line[10].Split('|')[1];

                TextBox35.Text = line[11].Split('|')[1];
                TextBox37.Text = line[12].Split('|')[1];
                TextBox39.Text = line[13].Split('|')[1];


                //string DataParametter = ReadText(@"D:\ATPro\CodeProject\GatewayPi\TTYTBaoKhoa\20201116_BaoKhoa_4CbTtytThanhPhuBenTrePi\nhietdokhovacxin\nhietdokhovacxin\bin\Debug\Files\ThoiGianCapNhat.txt").Trim();
                string DataParametter = ReadText(pathFile + "ThoiGianCapNhat.txt").Trim();
                TextBox22.Text = DataParametter.Split('|')[0];
                TextBox30.Text = DataParametter.Split('|')[1];

                //CHINH CHO NAY
                //DataParametter = ReadText(@"D:\ATPro\CodeProject\GatewayPi\TTYTBaoKhoa\20201116_BaoKhoa_4CbTtytThanhPhuBenTrePi\nhietdokhovacxin\nhietdokhovacxin\bin\Debug\Files\GioiHanNhietDo.txt").Trim();
                DataParametter = ReadText(pathFile + "GioiHanNhietDo.txt").Trim();
                TextBox23.Text = DataParametter.Split('|')[2];
                TextBox25.Text = DataParametter.Split('|')[3];
                TextBox26.Text = DataParametter.Split('|')[0];
                TextBox27.Text = DataParametter.Split('|')[1];
                TextBox32.Text = DataParametter.Split('|')[4];
                TextBox33.Text = DataParametter.Split('|')[5];
            }
        }
        else
        {
            Response.Redirect("~/taikhoan.aspx");
        }
    }
    private void capnhatid(TextBox mytextbox1, TextBox mytextbox2, Label mylabel)
    {
        try
        {
            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoacdcbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();

            string data = "1|" + TextBox10.Text + "|" + TextBox1.Text.Trim()
                + "\r\n2|" + TextBox11.Text + "|" + TextBox2.Text.Trim()
                + "\r\n3|" + TextBox12.Text + "|" + TextBox3.Text.Trim()
                + "\r\n4|" + TextBox13.Text + "|" + TextBox4.Text.Trim()
                + "\r\n5|" + TextBox14.Text + "|" + TextBox5.Text.Trim()
                + "\r\n6|" + TextBox15.Text + "|" + TextBox6.Text.Trim()
                + "\r\n7|" + TextBox16.Text + "|" + TextBox7.Text.Trim()
                + "\r\n8|" + TextBox17.Text + "|" + TextBox8.Text.Trim()
                + "\r\n9|" + TextBox18.Text + "|" + TextBox9.Text.Trim()
                + "\r\n10|" + TextBox28.Text + "|" + TextBox24.Text.Trim()
                + "\r\n11|" + TextBox31.Text + "|" + TextBox29.Text.Trim()
                + "\r\n12|" + TextBox35.Text + "|"
                + "\r\n13|" + TextBox37.Text + "|"
                + "\r\n14|" + TextBox39.Text + "|"
                + "\r\n15|Nguon Dien|";
            WriteText(data, pathFile + "idds18b20.txt");

            data = "update idds18b20 set DCID ='" + mytextbox1.Text.Trim() + "' , tencb='" + mytextbox2.Text.Trim() + "' where id='" + mylabel.Text.Trim() + "'";
            com = new MySqlCommand(data, Con);
            com.ExecuteNonQuery();
            Con.Close();

            ExecuteNonQuery(data);//cap nhat local
        }
        catch { }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox1, TextBox10, Label1);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["Khoa"] = "Khoa";
        Response.Redirect("~/taikhoan.aspx");
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox2, TextBox11, Label2);
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox3, TextBox12, Label3);
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox4, TextBox13, Label4);
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox5, TextBox14, Label5);
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox6, TextBox15, Label6);
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox7, TextBox16, Label7);
    }
    protected void Button13_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox8, TextBox17, Label8);
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        try
        {
            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoacdcbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            // truy van de lay cac thong so h cua tai khoan
            string data3 = "SELECT * FROM taikhoan";
            com = new MySqlCommand(data3, Con);
            com.ExecuteNonQuery();
            da = new MySqlDataAdapter(com);
            dt = new DataTable();
            da.Fill(dt);

            if (TextBox19.Text == dt.Rows[0][1].ToString())
            {
                if (TextBox20.Text.Trim() == TextBox21.Text.Trim())
                {

                    string data1 = "update taikhoan set pass ='" + TextBox20.Text.Trim() + "'  where user='admin '";
                    com = new MySqlCommand(data1, Con);
                    com.ExecuteNonQuery();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert(' Đổi password Thành Công !!!!');</script>");

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert(' Password mới không trùng!!!!');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert(' Password Sai!!!!');</script>");
            }
            Con.Close();
        }
        catch { }
    }
    protected void Button16_Click(object sender, EventArgs e)
    {
        try
        {

            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoacdcbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            // update thoi gian cap nhat du lieu
            string data = "update thoigiancapnhat set thoigian = '" + TextBox22.Text.Trim() + "',thoigiancoi= '" + TextBox30.Text.Trim() + "'";

            WriteText(TextBox22.Text.Trim() + "|" + TextBox30.Text.Trim(), pathFile + "ThoiGianCapNhat.txt");

            com = new MySqlCommand(data, Con);
            com.ExecuteNonQuery();
            Con.Close();
        }
        catch { }

    }
    protected void Button17_Click(object sender, EventArgs e)
    {
        try
        {
            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoacdcbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            // update sodt,email nhan canh bao
            string data = "update gioihannhietdo set sdtsms = '" + TextBox23.Text.Trim() + "',email = '" + TextBox25.Text.Trim() + "'";

            WriteText(TextBox26.Text.Trim() + "|" + TextBox27.Text.Trim()
                + "|" + TextBox23.Text.Trim() + "|" + TextBox25.Text.Trim()
                + "|" + TextBox32.Text.Trim() + "|" + TextBox33.Text.Trim(), pathFile + "GioiHanNhietDo.txt");

            com = new MySqlCommand(data, Con);
            com.ExecuteNonQuery();
            Con.Close();

            ExecuteNonQuery(data);//cap nhat local
        }
        catch { }
    }
    protected void Button18_Click(object sender, EventArgs e)
    {
        try
        {
            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoacdcbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            // update nguong canh bao nhiet do
            string data = "update gioihannhietdo set muccao = '" + TextBox26.Text.Trim() + "',mucthap = '" + TextBox27.Text.Trim() + "'";

            WriteText(TextBox26.Text.Trim() + "|" + TextBox27.Text.Trim()
               + "|" + TextBox23.Text.Trim() + "|" + TextBox25.Text.Trim()
               + "|" + TextBox32.Text.Trim() + "|" + TextBox33.Text.Trim(), pathFile + "GioiHanNhietDo.txt");

            com = new MySqlCommand(data, Con);
            com.ExecuteNonQuery();
            Con.Close();

            ExecuteNonQuery(data);//cap nhat local
        }
        catch { }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("alarm.aspx");
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("baocao.aspx");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("cauhinh.aspx");
    }


    protected void Button14_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox9, TextBox18, Label9);
    }

    protected void Button19_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox24, TextBox28, Label11);
    }

    protected void Button20_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox29, TextBox31, Label12);
    }

    protected void Button21_Click(object sender, EventArgs e)
    {
        try
        {
            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoacdcbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            // update nguong canh bao nhiet do
            string data = "update gioihannhietdo set caoamsau = '" + TextBox32.Text.Trim() + "',thapamsau = '" + TextBox33.Text.Trim() + "'";

            WriteText(TextBox26.Text.Trim() + "|" + TextBox27.Text.Trim()
               + "|" + TextBox23.Text.Trim() + "|" + TextBox25.Text.Trim()
               + "|" + TextBox32.Text.Trim() + "|" + TextBox33.Text.Trim(), pathFile + "GioiHanNhietDo.txt");

            com = new MySqlCommand(data, Con);
            com.ExecuteNonQuery();
            Con.Close();

            ExecuteNonQuery(data);//cap nhat local
        }
        catch { }
    }

    protected void Button22_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox29, TextBox35, Label13);
    }

    protected void Button23_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox29, TextBox37, Label14);
    }

    protected void Button24_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox29, TextBox39, Label15);
    }

    protected void Button25_Click(object sender, EventArgs e)
    {
        Restart();
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
}