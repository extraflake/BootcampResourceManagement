function isValidEmail(email) {
    let re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function updateToken(email) {
    debugger;
    let userEmail = email;
    console.log(userEmail);
    let timerInterval
    Swal.fire({
        timer: 10000,
        imageUrl: '../images/loadingtwo.gif',
        showConfirmButton: false,
        background: 'rgba(0,0,123,0) ',

        onClose: () => {
            clearInterval(timerInterval)
        }

    }).then((result) => {
        if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.timer
        ) {
            console.log('I was closed by the timer')
        }
    })
    if (!isValidEmail(userEmail)) {
        Swal.fire(
            'Error',
            'Email is invalid',
            'error'
        )
        return;
    }

    let updateTokenReq = { "email": userEmail };
    $.ajax({
        type: "POST",
        url: "/UpdateToken/SendUpdateTokens/",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: updateTokenReq,
        error: function (result) {
            alert(JSON.stringify(result));
        },
        success: function (result) {
            debugger;
            Swal.fire(
                result.IsSuccessStatusCode === false ? 'Error' : 'Success',
                result.IsSuccessStatusCode === false ? res.ReasonPhrase : 'Change Password Email sent Successfully',
                result.IsSuccessStatusCode === false ? 'error' : 'success'
            );
        }
    });

}