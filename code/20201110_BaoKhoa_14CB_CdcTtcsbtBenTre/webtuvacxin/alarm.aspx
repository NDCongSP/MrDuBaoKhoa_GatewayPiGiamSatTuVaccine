<%@ Page Language="C#" AutoEventWireup="true" CodeFile="alarm.aspx.cs" Inherits="alarm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ALARM</title>
    <meta charset="utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table style="width:100%;height:100% "> 
    <tr>
        
         <td  >

             <asp:Image ID="Image2" runat="server" ImageUrl="~/BOYTE1.jpg"  />

         </td>
         <td style="width:60%">
            <table style="width:100%;height:100% ">
                <tr>
                    <td style="text-align:center">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Cambria" Font-Size="XX-Large" ForeColor="#006600" Text="TRUNG TÂM Y TẾ DỰ PHÒNG QUẬN THỦ ĐỨC"></asp:Label>
&nbsp;</td>
                </tr>
                <tr style="text-align:center">
                    <td style="color: #FF0000; font-size: x-large">
                        PHẦN MỀM GIÁM SÁT CẢNH BÁO NHIỆT ĐỘ KHO VÀ CÁC TỦ VACCINE</td>
                </tr>
            </table>
            

         </td> 
         <td>

             <asp:Image ID="Image1" runat="server" Style="height: 50%; width: 50%" ImageUrl="~/logoCty.jpg" />

         </td>
    </tr>
    <tr >
         <td colspan="3" >
              <table style="width:100%;height:100%">
                  <tr>
                      <td style="width:25%">


                                     &nbsp;</td>
                      
                      <td style="width:15%">

                          <asp:Button ID="Button5" runat="server" Height="100%"  Text="HIỂN THỊ" Width="100%" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" style="color: #FF0000" OnClick="Button5_Click" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button4" runat="server" Height="100%" Text="CẤU HÌNH" Width="100%" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" style="color: #FF0000" OnClick="Button4_Click" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button3" runat="server" Height="100%"  Text="BÁO CÁO" Width="100%" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" style="color: #FF0000" OnClick="Button3_Click" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button2" runat="server" Height="100%"  Text="ALARM" Width="100%" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" style="color: #FF0000" OnClick="Button2_Click" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button1" runat="server" Height="100%"  Text="THOÁT" Width="100%" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" style="color: #FF0000" OnClick="Button1_Click1" />

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
                                        <table style="width:100%;border: 2px solid Navy" >
                                            <tr>
                                                <td style="text-align:center; width:30%; font-size: x-large; font-weight: 700;border: 2px solid Navy">

                                                    KHOẢNG THỜI GIAN TỪ:</td>
                                                <td style="text-align:center; width:25%; font-size: x-small;border: 2px solid Navy">

                                                    <asp:TextBox ID="TextBox1" runat="server" Height="80%" Width="90%" Font-Names="Times New Roman" Font-Size="18pt"></asp:TextBox>

                                                </td>
                                                <td style="text-align:center; width:5%; font-size: x-small;border: 2px solid Navy">

                                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="35px" ImageUrl="~/calender.jpg" OnClick="ImageButton1_Click" Width="42px" />

                                                </td>
                                                <td style="text-align:center; font-size: x-large; font-weight: 700;border: 2px solid Navy" >

                                                    ĐẾN:&nbsp;</td>
                                                <td style="text-align:center; width:25%; font-size: x-small;border: 2px solid Navy">

                                                    <asp:TextBox ID="TextBox2" runat="server" Height="80%" Width="90%" Font-Names="Times New Roman" Font-Size="18pt"></asp:TextBox>

                                                </td>
                                                <td style="text-align:center; width:5%; font-size: x-small;border: 2px solid Navy">

                                                    <asp:ImageButton ID="ImageButton2" runat="server" Height="35px" ImageUrl="~/calender.jpg" OnClick="ImageButton2_Click" Width="42px" />

                                                </td>
                                            </tr>
                                            <tr>
                                                
                                                <td style="text-align:center; width:30%; ">

                                                    <asp:Button ID="Button6" runat="server" BorderWidth="2pt" Font-Names="Times New Roman" Font-Size="12pt" style="text-align: center; font-weight: 700" Text="XUẤT ALARM" Width="60%" BackColor="#FF80FF" OnClick="Button6_Click"  />
                                                    <br />
                                                    <br />

                                                    <asp:Button ID="Button7" runat="server" BorderWidth="2pt" Font-Names="Times New Roman" Font-Size="12pt" style="text-align: center; font-weight: 700" Text="XUẤT EXCEL" Width="60%" BackColor="#FF80FF" OnClick="Button7_Click"  />
                                                </td>
                                                <td style="text-align:center; width:25%; font-size: x-small;">

                                                    <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="221px"  ShowGridLines="True" Width="100%" OnSelectionChanged="Calendar1_SelectionChanged1" >
                                                        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                        <OtherMonthDayStyle ForeColor="#CC9966" />
                                                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                        <SelectorStyle BackColor="#FFCC66" />
                                                        <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                                    </asp:Calendar>

                                                </td>
                                                <td style="text-align:center; width:5%; font-size: x-small">

                                                    &nbsp;</td>
                                                <td style="text-align:center; " >

                                                    &nbsp;</td>
                                                <td style="text-align:center; width:25%; font-size: x-small">

                                                    <asp:Calendar ID="Calendar2" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="221px"  ShowGridLines="True" Width="100%" OnSelectionChanged="Calendar2_SelectionChanged">
                                                        <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                                                        <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                        <OtherMonthDayStyle ForeColor="#CC9966" />
                                                        <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                        <SelectorStyle BackColor="#FFCC66" />
                                                        <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                                                        <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
                                                    </asp:Calendar>
                                                </td>
                                                <td style="text-align:center; width:5%; font-size: x-small">

                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:center; font-size: x-large;border: 2px solid Navy" colspan="6">

                                                    <asp:GridView ID="GridView1" runat="server" Width="100%" >
                                                    </asp:GridView>
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
