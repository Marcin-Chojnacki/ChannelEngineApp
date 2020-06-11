$(document)
    .ready(function () {
        $("#orderStatusName").change(function () {
            $("#ordersForm").submit();
        });
    })