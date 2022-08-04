<%@ Page Language="C#" AutoEventWireup="true" CodeFile="taikhoan.aspx.cs" Inherits="taikhoan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LOGIN</title>
    <meta charset="utf-8" />
    <style type="text/css">
        .auto-style1 {
            font-family: ti;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <table style="width:100%;height:100% "> 
    <tr>
        
         <td style="width:17%;text-align:center" >

             <asp:Image ID="Image2" runat="server" ImageUrl="~/BOYTE1.jpg"  />

         </td>
         <td style="width:60%">
            <table style="width:100%">
                <tr>
                    <td style="text-align:center">
                            &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Cambria" Font-Size="XX-Large" ForeColor="#006600" Text="TRUNG TÂM Y TẾ DỰ PHÒNG QUẬN THỦ ĐỨC"></asp:Label>

                    </td>
                </tr>
                <tr style="text-align:center">
                    <td style="color: #FF0000; font-size: x-large;">
                        PHẦN MỀM GIÁM SÁT CẢNH BÁO NHIỆT ĐỘ KHO VÀ CÁC TỦ VACCINE</td>
                </tr>
            </table>
            

         </td> 
         <td style="width:23%;text-align:center">

             <asp:Image ID="Image1" runat="server" ImageUrl="~/THO.png" />

         </td>
    </tr>
   
    <tr >
         <td colspan="3" style="align-items:center"  >
             <table style="width:100%">
                 <tr>
                     <td style="text-align:center;width:100%"  >
                            <table style="text-align:center;width:100%">
                                <tr>
                                    <td style="text-align:center;width:25%">

                                    </td>
                                    <td style="text-align:center;width:50%">
                                        <table style="text-align:center;width:100%;border: 5px solid Navy;background-color:LightCyan">
                                            <tr>
                                                <td style="text-align:center;border: 2px solid Navy; font-size: 50pt; text-transform: uppercase; color: #FF0000; font-weight: 700;" colspan="2" class="auto-style1">

                                                    ĐĂNG NHẬP</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center;border: 2px solid Navy;width:50%; font-size: xx-large; font-weight: 700; color: #000000;">

                                                    TÀI KHOẢN<br />
                                                    ĐĂNG NHẬP</td>
                                                <td style="text-align:center;border: 2px solid Navy;width:50%">

                                                    <asp:TextBox ID="TextBox1" runat="server" Font-Names="Times New Roman" Font-Size="20pt" Width="90%"></asp:TextBox>

                                                </td>
                                              
                                            </tr>
                                            <tr>
                                                <td style="text-align:center;border: 2px solid Navy;width:50%; font-size: xx-large; font-weight: 700; color: #000000;">

                                                    MẬT<br />
&nbsp;KHẨU</td>
                                                <td style="text-align:center;width:50%;border: 2px solid Navy; font-size: xx-large;">

                                                    <asp:TextBox ID="TextBox2" runat="server" Font-Names="Times New Roman" Font-Size="20pt" Width="90%" TextMode="Password"></asp:TextBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center;border: 2px solid Navy;" colspan="2">

                                                    <asp:Button ID="Button6" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" Text="ĐĂNG NHẬP" Width="50%" style="color: #FF0000" BackColor="#FF99FF" OnClick="Button6_Click" />

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align:center;width:25%">

                                    </td>
                                </tr>
                       
                            </table>
                         &nbsp;</td>
                 </tr>
             </table>
         </td>   
    </tr>
    
    
</table>
    </div>
    </form>
</body>
</html>
