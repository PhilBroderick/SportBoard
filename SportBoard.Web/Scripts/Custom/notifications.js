$('.notificationContainer').on("click", function () {
    var notificationId = $(this).find('#notification_NotificationId').val();
    var formData = new FormData();
    formData.append("notificationId", notificationId);

    $.ajax({
        type: "POST",
        url: '/UserNotification/ReadNotifications',
        data: formData,
        contentType: false,
        processData: false,
        success: function (response) {
            window.location.href = response.Url;
        },
        error: function () {
            alert("An unexpected error has occured.");
        }
    });
});