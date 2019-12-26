$(document).ready(function () {
    LoadIndexAsset();
    $('#table').DataTable({
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }],
        "ajax": LoadIndexAsset(),
        "orderMulti": false
    })
})

function ResetTable() {
    $('#table').DataTable().destroy();
    $('#table').DataTable({
        "ajax": LoadIndexAsset()
    })
}

function ValidateName(name) {
    var nameReg = /^[0-9]+$/i;
    return nameReg.test(name);
}

function Save() {
    debugger;
    if($('#Type').val() == 0) {
        Swal.fire('Error', 'Please Choose Type', 'error')
    } else if ($('#Id').val() == "" || $('#Id').val() == " ") {
        Swal.fire('Error', 'Please Insert Id Properly', 'error')
    } else if ($('#Number').val() == "" || $('#Number').val() == " ") {
        Swal.fire('Error', 'Please Insert Number', 'error')
    } else if (!ValidateName($('#Number').val())) {
        Swal.fire('Error', 'Please Insert Number Properly', 'error')
    } else {
        var asset = new Object();
        if ($('#Type').val() == 'ID') {
            asset.id = "IDXX";
        }
        if ($('#Type').val() == 'LC' && $('#Number').val().length > 2) {
            Swal.fire('Error', 'Locker Id only have 2 number maximum');
        } else {
            asset.id = $('#Id').val();
            asset.number = $('#Number').val();
            asset.type = $('#Type').val();
            $.ajax({
                type: 'POST',
                url: '/Assets/Insert/',
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                data: asset
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire('Success', 'Insert Successfully', 'success');
                    $('#myModal').modal('hide');
                    ResetTable();
                    ClearScreen();
                }
                else {
                    Swal.fire('Error', 'Insert Fail', 'error');
                    ClearScreen();
                }
            });
        }
    }
};

function LoadIndexAsset() {
    var nik = '';
    var employee = '';
    $.ajax({
        type: "GET",
        async: false,
        url: "/Assets/LoadAssetDisplay",
        success: function (data) {
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                if (val.nik != null) {
                    nik = val.nik;
                }
                if (val.employee != null) {
                    employee = val.employee;
                } else {
                    nik = '-';
                    employee = '-';
                }
                html += '<tr>';
                html += '<td style="text-align:center">' + i + '</td>';
                html += '<td>' + nik + '</td>';
                html += '<td>' + employee + '</td>';
                html += '<td>' + val.type + '</td>';
                html += '<td>' + val.id + '</td>';
                html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.id + '\')"></a>';
                html += ' | <a href="#" class="fa fa-refresh" onclick="return GetBack(\'' + val.id + '\')"></a></td>';
                html += '</tr>';
                i++;
            });
            $('.tbody').html(html);
        }
    });
}

function Edit() {
    if ($('#Id').val() == 'IDXX') {
        debugger;
        var historyasset = new Object();
        historyasset.id = $('#Id').val() + "-" + $('#Employee').val();
        historyasset.asset = $('#Id').val();
        historyasset.employee = $('#Employee').val();
        $.ajax({
            url: "/EmployeeAssets/Insert/",
            type: "POST",
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: historyasset
        }).then((result) => {
            debugger;
            if (result.StatusCode == 200) {
                Swal.fire('Success', 'Update Successfully', 'success');
                $('#myModal').modal('hide');
                ResetTable();
                ClearScreen();
            }
            else {
                Swal.fire('Update Fail', 'You Dont Have Valid ID', 'error');
                $('#myModal').modal('hide');
                ClearScreen();
            }
        });
    }
    else {
        debugger;
        if ($('#Employee').val() == 0) {
            Swal.fire('Error', 'Please Choose Employee', 'error');
        } else if ($('#Type').val() == 0) {
            Swal.fire('Error', 'Please Choose Type', 'error')
        } else if ($('#Id').val() == "" || $('#Id').val() == " ") {
            Swal.fire('Error', 'Please Insert Id', 'error')
        } else if ($('#Number').val() == "" || $('#Number').val() == " ") {
            Swal.fire('Error', 'Please Insert Number', 'error')
        } else {
            var asset = new Object();
            asset.id = $('#Id').val();
            asset.number = $('#Number').val();
            asset.type = $('#Type').val();
            asset.employee = $('#Employee').val();
            $.ajax({
                url: "/Assets/Update/",
                data: asset
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire('Success', 'Update Successfully', 'success');
                    $('#myModal').modal('hide');
                    ResetTable();
                    ClearScreen();
                }
                else {
                    Swal.fire('Error', 'Update Fail', 'error');
                    $('#myModal').modal('hide');
                    ClearScreen();
                }
            });
        }
    }
};

