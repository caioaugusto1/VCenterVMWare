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

            $("#dropDownServers").val($("#dropDownServers option:first").val());

            if ($('#pageValueIdentity').val() === "CreateVM") {
                $('#create_partial_view').remove();

                return;
            } else if ($('#pageValueIdentity').val() === "History") {
                $('#list-vms-table_wrapper').remove();
            } else {
                $('#tbody-tableInformation>tr').remove();
            }
        });

        $('#btn-search').click(function () {

            Util.openLoadingModal();

            const ip = $('#dropDownServers').val();

            if (!ip) {

                Util.showAlertModal('Por favor!', 'Selecione algum server!');
                return;
            }

            if ($('#pageValueIdentity').val() === "GetByAPI") {

                $('#tbody-tableInformation>tr').remove()
                $('#btn-export').removeAttr("disabled");

                getAllByAPI(ip);

            } else if ($('#pageValueIdentity').val() === "CreateVM") {
                createVM(ip);

            } else {
                getAllByHistory(ip);
            }
        });
    };

    var getAllByHistory = function (apiId) {

        $('#list-vms-table').remove();

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

                    var tr = "";
                    var power = "OFF";

                    if (value.power == "POWERED_ON") {
                        power = "ON";

                        tr = `<tr><td>${value.name}</td><td>${value.memory}</td><td>${value.cpu}</td><td>${power}</td>
                                    <td><input type="checkbox" checked class="turnOnOrTurnOff" onchange="VMjs.turnOnOrTurnOff('${value.vm}', '${apiId}', true)" /></br><button type="button" class="btn btn-danger" disabled>Apagar</button><td>
                                </tr>`;

                        totalOn++;
                    } else {

                        tr = `<tr><td>${value.name}</td><td>${value.memory}</td><td>${value.cpu}</td><td>${power}</td>
                                    <td><input type="checkbox" class="turnOnOrTurnOff" onchange="VMjs.turnOnOrTurnOff('${value.vm}', '${apiId}', false)" /></br><button type="button" class="btn btn-danger" onclick="VMjs.deleteVM('${value.vm}', '${apiId}')">Apagar</button><td>
                                </tr>`;
                    }

                    //<div class="custom-control custom-switch custom-switch-off-danger custom-switch-on-success">
                    //    <input type="checkbox" class="custom-control-input" id="customSwitch3">
                    //        <label class="custom-control-label" for="customSwitch3">Toggle this custom switch element with custom colors danger/success</label>
                    //</div>

                    $('#tbody-tableInformation').append(tr);
                });

                $('#totalOn').text(totalOn);
                $('#totalOff').text(countTotal - totalOn);
                $('#totalVMS').text(countTotal);

                $('#list-vms-table-allbyapi').DataTable();

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

    var createVM = function (apiId) {

        Util.request('/VM/Create', 'GET', { "apiId": apiId }, 'html', false, function (data) {

            if (data != null) {
                $('#div-crate-body').append(data);
            }

            //Util.closeDeleteModal();

            //if (data === 409) {

            //    Util.showAlertModal('Aconteceu algum erro, por favor, tente novamente!');

            //    return;
            //} else {
            //    Util.closeSuccessModal('Sucesso', 'Usuário deletado');
            //}

        }, function (request, status, error) {

        });
    }

    var createSaveVM = function () {
  
        Util.request('/VM/CreateSave', 'POST', $('#formCreateVM').serialize(), 'json', false, function (data) {

            if (data.statusCode === 200) {
                $('#modalSuccess').modal('show');

                setTimeout(function () {
                    location.href = '/VM/AllByAPI';
                }, 3000);

            } else {
                $('#modalAlert').modal('show');

            }

        }, function (request, status, error) {

        });
    };

    var cancelCreate = function () {

        $('#create_partial_view').remove();
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

                    if (data.statusCode == 200) {
                        Util.showSuccessModal('Requisição feita com sucesso', `A máquina foi deletada com sucesso ${vmName}!`);

                        $("#tbody-tableInformation > tr").remove();
                        $('#btn-search').trigger('click');

                    } else {
                        Util.showAlertModal('Ocorreu um erro ao tentar fazer a requisição', 'Por favor, tente novamente!');
                    }

                }, function (request, status, error) {

                });
            }
        });

    };

    var turnOnOrTurnOff = function (vmName, apiId, beTurnedOn) {

        if (beTurnedOn) {

            Swal.fire({
                title: `Você tem certeza que deseja desligar a máquina ${vmName}?`,
                text: 'Você não poderá reverter essa ação!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim, desligar!'
            }).then((result) => {

                if (result.isConfirmed) {

                    Util.request('/VM/TurnONorTurnOff', 'POST', { 'apiId': apiId, 'name': vmName, 'turnOn': false }, 'json', true, function (data) {

                        if (data.statusCode == 200) {
                            Util.showSuccessModal('Requisição feita com sucesso', `A máquina foi desligada com sucesso ${vmName}!`);

                            $("#tbody-tableInformation > tr").remove();
                            $('#btn-search').trigger('click');
                        } else {
                            Util.showAlertModal('Ocorreu um erro ao tentar fazer a requisição', 'Por favor, tente novamente!');
                        }

                    }, function (request, status, error) {

                    });
                }
            });

        } else {

            Swal.fire({
                title: `Você tem certeza que deseja ligar a máquina ${vmName}?`,
                text: 'Você não poderá reverter essa ação!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sim, ligar!'
            }).then((result) => {

                if (result.isConfirmed) {

                    Util.request('/VM/TurnONorTurnOff', 'POST', { 'apiId': apiId, 'name': vmName, 'turnOn': true }, 'json', true, function (data) {

                        if (data.statusCode == 200) {
                            Util.showSuccessModal('Requisição feita com sucesso', `A máquina foi ligada com sucesso ${vmName}!`);

                            $("#tbody-tableInformation > tr").remove();
                            $('#btn-search').trigger('click');
                        } else {
                            Util.showAlertModal('Ocorreu um erro ao tentar fazer a requisição', 'Por favor, tente novamente!');
                        }

                    }, function (request, status, error) {

                    });
                }
            });

        }
    }

    loadingPage();

    return { deleteVM, turnOnOrTurnOff, cancelCreate, createSaveVM };

}();