<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<script runat="server">

  
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HIEN THI</title>
    <meta charset="utf-8" />
    <style type="text/css">
        
        </style>
    </head>
<body style="height: 109px">
    <form id="form1" runat="server">
    <div>
    

   
    
<table style="width:100%;height:100% "> 
    <tr>
        
         <td style="width:17%;text-align:center" >

             <asp:Image ID="Image2" runat="server" ImageUrl="~/BOYTE1.jpg"  />

         </td>
         <td style="width:60%">
            <table style="width:100%;height:100% ">
                <tr>
                    <td style="text-align:center">
                            <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Names="Cambria" Font-Size="XX-Large" ForeColor="#006600" Text="TRUNG TÂM Y TẾ DỰ PHÒNG QUẬN THỦ ĐỨC"></asp:Label>
&nbsp;</td>
                </tr>
                <tr style="text-align:center">
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


                                     <asp:ScriptManager ID="ScriptManager1" runat="server">
                                     </asp:ScriptManager>


             <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="1000">
             </asp:Timer>
            

                                 </td>
                      
                      <td style="width:15%">

                          <asp:Button ID="Button5" runat="server" Height="100%"  Text="HIỂN THỊ" Width="100%" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" style="color: #FF0000" OnClick="Button5_Click" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button4" runat="server" Height="100%"  Text="CẤU HÌNH" Width="100%" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" style="color: #FF0000" OnClick="Button4_Click" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button3" runat="server" Height="100%"  Text="BÁO CÁO" Width="100%" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" style="color: #FF0000" OnClick="Button3_Click" />

                      </td>
                      <td style="width:15%">

                          <asp:Button ID="Button2" runat="server" Height="100%" Text="ALARM" Width="100%" BackColor="#99CCFF" Font-Bold="True" Font-Names="Times New Roman" Font-Size="20pt" style="color: #FF0000" OnClick="Button2_Click" />

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
                                 <td style="text-align:center; width:30%; font-size: x-small;">

                                             <asp:Label ID="Label11" runat="server" Text="Label"  Font-Bold="True" Font-Names="Times New Roman" style="font-size: x-large"></asp:Label>

                                     </td>
                                 <td style="text-align:center ; width:30%" >

                                             <asp:Label ID="Label12" runat="server" Text="Label"  Font-Bold="True" Font-Names="Times New Roman" style="font-size: x-large"></asp:Label>

                                     </td>
                                 <td style="text-align:center ; width:30%" >

                                             <asp:Label ID="Label13" runat="server" Text="Label"  Font-Bold="True" Font-Names="Times New Roman" style="font-size: x-large"></asp:Label>

                                     </td>

                             </tr>
                              <tr>
                                 <td style="text-align:center ; width:30%" >

                                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                         <ContentTemplate>
                                             <asp:Label ID="Label1" runat="server" Text="Label" Width="80%"  Font-Bold="True" style="font-size: 50pt" Font-Names="Times New Roman"></asp:Label>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                         </Triggers>
                                     </asp:UpdatePanel>

                                 </td>
                                 <td style="text-align:center ; width:30%">

                                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                         <ContentTemplate>
                                             <asp:Label ID="Label2" runat="server" Text="Label" Width="80%" Font-Bold="True" Font-Names="Times New Roman" style="font-size: 50pt"></asp:Label>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                         </Triggers>
                                     </asp:UpdatePanel>

                                 </td>
                                 <td style="text-align:center ; width:30%">

                                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                         <ContentTemplate>
                                             <asp:Label ID="Label3" runat="server" Text="Label" Width="80%" Font-Bold="True" Font-Names="Times New Roman" style="font-size: 50pt"></asp:Label>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                         </Triggers>
                                     </asp:UpdatePanel>

                                 </td>

                             </tr>
                              <tr>
                                 <td style="text-align:center ; width:30%">

                                             <asp:Label ID="Label14" runat="server" Text="Label"  Font-Bold="True" Font-Names="Times New Roman" style="font-size: x-large"></asp:Label>

                                     </td>
                                 <td style="text-align:center ; width:30%">

                                             <asp:Label ID="Label15" runat="server" Text="Label"  Font-Bold="True" Font-Names="Times New Roman" style="font-size: x-large"></asp:Label>

                                     </td>
                                 <td style="text-align:center ; width:30%">

                                             <asp:Label ID="Label16" runat="server" Text="Label"  Font-Bold="True" Font-Names="Times New Roman" style="font-size: x-large"></asp:Label>

                                     </td>

                             </tr>
                              <tr>
                                 <td style="text-align:center ; width:30%">

                                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                         <ContentTemplate>
                                             <asp:Label ID="Label4" runat="server" Text="Label" Width="80%" Font-Bold="True" Font-Names="Times New Roman" style="font-size: 50pt"></asp:Label>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                         </Triggers>
                                     </asp:UpdatePanel>

                                 </td>
                                 <td style="text-align:center ; width:30%">

                                     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                         <ContentTemplate>
                                             <asp:Label ID="Label5" runat="server" Text="Label" Width="80%" Font-Bold="True" Font-Names="Times New Roman" style="font-size: 50pt"></asp:Label>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                         </Triggers>
                                     </asp:UpdatePanel>

                                 </td>
                                 <td style="text-align:center ; width:30%">

                                     <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                         <ContentTemplate>
                                             <asp:Label ID="Label6" runat="server"  Text="Label" Width="80%" style="font-size:50pt" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                         </Triggers>
                                     </asp:UpdatePanel>

                                 </td>

                             </tr>
                              <tr>
                                 <td style="text-align:center ; width:30%">

                                             <asp:Label ID="Label17" runat="server" Text="Label"  Font-Bold="True" Font-Names="Times New Roman" style="font-size: x-large"></asp:Label>

                                     </td>
                                 <td style="text-align:center ; width:30%">

                                             <asp:Label ID="Label18" runat="server" Text="Label"  Font-Bold="True" Font-Names="Times New Roman" style="font-size: x-large"></asp:Label>

                                     </td>
                                 <td style="text-align:center ; width:30%">

                                             <asp:Label ID="Label19" runat="server" Text="Label"  Font-Bold="True" Font-Names="Times New Roman" style="font-size: x-large"></asp:Label>

                                     </td>

                             </tr>
                              <tr>
                                 <td style="text-align:center ; width:30%">

                                     <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                         <ContentTemplate>
                                             <asp:Label ID="Label7" runat="server" Text="Label" Width="80%" Font-Names="Times New Roman" style="font-size: 50pt" Font-Bold="True"></asp:Label>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                         </Triggers>
                                     </asp:UpdatePanel>

                                 </td>
                                 <td style="text-align:center ; width:30%">

                                     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                         <ContentTemplate>
                                             <asp:Label ID="Label8" runat="server" Text="Label" Width="80%" style="font-size:50pt" Font-Bold="True" Font-Names="Times New Roman"></asp:Label>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                         </Triggers>
                                     </asp:UpdatePanel>

                                 </td>
                                 <td style="text-align:center ; width:30%">

                                     <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                         <ContentTemplate>
                                             <asp:Label ID="Label9" runat="server" style="font-size: 50pt" Text="Label" Width="80%" Font-Names="Times New Roman" Font-Bold="True"></asp:Label>
                                         </ContentTemplate>
                                         <Triggers>
                                             <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                         </Triggers>
                                     </asp:UpdatePanel>

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
