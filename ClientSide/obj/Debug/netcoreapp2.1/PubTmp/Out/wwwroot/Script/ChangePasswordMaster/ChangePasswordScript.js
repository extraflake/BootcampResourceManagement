$(document).ready(function () {
    GetTokenByEmail();
});

function ValidatePassword(password) {
    let re = /^[a-zA-Z0-9!@#\$%\^\&*\)\(+=._-]{6,}$/;
    return re.test(String(password));
};

$(".btnShow1").mousedown(function () {
    $(".pwd1").attr("type", "text");
});

$(".btnShow1").on("mouseleave", function () {
    $(".pwd1").attr("type", "password");
});

$(".btnShow2").mousedown(function () {
    $(".pwd2").attr("type", "text");
});

$(".btnShow2").on("mouseleave", function () {
    $(".pwd2").attr("type", "password");
});

$(".btnShow3").mousedown(function () {
    $(".pwd3").attr("type", "text");
});

$(".btnShow3").on("mouseleave", function () {
    $(".pwd3").attr("type", "password");
});

function ChangePassword() {
    if ($('#OldPassword').val() == "" || $('#OldPassword').val() == " ") {
        Swal.fire("Oops", "Please Insert Old Password", "error")
    }
    else if ($('#OldPassword').val() != $('#OldPassword').val()) {
        Swal.fire("Oops", "Old Password Not Match", "error")
    }
    else if ($('#Newpassword').val() == "" || $('#Newpassword').val() == " ") {
        Swal.fire("Oops", "Please Insert New Password", "error")
    }
    else if ($('#confirmpassword').val() == "" || $('#confirmpassword').val() == " ") {
        Swal.fire("Oops", "Please Insert Confirm Password", "error")
    }
    else if ($('#Newpassword').val() != $('#confirmpassword').val()) {
        alert("\nPassword did not match: Please try again...")
    }
    else {
        var credential = new Object();
        credential.oldpassword = $('#OldPassword').val();
        credential.newpassword = $('#Newpassword').val();
        credential.token = $('#token').val();
        $.ajax({
            type: "PUT",
            url: "/UpdatePassword",
            data: { changePasswordVM: credential }
        }).then((result) => {
            debugger;
            if (result == "200") {
                Swal.fire('Success', 'Update Successfully', 'success');
                window.location.href = "/Dashboard/";
            }
            else {
                Swal.fire('Error', 'Update Fail', 'error');
                ClearScreen();
            }
        });
    }
}

function GetTokenByEmail() {
    $.ajax({
        type: "GET",
        url: "/GetTokenByEmail"
    }).then((result) => {
        if (result != null) {
            $("#token").val(result.token);
        }
    });
}

