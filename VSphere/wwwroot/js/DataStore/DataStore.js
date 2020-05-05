var DataStorejs = function () {

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

        $('#btn-clean').click(function () {
            $("#dropDownServers ").val($("#dropDownServers  option:first").val());
        });


        getDivSpeedDataStore();
    };

    function getDivSpeedDataStore() {

        $('#btn-search').click(function () {

            const ip = $('#dropDownServers').val();

            if (!ip) {
                Util.showAlertModal('Por favor!', 'Selecione algum server!');
                return;
            }

            var api = "";

            if ($('#pageValueIdentity').val() === "GetByAPI") {
                getAllByAPI(ip);
            } else {
                getAllByHistory(ip);
            }
        });
    };

    function getAllByAPI(apiId) {

        Util.request('/DataStore/GetAllByAPI', 'GET', { apiId }, 'json', false, function (data) {

            var idCount = 0;
            $.each(data, function (index, value) {

                idCount++;

                $('#main-speed-div').append(`<div id='speedDiv_${idCount}' class='col-md-3'><br/></div>`);

                var used = value.capacity - value.freeSpace;
                var result = (used * 100) / value.capacity;

                buildDivSpeedDataStore(result, idCount, value.name, value.type);
            });

        }, function (request, status, error) {
        });
    };


    function getAllByHistory(apiId) {

        var rangeDate = $('#rangeDateTime').val().split('-');
        var datetimeFrom = rangeDate[0].trim();
        var datetimeTo = rangeDate[1].trim();

        Util.request('/DataStore/GetAllByFilterHistory', 'GET', { apiId, datetimeFrom, datetimeTo }, 'json', false, function (data) {

            $.each(data, function (index, value) {

                $('#main-speed-div').append(`<div id='speedDiv_${value.id}' class='col-md-3'><br/></div>`);

                var used = value.capacity - value.freeSpace;
                var result = (used * 100) / value.total

                buildDivSpeedDataStore(result, value.id, value.name, value.name);
            });

        }, function (request, status, error) {
        });
    };

    function buildDivSpeedDataStore(value, divId, name) {

        var data = [
            {
                domain: { x: [0, 1], y: [0, 1] },
                value: value,
                delta: { reference: 70 },
                title: {
                    text: `${name}<br>`
                },
                showlegend: false,
                gauge: { axis: { visible: true, range: [0, 100] } },
                type: "indicator",
                mode: "gauge+number"
            }
        ];

        var layout = { width: 299, height: 298, margin: { t: 0, b: 0 } };
        Plotly.newPlot('speedDiv_' + divId, data, layout, { modeBarButtonsToRemove: ['toImage', ''] });
    };

    loadingPage();

    return { buildDivSpeedDataStore, getDivSpeedDataStore };

}();