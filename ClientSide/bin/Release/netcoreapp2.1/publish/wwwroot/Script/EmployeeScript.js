$(document).ready(function () {
    $('#table').DataTable({
        "columnDefs":
            [{
                "targets": [0],
                "visible": true,
                "searchable": true
            }],
        "ajax": LoadIndexEmployee(),
        "responsive": true
    });  
    $("#BirthDate").datepicker();
});

function LoadIndexEmployee() {
    $.ajax({
        type: "GET",
        url: "/Employees/LoadEmployee/",
        async: false,
        success: function (data) {
            debugger;
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                html += '<tr>';
                html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.email + '\')"></a>';
                html += ' | <a href="#" class="fa fa-trash" onclick="return Delete(\'' + val.email + '\')"></a>';
                html += ' | <a href="#" class="fa fa-user" onclick="return GetByEmail(\'' + val.email + '\')"></a></td > ';
                html += '<td style="text-align:center">' + i + '</td>';
                html += '<td>' + val.first_name + ' ' + val.last_name + '</td>';
                html += '<td>' + val.email + '</td>';
                html += '<td style="text-align:center">' + val.hiring_location + '</td>';
                html += '<td>' + val.phone + '</td>';
                html += '</tr>';
                i++;
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
        success: function (response) {
            if (response.length == 0)
                Swal.fire('Success',
                    'Your data has been added',
                    'success');
            else {
                $('#dvData').html(response);
            }
            LoadIndexEmployee();
        },
        error: function (e) {
            $('#dvData').html(e.responseText);
        }
    });
}

function GetById(Email) {
    debugger;
    $.ajax({
        url: "/Employees/GetById/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { email: Email },
        success: function (result) {
            debugger;
            //var birthDate = moment(result.birth_date).format('MM/DD/YYYY');
            var getPhone = result.phone.substring(3, result.phone.length);
            $('#Id').val(result.id);
            $('#FirstName').val(result.first_name);
            $('#LastName').val(result.last_name);
            $('#Phone').val(getPhone);
            $('#Email').val(result.email);
            $('#Hiring_Location').val(result.hiring_location);

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
            var getPhone = result.phone.substring(3, result.phone.length);
            $('#IdUpdate').val(result.id);
            $('#EmailUpdate').val(result.email);
            $('#FirstNameUpdate').val(result.first_name);
            $('#LastNameUpdate').val(result.last_name);
            $('#Hiring_LocationUpdate').val(result.hiring_location);
            $('#PhoneUpdate').val(getPhone);

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
                success: function (response) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    $('#table').refresh();
                },
                error: function (response) {
                    Swal.fire("Oops", "We couldn't connect to the server!", "error")
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
    employeeVM.Phone = "+62"+$('#Phone').val();
    $.ajax({
        url: "/Employees/InsertOrUpdate/",
        data: employeeVM,
        success: function (result) {
            Swal.fire(
                'Success',
                'Your file has been updated.',
                'success'
            )
            $('#myModal').modal('hide');
            $('#table').refresh();
        },
        error: function (result) {
            Swal.fire(
                'Failed',
                'Your file has not been updated.',
                'error'
            )
        }
    });
}

function EditId() {
    debugger;
    var employeeVM = new Object();
    employeeVM.Id = $('#IdUpdate').val();
    employeeVM.Email = $('#EmailUpdate').val();
    employeeVM.FirstName = $('#FirstNameUpdate').val();
    employeeVM.LastName = $('#LastNameUpdate').val();
    employeeVM.Email = $('#EmailUpdate').val();
    employeeVM.Hiring_Location = $('#Hiring_LocationUpdate').val();
    employeeVM.Phone = $('#PhoneUpdate').val();
    $.ajax({
        url: "/Employees/InsertOrUpdate/",
        data: employeeVM,
        success: function (result) {
            Swal.fire(
                'Success',
                'Your file has been updated.',
                'success'
            )
            $('#myModalUpdateId').modal('hide');
            $('#table').refresh();
        },
        error: function (result) {
            Swal.fire(
                'Failed',
                'Your file has not been updated.',
                'error'
            )
        }
    });
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
    var validatePhone = $('#Phone').val();
    var region = "+62";
    if (validatePhone.substring(0, 1) == "0") {
        employeeParam.Phone = validatePhone.replace(validatePhone.substring(0, 1), "+62");
        window.alert(employeeParam.Phone);
    } else if (validatePhone.substring(0, 1) == "1" ||
        validatePhone.substring(0, 1) == "2" ||
        validatePhone.substring(0, 1) == "3" ||
        validatePhone.substring(0, 1) == "4" ||
        validatePhone.substring(0, 1) == "5" ||
        validatePhone.substring(0, 1) == "6" ||
        validatePhone.substring(0, 1) == "7" ||
        validatePhone.substring(0, 1) == "8" ||
        validatePhone.substring(0, 1) == "9") {
        employeeParam.Phone = region.concat(validatePhone);
        window.alert(employeeParam.Phone);
    }
    $.ajax({
        type: "POST",
        url: "/Employees/InsertOrUpdate/",
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { employeeVM: employeeParam },
        success: function (result) {
            debugger;
            Swal.fire(
                'Success',
                'Your data has been added',
                'success'
            )
            $('#myModal').modal('hide');
            $('#table').refresh();
            ClearScreen();
        }
    });
}

function ClearScreen() {
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
    if ($('#FirstName').val() == "" || $('#FirstName').val() == " ") {
        Swal.fire("Oops", "Please Insert First Name", "error")
    } else if ($('#LastName').val() == "" || $('#LastName').val() == " ") {
        Swal.fire("Oops", "Please Insert Last Name", "error")
    } else if ($('#Email').val() == "" || $('#Email').val() == " ") {
        Swal.fire("Oops", "Please Insert Email", "error")
    } else if (!ValidateEmail($('#Email').val())) {
        Swal.fire("Oops", "Please Insert Email Properly", "error")
    } else if ($('#Hiring_Location').val() == "" || $('#Hiring_Location').val() == " ") {
        Swal.fire("Oops", "Please Insert Hiring Location", "error")
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
    var char = String.fromCharCode(evt.which);
    if (!(/[0-9]/.test(char))) {
        evt.preventDefault();
    }
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

function LoadDistrict(DistrictIndex) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/Districts/LoadDistrictsByParam/",
        data: { 'param': $(DistrictIndex).val() },
        success: function (data) {
            console.log(data);
            renderDistrict($(DistrictIndex).parents('.modal-body').find('select.district'), data);
        },
        error: function (error) {
            console.log(error);
        }
    })
}

function renderDistrict(element, data) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select District'));
    $.each(data, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.name));
    })
}