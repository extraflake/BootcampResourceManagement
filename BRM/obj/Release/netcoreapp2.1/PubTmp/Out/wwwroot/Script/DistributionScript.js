$(document).ready(function () {
    LoadDistribution();
});

function LoadDistribution() {
    var data = "";
    $.ajax({
        type: "GET",
        url: "/Dashboard/LoadDistribution/",
        dataType: 'json',
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            Morris.Donut({
                element: 'morris-donut-chart',
                data: data,
                resize: true
            });
        }
    });
}