function View() {
    return "Arquivos";
}

// datapicker
function Datapicker() {
    jQuery(document).ready(function () {
        jQuery("#Arq_dtDiario, #Arq_dtSessao, #Arq_dtExpediente").datepicker({
            dateFormat: 'dd/mm/yy',
            dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
            dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
            dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
            monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
            monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
            nextText: 'Próximo',
            prevText: 'Anterior'
        });
    });
}

// cria novo objeto
function Save() {
    jQuery(document).ready(function () {
        jQuery("#salvar").live("click", function () {
            jQuery("#bgModal").fadeIn();
            if (jQuery("#Arq_id").val() == "0") { var view = "Create"; } else { var view = "Update"; }
            jQuery.ajax({
                type: "post",
                url: "http://" + caminhoAbsoluto() + "/" + View() + "/" + view,
                data: {
                    Sec_id: jQuery("#Sec_id").val(),
                    Arq_diario: jQuery("#Arq_diario").val(),
                    Arq_pauta: jQuery("#Arq_pauta").val(),
                    Arq_dtDiario: jQuery("#Arq_dtDiario").val(),
                    Arq_dtSessao: jQuery("#Arq_dtSessao").val(),
                    Arq_dtExpediente: jQuery("#Arq_dtExpediente").val(),
                    Arq_conteudo: jQuery("#Arq_conteudo").val(),
                    Action: "create"
                },
                success: function (retorno) {
                    Modal(retorno.indice, retorno.titulo, retorno.mensagem, retorno.url, View());
                }
            });
        });
    });
}

// total de itens
function Total() {
    if (jQuery("#Arq_id").val() == "0") {
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
    if (jQuery("#Arq_id").val() == "0") {
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
            Arq_id: jQuery("#Arq_id").val(),
            Pagination: pagination
        },
        success: function (dados) {
            var qtd = dados.length;
            var retorno = "";
            if (qtd > 0) {
                retorno += '<table table class="tabela" cellspacing="0" cellpadding="0">';
                retorno += '<thead>';
                retorno += '<tr>';
                retorno += '<td class="celulathead" style="width: 90%;">Arquivos relativos a processos</td>';
                retorno += '<td class="celulathead" style="width: 10%; text-align: center; padding-left: 0px;">Detalhes</td>';
                retorno += '</tr>';
                retorno += '</thead>';
                for (i = 0; i < qtd; i++) {
                    retorno += '<tr>';
                    retorno += '<td class="celulabody paddingleft">';
                    retorno += '<a href="http://' + caminhoAbsoluto() + '/' + View() + '/Details?id=' + dados[i].Arq_id + '">';
                    retorno += dados[i].Arq_titulo;
                    retorno += '</a>';
                    retorno += '<div class="detalhes">';
                    retorno += '<b>Nº do Diário: </b>' + dados[i].Arq_diario + ' | ';
                    retorno += '<b>Diário publicado em: </b>' + dados[i].Arq_dtDiario + ' | ';
                    retorno += '<b>Adicionado ao sistema em: </b>' + dados[i].Arq_data + '';
                    retorno += '</div>';
                    retorno += '</td>';
                    retorno += '<td class="celulabody celulacentralizar">';
                    retorno += '<a href="http://' + caminhoAbsoluto() + '/' + View() + '/Details?id=' + dados[i].Arq_id + '">';
                    retorno += '<img src="http://' + caminhoAbsoluto() + '/Imagens/arquivos.png" border="0" title="Detalhes" style="cursor: pointer;" id="' + dados[i].Arq_id + '" />';
                    retorno += '</a>';
                    retorno += '</td>';
                }
                retorno += '</table>';
                jQuery("#read").html(retorno);
                // paginação
                var total = Total();
                var numPagina = Math.ceil(total / regPagina);
                var paginas = "";
                if (numPagina > 0) {
                    // paginação exibida
                    var limite = 5;
                    // pagina inicial
                    if ((page - limite) > 1) { var inicio = page - limite; } else { var inicio = 1; }
                    // página final
                    if ((page + limite) < numPagina) { var fim = page + limite; } else { var fim = numPagina; }
                    paginas += "<a href='http://" + caminhoAbsoluto() + "/" + View() + "/Index?pag=1' title='Ir para a primeira página'>";
                    paginas += "<div class='page'><<</div>";
                    paginas += "</a>";
                    for (var j = inicio; j < fim + 1; j++) {
                        paginas += "<a href='http://" + caminhoAbsoluto() + "/" + View() + "/Index?pag=" + j + "'>";
                        if (page == j) {
                            paginas += "<div class='page pageAct'>" + j + "</div>";
                        } else {
                            paginas += "<div class='page'>" + j + "</div>";
                        }
                        paginas += "</a>";
                    }
                    if (page != numPagina) {
                        paginas += "<div class='page pageRet'>...</div>";
                        paginas += "<a href='http://" + caminhoAbsoluto() + "/" + View() + "/Index?pag=" + numPagina + "'>";
                        paginas += "<div class='page'>" + numPagina + "</div>";
                        paginas += "</a>";
                    }
                    paginas += "<a href='http://" + caminhoAbsoluto() + "/" + View() + "/Index?pag=" + numPagina + "' title='Ir para a última página'>";
                    paginas += "<div class='page'>>></div>";
                    paginas += "</a>";
                }
                jQuery("#pagination").html(paginas);
            } else {
                jQuery("#read").html('<div class="alerta">Ainda não existem itens cadastrados</div>');
            }
        }
    });
}

// seleciona item por Id
function ReadGet() {
    if (jQuery("#Arq_id").val() !== "0") {
        setTimeout(function () {
            jQuery.ajax({
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                type: "get",
                url: "http://" + caminhoAbsoluto() + "/" + View() + "/ReadGet",
                data: {
                    Arq_id: jQuery("#Arq_id").val(),
                    Action: "listarGet"
                },
                success: function (dados) {
                    var qtd = dados.length;
                    var retorno = "";
                    if (qtd > 0) {
                        for (i = 0; i < qtd; i++) {
                            var texto = dados[i].Arq_conteudo.split("\n");
                            var textoFormat = "";
                            var textoFormat = dados[i].Arq_pauta + "<br /><br />";
                            for (j = 0; j < texto.length; j++) {
                                textoFormat += texto[j] + "<br />";
                            }
                            jQuery("#detalhesProcesso").html(textoFormat);
                        }
                    }
                }
            });
        }, 200);
    }
}