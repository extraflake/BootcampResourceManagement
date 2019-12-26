$(document).ready(function () {
    $('#table').DataTable({
        "order": [],
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }],
        "ajax": LoadIndexEmployeeRole(),
        "responsive": true
    });

    $(".select2").select2({
        placeholder: "Select Employee"
    });
    $(".select3").select3({
        placeholder: "Select Role"
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

function LoadIndexEmployeeRole(){
    $.ajax({
        type: "GET",
        url: "/EmployeeRoles/LoadEmployeeRoles/",
        async: false,
        success: function (data) {
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                html += '<tr>';
                html += '<td style="text-align:center">' + i + '</td>';
                html += '<td>' + val.nik + '</td>';
                html += '<td>' + val.name + '</td>';
                html += '<td>' + val.role_name + '</td>';
                html += '<td style="text-align:center"><a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.id + '\')"></a>';
                html += ' | <a href="#" class="fa fa-trash" onclick="return Delete(\'' + val.id + '\')"></a></td>';
                html += '</tr>';
                i++;
            });
            $('.tbody').html(html);
        }
    });
}

function GetById(Id) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/EmployeeRoles/GetById/",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            debugger;
            $('#Id').val(result.id);
            $('#Nik').val(result.nik);
            $('#Employees').val(result.name);
            $('#RoleUpdate').val(result.role);

            $('#myModalUpdate').modal('show');
            $('#btn_Update').show();
            $('#btn_Save').hide();
        }
    })
}
function Save() {
    if ($('#Employee').val() == null) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Employee',
            showConfirmButton: false,
            timer: 1500
        });
    } else if ($('#Role').val() == ""){
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Role',
            showConfirmButton: false,
            timer: 1500
        });
    }
    else {
        var datas = new Object();
        datas.nik = $('#Employee').val();
        datas.role = $('#Role').val();
        $.ajax({
            type: "POST",
            url: "/EmployeeRoles/Insert/",
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: datas
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Insert Successfully',
                    showConfirmButton: false,
                    timer: 1500
                });
                window.location.href = "/EmployeeRoles/";
            }
            else {
                Swal.fire('Error', 'Insert Fail', 'error');
                ClearScreen();
            }
        });
    }
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
                url: "/EmployeeRoles/Delete/",
                type: "DELETE",
                data: { id: Id }
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Delete Successfully',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    window.location.href = "/EmployeeRoles/";
                }
                else {
                    Swal.fire('Error', 'Delete Fail', 'error');
                    ClearScreen();
                }
            });
        }
        $('#table').refresh();
    })
}
function Update() {
    if ($('#RoleUpdate').val() == "") {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Role',
            showConfirmButton: false,
            timer: 1500
        });
    } else {
        debugger;
        var data = new Object();
        data.id = $('#Id').val();
        data.nik = $('#Nik').val();
        data.role = $('#RoleUpdate').val();
        $.ajax({
            url: "/EmployeeRoles/InsertOrUpdate/",
            data: data
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Update Successfully',
                    showConfirmButton: false,
                    timer: 1500
                });
                window.location.href = "/EmployeeRoles/";
            }
            else {
                Swal.fire('Error', 'Update Fail', 'error');
                ClearScreen();
            }
        });
    }
}

function ClearScreen() {
    $('#id').val('');
    $('#Employee').val('');
    $('#Role').val('');
    $('#Update').hide();
    $('#Save').show();
}
function Validate() {
    debugger;
    if ($('#Employee').val() == "") {
        Swal.fire("Oops", "Please Insert Name Properly", "error")
    } else if ($('#id').val() == 0) {
        Save();
    } else {
        Update();
    }
}

var Employees = []
function LoadEmployee(element) {
    if (Employees.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Employees/LoadEmployee/",
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
        $ele.append($('<option/>').val(val.id).text(val.first_name+" "+val.last_name));
    })
}
LoadEmployee($('#Employee'));
var Roles = []
function LoadRoles(element) {
    if (Roles.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Roles/LoadRoles/",
            success: function (data) {
                Roles = data;
                renderRoles(element);
            }
        })
    }
    else {
        renderRoles(element);
    }
}
function renderRoles(element) {
    var $ele = $(element);
    $ele.empty();
    $.each(Roles, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.name));
    })
}
LoadRoles($('#Role'));
LoadRoles($('#RoleUpdate'));