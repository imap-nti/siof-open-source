// JavaScript Document

//START READY
$(document).ready(function () {

    /************************
    *
    * REMOVENDO O CACHE DOS
    * AJAX DA APLICACAO
    *
    *************************/
    $.ajaxSetup({ cache: false });

    /************************
	*
	* EXTERNAL LINKS
	*
	*************************/
    if ($('a[rel="external"]').length > 0) {
        externalLinks();
    }

    /*************************
	*
	*	TRANSFORM INPUT FILE
	*
	**************************/
    /*$(window).on('click', 'input[type="file"]', function () {
        getValueFile();
    });

    if ($('input[type="file"]').length > 0) {
        transformInputFile();
        getValueFile();
    }*/

    /*************************
	*
	*	CHOSEN
	*
	**************************/
    if ($('.chosen').length > 0) {
        $('.chosen').chosen();
    }

    // FIX BUG CHOSEN + BUTTON
    if ($.browser.webkit) {
        $('.chzn-container').ready(function () {
            $('.chzn-container + input[type="submit"], .chzn-container + input[type="reset"], .chzn-container + input[type="button"], .chzn-container + button, .chzn-container + .button_style').css('float', 'none');
        });
    }


    /*************************
	*
	*	PIN UNPIN
	*
	**************************/
    if ($('.unpin_content').length > 0) {
        pinUnpin();
    }


    /************************
	*
	* jQuery UI DATEPICKER
	*
	*************************/
    //DATEPICKER CONVENCIONAL, COM SELEÇÃO DE ANO E MÊS POR DROPDOWN
    $(".date, .datepicker").datepicker({
        // autoSize: true,
        showAnim: 'drop',
        duration: 'normal',
        showOptions: { direction: 'up' },
        dateFormat: "dd/mm/yy",
        monthNames: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        monthNamesShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
        dayNames: ["Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sábado"],
        dayNamesShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"],
        dayNamesMin: ["D", "S", "T", "Q", "Q", "S", "S"],
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        yearRange: '1900:2030'
    });
    //DATEPICKER COM DATE RANGE(EXEMPLO DE USO).
    $(".date_range").datepicker({
        // autoSize: true,
        showAnim: 'drop',
        duration: 'normal',
        showOptions: { direction: 'up' },
        dateFormat: 'dd/mm/yy',
        monthNames: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
        monthNamesShort: ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"],
        dayNames: ["Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sábado"],
        dayNamesShort: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"],
        dayNamesMin: ["D", "S", "T", "Q", "Q", "S", "S"],
        nextText: "",
        prevText: "",
        changeMonth: true,
        changeYear: true,
        yearRange: '1900:2030',
        minDate: -10,
        maxDate: +5
    });


    /************************
	*
	* ZEBRA STYLE TABELAS
	*
	*************************/
    if ($('table').length > 0) {
        zebraStyle();
    }
    if ($('table.verticalHeaders').length > 0) {
        zebraStyleVertical();
    }

    /************************
	*
	* TABLE SORTER
	*
	*************************/
    if ($('.tablesorter').length > 0) {
        setupTablesorter();
    }

    /************************
    *
    * JQUERY-UI TOOLTIP
    *
    *************************/
    //$(window).tooltip({
    //    track: false,
    //    tooltipClass: "custom-tooltip-styling"
    //});



    /************************
    *
    * MAIN NAV BEHAVIOR
    *
    *************************/
    if ($('#mainNav_wrapper').length > 0) {
        attrConvert();
        $('a.bt_mainNav', 'ul#mainNav').bind({
            click: showSubmenu,
            mouseover: showTitleMenu
            //mouseout: changeTitleMenu
        });
        $('ul#mainNav').bind({
            mouseleave: hideTitleMenu
        });

        showChild();
        hideChild();
        showSubChild();
        hideSubChild();


        //INICIA SORTABLE JQUERY UI PARA SUBMENU
        $('ul.submenu', '#mainNav_wrapper').sortable({
            items: 'li',
            contaiment: 'parent',
            handle: '.handler',
            placeholder: "ui-state-highlight",
            revert: true,
            tolerance: 'pointer',
            opacity: 0.5
        });
    }


    /************************
	*
	* CAD MASK
	*
	*************************/
    //	MASCARA DE CAMPO CPF/CNPJ
    if ($('.mask_cpf_cnpj')[0]) {
        $('.mask_cpf_cnpj').cadMask({ hasRawField: true });
        $('.mask_cpf_cnpj').keyup(function () {
            if ($('#raw_id').val().length > 0) {
                $('#raw_id').val($(this).val());
            }
        }).blur(function () {
            $('#raw_id').val('');
            $(this).data('value', new Array());
        });
    }
    //MASCARA DE NUMEROS
    $('body').on('focus', '.mask_numbers', function (event) {
        event.preventDefault();
        $('.mask_numbers').attr("onkeypress", "return sonumeros(event);");
    })
    //	CLASSE PARA DESABILITAR A TECLA ENTER
	.on('keyup', '.disableEnter', function (event) {
	    event.preventDefault();
	    var unicode = event.keyCode;
	    // Desabilitando o enter
	    if (unicode == 13) {
	        return false;
	    }
	    return true;
	})
    //	MASCARA DE METRO QUADRADO
    .on('focus', '.mask_metro_quadrado', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_metro_quadrado').maskMoney({ precision: 3 });
        if ($(this).hasClass('must_return_value')) {
            $(this).blur(function () {
                if (($(this).val() == '' || $(this).val() == '0.000' || $(this).val() == undefined))
                    $(this).val('0.000');
            });
        }
    })
    //	MASCARA DE CAMPOS PARA VERIFICACAO DE EMAIL
    .on('focus', '.mask_email', function (event) {
        event.preventDefault();
        $('.mask_email').attr("onblur", "return checaValidadeEmail(this.name, this.value);");
    })
    //	MASCARA DE NIRF
    .on('focus', '.mask_nirf', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_nirf').mask('9.999.999-9');
    })
        //	MASCARA DE MAC
    .on('focus', '.mask_mac', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_mac').mask('99:99:99:99:99:99');
    })
    //	MASCARA DE CODIGO INCRA
    .on('focus', '.mask_codigo_incra', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_codigo_incra').mask('999.999.999.999-9');
    })
    //	MASCARA DE TELEFONE
    .on('focus', '.mask_telefone', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_telefone').mask('(99) 9999-9999');
    })
    //	MASCARA DE CEP
    .on('focus', '.mask_cep', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_cep').mask('99.999-999');
    })
    //	MASCARA DE CPF
    .on('focus', '.mask_cpf', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_cpf').mask('999.999.999-99');
    })
    //	MASCARA DE CNPJ
    .on('focus', '.mask_cnpj', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_cnpj').mask('99.999.999/9999-99');
    })
    //	MASCARA DE DATA
    .on('focus', '.mask_date', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_date').mask('99/99/9999');
    })
    //	MASCARA DE HORAS
    .on('focus', '.mask_hours', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_hours').mask('99:99');
    })
    //	MASCARA DE VALORES - REAL
    .on('focus', '.mask_real', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_real').maskMoney({ showSymbol: true, symbol: 'R$ ', defaultZero: false, thousands: '.', decimal: ',' });
        if ($(this).hasClass('must_return_value')) {
            $(this).blur(function () {
                if (($(this).val() == '' || $(this).val() == '0,00' || $(this).val() == undefined))
                    $(this).val('0,00');
            });
        }
    })
    //	MASCARA DE PERCENTUAL
    .on('focus', '.mask_percentual', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_percentual').maskMoney({ showSymbol: true, symbol: '%', precision: 3, decimal: ',' });
        if ($(this).hasClass('must_return_value')) {
            $(this).blur(function () {
                if (($(this).val() == '' || $(this).val() == '0.000' || $(this).val() == undefined))
                    $(this).val('0.000');
            });
        }
    })
    //	MASCARA DE QUANTIDADE
    .on('focus', '.mask_qtd', function (event) {
        event.preventDefault();
        $(this).removeClass('mask_qtd').maskMoney({ showSymbol: false, symbol: '', defaultZero: false, thousands: '', decimal: '' });
    });


    /************************
	*
	* LEGEND BLOCK
	*
	*************************/
    if ($('.legend_block').length > 0) {
        legendBlock();
    }



    //AGUARDA A RENDERIZAÇÃO DA PÁGINA APÓS O CARREGAMENTO DO DOM
    $(window).load(function () {
        /*****************************************************
		*
		* AUTO SUBTITLE PARA IMAGENS PARA A CLASSE autoSubtitle
		* AUTO WIDTH PARA CLASSE .legenda
		* PHOTO GALLERY PARA A CLASSE .photoGallery
		*
		*****************************************************/
        if ($('.autoSubtitle').length > 0) {
            autoSubtitleImg();
        }
        if ($('.legenda').length > 0) {
            autoWidthSubtitle();
        }

        /*************************
		*
		*	KONSERTINA
		*
		**************************/
        if ($('.konsertina').length > 0) {
            konsertina();
        }
    });


});
//END READY





