$(document).ready(function () {
    $('#table').DataTable({
        "order": [],
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }],
        "ajax": LoadIndexBatchClass(),
        "responsive": true
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
    $("#Batch").change(function () {
        debugger;
        var getBatch = $('#Batch').val();
        LoadTrainer($('#Trainer'), getBatch)
    });
});

function ResetTable() {
    $('#table').DataTable().destroy();
    $('#table').DataTable({
        "ajax": LoadIndexBatchClass()
    })
}

function ResetTableSecondLayer(Id, Class) {
    $('#table').DataTable().destroy();
    $('#table').DataTable({
        "ajax": LoadIndexParticipant(Id, Class)
    })
}

function LoadIndexBatchClass() {
    var today = new Date();
    var todayDate = moment(today.getDate(), 'MM/DD/YYYY');
    $.ajax({
        type: "GET",
        url: "/BatchClasses/LoadBatchClasses/",
        async: false,
        success: function (data) {
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                var getCurrentDate = moment(val.start_date, 'MM/DD/YYYY');
                    html += '<tr>';
                    html += '<td style="text-align:center">' + i + '</td>';
                    html += '<td>' + val.batch + '</td>';
                    html += '<td>' + val.class + '</td>';
                    html += '<td>' + val.trainer + '</td>';
                    html += '<td>' + val.room + '</td>';
                if (todayDate > getCurrentDate) {
                    html += '<td style="text-align:center"> <a href="#" class="fa fa-user" onclick="return Detail(\'' + val.id + '\', \'' + val.batch + '\', \'' + val.class + '\')"></a></td> ';
                }
                else {
                    html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.id + '\')"></a>';
                    html += ' | <a href="#" class="fa fa-user" onclick="return Detail(\'' + val.id + '\', \'' + val.batch + '\', \'' + val.class + '\')"></a></td>';
                }
                html += '</tr>';
                i++;
            });
            $('.tbody').html(html);
        }
    });
}

var Batches = []
function LoadBatch(element) {
    if (Batches.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Batches/LoadBatch/",
            success: function (data) {
                Batches = data;
                renderBatch(element);
            }
        })
    }
    else {
        renderBatch(element);
    }
}

function renderBatch(element) {
    today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //As January is 0.
    var yyyy = today.getFullYear();
    var getDate = yyyy + "-" + mm + "-" + dd + "T00:00:00";
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select Batch'));
    $.each(Batches, function (i, val) {
        $ele.append($('<option/>').val(val.Id).text(val.Id));
    })
}
LoadBatch($('#Batch'));

var Classes = []
function LoadClass(element) {
    if (Batches.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Classes/LoadClass/",
            success: function (data) {
                Classes = data;
                renderClass(element);
            }
        })
    }
    else {
        renderClass(element);
    }
}

function renderClass(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select Class'));
    $.each(Classes, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.name));
    })
}
LoadClass($('#Class'));

var Rooms = []
function LoadRoom(element) {
    if (Rooms.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Rooms/LoadRoom/",
            success: function (data) {
                Rooms = data;
                renderRoom(element);
            }
        })
    }
    else {
        renderRoom(element);
    }
}

function renderRoom(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select Room'));
    $.each(Rooms, function (i, val) {
        $ele.append($('<option/>').val(val.Id).text(val.Name));
    })
}
LoadRoom($('#Room'));

var Employees = []
function LoadEmployee(element) {
    if (Employees.length == 0) {
        $.ajax({
            type: "GET",
            url: "/Employees/LoadEmployeeParticipant/",
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
        $ele.append($('<option/>').val(val.id).text(val.name));
    })
}
LoadEmployee($('#Participant'));

var Trainers = []
function LoadTrainer(element, getBatch) {
    debugger;
    //if (Trainers.length == 0) {
    //    $.ajax({
    //        url: "/Employees/LoadTrainer/",
    //        type: "GET",
    //        dataType: 'json',
    //        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
    //        data: { id: getBatch },
    //        success: function (result) {
    //            debugger;
    //            Trainers = result;
    //            renderTrainer(element);
    //        }
    //    })
    //}
    //else {
    //    renderTrainer(element);
    //}
    $.ajax({
        url: "/Employees/LoadTrainer/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: getBatch },
        success: function (result) {
            debugger;
            Trainers = result;
            renderTrainer(element);
        }
    })
}

function renderTrainer(element) {
    var $ele = $(element);
    $ele.empty();
    $ele.append($('<option/>').val('0').text('Select Trainer'));
    $.each(Trainers, function (i, val) {
        if (val.is_trainer = true) {
            $ele.append($('<option/>').val(val.id).text(val.first_name + " " + val.last_name));
        }
    })
}
LoadTrainer($('#Trainer'))

function Save() {
    var batchclass = new Object();
    batchclass.participant = $('#Participant').val();
    batchclass.batch = $('#Batch').val();
    batchclass.room = $('#Room').val();
    batchclass.trainer = $('#Trainer').val();
    batchclass.class = $('#Class').val();
    batchclass.quantity = 10;
    batchclass.id = $('#Batch').val() + "-" + $('#Class :selected').text(); 
    $.ajax({
        type: 'POST',
        url: '/BatchClasses/Insert/',   
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { batchClassVM: batchclass }
    }).then((result) => {
        debugger;
        if (result.StatusCode == 200) {
            Swal.fire('Success', 'Insert Successfully', 'success');
            ResetTable();
            $('#myModal').modal('hide');
            ClearScreen();
        }
        else {
            Swal.fire('Error', 'Already Exist', 'error');
            ClearScreen();
        }
    });
};

function GetById(Id) {
    debugger;
    $.ajax({
        url: "/BatchClasses/GetById/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            debugger;
            $("#myModalLabel").text("Update Form");
            document.getElementById("Batch").disabled = true;
            document.getElementById("Class").disabled = true;
            $('#Id').val(result.id);
            $('#Batch').val(result.batch);
            $("#Trainer").val(result.trainer);
            $("#Room").val(result.room);
            $("#Class").val(result.class);

            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function Edit() {
    debugger;
    var batchClassVM = new Object();
    batchClassVM.batch = $('#Batch').val();
    batchClassVM.room = $('#Room').val();
    batchClassVM.trainer = $('#Trainer').val();
    batchClassVM.class = $('#Class').val();
    batchClassVM.id = $('#Id').val();
    $.ajax({
        url: "/BatchClasses/Update/",
        data: batchClassVM
    }).then((result) => {
        debugger;
        if (result.StatusCode == 200) {
            Swal.fire('Success', 'Update Successfully', 'success');
            ResetTable();
            $('#myModal').modal('hide');
            ClearScreen();
        }
        else {
            Swal.fire('Error', 'Update Fail', 'error');
            ClearScreen();
        }
    });
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
                url: "/BatchClasses/Delete/",
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
                },
                error: function (response) {
                    Swal.fire("Oops", "We couldn't connect to the server!", "error")
                }
            });
        }
        $('#table').refresh();
    })
}

