$("#srch-term").focus();

$("#srch-button").on("click", function () {
    $("#srch-term").select().focus();
});

$("#srch-term, #startsWith, #contains, #endsWith").on("keypress", function (e) {
    if (e.which == 13) {
        $("#srch-button").click();
    }
});

$("#filterAnchor").on("click", function () {
    var $filters = $("#filters");
    $filters.toggleClass("hidden");

    var $filterAnchor = $("#filterAnchor");

    if ($filters.hasClass("hidden")) {
        $("#srch-term").focus();
        $filterAnchor.removeClass("active");
    } else {
        $("#startsWith").focus();
        $filterAnchor.addClass("active");
    }
});

$("#edit-button").on("click", function () {
    $("button[id^='trk-']").toggleClass("hidden");
});

$("button[id^='trk-']").on("click", function () {
    var $src = $("#srch-term");
    $src.val($src.val() + $(this).html());
});