// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {
    const toastElList = document.querySelectorAll('.toast')
    const toastList = [...toastElList].map(toastEl => new bootstrap.Toast(toastEl));

    const isLoggedIn = $('#LoggedIn').val();
    if (isLoggedIn) {
        $.get('/Notification/GetNotificationsPartial', function(data) {
            if (data) {
                $('#notifications-container').html(data);
                const notificationCount = +($('#notification-count-server').val() || 0);
                if (!notificationCount) {
                    $('.notification-count').hide();
                } else {
                    $('.notification-count').show();
                    $('.notification-count').text(notificationCount > 10 ? '10+' : notificationCount);
                }

                $('.notification-loader').hide();
                $('.notification-icon').show();

                $(".toast").toast("show");
            }
        });
    }
});