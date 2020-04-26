var VMjs = function () {

    function loadingPage() {

        $('#rangeDateTime').daterangepicker({
            timePicker: true,
            timePicker24Hour: true,
            minDate: new Date(Util.getCurrentDate().getFullYear(), Util.getCurrentDate().getMonth(), Util.getCurrentDate().getDate() - 10),
            maxDate: new Date(Util.getCurrentDate().getFullYear(), Util.getCurrentDate().getMonth(), Util.getCurrentDate().getDate()),
            locale: {
                format: 'DD/MM/YYYY HH:mm'
            }
        });


        $('#btn-search-by-ip').click(function () {

            const ip = $('#dropDownServers').val();

            if ($('#pageValueIdentity').val() === "GetByAPI") {
                getAllByAPI(ip);
            } else {
                getAllByHistory(ip);
            }
        });
    };

    var getAllByHistory = function (apiId) {

        var rangeDate = $('#rangeDateTime').val().split('-');
        var datetimeFrom = rangeDate[0].trim();
        var datetimeTo = rangeDate[1].trim();

        Util.request('/VM/GetAllByFilterHistory', 'GET', { apiId, datetimeFrom, datetimeTo }, 'html', false, function (data) {

            buildList(data);

        }, function (request, status, error) {
        });
    };

    var getAllByAPI = function (apiId) {

        Util.request('/VM/GetAllByAPI', 'GET', { "apiId": apiId }, 'html', false, function (data) {

            buildList(data);

        }, function (request, status, error) {

        });
    };

    var buildList = function (data) {

        $('#dataTable').remove();

        if (data == 409) {
            alert('Please, let me know what IP server would you like to get VMs');
            return;
        }

        $('#div-table').append(data);

        $('#dataTable').DataTable();

    };

    loadingPage();

    return {};

}();