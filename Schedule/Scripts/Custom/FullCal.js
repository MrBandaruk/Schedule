$(document).ready(function () {

    var now = new Date();
    var eventsData = [];

    function GetCalData() {
        $.ajax({
            url: 'EventsData',
            dataType: "json",
            async: false,
            success: function (data) {
                eventsData = data.Events;
            }
        });
    };


    GetCalData();

    $('#calendar').fullCalendar({
        schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source',
        firstDay: 1,
        monthNames: ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
        monthNamesShort: ['Янв', 'Фев', 'Мрт', 'Апр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек'],
        dayNames: ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота'],
        dayNamesShort: ['Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб'],
        
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay,listWeek'
        },

        slotLabelFormat: "HH:mm",
        titleFormat: 'D, MMMM, YYYY',
        businessHours: {
            
            dow: [ 1, 2, 3, 4, 5, 6 ], 

            start: '06:00', 
            end: '20:00', 
        },

        minTime: "06:00",
        maxTime: "20:00",
        contentHeight: 685,
        columnFormat: 'dddd',
        timezone: 'local',

        defaultDate: now,
        navLinks: true, // can click day/week names to navigate views
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        events: eventsData,
        //[
        //    {
        //        title: 'All Day Event',
        //        start: '2017-07-01'
        //    },
        //    {
        //        title: 'Long Event',
        //        start: '2017-07-07',
        //        end: '2017-07-10'
        //    },
        //    {
        //        id: 999,
        //        title: 'Repeating Event',
        //        start: '2017-07-09T16:00:00'
        //    },
        //    {
        //        id: 999,
        //        title: 'Repeating Event',
        //        start: '2017-07-16T16:00:00'
        //    },
        //    {
        //        title: 'Conference',
        //        start: '2017-07-11',
        //        end: '2017-07-13'
        //    },
        //    {
        //        title: 'Meeting',
        //        start: '2017-07-12T10:30:00',
        //        end: '2017-07-12T12:30:00'
        //    },
        //    {
        //        title: 'Lunch',
        //        start: '2017-07-12T12:00:00'
        //    },
        //    {
        //        title: 'Meeting',
        //        start: '2017-07-12T14:30:00'
        //    },
        //    {
        //        title: 'Happy Hour',
        //        start: '2017-07-12T17:30:00'
        //    },
        //    {
        //        title: 'Dinner',
        //        start: '2017-07-12T20:00:00'
        //    },
        //    {
        //        title: 'Birthday Party',
        //        start: '2017-07-13T07:00:00'
        //    },
        //    {
        //        title: 'Click for Google',
        //        url: 'http://google.com/',
        //        start: '2017-07-28'
        //    }
        //],

        dayClick: function(date, jsEvent, view) {

            //alert('Clicked on: ' + date.format());

            $.confirm({
                title: 'Add new event.',
                content: '' +
                '<form id="eventData">' +
                '<div class="form-group">' +
                '<label>Title</label>' +
                '<input type="text" placeholder="Event title.." class="name form-control" name="Title" autocomplete="off" required />' +
                '<br />' +
                '<label>Additional information</label>' +               
                '<textarea placeholder="Add some information about event" name="Additional" class="info form-control" autocomplete="off" required /> </textarea>' +
                '<br />' +
                '<label>Start date</label>' +
                '<input type="date" value="' + formatDate(date._d) + '" class="startDate form-control" name="StartDate" required />' +
                '<br />' +
                '<label>End date</label>' +
                 '<input type="date" value="' + formatDate(date._d) + '" class="endDate form-control" name="EndDate" required />' +
                '</div>' + 
                '</form>',

                buttons: {
                    formSubmit: {
                        text: 'Save',
                        btnClass: 'btn-success',
                        action: function () {

                            //var formData = $('eventData').serialize();
                            var titleF = this.$content.find('.name').val();
                            var additionalF = this.$content.find('.info').val();
                            var startDateF = this.$content.find('.startDate').val();
                            var endDateF = this.$content.find('.endDate').val();
                            //var fDate = JSON.stringify($('#eventData').serialize());

                            $.ajax({
                                url: 'CreateEvent',
                                data: { title: titleF, additional: additionalF, startDate: startDateF, endDate: endDateF},
                                dataType: "JSON",
                                success: function (data) {
                                    var event = { id: data.item.Id, title: data.item.Title, start: toDateFromJson(data.item.EndDate) }
                                    $('#calendar').fullCalendar('renderEvent', event, true);
                                    //$('#calendar').fullCalendar('refetchEvents');
                                }
                            });                            
                        }                            
                    },                  
                    cancel: {}, 
            },
          });
        },

        eventClick: function(calEvent, jsEvent, view) {

            var item = [];

            $.ajax({
                url: 'EditEvent',
                data: {id: calEvent.id},
                dataType: "json",
                method: "GET",
                async: false,
                success: function (data) {
                    item = data.item;
                }
            });

            $.alert({
                title: item.Title,
                content: item.Additional,
                buttons: {
                    Edit: function () {
                        $.confirm({
                            title: 'Edit event.',
                            content: '' +
                            '<form id="eventData">' +
                            '<div class="form-group">' +
                            '<label>Title</label>' +
                            '<input type="text" value="'+ item.Title + '" class="name form-control" name="Title" autocomplete="off" required />' +
                            '<br />' +
                            '<label>Additional information</label>' +
                            '<textarea name="Additional" class="info form-control" autocomplete="off" required >'+ item.Additional +'</textarea>' +
                            '<br />' +
                            '<label>Start date</label>' +
                            '<input type="date" value="' + '" class="startDate form-control" name="StartDate" required />' +
                            '<br />' +
                            '<label>End date</label>' +
                             '<input type="date" value="' + '" class="endDate form-control" name="EndDate" required />' +
                            '</div>' +
                            '</form>',

                            buttons: {
                                formSubmit: {
                                    text: 'Save',
                                    btnClass: 'btn-success',
                                    action: function () {

                                        //var formData = $('eventData').serialize();
                                        var titleF = this.$content.find('.name').val();
                                        var additionalF = this.$content.find('.info').val();
                                        var startDateF = this.$content.find('.startDate').val();
                                        var endDateF = this.$content.find('.endDate').val();
                                        //var fDate = JSON.stringify($('#eventData').serialize());

                                        //$.ajax({
                                        //    url: 'CreateEvent',
                                        //    data: { title: titleF, additional: additionalF, startDate: startDateF, endDate: endDateF },
                                        //    dataType: "JSON",
                                        //    success: function (data) {
                                        //        var event = { id: data.item.Id, title: data.item.Title, start: toDateFromJson(data.item.EndDate) }
                                        //        $('#calendar').fullCalendar('renderEvent', event, true);
                                        //    }
                                        //});
                                    }
                                },
                                cancel: {},
                            },
                        });
                        
                    },
                    delte: {},
                    cancel: {}
                    }
            });

    }


    });


    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    };

    function toDateFromJson(src) {
        return new Date(parseInt(src.substr(6)));
    }
});