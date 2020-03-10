var table = null;
$(document).ready(function () {
    $('#table').DataTable({
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }],
        "ajax": LoadIndexInterviewHistory(),
        "dom": 'lBfrtip',
        "buttons": [
            {
                extend: 'collection',
                text: 'Export',
                buttons: [
                    'copy',
                    'excel',
                    'csv',
                    'pdf',
                    'print'
                ]
            }
        ]
    })

    $(".select2").select2({
        placeholder: "Select Employee"
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
})

function ResetTable() {
    $('#table').DataTable().destroy();
    $('#table').DataTable({
        "ajax": LoadIndexInterviewHistory()
    })
}

function LoadIndexInterviewHistory() {
     $.ajax({
         type: "get",
         async: false,
         url: "/InterviewHistories/LoadInterviewHistoryVM/",
         success: function (data) {
			 console.log(data);
             var html = '';
             var i = 1;
             $.each(data, function (index, val) {
                 var interviewdatetime = moment(val.interview_datetime).format('DD/MM/YYYY HH:mm');
                 html += '<tr>';
                 html += '<td style="text-align:center">' + i + '</td>';
                 html += '<td>' + val.nik + '</td>';
                 html += '<td>' + val.employee + '</td>';
                 html += '<td>' + interviewdatetime + '</td>';
                 html += '<td>' + val.customer + '</td>';
                 html += '<td>' + val.pic + '</td>';
                 html += '<td>' + val.department + '</td>';
                 html += '<td>' + val.note + '</td>';
                 html += '<td style="text-align:center"> <a href="#" style="color:#55ce63" class="fa fa-pencil" onclick="return getbyid(\'' + val.id + '\')"></td>';
                 html += '</tr>';
                 i++;
             });
             $('.tbody').html(html);
         }
     });
 }

function LoadIndexInterviewHistorySort() {
    var startdate = $("#startDate").val();
    var endadate = $("#endDate").val();
    var table = $('#table').DataTable();
    table.destroy();
    $.ajax({
        type: "GET",
        async: false,
        url: "/InterviewHistories/LoadInterviewHistoryVMSort/",
        data: { start: startdate, end: endadate },
        success: function (data) {
			console.log(data);
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                var interviewdatetime = moment(val.interview_datetime).format('DD/MM/YYYY HH:mm');
                html += '<tr>';
                html += '<td style="text-align:center">' + i + '</td>';
                html += '<td>' + val.nik + '</td>';
                html += '<td>' + val.employee + '</td>';
                html += '<td>' + interviewdatetime + '</td>';
                html += '<td>' + val.customer + '</td>';
                html += '<td>' + val.pic + '</td>';
                html += '<td>' + val.department + '</td>';
                html += '<td>' + val.note + '</td>';
                html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.id + '\')"></a>\</td>';
                html += '</tr>';
                i++;
            });
            $('.tbody').html(html);
        }
    });
}

function LoadIndexFilterSort() {
    var startdate = $("#startDate").val();
    var endadate = $("#endDate").val();
    if (startdate != "" && endadate != "") {
        var table = $('#table').DataTable();
        table.destroy();
        $('#table').DataTable({
            "ajax": LoadIndexInterviewHistorySort()
        });
    }
    else {
        Swal.fire(
            'Oops!',
            'Please Fill Both Date Picker',
            'error'
        )
    }
}

function LoadIndexReset() {
    var table = $('#table').DataTable();
    table.destroy();
    $('#table').DataTable({
        "ajax": LoadIndexInterviewHistory()
    });
    $('#startDate').val('');
    $('#endDate').val('');
}

var Customers = []
function LoadCustomers(element) {
    if (Customers.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Customers/LoadCustomer/",
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
    $ele.append($('<option/>').val('0').text('Select Customer'));
    $.each(Customers, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.name));
    })
}
LoadCustomers($('#Customer'));

var CustomersUpdate = []
function LoadCustomersUpdate(element) {
    if (CustomersUpdate.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Customers/LoadCustomer/",
            success: function (data) {
                Customers = data;
                renderCustomerUpdate(element);
            }
        })
    }
    else {
        renderCustomerUpdate(element);
    }
}

