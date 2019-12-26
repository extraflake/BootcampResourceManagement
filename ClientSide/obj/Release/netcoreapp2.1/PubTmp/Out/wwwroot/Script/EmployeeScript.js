$(document).ready(function () {
    $('#table').DataTable({
        "order": [],
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }],
        "ajax": LoadIndexEmployee(),
        "responsive": true
    });
    $("#BirthDate").datepicker();
    $("#Province").change(function () {
        debugger;
        var get = $('#Province').val();
        $.ajax({
            type: "GET",
            url: "/Districts/LoadDistrictsByParam/",
            data: { 'param': get },
            success: function (data) {
                debugger;
                renderDistrict($("#District").parents('.modal-body').find('select.district'), data);
            },
            error: function (error) {
                console.log(error);
            }
        })
    })
    //document.getElementById("Progress_Status").hidden = true;
});

function ForgotPassword() {
    debugger;
    $.ajax({
        url: "/Employees/GetByEmail/",
        type: "POST",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { email: Email },
        success: function (result) {
            debugger;
            //var birthDate = moment(result.birth_date).format('MM/DD/YYYY');
            //var getPhone = result.phone.substring(3, result.phone.length);
            document.getElementById("Email").disabled = true;
            //$('#Id').val(result.id);
            //$('#FirstName').val(result.first_name);
            //$('#LastName').val(result.last_name);
            //$('#Phone').val(getPhone);
            $('#Email').val(result.email);
            //$('#Province').val(2);
            //$('#District').val(10);

            //$('#myModal').modal('show');
            //$('#Update').show();
            $('#Send').show();
        }
    })

}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function LoadIndexEmployee() {
    $.ajax({
        type: "GET",
        url: "/Employees/LoadEmployeeDisplay/",
        async: false,
        success: function (data) {
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                //debugger;
                var id = val.id;
                var getphone = null;
                if (val.phone != "") {
                    getphone = "+62" + val.phone;
                }
                else {
                    getphone = "-";
                }
                if (id == val.id) {
                    html += '<tr>';
                    html += '<td style="text-align:center">' + i + '</td>';
                    html += '<td>' + val.first_name + ' ' + val.last_name + '</td>';
                    html += '<td>' + val.email + '</td>';
                    html += '<td style="text-align:center">' + val.district + '</td>';
                    html += '<td>' + getphone + '</td>';
                    html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.email + '\')"></a>';
                    html += ' | <a href="#" class="fa fa-user" onclick="return GetByEmail(\'' + val.email + '\')"></a></td > ';
                    html += '</tr>';
                    i++;
                }
                else {
                    html += '<tr>';
                    html += '<td style="text-align:center">' + i + '</td>';
                    html += '<td>' + val.first_name + ' ' + val.last_name + '</td>';
                    html += '<td>' + val.email + '</td>';
                    html += '<td style="text-align:center">' + val.hiring_location + '</td>';
                    html += '<td>' + getphone + '</td>';
                    html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.email + '\')"></td>';
                    html += '</tr>';
                    i++;
                }
            });
            $('.tbody').html(html);
        }
    });
}

function Upload() {
    debugger;
    var fileExtension = ['xls', 'xlsx'];
    var filename = $('#fUpload').val();
    if (filename.length == 0) {
        alert("Please select a file.");
        return false;
    }
    else {
        var extension = filename.replace(/^.*\./, '');
        if ($.inArray(extension, fileExtension) == -1) {
            alert("Please select only excel files.");
            return false;
        }
    }
    var fdata = new FormData();
    var fileUpload = $("#fUpload").get(0);
    var files = fileUpload.files;
    fdata.append(files[0].name, files[0]);
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
    $.ajax({
        type: "POST",
        url: "/Employees/Upload/",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: fdata,
        contentType: false,
        processData: false,
    }).then((res) => {
        debugger;
        if (res > 0) {
            Swal.fire('Success', res + 'row, Insert Successfully', 'success');
            ResetTable();
            $('#myModal').modal('hide');
            ClearScreen();
        }
        else {
            Swal.fire('Error', 'Insert Fail', 'error');
            ResetTable();
            $('#myModal').modal('hide');
            ClearScreen();
        }
    });
}

