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

public partial class cauhinh : System.Web.UI.Page
{
    MySqlConnection Con;
    MySqlCommand com;
    MySqlDataAdapter da;
    DataTable dt;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Khoa"] == "Mo")
        {
            if (!IsPostBack)
            {
                Label10.Text = ConfigurationManager.AppSettings["tendonvi"];
                Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoabvculaominhbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
                Con.Open();
                // truy van de lay cac thong so hien len label,textbox
                string data = "SELECT * FROM idds18b20";
                com = new MySqlCommand(data, Con);
                com.ExecuteNonQuery();
                da = new MySqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                Label1.Text = dt.Rows[0][0].ToString();
                Label2.Text = dt.Rows[1][0].ToString();
                Label3.Text = dt.Rows[2][0].ToString();
                Label4.Text = dt.Rows[3][0].ToString();
                Label5.Text = dt.Rows[4][0].ToString();

                TextBox1.Text = dt.Rows[0][1].ToString();
                TextBox2.Text = dt.Rows[1][1].ToString();
                TextBox3.Text = dt.Rows[2][1].ToString();
                TextBox4.Text = dt.Rows[3][1].ToString();
                TextBox5.Text = dt.Rows[4][1].ToString();

                TextBox10.Text = dt.Rows[0][2].ToString();
                TextBox11.Text = dt.Rows[1][2].ToString();
                TextBox12.Text = dt.Rows[2][2].ToString();
                TextBox13.Text = dt.Rows[3][2].ToString();
                TextBox14.Text = dt.Rows[4][2].ToString();
                // truy van de lay thoi gian hien len text box
                string data1 = "SELECT * FROM thoigiancapnhat";
                com = new MySqlCommand(data1, Con);
                com.ExecuteNonQuery();
                da = new MySqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                TextBox22.Text = dt.Rows[0][0].ToString();
                TextBox30.Text = dt.Rows[0][1].ToString();
                // truy van de lay cac du lieu trong bang
                string data2 = "SELECT * FROM gioihannhietdo";
                com = new MySqlCommand(data2, Con);
                com.ExecuteNonQuery();
                da = new MySqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                TextBox23.Text = dt.Rows[0][2].ToString();
                TextBox25.Text = dt.Rows[0][4].ToString();
                TextBox26.Text = dt.Rows[0][0].ToString();
                TextBox27.Text = dt.Rows[0][1].ToString();
                Con.Close();
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
            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoabvculaominhbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            string data = "update idds18b20 set DCID ='" + mytextbox1.Text.Trim() + "' , tencb='" + mytextbox2.Text.Trim() + "' where id='" + mylabel.Text.Trim() + "'";
            com = new MySqlCommand(data, Con);
            com.ExecuteNonQuery();
            Con.Close();
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
    protected void Button15_Click(object sender, EventArgs e)
    {
        try
        {
            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoabvculaominhbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
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

            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoabvculaominhbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            // update thoi gian cap nhat du lieu
            string data = "update thoigiancapnhat set thoigian = '" + TextBox22.Text.Trim() + "',thoigiancoi= '" + TextBox30.Text.Trim() + "'";
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
            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoabvculaominhbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            // update sodt,email nhan canh bao
            string data = "update gioihannhietdo set sdtsms = '" + TextBox23.Text.Trim() + "',email = '" + TextBox25.Text.Trim() + "'";
            com = new MySqlCommand(data, Con);
            com.ExecuteNonQuery();
            Con.Close();
        }
        catch { }
    }
    protected void Button18_Click(object sender, EventArgs e)
    {
        try
        {
            Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoabvculaominhbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            Con.Open();
            // update nguong canh bao nhiet do
            string data = "update gioihannhietdo set muccao = '" + TextBox26.Text.Trim() + "',mucthap = '" + TextBox27.Text.Trim() + "'";
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
    
}