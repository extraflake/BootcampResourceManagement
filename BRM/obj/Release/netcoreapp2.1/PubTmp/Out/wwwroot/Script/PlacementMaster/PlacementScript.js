$(document).ready(function () {
	$('#table').DataTable({
		"columnDefs": [{
			"targets": 'no-sort',
			"orderable": false,
		}],
		"ajax": LoadIndexPlacement()
	});

	$(".select2").select2({
		placeholder: "Select Participant"
	});

	$(".ajax").select2({
		ajax: {
			url: "https://api.github.com/search/repositories",
			dataType: 'json',
			delay: 250,
			data: function (params) {
				return {
					q: params.term,
					page: params.page
				};
			},
			processResults: function (data, params) {
				params.page = params.page || 1;
				return {
					results: data.items,
					pagination: {
						more: (params.page * 30) < data.total_count
					}
				};
			},
			cache: true
		},
		escapeMarkup: function (markup) {
			return markup;
		},
		minimumInputLength: 1
	});
});

function ResetTable() {
    $('#table').DataTable().destroy();
    $('#table').DataTable({
        "ajax": LoadIndexPlacement()
    })
}

function ClearScreen()
{
	$('#IId_Placement').val('');
    $('#INIK').val('');
    $('#IName').val('').change();
	$('#IDepartment').val('');
	$('#IStart_Date').val('');
    $('#ICustomerSite').val('').change();
	$('#IRM_Id').val('');
	$('#IRM').val('');
	$('#IPhone').val('');
	$('#INote').val('');
	$('#Update').hide();
	$('#Save').show();
}

function ValidateName(name)
{
	var nameReg = /^[A-Z][a-z 0-9]+$/i;
	return nameReg.test(name);
}

function ValidateUpdate()
{
	if ($('#UDepartment').val() == 0) {
		Swal.fire('Error', 'Please Enter the Department', 'error');
	} else if (!ValidateName($('#UDepartment').val())) {
		Swal.fire('Error', 'Please Enter the Department just number and character', 'error');
	} else if ($('#UStart_Date').val() == 0) {
		Swal.fire('Error', 'Please Enter the Start Date', 'error');
	} else if ($('#UCustomerSite').val() == 0) {
		Swal.fire('Error', 'Please Choose Customer Site', 'error');
	} else {
		Edit();
	}
}

function ValidateInsert() {
	if ($('#IName').val() == 0) {
		Swal.fire('Error', 'Please Choose Participants', 'error');
	} else if ($('#IDepartment').val() == 0) {
		Swal.fire('Error', 'Please Enter the Department', 'error');
	} else if (!ValidateName($('#IDepartment').val())) {
		Swal.fire('Error', 'Please Enter the Department just number and character', 'error');
	} else if ($('#IStart_Date').val() == 0) {
		Swal.fire('Error', 'Please Enter the Start Date', 'error');
	} else if ($('#ICustomerSite').val() == 0) {
		Swal.fire('Error', 'Please Choose Customer Site', 'error');
	} else {
		Save();
	}
}

function LoadIndexPlacement() {
    var finish = null;
	$.ajax
		({
			type: "GET",
			async: false,
			url: "/Placements/LoadPlacement/",
			success: function (data) {
				var html = '';
				var i = 1;
                $.each(data, function (index, val) {
                    if (val.finish_date.substring(0, 10) == "0001-01-01") {
                        finish = "-";
                    }
                    else {  
                        finish = moment(val.finish_date).format('DD/MM/YYYY')
                    }
					html += '<tr>';
					html += '<td style="text-align:center">' + i + '</td>';
					html += '<td>' + val.employee + '</td>';
					html += '<td>' + val.name + '</td>';
                    html += '<td>' + moment(val.start_date).format('DD/MM/YYYY') + '</td>';
                    html += '<td>' + finish + '</td>';
					html += '<td>' + val.department + '</td>';
					html += '<td>' + val.customer + '</td>';
					html += '<td>' + val.relation_manager + '</td>';
					html += '<td>' + val.notes + '</td>';
					html += '<td  style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(' + val.id + ')"></a>';
					html += ' | <a href="#" class="fa fa-calendar" onclick="return GetDate(\'' + val.id + '\')"></a></td>';
					html += '</tr>';
					i++;
				});
				$('.tbody').html(html);
			}
		});
}

function ResetPlacement() {
	$('#table').DataTable().destroy();
	$('#table').DataTable({
		"columnDefs": [{
			"targets": 'no-sort',
			"orderable": false,
		}],
		"ajax": LoadIndexPlacement(),
		"orderMulti": false
	});
}

