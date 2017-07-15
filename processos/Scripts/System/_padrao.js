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