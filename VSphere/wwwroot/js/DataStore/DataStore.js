var DataStorejs = function () {

    function loadingPage() {

        getDivSpeedDataStore();
    };

    function getDivSpeedDataStore() {

        $('#btn-search-by-ip').click(function () {

            const ip = $('#dropDownServers').val();

            if (ip == "") {
                return;
            }

            var api = "";

            if ($('#pageValueIdentity').val() === "GetByAPI") {
                api = '/DataStore/GetAllByAPI';
            } else {
                //var days = $('#reservation').val().split('-');
                //var from = days[0].trim(); 
                //var to = days[1].trim();

                api = "/DataStore/GetAllByFilterHistory";
            }

            Util.request(api, 'GET', { "apiId": ip, "from": 10 / 10 / 2010, "to": 23 / 10 / 2020 }, 'json', false, function (data) {

                $('#dataTable').remove();

                if (data == 409) {
                    alert('Please, let me know what IP server would you like to get DataStores');
                    return;
                }

                $.each(data, function (index, value) {

                    $('#main-speed-div').append(`<div id='speedDiv_${value.id}' class='col-md-3'><br/></div>`);

                    buildDivSpeedDataStore(12, value.id, value.datastore, value.name, value.type);
                });

            }, function (request, status, error) {

            });
        });
    };

    function buildDivSpeedDataStore(value, divId, dataStore, name, type) {

        var data = [
            {
                domain: { x: [0, 1], y: [0, 1] },
                value: value,
                delta: { reference: 70 },
                title: {
                    text: `${dataStore}<br><span style='font-size:0.8em;color:gray'>${name}</span><br><span style='font-size:0.8em;color:gray'>${type}</span>`
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