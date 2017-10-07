$(document).ready(function () {
	var shortTitleChars = $(".char:eq( 0 )");
	var fullTitleChars = $(".char:eq( 1 )");
	var shortArticleChars = $(".char:eq( 2 )");

	shortTitleChars.text(60 - $("#tdShortTitle").val().length + " left");
	fullTitleChars.text(256 - $("#tdFullTitle").val().length + " left");
	shortArticleChars.text(410 - $("#tdShortArticle").val().length + " left"); 

	Counter(shortTitleChars, 60, $("#tdShortTitle"));
	Counter(fullTitleChars, 256, $("#tdFullTitle"))
	Counter(shortArticleChars, 410, $("#tdShortArticle"))
});

function Counter(chars, maxChars, input) {
	$(input).on("keydown keyup focus keypress", function () {
		var len = $(this).val().length;
		len = maxChars - len;
		if (!len) {
			$(this).val($(this).val().substr(0, maxChars - 1));
			
			$(chars).css('color', 'red').text("You have reached the maximum number of characters.");
		} else {
			$(chars).css('color', 'gray').text(len + " left");
		}
		
	});
}