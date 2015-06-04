<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenericErrorPage.aspx.cs" Inherits="AppCasc.errors.GenericErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Error</title>
    <link href="../css/MstCasc.css" rel="stylesheet" type="text/css" />
</head>
<body>

    <div id="mainContent">

    <form id="form1" runat="server">
    <div>
        <h3><%=err %></h3>
    </div>
    </form>

    </div>
</body>
</html>
