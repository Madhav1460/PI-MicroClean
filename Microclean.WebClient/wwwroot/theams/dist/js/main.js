$(function () {
    $("#wizard").steps({
        headerTag: "h3",
        bodyTag: "section",
        transitionEffect: "fade",
        enableAllSteps: true,
        onStepChanging: function (event, currentIndex, newIndex) {
            if (newIndex === 1) {
                $("#page-link-2").addClass("active");
                $("#page-link-1").removeClass("active");
            } else {
                $('.wizard > .steps ul').removeClass('step-2');
            }
            if (newIndex === 2) {
                $('.wizard > .steps ul').addClass('step-3');
            } else {
                $('.wizard > .steps ul').removeClass('step-3');
            }
            return true;
        },
        onFinishing: function (event, currentIndex) {
            datapush();
        },
        labels: {
            finish: "Submit",
            next: "Continue",
            previous: "Back"
        }
    });
    // Custom Button Jquery Steps
    $('.forward').click(function () {
        $("#wizard").steps('next');
    })
    $('.backward').click(function () {
        $("#wizard").steps('previous');
    })
})