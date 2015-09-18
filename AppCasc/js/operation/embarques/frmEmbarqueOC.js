var MngEmbarqueOC = function () {
    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {
       
    }

    function clearControls() {
        $('#ul_result_oc').html('');
        $('#tbody_remisiones').html('');
    }

    function fillResultSearchOC(data) {
        var ul_result_oc = $('#ul_result_oc');
        $.each(data, function (i, o) {
            var li = '<li>';
            li += '<a href="#" id="ocKey_' + o.Id + '" class="liOc">' + o.Folio_orden_carga + '</a>';
            li += '</li>';
            ul_result_oc.append(li);
        });

        $('.liOc').each(function () {
            $(this).unbind('click').click(function () {
                var idOC = $(this).attr('id').split('_')[1];
                //alert(idOC);
                loadOCById(idOC);
                return false;
            });
        });
    }

    function loadOC(folio) {
        $.ajax({
            type: 'GET',
            url: "/handlers/Operation.ashx?op=ordenCarga&opt=getByFolio",
            //dataType: "jsonp",
            data: {
                folio: folio
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {
                clearControls();
                fillResultSearchOC(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var oErrorMessage = new ErrorMessage();
                oErrorMessage.SetError(jqXHR.responseText);
                oErrorMessage.Init();
            }
        });
    }

    function fillRemisiones(data) {

        var tbody_remisiones = $('#tbody_remisiones');
        var tr;
        var td;
        $.each(data.LstRem, function (i, o) {
            tr = '<tr>';

            td = '<td align="center">';
            td += o.PSalRem.Referencia;
            td += '</td>';
            tr += td;

            td = '<td>';
            td += '<div><label>Tipo de documento:</label><select></select></div>';
            td += '<div><label>Referencia del documento:</label><input type="text" /></div>';
            td += '<div><input type="button" value="Agregar"/></div>';
            td += '<div>';
            td += '<ul id="ulDocCap_' + o.PSalRem.Id + '">Documentos Capturados';
            td += '<li id="liDoc_' + o.PSalRem.Id + '_3">Remisión->' + o.PSalRem.Folio_remision + '</li>';
            td += '</ul></div>';
            td += '</td>';
            tr += td;

            tr += '</tr>';
            tbody_remisiones.append(tr);
        });
    }

    function loadOCById(id_orden_carga) {
        $.ajax({
            type: 'GET',
            url: "/handlers/Operation.ashx?op=ordenCarga&opt=getById",
            //dataType: "jsonp",
            data: {
                id_orden_carga: id_orden_carga
            },
            complete: function () {
                //$('#up_cantidades').removeClass('ajaxLoading');
            },
            success: function (data) {
                clearControls();
                fillRemisiones(data);
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
var pag = new MngEmbarqueOC();
master.Init(pag);