var isSelected = false;

function LoadSubPermission(permissionId) {
    var permission = $("#" + permissionId);
    var x = isSelected;
    if (permission.is(':checked')) {
        $.get("/Admin/ManageUsers/GetSubPermissions?permissionId=" + permissionId,
            function (res) {
                if (res !== null) {
                    $.each(res, function (i, val) {
                        $("#P_" + permissionId).append('<ul class="UlList"><li><div class="checkbox"><label><input type="checkbox" class="colored-success" value="' + val.Value + '" name="SelectedPermissions"><span class="text">' + val.Text + '</span></label></div ></li></ul>');
                    });
                }
            });
    } else {

        $("#P_" + permissionId + " ul").empty();
    }

}

function DeleteRole(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteRole?roleId=" + id, function (res) {
            location.reload();
        });
    });
}

function SetDefaultRole(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/SetDefaultRole?roleId=" + id, function (res) {
            location.reload();
        });
    });
}
