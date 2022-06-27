$(document).ready(function () {

//console.log("Hello, from OpenERP");


var listItems = $(".list li");
listItems.on("click", function () {
    console.log("You clicked on " + $(this).text());
});

var $loginFormArea = $(".loginFormArea");
var $pageTitle = $(".pageTitle");

$pageTitle.on("click", function () {
    //$popUpForm.toggle(); //if hidden, show. if shown, hide.
    $loginFormArea.slideToggle(200); //as above, but animated.
});

});