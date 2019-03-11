var unreadNotificationCount = 0;

function getUnreadNotifications() {
    $.ajax({
        type: "GET",
        url: "/api/notifications?userId=" + '@User.Identity.GetUserId()',
        contentType: false,
        processData: false,
        success: function (response) {
            unreadNotificationCount = response.notificationCount;
        },
        error: function () {
            alert("error");
        }
    })
}