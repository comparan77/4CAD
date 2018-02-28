var Common = function () {

    this.GetOnlyDecimal = getOnlyDecimal;
    this.GetCurrencyFormat = getCurrencyFormat;
    this.GetSeparatorComasNumber = getSeparatorComasNumber;
    this.SetCaretAtEnd = setCaretAtEnd;

    function getOnlyDecimal(dato) {
        var isNegative;
        isNegative = dato.indexOf('-');
        if (isNegative >= 0)
            isNegative = -1;
        else
            isNegative = 1;
        return dato.replace(/[^0-9.]/g, '') * isNegative;
    }

    function getCurrencyFormat(dato, decimals) {
        var num_decimal = typeof decimals !== 'undefined' ? decimals : 2;
        var isNegative;
        if (dato < 0)
            isNegative = '-$';
        else
            isNegative = '$';
        dato = Math.abs(dato);
        return isNegative + dato.toFixed(num_decimal).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,');
    }

    function getSeparatorComasNumber(dato) {
        dato = dato * 1;
        return dato.toFixed(0).replace(/(\d)(?=(\d\d\d)+(?!\d))/g, '$1,');
    }

    function setCaretAtEnd(elem) {
        var elemLen = elem.val().length;
        // For IE Only
        if (document.selection) {
            // Set focus
            elem.focus();
            // Use IE Ranges
            var oSel = document.selection.createRange();
            // Reset position to 0 & then set at end
            oSel.moveStart('character', -elemLen);
            oSel.moveStart('character', elemLen);
            oSel.moveEnd('character', 0);
            oSel.select();
        }
        else if (elem.selectionStart || elem.selectionStart == '0') {
            // Firefox/Chrome
            elem.selectionStart = elemLen;
            elem.selectionEnd = elemLen;
            elem.focus();
        } // if
    } // SetCaretAtEnd()
}


Common.fillSelect = function (id_control, array, first_element) {
    var ddl = document.getElementById(id_control);
    for (var i = 0; i < array.length; i++) {
        opt = document.createElement('option');
        opt.innerHTML = array[i].datatext;
        opt.value = array[i].datavalue;
        ddl.appendChild(opt);
    }
}