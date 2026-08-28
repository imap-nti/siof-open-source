// JavaScript Document

//CONTADOR HAND NOTE
var i = 1;

$(document).ready(function() {

	//EXECUTA A FUNÇÃO HAND NOTE
	if($('#handNote_wrapper').length > 0){
		// setTimeout('handNote()',10000);
		handNote();
	}

});

//FUNÇÃO HAND NOTE
function handNote(){
	//seta o elemento pai
	var parent = $('#handNote_wrapper');

	$( $('#msg_'+i , $(parent))).fadeOut();
	i++;
	if(i < 6){
		$( $('#msg_'+i , $(parent))).delay(400).fadeIn();
		setTimeout('handNote()',10000);
	}else{
		i=0;
		handNote();
	}
}