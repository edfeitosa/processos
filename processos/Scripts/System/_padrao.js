// loader e background
jQuery(document).ready(function () {
    jQuery("#bgModal").hide();
})

// caminho absoluto
function caminhoAbsoluto() {
    var servidor = location.href;
    var nomeServidor = servidor.split("/");
    return nomeServidor[2] + "/processos";
}

// desloga por inatividade
function inatividade(tempo) {
    function desloga() {
        jQuery.ajax({
            type: "POST",
            url: "http://" + caminhoAbsoluto() + "/Home/Logout",
            data: {
                acao: "sair"
            },
            success: function (retorno) {
                if (retorno.indice == 'sucesso') {
                    jQuery(window.document.location).attr('href', "http://" + caminhoAbsoluto() + "" + retorno.url);
                } else {
                    Modal(retorno.indice, retorno.titulo, retorno.mensagem, retorno.url, View());
                }
            }
        });
    }

    jQuery(function () {
        timeout = setTimeout(function () {
            desloga();
        }, tempo);
    });

    jQuery(document).live('mousemove', function () {
        if (timeout !== null) {
            clearTimeout(timeout);
        }
        timeout = setTimeout(function () {
            desloga();
        }, tempo);
    });
}

// sair
jQuery(document).ready(function () {
    jQuery(".sair").live("click", function () {
        jQuery('#bgModal').fadeIn();
        // confirmação
        Modal("duvida", "Confirmação de Saída", "Deseja realmente sair do sistema?", "", "Home");
        jQuery("#okConf").live("click", function () {
            jQuery.ajax({
                type: "POST",
                url: "http://" + caminhoAbsoluto() + "/Home/Logout",
                data: {
                    Action: "sair"
                },
                success: function (retorno) {
                    if (retorno.indice == 'sucesso') {
                        jQuery(window.document.location).attr('href', "http://" + caminhoAbsoluto() + "" + retorno.url);
                    } else {
                        Modal(retorno.indice, retorno.titulo, retorno.mensagem, retorno.url, View());
                    }
                }
            });
        });
    });
});