webApp = {
    Master: function () {

        this.Init = init;

        function init(obj) {
            $(document).ready(function () {
                $('#mainHeader').corner('3px');
                $("input").attr("autocomplete", "off");
                $('#ctl00_up_wucErrMsg').panelReady(function () {
                    var oErrorMessage = new ErrorMessage();
                    oErrorMessage.Init();
                });

                obj.Init();

                //setHeartbeat();
            });
        } // Init <<fin>>
    }
}

var ErrorMessage = function () {

    this.Init = init;
    this.SetError = setError;
    this.ShowError = showError;

    function init() {

        var divError = $('.divError');

        divError.dialog({
            //autoOpen: false,
            height: 190,
            width: 420,
            modal: true,
            resizable: false,
            beforeClose: function (event, ui) {
                divError.children('span').html('');
            },
            close: function (event, ui) {
                $(this).dialog('destroy');
            }
        });

        if (divError.children('span').html() == '') {
            //divError.hide();
            divError.dialog('destroy');
        }

        //        divError.click(function () {
        //            $(this).hide();
        //            divError.children('span').html('');
        //        });
    }

    function showError() {
        if ($('.divError').children('span').html() == '') {
            $('.divError').hide();
        }

        $('.divError').click(function () {
            $(this).hide();
        });
    }

    function setError(error) {
        $('.divError').children('span').html(error);
    }
}

function setHeartbeat() {
    setTimeout("heartbeat()", 300000); // every 5 min
} // setHeartbeat <<fin>>

function heartbeat() {
    //$('#loginContent').hide();
    $.ajax({
        type: 'GET',
        //cache: false,
        url: '/SessionHeartbeatHttpHandler.ashx',
        success: function (data) {
            //alert('vivo');
            setHeartbeat();
            //$('#loginContent').slideDown('slow');
            //var userName = $('#ctl00_lbl_UserName').html();
            //$('#lblTimeInPage').html(data);
        },
        error: function (event, request, settings) {
            alert(settings);
        }
    });

} // heartbeat <<fin>>