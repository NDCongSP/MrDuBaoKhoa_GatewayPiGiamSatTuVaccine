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

public partial class cauhinh : System.Web.UI.Page
{
    MySqlConnection Con;
    MySqlCommand com;
    MySqlDataAdapter da;
    DataTable dt;

    string conStringLocal = "Server =localhost; Database = cauhinh ; Port=3306;User ID=root ;Password=100100;charset=utf8";

    //static string pathFile = @"D:\ATPro\CodeProject\GatewayPi\TTYTBaoKhoa\20201121_BaoKhoa_3CbTtytChoLachBenTrePi\nhietdokhovacxin\nhietdokhovacxin\bin\Debug\Files\";
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Khoa"] == "Mo")
        {
            if (!IsPostBack)
            {
                Label10.Text = ConfigurationManager.AppSettings["tendonvi"];
                // truy van de lay cac du lieu trong bang

                //using (MySqlConnection connection = new MySqlConnection("Server =45.119.212.41 ; Database = cauhinh ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8"))

                //dt = ExecuteQuery("SELECT * FROM gioihannhietdo");
                //if (dt != null && dt.Rows.Count != 0)
                //{
                //    TextBox23.Text = dt.Rows[0][2].ToString();
                //    TextBox25.Text = dt.Rows[0][4].ToString();
                //    TextBox26.Text = dt.Rows[0][0].ToString();
                //    TextBox27.Text = dt.Rows[0][1].ToString();
                //}
                //dt.Clear();

                //// truy van de lay cac thong so hien len label,textbox
                //dt = ExecuteQuery("SELECT * FROM idds18b20");
                //if (dt != null && dt.Rows.Count != 0)
                //{
                //    Label1.Text = dt.Rows[0][0].ToString();
                //    Label2.Text = dt.Rows[1][0].ToString();
                //    Label3.Text = dt.Rows[2][0].ToString();

                //    TextBox10.Text = dt.Rows[0][2].ToString();
                //    TextBox11.Text = dt.Rows[1][2].ToString();
                //    TextBox12.Text = dt.Rows[2][2].ToString();
                //}
                //dt.Clear();

                //// truy van de lay thoi gian hien len text box
                //dt = ExecuteQuery("SELECT * FROM thoigiancapnhat");
                //if (dt != null && dt.Rows.Count != 0)
                //{
                //    TextBox22.Text = dt.Rows[0][0].ToString();
                //    TextBox30.Text = dt.Rows[0][1].ToString();
                //}
                //dt.Clear();

                //CHINH CHO NAY
                string[] lines = System.IO.File.ReadAllLines(pathFile + "idds18b20.txt");
                Label1.Text = TextBox10.Text = lines[0].Split('|')[1];
                Label2.Text = TextBox11.Text = lines[1].Split('|')[1];
                Label3.Text = TextBox12.Text = lines[2].Split('|')[1];

                //CHINH CHO NAY
                string DataParametter = ReadText(pathFile + "ThoiGianCapNhat.txt").Trim();
                TextBox22.Text = DataParametter.Split('|')[0];
                TextBox30.Text = DataParametter.Split('|')[1];

                //CHINH CHO NAY
                DataParametter = ReadText(pathFile + "GioiHanNhietDo.txt").Trim();
                TextBox23.Text = DataParametter.Split('|')[2];
                TextBox25.Text = DataParametter.Split('|')[3];
                TextBox26.Text = DataParametter.Split('|')[0];
                TextBox27.Text = DataParametter.Split('|')[1];
            }
        }
        else
        {
            Response.Redirect("~/taikhoan.aspx");
        }
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
    private void capnhatid(TextBox mytextbox2, Label mylabel)
    {
        try
        {
            string data = "1|" + TextBox10.Text + "\r\n2|" + TextBox11.Text + "\r\n3|" + TextBox12.Text + "\r\n4|Nguon Dien";

            //WriteText(data, @"D:\ATPro\CodeProject\GatewayPi\TTYTBaoKhoa\20201116_BaoKhoa_4CbTtytThanhPhuBenTrePi\nhietdokhovacxin\nhietdokhovacxin\bin\Debug\Files\idds18b20.txt");
            WriteText(data, pathFile + "idds18b20.txt");

            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoattytcholachbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();

            //cap nhat server
            com = new MySqlCommand(data, Con);
            com.ExecuteNonQuery();
            Con.Close();
        }
        catch { }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox10, Label1);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["Khoa"] = "Khoa";
        Response.Redirect("~/taikhoan.aspx");
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox11, Label2);
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        capnhatid(TextBox12, Label3);
    }
    protected void Button15_Click(object sender, EventArgs e)
    {
        try
        {
            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoattytcholachbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
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

            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoattytcholachbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
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

    //Cài đặt thông số gửi alarm
    protected void Button17_Click(object sender, EventArgs e)
    {
        try
        {
            string data = "update gioihannhietdo set sdtsms = '" + TextBox23.Text.Trim() + "',email = '" + TextBox25.Text.Trim() + "'";

            //WriteText(TextBox26.Text.Trim() + "|" + TextBox27.Text.Trim() + "|" + TextBox23.Text.Trim() + "|" + TextBox25.Text.Trim(), "/home/pi/GioiHanNhietDo.txt");
            //WriteText(TextBox26.Text.Trim() + "|" + TextBox27.Text.Trim() + "|" + TextBox23.Text.Trim() + "|" + TextBox25.Text.Trim()
            //    , @"D:\ATPro\CodeProject\GatewayPi\TTYTBaoKhoa\20201116_BaoKhoa_4CbTtytThanhPhuBenTrePi\nhietdokhovacxin\nhietdokhovacxin\bin\Debug\Files\GioiHanNhietDo.txt");
            WriteText(TextBox26.Text.Trim() + "|" + TextBox27.Text.Trim() + "|" + TextBox23.Text.Trim() + "|" + TextBox25.Text.Trim(), pathFile + "GioiHanNhietDo.txt");


            ExecuteNonQuery(data);//cap nhat local

            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoattytcholachbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            // update sodt,email nhan canh bao
            com = new MySqlCommand(data, Con);
            com.ExecuteNonQuery();
            Con.Close();
        }
        catch { }
    }

    //cai dat muc cao thap
    protected void Button18_Click(object sender, EventArgs e)
    {
        try
        {
            string data = "update gioihannhietdo set muccao = '" + TextBox26.Text.Trim() + "',mucthap = '" + TextBox27.Text.Trim() + "'";

            WriteText(TextBox26.Text.Trim() + "|" + TextBox27.Text.Trim() + "|" + TextBox23.Text.Trim() + "|" + TextBox25.Text.Trim(), pathFile + "GioiHanNhietDo.txt");
            //WriteText(TextBox26.Text.Trim() + "|" + TextBox27.Text.Trim() + "|" + TextBox23.Text.Trim() + "|" + TextBox25.Text.Trim()
            //    , @"D:\ATPro\CodeProject\GatewayPi\TTYTBaoKhoa\20201116_BaoKhoa_4CbTtytThanhPhuBenTrePi\nhietdokhovacxin\nhietdokhovacxin\bin\Debug\Files\GioiHanNhietDo.txt");

            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoattytcholachbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            // update sodt,email nhan canh bao
            com = new MySqlCommand(data, Con);
            com.ExecuteNonQuery();
            Con.Close();
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


    protected void Button9_Click(object sender, EventArgs e)
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