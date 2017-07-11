function View() {
    return "Separadores";
}

// cria novo objeto
function Create() {
    jQuery(document).ready(function () {
        jQuery("#salvar").live("click", function () {
            jQuery("#bgModal").fadeIn();
            jQuery.ajax({
                type: "post",
                url: "http://" + caminhoAbsoluto() + "/" + View() + "/Create",
                data: {
                    Sep_titulo: jQuery("#Sep_titulo").val(),
                    Sep_separador: jQuery("#Sep_separador").val(),
                    acao: "create"
                },
                success: function (retorno) {
                    if (retorno.indice == "sucesso") {
                        if (retorno.url == "") {
                            modalSucesso(retorno.titulo, retorno.mensagem, "");
                        } else {
                            modalSucesso(retorno.titulo, retorno.mensagem, "http://" + caminhoAbsoluto() + "" + retorno.url);
                        }
                    } else if (retorno.indice == "alerta") {
                        if (retorno.url == "") {
                            modalAlerta(retorno.titulo, retorno.mensagem, "");
                        } else {
                            modalAlerta(retorno.titulo, retorno.mensagem, "http://" + caminhoAbsoluto() + "" + retorno.url);
                        }
                    } else {
                        if (retorno.url == "") {
                            modalFalha(retorno.titulo, retorno.mensagem, "");
                        } else {
                            modalFalha(retorno.titulo, retorno.mensagem, "http://" + caminhoAbsoluto() + "" + retorno.url);
                        }
                    }
                }
            });
        });
    });
}