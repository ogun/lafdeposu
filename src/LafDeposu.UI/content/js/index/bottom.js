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

    if ($filters.hasClass("hidden")) {
        $("#srch-term").focus();
    } else {
        $("#startsWith").focus();
    }
});