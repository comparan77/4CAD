var RemDetail = function () {

    this.ShowRemisionDetail = showRemisionDetail;

    function showRemisionDetail(div_tbl_folio_remision, eliminar_remision) {

        $('.lnk-folio-remision').each(function () {
            $(this).click(function () {
                $('#ui-id-3').html($(this).html() + ', Fecha: ' + $(this).attr('title'));
                $(div_tbl_folio_remision).dialog('open');

                var id_salida_remision = $(this).parent().children('input').last().val();
                $('#spn-dlt').html(id_salida_remision);
                for (var iRes = 1; iRes <= 2; iRes++) {
                    $('#spn-bulto-' + iRes).html('');
                    $('#spn-piezaxbulto-' + iRes).html('');
                    $('#spn-pieza-' + iRes).html('');
                }

                $('#spn-piezatotal').html('');

                var arrData = ['dano_especifico', 'etiqueta_rr', 'fecha_recibido', 'folio_cita', 'tieneOrdenCarga'];
                var hf_element = $(this).next();
                for (var data in arrData) {
                    $('#spn-' + arrData[data]).html($(hf_element).val());
                    hf_element = $(hf_element).next();
                }

                $.ajax({
                    type: 'GET',
                    url: "/handlers/Operation.ashx",
                    //dataType: "jsonp",
                    data: {
                        op: 'remDetail',
                        key: id_salida_remision
                    },
                    complete: function () {
                        $('#up_cantidades').removeClass('ajaxLoading');
                    },
                    success: function (data) {

                        var piezaTotal = 0;

                        $(data).each(function (i, obj) {
                            i = i + 1;
                            $('#spn-bulto-' + i).html(obj.Bulto);
                            $('#spn-piezaxbulto-' + i).html(obj.Piezaxbulto);
                            $('#spn-pieza-' + i).html(obj.Piezas);
                            $('#spn-estado-' + i).html(obj.Danado ? 'Dañado' : 'Indemne');
                            piezaTotal += obj.Piezas * 1;
                        });

                        $('#spn-piezatotal').html(piezaTotal);

                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        var oErrorMessage = new ErrorMessage();
                        oErrorMessage.SetError(jqXHR.responseText);
                        oErrorMessage.Init();
                    }
                });

                return false;
            });
        });

    } //fin showRemisionDetail


}

