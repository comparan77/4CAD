<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppCasc._Default" MasterPageFile="~/MstCasc.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="js/jquery.updatepanel.js" type="text/javascript"></script>
    <script type="text/javascript">

//        var urlHandler = 'http://localhost:52289/';
//        var BeanChartJs = function (title, opcion, anio, mes, id_cliente, id_bodega) {
//            this.Title = title;
//            this.Data = null;
//            this.Opcion = opcion;
//            this.Anio = anio;
//            this.Mes = mes;
//            this.Id_cliente = id_cliente;
//            this.Id_bodega = id_bodega;
//        }
//        var BeanEntrada_aud_uni = function (id_entrada_precarga, id_transporte_tipo, referencia, operador, placa, caja, caja1, caja2, sello, sello_roto, acta_informativa, vigilante, lst_files) {
//            this.PUsuario;
//            this.Id = 0;
//            this.Id_entrada_pre_carga = id_entrada_precarga;
//            this.Id_transporte_tipo = id_transporte_tipo;
//            this.Informa;
//            this.Referencia = referencia;
//            this.Operador = operador;
//            this.Placa = placa;
//            this.Caja = caja;
//            this.Caja1 = caja1;
//            this.Caja2 = caja2;
//            this.Sello = sello;
//            this.Sello_roto = sello_roto;
//            this.Acta_informativa = acta_informativa;
//            this.Fecha = '01/01/0001';
//            this.Vigilante = vigilante;
//            this.PLstEntAudUniFiles = lst_files;
//            this.PLstAudImg = lst_files;
//        }

//        function Common() { }

//        Common.fetchJSONFile = function (path, callback, type, jsonData) {
//            var httpRequest = new XMLHttpRequest();
//            httpRequest.open(type, path, true);
//            httpRequest.setRequestHeader("Content-type", "application/json");
//            httpRequest.onreadystatechange = function () {
//                if (httpRequest.readyState === 4) {
//                    if (httpRequest.status === 200) {
//                        var data = JSON.parse(httpRequest.responseText);
//                        if (callback) callback(data);
//                    }
//                }
//            };
//            httpRequest.send(jsonData);
//        } 

//        
//        OperationModel.entradaAudUniAdd = function (objEntAudUni, callback) {
//            var url = urlHandler + 'handlers/CAEApp.ashx?op=entrada&opt=AudUniAdd';
//            try {
//                OperationModel.fetchJSONFile(
//                    url,
//                    function(data) {
//                        callback(data);
//                    },
//                    'POST',
//                    JSON.stringify(objEntAudUni)
//                );
//            } catch (error) {
//                alert('entradaAudUniAdd' + error);
//            }
//        }




        $(document).ready(function () {
//            try {
//                var oChartJs = new BeanChartJs('Unidades', 1, 2016, 0, 0, 0);

//                var url = urlHandler + 'handlers/CAEApp.ashx?op=reporte&opt=' + oChartJs.Title;
//                alert(url);
//                Common.fetchJSONFile(
//                    url,
//                    function (data) {
//                        callback(data);
//                    },
//                    'POST',
//                    JSON.stringify(oChartJs)
//                );
//            } catch (e) {
//                alert(e);
//            }

            //            var oBEAU = new BeanEntrada_aud_uni(
            //                    1,
            //                    1,
            //                    '',
            //                    '',
            //                    '',
            //                    '',
            //                    '',
            //                    '',
            //                    '',
            //                    false,
            //                    '',
            //                    1,
            //                    []
            //                );
            //            OperationModel.entradaAudUniAdd(oBEAU, function (data) {
            //                alert(data);
            //                if (typeof (data) == "object") {
            //                    //                    Common.notificaRegExitoso();
            //                    //                    window.open(urlHandler + 'rpt/entradas_aud/' + oBEAU.Referencia + '/casc028.pdf?' + new Date().getTime(), '_system', 'location=yes');
            //                    //                    clearFormValues();
            //                }
            //                else {
            //                    alert(data);
            //                }
            //                //Common.setEstatusBtn('btn_save', 'Guardar', false);

            //            });

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

</asp:Content>