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
    debugger;
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
    debugger;
    $.ajax({
        url: "/Logins/Validate/",
        type: "GET",
        data: { email: mail, password: pword },
    }).then((result) => {
		debugger;
        if (result.StatusCode == 200) {
			let timerInterval
            Swal.fire({
				timer: 3000,
                imageUrl: '../images/loadingtwo.gif',
                showConfirmButton: false,
                background: 'rgba(0,0,123,0) ',

				onClose: () => {
					clearInterval(timerInterval)
                }
			}).then((result) => {
				if (result.dismiss === Swal.DismissReason.timer) {
					console.log('I was closed by the timer')
				}
			})
            setTimeout(function () {
				window.location.href = '/Dashboard/';
			}, 3000);
		}
        else {
            debugger;
			Swal.fire('Oops', 'User Credential Not Match!', 'error');
        }
	});
}