using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace nhietdokhovacxin
{
    public class GhiMySQL
    {
        public string ChuoiKetnoiMySQL_local = "Server =45.119.212.41 ; Database = ctybaokhoattytmocaybacbentre ; Port=3306;User ID=customer_ttp ;Password=ThinhTamPhat!@#456&*(;charset=utf8";
        //public string ChuoiKetnoiMySQL_local = "Server =192.168.0.102 ; Database = ctybaokhoattytmocaybacbentre ; Port=3306;User ID=root ;Password=100100;charset=utf8";
        static MySqlConnection connect_local;
        /// <summary>
        /// method ket noi den CSDL NTC Cloud server.
        /// </summary>
        /// <returns>tra ve GOOD hoac BAD.</returns>
        public string Ketnoi_local()
        {
            try
            {
                connect_local = new MySqlConnection(ChuoiKetnoiMySQL_local);
                connect_local.Open();
                return "GOOD";
            }
            catch { return "BAD"; }
        }

        public string Ngatketnoi_local()
        {
            try
            {
                connect_local.Dispose();
                return "GOOD";
            }
            catch { return "BAD"; }
        }

        public string DongBoThoiGian()
        {
            try
            {
                this.Ketnoi_local();
                string TimeNow = "";
                MySqlCommand cmd = new MySqlCommand("SELECT NOW()", connect_local);
                TimeNow = Convert.ToString(cmd.ExecuteScalar());
                //Console.WriteLine("thoi gian mysql " + TimeNow);
                //Console.WriteLine("chuyn thoi gian mysql " + Convert.ToDateTime(TimeNow).ToString("dd-MM-yyyy HH:mm:ss"));
                cmd.Dispose();
                this.Ngatketnoi_local();
                return TimeNow;
            }
            catch { return "Bad"; }
        }
         public DataTable select_limit(string tenbang)
        {
            try
            {
                DataTable TableReturn = new DataTable();
                MySqlDataAdapter ad = new MySqlDataAdapter("SELECT * FROM " + tenbang + " order by thoigianbd desc limit 1", connect_local);
                ad.Fill(TableReturn);

                return TableReturn;
            }
            catch { return null; }
        }
     
      

        /// <summary>
        /// method doc databases tren server.
        /// </summary>
        /// <param name="tenbang">ten bang can doc.</param>
        /// <returns>tra ve datatable, neu = null la loi.</returns>
        public DataTable DocMySQL(string tenbang)
        {
            try
            {
                DataTable TableReturn = new DataTable();
                MySqlDataAdapter ad = new MySqlDataAdapter("SELECT * FROM " + tenbang, connect_local);
                ad.Fill(TableReturn);
                return TableReturn;
            }
            catch { return null; }
        }
        public string insert_cmd(string tenbang, string data)
        {
            try
            {
                string MySqlCmd = "insert into " + tenbang + " values(" + data + ")";
                MySqlCommand cmd = new MySqlCommand(MySqlCmd, connect_local);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return "GOOD";
            }
            catch
            {
                return "BAD";
            }
        }
        public string insert_data(string tenbang, string data)
        {
            try
            {
                string MySqlCmd = "insert into " + tenbang + " values(" + data + ")";
                MySqlCommand cmd = new MySqlCommand(MySqlCmd, connect_local);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return "GOOD";
            }
            catch
            {
                return "BAD";
            }
        }
        public DataTable DocMySQL_where(string tenbang,string dk)
        {
            try
            {
                DataTable TableReturn = new DataTable();
                MySqlDataAdapter ad = new MySqlDataAdapter("SELECT * FROM " + tenbang + " " +dk , connect_local);
                ad.Fill(TableReturn);
                return TableReturn;
            }
            catch { return null; }
        }

        public string capnhatgiatriweb(string cb1, string cb2, string cb3, string cb4)
        {
            try
            {
                string MysqlCmd = "update hienthiweb set cb1 = '" + cb1 + "',cb2 = '" + cb2 + "',cb3 = '" + cb3 + "',cb4 = '" + cb4 + "'";
                MySqlCommand cmd = new MySqlCommand(MysqlCmd, connect_local);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                return "GOOD";
            }
            catch { return "BAD"; }
        }
      
       
    }
}