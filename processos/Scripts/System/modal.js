function HtmlModal(tipo, titulo, mensagem, url) {
    switch (tipo) {
        case "sucesso": var imagem = "sucesso.png"; break;
        case "erro": var imagem = "erro.png"; break;
        case "informacao": var imagem = "informacao.png"; break;
        case "duvida": var imagem = "duvida.png"; break;
        default: imagem = ""; break;
    }
    var reply = '';
    reply += '<div id="modal" class="modal sombra">';
    reply += '<div id="cabecalho">' + titulo + '</div>';
    reply += '<div id="icone"><img src="http://' + caminhoAbsoluto() + '/Imagens/' + imagem + '" alt="icone" /></div>';
    reply += '<div class="resposta">' + mensagem + '</div>';
    if (tipo == "duvida") {
        reply += '<div class="ok" id="cancela">Não</div>';
        reply += '<div class="ok" id="ok" style="margin-right: 120px;">Sim</div>';
    } else {
        reply += '<div class="ok" id="ok">Ok</div>';
    }
    reply += '</div>"';
    return reply;
}

function Modal(tipo, titulo, mensagem, url, view) {
    jQuery("#bgModal").fadeIn();
    setTimeout(function () {
        jQuery("#bgModal").html(HtmlModal(tipo, titulo, mensagem, url));
    }, 300);
    jQuery("#ok, #cancela").live("click", function () {
        if (url != "") {
            var urlFinal = "http://" + caminhoAbsoluto() + url;
            jQuery(window.document.location).attr('href', urlFinal);
        } else {
            jQuery("#bgModal").html("");
            jQuery("#bgModal").hide();
        }
    });
}