/************************
*
* EXTERNAL LINKS
*
*************************/
function externalLinks() {
    $('a[rel="external"]').each(function (index, element) {
        $(element).attr('target', 'blank');
    });
}


/*************************
*
*	TRANSFORM INPUT FILE
*
**************************/
/*function transformInputFile() {
    $('input[type="file"]').each(function (index, element) {
        var id = $(element).attr('id');

        $('<div class="updateFile" id="updateFile_' + index + '"><input type="text" value="selecione um arquivo..." /><a href="javascript:void(0)" class="button">buscar aqruivo</a></div>').insertBefore(element);
        $('#updateFile_' + index).prepend(element);
        $('input[type="text"]', '#updateFile_' + index).attr('id', id + '_' + index);
        $('input[type="file"]', '#updateFile_' + index).removeAttr('size').css({
            width: '260px',
            height: '30px'
        });
    });
}

function getValueFile() {
    $('input[type="file"]', '.updateFile').live('change', function () {
        var value = $(this).val();

        $(this).parent().children('input[type="text"]').val(value);
    });
}*/


/************************
*
* ZEBRA STYLE TABELAS
*
*************************/
//ZEBRA HORIZONTAL
function zebraStyle() {
    //o seletor :even é par e o :odd é ímpar, mas como o seletor jQuery é baseado em um 0-indexed, por isso a inversão das classes
    $('table').children('tbody').find('tr:even').addClass('odd');
    $('table').children('tbody').find('tr:odd').addClass('even');
}
//ZEBRA VERTICAL
function zebraStyleVertical() {
    //o seletor :even é par e o :odd é ímpar, mas como o seletor jQuery é baseado em um 0-indexed, por isso a inversão das classes
    $('table.verticalHeaders').children('tbody').find('tr').find('td:even').addClass('odd');
    $('table.verticalHeaders').children('tbody').find('tr').find('td:odd').addClass('even');
    // LIMPAR O ZEBRA HORIZONTAL
    $('table.verticalHeaders').children('tbody').find('tr').removeAttr('class');
}


