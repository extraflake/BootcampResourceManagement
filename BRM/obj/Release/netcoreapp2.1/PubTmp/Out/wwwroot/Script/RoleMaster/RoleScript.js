$(document).ready(function () {
     $('#table').DataTable({
        "order": [],
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
            
        }],
        "ajax": LoadIndexRole(),
        "responsive": true
    });
});
function LoadIndexRole() {
    $.ajax({
        type: "GET",
        url: "/Roles/LoadRoles/",
        async: false,
        success: function (data) {
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                    html += '<tr>';
                    html += '<td style="text-align:center">' + i + '</td>';
                    html += '<td>' + val.name + '</td>';
                    html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.id + '\')"></a>';
                    html += ' | <a href="#" class="fa fa-trash" onclick="return Delete(\'' + val.id + '\')"></a></td > ';
                    html += '</tr>';
                    i++;
            });
            $('.tbody').html(html);
        }
    });
}

function ValidateName(name) {
    var nameReg = /^[A-Z][a-z 0-9]+$/i;
    return nameReg.test(name);
}

function Save() {
    if ($('#name').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Name',
            showConfirmButton: false,
            timer: 1500
        });
    } else if (!ValidateName($('#name').val())) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Fill Corectly',
            showConfirmButton: false,
            timer: 1500
        });  
    } else {
        var role = new Object();
        role.name = $('#name').val();
        $.ajax({
            type: 'POST',
            url: '/Roles/InsertOrUpdate/',
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: role
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Insert Successfully'
                });
                window.location.href = "/Roles/";
            }
            else {
                Swal.fire('Error', 'Insert Fail', 'error');
                ClearScreen();
            }
        });
    }
}

function Update() {
    if ($('#name').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Name',
            showConfirmButton: false,
            timer: 1500
        });
    } else if (!ValidateName($('#name').val())){
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Fill Corectly',
            showConfirmButton: false,
            timer: 1500
        });  
    } else {
        var data = new Object();
        data.id = $('#id').val();
        data.name = $('#name').val();
        $.ajax({
            url: "/Roles/InsertOrUpdate/",
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
                window.location.href = "/Roles/";
            }
            else {
                Swal.fire('Error', 'Update Fail', 'error');
                ClearScreen();
            }
        });
    }
}

function GetById(Id) {
    debugger;
    $.ajax({
        type: "GET",
        url: "/Roles/GetById/",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            debugger;
            $('#id').val(result.id);
            $('#name').val(result.name);

            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function ClearScreen() {
    $('#id').val('');
    $('#name').val('');
    $('#Update').hide();
    $('#Save').show();
}

function Validate() {
    debugger;
    if ($('#name').val() == "") {
        Swal.fire("Oops", "Please Insert Name Properly", "error")
    } else if ($('#id').val() == 0) {
        Save();
    } else {
        Edit();
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
                url: "/Roles/Delete/",
                type: "DELETE",
                data: { id: Id }
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Delete Successfully'
                    });
                    window.location.href = "/Roles/";
                }
                else {
                    Swal.fire('Error', 'Update Fail', 'error');
                    ClearScreen();
                }
            });
        }
    })
}