function uploadFiles() {
    debugger;
    var input = document.getElementById(fUpload);
    var files = input.files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
    }
    
    $.ajax({
        url: "Employees/Upload/",
        type: 'POST',
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (result) {
        },
        error: function (jqXHR) {
        },
        complete: function (jqXHR, status) {
        }
    });
}

function GetById(Email) {
    $.ajax({
        url: "/Employees/GetByIdDisplay/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { email: Email },
        success: function (result) {
            debugger;
            document.getElementById("emailclass").hidden = true;
            var get = result.district;
            $('#myModalLabel').text("Update Form");
            $('#Id').val(result.id);
            $('#FirstName').val(result.first_name);
            $('#LastName').val(result.last_name);
            $('#Phone').val(result.phone);
            $('#Email').val(result.email);
            $("#Province option:contains(" + result.province + ")").attr('selected', 'selected');
            $('#hiddenfield').val(result.district);
            $('#Province').trigger('change');

            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function GetByEmail(Email) {
    debugger;
    $.ajax({
        url: "/Employees/GetById/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { email: Email },
        success: function (result) {
            debugger;
            document.getElementById("Email").disabled = true;
            $('#myModalLabelUpdate').text("Update Form");
            $('#IdUpdate').val(result.id);
            $('#EmailUpdate').val(result.email);
            $('#FirstNameUpdate').val(result.first_name);
            $('#LastNameUpdate').val(result.last_name);
            $('#Hiring_LocationUpdate').val(result.hiring_location);
            $('#PhoneUpdate').val(result.phone);

            $('#myModalUpdateId').modal('show');
            $('#Update').show();
        }
    })
}

function Delete(Id) {
    debugger;
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
            debugger;
            $.ajax({
                url: "/Employees/Delete/",
                type: "DELETE",
                data:
                {
                    id: Id
                },
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire('Success', 'Delete Successfully', 'success');
                    window.location.href = "/Participants/";
                }
                else {
                    Swal.fire('Error', 'Delete Fail', 'error');
                    ClearScreen();
                }
            });
        }
    })
}

function Edit() {
    debugger;
    var employeeVM = new Object();
    employeeVM.Id = $('#Id').val();
    employeeVM.FirstName = $('#FirstName').val();
    employeeVM.LastName = $('#LastName').val();
    employeeVM.Email = $('#Email').val();
    employeeVM.Hiring_Location = $('#District').val();
    employeeVM.Phone = $('#Phone').val();
    if ($('#Phone').val().substring(0, 1) == "0") {
        employeeVM.Phone = $('#Phone').val().substring(1, $('#Phone').val().length);
    }
    $.ajax({
        url: "/Employees/Update/" + employeeVM.Id,
        data: employeeVM,
    }).then((result) => {
        debugger;
        if (result.StatusCode == 200) {
            Swal.fire('Success', 'Update Successfully', 'success');
            ResetTable();
            $('#myModal').modal('hide');
            ClearScreen();
            setTimeout("location.reload(true);", 1500);
        }
        else {
            Swal.fire('Error', 'Update Fail', 'error');
            ClearScreen();
        }
    });
}

function EditId() {
    debugger;
    if ($('#IdUpdate').val().length > 5) {
        Swal.fire("Oops", "Please Length Id max 5 digit", "error")
    }
    else {
        var employeeVM = new Object();
        employeeVM.Id = $('#IdUpdate').val();
        employeeVM.Email = $('#EmailUpdate').val();
        employeeVM.FirstName = $('#FirstNameUpdate').val();
        employeeVM.LastName = $('#LastNameUpdate').val();
        employeeVM.Email = $('#EmailUpdate').val();
        employeeVM.Hiring_Location = $('#Hiring_LocationUpdate').val();
        employeeVM.Phone = $('#PhoneUpdate').val();
        $.ajax({
            url: "/Employees/UpdateNIK/",
            data: employeeVM,
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire('Success', 'Update Successfully', 'success');
                ResetTable();
                $('#myModal').modal('hide');
                ClearScreen();
                setTimeout("location.reload(true);", 1500);
            }
            else {
                Swal.fire('Error', 'Update Fail', 'error');
                ClearScreen();
            }
        });
    }
}

