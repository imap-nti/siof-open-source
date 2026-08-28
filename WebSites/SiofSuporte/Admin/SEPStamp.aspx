<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SEPStamp.aspx.cs" Inherits="Admin_SEPStamp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" action="https://www.comprova.com/servlet/timestamper"
    enctype="multipart/form-data" method="post">
    <input name="cmd" id="cmd" type="hidden" value="doLWDirectTimeStamping" />
    <input name="user_id" type="hidden" value="45" />
    <input name="user_key" type="hidden" value="Imap_ashjdasgfajsfas" />
    <input name="transaction_id" type="hidden" value="1" />
    <input name="return_email" type="hidden" value="id4ni10@gmail.com" />
    <input name="return_errors" type="hidden" value="id4ni10@gmail.com" />
    <input id="hash_code" name="hash_code" type="hidden" value="" runat="server" />
    <input name="algoritm" type="hidden" value="SHA1" />
    </form>
</body>
</html>
