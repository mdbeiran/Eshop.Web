$(document).ready(function() {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 200) {
            $('#scroll').fadeIn();
        } else {
            $('#scroll').fadeOut();
        }
    });
    $('#scroll').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 1000);
        return false;
    });
});

// copy this at the top of the page
// <a href="#" id="scroll" style="display: none;"><span></span></a>