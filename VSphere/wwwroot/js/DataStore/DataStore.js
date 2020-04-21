var DataStorejs = function () {

    function loadingPage() {
        
        var data = [
            {
                domain: { x: [0, 1], y: [0, 1] },
                value: 270,
                title: { text: "Speed" },
                type: "indicator",
                mode: "gauge+number"
            }
        ];

        var layout = { width: 300, height: 200, margin: { t: 0, b: 0 } };
        Plotly.newPlot('myDiv', data, layout);


        //$('#btn-search-by-ip').click(function () {

        //    const ip = $('#dropDownServers').val();

        //    var api = "";

        //    if ($('#pageValueIdentity').val() === "GetByAPI") {
        //        api = '/VM/GetAllByAPI';
        //    } else {
        //        api = "/VM/GetAllByFilterHistory";
        //    }

        //    Util.request(api, 'GET', { "apiId": ip }, 'html', false, function (data) {

        //        $('#dataTable').remove();

        //        if (data == 409) {
        //            alert('Please, let me know what IP server would you like to get VMs');
        //            return;
        //        }

        //        $('#div-table').append(data);

        //        $('#dataTable').DataTable();

        //    }, function (request, status, error) {

        //    });
        //});
    };

    loadingPage();

    return {};

}();