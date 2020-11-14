var userJs = function () {

    var forgotPasswordSave = function (event) {

        var email = $('#email').val();
        if (email === undefined && email === "" && email === null) {
            Util.showAlertModal('Email is required!!!!');
            return;
        }

        Util.request('/User/ForgotPassword', 'POST', { email: email }, 'json', false, function (data) {

            if (data.statusCode === 204) {

                Util.showSuccessModal('Email enviado com sucesso!');

                setTimeout(function () {
                    location.href = "/";
                }, 3000);

                return;
            } else {
                Util.showAlertModal('Aconteceu algum erro, por favor, tente novamente!');
            }

        });
    };


    var loading = function () {

        $('#deleteButton').click(function () {

            Util.request('/User/Delete', 'Delete', { "id": $(this).val() }, 'json', false, function (data) {

                Util.closeDeleteModal();

                if (data === 409) {

                    Util.showAlertModal('Aconteceu algum erro, por favor, tente novamente!');

                    return;
                } else {
                    Util.closeSuccessModal('Sucesso', 'Usuário deletado');
                }

            }, function (request, status, error) {

            });
        });
    };

    var openPopUpDeleteUser = function (id) {

        Util.showDeleteModal();

        $('#deleteButton').val(id);
    };

    loading();

    return { openPopUpDeleteUser, forgotPasswordSave };

}();