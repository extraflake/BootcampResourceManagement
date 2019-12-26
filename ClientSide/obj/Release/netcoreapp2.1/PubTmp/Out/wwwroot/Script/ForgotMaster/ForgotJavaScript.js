function isValidEmail(email) {
    let re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function forgotPassword() {
    let userEmail = $("#email").val();
    console.log(userEmail);
    if (!isValidEmail(userEmail)) {
        Swal.fire(
            'Error',
            'Email is invalid',
            'error'
        )
        return;
    }

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
        window.location.href = '/Home';
    }, 1500);

    let forgotPasswordReq = { "email": userEmail };
    $.ajax({
        type: "POST",
        url: "/Forgot/SendForgotPassword/",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: forgotPasswordReq
    }).then((res) => {
        debugger;
        var result = res.substring(14, 3);
        if (result == "200") {
            document.getElementById("Progress_Status").hidden = true;
            Swal.fire('Success', 'Forgot Password Email sent Successfully', 'success');
            ResetTable();
            $('#myModal').modal('hide');
            ClearScreen();
        }
        else {
            Swal.fire('Error', 'Forgot Password Email Fail to sent', 'error');
            ResetTable();
            $('#myModal').modal('hide');
            ClearScreen();
        }
    });
}