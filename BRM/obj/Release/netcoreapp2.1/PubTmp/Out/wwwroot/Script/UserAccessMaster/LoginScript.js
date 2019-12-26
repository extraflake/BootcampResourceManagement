$(document).ready(function () {
    //your code here
});

function ValidateEmail(email) {
    let re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function ValidatePassword(pword) {
    var status = false;
    if (pword == "" || pword == " ") {
        return status;
    }
    else {
        status = true;
        return status;
    }
}

function Validate() {
    let userEmail = $("#user").val();
    if (!ValidateEmail(userEmail)) {
        Swal.fire("Oops", "Please Insert Email Properly", "error");
    }
    else if ($('#user').val() == "" || $('#user').val() == " ") {
        Swal.fire(
            'Oops!',
            'Please Insert Email',
            'error'
        )
    }
    else if ($('#pass').val() == "" || $('#pass').val() == " ") {
        Swal.fire("Oops", "Please Insert Password", "error");
    }
    else {
        LoginProcess($('#user').val(), $('#pass').val());
    }
}

function LoginProcess(mail, pword) {
    $.ajax({
        url: "/Logins/Validate/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { email: mail, password: pword },
        success: function (result) {
            if (result.StatusCode == 200) {
                let timerInterval
                Swal.fire({
                    timer: 1500,
                    imageUrl: '../images/loadingtwo.gif',
                    showConfirmButton: false,
                    background: 'rgba(0,0,123,0) ',

                    onClose: () => {
                        clearInterval(timerInterval)
                    }

                }).then((result) => {
                    if (
                        result.dismiss === Swal.DismissReason.timer
                    ) {
                        console.log('I was closed by the timer')
                    }
                })
                setTimeout(function () {
                    window.location.href = '/Home/';
                }, 1500);

            }
            else {
                Swal.fire('Oops',
                    'User Credential Not Match!',
                    'error');
            }
        },
        error: function (result) {
            Swal.fire('Oops',
                'Something Went Wrong',
                'error');
        }
    });
}