function GetById(Id) {
    $.ajax({
        url: "/Assets/GetById/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            $("#myModalLabel").text("Update Form");
            document.getElementById("numberdiv").hidden = true;
            document.getElementById("Id").disabled = true;
            document.getElementById("typediv").hidden = true;
            document.getElementById("employeediv").hidden = false;
            $('#Id').val(result.id);
            $('#Number').val(result.number);
            $("#Employee").val(result.employee);
            $("#Type").val(result.type);

            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function GetBack(Id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, bring it back!'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Assets/Delete/",
                data: { id: Id },
                type: "DELETE"
            }).then((result) => {
                if (result == 200) {
                    Swal.fire('Success!', 'Your access has been taken.', 'success');
                }
                else {
                    Swal.fire("Oops", "We couldn't connect to the server!", "error");
                }
            });
        }
    });
}

function ClearScreen() {
    debugger;
    document.getElementById("numberdiv").hidden = false;
    document.getElementById("Id").disabled = false;
    document.getElementById("typediv").hidden = false;
    document.getElementById("employeediv").hidden = true;
    //document.getElementById("Update").hidden = true;
    $('#Number').val('');
    $('#Type').val(0);
    $('#Employee').val(0);
    $('#Id').val('');
    $('#Update').hide();
    $('#Save').show();
}

function GetType() {
    if ($('#Type').val() == 0) {
        $('#Id').val('');
    }
    else if ($('#Type').val() == 'ID') {
        $('#Id').val("IDXX"); 
        $('#Number').val("0"); 
        document.getElementById("numberdiv").hidden = true;
    }
    else if ($('#Type').val() == 'LC') {
        $('#Id').val("LC"); 
    }
    else {
        document.getElementById("Number").disabled = false;
        document.getElementById("numberdiv").hidden = false;
        $.ajax({
            url: "/Assets/GetCount/",
            type: "GET"
        }).then((result) => {
            debugger;
            if (result != null && result.count < 10) {
                $('#Id').val($('#Type').val() + "0" + result.count);
            }
            else if (result != null) {
                $('#Id').val($('#Type').val() + result.count);
            }
            else {
                $('#Id').val($('#Type').val() + "0");
            }
        });
        $('#Number').val();
    }
}

function Reset() {
    document.getElementById("typediv").hidden = false;
    document.getElementById("numberdiv").hidden = false;
    $('#Type').val(0);
    $('#Id').val('');
    $('#Number').val('');
}

function IsExist(Id) {
    $.ajax({
        url: "/Assets/GetById/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            debugger;
        }
    })
}

function Validate() {
    debugger;
    if (IsExist($('#Id').val()) != null) {
        Edit();
    }
    else {
        Save();
    }
}

function GetIdAsset() {
    if ($('#Type').val() == "LC") {
        if ($('#Number').val() < 10) {
            $('#Id').val($('#Type').val() + "0" + $('#Number').val());
        }
        else {
            $('#Id').val($('#Type').val() + $('#Number').val());
        }
        
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
    $ele.append($('<option/>').val('0').text('Select Employee'));
    $.each(Employees, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.first_name + " " + val.last_name));
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
        if (val.id != 'ID') {
            $ele.append($('<option/>').val(val.id).text(val.name));
        }
    })
}
LoadType($('#Type'));