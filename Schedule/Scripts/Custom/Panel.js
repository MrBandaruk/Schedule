$(document).ready(function () {
    var selected = [];
    $("#btn-delete").prop("disabled", true);
    $("#btn-delete").css("color", "black");

    table = $('#myTable').DataTable({
        "bServerSide": true,
        "sAjaxSource": "dataTablesData",
        "lengthMenu": [[5, 10, 15, -1], [5, 10, 15, "All"]],
        columns: [
            { data: 'Id' },
            { data: 'FullTitle' },
            { data: 'FullArticle' },
            { data: 'Id' },
            { data: 'Id' },
            
        ],
        "columnDefs": [
            {
                "targets": [ 4 ],
                "visible": false,
                "searchable": false
            },

            {
                "targets": [0],
                "searchable": false,
                "sortable": false
            },

            {
                "targets": [3],
                "searchable": false,
                "sortable": false
            },

            ],

        "order": [[4, "desc"]],


        //select: true,

        "createdRow": function (row, data, index) {
            $('td:eq(0)', row).html('<a class="btn btn-default" href="Edit/' + data.Id + '"><span class="glyphicon glyphicon-edit"></span></button>');
            $('td:eq(3)', row).html('<button class="btn btn-default" onclick="btnDelete(' + data.Id + ')"><span class="glyphicon glyphicon-trash"></span></button>');
        },



    });

    $('#myTable tbody').on('click', 'tr', function () {

        $(this).toggleClass('selected');

        var index = selected.indexOf(table.row(this).data().Id);
        if (index == -1) {
            selected.push(table.row(this).data().Id);
        } else {
            selected.splice(index, 1);
        }

        if (selected.length != 0) {
            $("#btn-delete").prop("disabled", false);
            $("#btn-delete").css("color", "white");
        } else {
            $("#btn-delete").prop("disabled", true);
            $("#btn-delete").css("color", "black");
        }
        
    });

    $('#btn-delete').click(function () {

        $.confirm({
            title: 'Are you sure?',
            content: 'Are you sure, you want to delete '+ selected.length +' articles?',
            autoClose: 'cancel|9000',
            theme: 'material',
            type: 'red',
            buttons: {
                confirm: {
                    text: 'Delete',
                    btnClass: 'btn-danger',
                    keys: ['enter'],                   
                    action: function () {
                            //$.redirect("DeleteMany", { id: selected }, "GET");
                            $.ajax({
                                url: 'DeleteMany',
                                data: { id: selected },
                                traditional: true, 
                                dataType: "json",
                                success: function () {
                                    table.rows($('#myTable tr.active')).remove().draw(false);
                                }
                            })

                                          
                    }
                },
                cancel: {}
            }
        });
    });

});

function btnDelete(id) {
    $.confirm({
        title: 'Are you sure?',
        content: 'Are you sure, you want to delete this article?',
        autoClose: 'cancel|9000',
        theme: 'material',
        type: 'red',
        buttons: {
            confirm: {
                text: 'Delete',
                btnClass: 'btn-danger',
                keys: ['enter'],
                action: function () {
                    $.ajax({
                        url: 'Delete/' + id,
                        success: function () {
                            table.rows($('#myTable tr.active')).remove().draw(false);
                        }
                    })
                }
            },
            cancel: {}
        }
    });
};