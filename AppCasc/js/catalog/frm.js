var Form = function () {

    this.Init = init;

    function init() {
        var divActions = $('#divActions');
        divActions.children('input[type="submit"]').each(function () {
            $(this).button();
        });
    }

    //loadError();

}

function loadError() {

    $('#errorMsgs').attr('title', $('#ctl00_body_hfTitleErr').val())

    $('#errorMsgs').dialog({
        autoOpen: false,
        height: 190,
        width: 420,
        modal: true,
        resizable: false
    });

    if ($('#ctl00_body_hfTitleErr').val().length > 0) {
        $('#errorMsg').html($('#ctl00_body_hfDescErr').val());
        $('#errorMsgs').dialog('open');
        $('#ctl00_body_hfTitleErr').val('');
    }
}

//$(document).ready(function () {

//    var oForm = new Form();
//    oForm.Init();

//});

var master = new webApp.Master;
var pag = new Form();
master.Init(pag);