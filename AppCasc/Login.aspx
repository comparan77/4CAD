<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppCasc.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="css/excite-bike/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="css/common.css?v1.1.150611_1234" rel="stylesheet" type="text/css" />
    <link href="css/frmLogin.css?v1.1.150611_1234" rel="stylesheet" type="text/css" />

    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.10.4.custom.min.js" type="text/javascript"></script>
    <script src="js/frmLogin.js?v1.1.150611_1234" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="div_login" class="centerScreen">
    <asp:HiddenField runat="server" ID="hfTitleErr" />
    <asp:HiddenField runat="server" ID="hfDescErr" />

    <asp:Login ID="logCasc" runat="server" CssClass="centerScreen"
    OnLoggedIn="logCasc_LoggedIn"
    DisplayRememberMe="False" 
    FailureText="Nombre de usuario y/o contraseñas no válidas." 
    LoginButtonText="Acceder" PasswordLabelText="Contraseña:" TitleText="Acceso" 
    UserNameLabelText="Nombre de Usuario:">
    </asp:Login>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ValidationGroup="logCasc" />

    </div>
    </form>
</body>
</html>
