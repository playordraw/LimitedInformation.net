$('.rating').mousemove(function (e)
{
    var x = e.pageX - $(this).offset().left;
    $('.rating').css('width', (x + 1) + 'px');
});
