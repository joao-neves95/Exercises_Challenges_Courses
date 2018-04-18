$("document").ready(function(){


//Por defeito é -1, retorna todos os cartões
function GetCard(id = -1){
    $.ajax({
        url: "../json/cards.json",

    }).success().fail();
}

});