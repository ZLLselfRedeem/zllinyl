<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeAdvertShow.aspx.cs" Inherits="HomeNew_HomeAdvertShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridViewImage" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ControlStyle BorderStyle="None" />
                    <FooterStyle BorderStyle="None" />
                    <HeaderStyle BorderStyle="None" />
                    <ItemStyle BorderStyle="None" VerticalAlign="Middle" />
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" Width="400px" Height="160px" ImageUrl='<%# Eval("ImageUrl") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
