var snapshotJs = function () {

    $('#btn-search').click(function () {

        Util.openLoadingModal();

        const ip = $('#dropDownServers').val();

        if (!ip) {

            Util.showAlertModal('Por favor!', 'Selecione algum server!');
            return;
        }

        if ($('#pageValueIdentity').val() === "GetByAPI") {

            $('#list-snapsshot-datatable_wrapper').remove();

            getAllByAPI(ip);

        }
    });

    $('#btn-clean').click(function () {

        $("#dropDownServers").val($("#dropDownServers option:first").val());

        if ($('#pageValueIdentity').val() === "GetByAPI") {
            $('#list-snapsshot-datatable_wrapper').remove();
        }
    });



    var getAllByAPI = function (apiId) {

        Util.request('/Snapshot/List', 'GET', { 'apiId': apiId }, 'html', false, function (data) {

            $('#list-snapsshot-datatable>tbody').append(data);

            $('#list-snapsshot-datatable').DataTable();

            Util.closeLoadingModal();

        }, function (request, status, error) {
        });
    };

    return { getAllByAPI }

}();