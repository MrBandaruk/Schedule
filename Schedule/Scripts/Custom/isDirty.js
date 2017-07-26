var isDirty = false;
var linkClicked = false;

    $("#btn-save").click(function () {
        linkClicked = true;
    });

    $(":input").change(function () {
        isDirty = true;
    });

    $("#article").change(function () {
        isDirty = true;
    });


    $(window).on('beforeunload ', function () {
        if (isDirty && !linkClicked) {
            return 'Are you sure?';
        } else return;
    });


