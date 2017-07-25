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
                url: "http://" + caminhoAbsoluto() + "/" + View() + "/Save",
                data: {
                    Sec_id: jQuery("#Sec_id").val(),
                    Sec_idPai: jQuery("#Sec_idPai").val(),
                    Sec_titulo: jQuery("#Sec_titulo").val(),
                    Action: action
                },
                success: function (retorno) {
                    Modal(retorno.indice, retorno.titulo, retorno.mensagem, retorno.url, View());
                }
            });
        });
    });
}