$(document).ready(function () {
	debugger;
    CheckingSession();
});

function CheckingSession() {
	debugger;
    $.ajax({
        url: "Home/CheckingSession",
        dataType: "json"
    }).then((response) => {
        debugger;
		if(response.available == "")
		{
			console.log("active");
		}
		else {
			console.log("not-active");
		}
	});
}