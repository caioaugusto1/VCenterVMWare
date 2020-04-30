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

        $('#btn-export').click(function () {
            pdfExport();
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

            $('#div-table').append(data);

        }, function (request, status, error) {
        });
    };

    var getAllByAPI = function (apiId) {

        Util.request('/VM/GetAllByAPI', 'GET', { "apiId": apiId }, 'json', false, function (data) {

            if (data.statusCode == "200") {

                var countTotal = Object.keys(data.data).length;
                var totalOn = 0;

                $.each(data.data, function (index, value) {

                    var power = "OFF";

                    if (value.power == "POWERED_ON") {
                        power = "ON";
                        totalOn++;
                    };

                    var tr = `<tr><td>${value.name}</td><td>${value.memory}</td><td>${value.cpu}</td><td>${power}</td></tr>`;

                    $('#tbody-tableInformation').append(tr);
                });

                $('#totalOn').text(totalOn);
                $('#totalOff').text(countTotal - totalOn);
                $('#totalVMS').text(countTotal);

                $('#dataTable').DataTable();

            } else {
                return;
            }

        }, function (request, status, error) {

        });
    };

    var pdfExport = function () {

        var htmlBody = $('#sectionTable').html();

        Util.request('/VM/PDFGenerator', 'POST', { 'html': htmlBody }, 'html', false, function (data) {

        });
    };

    var buildList = function (data) {


    };

    loadingPage();

    return {};

}();