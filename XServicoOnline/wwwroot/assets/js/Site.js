$('.nav-item').click(function () {
    $('.active').removeClass();
});
var submit = function (form,url) {
    $(form).submit(function () {
        let dados = $(this).serialize();

        $.ajax({
            type: "POST",
            url: url,
            data: dados,
            success: function (data) {
                alert(data);
            }
        });

        return false;
    });
};