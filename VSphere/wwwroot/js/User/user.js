var userJs = function () {

    var loading = function () {

        $('#deleteButton').click(function () {

            Util.request('/User/Delete', 'Delete', { "id": $(this).val() }, 'json', false, function (data) {

                Util.closeDeleteModal();

                if (data == 409) {

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

    return { openPopUpDeleteUser };

}();