/*****************************
*
* jQuery UI CAIXAS DE ALERTAS
*
*****************************/
//CAIXA DE MENSAGEM
function messageBox(title_messageBox, content_message) {
    $('#messageBox').html(content_message);
    $('#messageBox').dialog({
        modal: true,
        buttons: { 'OK': function () { $(this).dialog('close') } },
        resizable: false,
        title: title_messageBox,
        show: 'clip',
        hide: 'clip',
        position: ['center', 'center']
    });
}

//CAIXA DE ALERTA
function alertBox(title_alertBox, content_alertBox, icon) {
    //exemplo de mensagem
    var content_alertBox2 = '<p><span class="' + icon + '"></span>' + content_alertBox + '</strong></p>';
    $('#alertBox').html(content_alertBox2);
    $('#alertBox').dialog({
        modal: false,
        resizable: false,
        title: title_alertBox,
        show: 'clip',
        hide: 'clip',
        position: ['center', 'center']
    });
}

//CAIXA COM BOTÕES CUSTOMIZÁVEIS
function customBox(title_customBox, content_customBox) {
    $('#customBox').html(content_customBox);
    $('#customBox').dialog({
        modal: true,
        resizable: false,
        title: title_customBox,
        show: 'clip',
        hide: 'clip',
        position: ['center', 'center'],
        //remove obloco personalizado de botões inseridos com o insertAfter, caso contrário eles seriam repetidos a cada vez que abrisse o elemento
        close: function () { $('.ui-dialog-buttonpane').remove(); }
    });
    //remove estilos inline do plugin
    $('#customBox').removeAttr('style');
    //inseri bloco personalizado de botões
    $('<div class="ui-dialog-buttonpane ui-widget-content ui-helper-clearfix"><div class="ui-dialog-buttonset"><a href="javascript:void" rel="nofollow" class="button_style icon_print">Imprimir</a><a href="javascript:void" rel="nofollow" class="button_style icon_cancel">Cancelar</a></div></div>').insertAfter('#customBox');
}


