var bool = false;
$(function () {

    $('.the-button-light').hover(PlayTouch);

    $('.scroll li').hover(PlayTouch);

    $('.arrowUp').hover(Scroll);

    $('.arrowDown').hover(Scroll);

    $('.arrowUp').click(Scroll);

    $('.arrowDown').click(Scroll);

    $('.scroll').on('scroll', function () {
        var currentScroll = $(this).scrollTop();

        $('#nScroll').html(currentScroll);
    });

    $("a").tooltip();
    $("button").tooltip();

});


function PlayTouch() {

    // var rollSound = new Audio("sound/hover_ui.mp3");
    // rollSound.play();

    var audioElement = document.createElement('audio');
    audioElement.setAttribute('src', 'sound/hover_ui.mp3');
    audioElement.play();
}

function Scroll() {

    var id = $(this).attr('id');
    var currentScroll = $('#nScroll').html();

    if (!$('#' + id).hasClass("already")) {

        if (currentScroll > 0) {
            $('.scroll').animate({ scrollTop: '0' }, 150);
            $('#' + id).toggleClass('already');
        } else {
            $('.scroll').animate({ scrollTop: '200' }, 150);
            $('#' + id).toggleClass('already');
        }

    } else {
        if (id == "arrowDown") {
            $("#arrowUp").removeClass("already");
        }

        if (id == "arrowUp") {
            $("#arrowDown").removeClass("already");
        }
    }



    PlayTouch();
}
