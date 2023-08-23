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


public partial class alarm : System.Web.UI.Page
{
    MySqlConnection Con;
    MySqlCommand com;
    MySqlDataAdapter da;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Khoa"] == "Mo")
        {

            Calendar1.Visible = false;
            Calendar2.Visible = false;
        }
        else
        {
            Response.Redirect("~/taikhoan.aspx");
        }
        if (!IsPostBack)
        {
            Label1.Text = ConfigurationManager.AppSettings["tendonvi"];
        }
    }



    protected void Button7_Click(object sender, EventArgs e)
    {
        // xuat file exccl
        try
        {
            if (GridView1.Rows.Count != 0)
            {
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename= alarm.xls");
                Response.ContentType = "application/vnd.xls";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GridView1.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Xuất Alarm trước để có dữ liệu rồi mới xuất file Excel  !!!!');</script>");
            }
        }
        catch { }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        try
        {


            if (TextBox1.Text != "" || TextBox2.Text != "")
            {
                if (Convert.ToDateTime(TextBox2.Text) > Convert.ToDateTime(TextBox1.Text))
                {
                    Con = new MySqlConnection(ConfigurationManager.AppSettings["conStr"]);
                    Con.Open();

                    string data1 = "SELECT * FROM alarm WHERE thoigian >= '" + TextBox1.Text + "' AND thoigian <= '" + TextBox2.Text + "'";
                    com = new MySqlCommand(data1, Con);
                    com.ExecuteNonQuery();
                    da = new MySqlDataAdapter(com);
                    dt = new DataTable();
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    Con.Close();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Thời gian đầu không thể lớn hơn thời gian cuối  !!!!');</script>");
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Chưa chọn đủ các mốc thời gian  !!!!');</script>");
            }
        }
        catch { }
           
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("cauhinh.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("baocao.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("alarm.aspx");
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Calendar1.Visible = true;
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Calendar2.Visible = true;
    }


    protected void Button5_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
  
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Session["Khoa"] = "Khoa";
        Response.Redirect("taikhoan.aspx");
    }
    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        TextBox2.Text = Calendar2.SelectedDate.ToString("yyyy-MM-dd 23:59:59");
        Calendar2.Visible = false;
    }
   
    protected void Calendar1_SelectionChanged1(object sender, EventArgs e)
    {
        TextBox1.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss");
        Calendar1.Visible = false;
    }
}
