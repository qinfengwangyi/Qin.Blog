var dataTypeId;
$(function () {
    $(".navbar-toggle").on("click", function () {
        $(this).toggleClass("collapsed");
    });

    $(window).scroll(function () {
        var top = $(window).scrollTop();
        if (top >= 800) {
            $(".floatBox").css({ "display": "block" });
        } else {
            $(".floatBox").css({ "display": "none" });
        }
    });

    $(".floatBox .top").on("click", function () {
        $("body").animate({ "scrollTop": [0, "swing"] }, 500);
    });
    $(".floatBox .bottom").hover(function () {
        $(this).find(".qrcode").animate({ "display": "block" }, 500);
    });

})