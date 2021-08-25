function DeletearticleCategory(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.blockUI({ message: null });
        $.get("/Admin/ManageArticles/DeleteCategory/" + id,
            function (xhr, statusText) {
                if (statusText === "success") {
                    setTimeout($.unblockUI, 5);
                    $("#CategoryBox_" + id).fadeOut(500);
                } else {
                    alert("یافت نشد");
                    setTimeout($.unblockUI, 5);
                }
            });
        setTimeout($.unblockUI, 5);
    });
}

var isSelected = false;

function LoadSubCategory(mainId) {
    var permission = $("#" + mainId);
    var x = isSelected;
    if (permission.is(':checked')) {
        $.get("/Admin/Managearticles/GetArticleSubCategory?mainId=" + mainId,
            function (res) {
                if (res !== null) {
                    $.each(res, function (i, val) {
                        $("#C_" + mainId).append('<ul class="UlList"><li><div class="checkbox"><label><input type="checkbox" class="colored-success" value="' + val.Value + '" name="SelectedCategories"><span class="text">' + val.Text + '</span></label></div ></li></ul>');
                    });
                }
            });
    } else {

        $("#C_" + mainId + " ul").empty();
    }

}

$('#Tags').tagsinput({
    tagClass: 'big'
});
$('#Tags').tagsinput({
    onTagExists: function (item, $tag) {
        $tag.hide.fadeIn();
    }
});

function DeleteArticle(id) {

    $(document).on('confirmation', '.remodal', function () {
        $.blockUI({ message: null });
        $.get("/Admin/ManageArticles/DeleteArticle/" + id, function (res) {
            $("#ArticleBox_" + id).fadeOut(300);
            setTimeout($.unblockUI, 1);
        });
    });
}


function ReturndArticle(id) {

    $(document).on('confirmation', '.remodal', function () {
        $.blockUI({ message: null });
        $.get("/Admin/ManageArticles/ReturnArticle/" + id, function (res) {
            $("#ArticleBox_" + id).fadeOut(300);
            setTimeout($.unblockUI, 1);
        });
    });
}

function GetMoreComments() {
    var articleId = $("#ArticleID").val();

    var pageId = Number($("#pageId").val());

    $("#pagingLayer").fadeOut();
    $("#waiting").fadeIn(100);

    $("#comment-list-" + pageId).load("/Admin/ManageArticles/GetMoreComments?articleId=" + articleId + "&pageId=" + pageId,
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