$(document).ready(function () {
    $('#table').DataTable({
        "order": [],
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,

        }],
        "ajax": LoadIndexType(),
        "responsive": true
    });
});
function LoadIndexType() {
    $.ajax({
        type: "GET",
        url: "/Types/LoadType/",
        async: false,
        success: function (data) {
            debugger;
            var html = '';
            var i = 1;
            $.each(data, function (index, val) {
                debugger;
                html += '<tr>';
                html += '<td style="text-align:center">' + i + '</td>';
                html += '<td>' + val.name + '</td>';
                html += '</tr>';
                i++;
            });
            $('.tbody').html(html);
        }
    });
}

function ClearScreen() {
    $('#Id').val('');
    $('#Name').val('');
    $('#Update').hide();
    $('#Save').show();
}

function Save() {
    var type = new Object();
    type.id = $("#Id").val();
    type.name = $('#Name').val();
    $.ajax({
        type: 'POST',
        url: '/Types/InsertOrUpdate/',
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        data: type
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