var VMjs = function () {

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

        $('#btn-export').click(function () {

            Util.openLoadingModal();

            pdfExport();
        });

        $('#btn-clean').click(function () {
            $("#dropDownServers ").val($("#dropDownServers  option:first").val());
        });

        $('#btn-search').click(function () {

            Util.openLoadingModal();

            const ip = $('#dropDownServers').val();

            if (!ip) {

                Util.showAlertModal('Por favor!', 'Selecione algum server!');
                return;
            }

            if ($('#pageValueIdentity').val() === "GetByAPI") {

                $('#btn-export').removeAttr("disabled");

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

            $('#list-vms-table').DataTable();

            Util.closeLoadingModal();

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
                    debugger;
                    var tr = `<tr><td>${value.name}</td><td>${value.memory}</td><td>${value.cpu}</td><td>${power}</td>
                                    <td><label class="custom-control-label" for="customSwitch3"></label><button type="button" class="btn btn-danger" onclick="VMjs.deleteVM('${value.vm}', '${apiId}')">Apagar</button><td>
                                </tr>`;

                    $('#tbody-tableInformation').append(tr);
                });

                $('#totalOn').text(totalOn);
                $('#totalOff').text(countTotal - totalOn);
                $('#totalVMS').text(countTotal);

                $('#list-vms-table').DataTable();

                Util.closeLoadingModal();

            } else {
                return;
            }

        }, function (request, status, error) {

        });
    };

    var pdfExport = function () {

        var htmlBody = $('#div-table').html();

        Util.request('/VM/PDFGenerator', 'POST', { 'html': htmlBody }, 'json', true, function (data) {

            Util.closeLoadingModal();

            if (data == 409) {

                Util.showAlertModal('Requisição não foi finalizada', 'O PDF não foi enviado, por favor, tente novamente!');
                return;
            } else if (data == 400) {

                Util.showAlertModal('Requisição não foi finalizada', 'O servidor SMTP está fora do AR, por favor, mais tarde!');
                return;
            }

            Util.showSuccessModal('Requisição feita com sucesso', 'O PDF foi enviado para seu e-mail de cadastro!');

        }, function (request, status, error) {
        });
    };

    var deleteVM = function (vmName, apiId) {

        Swal.fire({
            title: `Você tem certeza que deseja apagar a máquina ${vmName}?`,
            text: 'Você não poderá reverter essa ação!',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sim, deletar!'
        }).then((result) => {

            if (result.isConfirmed) {

                Util.request('/VM/Delete', 'DELETE', { 'apiId': apiId, 'name': vmName }, 'json', true, function (data) {
                    debugger;
                    if (data.statusCode == 200) {
                        Util.showSuccessModal('Requisição feita com sucesso', `A máquina foi deletada com sucesso ${vmName}!`);
                    } else {
                        Util.showAlertModal('Ocorreu um erro ao tentar fazer a requisição', 'Por favor, tente novamente!');
                    }

                }, function (request, status, error) {

                });
            }
        });

    };

    loadingPage();

    return { deleteVM };

}();