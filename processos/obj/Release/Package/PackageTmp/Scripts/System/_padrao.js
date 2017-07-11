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

// modal sucesso
function modalSucesso(titulo, mensagem, url) {
    jQuery("#bgModal").fadeIn();
    jQuery("#bgModal").load("http://" + caminhoAbsoluto() + "/Modal/sucesso.html");
    jQuery("#sucesso").show();
    setTimeout(function () {
        jQuery('#cabecalhoSucesso').html(titulo);
        jQuery('.respostaSucesso').html(mensagem);
    }, 600);
    jQuery("#okSucesso").live("click", function () {
        jQuery("#sucesso").hide();
        setTimeout(function () {
            jQuery("#bgModal").hide();
        }, 300);
        if (url != "") {
            jQuery(window.document.location).attr('href', url);
        }
    });
}

// modal erro
function modalFalha(titulo, mensagem, url) {
    jQuery("#bgModal").fadeIn();
    jQuery("#bgModal").load("http://" + caminhoAbsoluto() + "/Modal/falha.html");
    jQuery("#falha").show();
    setTimeout(function () {
        jQuery('#cabecalhoFalha').html(titulo);
        jQuery('.respostaFalha').html(mensagem);
    }, 600);
    jQuery("#okFalha").live("click", function () {
        jQuery("#falha").hide();
        setTimeout(function () {
            jQuery("#bgModal").hide();
        }, 300);
        if (url != "") {
            jQuery(window.document.location).attr('href', url);
        }
    });
}

// modal pergunta
function modalPergunta(titulo, mensagem) {
    jQuery("#bgModal").fadeIn();
    jQuery("#bgModal").load("http://" + caminhoAbsoluto() + "/Modal/pergunta.html");
    jQuery("#pergunta").show();
    setTimeout(function () {
        jQuery('#cabecalhoPergunta').html(titulo);
        jQuery('.respostaPergunta').html(mensagem);
    }, 600);
    jQuery("#cancelaPergunta").live("click", function () {
        jQuery("#pergunta").hide();
        setTimeout(function () {
            jQuery("#bgModal").hide();
        }, 300);
    });
}

// modal alerta
function modalAlerta(titulo, mensagem, url) {
    jQuery("#bgModal").fadeIn();
    jQuery("#bgModal").load("http://" + caminhoAbsoluto() + "/Modal/alerta.html");
    jQuery("#alerta").show();
    setTimeout(function () {
        jQuery('#cabecalhoAlerta').html(titulo);
        jQuery('.respostaAlerta').html(mensagem);
    }, 600);
    jQuery("#okAlerta").live("click", function () {
        jQuery("#alerta").hide();
        setTimeout(function () {
            jQuery("#bgModal").hide();
        }, 300);
        if (url != "") {
            jQuery(window.document.location).attr('href', url);
        }
    });
}