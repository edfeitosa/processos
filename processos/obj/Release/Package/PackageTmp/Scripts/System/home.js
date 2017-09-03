function View() {
    return "Home";
}

function AcessoLogin() {
    jQuery("#bgModal").fadeIn();
    jQuery("#load").show();
    jQuery.ajax({
        type: "post",
        url: "http://" + caminhoAbsoluto() + "/" + View() + "/Login",
        data: {
            Usu_login: jQuery("#Usu_login").val(),
            Usu_senha: $.md5(jQuery("#Usu_senha").val())
        },
        success: function (retorno) {
            if (retorno.indice == "sucesso") {
                jQuery(window.document.location).attr('href', "http://" + caminhoAbsoluto() + "" + retorno.url);
            } else {
                Modal(retorno.indice, retorno.titulo, retorno.mensagem, retorno.url, View());
            }
        }
    });
}

function Login() {
    jQuery(document).ready(function () {
        jQuery("#acessar").live("click", function () {
            AcessoLogin();
        });
        jQuery('#Usu_login, #Usu_senha').live("keypress", function (e) {
            var code = null;
            code = (e.keyCode ? e.keyCode : e.which);
            if (code == 13) {
                AcessoLogin();
            }
        });
    });
}