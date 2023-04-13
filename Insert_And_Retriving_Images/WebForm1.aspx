

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Insert_And_Retriving_Images.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
           
        }
        .auto-style2 {
            height: 41px;
            
        }
        .auto-style3 {
            height: 41px;
            width: 171px;
             border:ridge;
            text-align:center
        }
        .auto-style4 {
            width: 171px;
        }
        .auto-style5 {
            width: 171px;
            height: 49px;
        }
        .auto-style6 {
            height: 49px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table cellpadding="4" cellspacing="4" class="auto-style1">
                <tr>
                    <td class="auto-style3"  >File Upload&nbsp;</td>
                    <td class="auto-style2">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                     
                    </td>
                </tr>
                <tr>
                    <td class="auto-style5"></td>
                    <td class="auto-style6">

                        
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Save" Width="81px" />
                        <Columns>
        
    </Columns>

&nbsp;<asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td>  

                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"  ShowHeader="False">
                            
                            <Columns>
                                <asp:BoundField DataField="Image_Name" HeaderText="Image_Name" />
                                
                                <asp:TemplateField>

                                    
                                    <ItemTemplate>

                                            
                                        <asp:LinkButton ID="LinkButton1" Text='<%# Bind("Image_Name") %>'   runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
                                         
                                              
                                       
                                                    
                                                   
                                                    

                                                
                                               
                                            

                                    </ItemTemplate>



                                </asp:TemplateField>


                            </Columns>
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>


                                        <table>

                                            <tr>
                                               
                                                <td>

                                                      <asp:Label runat="server" Text='<%# Eval("id") %>'></asp:Label>

                                                </td>
                                                <td>

                                                    
                                                    <asp:Image ID="Image1" height="150" Width="150" ImageUrl='<%#GetImageUrl( Eval("Image_Name")) %>' runat="server"></asp:Image>



                                                </td>


                                                

                                                 

                                                



                                            </tr>



                                        </table>




                                    </ItemTemplate>
                                   



                                </asp:TemplateField>


















                            </Columns>

                        
                        
                        
                        
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td>  
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
