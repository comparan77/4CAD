<%@ Page Title="" Language="C#" MasterPageFile="~/MstCasc.Master" AutoEventWireup="true" CodeBehind="frmOrdCrg.aspx.cs" Inherits="AppCasc.operation.frmOrdCrg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

<asp:UpdatePanel runat="server" ID="up_ot_cerrada" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="grd_ot_cerrada" EventName="PageIndexChanging" />
    <asp:AsyncPostBackTrigger ControlID="grd_ot_cerrada" EventName="RowCommand" />
</Triggers>
<ContentTemplate>
<asp:GridView runat="server" ID="grd_ot_cerrada" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="5"
OnPageIndexChanging="grd_ot_Page_idx_chg" OnRowCommand="grd_ot_RowCommand"
SelectedRowStyle-BackColor="CornflowerBlue" DataKeyNames="Id" CssClass="grdCascSmall"
>
<Columns>
<asp:ButtonField ButtonType="Link" ControlStyle-CssClass="ui-icon ui-icon-triangle-1-e icon-button-action" CommandName="sel_ot" />
<asp:TemplateField SortExpression="Item">
<HeaderTemplate>
    <asp:TextBox runat="server" ID="txtRefEnt" AutoPostBack="true" OnTextChanged="txtRefEnt_textChanged"></asp:TextBox>
    <asp:LinkButton runat="server" ID="lnk_clear" OnClick="lnk_clear_click" CssClass="ui-icon ui-icon-refresh"></asp:LinkButton>
</HeaderTemplate>
<ItemTemplate>
<span><%# Eval("PEnt.Referencia")%></span>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="Referencia" HeaderText="Ref. Cte." />
<asp:BoundField DataField="Folio" HeaderText="Folio" />
<asp:BoundField DataField="Fecha" HeaderText="Fecha O.T." DataFormatString="{0:dd/MM/yyyy}" />
</Columns>
</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel runat="server" ID="up_maquila" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="grd_ot_cerrada" EventName="RowCommand" />
    </Triggers>
    <ContentTemplate>
        <asp:GridView runat="server" ID="grd_maquila" CssClass="grdCascSmall" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Ref2" HeaderText="Referencia" />
            <asp:BoundField DataField="Piezas" HeaderText="Pz(s) Tot" DataFormatString="{0:N0}" />
            <asp:BoundField DataField="PiezasMaq" HeaderText="Pz(s) Maq" DataFormatString="{0:N0}" />
            <asp:BoundField DataField="Faltantes" HeaderText="Pz(s) Faltantes" DataFormatString="{0:N0}" />
            <asp:BoundField DataField="Sobrantes" HeaderText="Pz(s) Sobrantes" DataFormatString="{0:N0}" />
            <asp:BoundField DataField="PasosMaq" HeaderText="Pasos" DataFormatString="{0:N0}" />
        </Columns>
        </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
