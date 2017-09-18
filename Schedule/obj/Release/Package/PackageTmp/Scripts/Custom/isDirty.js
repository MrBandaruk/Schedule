var isDirty = false;
var linkClicked = false;

    $("#btn-save").click(function () {
        linkClicked = true;
    });

    $(":input").change(function () {
        isDirty = true;
    });
 
    $(window).on('beforeunload ', function (e) {

        if (isDirty && !linkClicked) {
            return 'Are you sure?';
        } else return;

    });



