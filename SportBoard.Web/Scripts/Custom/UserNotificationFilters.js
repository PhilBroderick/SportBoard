$(document).ready(function () {
    $('#filterOptions li').on("click", function () {
        var onlyUnread = isOnlyUnread();
        removeActive();

        $(this).toggleClass('active');
        var selection = $(this).text();
        var linkUrl = '/UserNotification/FilterNotifications?filterOption=' + selection + "&onlyUnread=" + onlyUnread;

        $.ajax({
            url: linkUrl,
            type: "GET",
            dataType: "html",
            success: function (data) {
                $('.notificationView').html(data);
            }
        });
    });

    $('#viewRead').change(function () {
        var onlyUnread = isOnlyUnread();
        var selection = activeFilterOption();
        var linkUrl = '/UserNotification/FilterNotifications?filterOption=' + selection + "&onlyUnread=" + onlyUnread;

        $.ajax({
            url: linkUrl,
            type: "GET",
            dataType: "html",
            success: function (data) {
                $('.notificationView').html(data);
            }
        });
    });

    function removeActive() {
        var kids = $('#filterOptions').find('li');
        kids.removeClass('active');
    }

    var isOnlyUnread = function () {
        var onlyUnread = true;
        if ($('#viewRead').prop("checked")) {
            onlyUnread = false;
        }
        return onlyUnread;
    }

    var activeFilterOption = function () {
        return $('.active').text();
    }
});