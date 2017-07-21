function View() {
    return "Secoes";
}

// salva dados
function Save() {
    jQuery(document).ready(function () {
        jQuery("#salvar").live("click", function () {
            jQuery("#bgModal").fadeIn();
            if (jQuery("#Sec_id").val() == "0") { var action = "create"; } else { var action = "update"; }
            jQuery.ajax({
                type: "post",
                url: "http://" + caminhoAbsoluto() + "/" + View() + "/Create",
                data: {
                    Sep_titulo: jQuery("#Sep_titulo").val(),
                    Sep_separador: jQuery("#Sep_separador").val(),
                    Action: action
                },
                success: function (retorno) {
                    Modal(retorno.indice, retorno.titulo, retorno.mensagem, retorno.url, View());
                }
            });
        });
    });
}