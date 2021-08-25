function addFeature() {
    $.ajax({
        url: "/Admin/ManageProducts/AddFeature",
        type: "Get",
        data: { id: $("#Features :selected").val(), title: $("#Features :selected").text(), value: $("#txtValue").val() }
    }).done(function (res) {
        $("#listfeature").html(res);
        $("#txtValue").val("");
    });
}
function AddFeatureForEdit(id) {

    $.ajax({
        url: "/Admin/ManageProducts/AddFeatureForEdit",
        type: "Get",
        data: { id: $("#Features :selected").val(), title: $("#Features :selected").text(), value: $("#txtValue").val(), ProductId: id }
    }).done(function (res) {
        $("#listfeature").html(res);
        $("#txtValue").val("");
    });
}

function DeleteFeature(id, value) {
    $.get("/Admin/ManageProducts/RemoveFeature/" + id + "?value=" + value, function (res) {

        $("#listfeature").html(res);
    });
}

function DeleteFeatureForEdit(value, id, PFID) {
    $.get("/Admin/ManageProducts/RemoveFeatureForEdit/" + id + "?value=" + value + "&PfID=" + PFID, function (res) {

        $("#listfeature").html(res);
    });
}


function DeleteProductGalleryImage(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/DeleteProductGallery?galleryId=" + id, function (xhr, statusText) {
            if (statusText === "success") {
                $("#ProductGalleryImageBox_" + id).fadeOut(200);
            } else {
                alert("Error");
            }
        });
    });
}

function DeleteProduct(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/DeleteProduct?productId=" + id, function (xhr, statusText) {
            if (statusText === "success") {
                location.reload();
            } else {
                alert("Error");
            }
        });
    });
}

function ReturnProduct(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/ReturnProduct?productId=" + id, function (xhr, statusText) {
            if (statusText === "success") {
                location.reload();
            } else {
                alert("Error");
            }
        });
    });
}

$(document).ready(function () {
    $("#MainGroupId").change(function () {
        $("#SubGroupId").empty();
        $.ajax({
            type: 'Get',
            url: '/Admin/ManageProducts/GetSubGroups/',
            dataType: 'json',
            data: { id: $("#MainGroupId").val() },
            success: function (subs) {
                $.each(subs, function (i, sub) {
                    $("#SubGroupId").append('<option value="' + sub.Value + '">' + sub.Text + '</option>');
                });
            },
            error: function (ex) {
                alert('Failed to retrieve states.' + ex);
            }
        });
        return false;
    });

    $('#Tags').tagsinput({
        tagClass: 'big'
    });
    $('#Tags').tagsinput({
        onTagExists: function (item, $tag) {
            $tag.hide.fadeIn();
        }
    });

});

function EditFeature(id) {

    var feature = $("#Feature_" + id).val();

    var data = [id, feature];

    $.get("/Admin/ManageProducts/EditFeatures?featureId=" + id + "&featureValue=" + feature, function (res) {
        if (res) {
            alert("مقدار مورد نظر با موفقیت تغییر یافت");
        } else {
            alert("در تغییر مقدار خطایی رخ داد");
        }
    });
}