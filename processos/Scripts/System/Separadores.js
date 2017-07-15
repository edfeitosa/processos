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
                    Action: "create"
                },
                success: function (retorno) {
                    Modal(retorno.indice, retorno.titulo, retorno.mensagem, retorno.url, View());
                }
            });
        });
    });
}