function Save() {
    debugger;
    var employeeParam = new Object({
        'FirstName': $('#FirstName').val(),
        'LastName': $('#LastName').val(),
        'Email': $('#Email').val(),
        'Hiring_Location': $('#District').val(),
        'Phone': $('#Phone').val()
    });
    if ($('#Phone').val().substring(0, 1) == "0") {
        employeeParam.Phone = $('#Phone').val().substring(1, $('#Phone').val().length);
    }
    $.ajax({
        type: "POST",
        url: "/Employees/Insert/",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { employeeVM: employeeParam }
    }).then((result) => {
        debugger;
        if (result >= 1) {
            Swal.fire('Success', result + ' row affected', 'success');
            ResetTable();
            $('#myModal').modal('hide');
            ClearScreen();
            setTimeout("location.reload(true);", 1500);
        }
        else {
            Swal.fire('Error', result + ' row affected', 'error');
            ClearScreen();
        }
    });
}

function ResetTable() {
    $('#table').DataTable().destroy();
    $('#table').DataTable({
        "ajax": LoadIndexEmployee()
    })
}

function ClearScreen() {
    document.getElementById("Email").disabled = false;
    document.getElementById("emailclass").hidden = false;
    $('#myModalLabel').text("Employee Form");
    $('#FirstName').val('');
    $('#LastName').val('');
    $('#Email').val('');
    $('#Province').val(0);
    $('#District').val(0);
    $('#Phone').val('');
    $('#BirthDate').val('');
    $('#Id').val('');
    $('#Update').hide();
    $('#Save').show();
}

function ValidateEmail(email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test(email);
}

function Validate() {
    debugger;
    if ($('#FirstName').val() == "" || $('#FirstName').val() == " " ) {
        Swal.fire("Oops", "Please Insert First Name Properly", "error")
    } else if ($('#LastName').val() == "" || $('#LastName').val() == " " ) {
        Swal.fire("Oops", "Please Insert Last Name Properly", "error")
    } else if ($('#Email').val() == "" || $('#Email').val() == " ") {
        Swal.fire("Oops", "Please Insert Email", "error")
    } else if (!ValidateEmail($('#Email').val())) {
        Swal.fire("Oops", "Please Insert Email Properly", "error")
    } else if ($('#Province').val() == 0 || $('#District').val() == 0) {
        Swal.fire("Oops", "Please Insert Hiring Location", "error")
    } else if (!ValidationPhone($('#Phone').val())) {
        Swal.fire("Oops", "Please Insert Phone Properly", "error")
    } else if ($('#Phone').val() == "" || $('#Phone').val() == " ") {
        Swal.fire("Oops", "Please Insert Phone", "error")
    } else if ($('#Phone').val().length < 9 || $('#Phone').val().length > 16) {
        Swal.fire("Oops", "Please Length Phone min 9 digit and max 16 digit", "error")
    } else if ($('#Id').val() == "") {
        Save();
    } else {
        Edit();
    }
}

function ValidateUpdate() {
    if ($('#EmailUpdate').val() == "" || $('#EmailUpdate').val() == " ") {
        Swal.fire("Oops", "Please Insert Email", "error")
    } else {
        EditId();
    }
}

function ValidationPhone(evt) {
    debugger;
    //var regex = "^\d+(\.\d{2})?$";
    var regex = /\d+/g;
    return regex.test(evt);
}

var Provinces = []
function LoadProvince(element) {
    if (Provinces.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Provinces/LoadProvince/",
            success: function (data) {

                Provinces = data;
                renderProvince(element);
            }
        })
    }
    else {
        renderProvince(element);
    }
}

function renderProvince(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select Province'));
    $.each(Provinces, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.name));
    })
}
LoadProvince($('#Province'));

function renderDistrict(element, data) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select District'));
    $.each(data, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.name));
    })
    debugger;
    var gethidden = $('#hiddenfield').val();
    $("#District option:contains(" + gethidden + ")").attr('selected', 'selected');
}