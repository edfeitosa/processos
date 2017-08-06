﻿function View() {
    return "Secoes";
}

// salva dados
function Save() {
    jQuery(document).ready(function () {
        jQuery("#salvar").live("click", function () {
            jQuery("#bgModal").fadeIn();
            if (jQuery("#Sec_id").val() === "0") { let action = "create"; } else { let action = "update"; }
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

function Total() {
    if (jQuery("#Sec_id").val() == "0") {
        var total;
        jQuery.ajax({
            async: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            type: "get",
            url: "http://" + caminhoAbsoluto() + "/" + View() + "/Read",
            data: {
                Action: "total"
            },
            success: function (dados) {
                total = dados.length;
            }
        });
        return total;
    } else {
        return 0;
    }
}

// lista dados 
function Read(regPagina = 10, page = 1) {
    if (jQuery("#Sec_id").val() == "0") {
        var start = (regPagina * page) - regPagina;
        var pagination = "LIMIT " + start + ", " + regPagina + "";
    } else {
        var pagination = "";
    }
    jQuery.ajax({
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        type: "get",
        url: "http://" + caminhoAbsoluto() + "/" + View() + "/Read",
        data: {
            Sec_id: jQuery("#Sec_id").val(),
            Pagination: pagination
        },
        success: function (dados) {
            var qtd = dados.length;
            var retorno = "";
            if (qtd > 0) {
                retorno += '<table table class="tabela" cellspacing="0" cellpadding="0">';
                retorno += '<thead>';
                retorno += '<tr>';
                retorno += '<td class="celulathead" style="width: 70%;">Seções</td>';
                retorno += '<td class="celulathead" style="width: 10%; text-align: center;" style="padding-left: 0px;">Subseções</td>';
                retorno += '<td class="celulathead" style="width: 10%; text-align: center;" style="padding-left: 0px;">Editar</td>';
                retorno += '<td class="celulathead" style="width: 10%; text-align: center;" style="padding-left: 0px;">Excluir</td>';
                retorno += '</tr>';
                retorno += '</thead>';
                for (i = 0; i < qtd; i++) {
                    retorno += '<tr>';
                    retorno += '<td class="celulabody paddingleft">' + dados[i].Sec_titulo + '</td>';
                    retorno += '<td class="celulabody celulacentralizar">';
                    retorno += '<a href="http://' + caminhoAbsoluto() + '/' + View() + '/Save?id=' + dados[i].Sec_id + '">';
                    retorno += '<img src="http://' + caminhoAbsoluto() + '/Imagens/secoes.png" border="0" title="Editar item" class="separabotao" />';
                    retorno += '</a>';
                    retorno += '</td>';
                    retorno += '<td class="celulabody celulacentralizar">';
                    retorno += '<a href="http://' + caminhoAbsoluto() + '/Reserva/Index?fun_id=' + dados[i].Sec_id + '&fun_nome=' + dados[i].Sec_titulo + '">';
                    retorno += '<img src="http://' + caminhoAbsoluto() + '/Imagens/editar.png" border="0" title="Cadastro de reserva" style="cursor: pointer;" class="separabotao" id="' + dados[i].Sec_id + '" />';
                    retorno += '</a>';
                    retorno += '</td>';
                    retorno += '<td class="celulabody celulacentralizar">';
                    retorno += '<img src="http://' + caminhoAbsoluto() + '/Imagens/lixeira.png" border="0" title="Excluir item" style="cursor: pointer;" class="separabotao excluirItem" id="' + dados[i].Sec_id + '" />';
                    retorno += '<input type="hidden" name="fun_nome-' + dados[i].Sec_id + '" id="fun_nome-' + dados[i].Sec_id + '" value="' + dados[i].Sec_titulo + '" />';
                    retorno += '</td>';
                    retorno += '</tr>';
                }
                retorno += '</table>';
                jQuery("#read").html(retorno);
                // paginação
                var total = Total();
                var numPagina = Math.round(total / regPagina);
                var paginas = "";
                for (var j = 1; j < numPagina + 1; j++) {
                    paginas += "<a href='http://" + caminhoAbsoluto() + "/" + View() + "/Index?pag=" + j + "'>";
                    paginas += "<div class='page'>" + j + "</div>";
                    paginas += "</a>";
                }
                jQuery("#pagination").html(paginas);
            } else {
                jQuery("#read").html('<div class="alerta">Ainda não existem itens cadastrados</div>');
            }
        }
    });
}