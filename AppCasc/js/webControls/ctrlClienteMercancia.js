﻿var beanCliente_mercancia = function (id_cliente_grupo, codigo, nombre, clase, negocio, unidad) {

    this.Id = 0;
    this.Id_cliente_grupo = id_cliente_grupo;
    this.Codigo = codigo;
    this.Nombre = nombre;
    this.Clase = clase;
    this.Negocio = negocio;
    this.Valor_unitario = 0;
    this.Unidad = unidad;
    this.Presentacion_x_bulto = 0;
    this.Bultos_x_tarima = 0;
    this.IsActive = true;
}

var ctrlClienteMercancia = function () {
    this.Init = init;
    this.OpenFrm = openFrm;

    function init() {
        initControls();
    }

    function initControls() {
        dialog = $("#ctrlClienteMercancia").dialog({
            autoOpen: false,
            //height: 300,
            width: 480,
            modal: true,
            resizable: false,
            buttons: {
                "Guardar Mercancía": addMercancia,
                "Cancelar": function () {
                    $(this).dialog('close');
                }
            },
            close: function () {
                // form[0].reset();
                // allFields.removeClass("ui-state-error");
            }
        });

        $('#txt_clase').combobox({ maxLength: 3 });
        $('#txt_negocio').combobox({ maxLength: 3 });
        $('#txt_unidad').combobox({ maxLength: 2 });
    }

    function openFrm(cod) {
        $('#txt_codigo').val(cod);
        $("#ctrlClienteMercancia").dialog('open');
    }

    function addMercancia() {

        var oCM = new beanCliente_mercancia(1, $('#txt_codigo').val(), $('#txt_nombre').val(), $('#txt_clase').next().children('input').val(), $('#txt_negocio').next().children('input').val(), $('#txt_unidad').next().children('input').val());

        $.ajax({
            type: "POST",
            url: '/handlers/Catalog.ashx?catalogo=cliente_mercanciaAdd',
            data: JSON.stringify(oCM),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            complete: function () {

            },
            success: function (data) {
                alert(data);
                $("#ctrlClienteMercancia").dialog('close');
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