function renderCustomerUpdate(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select Customer'));
    $.each(Customers, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.name));
    })
}
LoadCustomersUpdate($('#CustomerUpdate'));

var Employees = []
function LoadEmployee(element) {
    if (Employees.length == 0) {
        $.ajax({
            type: "GET",
            url: "/ParticipantDisplays/LoadParticipantDisplay/",
            success: function (data) {
                Employees = data;
                renderEmployee(element);
            }
        })
    }
    else {
        renderEmployee(element);
    }
}

function renderEmployee(element) {
    var $ele = $(element);
    $ele.empty();
    $.each(Employees, function (i, val) {
        if (val.id != "guest") {
            $ele.append($('<option/>').val(val.id).text(val.name));
        }
    })
}
LoadEmployee($('#Employee'));

function GetById(Id) {
    $.ajax({
        url: "/InterviewHistories/GetById/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            debugger;
            var interviewdatetime = moment(result.interview_datetime).format('YYYY-MM-DD HH:mm');
            //var getPhone = result.phone.substring(3, result.phone.length);
            $('#myLabelUpdate').text("Update Form");
            $('#IdUpdate').val(result.id);
            $('#NIKUpdate').val(result.nik);
			$('#DepartmentUpdate').val(result.department);
            $('#EmployeeUpdate').val(result.employee);
            $('#InterviewerUpdate').val(result.pic);
            $('#datetimepickerUpdate').val(interviewdatetime);
            $('#CreatedByUpdate').val(result.create_by);
            $('#CreateDatetimeUpdate').val(result.create_datetime);
            $('#UpdatedByUpdate').val(result.update_by);
            $('#UpdateDatetimeUpdate').val(result.update_datetime);
            $('#CustomerUpdate').val(result.customer);
            $('#NoteUpdate').val(result.note);

            $('#myModalUpdate').modal('show');
            $('#Update').show();
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
                url: "/InterviewHistories/Delete/",
                data: { id: Id },
                type: "DELETE"
            }).then((result) => {
                //debugger;
                if (result.StatusCode == 200) {
                    Swal.fire('Success', 'Delete Successfully', 'success');
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

function ValidateInterviewer(pic) {
    var picReg = /^[a-zA-Z .,]+$/i;
    return picReg.test(pic);
}

function Update() {
    debugger;
    if ($('#InterviewerUpdate').val() == "" || $('#InterviewerUpdate').val() == " ") {
        Swal.fire("Oops", "Please Insert Interviewer ", "error")
    } else if (!ValidateInterviewer($('#InterviewerUpdate').val())) {
        Swal.fire("Oops", "Please Insert Interviewer Properly", "error")
    } else if ($('#datetimepickerUpdate').val() == "" || $('#datetimepickerUpdate').val() == " ") {
        Swal.fire("Oops", "Please Insert Interview Date", "error")
    } else if ($('#CustomerUpdate').val() == 0) {
        Swal.fire("Oops", "Please Insert Customer", "error")
    } else if ($('#DepartmentUpdate').val() == "" || $('#DepartmentUpdate').val() == " ") {
		Swal.fire("Oops", "Please Insert Divisi - Department", "error")
	} else {
        debugger;
        var currentdate = new Date();
        var interviewHistory = new Object();
        interviewHistory.id = $('#IdUpdate').val();
        interviewHistory.interview_datetime = $('#datetimepickerUpdate').val();
        interviewHistory.pic = $('#InterviewerUpdate').val();
        interviewHistory.department = $('#DepartmentUpdate').val();
        interviewHistory.note = $('#NoteUpdate').val();
        interviewHistory.created_by = $('#CreatedByUpdate').val();
        interviewHistory.create_datetime = $('#CreateDatetimeUpdate').val();
        interviewHistory.updated_by = $('#UpdatedByUpdate').val();
        interviewHistory.update_datetime = + currentdate.getFullYear() + "-"
            + (currentdate.getMonth() + 1) + "-"
            + currentdate.getDate() + " "
            + currentdate.getHours() + ":"
            + currentdate.getMinutes() + ":"
            + currentdate.getSeconds();
        interviewHistory.customer = $('#CustomerUpdate').val();
        interviewHistory.employee = $('#NIKUpdate').val();
        //debugger;
        $.ajax({
            url: "/InterviewHistories/Update/",
            data: interviewHistory,
        }).then((result) => {
            debugger;
            if (result == 200) {
                Swal.fire('Success', 'Update Successfully', 'success');
                ResetTable();
                $('#myModalUpdate').modal('hide');
            }
            else {
                Swal.fire('Error', 'Update Fail', 'error');
                $('#myModalUpdate').modal('hide');
            }
        });
    }
};

function Save() {
    debugger;
    if ($('#Employee').val() == "" || $('#Employee').val() == " ") {
        Swal.fire("Oops", "Please Select Employee ", "error")
    } else if ($('#Interviewer').val() == "" || $('#Interviewer').val() == " ") {
        Swal.fire("Oops", "Please Insert Interviewer", "error")
    } else if (!ValidateInterviewer($('#Interviewer').val())) {
        Swal.fire("Oops", "Please Insert Interviewer Properly", "error")
    } else if ($('#datetimepicker').val() == "" || $('#datetimepicker').val() == " ") {
        Swal.fire("Oops", "Please Select Interview Date", "error")
    } else if ($('#Customer').val() == null || $('#Customer').val() == "" || $('#Customer').val() == " " || $('#Customer').val() == 0) {
        Swal.fire("Oops", "Please Select Customer", "error")
    } else if ($('#Department').val() == "" || $('#Department').val() == " ") {
		Swal.fire("Oops", "Please Insert Divisi - Department", "error")
	} else {
        var currentdate = new Date();
        var interviewHistory = new Object();
        interviewHistory.interview_datetime = $('#datetimepicker').val();
        interviewHistory.pic = $('#Interviewer').val();
		interviewHistory.department = $('#Department').val();
        interviewHistory.note = $('#Note').val();
        interviewHistory.create_by = "13144";
        interviewHistory.create_datetime = + currentdate.getFullYear() + "-"
            + (currentdate.getMonth() + 1) + "-"
            + currentdate.getDate() + " "
            + currentdate.getHours() + ":"
            + currentdate.getMinutes() + ":"
            + currentdate.getSeconds();
        interviewHistory.update_by = "13144";
        interviewHistory.update_datetime = + currentdate.getFullYear() + "-"
            + (currentdate.getMonth() + 1) + "-"
            + currentdate.getDate() + " "
            + currentdate.getHours() + ":"
            + currentdate.getMinutes() + ":"
            + currentdate.getSeconds();
        interviewHistory.customer = $('#Customer').val();
        interviewHistory.employee = $('#Employee').val();
        $.ajax({
            type: 'POST',
            url: '/InterviewHistories/Insert/',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: interviewHistory
        }).then((result) => {
            debugger;
            if (result > 0) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Success ' + result + ' row, Insert Successfully'
                });
                ResetTable();
                $('#myModal').modal('hide');
                $.ajax({
                    url: 'http://www.mpssoft.co.id/smsgateway/api/sendsms',
                    type: "post",
                    dataType: "json",
                    data: { username: 081295884582, password: "123456", message: "Hello, How are you???", number: 085747478817 },
                    success: function (result2) {
                        debugger;
                        if (!result2.error) {
                            alert('Message had been sent.');
                        } else {
                            alert('Message Failed To Sent.' + result2.error);
                        }
                    },
                    error: function (a, b, c) {
                        alert('Error Occurred.');
                    }
                });
            }
            else {
                Swal.fire('Error', 'Insert Fail', 'error');
            }
        });
    }
};

function ClearScreen() {
    document.getElementById("Employee").disabled = false;
    $("#Employee").val(null).trigger("change");
	$('#Department').val('');
    $('#Interviewer').val('');
    $('#Note').val('');
    $('#datetimepicker').val('');
    $('#Customer').val('');
    $('#Update').hide();
    $('#Save').show();
}