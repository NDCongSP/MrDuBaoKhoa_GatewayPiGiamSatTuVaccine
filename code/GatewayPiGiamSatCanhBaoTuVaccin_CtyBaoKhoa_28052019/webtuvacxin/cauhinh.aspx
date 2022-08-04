<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cauhinh.aspx.cs" Inherits="cauhinh" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CAU HINH</title>
    <meta charset="utf-8" />
    <style type="text/css">
      
        
       
      
        
        .auto-style1 {
            font-size: xx-small;
            height: 36px;
        }
        .auto-style2 {
            font-size: small;
            color: #FF0000;
        }
      
        
       
      
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width:100% "> 
    <tr>
        
         <td style="width:17%;text-align:center" >

             <asp:Image ID="Image2" runat="server" ImageUrl="~/BOYTE1.jpg"  />

         </td>
         <td style="width:60%">
            <table style="width:100% ">
                <tr>
                    <td style="text-align:center;width:100%">
                            <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Cambria" Font-Size="XX-Large" ForeColor="#006600" Text="TRUNG TÂM Y TẾ DỰ PHÒNG QUẬN THỦ ĐỨC"></asp:Label>
&nbsp;</td>
                </tr>
                <tr style="text-align:center;width:100%">
                    <td style="color: #FF0000; font-size: x-large">
                        PHẦN MỀM GIÁM SÁT CẢNH BÁO NHIỆT ĐỘ KHO VÀ CÁC TỦ VACCINE</td>
                </tr>
            </table>
            

         </td> 
         <td style="width:23%;text-align:center">

             <asp:Image ID="Image1" runat="server" ImageUrl="~/THO.png" />

         </td>
    </tr>
    <tr >
         <td colspan="3" >
              <table style="width:100%;height:100%">
                  <tr>
                      <td style="width:25%">


                                     &nbsp;</td>
                      
                      <td style="width:15%">

                          <asp:Button ID="Button5" runat="server" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" Height="100%" OnClick="Button5_Click" style="color: #FF0000" Text="HIỂN THỊ" Width="100%" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button4" runat="server" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" Height="100%" OnClick="Button4_Click" style="color: #FF0000" Text="CẤU HÌNH" Width="100%" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button3" runat="server" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" Height="100%" OnClick="Button3_Click" style="color: #FF0000; font-weight: 700" Text="BÁO CÁO" Width="100%" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button2" runat="server" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" Height="100%" OnClick="Button2_Click" style="color: #FF0000" Text="ALARM" Width="100%" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button1" runat="server" Height="100%" OnClick="Button1_Click" Text="THOÁT" Width="100%" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" style="color: #FF0000" />

                      </td>
                  </tr>
              </table>

         </td>   
    </tr>
    <tr >
         <td colspan="3" style="align-items:center"  >
             <table style="width:100%;height:100%; border: 10px solid Navy">
                 <tr>
                     <td  >
                         <table style="width:100%;height:100%;background-color:LightCyan">
                             <tr>
                                 <td style="width:100%;height:100%">
                                     <table style="width:100%;height:100% " >
                                         <tr>
                                             <td style ="width:100%;height:100%">
                                                 <table style="width:100%;border: 5px solid Navy">
                                                     <tr>
                                                         <td  style="text-align:center; width:25%;height:100%; font-size: x-large;border: 2px solid Navy">

                                                             IDCB</td>
                                                         <td  style="text-align:center; font-size: x-large;width:25%;height:100%;border: 2px solid Navy ">

                                                             ĐCID DS18B20</td>
                                                         <td  style="text-align:center;width:25%;height:100%; font-size: x-large;border: 2px solid Navy">

                                                             TÊN</td>
                                                         <td  style="text-align:center;width:25%;height:100%; font-size: x-large;border: 2px solid Navy">

                                                             THAO TÁC</td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Label ID="Label1" runat="server" style="font-size: large; font-weight: 700"></asp:Label>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox1" runat="server" Height="90%" Width="90%" ></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox10" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Button ID="Button6" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button6_Click" />

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Label ID="Label2" runat="server" style="font-size: large; font-weight: 700"></asp:Label>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox2" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox11" runat="server" Height="90%" Width="90%" ></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Button ID="Button7" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button7_Click" />

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;border: 2px solid Navy" class="auto-style1">

                                                             <asp:Label ID="Label3" runat="server" style="font-size: large; font-weight: 700"></asp:Label>

                                                         </td>
                                                         <td  style="text-align:center;border: 2px solid Navy" class="auto-style1">

                                                             <asp:TextBox ID="TextBox3" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;border: 2px solid Navy" class="auto-style1">

                                                             <asp:TextBox ID="TextBox12" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;border: 2px solid Navy" class="auto-style1">

                                                             <asp:Button ID="Button8" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button8_Click" />

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Label ID="Label4" runat="server" style="font-size: large; font-weight: 700"></asp:Label>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox4" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox13" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Button ID="Button9" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button9_Click" />

                                                         </td>
                                                     </tr>
                                                      <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Label ID="Label5" runat="server" style="font-size: large; font-weight: 700"></asp:Label>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox5" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox14" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Button ID="Button10" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button10_Click" />

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Label ID="Label6" runat="server" style="font-size: large; font-weight: 700"></asp:Label>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox6" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox15" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Button ID="Button11" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button11_Click" />

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Label ID="Label7" runat="server" style="font-size: large; font-weight: 700"></asp:Label>

                                                         </td>
                                                         <td style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox7" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox16" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Button ID="Button12" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button12_Click" />

                                                         </td>
                                                     </tr>
                                                      <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Label ID="Label8" runat="server" style="font-size: large; font-weight: 700"></asp:Label>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy" >

                                                             <asp:TextBox ID="TextBox8" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox17" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:Button ID="Button13" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button13_Click" />

                                                         </td>
                                                     </tr>
                                                 </table>
                                                 &nbsp;</td>

                                         </tr>
                                         <tr>
                                             <td>
                                                 <table style="width:100%;height:100%;border: 5px solid Navy">
                                                     <tr>
                                                         <td  style="text-align:center; width:25%;font-size: x-large; font-weight: 700;border: 2px solid Navy" rowspan="3">

                                                             TÀI KHOẢN NGƯỜI<br />
                                                             DÙNG</td>
                                                         <td  style="text-align:center; font-size: medium;border: 2px solid Navy">

                                                             NHẬP MẬT KHẨU CŨ</td>
                                                         <td  style="text-align:center;width:25%;height:100%; font-size: x-large;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox19" runat="server" Height="90%" Width="90%" TextMode="Password"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;font-size: x-large;border: 2px solid Navy" rowspan="3">

                                                             <asp:Button ID="Button15" runat="server" style="font-size: medium; height: 28px;" Text="THỰC HIỆN" Width="80%" OnClick="Button15_Click" />

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy" >

                                                             NHẬP MẬT KHẨU MỚI</td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox20" runat="server" Height="90%" Width="90%" TextMode="Password"></asp:TextBox>

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy" >

                                                             NHẬP LẠI MẬT KHẨU MỚI</td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox21" runat="server" Height="90%" Width="90%" TextMode="Password"></asp:TextBox>

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center; font-weight: 700; font-size: x-large;border: 2px solid Navy" rowspan="2">

                                                             LƯU DỮ LIỆU</td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             THỜI GIAN LƯU DATA(Phút)</td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox22" runat="server" Height="90%" Width="90%" ></asp:TextBox>

                                                             </td>
                                                         <td  style="text-align:center;width:25%;border: 2px solid Navy" rowspan="2" >

                                                             <asp:Button ID="Button16" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button16_Click" />

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             THỜI GIAN TRỄ CẢNH BÁO(Giây)</td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox30" runat="server" Height="90%" Width="90%" ></asp:TextBox>

                                                             </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;width:25%; font-weight: 700; font-size: x-large;border: 2px solid Navy" rowspan="2">

                                                             CÀI ĐẶT ALARM<br />
                                                             <span class="auto-style2">(lưu ý các số đt,email cách nhau&nbsp; dấu &quot;,&quot;)</span></td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy"  >

                                                             CÁC SỐ ĐT NHẬN SMS</td>
                                                         <td style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox23" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy" rowspan="2">

                                                             <asp:Button ID="Button17" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button17_Click" />

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             CÁC EMAIL NHẬN ALARM</td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy" >

                                                             <asp:TextBox ID="TextBox25" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                     </tr>
                                                     <tr>
                                                         <td  style="text-align:center;width:25%; font-size: x-large; font-weight: 700;border: 2px solid Navy" rowspan="2">

                                                             ĐẶT MỨC BÁO ĐỘNG</td>
                                                         <td style="text-align:center;width:25%;height:100%;border: 2px solid Navy" >

                                                             NGƯỠNG CAO&nbsp;TỦ </td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox26" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                         </td>
                                                         <td  style="text-align:center;width:25%;border: 2px solid Navy" rowspan="2">

                                                             <asp:Button ID="Button18" runat="server" style="font-size: medium" Text="THỰC HIỆN" Width="80%" OnClick="Button18_Click" />

                                                         </td>
                                                     </tr>
                                                      <tr>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             NGƯỠNG THẤP TỦ</td>
                                                         <td  style="text-align:center;width:25%;height:100%;border: 2px solid Navy">

                                                             <asp:TextBox ID="TextBox27" runat="server" Height="90%" Width="90%"></asp:TextBox>

                                                          </td>
                                                     </tr>                                                
                                                 </table>
                                              </td>
                                         </tr>
                                         </table>
                                 </td>
                             </tr>
                         </table>
                     </td>
                 </tr>
             </table>
         </td>   
    </tr>
    
    
</table>
    </div>
    </form>
</body>
</html>