function LoadFilterPlacement(value) {
	$.ajax
		({
			type: "GET",
			async: false,
			url: "/Placements/LoadCustomer/",
			data: value,
			success: function (data) {
				var html = '';
				var i = 1;
                $.each(data, function (index, val) {
                    debugger;
					html += '<tr>';
                    html += '<td style="text-align:center">' + i + '</td>';
                    html += '<td>' + val.employee + '</td>';
					html += '<td>' + val.name + '</td>';
					html += '<td>' + val.start_date.substring(0, 10) + '</td>';
					html += '<td>' + val.finish_date.substring(0, 10) + '</td>';
					html += '<td>' + val.department + '</td>';
					html += '<td>' + val.customer_name + '</td>';
					html += '<td>' + val.relation_manager + '</td>';
					html += '<td>' + val.notes + '</td>';
					html += '<td  style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(' + val.id + ')"></a>';
					html += ' | <a href="#" class="fa fa-calendar" onclick="return GetDate(\'' + val.id + '\')"></a></td>';
					html += '</tr>';
					i++;
				});
				$('.tbody').html(html);
			}
		});
}

function LoadIndexCustomer(value) {
	//debugger;
	$('#table').DataTable().destroy();
	$('#table').DataTable({
		"columnDefs": [{
			"target": 'no-sort',
			"orderable": false,
		}],
		"ajax": LoadFilterPlacement(value),
		"orderMulti": false
	});
}

function Save() {
	var placement = new Object();
	placement.employee = $('#IName').val();
	placement.department = $('#IDepartment').val();
	placement.start_date = $('#IStart_Date').val();
	placement.customer = $('#ICustomerSite').val();
	placement.relation_name = $('#IRM_Id').val();
	placement.relation_phone = $('#IPhone').val();
	placement.notes = $('#INote').val();
	$.ajax({
		type: 'POST',
		url: '/Placements/Insert/',
		contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: placement
    }).then((result) => {
        debugger;
        //if (result.StatusCode == '200' || result.StatusCode == 'OK') {
        //    Swal.fire({
        //        position: 'center',
        //        type: 'success',
        //        title: 'Insert Successfully'
        //    });
        //    ResetTable();
        //    $('#myModal').modal('hide');
        //    ClearScreen();
        //}
        //else {
        //    Swal.fire('Error', 'Insert Fail', 'error');
        //    ClearScreen();
        //}
        if (result > 0 ) {
            Swal.fire({
                position: 'center',
                type: 'success',
                title: 'Success '+ result + ' row, Insert Successfully'
            });
            ResetTable();
            $('#myModal').modal('hide');
            ClearScreen();
        }
        else {
            Swal.fire('Insert Fail', 'Please Set Finish Date, Before Assign to Another Site', 'error');
            ClearScreen();
        }
    });

}

function GetById(Id) {
	$.ajax({
		url: "/Placements/GetById/",
		type: "GET",
		dataType: 'json',
		contentType: 'application/x-www-form-urlencoded; charset=utf-8',
		data: { id: Id },
        success: function (result) {
            debugger;
			$('#UId_Placement').val(result.id);
			$('#UNIK').val(result.employee);
			$('#UName').val(result.name);
			$('#UDepartment').val(result.department);
			$('#UStart_Date').val(result.start_date.substring(0,10));
			$('#UCustomerSite').val(result.customer);
			$('#URM_Id').val(result.rm_id);
			$('#URM').val(result.relation_manager);
			$('#UPhone').val(result.rm_phone);
			$('#UNote').val(result.notes);
			$('#myModalUpdate').modal('show');
			$('#Update').show();
			$('#Save').hide();
		}
	})
}

function GetDate(Id) {
	//debugger;
	$.ajax({
		url: "/Placements/GetById/",
		type: "GET",
		dataType: 'json',
		contentType: 'application/x-www-form-urlencoded; charset=utf-8',
		data: { id: Id },
		success: function (result) {
			//debugger;
			$('#FId_Placement').val(result.id);
			$('#FFinish_Date').val(result.finish_date);
            $('#FStartDate_Placement').val(result.start_date);

			$('#myFinishDate').modal('show');
			$('#Update').show();
			$('#Save').hide();

		}
	})
}

function Edit() {
    if ($('#FFinish_Date').val() < $('#FStartDate_Placement').val()) {
        Swal.fire('Error', 'Finish Date cannot be small than Start Date', 'error')
    }
    else {
        var placement = new Object();
        placement.id = $('#UId_Placement').val();
        placement.relation_name = $('#URM_Id').val();
        placement.relation_phone = $('#UPhone').val();
        placement.department = $('#UDepartment').val();
        placement.employee = $('#UNIK').val();
        placement.start_date = $('#UStart_Date').val();
        placement.customer = $('#UCustomerSite').val();
        placement.notes = $('#UNote').val();
        placement.updated_by = '13306';
        $.ajax({
            url: "/Placements/Update",
            data: placement
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Update Successfully'
                });
                ResetTable();
                $('#myModalUpdate').modal('hide');
                ClearScreen();
            }
            else {
                Swal.fire('Error', 'Update Fail', 'error');
                ClearScreen();
            }
        });

    }
}

