$(document).ready(function () {
    $("paggingTable")
    .tablesorter({ widthFixed: true, widgets: ['zebra'] })
    .tablesorterPager({ container: $("#pager") });
});