function Detail(Id, Batch, Class) {
    debugger;
    LoadEmployee($('#Participant'));
    $('#myDetail').text("Participants of "+ Batch + " " + Class);
    $('#Batchclass').val(Id);
    ResetTableSecondLayer(Id, Class)
    $("#Participant").val(null).trigger("change");
    $('#myModalDetail').modal('show');
}

function LoadIndexParticipant(Id, Class) {
    debugger;
    $.ajax({
        url: "/Participants/Detail/",
        type: "GET",
        async: false,
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            debugger;
            var html = '';
            var i = 1;
            $.each(result, function (index, val) {
                debugger;
                if (val.is_deleted == false) {
                    html += '<tr>';
                    html += '<td style="text-align:center">' + i + '</td>';
                    html += '<td>' + val.id + '</td>';
                    html += '<td>' + val.name + '</td>';
                    html += '<td>' + val.grade + '</td>';
                    html += '</tr>';
                    i++;
                }
            });
            $('.tbodydet').html(html);
            $('#myModalDetail').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function ClearScreen() {
    document.getElementById("Batch").disabled = false;
    document.getElementById("Class").disabled = false;
    $('#Id').val('');
    $('#Class').val(0);
    $('#Room').val(0);
    $('#Batch').val(0);
    $('#Trainer').val(0);
    //$('#Participant').empty();
    $('#Update').hide();
    $('#Save').show();
}

function Validate() {
    debugger;
    if ($('#Batch').val() == 0) {
        Swal.fire('Error', 'Please Choose Batch', 'error');
    } else if ($('#Class').val() == 0) {
        Swal.fire('Error', 'Please Choose Class', 'error');
    } else if ($('#Trainer').val() == 0) {
        Swal.fire('Error', 'Please Choose Trainer', 'error');
    } else if ($('#Room').val() == 0) {
        Swal.fire('Error', 'Please Choose Room', 'error');
    } else if ($('#Id').val() == "") {
        Save();
    } else {
        Edit();
    }
}

function InsertParticipant() {
    debugger;
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes!'
    }).then((result) => {
        if ($('#Participant').val() == "" || $('#Participant').val() == " ") {
            Swal.fire('Error', 'Please Choose Participant', 'error');
        }
        else {
            debugger;
            var participant = new Object();
            participant.id = $('#Participant').val();
            participant.batch_class = $('#Batchclass').val();
            $.ajax({
                type: 'POST',
                url: '/Participants/Insert/',
                contentType: 'application/x-www-form-urlencoded; charset=utf-8',
                data: participant
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire('Success', 'Insert Successfully', 'success');
                    LoadEmployee($('#Participant'));
                    $('#myModalDetail').modal('hide');
                    setTimeout("location.reload(true);", 1500);
                }
                else {
                    Swal.fire('Oops', 'This Participant In Another Class', 'warning');
                    ClearScreen();
                }
            });
        }
    });
}

function DeleteParticipant(Id, Class) {
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
                url: "/Participants/Delete/",
                type: "DELETE",
                data: { id: Id }
            }).then((result) => {
                debugger;
                if (result.StatusCode == 200) {
                    Swal.fire('Success', 'Delete Successfully', 'success');
                    $('#myModalDetail').modal('hide');
                    LoadEmployee($('#Participant'));
                    ClearScreenSecondLayer();
                }
                else {
                    Swal.fire('Error', 'Delete Fail', 'error');
                    ClearScreen();
                }
            });
        }
    })
}