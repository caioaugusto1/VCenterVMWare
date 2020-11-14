var Util = function () {

    $('#document').ready(function () {

        loadingPage();
    });

    var loadingPage = function () {

    };

    function request(endpoint, type, param, dataType, async, callbackSuccess, callbackError) {

        $.ajax({
            url: endpoint,
            type: type,
            data: param,
            dataType: dataType,
            async: async,
            //cache: false,
            success: function (data) {
                callbackSuccess(data);
            }, error: function (request, status, error) {
                callbackError(request, status, error);
            }
        });
    };

    function setDateRange() {
      
    }

    function getCurrentYear() {
        return getToday().getFullYear();
    }

    function getCurrentMonth() {
        return getToday().getMonth() + 1;
    }

    function getCurrentDay() {
        return getToday().getDate();
    }

    function getToday() {
        return new Date();
    }

    function getCurrentDate() {
        return new Date(getCurrentYear(), getCurrentMonth(), getCurrentDay());
    }

    function showSuccessModal(description, subDescription) {
        
        //if (!description) {
        //    $('#description').text('Successo');
        //} else {
        //    $('#description').text(description);
        //}

        //if (!subDescription) {
        //    $('#sub-description').text('Requesição concluída');
        //} else {
        //    $('#sub-description').text(subDescription);
        //}

        $('#modalSuccess').modal('show');
    }

    function closeSuccessModal() {
        $('#modalSuccess').modal('hide');
    }

    function showAlertModal(description, subDescription) {

        if (!description) {
            $('#description').text('Alerta!');
        } else {
            $('#description').text(description);
        }

        if (!subDescription) {
            $('#sub-description').text('Há algo de errado!');
        } else {
            $('#sub-description').text(subDescription);
        }

        $('#modalAlert').modal('show');
    }

    function closeAlertModal() {
        $('#modalAlert').modal('hide');
    }

    function showDeleteModal(description, subDescription) {

        if (!description) {
            $('#description').text('Alerta!');
        } else {
            $('#description').text(description);
        }

        if (!subDescription) {
            $('#sub-description').text('Há algo de errado!');
        } else {
            $('#sub-description').text(subDescription);
        }

        $('#modalDelete').modal('show');
    }

    function closeDeleteModal() {
        $('#modalDelete').modal('hide');
    }

    async function openLoadingModal() {
        $('#loading').modal('show');
    }

    async function closeLoadingModal() {
        $('#loading').modal('hide');
    }


    return { request, getCurrentDate, showSuccessModal, closeSuccessModal, showAlertModal, closeAlertModal, showDeleteModal, closeDeleteModal, setDateRange, openLoadingModal, closeLoadingModal };

}();