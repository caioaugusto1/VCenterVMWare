var Util = function () {

    $('#document').ready(function () {

        loadingPage();
    });

    var loadingPage = function () {

        $('#reservation').daterangepicker({
            dateFormat: 'dd-mm-yy'
        });
    };

    function request(endpoint, type, param, dataType, async, callbackSuccess, callbackError) {

        $.ajax({
            url: endpoint,
            type: type,
            data: param,
            dataType: dataType,
            async: async,
            //cache: false,
            success: function (data) {
                callbackSuccess(data);
            }, error: function (request, status, error) {
                callbackError(request, status, error);
            }
        });
    };

    return { request };

}();