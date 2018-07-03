webApp = {
    Master: function () {

        this.Init = init;

        function init(obj) {
            $(document).ready(function () {
                $("input").attr("autocomplete", "off");
                
//                $('#ctl00_up_wucErrMsg').panelReady(function () {
//                    var oErrorMessage = new ErrorMessage();
//                    oErrorMessage.Init();
//                });

                obj.Init();

                $('body').keydown(function (e) {
                    if (e.which === 116) {
                        return false;
                    }
                });
            });
        }
    }
}

webApp.Master.loading = function (open) {

    var div_loading = document.getElementById('div_loading');
    if (open)
        div_loading.style.display = 'block';
    else
        div_loading.style.display = 'none';
}