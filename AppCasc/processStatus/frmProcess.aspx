<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmProcess.aspx.cs" Inherits="AppCasc.processStatus.frmProcess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<div>
    <ul>
        <li>
            <div title="Inventario">
                <ul>
                    <li><label>Por Autorizar:</label><span id="invXAut">0</span></li>
                    <li><label>Autorizarados:</label><span id="invAut">0</span></li>
                </ul>
            </div>
        </li>
        <li>
            <div title="Maquila">
                <ul>
                    <li><label>Por Autorizar:</label><span id="maqXAut">0</span></li>
                    <li><label>Por Autorizar:</label><span id="maqAut">0</span></li>
                </ul>
            </div>
        </li>
        <li>
            <div title="Remisión">
                <ul>
                    <li><label>Por Autorizar:</label><span id="remXAut">0</span></li>
                    <li><label>Por Autorizar:</label><span id="remAut">0</span></li>
                </ul>
            </div>
        </li>
    </ul>
</div>

</asp:Content>
