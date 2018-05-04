var MngRegDatMer = function () {

    this.Init = init;

    function init() {
        initControls();
    }

    function initControls() {
        $('#udt_drive').click(function () {
            $(this).attr('disabled', 'disabled');
            $(this).html('Actualizando base de datos ...');
            update_liverpool_drive();
            return false;
        });
    }

    function update_liverpool_drive() {
        try {
            $.ajax({
                url: "https://4cad.casc.com.mx:8001/liverpool",
                crossDomain: true,
                dataType: 'jsonp',
                complete: function () {
                    $('#udt_drive').removeAttr('disabled');
                    $('#udt_drive').html('Actualizar del drive');
                },
                success: function (data) {
                    alert(data);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    var oErrorMessage = new ErrorMessage();
                    alert('error ' + JSON.stringify(jqXHR));
                    oErrorMessage.SetError(jqXHR.responseText);
                    oErrorMessage.Init();
                }
            });
        } catch (error) {
            alert(error.Message);
        }
    }
}