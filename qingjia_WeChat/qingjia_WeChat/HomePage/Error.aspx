<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="qingjia_YiBan.HomePage.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        发生了错误！
    </div>
        <br />
        <asp:TextBox ID="ErrorMessage" runat="server" Visible="false"></asp:TextBox>
    </form>
</body>
</html>
