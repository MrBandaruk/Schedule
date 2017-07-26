$(document).ready(function () {

    table = $('#myTable').DataTable({
        "bServerSide": true,
        "sAjaxSource": "dataTablesData",

        columns: [
            { data: 'Id' },
            { data: 'FullTitle' },
            { data: 'FullArticle' }
        ],
        "order": [[0, "desc"]],

        select: {
            style: 'single'
        },

        "createdRow": function (row, data, index) {
            $('td:eq(0)', row).html(data.Id);
        }

    });


    $('#btn-delete').click(function () {
        var agreement = confirm('Are you sure, you want to delete this article?');
        var data = table.row('.selected').data().Id;
        if (agreement) {
            $.ajax({
                url: 'Delete/' + data,
                success: function () {
                    table.rows($('#myTable tr.active')).remove().draw(false);
                }
            });
        }
    });


    $('#btn-edit').click(function () {
        var data = table.row('.selected').data().Id;
        $.redirect("Edit", { id: data }, "GET");
    });

});