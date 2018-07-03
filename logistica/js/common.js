function Common() {}

Common.ajax = function (url, method, datatype, obj, callback, error) {

    var request = $.ajax({
        url: url,
        data: obj,
        method: method,
        dataType: datatype
    });

    request.done(function (data) {
        if (callback) callback(data);
    });

    request.fail(function (jqXHR, textStatus) {
        if (error) error(jqXHR, textStatus);
    });
}

Common.ajaxAppJson = function (url, method, obj, callback, error) {

    var request = $.ajax({
        url: url,
        data: obj,
        method: method,
        contentType: "application/json; charset=utf-8"
    });

    request.done(function (data) {
        if (callback) callback(data);
    });

    request.fail(function (jqXHR, textStatus) {
        if (error) error(jqXHR, textStatus);
    });
}

Common.ajaxFileUpload = function (url, frmData, callback, error) {

    var request = $.ajax({
        url: url,
        method: "POST",
        data: frmData,
        processData: false,
        contentType: false
    });

    request.done(function (data) {
        if (callback) callback(data);
    });

    request.fail(function (jqXHR, textStatus) {
        if (error) error(jqXHR, textStatus);
    });
}

Common.getCurrencyFormat = function (dato, decimals) {
 
    var num_decimal = typeof decimals !== 'undefined' ? decimals : 2;
    var isNegative;
    if (dato < 0)
        isNegative = '-$';
    else
        isNegative = '$';
    dato = Math.abs(dato);
    return isNegative + dato.toFixed(num_decimal).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,');
}