/*********************************
*
* AUTO SUBTITLE PARA IMAGENS
*
*********************************/
function autoSubtitleImg(class_mapper, classCustomStyle) {
    // default
    var class_mapper = (class_mapper == '' || class_mapper == undefined) ? '.autoSubtitle' : class_mapper;
    var classCustomStyle = (classCustomStyle == '' || classCustomStyle == undefined) ? '' : classCustomStyle;

    $(class_mapper).each(function (index, element) {
        var subtitle = $(element).attr('alt');
        var heightImg = $(element).height();
        var widthImg = $(element).width();

        $(element).wrap('<div class="container_image" />').after('<p class="legenda ' + classCustomStyle + '">' + subtitle + '</p>');
        $('.container_image').css({
            height: heightImg,
            width: widthImg
        });

    });
}

/*********************************
*
* AUTO WIDTH PARA CLASSE .legenda
*
*********************************/
function autoWidthSubtitle(class_mapper) {
    // default
    var class_mapper = (class_mapper == '' || class_mapper == undefined) ? '.legenda' : class_mapper;

    $(class_mapper).each(function (index, element) {
        var siblingWidth = $(element).prev().outerWidth();
        $(element).css('width', siblingWidth);

        var outerWidth = $(element).outerWidth();
        var width = $(element).width();
        var padding = parseInt(outerWidth - width);

        if (outerWidth > width) {
            $(element).css('width', width - padding);
        }

    });
}




/************************
*
* TABLE SORTER
*
*************************/
if ($('.tablesorter').length > 0) {
    setupTablesorter();
}
function setupTablesorter() {
    //	Method money
    $.tablesorter.addParser({
        id: "money",
        is: function (s) {
            return true;
        },
        format: function (s) {
            return $.tablesorter.formatFloat(s.replace(/ /, '').replace('R$', '').replace(/\./, '').replace(/\,/, '.').replace(new RegExp(/[^0-9,]/g), ""));
        },
        type: "numeric"
    });

    var classHeaders = {
        'text': '.sorter-text',
        'digit': '.sorter-digit',
        'currency': '.sorter-currency',
        'ipAddress': '.sorter-ipAddress',
        'url': '.sorter-url',
        'isoDate': '.sorter-isoDate',
        'usLongDate': '.sorter-usLongDate',
        'shortDate': '.sorter-shortDate',
        'time': '.sorter-time',
        'metadata': '.sorter-metadata',
        'money': '.sorter-money',
    };

    var tableHeaders = '', separator;

    $('.tablesorter').each(function (i, e) {
        $.each(classHeaders, function (key, value) {
            $(this).find(value).each(function (pos) {
                if (separator == undefined)
                    separator = ',';
                tableHeaders += separator + ' ' + $(this).index() + ' : { sorter: "' + key + '"}';
            });
        });
        $(this).tablesorter({ widgets: ['zebra'], dateFormat: 'uk', noSorterClass: 'no-sorter', headers: tableHeaders });
    });
}



/***********************************
 *
 *      MAIN NAV BEHAVIOR
 *
 **********************************/

submenuOpen = '';

