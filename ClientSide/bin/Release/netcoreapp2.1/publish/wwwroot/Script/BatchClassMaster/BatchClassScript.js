$(document).ready(function () {
    $('#table').DataTable({
        "columnDefs":
            [{
                "targets": [0],
                "visible": true,
                "searchable": true
            }],
        "ajax": LoadIndexBatchClass(),
        "responsive": true
    });
    $("#BirthDate").datepicker();
    $(".select2").select2({
        placeholder: "Select Participant"
    });
    $("#Trainer").select2({
        maximumSelectionLength: 1,
        placeholder: "Select Trainer"
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

function LoadIndexBatchClass() {
    $.ajax({
        type: "GET",
        url: "/BatchClasses/LoadBatchClasses/",
        async: false,
        success: function (data) {
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                html += '<tr>';
                html += '<td style="text-align:center"> <a href="#" class="fa fa-pencil" onclick="return GetById(\'' + val.id + '\')"></a>';
                html += ' | <a href="#" class="fa fa-user" onclick="return Detail(\'' + val.id + '\')"></a></td> ';
                html += '<td style="text-align:center">' + i + '</td>';
                html += '<td>' + val.batch + '</td>';
                html += '<td>' + val.class + '</td>';
                html += '<td>' + val.trainer + '</td>';
                html += '<td>' + val.room + '</td>';
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
LoadEmployee($('#Participant'));
LoadEmployee($('#Trainer'))

function Save() {
    debugger;
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
        data: { batchClassVM: batchclass },
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

function GetById(Id) {
    $('#Trainer').val(null).trigger('change');
    $.ajax({
        url: "/BatchClasses/GetById/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            debugger;
            var newOption = new Option(result.trainer, null, true, true);
            $('#Trainer').append(newOption).trigger('change');
            $('#Id').val(result.id);
            $('#Batch').val(result.batch);
            $("#Room option:contains(" + result.room + ")").attr('selected', 'selected');
            $("#Class option:contains(" + result.class + ")").attr('selected', 'selected');
            //$('#Trainer').val(result.trainer);

            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function Edit() {
    var batchclass = new Object();
    batchclass.participant = $('#Participant').val();
    batchclass.batch = $('#Batch').val();
    batchclass.room = $('#Room').val();
    batchclass.trainer = $('#Trainer').val();
    batchclass.class = $('#Class').val();
    batchclass.id = $('#Id').val();
    $.ajax({
        url: "/BatchClasses/Update/",
        data: batch,
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

function Detail(Id) {
    $.ajax({
        url: "/Participants/Detail/",
        type: "GET",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: { id: Id },
        success: function (result) {
            var html = '';
            var i = 1;
            $.each(result, function (index, val) {
                debugger;
                html += '<tr>';
                html += '<td style="text-align:center">' + i + '</td>';
                html += '<td>' + val.id + '</td>';
                html += '<td>' + val.name + '</td>';
                html += '<td>' + val.grade + '</td>';
                html += '</tr>';
                i++;
            });
            $('.tbodydet').html(html);

            $('#myModalDetail').modal('show');
            $('#Update').show();
            $('#Save').hide();
        }
    })
}

function ClearScreen() {
    $('#Id').val('');
    $('#Class').val(0);
    $('#Room').val(0);
    $('#Batch').val(0);
    $('#Trainer').val(null).trigger('change');
    //$('#Participant').empty();
    $('#Update').hide();
    $('#Save').show();
}