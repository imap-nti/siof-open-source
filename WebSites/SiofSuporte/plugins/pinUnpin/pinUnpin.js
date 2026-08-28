/************************************************************************************************************
*
*   PIN UNPIN E MODAL
*
*   autor:Thiago Teles Pereira
*   criado em: 19/03/2012
*   modificado em: 28/01/2013
*   versão: 2.0
*
************************************************************************************************************/
/************************
*
* PIN UNPIN
*
*************************/
function pinUnpin(){
    //ADD BUTTON
    $('.unpin_content').append('<a class="bt_unpin" href="javascript:void(0)" rel="nofollow">Maximixar</a>');

    // PIN
    $('.bt_unpin').live('click' , function(){
        $(this).parent().wrap('<div class="unpin_container_on"></div>');
        $(this).html('minimizar').addClass('bt_pin').removeClass('bt_unpin');
        $('.unpin_container_on').animate({
            opacity: 1
        },1000);
    })

    // UNPIN
    $('.bt_pin').live('click', function(){
        $('.unpin_container_on').animate({
            opacity: 0
        }, 1000, function(){
            $('.unpin_content').unwrap();
        });
        $(this).html('maximixar').addClass('bt_unpin').removeClass('bt_pin');
    })

    $(document).live('keydown',function(e){
        if(e.which == 27){
            $('.unpin_container_on').animate({
                opacity: 0
            }, 1000, function(){
                $('.unpin_content').unwrap();
            });
            $('.bt_pin').html('maximixar').addClass('bt_unpin').removeClass('bt_pin');
        }
    });
}


/************************
*
*   MODAL
*
*************************/
function modal(contentModal, containerContent, closable){

    //function closeModal
    function closeModal(){
        $('.closable').slideUp('normal', function(){
            $('.closable').parent().fadeOut('normal', function(){
                $(this).remove();
            });
        });
    }


    if((containerContent == '') || (containerContent == undefined) || (containerContent == false)){
        containerContent = '';
    }else{
        containerContent = 'on';
    }

    if((closable == '') || (closable == undefined) || (closable == false)){
        closable = '';
    }else{
        closable = 'closable';
    }

    $(document).live('keydown',function(e){
        if(e.which == 27){
            closeModal();
        }
    });

    template_overlay = '<div class="modal_overlay"><div class="containerContent'+' ' + containerContent +' ' + closable + '">'+ contentModal +'</div></div>';

    $('body').prepend(template_overlay);
}
