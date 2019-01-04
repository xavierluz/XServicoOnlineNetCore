$('.nav-item').click(function () {
    $('.active').removeClass();
});

$(function () {
    $(document).ready(function () {
        $('.pagina-anterior').click(function () {
            window.history.back();
        })
    });
})
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
var submitAjax = function (form, url) {
    if ($(form).valid()) {
        let dados = $(form).serialize();

        $.ajax({
            type: "POST",
            url: url,
            data: dados,
            success: function (data) {
                if (data.retorno.erro === undefined || data.retorno.erro === null) {
                    md.notificacaoSucesso('top', 'right', data.retorno.sucesso);
                } else {
                    md.notificacaoErro('top', 'right', data.retorno.erro);
                }
            },
            error: function (objeto) {
                md.notificacaoAtencao('top', 'right', 'Ocorreu um erro, verifique coma Administrador(rede ou sistema)');
            }
            
        });
    } else {
        return false;
    }
    

    //<button class="btn btn-primary btn-block" onclick="md.showNotification('top','center')">Top Center</button>
};
