var Util = function () {

    $('#document').ready(function () {

        loadingPage();
    });

    var loadingPage = function () {

    };

    function getCurrentYear() {
        return getToday().getFullYear();
    }

    function getCurrentMonth() {
        return getToday().getMonth() + 1;
    }

    function getCurrentDay() {
        return getToday().getDate();
    }

    function getToday() {
        return new Date();
    }

    function getCurrentDate() {
        return new Date(getCurrentYear(), getCurrentMonth(), getCurrentDay());
    }

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

    return { request, getCurrentDate };

}();