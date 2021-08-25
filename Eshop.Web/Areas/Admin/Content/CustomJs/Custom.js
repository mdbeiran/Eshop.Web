$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});
function FillPageId(id) {
    $("#pageId").val(id);
    $("#filter-search").submit();
}

$(function () {
    $('input[type=radio]').change(function () {
        $("#filter-search").submit();
    });
});


function DeletePage(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManagePages/DeletePage?pageId=" + id, function (res) {
            if (res === "True") {
                location.reload();
            }
        });
    });
}

function SendOrderToUser(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/SetSendForOrder?orderId=" + id, function (res) {
            if (res.status === "Done") {
                location.reload();
            }
        });
    });
}

function DeleteSlider(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageSlider/DeleteSlider/" + id, function (res) {
            location.reload();
        });
    });
}


function DeleteProductGroup(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/DeleteGroup/" + id, function (res) {
            if (statusText === "success") {
                location.reload();
            }
            
        });
    });
}
function ReturnProductGroup(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/ReturnGroup/" + id, function (res) {
            if (statusText === "success") {
                location.reload();
            }
        });
    });
}


function DeleteProductFeature(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/DeleteFeature/" + id, function (res) {
            location.reload();
        });
    });
}
function returnProductFeature(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/ReturnFeature/" + id, function (res) {
            location.reload();
        });
    });
}



function DeleteProjectStatus(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProjects/DeleteProjectStatus/" + id, function (res) {
            if (res === "True") {
                $("#Status_" + id).fadeOut(400);
            }
        });
    });
}

function DeleteEmailSetting(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/Setting/DeleteEmailSetting/" + id, function (res) {
            if (res === 'True') {
                location.reload();
            }
            if (res === 'False') {
                alert("ایمیل پیش فرض را نمیتوان حذف کرد");
            }
        });
    });
}


function DeleteCountry(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteCountry?countryId=" + id, function (res) {
            $("#Country_" + id).fadeOut(300);
        });
    });
}

function DeleteState(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteState?StateId=" + id, function (res) {
            $("#State_" + id).fadeOut(300);
        });
    });
}

function DeleteCity(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteCity/" + id, function (res) {
            $("#City_" + id).fadeOut(300);
        });
    });
}

function DeleteUserEducationalDegree(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteUserEducation/" + id, function (res) {
            $("#UserEducationDegree_" + id).remove();
        });
    });
}

function DeleteUserInterest(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteUserInterest/" + id, function (res) {
            $("#UserEducationDegree_" + id).remove();
        });
    });
}

function DeleteUserPortfolio(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteUserPortfolio/" + id, function (res) {
            $("#UserPortfolio_" + id).remove();
        });
    });
}


function DeleteUserGuide(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteUserGuide?guideId=" + id, function (res) {
            location.reload();
        });
    });
}

function DeleteNotification(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteNotification/" + id, function (res) {
            $("#Notification_" + id).remove();
        });
    });
}

function DeleteUserSetting(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteUserSetting/" + id, function (res) {
            location.reload();
        });
    });
}

function DeleteSettingCategory(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteUserSettingCategory/" + id, function (res) {
            location.reload();
        });
    });
}

function DeletePortfolioGallery(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteGallery/" + id, function (res) {
            $("#gallery_" + id).remove();
        });
    });
}

function DeleteContactAdminCategory(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteContactAdminCategory/" + id, function (res) {
            location.reload();
        });
    });
}


function DeleteUser(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteUser/" + id, function (res) {
            location.reload();
        });
    });
}

function ReturnUser(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/ReturnUser/" + id, function (res) {
            location.reload();
        });
    });
}


function DeleteRole(id) {

    $(document).on('confirmation',
        '.remodal',
        function () {
            $.ajax({
                url: "/Admin/ManageUsers/DeleteRole",
                type: "post",
                data: { id },
                success: function (response) {
                    location.reload();
                }
            });
        });
}



$("#ArticleImageInput").change(function () {
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#ArticleImage').attr('src', e.target.result);
        };
        reader.readAsDataURL(this.files[0]);
    }
});


$("#sliderImageInput").change(function () {
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#SliderImage').attr('src', e.target.result);
        };
        reader.readAsDataURL(this.files[0]);
    }
});

$("#SingleProductImageInput").change(function () {
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#SingleProductImage').attr('src', e.target.result);
        };
        reader.readAsDataURL(this.files[0]);
    }
});


$("#PublicProductImageInput").change(function () {
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#PublicProductImage').attr('src', e.target.result);
        };
        reader.readAsDataURL(this.files[0]);
    }
});


$(function () {
    var selectedCategory = $("#AdminContactCategory :selected").val();
    $("#AdminContactCategory").empty();
    $("#AdminContactCategory").append('<option value="0">بر اساس گروه</option>');

    $.get("/Admin/ManageUsers/GetAdminContactCategory/" + selectedCategory, function (res) {
        $.each(res, function (i, val) {
            if (selectedCategory !== val.Value) {
                $("#AdminContactCategory").append('<option value="' + val.Value + '">' + val.Text + '</option>');
            } else {
                $("#AdminContactCategory").append('<option value="' + val.Value + '" selected>' + val.Text + '</option>');
            }
        });
    });

});


$(function () {
    $('#AdminContactCategory').change(function () {
        $("#search-contact").submit();
    });
});




function OpenArticleComment(id) {
    $("#Comment_Text_" + id).fadeIn(300);
}

function DeleteArticleComment(id) {
    $.get("/Admin/Home/DeleteArticleComment?commentId=" + id, function (res) {
        $("#Comment_Box_" + id).fadeOut(300, function () {
            $.ajax({
                url: '/Admin/Home/GetSingleArticleComment',
                cache: false,
                success: function (html) { $("#Commnets_List_Self").append(html); }
            });
        });

    });
}

function ReadArticleComment(id) {
    $.get("/Admin/Home/ReadArticleComment?commentId=" + id, function (res) {
        $("#Comment_Box_" + id).fadeOut(300, function () {
            $.ajax({
                url: '/Admin/Home/GetSingleArticleComment',
                cache: false,
                success: function (html) { $("#Commnets_List_Self").append(html); }
            });
        });
    });
}


