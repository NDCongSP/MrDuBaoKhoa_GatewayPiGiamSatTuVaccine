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
public partial class taikhoan : System.Web.UI.Page
{

    MySqlConnection Con;
    MySqlCommand com;
    MySqlDataAdapter da;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            Label1.Text = ConfigurationManager.AppSettings["tendonvi"];
           
        }
        
    }
   
    protected void Button6_Click(object sender, EventArgs e)
    {
        
        Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoattytgiongtrombentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
        Con.Open();
        // truy van de lay cac thong so h cua tai khoan
        string data3 = "SELECT * FROM taikhoan";
        com = new MySqlCommand(data3, Con);
        com.ExecuteNonQuery();
        da = new MySqlDataAdapter(com);
        dt = new DataTable();
        da.Fill(dt);
        if (TextBox1.Text == "admin" && TextBox2.Text.Trim() == dt.Rows[0][1].ToString().Trim())
        {
            Session["Khoa"] = "Mo";
            Response.Redirect("Default.aspx");//khi dang nhap dung nhay vo trang hien thi
        }
        else
        {
            Session["Khoa"] = "Khoa";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng nhập lại!!!!');</script>");
        }
    }
   
}