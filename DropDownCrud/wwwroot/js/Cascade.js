

$(document).ready(function () {
    GetUserData();
});
$("#AddUser").click(function () {
    ClearTextBox();
    $("#Cascade_model").modal('show');
    $("#header").text("Add User");
    $("#btnSave").val('Save');
    $("#btnSave").removeClass('btn-warning');
    $("#btnSave").addClass('btn-primary');
    $('#State').attr('disabled', true);
    $('#Distric').attr('disabled', true);
    GetCountries();
});
$("#Country").on("change", function () {
    let countryID = parseInt($(this).val());
    if (countryID > 0) {
        $('#State').attr('disabled', false);
        $("#Distric").val($("#Distric option:first").val());
        $('#Distric').attr('disabled', true);
        getStates(countryID);
    }
    else {
        $("#State").val($("#State option:first").val());
        $("#Distric").val($("#Distric option:first").val());
        $('#State').attr('disabled', true);
        $('#Distric').attr('disabled', true);
    }

})
$("#State").on("change", function () {
    let StateID = parseInt($(this).val());
    if (StateID > 0) {
        $('#Distric').attr('disabled', false);
        getCities(StateID);
    }
    else {
        $("#Distric").val($("#Distric option:first").val());
        $('#Distric').attr('disabled', true);
    }

})
$(".btnClose").click(function () {
    ClearTextBox();
});
function GetUserData() {
    $.ajax({
        url: '/Dropdown/GetUserList',
        type: 'GET',
        success: function (res) {
            $("#tblCascade").html('');
            $.each(res, function (i, item) {
                $('#tblCascade').append(`
                <tr>
                    <td>${item.userID}</td>
                    <td>${item.userName}</td>
                    <td>${item.email}</td>
                    <td>${item.countryName}</td>
                    <td>${item.stateName}</td>
                    <td>${item.cityName}</td>
                    <td>
                        <a class="btn btn-sm btn-primary" onclick="EditUser(${item.userID})">Edit</a> |
                        <a class="btn btn-sm btn-danger" onclick="DeleteUser(${item.userID})">Delete</a>
                    </td>
                </tr>`);
            });

        },
        error: function (error) {

        }

    });
}
function GetCountries(countyID) {
    $.ajax({
        url: '/Dropdown/GetCountryList',
        method: 'GET',
        success: function (res) {
            $('#Country').html('');
            $('#Country').html('<option>--Select Country--</option>');
            if (countyID == null || countyID == '') {
                $.each(res, function (i, item) {
                    $('#Country').append('<option value=' + item.cid + '>' + item.countryName + '</option>');
                });
            }
            else {
                $.each(res, function (i, item) {
                    if (countyID == item.cid) {
                        $('#Country').append('<option value=' + item.cid + ' selected>' + item.countryName + '</option>');
                    }
                    else {
                        $('#Country').append('<option value=' + item.cid + '>' + item.countryName + '</option>');
                    }

                });
            }

        },
        error: function (err) {

        }
    });
}
function getStates(CountryID, stateID) {
    $.ajax({
        url: '/Dropdown/GetStateList?CountryID=' + CountryID,
        method: 'GET',
        success: function (res) {
            $('#State').html('');
            $('#State').html('<option>--Select State--</option>');
            if (stateID == null || stateID == '') {
                $.each(res, function (i, item) {
                    $('#State').append('<option value=' + item.sId + '>' + item.stateName + '</option>');
                });
            }
            else {
                $.each(res, function (i, item) {
                    if (stateID == item.sId) {
                        $('#State').attr('disabled', false);
                        $('#State').append('<option value=' + item.sId + ' selected>' + item.stateName + '</option>');
                    }
                    else {
                        $('#State').append('<option value=' + item.sId + '>' + item.stateName + '</option>');
                    }
                });
            }
        },
        error: function (err) {

        }
    });
}
function getCities(StateID, cityID) {
    $.ajax({
        url: '/Dropdown/GetDistricList?StateID=' + StateID,
        method: 'GET',
        success: function (res) {
            $('#Distric').html('');
            $('#Distric').html('<option>--Select Distric--</option>');
            if (cityID == null || cityID == '') {
                $.each(res, function (i, item) {
                    $('#Distric').append('<option value=' + item.distID + '>' + item.districName + '</option>');
                });
            }
            else {
                $.each(res, function (i, item) {
                    if (cityID == item.distID) {
                        $('#Distric').attr('disabled', false);
                        $('#Distric').append('<option value=' + item.distID + ' selected>' + item.districName + '</option>');
                    }
                    else {
                        $('#Distric').append('<option value=' + item.distID + '>' + item.districName + '</option>');
                    }
                });
            }
            
        },
        error: function (err) {

        }
    });
}
function ClearTextBox() {
    $("#UserId").val(' ');
    $("#Name").val(' ');
    $("#Email").val(' ');
    $("#Country").val($("#Country option:first").val());
    $("#State").val($("#State option:first").val());
    $("#Distric").val($("#Distric option:first").val());
}
function GetFormData() {
    var userData = {};
    userData.UID = $("#UserId").val();
    userData.UserName = $("#Name").val();
    userData.Email = $("#Email").val();
    userData.CountyID = $("#Country").val();
    userData.StateID = $("#State").val();
    userData.CityID = $("#Distric").val();
    userData.IsActive = true;
    return userData;
}
function SaveUserData() {
    var data = GetFormData();
    $.ajax({
        url: '/Dropdown/AddUser',
        method: 'POST',
        data: data,
        success: function (res) {
            console.log(res);
            $("#Cascade_model").modal('hide');
            GetUserData();
            $("#tblNotification").html(res.message);
            $("#tblNotification").css('display', 'block');
            setTimeout(function () {
                $("#tblNotification").fadeOut();
            }, 1000);
            ClearTextBox();
        },
        error: function (error) {

        }

    });
}
function EditUser(UserID) {
    $.ajax({
        url: '/Dropdown/GetUserDataByID?UserID=' + UserID,
        method: 'GET',
        success: function (res) {
            console.log("res", res);
            ClearTextBox();
            $("#Cascade_model").modal('show');
            $("#header").text('Update User');
            $("#btnSave").val('Update');
            $("#btnSave").removeClass('btn-primary');
            $("#btnSave").addClass('btn-warning');
            $("#UserId").val(res.uid);
            $("#Name").val(res.userName);
            $("#Email").val(res.email);
            GetCountries(res.countyID);
            getStates(res.countyID, res.stateID);
            getCities(res.stateID, res.cityID);
        },
        error: function (err) {
            alert("data not received");
        }
    });

}
function DeleteUser(UserID) {
    $.ajax({
        url: '/Dropdown/DeleteUser?UserID=' + UserID,
        method: 'POST',
        success: function (res) {
            alert(res.message);
            GetUserData();
        },
        error: function (err) {

        }
    })
}
