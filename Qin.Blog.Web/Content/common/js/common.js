var dataTypeId;
$(function () {
    $(".navbar-toggle").on("click", function () {
        $(this).toggleClass("collapsed");
    });

    //Nav Click
    //$("ul.nav li").on("click", function () {
    //    $(this).parent().children(".navactive").removeClass("navactive");
    //    $(this).addClass("navactive");
    //    var id = $(this).children("a").attr("data-typeid");
    //    if (id != "4" && id != "0") {
    //        $(".leavemsgWrap").css("display", "none");
    //        $.ajax({
    //            url: '/Article/CategoryPage',
    //            dataType: 'json',
    //            data: { index: id },
    //            success: function (data) {
    //                if (data.status) {
    //                    //if (id != 4) {
    //                    $(".article-page").siblings(".article").remove();
    //                    $(".total").text(data.total);
    //                    $(".article-page .loaded").text(--data.pageindex * data.pagesize + data.curpagecount);
    //                    var articlelist = $(".articlelist");
    //                    $.each(data.data, function (index, element) {
    //                        articlelist.append(TrimPath.processDOMTemplate("dataTmpl", element));
    //                    });
    //                    if ((data.pageindex * data.pagesize + data.curpagecount) >= data.total) {
    //                        $(".more").addClass('none');
    //                        $(".more").text("没有更多了").attr("disabled", "disabled");
    //                    }
    //                    //} else {//return leavemessage

    //                    //}
    //                } else {//return false
    //                    $(".total").text(0);
    //                    $(".article-page .loaded").text(0);
    //                    $(".article-page").siblings(".article").remove();
    //                }
    //            },
    //            error: function (data) {
    //            }
    //        });
    //    } else {//get leavemsg
    //        //$(".leavemsgWrap").css("display", "block");
    //        //$(".article-page").siblings(".article").remove();
    //        //$.ajax({
    //        //    url: '/LeaveMessage/Pages',
    //        //    dataType: 'json',
    //        //    data: {},
    //        //    success: function (data) {
    //        //        if (data.status) {
    //        //            $(".total").parent().html('共：<span class="total">' + data.total + '</span> 条');
    //        //            $(".article-page .loaded").parent().html('已加载：<span class="loaded">' + (--data.pageindex * data.pagesize + data.curpagecount) + '</span> 条');
    //        //            var leavemsglist = $(".leavemsg-page");
    //        //            $.each(data.data, function (index, element) {
    //        //                leavemsglist.append(TrimPath.processDOMTemplate("leaveMsgTmpl", element));
    //        //            });
    //        //            if ((data.pageindex * data.pagesize + data.curpagecount) >= data.total) {//前面已经减过一次了
    //        //                $(".more").addClass('none');
    //        //                $(".more").text("没有更多了").attr("disabled", "disabled");
    //        //            }
    //        //        } else {
    //        //            $(".total").parent().html('共：<span class="total">' + 0 + '</span> 条');
    //        //            $(".article-page .loaded").parent().html('已加载：<span class="loaded">' + 0 + '</span> 条');
    //        //            $(".article-page").siblings(".article").remove();
    //        //        }
    //        //    }
    //        //});
    //    }
    //});

    //Tag Click

})