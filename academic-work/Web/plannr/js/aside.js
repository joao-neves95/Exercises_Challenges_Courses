$(function(){
    $("#sidebar-hide").on("click", function(e){
        e.preventDefault();
        var $sidebar = $("#sidebar");
            //$sidebar.removeClass("s4 m3 l2").addClass("float-bar");
            $("span.text").hide();
            $("#planner").removeClass("s8 m9 l10").addClass("s12 m12 l12");
            $sidebar.animate({width:'toggle'},350);
            setTimeout(function(){
            $("#sidebar-show").removeClass("hidden").show()}, 300);
    })

    $("#sidebar-show").on("click", function(e){
        e.preventDefault();
        var $sidebar = $("#sidebar");
        //$sidebar.removeClass("float-bar").addClass("s4 m3 l2");
        $sidebar.animate({width:'toggle'},350);
        $("span.text").show();
        $("#planner").removeClass("s12 m12 l12").addClass("s8 m9 l10");
        $(this).hide();
    });
})