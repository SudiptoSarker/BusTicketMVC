$(document).ready(function () {
    $('#login_name i').on('click', function () {
        $('#login_name').css('display', 'none');
        $.ajax({
            type: 'GET',
            url: '/User/RemoveDisplay',
            success: function (data) {
            },
            complete: {
            }
        });
    });
});