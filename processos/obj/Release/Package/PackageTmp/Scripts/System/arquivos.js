function View() {
    return "Arquivos";
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
                    Arq_conteudo: jQuery("#conteudo").val(),
                    Action: "create"
                },
                success: function (retorno) {
                    //Modal(retorno.indice, retorno.titulo, retorno.mensagem, retorno.url, View());
                }
            });
        });
    });
}