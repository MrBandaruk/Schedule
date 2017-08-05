$(document).ready(function () {

    var now = new Date();

    function GetCalData() {
        var eventsData = [];
        $.ajax({
            url: 'Calendar/EventsData',
            dataType: "json",
            async: false,
            success: function (data) {
                eventsData = data.Events;
            }
        });

        return eventsData;
    };


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

        buttonText: {
            prev: "<",
            next: ">",
            prevYear: "<",
            nextYear: ">",
            today: "Сегодня",
            month: "Месяц",
            week: "Неделя",
            day: "День",
            listWeek: "Список"
        },

        slotLabelFormat: "HH:mm",
        titleFormat: 'D, MMMM, YYYY',
        businessHours: {

            dow: [1, 2, 3, 4, 5, 6],

            start: '06:00',
            end: '20:00',
        },

        minTime: "06:00",
        maxTime: "20:00",
        contentHeight: 685,
        columnFormat: 'dddd',
        //timeFormat: 'h:mm',
        timezone: 'local',
        eventColor: '#465775',

        defaultDate: now,
        navLinks: true, // can click day/week names to navigate views
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        events: GetCalData(),

        dayClick: function (date, jsEvent, view) {

            $.confirm({
                title: 'Добавить новое событие',
                content: '' +
                '<form id="eventData">' +
                '<div class="form-group">' +
                '<label>Название</label>' +
                '<input type="text" placeholder="Название события.." class="name form-control" name="Title" autocomplete="off" required />' +
                '<br />' +
                '<label>Описание</label>' +
                '<textarea placeholder="Добавьте описание.." name="Additional" class="info form-control" autocomplete="off" required /> </textarea>' +
                '<br />' +
                '<label>Начало</label>' +
                '<div class="input-group date" id="datetimepicker1"><input type="text" class="form-control startDate" /><span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span></div>' +
                //'<input type="datetime" value="' + formatDate(date._d) + '" class="startDate form-control" name="StartDate" required />' +
                '<br />' +
                '<label>Конец</label>' +
                '<div class="input-group date" id="datetimepicker2"><input type="text" class="form-control endDate" /><span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span></div>' +
                //'<input type="datetime" value="' + formatDate(date._d) + '" class="endDate form-control" name="EndDate" required />' +
                '</div>' +
                '</form>',

                onContentReady: function () {

                    $('#datetimepicker1').datetimepicker({
                        locale: 'ru',

                        format: 'DD.MM.YYYY HH:mm',
                        date: formatDate(date._d),

                    });

                    $('#datetimepicker2').datetimepicker({
                        locale: 'ru',
                        format: 'DD.MM.YYYY HH:mm',
                        date: formatDate(date._d),

                    });
                },

                buttons: {
                    formSubmit: {
                        text: 'Сохранить',
                        btnClass: 'btn-success',
                        action: function () {

                            $.ajax({
                                url: 'Calendar/CreateEvent',
                                data: {
                                    title: this.$content.find('.name').val(),
                                    additional: this.$content.find('.info').val(),
                                    startDate: $('.startDate').val(),
                                    endDate: $('.endDate').val()
                                },
                                dataType: "JSON",
                                success: function (data) {
                                    //var event = { id: data.item.Id, title: data.item.Title, start: toDateFromJson(data.item.EndDate) }
                                    //$('#calendar').fullCalendar('renderEvent', event, true);
                                    location.reload(); //перезагрузка страницы
                                }
                            });
                        }
                    },
                    cancel: {
                        text: 'Отмена'
                    },
                },
            });
        },

        eventClick: function (calEvent, jsEvent, view) {

            var item = [];

            var eventEnd;

            if (calEvent.end != null) {
                eventEnd = calEvent.end.format();
            } else {
                eventEnd = calEvent.start.format();
            }

            $.ajax({
                url: 'Calendar/EditEvent',
                data: { id: calEvent.id },
                dataType: "json",
                method: "GET",
                async: false,
                success: function (data) {
                    item = data.item;
                }
            });

            $.alert({
                title: item.Title,
                content: '' +
                    '<p>' + item.Additional + '</p>' +
                    '<p><b>Начало:</b><br/>' + formatDate(calEvent.start.format()) + '</p>' +
                    '<p><b>Конец:</b><br/>' + formatDate(eventEnd) + '</p>',
                buttons: {
                    Edit: function () {

                        $.confirm({
                            title: 'Редактировать',
                            content: '' +
                            '<form id="eventData">' +
                            '<div class="form-group">' +
                            '<label>Название</label>' +
                            '<input type="text" value="' + item.Title + '" class="name form-control" name="Title" autocomplete="off" required />' +
                            '<br />' +
                            '<label>Описание</label>' +
                            '<textarea name="Additional" class="info form-control" autocomplete="off" required >' + item.Additional + '</textarea>' +
                            '<br />' +
                            '<label>Начало</label>' +
                            '<div class="input-group date" id="datetimepicker1"><input type="text" class="form-control startDate" /><span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span></div>' +
                            //'<input type="date" value="' + formatDate(calEvent.start) + '" class="startDate form-control" name="StartDate" required />' +
                            '<br />' +
                            '<label>Конец</label>' +
                            '<div class="input-group date" id="datetimepicker2"><input type="text" class="form-control endDate" /><span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span></div>' +
                            //'<input type="date" value="' + formatDate(calEvent.start) + '" class="endDate form-control" name="EndDate" required />' +
                            '</div>' +
                            '</form>',

                            onContentReady: function () {

                                $('#datetimepicker1').datetimepicker({
                                    locale: 'ru',

                                    format: 'DD.MM.YYYY HH:mm',
                                    date: formatDate(calEvent.start),

                                });

                                $('#datetimepicker2').datetimepicker({
                                    locale: 'ru',
                                    format: 'DD.MM.YYYY HH:mm',
                                    date: formatDate(calEvent.start),

                                });
                            },


                            buttons: {
                                formSubmit: {
                                    text: 'Сохранить',
                                    btnClass: 'btn-success',
                                    action: function () {


                                        $.ajax({
                                            url: 'Calendar/EditEvent',
                                            method: "POST",
                                            data: {
                                                id: calEvent.id,
                                                title: this.$content.find('.name').val(),
                                                additional: this.$content.find('.info').val(),
                                                startDate: this.$content.find('.startDate').val(),
                                                endDate: this.$content.find('.endDate').val()
                                            },
                                            dataType: "JSON",
                                            success: function (data) {

                                                location.reload(); //перезагрузка страницы
                                            }
                                        });
                                    }
                                },
                                cancel: {},
                            },
                        });

                    },
                    delete: function () {
                        $.confirm({
                            title: 'Вы уверены?',
                            content: 'Вы уверены, что хотите удалить это событие?',
                            autoClose: 'cancel|9000',
                            theme: 'material',
                            type: 'red',
                            buttons: {
                                confirm: {
                                    text: 'Удалить',
                                    btnClass: 'btn-danger',
                                    keys: ['enter'],
                                    action: function () {
                                        $.ajax({
                                            url: 'Calendar/DeleteEvent',
                                            data: { id: item.Id },
                                            traditional: true,
                                            dataType: "json",
                                            success: function () {
                                                $('#calendar').fullCalendar('removeEvents', calEvent.id);
                                            }
                                        })
                                    }
                                },
                                cancel: {
                                    text: 'Отмена'
                                }
                            }
                        });
                    },
                    close: {
                        text: 'Закрыть'
                    }
                }
            });

        },


        eventDrop: function (event) {

            var eventEnd;

            if (event.end != null) {
                eventEnd = event.end.format();
            } else {
                eventEnd = event.start.format();
            }

            //if (confirm("Are you sure about this change?")) {                
            $.ajax({
                url: 'Calendar/DragEditEvent',
                method: "POST",
                data: {
                    id: event.id,
                    title: event.title,
                    start: event.start.format(),
                    end: eventEnd
                },
                dataType: "JSON",
                success: function (data) {
                    //revertFunc();
                    //location.reload(); //перезагрузка страницы
                }
            });
            //}

        },


        eventResize: function (event, delta, revertFunc) {
            //if (confirm("Are you sure about this change?")) {
            $.ajax({
                url: 'Calendar/DragEditEvent',
                method: "POST",
                data: {
                    id: event.id,
                    title: event.title,
                    start: event.start.format(),
                    end: event.end.format()
                },
                dataType: "JSON",
                success: function (data) {
                    //revertFunc();
                    //location.reload(); //перезагрузка страницы
                }
            });
            //}

        },

        timeFormat: 'H:mm'

    });
    
    function addZero(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    }

    var header = $(".fc-button-group").addClass("btn-group").removeClass("fc-button-group");
    header.children().addClass("btn").removeClass("fc-button fc-state-default fc-today-button fc-state-default fc-corner-left fc-corner-right fc-state-disabled");

    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = '' + d.getFullYear(),
            hours = '' + addZero(d.getHours()),
            minutes = '' + addZero(d.getMinutes());

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [day, month, year].join('.') + ' ' + [hours, minutes].join(':');
    };

    function toDateFromJson(src) {
        return new Date(parseInt(src.substr(6)));
    }
});