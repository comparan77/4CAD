var beanCliente_vendor = function (id_fiscal, id_cliente_grupo, codigo, nombre, direccion) {

    this.Id_fiscal = id_fiscal;
    this.Id_cliente_grupo = id_cliente_grupo;
    this.Codigo = codigo;
    this.Nombre = nombre;
    this.Direccion = direccion;
    this.IsActive = true;
}

var ctrlClienteVendor = function () {
    this.Init = init;
    this.OpenFrm = openFrm;

    function init() {
    }

    function initControls(page) {
        dialog = $("#ctrlClienteVendor").dialog({
            autoOpen: false,
            //height: 300,
            width: 480,
            modal: true,
            resizable: false,
            buttons: {
                "Guardar Vendor": function () {
                    if (validaRequeridos())
                        addVendor(page);
                },
                "Cancelar": function () {
                    $(this).dialog('close');
                }
            },
            close: function () {
                $(this).dialog("destroy");
            }
        });

    }

    function openFrm(cod, page) {
        initControls(page);
        $('#txt_vendor').val(cod);
        $("#ctrlClienteVendor").dialog('open');
    }

    function validaRequeridos() {
        var valid = true;
        $('.requeridoCV').each(function () {
            if ($(this).val() == '' || $(this).val() == null) {
                alert('Es necesario capturar: ' + $(this).prev().html());
                $(this).focus();
                valid = false;
                return false;
            }

        });
        return valid;
    }

    function addVendor(page) {

        var oCV = new beanCliente_vendor($('#txt_id_fiscal').val(), 1, $('#txt_vendor').val(), $('#txt_proveedor').val(), $('#txt_direccion').val());

        $.ajax({
            type: "POST",
            url: '/handlers/Catalog.ashx?catalogo=cliente_vendorAdd',
            data: JSON.stringify(oCV),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {
                alert(data);
                $("#ctrlClienteVendor").dialog('close');
                if (page != null) {
                    page.Recall(oCV, page.CtrlCM);
                }
                // window.location.href = 'frmMaquila.aspx?_fk=' + id_entrada + "&_pk=" + id_entrada_inventario;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });

    }
}