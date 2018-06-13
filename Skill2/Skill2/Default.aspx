<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Skill2.WebForm1" MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Main.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="div-full">
            <div class="div-left">
                <asp:Panel ID="panel1" runat="server">
                </asp:Panel>
            </div>
            <div class="div-right">
                <div style="position:fixed; top: 0px; text-align: center; align-self: center;">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