//FUNÇÃO ATTR CONVERT
function attrConvert() {
    $('ul.submenu', '#mainNav_wrapper').each(function (index, element) {
        //pega o valor do atributo original
        var sourceTitle = $(element).attr('title');
        //insere o novo atributo com o valor do atributo original
        $(element).attr('submenu_name', sourceTitle);
        //remove o atributo original
        $(element).removeAttr('title');
    });
}
//FUNÇÃO MOSTRAR SUBMENU
function showSubmenu() {
    //retira classe controle
    $('a.bt_mainNav', 'ul#mainNav').removeClass('stateHover');
    //pega o valor do atributo rel do link clicado
    var rel = $(this).attr('rel');

    if ($('ul.submenu[submenu_name=' + rel + ']', '#mainNav_wrapper').is(':hidden')) {
        //oculta todos os submenus
        $('.submenu').hide();
        //show submenu
        $('ul.submenu[submenu_name=' + rel + ']', '#mainNav_wrapper').slideDown('normal', 'easeOutBounce');
        //insere classe no botão clicado
        $(this).addClass('stateHover');
        //atribui valor do rel na variável de controle
        submenuOpen = rel.replace(/_/g, ' ');
        //testa se o elemento title_menus está fechado - caso se clique no link para fechar e em seguida clique novamente para abrir sem disparar o evento mouseover, desta forma o title_menu não abre.
        if ($('#titles_menu', 'div#mainNav_wrapper').is('.closed')) {
            //pega o valor do atributo rel
            var title = $(this).attr('rel').replace(/_/g, ' ');
            //inseri texto no elemento titles_menu e show titles_menu
            $('#titles_menu', 'div#mainNav_wrapper').addClass('open').html(title).slideDown('fast', 'swing');
        }
    } else {
        //oculta todos os submenus
        $('ul.submenu[submenu_name=' + rel + ']', '#mainNav_wrapper').slideUp('fast', 'swing');
        //hide titles_menu
        $('#titles_menu', '#mainNav_wrapper').removeClass('open').slideUp('fast', 'swing').empty().addClass('closed');
        submenuOpen = '';
    }
}

//FUNÇÃO MOSTRAR TITLES MENU
function showTitleMenu() {
    //pega o valor do atributo rel
    var title = $(this).attr('rel').replace(/_/g, ' ');
    //inseri texto no elemento titles_menu e show titles_menu
    $('#titles_menu', 'div#mainNav_wrapper').html(title).slideDown('fast', 'swing').addClass('open').removeClass('closed');
    $('#titles_menu', 'div#mainNav_wrapper').clearQueue();
}
//FUNÇÃO MUDAR TITLES MENU
function changeTitleMenu() {
    //limpa comteúdo do elemento titles_menu
    $('#titles_menu', '#mainNav_wrapper').empty();
    //remover da fila todos os itens que ainda não foram executados
    $('#titles_menu', '#mainNav_wrapper').clearQueue();
}
//FUNÇÃO OCULTAR TITLES MENU
function hideTitleMenu() {
    if (submenuOpen == '') {
        //fecha title_menu
        $('#titles_menu', '#mainNav_wrapper').slideUp('fast', 'swing').empty().removeClass('open').addClass('closed');
    } else {
        $('#titles_menu', '#mainNav_wrapper').html(submenuOpen);
    }
}

// FUNÇÃO SHOW CHILD
function showChild() {
    $('ul.submenu > li').live('mouseover', function () {
        if ($('ul.child', this).length > 0) {
            $(this).addClass('active');
            $(this).find('.child').fadeIn().clearQueue();
        }
    })
}
// FUNÇÃO HIDE CHILD
function hideChild() {
    $('ul.submenu > li').live('mouseleave', function () {
        $(this).find('.child').fadeOut().clearQueue();
        $(this).removeClass('active');
    })
}

// FUNÇÃO SHOW SUBCHILD
function showSubChild() {
    $('ul.child > li').live('mouseover', function () {
        if ($('.subChild', this).length > 0) {
            $(this).addClass('active');
            $(this).find('.subChild').fadeIn().clearQueue();
        }
    })
}
// FUNÇÃO HIDE SUBCHILD
function hideSubChild() {
    $('ul.child > li').live('mouseleave', function () {
        $(this).find('.subChild').fadeOut().clearQueue();
        $(this).removeClass('active');
    })
}


/************************
*
* LEGEND BLOCK
*
*************************/
function legendBlock(class_mapper, mainTitle, grid) {
    // default
    var class_mapper = (class_mapper == '' || class_mapper == undefined) ? '.icon_legend' : class_mapper,
		mainTitle = (mainTitle == '' || mainTitle == undefined) ? 'Legendas' : mainTitle,
		grid = (grid == '' || grid == undefined) ? 'grid_3' : grid
    ;

    $('.legend_block').each(function (index, element) {
        $(element).wrap('<div class="legendBlock_container grad_rd_box" id="legendContainer_' + index + '" />');
    });

    $('.legendBlock_container').prepend('<h2>' + mainTitle + '</h2>');

    $('.legend_block').addClass('clearfix').children('li').addClass('flutuaEsqLess ' + grid).find(class_mapper).each(function (index, element) {
        $('<span class="legend_text">-&nbsp;' + $(element).text() + '</span>').insertAfter(element);

    });
}