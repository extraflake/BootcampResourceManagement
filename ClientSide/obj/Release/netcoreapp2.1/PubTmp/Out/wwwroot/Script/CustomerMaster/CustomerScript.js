$(document).ready(function () {
	$('#table').DataTable({
		"order": [],
		"columnDefs": [{
			"targets": 'no-sort',
			"orderable": false,
		}],
		"ajax": LoadIndexCustomer(),
		"responsive": true
	});
	$("#Province").change(function () {
		debugger;
		var get = $('#Province').val();
		$.ajax({
			type: "GET",
			url: "/Districts/LoadDistrictsByParam/",
			data: { 'param': get },
			success: function (data) {
				renderDistrict($("#District").parents('.modal-body').find('select.district'), data);
				debugger;
			},
			error: function (error) {
				console.log(error);
			}
		})
	})
	$("#District").change(function () {
		debugger;
		var get = $('#District').val();
		$.ajax({
			type: "GET",
			url: "/SubDistricts/LoadSubDistrictsByParam/",
			data: { 'param': get },
			success: function (data) {
				renderSubDistrict($("#Subdistrict").parents('.modal-body').find('select.subdistrict'), data);
				debugger;
			},
			error: function (error) {
				console.log(error);
			}
		})
	})
	$("#Subdistrict").change(function () {
		debugger;
		var get = $('#Subdistrict').val();
		$.ajax({
			type: "GET",
			url: "/Villages/LoadVillagesByParam/",
			data: { 'param': get },
			success: function (data) {
				renderVillage($("#Village").parents('.modal-body').find('select.village'), data);
				debugger;
			},
			error: function (error) {
				console.log(error);
			}
		})
	})
});

function LoadIndexCustomer() {
	$.ajax({
		type: "GET",
		async: false,
		url: "/Customers/LoadCustomer/",
		success: function (data) {
			var html = '';
			var i = 1;
			$.each(data, function (index, val) {
				//debugger;
				html += '<tr>';
				html += '<td style="text-align:center">' + i + '</td>';
				html += '<td>' + val.name + '</td>';
				html += '<td>' + val.address + '</td>';
				html += '<td>' + val.phone + '</td>';
				html += '<td>' + val.relation_manager + '</td>';
				html += '<td>' + val.village + '</td>';
				html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.id + '\')"></a>';
				html += ' | <a href="#" class="fa fa-trash" onclick="return Delete(\'' + val.id + '\')"></a></td>';
				html += '</tr>';
				i++;
			});
			$('.tbody').html(html);
		}
	});
}
	
function GetById(Id) {
	//debugger;
	$.ajax({
		url: "/Customers/GetById/",
		type: "GET",
		dataType: 'json',
		contentType: 'application/x-www-form-urlencoded; charset=utf-8',
		data: { id: Id },
		success: function (result) {
			//debugger;
			//var birthDate = moment(result.birth_date).format('MM/DD/YYYY');
			//var getPhone = result.phone.substring(3, result.phone.length);
			document.getElementById("Id").disabled = true;
			$('#Id').val(result.id);
			$('#Name').val(result.name);
			$('#Address').val(result.address);
			$('#Phone').val(result.phone);
			$('#RelationManager').val(result.relation_manager);
			$('#Province').val(2);
			$('#District').val(10);

			$('#myModal').modal('show');
			$('#Update').show();
			$('#Save').hide();
		}
	})
}