function FinishDate() {
    debugger;
    if ($('#FFinish_Date').val() < $('#FStartDate_Placement').val()) {
        Swal.fire('Error', 'Finish Date cannot be small than Start Date', 'error')
    }
    else {
        //debugger;
        var placement = new Object();
        placement.id = $('#FId_Placement').val();
        placement.finish_date = $('#FFinish_Date').val();

        $.ajax({
            url: "/Placements/FinishDate",
            data: placement,
            success: function (result) {
                //debugger;
                Swal.fire(
                    'Success',
                    'Your file has been updated.',
                    'success'
                )
                $('#myFinishDate').modal('hide');
                ResetTable();
                ClearScreen();
            },
            error: function () {
                Swal.fire('Error', 'Failed to Update', 'error')
            }
        });
    }
}

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
				url: "/Placements/Delete/",
				data: { id: Id },
                type: "DELETE"
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Delete Successfully'
                    });
                    ResetTable();
                    ClearScreen();
                }
                else {
                    Swal.fire('Error', 'Delete Fail', 'error');
                    ClearScreen();
                }
            });
		}
	})
}

//Select2 column name
var ParticipantDisplays = []
function LoadParticipantDisplay(element) {
	if (ParticipantDisplays.length == 0) {
		$.ajax({
			type: "GET",
			url: "/ParticipantDisplays/LoadParticipantDisplay/",
			success: function (data) {
				ParticipantDisplays = data;
				renderParticipantDisplay(element);
			}
		})
	}
	else {
		renderParticipantDisplay(element);
	}
}

function renderParticipantDisplay(element) {
	var $ele = $(element);
	$ele.empty();
	$ele.append($('<option/>').val('0').text('Select Participant').hide);
	$.each(ParticipantDisplays, function (i, val) {
		$ele.append($('<option/>').val(val.id).text(val.name));
	})
}
LoadParticipantDisplay($('#IName'));

//dropdown insert customer site
var Customers = []
function LoadCustomer(element) {
	if (Customers.length == 0) {
		$.ajax({
			type: "GET",
			url: "/Customers/LoadCustomer",
			success: function (data) {
				Customers = data;
				renderCustomer(element);

			}
		})
	}
	else {
		renderCustomer(element);
	}
}

function renderCustomer(element) {
	var $ele = $(element);
	$ele.empty();
	$ele.append($('<option/>').val('0').text('Select Customer Site').hide());
	$.each(Customers, function (i, val) {
		$ele.append($('<option/>').val(val.id).text(val.name));
	})
}
LoadCustomer($('#ICustomerSite'));
LoadCustomer($('#UCustomerSite'));
LoadCustomer($('#SortCustomer'));

//dropdown filtering datatable relation manager
var RelationManager = []
function LoadRelationManager(element) {
	if (RelationManager.length == 0) {
		$.ajax({
			url: "/Customers/LoadRelationManager/",
			success: function (data) {
				RelationManager = data;
				renderRelationManager(element);
			}
		})
	}
	else {
		renderRelationManager(element);
	}
}

function renderRelationManager(element) {
	var $ele = $(element);
	$ele.empty();
	$ele.append($('<option/>').val('0').text('Select Relation Manager').hide());
	$.each(RelationManager, function (i, val) {
		$ele.append($('<option/>').val(val.id).text(val.relation_manager));
	})
}
LoadRelationManager($('#SortRelationManager'));

function GetRM() {
	$.each(Customers, function (id, val) {
		if (val.name = $('#ICustomerSite').val()) {
			$('#IRM').val(val.relation_manager).text(val.relation_manager);
			$('#IPhone').val(val.phone_relation_manager);
			$('#IRM_Id').val(val.nik).text(val.relation_name);
		}
		else {
			$('#IRM').val('');
			$('#IPhone').val('');
			$('#IRM_Id').val('');
		}
	})
}

function GetRM2() {
	$.each(Customers, function (id, val) {
		if (val.name = $('#UCustomerSite').val()) {
			$('#URM').val(val.relation_manager).text(val.relation_manager);
			$('#UPhone').val(val.phone_relation_manager);
			$('#URM_Id').val(val.nik).text(val.relation_name);
		}
		else {
			$('#URM').val('');
			$('#UPhone').val('');
			$('#URM_Id').val('');
		}
	})
}

//onchange filter 
function LoadFilter() {
	var asset = new Object();
	asset.customer = $('#SortCustomer').val();
	asset.relation_manager = $('#SortRelationManager').val();
	//var value = $('#SortCustomer').val();
	if (asset == 0 || asset.customer == '0' && asset.relation_manager == '0') {
		//LoadIndexPlacement();
		ResetPlacement();
	}
	else {
		LoadIndexCustomer(asset);
	}
}