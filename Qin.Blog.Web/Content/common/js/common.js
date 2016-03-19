var dataTypeId;
$(function () {
    $(".navbar-toggle").on("click", function () {
        $(this).toggleClass("collapsed");
    });

    $(window).scroll(function () {
        var top = $(window).scrollTop();
        if (top >= 800) {
            $(".floatBox").css({ "opacity": 1 });
        } else {
            $(".floatBox").css({ "opacity": 0 });
        }
    });

    $(".floatBox .top").on("click", function () {
        $("body").animate({ "scrollTop": [0, "swing"] }, 500);
    });

})