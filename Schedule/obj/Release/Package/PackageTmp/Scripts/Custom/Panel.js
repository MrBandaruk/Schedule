$(document).ready(function () {
    


    var selected = [];
    $("#btn-delete").prop("disabled", true);
    $("#btn-delete").css("color", "grey");


    //dataTables
    table = $('#myTable').DataTable({
        "bServerSide": true,
        "sAjaxSource": "dataTablesData",
        "lengthMenu": [[5, 10, 15, -1], [5, 10, 15, "All"]],
        columns: [
            {
              "className": 'details-control',
              "orderable": false,
              "data": null,
              "defaultContent": ''
            },
            { data: 'Id' },
            { data: 'ShortTitle' },
            { data: 'ShortArticle' },
            { data: 'Id' },
            { data: 'Id' },
            { data: 'FullTitle' },
            { data: 'FullArticle' },
            
        ],
        "columnDefs": [
            {
                "targets": [5],
                "visible": false,
                "searchable": false
            },

            {
                "targets": [1],
                "searchable": false,
                "sortable": false
            },

            {
                "targets": [4],
                "searchable": false,
                "sortable": false
            },
            {
                "targets": [6, 7],
                "visible": false,
                "searchable": false,
                "sortable": false
            },

            ],

        "order": [[5, "desc"]],


        "createdRow": function (row, data, index) {
            $('td:eq(1)', row).html('<a class="btn btn-default" href="Edit/' + data.Id + '"><span class="glyphicon glyphicon-edit"></span></button>');
            $('td:eq(4)', row).html('<button class="btn btn-default" onclick="btnDelete(' + data.Id + ')"><span class="glyphicon glyphicon-trash"></span></button>');
        },
    });



    //click to select
    $('#myTable tbody').on('click', 'tr', function (e) {

        if (e.ctrlKey) {
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
                $("#btn-delete").hover(function () {
                    $(this).css("color", "white");
                });
            } else {
                $("#btn-delete").prop("disabled", true);
                $("#btn-delete").css("color", "grey");
                $("#btn-delete").hover(function () {
                    $(this).css("color", "grey");
                });
            }
        }
    });

    function imgIds(Nid) {
        var content = "";

        $.ajax({
            url: 'GetImagesIdByNewsId',
            data: { id: Nid },
            dataType: "json",
            async: false,
            success: function (data) {
                for (var i = 0; i < data.images.length; i++) {
                    content += '<div><img style="height: 250px; width: 373px" src="GetImageById/';
                    content += data.images[i].Id;
                    content += '" class="img-responsive"/></div>';
                    
                };
                //alert(content);
            }
        });

        return content;
    };


    function format(data) {

        var htmlText = imgIds(data.Id);

        return '<div class="mySlick" style="width: 1120px; ">' +
                    htmlText +
               '</div>' +
             '<p>' + data.FullTitle + '</p>' +
             '<p>' + data.FullArticle + '</p>'
    }

    $('#myTable tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
            //$('#mySlick').slick('unslick');

        }
        else {
            row.child(format(row.data())).show();
            tr.addClass('shown');
        $('.mySlick').slick({
            accessibility: 'true',
            arrows: 'true',
            slidesToShow: 3,
            slidesToScroll: 1
        });



        }


    });




    //DeleteMany confirmation
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
                cancel: {
                    action: function () {
                        $("#myTable tbody tr").removeClass("selected");
                        selected = [];
                        $("#btn-delete").prop("disabled", true);
                        $("#btn-delete").css("color", "grey");
                        $("#btn-delete").hover(function () {
                            $(this).css("color", "grey");
                        });
                    }
                }
            }
        });
    });


});


//Delete confirmation
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