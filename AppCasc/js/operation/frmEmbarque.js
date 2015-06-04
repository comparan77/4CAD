var btn_validar_referencia;
var txt_referencia;
var ddl_cliente;

var MngEmbarque = function () {

    this.Init = init;

    function init() {

        InitControls();

    }

    function InitControls() {

        btn_validar_referencia = $('#btn_validar_referencia');
        txt_referencia = $('#ctl00_body_txt_referencia');
        ddl_cliente = $('#ctl00_body_ddlCliente');
        txt_referencia.mask('99-9999-9999999');
        $('#ctl00_body_txt_hora_salida, #ctl00_body_txt_hora_carga').scroller({
            preset: 'time',
            theme: 'default',
            display: 'modal',
            mode: 'clickpick',
            timeFormat: 'HH:ii:ss'
        });
        $(btn_validar_referencia).button().click(function () {
            if (txt_referencia.val().length > 0) {
                if ($(ddl_cliente).val() * 1 == 1 || $(ddl_cliente).val() * 1 == 23)
                    validaReferencia($(txt_referencia).val(), $(ddl_cliente).val());
                else {
                }
            }
            else
                alert('Es necesario proporcionar una referencia');
            return false;
        });
    }

    function validaReferencia(referencia, id_cliente) {

        $.ajax({
            type: 'GET',
            url: "/handlers/Operation.ashx",
            //dataType: "jsonp",
            data: {
                op: 'embarque',
                opt: 'valRef',
                ref: referencia,
                cliente: id_cliente
            },
            success: function (data) {
                if (typeof (data) == 'string')
                    alert(data);
                else {
                    
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }
}


var master = new webApp.Master;
var pag = new MngEmbarque();
master.Init(pag);