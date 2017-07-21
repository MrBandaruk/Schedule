/*
function FeedbackSendMessage() {
    document.getElementById('frmFeedback').action =
        'mailto:nbandaruk@gmail.com' + '?subject=' + encodeURIComponent(document.getElementById('tbName').value)
        + '&body=' + encodeURIComponent(document.getElementById('tbMessage').value);
    return false;
} */

$(document).ready(function () {
    $("#btnSend").click(function () {
       // var name = $("#tbName").val();
       // var body = $('#tbMessage').val();
        var letter = 'mailto:nbandaruk@gmail.com' + '?subject=' + $("#tbName").val() + '&body=' + $('#tbMessage').val();
        $("#frmFeedback").attr('action', letter);
    });
});

