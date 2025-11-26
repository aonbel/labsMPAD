$(document).on("click", "#pager-nav a", function (event) {
    event.preventDefault();
    let url = $(this).attr("href");
    $("#games-menu").load(url);
    console.log(url);
})
