
$(function () {
    var dados = new Array();
    $.ajax({
        type: "GET",
        url: '/Movie/MovieList',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#FiltroNome").autocomplete({
                source: data
            });
        }
    });
    
});