function Save() {
	if ($('#Id').val() == "" || $('#Id').val() == " " || $('#Id').val().length <= 2) {
		Swal.fire("Oops", "Please Insert ID", "error")
	} else if ($('#Name').val() == "" || $('#Name').val() == " " || $('#Name').val().length <= 2) {
		Swal.fire("Oops", "Please Insert First Name Properly", "error")
	} else if ($('#RelationManager').val() == 0) {
		Swal.fire("Oops", "Please Insert Relation Manager", "error")
	} else if ($('#Province').val() == 0 || $('#District').val() == 0) {
		Swal.fire("Oops", "Please Insert Village", "error")
	} else if ($('#Phone').val() == "" || $('#Phone').val() == " ") {
		Swal.fire("Oops", "Please Insert Phone", "error")
	} else if ($('#Phone').val().length < 9 || $('#Phone').val().length > 16) {
		Swal.fire("Oops", "Please Length Phone min 9 digit and max 16 digit", "error")
	} else {
		var customer = new Object();
		customer.id = $('#Id').val();
		customer.name = $('#Name').val();
		customer.address = $('#Address').val();
		customer.phone = $('#Phone').val();
		customer.relation_manager = $('#RelationManager').val();
		customer.village = $('#District').val();

		var validatePhone = $('#Phone').val(); region = "+62";
		if (validatePhone.substring(0, 1) == "0") {
			customer.Phone = validatePhone.replace(validatePhone.substring(0, 1), "+62");
			window.alert(customer.Phone);
		} else if (validatePhone.substring(0, 1) == "1" ||
			validatePhone.substring(0, 1) == "2" ||
			validatePhone.substring(0, 1) == "3" ||
			validatePhone.substring(0, 1) == "4" ||
			validatePhone.substring(0, 1) == "5" ||
			validatePhone.substring(0, 1) == "6" ||
			validatePhone.substring(0, 1) == "7" ||
			validatePhone.substring(0, 1) == "8" ||
			validatePhone.substring(0, 1) == "9") {
			customer.Phone = region.concat(validatePhone);
			window.alert(customer.Phone);
		}
		$.ajax({
			type: 'POST',
			url: '/Customers/Insert/',
			contentType: 'application/x-www-form-urlencoded; charset=utf-8',
			data: customer
		}).then((result) => {
			if (result.StatusCode == 200) {
				Swal.fire('Success', 'Insert Successfully', 'success');
				window.location.href = "/Customers/";
			}
			else {
				Swal.fire('Error', 'Insert Fail', 'error');
				ClearScreen();
			}
		});
	}
}

function Update() {
	if ($('#Id').val() == "" || $('#Id').val() == " " || $('#Id').val().length <= 2) {
		Swal.fire("Oops", "Please Insert ID", "error")
	} else if ($('#Name').val() == "" || $('#Name').val() == " " || $('#Name').val().length <= 2) {
		Swal.fire("Oops", "Please Insert First Name Properly", "error")
	} else if ($('#RelationManager').val() == 0) {
		Swal.fire("Oops", "Please Insert Relation Manager", "error")
	} else if ($('#Province').val() == 0 || $('#District').val() == 0) {
		Swal.fire("Oops", "Please Insert Village", "error")
	} else if ($('#Phone').val() == "" || $('#Phone').val() == " ") {
		Swal.fire("Oops", "Please Insert Phone", "error")
	} else if ($('#Phone').val().length < 9 || $('#Phone').val().length > 16) {
		Swal.fire("Oops", "Please Length Phone min 9 digit and max 16 digit", "error")
	} else {
		var customer = new Object();
		customer.id = $('#Id').val();
		customer.name = $('#Name').val();
		customer.address = $('#Address').val();
		customer.phone = $('#Phone').val();
		customer.relation_manager = $('#RelationManager').val();
		customer.village = $('#District').val();
		var validatePhone = $('#Phone').val(); region = "+62";
		if (validatePhone.substring(0, 1) == "0") {
			customer.Phone = validatePhone.replace(validatePhone.substring(0, 1), "+62");
			window.alert(customer.Phone);
		} else if (validatePhone.substring(0, 1) == "1" ||
			validatePhone.substring(0, 1) == "2" ||
			validatePhone.substring(0, 1) == "3" ||
			validatePhone.substring(0, 1) == "4" ||
			validatePhone.substring(0, 1) == "5" ||
			validatePhone.substring(0, 1) == "6" ||
			validatePhone.substring(0, 1) == "7" ||
			validatePhone.substring(0, 1) == "8" ||
			validatePhone.substring(0, 1) == "9") {
			customer.Phone = region.concat(validatePhone);
			window.alert(customer.Phone);
		}
		$.ajax({
			url: "/Customers/Update/",
            data: customer
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire('Success', 'Update Successfully', 'success');
                window.location.href = "/Customers/";
            }
            else {
                Swal.fire('Error', 'Update Fail', 'error');
                ClearScreen();
            }
        });
	}
};

function Delete(Id) {
	Swal.fire({
		title: 'Are you sure?',
		text: "You won't be able to revert this!",
		type: 'warning',
		showCancelButton: true,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: '#d33',
		confirmButtonText: 'Yes, delete it!'
	}).then((result) => {
		if (result.value) {
			$.ajax({
				url: "/Customers/Delete/",
				data: { id: Id },
                type: "DELETE"
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire('Success', 'Delete Successfully', 'success');
                    window.location.href = "/Assets/";
                }
                else {
                    Swal.fire('Error', 'Delete Fail', 'error');
                    ClearScreen();
                }
            });
		}
	})
}

function ClearScreen() {
	$('#Id').val('');
	$('#Name').val('');
	$('#Address').val('');
	$('#Phone').val('');
	$('#RelationManager').val(0);
	$('#Province').val(0);
	$('#District').val('');
	$('#Update').hide();
	$('#Save').show();
}