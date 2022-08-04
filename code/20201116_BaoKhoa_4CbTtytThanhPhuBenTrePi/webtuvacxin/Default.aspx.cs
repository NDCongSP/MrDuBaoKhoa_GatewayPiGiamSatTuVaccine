using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Reflection;

public partial class _Default : System.Web.UI.Page
{
    MySqlConnection Con;
    MySqlCommand com;
    MySqlDataAdapter da;
    DataTable dt;
    static byte[] canhbao_cao = { 0, 0, 0, 0, 0, 0 };
    static byte[] canhbao_thap = { 0, 0, 0, 0, 0, 0};
    static byte[] canhbao_khoi = { 0, 0 };
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Khoa"] == "Mo")
        {
            if (!IsPostBack)
            {
                Label25.Text = ConfigurationManager.AppSettings["tendonvi"];
                Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoattytthanhphubentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
                Con.Open();
                // truy van de lay ten cam bien HIEN THI LEN LABELL 
                string data = "SELECT * FROM idds18b20";
                com = new MySqlCommand(data, Con);
                com.ExecuteNonQuery();
                da = new MySqlDataAdapter(com);
                dt = new DataTable();
                da.Fill(dt);
                Label11.Text = dt.Rows[0][2].ToString();
                Label12.Text = dt.Rows[1][2].ToString();
                Label13.Text = dt.Rows[2][2].ToString();
                Label26.Text = dt.Rows[3][2].ToString();
                Label14.Text = dt.Rows[4][2].ToString();

            }
        }
        else
        {
            Response.Redirect("~/taikhoan.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["Khoa"] = "Khoa";
        Response.Redirect("~/taikhoan.aspx");
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        
        Timer1.Enabled = false;
        try
        {
            MySqlConnection _Con = new MySqlConnection("Server =45.119.212.41 ; Database = ctybaokhoattytthanhphubentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8");
            _Con.Open();
            string data = "SELECT * FROM hienthiweb";
            MySqlCommand _com = new MySqlCommand(data, _Con);
            _com.ExecuteNonQuery();
            MySqlDataAdapter _da = new MySqlDataAdapter(_com);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            Label1.Text = _dt.Rows[0][0].ToString();
            Label2.Text = _dt.Rows[0][1].ToString();
            Label3.Text = _dt.Rows[0][2].ToString();
            Label27.Text = _dt.Rows[0][3].ToString();
            Label4.Text = _dt.Rows[0][4].ToString();
            string data1 = "SELECT * FROM gioihannhietdo";
            _com = new MySqlCommand(data1, _Con);
            _com.ExecuteNonQuery();
            _da = new MySqlDataAdapter(_com);
            _dt = new DataTable();
            _da.Fill(_dt);
            hienthi(Label1, _dt.Rows[0][1].ToString(), _dt.Rows[0][0].ToString(),0);
            hienthi(Label2, _dt.Rows[0][1].ToString(), _dt.Rows[0][0].ToString(),1);
            hienthi(Label3, _dt.Rows[0][1].ToString(), _dt.Rows[0][0].ToString(),2);
            hienthi(Label27, _dt.Rows[0][1].ToString(), _dt.Rows[0][0].ToString(),2);
            
            khoi(Label4);
            _Con.Close();
        }
        catch { }
        Timer1.Enabled = true;
    }
    private void hienthi9(Label mylabel, string caophong,int a)
    {
        try
        {
            #region xet dieu kien de dua ra tin hieu canh bao
            if (Convert.ToDouble(mylabel.Text) > (Convert.ToDouble(caophong) + 0.2))
            {
                canhbao_cao[a] = 1;
            }
            else if (Convert.ToDouble(mylabel.Text) < (Convert.ToDouble(caophong) - 0.2))
            {
                canhbao_cao[a] = 0;
            }
            if (canhbao_cao[a] == 1)
            {
                mylabel.BackColor = Color.Red;
            }
            else
            {
                mylabel.BackColor = Color.GreenYellow;
            }
            #endregion
        }
        catch { }
    }
    private void hienthi(Label mylabel,string thap, string cao,int a)
    {
        try
        {
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
        }
        catch {  }
    }
    private void khoi(Label mylabel)
    {
        try
        {
            if (mylabel.Text.Trim() == "GOOD")
            {
                mylabel.BackColor = Color.GreenYellow;
            }
            if (mylabel.Text.Trim() == "ALARM")
            {
                mylabel.BackColor = Color.Red;
            }
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
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("cauhinh.aspx");
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
