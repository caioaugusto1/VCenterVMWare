var hostJS = function () {

    function loadingPage() {

        $('#btn-search-by-ip').click(function () {

            const ip = $('#dropDownServers').val();

            Util.request('/Host/GetAllByAPI', 'GET', { "apiId": ip }, 'html', false, function (data) {

                $('#dataTable').remove();

                if (data == 409) {
                    alert('Please, let me know what IP server would you like to get VMs');
                    return;
                }

                $('#div-table').append(data);

                $('#dataTable').DataTable();

            }, function (request, status, error) {

            });
        });
    };

    loadingPage();

    return {};

}();