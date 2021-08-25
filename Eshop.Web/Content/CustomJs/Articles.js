function FillPageId(id) {
    $("#pageId").val(id);
    $("#filter-search").submit();
}

function FillCommentParentId(mainId) {
    $("#reply-comment-id").val(mainId);
    $("#reply-comment-id").val = mainId;
    $("#reply-comment-text").val("");
}


function GetMoreComments() {

    var articleId = $("#ArticleID").val();

    var pageId = Number($("#pageId").val());

    $("#pagingLayer").fadeOut();
    $("#waiting").fadeIn(100);

    $("#comment-list-" + pageId).load("/Blog/GetMoreComments?articleId=" + articleId + "&pageId=" + pageId,
        function (res) {
            if (res !== null) {
                var newPageId = pageId + 1;
                $("#pageId").val(newPageId);
                $("#waiting").fadeOut(100);
                $("#pagingLayer").fadeIn(100);
            }
        });
    $("#waiting").fadeOut(100);
    $("#pagingLayer").fadeIn(100);
}