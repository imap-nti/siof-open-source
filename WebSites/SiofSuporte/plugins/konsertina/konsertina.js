/************************************************************************************************************
*
*   KONSERTINA
*
*   autor:Thiago Teles Pereira
*   criado em: 19/03/2012
*   modificado em: 16/11/2012
*   versão: 3.0
*
*
*   DESCRIÇÃO
*    Cria o efeito sanfona para ocultar ou mostrar itens.
*    A palavra konsertina significa sanfona em norueguês.
*
*
*   HTML, CLASSES E ID'S BÁSICO:
*    <ul class="konsertina">
*         <li id="step_1" class="title startOpen">
*            <h3 class="elementMainTitle">Some title</h3>
*            <ul class="explanation">
*                <li>conteúdo expansível</li>
*            </ul>
*        </li>
*        <li id="step_2" class="disabled">
*            <h3 class="elementMainTitle">Some title</h3>
*            <ul class="explanation">
*                <li>conteúdo expansível</li>
*            </ul>
*        </li>
*    </ul>
*
*
*   FUNCIONALIDADES:
*    Disabled -> Impede com que o elemento seja expandido. Adicione a classe .disabled ao invés da classe title;
*
*    Start Open -> Para iniciar com um elemento já expandido basta adicionar a classe .startOpen no elemento li.title||li.disabled;
*
*    Step -> Para usar o recurso de ativação por passos(step), deve-se atribuir um id ao elemento li(title||disabled) com o valor step_n,
*           onde n significa o número do passo atual. Depois basta chamar a função callBack "konsertinaActivatingElement(step)", onde
*           o step é apenas o número corespondente a id do elemento que se deseja ativar.
*
*
*   JAVASCRIPT:
*    if($('.konsertina').length > 0){
*        konsertina();
*    }
*
*
*   DEPENDÊNCIAS:
*    > última versão do jQuery
*    > konsertina.js ou konsertina.min.js
*    > konsertina.css
*
************************************************************************************************************/
function konsertina(){
    $('.elementMainTitle').addClass('expand');

    $('<span class="expand"></span>').prependTo('.elementMainTitle' , '.konsertina .title');


    $('li.disabled', '.konsertina').each(function(index, element){
        $('.explanation', '.konsertina .disabled').slideUp();
        $('.elementMainTitle > span', element).remove();
    });

    $('li[class="title"] .explanation', '.konsertina').hide();

    if($('li.startOpen .explanation', '.konsertina').length > 0){
       $('.elementMainTitle', 'li.startOpen').removeClass('expand').addClass('collapse').children('.expand').removeClass('expand').addClass('collapse');
    }

    $('.elementMainTitle').click(function(){
        if($(this).parent().children('.explanation').length > 0 && $(this).parent().hasClass('title')){
            if($(this).is('.expand')){
                $(this).parent().find('.explanation').slideDown();
                $(this).removeClass('expand').addClass('collapse').children('.expand').removeClass('expand').addClass('collapse');
            }else{
                $(this).parent().find('.explanation').slideUp();
                $(this).removeClass('collapse').addClass('expand').children('.collapse').removeClass('collapse').addClass('expand');
            }
        }
    });
}

function konsertinaActivatingElement(step){
    konsertinaClosingAllElement(step);
    konsertinaEnablingElement(step);
    konsertinaExpandingElement(step);
}

function konsertinaClosingAllElement(step){
    $('.explanation', '.title').slideUp();
    $('.collapse').addClass('expand').removeClass('collapse');
}

function konsertinaEnablingElement(step){
    $('.disabled#step_'+step).children('.elementMainTitle').prepend('<span class="collapse"></span>');
    $('#step_'+step).removeClass('disabled').addClass('title').children('.elementMainTitle').addClass('collapse').removeClass('expand');
}

function konsertinaExpandingElement(step){
    $('#step_'+step).find('.explanation').slideDown();
}