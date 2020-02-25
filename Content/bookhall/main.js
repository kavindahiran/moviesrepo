(function ($) {
    // USE STRICT
    "use strict";
    $("#date_from").datepicker({
        dateFormat: "dd-mm-yy",
        showOn: "both",
        buttonText: '<i class="zmdi zmdi-calendar-alt"></i>',
    });

    $("#date_to").datepicker({
        dateFormat: "dd-mm-yy",
        showOn: "both",
        buttonText: '<i class="zmdi zmdi-calendar-alt"></i>',
    });

})(jQuery);
function Tang() {

    var hallId = $("#HallId").children("option:selected").val();

    if (hallId != "") {
        var x = document.getElementById("quantity").value;//lay gia tri cu trong text

        if (parseInt(x) >= 0) {
            document.getElementById("quantity").value = parseInt(x) + 1;// + gia tri lay dc len 1 roi gan kq vao o text
            GetPrice();
        } 
    }
}
function Giam() {

    var hallId = $("#HallId").children("option:selected").val();

    if (hallId != "") {
        var x = document.getElementById("quantity").value;
        if (parseInt(x) >= 1) {
            document.getElementById("quantity").value = parseInt(x) - 1;
            GetPrice();
        }
    }
}

function GetPrice() {
    var hallId = $("#HallId").children("option:selected").val();
    if (hallId != "") {
        var quantity = $("#quantity").val();
        $.ajax({
            url: '/RatingApp/Booktickets/GetPrice?hallId=' + hallId + '&quantity=' + quantity,
            type: "GET",
            dataType: "json",
            contentType: false,
            processData: false,
            success: function (price) {
                $("#Price").val(price);
            }
        });
    }
}