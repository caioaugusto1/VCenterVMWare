var DataStorejs = function () {

    function loadingPage() {

        $('#rangeDateTime').daterangepicker({
            timePicker: false,
            timePicker24Hour: false,
            minDate: new Date(Util.getCurrentDate().getFullYear(), Util.getCurrentDate().getMonth() - 1, Util.getCurrentDate().getDate() - 10),
            maxDate: new Date(Util.getCurrentDate().getFullYear(), Util.getCurrentDate().getMonth() - 1, Util.getCurrentDate().getDate()),
            locale: {
                format: 'DD/MM/YYYY'
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

                var capacityTotal = 0;
                if (value.capacity.length === 13) {
                    capacityTotal = value.capacity.charAt(0) + "." + value.capacity.charAt(1) + value.capacity.charAt(2) + "TB"
                } else if (value.capacity.length === 12) {
                    capacityTotal = value.capacity.charAt(0) + value.capacity.charAt(1) + value.capacity.charAt(2) + "GB"
                } else if (value.capacity.length === 9 || value.capacity.length === 10 || value.capacity.length === 11) {
                    capacityTotal = value.capacity.charAt(0) + value.capacity.charAt(1) + "GB"
                }

                $('#tbody-datatable').append(`<tr><td>${value.name}</td><td>${value.type}</td><td>${capacityTotal}</td><td>${result.toFixed(1)} % </td>`);

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