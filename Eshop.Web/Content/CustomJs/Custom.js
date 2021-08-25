$(document).ready(function () {
    $("#main-contact-form").onsubmit(function () {
        BlockUI();
    });
});

function FilterPrice(parameters) {
    var price = $.number($("#FilterPrice").val());
    $("#SinglePrice").val(price);
    $("#filter-search").submit();
}

function FillGroupId(id) {
    $("#groupId").val(id);
    $("#pageId").val(1);
    $("#filter-search").submit();
}


function BlockUI() {
    $.blockUI({ message: null });
}

function ClearInputs() {
    $("#contact_Name").val("");
    $("#contact_Mobile").val("");
    $("#contact_Subject").val("");
    $("#contact_Message").val("");
    $("#contact_Message").html("");
}

function AddSliderVisit(id) {
    $.get("/Home/AddSliderVisit/" + id, function () { });
}

function SuccessContactUsMessage() {
    setTimeout($.unblockUI, 100);
    $("#ErrorContactUsMessage").fadeOut(1);
    $("#SuccessContactUsMessage").fadeIn(400);
    ClearInputs();
}

function ErrorContactUsMessage() {
    setTimeout($.unblockUI, 100);
    $("#SuccessContactUsMessage").fadeOut(1);
    $("#ErrorContactUsMessage").fadeIn(400);
}

function FillPageId(id) {
    $("#pageId").val(id);
    $("#filter-search").submit();
}

function AddProductToPublicWishList(id) {
    $.get("/Home/AddPublicWishList/" + id,
        function (res) {
            swal("", "به لیست علاقه مندی اضافه شد", "success");
        });
}

function AddProductToSingleWishList(id) {
    $.get("/Home/AddSingleWishList/" + id,
        function (res) {
            swal("", "به لیست علاقه مندی اضافه شد", "success");
        });
}

function DeleteSingleWish(id) {
    $.get("/Home/DeleteSingleWish/" + id,
        function (res) {
            swal("", "از لیست علاقه مندی حذف شد", "success");
            $("#SingleWishBox_" + id).fadeOut(300);
        });
}

function DeletePublicWish(id) {
    $.get("/Home/DeletePublicWish/" + id,
        function (res) {
            swal("", "از لیست علاقه مندی حذف شد", "success");
            $("#PublicWishBox_" + id).fadeOut(300);
        });
}

function OrderSuccess(res) {
    if (res.status === "Exist") {
        swal("", "محصول مورد نظر در سبد خرید وجود دارد", "warning");
    } else if (res.status === "Count") {
        swal("", "لطفا تعداد محصصول را به درستی وارد کنید", "warning");
    } else if (res.status === "NotAuthorize") {
        swal("", "برای افزودن به سبد خرید میبایست ابتدا وارد سایت شوید", "warning");
    } else if (res.status === "Done") {
        swal("", "محصول مورد نظر به سبد خرید اضافه شد", "success");
    }
}

function OrderFailur() {
    swal("", "برای درج سفارش باید ابتدا وارد سایت شوید", "warning");
}

function SubmitFrom(id) {
    $("#EditOrderForm_" + id).submit();
}

function EditOrderSuccess() {
    swal("", "ویرایش با موفقیت انجام شد", "success");
    setInterval(function () {
        location.reload();
    }, 1500);
}

function EditOrderFailur() {
    swal("", "متاسفانه ویرایش با شکست مواجه شد", "warning");
    setInterval(function () {
        location.reload();
    }, 1500);
}

function DeleteOrderDetail(id) {
    $.get("/UserPanel/MyProducts/DeleteOrderDetail/" + id,
        function (res) {
            location.reload();
        });
}