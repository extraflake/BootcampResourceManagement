$(document).ready(function () {
	LoadIndexBatch();
	$('#table').DataTable({
		"ajax": LoadIndexBatch()
	})
})

$(function () {
	$('#startdate').datepicker({
		daysOfWeekDisabled: [0, 6]
	});
	$("#enddate").datepicker();
});

function ResetTable() {
    $('#table').DataTable().destroy();
    $('#table').DataTable({
        "ajax": LoadIndexBatch()
    })
}

function getEndDate() {
	var day = moment($("#startdate").val(), 'MM/DD/YYYY');
    
	day.add(2, 'months');

	$("#enddate").val(day.format("MM/DD/YYYY"));
}

function Save() {
    if ($('#Id').val() == "" || $('#Id').val() == " ") {
        Swal.fire("Oops", "Please Insert Batch", "error")
    } else if ($('#startdate').val() == "" || $('#startdate').val() == " ") {
        Swal.fire("Oops", "Please Insert Start Date", "error")
    } else if ($('#enddate').val() == "" || $('#enddate').val() == " ") {
        Swal.fire("Oops", "Please Insert End Date", "error")
    } else {
        var batch = new Object();
        batch.id = $('#Id').val();
        batch.startdate = $('#startdate').val();
        batch.enddate = $('#enddate').val();
        $.ajax({
            type: 'POST',
            url: '/Batches/Insert/',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: { batchVM: batch }
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire('Success', 'Insert Successfully', 'success');
                ResetTable();
                $('#myModal').modal('hide');
                ClearScreen();
            }
            else {
                Swal.fire('Error', 'Insert Fail', 'error');
                ClearScreen();
            }
        });
    }
};

function LoadIndexBatch() {
	$.ajax({
		type: "GET",
		async: false,
		url: "/Batches/LoadBatch/",
		success: function (data) {
			var html = '';
			var i = 1;
			$.each(data, function (index, val) {
				StartDate = moment(val.StartDate).format('MM/DD/YYYY');
				EndDate = moment(val.EndDate).format('MM/DD/YYYY');
				html += '<tr>';
				html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(' + val.Id + ')"></a>';
				html += ' | <a href="#" class="fa fa-trash" onclick="return Delete(' + val.Id + ')"></a></td>';
				html += '<td style="text-align:center">' + i + '</td>';
				html += '<td>' + val.Id + '</td>';
				html += '<td>' + StartDate + '</td>';
				html += '<td>' + EndDate + '</td>';
				html += '</tr>';
				i++;
			});
			$('.tbody').html(html);
		}
	});
}

function Edit() {
	var batch = new Object();
	batch.Id = $('#Id').val();
	batch.StartDate = $('#startdate').val();
	batch.EndDate = $('#enddate').val();
	$.ajax({
		url: "/Batches/InsertOrUpdate/",
        data: batch
    }).then((result) => {
        debugger;
        if (result.StatusCode == 200) {
            Swal.fire('Success', 'Update Successfully', 'success');
            window.location.href = "/Batches/";
            ResetTable();
            $('#myModal').modal('hide');
            ClearScreen();
        }
        else {
            Swal.fire('Error', 'Update Fail', 'error');
            ClearScreen();
        }
    });
};

function GetById(Id) {
	$.ajax({
		url: "/Batches/GetById/",
		type: "GET",
		dataType: 'json',
		contentType: 'application/x-www-form-urlencoded; charset=utf-8',
		data: { id: Id },
		success: function (result) {
			StartDate = moment(result.StartDate).format('MM/DD/YYYY');
			EndDate = moment(result.EndDate).format('MM/DD/YYYY');
			$('#Id').val(result.Id);
			$('#startdate').val(StartDate);
			$('#enddate').val(EndDate);

			$('#myModal').modal('show');
			$('#Update').show();
			$('#Save').hide();
		}
	})
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
				url: "/Batches/Delete/",
				data: { id: Id },
                type: "DELETE"
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire('Success', 'Delete Successfully', 'success');
                    window.location.href = "/Assets/";
                    ResetTable();
                    $('#myModal').modal('hide');
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

function ClearScreen() {
	$('#startdate').val('');
	$('#enddate').val('');
	$('#Id').val('');
	$('#Update').hide();
	$('#Save').show();
}

function Validate() {
    if ($('#Id').val() == "" || $('#Id').val() == " ") {
        Swal.fire("Oops", "Please Insert Batch", "error")
    } else if ($('#startdate').val() == "" || $('#startdate').val() == " ") {
        Swal.fire("Oops", "Please Insert Start Date", "error")
    } else if ($('#enddate').val() == "" || $('#enddate').val() == " ") {
        Swal.fire("Oops", "Please Insert End Date", "error")
    } 
}