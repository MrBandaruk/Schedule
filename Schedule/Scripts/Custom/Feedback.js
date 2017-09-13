/*
function FeedbackSendMessage() {
    document.getElementById('frmFeedback').action =
        'mailto:nbandaruk@gmail.com' + '?subject=' + encodeURIComponent(document.getElementById('tbName').value)
        + '&body=' + encodeURIComponent(document.getElementById('tbMessage').value);
    return false;
} */

$(document).ready(function () {


    $("#btnSend").click(function () {
    	var mail = $("#tbName").val();
    	var message = $("#tbMessage").val();
    	if (((mail.length != 0) && (mail.indexOf('@') == 1)) && (message.length != 0)) {
    		var letter = 'mailto:nbandaruk@gmail.com' + '?subject=' + mail + '&body=' + message;
    		$("#frmFeedback").attr('action', letter);
    	} else {
    		//$("#frmFeedback").append("<p color=\"red\">Invalid email or message</p>");
    		alert("Invalid email or message!");
    	};
    });
});

