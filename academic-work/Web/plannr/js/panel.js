$(function(){

    $(".blackout").on("click", function(){
        $(this).fadeOut(300);//hides the element when clicked
    });

    //Stopts the blackout from hidding when there is a click inside the panel(which is inside the blackout)
    $(".panel").on("click", function(e){
        e.stopPropagation();
    });
});