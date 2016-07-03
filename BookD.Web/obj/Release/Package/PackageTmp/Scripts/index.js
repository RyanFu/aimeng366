function loadStart(obj,size) {
    var winWidth = $(obj).width();
    var winHeight = $(obj).height();
    if (winWidth==0) {
        return;
    }
    if (winHeight==0) {
        winHeight = $(document).height() / 2;
    }
    if (size==undefined) {
        size = 5;
    }
    var tempSize = parseInt(winHeight / size);
    var tempWidth = parseInt((winWidth - tempSize) / 2);
    var tempHeight = parseInt((winHeight - tempSize) / 2);
    $(obj).append("<div id='loadingForWing'><div style ='width: " + winWidth + "px; height:" + winHeight + "px; background: #f8f8f8; z-index: 10;opacity: 0.5;'><img src='/resource/images/sys/loading.gif' width='" + tempSize + "px' height='" + tempSize + "px' style='display:block; position: relative;left: " + tempWidth + "px;top:" + tempHeight + "px;' /></div></div>")
}
function loadEnd(obj) {
    $(obj).find("#loadingForWing").remove();
}
function ShowMsg(title, msg) {
    $('#msgModal').remove();
    var html = "<div class='modal fade' id='msgModal' tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true'> " +
                "<div class='modal-dialog'> " +
                "<div class='modal-content'> " +
                    "<div class='modal-header'> " +
                    "<button type='button' class='close' data-dismiss='modal' aria-hidden='true'> " +
                    "&times; " +
                    "</button> " +
                    "<h4 class='modal-title' id='myModalLabel'> " + title +
                    "</h4> " +
                    "</div> " +
                    "<div class='modal-body'> " + msg +
                    "</div> " +
                    "<div class='modal-footer'> " +
                    "<button type='button' class='btn btn-default' data-dismiss='modal'>确认 " +
                    "</button> " +
                "</div> " +
                "</div> " +
                "</div> ";
    $("body").append($(html));
    $('#msgModal').modal('show');
}