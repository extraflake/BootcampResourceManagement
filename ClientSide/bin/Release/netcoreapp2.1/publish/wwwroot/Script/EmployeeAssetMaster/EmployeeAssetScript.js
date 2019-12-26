$(document).ready(function () {
    LoadIndexEmployeeAsset();
    $('#table').DataTable({
        "ajax": LoadIndexEmployeeAsset()
    })
})

function Save() {
    var asset = new Object();
    asset.id = $('#Id').val();
    asset.number = $('#Number').val();
    asset.type = $('#Type').val();
    asset.employee = $('#Employee').val();
    $.ajax({
        type: 'POST',
        url: '/Assets/Insert/',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: asset,
        success: function (result) {
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
};

function LoadIndexEmployeeAsset() {
    $.ajax({
        type: "GET",
        async: false,
        url: "/EmployeeAssets/LoadEmployeeAsset/",
        success: function (data) {
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                debugger;
                receive_date = moment(val.receive_date).format('MM/DD/YYYY');
                return_date = moment(val.return_date).format('MM/DD/YYYY');
                html += '<tr>';
                html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.id + '\')"></a>';
                html += ' | <a href="#" class="fa fa-trash" onclick="return Delete(\'' + val.id + '\')"></a></td>';
                html += '<td style="text-align:center">' + i + '</td>';
                html += '<td>' + val.asset + '</td>';
                html += '<td>' + val.employee + '</td>';
                html += '<td>' + receive_date + '</td>';
                html += '<td>' + return_date + '</td>';
                html += '</tr>';
                i++;
            });
            $('.tbody').html(html);
        }
    });
}

function Edit() {
    var asset = new Object();
    asset.id = $('#Id').val();
    asset.number = $('#Number').val();
    asset.type = $('#Type').val();
    asset.employee = $('#Employee').val();
    $.ajax({
        url: "/Assets/Update/",
        data: asset,
        success: function (result) {
            Swal.fire(
                'Success',
                'Your file has been updated.',
                'success'
            )
            $('#myModal').modal('hide');
            $('#table').refresh();
            ClearScreen();
        }
    });
};

function GetById(Id) {
    $.ajax({
        url: "/Assets/GetById/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            debugger;
            $('#Id').val(result.id);
            $('#Number').val(result.number);
            $('#Type').val(result.type);
            $('#Employee').val(result.employee);

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
                url: "/Assets/Delete/",
                data: { id: Id },
                type: "DELETE",
                success: function (response) {
                    Swal.fire(
                        'Deleted!',
                        'Your file has been deleted.',
                        'success'
                    )
                    LoadIndexBatch();
                },
                error: function (response) {
                    Swal.fire("Oops", "We couldn't connect to the server!", "error");
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
    $ele.append($('<option/>').val('0').text('Select Employee'));
    $.each(Employees, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.first_name + val.last_name));
    })
}
LoadEmployee($('#Employee'));

var Types = []
function LoadType(element) {
    if (Types.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Types/LoadType/",
            success: function (data) {
                Types = data;
                renderType(element);
            }
        })
    }
    else {
        renderType(element);
    }
}

function renderType(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select Type'));
    $.each(Types, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.name));
    })
}
LoadType($('#Type'));