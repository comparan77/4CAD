<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppCasc._Default" MasterPageFile="~/MstCasc.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="js/jquery.updatepanel.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#ctl00_body_up_Calendar').panelReady(function () {
                $('.arrowSelWeek').each(function () {
                    $(this).children('a').addClass('ui-icon ui-icon-arrowthick-1-e');
                });
            });

            $('#ctl00_body_up_OrdTrabajo').panelReady(function () {
                $("#div_ordTrab").accordion({
                    collapsible: true,
                    heightStyle: 'content',
                    activate: function (event, ui) {
                        $("html, body").animate({ scrollTop: $(document).height() }, "slow");
                    }
                });
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <asp:UpdatePanel runat="server" ID="up_Calendar" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="calOrdTrabajo" EventName="SelectionChanged" />
    </Triggers>
    <ContentTemplate>
        <asp:Calendar runat="server" ID="calOrdTrabajo" 
        OnSelectionChanged="calOrdTrabSelChanged"
        BackColor="White" 
        BorderColor="White" 
        BorderWidth="1px" 
        FirstDayOfWeek="Monday" 
        Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" 
        NextPrevFormat="FullMonth" SelectionMode="DayWeek" Width="100%">
        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" 
            VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <SelectorStyle CssClass="arrowSelWeek" />
        <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" 
            Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
        <TodayDayStyle BackColor="#CCCCCC" />
    </asp:Calendar>    
    </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="up_OrdTrabajo" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="calOrdTrabajo" EventName="SelectionChanged" />
        </Triggers>
    <ContentTemplate>
    <div id="div_ordTrab">
        <asp:Repeater runat="server" ID="rep_day" OnItemDataBound="rep_dayDataBound">
        <ItemTemplate>
                <h6><%# Eval("strDialSel")%></h6>
                <div>
                <asp:HiddenField runat="server" ID="hf_DaySel" Value='<%# Eval("daySel") %>' />
                <ul>
                    <asp:Repeater runat="server" ID="rep_OrdTrabajo">
                    <ItemTemplate>
                        <li style="float: left; display: block; padding: 3px 5px; margin-right:15px; list-style-type: none; ">
                            <table border="1" cellspacing="0" cellpadding="3" width="200">
                                <thead>
                                    <tr>
                                        <th colspan="2"><%# Eval("referencia") %></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td><%# Eval("orden_compra") %></td>
                                        <td><%# Eval("codigo") %></td>
                                    </tr>
                                </tbody>
                            </table>
                        </li>
                    </ItemTemplate>
                    </asp:Repeater>
                </ul>
                </div>
        </ItemTemplate>
        </asp:Repeater>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>

    <div id="importarFactruracion">
    <asp:HiddenField runat="server" ID="hf_path" />
    <asp:FileUpload runat="server" ID="fileup_facturacion" />
    <asp:Button runat="server" Text="Importar Archivo" ID="btn_importar" OnClick="click_btn_importar" />
    <asp:Button runat="server" ID="btn_process" Text="Procesar Archivo" OnClick="click_btn_processFile" />
    <asp:UpdatePanel runat="server" ID="up_procesa" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_process" EventName="click" />
    </Triggers>
    <ContentTemplate>
        <asp:HyperLink runat="server" ID="lnkFile"></asp:HyperLink>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="up_procesa_prgss">
    <ProgressTemplate>
        
        Generando el archivo...<span class="ui-icon ui-icon-clock" ></span>
        
    </ProgressTemplate>
    </asp:UpdateProgress>
    </div>

</asp:Content>