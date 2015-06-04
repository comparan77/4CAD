<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="AppCasc.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title></title>
    <script type="text/javascript" src="http://crypto-js.googlecode.com/svn/tags/3.1.2/build/rollups/aes.js"></script>
<script type="text/javascript">
//    window.onload = function (e) {
        //        var key = CryptoJS.enc.Base64.parse(document.getElementById('key').innerText);
        //        var iv = CryptoJS.enc.Base64.parse(document.getElementById('iv').innerText);
        //        var ciphertext = document.getElementById('ciphertext').innerText;
        //var decrypted = CryptoJS.AES.decrypt(ciphertext, key, { iv: iv });

        //document.getElementById('output').innerText = decrypted.toString(CryptoJS.enc.Utf8);

        //        var key = document.getElementById('key').innerText;
        //        var iv = document.getElementById('iv').innerText;
        //        var decrypted = CryptoJS.AES.encrypt('Message', key, { iv: iv });
        //        document.getElementById('output').innerText = decrypted;

    var rijndaelEncrypt = function () {
        document.getElementById('encrypted').innerText = "";
        var plainText = document.getElementById('plain').value;
        var key = document.getElementById('key').value;
        var encryptedText = encrypt(plainText, key);
        document.getElementById('encrypted').innerText = encryptedText;

        document.getElementById('encNumber').innerHTML = encodeToNumber(encryptedText);
    }
    var rijndaelDecrypt = function () {
        document.getElementById('decrypted').innerText = "";
        var encryptedText = document.getElementById('encrypted').innerHTML;
        var key = document.getElementById('key').value;
        var decryptedText = decrypt(encryptedText, key);
        document.getElementById('decrypted').innerText = decryptedText;
    }
        var encrypt = function (plainText, key) {
            var C = CryptoJS;
            plainText = C.enc.Utf8.parse(plainText);
            key = C.enc.Utf8.parse(key);
            var aes = C.algo.AES.createEncryptor(key, {
                mode: C.mode.CBC,
                padding: C.pad.Pkcs7,
                iv: key
            });
            var encrypted = aes.finalize(plainText);
            return C.enc.Base64.stringify(encrypted);
        }
        var decrypt = function (encryptedText, key) {
            var C = CryptoJS;
            encryptedText = C.enc.Base64.parse(encryptedText);
            key = C.enc.Utf8.parse(key);
            var aes = C.algo.AES.createDecryptor(key, {
                mode: C.mode.CBC,
                padding: C.pad.Pkcs7,
                iv: key
            });
            var decrypted = aes.finalize(encryptedText);
            return C.enc.Utf8.stringify(decrypted);
        }

        function encodeToNumber(string) {
            var number = "";
            var length = string.length;
            for (var i = 0; i < length; i++)
                number += string.charCodeAt(i).toString();
            return number.substr(0,6);
        }
        
//    }
</script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
	<form id="form1" runat="server">
    <input type="text" id="plain" value='<%=Message %>' style="width:100%" />
<p/>
<input type="text" id="key" value="52855327-D1DC-4D" placeholder="52855327-D1DC-4D" style="width:100%" />
<p/>
<button id="encrypt" onclick="rijndaelEncrypt(); return false;">Encrypt</button>
<p/>
<label id="encrypted" style="width:100%"></label>
<p/>
<button id="decrypt" onclick="rijndaelDecrypt(); return false;">Decrypt</button>
<p/>
<label id="decrypted" style="width:100%"></label> 

<hr />
<div id="encNumber"></div>
<label><%=EncryptMsg %></label>

	</form>
    <table class="style1">
    <thead>
        <tr>
            <th rowspan="4">
                <asp:GridView runat="server" ID="grdDetInv" CssClass="grdCasc" AutoGenerateColumns="false">
                    <Columns>
                    <asp:BoundField DataField="bultos" HeaderText="Bultos" />
                    <asp:BoundField DataField="piezasxbulto" HeaderText="Piezas x Bulto" />
                    </Columns>
                    </asp:GridView></th>
            <th>
                &nbsp;</th>
            <th>
                Inventario</th>
            <th>
                Maquilado</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Pallets</td>
            <td id="td_pallet_inventario" align="center"><%= oEI.Pallets.ToString() %></td>
            <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_pallet_maquilado"><%= oEM.Pallet.ToString() %></span></p></td>
        </tr>
        <tr>
            <td>Bultos</td>
            <td id="td_bulto_inventario" align="center"><%= oEI.Bultos.ToString() %></td>
            <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_bulto_maquilado"><%= oEM.Bulto.ToString() %></span></p></td>
        </tr>
        <tr>
            <td>Piezas Totales</td>
            <td id="td_pieza_inventario" align="center"><%= oEI.Piezas.ToString() %></td>
            <td align="center"><p style="margin: 0;"><span style="float: left;" class="ui-icon"></span><span id="td_pieza_maquilado"><%= oEM.Pieza.ToString() %></span></p></td>
        </tr>
    </tbody>
    </table>

    <hr />

    <table cellspacing="0" cellpadding="5" border="1" width="100%">
        <thead>
        <tr>
            <th rowspan="4">
                <div>
		<table cellspacing="0" border="1" style="border-collapse:collapse;" id="ctl00_body_grdDetInv" rules="all" class="grdCasc">
			<tbody><tr>
				<th scope="col">Bultos</th><th scope="col">Piezas x Bulto</th>
			</tr><tr>
				<td>278</td><td>24</td>
			</tr><tr>
				<td>1</td><td>4</td>
			</tr>
		</tbody></table>
	</div></th>
            <th>
                &nbsp;</th>
            <th>
                Inventario</th>
            <th>
                Maquilado</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Pallets</td>
            <td align="center" id="td1">3</td>
            <td align="center"><p style="margin: 0;"><span class="ui-icon ui-icon-alert" style="float: left;"></span><span id="Span1" style="color: rgb(255, 0, 0);">0</span></p></td>
        </tr>
        <tr>
            <td>Bultos</td>
            <td align="center" id="td2">279</td>
            <td align="center"><p style="margin: 0;"><span class="ui-icon ui-icon-alert" style="float: left;"></span><span id="Span2" style="color: rgb(255, 0, 0);">0</span></p></td>
        </tr>
        <tr>
            <td>Piezas Totales</td>
            <td align="center" id="td3">6676</td>
            <td align="center"><p style="margin: 0;"><span class="ui-icon ui-icon-alert" style="float: left;"></span><span id="Span3" style="color: rgb(255, 0, 0);">0</span></p></td>
        </tr>
    </tbody>
</table>

</body>
</html>