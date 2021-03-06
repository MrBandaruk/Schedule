﻿$(document).ready(function () {

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
        monthNames: [
            'Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь',
            'Декабрь'
        ],
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
        //contentHeight: $(window).height() * 0.80,
        viewRender: function(view, element) {
            if (view.name === "month" || view.name === "listWeek") {
                $('#calendar').fullCalendar('option', 'contentHeight', $(window).height() * 0.80);
            } else {
                $('#calendar').fullCalendar('option', 'contentHeight', "auto");
            }
        },       
        columnFormat: 'dddd',
        //timeFormat: 'h:mm',
        timezone: 'local',
        //eventColor: '#465775',

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
                '<label>Цвет:</label>' +
                '<select class="form-control" id="color">' +
                '<option>Стандартный</option>' +
                '<option>Зеленый</option>' +
                '<option>Оранжевый</option>' +
                '<option>Красный</option>' +
                '</select>' +
                '<br />' +
                '<label>Начало</label>' +
                '<div class="input-group date" id="datetimepicker1"><input type="text" class="form-control startDate" /><span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span></div>' +
                '<br />' +
                '<label>Конец</label>' +
                '<div class="input-group date" id="datetimepicker2"><input type="text" class="form-control endDate" /><span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span></div>' +
                '</div>' +
                '</form>',

                onContentReady: function () {

                    $('#datetimepicker1').datetimepicker({
                        locale: 'ru',

                        format: 'DD.MM.YYYY HH:mm',
                        //date: date._d,
                        
                        widgetPositioning: {
                            horizontal: 'auto',
                            vertical: 'top'
                        }
                    });

                    $('#datetimepicker2').datetimepicker({
                        locale: 'ru',
                        format: 'DD.MM.YYYY HH:mm',
                        //date: date._d,
                        
                        widgetPositioning: {
                            horizontal: 'auto',
                            vertical: 'top'
                        }
                    });

                    $('#datetimepicker1').data("DateTimePicker").date(moment(date._d).add(3, 'hours').format('DD.MM.YYYY HH:mm'));
                    $('#datetimepicker2').data("DateTimePicker").date(moment(date._d).add(4, 'hours').format('DD.MM.YYYY HH:mm'));

                },

                buttons: {
                    formSubmit: {
                        text: 'Сохранить',
                        btnClass: 'btn-success',
                        action: function () {

                            $.ajax({
                                url: 'Calendar/CreateEvent',
                                data: {
                                    title: $('.name').val(),
                                    additional: $('.info').val(),
                                    startDate: formatDate($("#datetimepicker1").data('DateTimePicker').date()),
                            //$('.startDate').val()),
                                    endDate: formatDate($("#datetimepicker2").data('DateTimePicker').date()),
                            //$('.endDate').val()),
                                    color: $('#color').val()
                                },
                                dataType: "JSON",
                                success: function (data) {
                                    //var event = { id: data.item.Id, title: data.item.Title, start: toDateFromJson(data.item.EndDate) }
                                	//$('#calendar').fullCalendar('renderEvent', event, true);
                                	location.reload(); //перезагрузка страницы
                                },
                                error: function (data) {
                                	alertify.error('All fields are required!');
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
                type: item.Color,
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
                            '<label>Цвет:</label>' +
                            '<select class="form-control" id="color">' +
                            '<option>Стандартный</option>' +
                            '<option>Зеленый</option>' +
                            '<option>Оранжевый</option>' +
                            '<option>Красный</option>' +
                            '</select>' +
                            '<br />' +
                            '<label>Начало</label>' +
                            '<div class="input-group date" id="datetimepicker1"><input type="text" class="form-control startDate" /><span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span></div>' +
                            '<br />' +
                            '<label>Конец</label>' +
                            '<div class="input-group date" id="datetimepicker2"><input type="text" class="form-control endDate" /><span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span></div>' +
                            '</div>' +
                            '</form>',

                            onContentReady: function () {

                                $('#datetimepicker1').datetimepicker({
                                    locale: 'ru',
                                    
                                    date: calEvent.start.format(),
                                    format: 'DD.MM.YYYY HH:mm',
                                    widgetPositioning: {
                                        horizontal: 'auto',
                                        vertical: 'top'
                                    }
                                });

                                $('#datetimepicker2').datetimepicker({
                                    locale: 'ru',
                                   
                                    date: calEvent.start.format(),
                                    format: 'DD.MM.YYYY HH:mm',
                                    widgetPositioning: {
                                        horizontal: 'auto',
                                        vertical: 'top'
                                    }
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
                                                title: $('.name').val(),
                                                additional: $('.info').val(),
                                                startDate: formatDate($("#datetimepicker1").data('DateTimePicker').date()),
                                                endDate: formatDate($("#datetimepicker2").data('DateTimePicker').date()),
                                                color: $('#color').val()
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
                    end: eventEnd,
                    color: event.color
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
                    end: event.end.format(),
                    color: event.color
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


    var header = $(".fc-button-group").addClass("btn-group").removeClass("fc-button-group");
    header.children().addClass("btn").removeClass("fc-button fc-state-default fc-today-button fc-state-default fc-corner-left fc-corner-right fc-state-disabled");



    //Меняет местами месяц и дату, тк DateTime в моделе имеет формат MM.DD.YYYY, а я вывожу формат DD.MM.YYYY.
    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = '' + d.getFullYear(),
            hours = '' + d.getHours(),
            minutes = '' + d.getMinutes();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;
        if (hours < 10) hours = '0' + hours;
        if (minutes < 10) minutes = '0' + minutes;

        var foo = [[month, day, year].join('.'), [hours, minutes].join(':')].join(' ');
        return foo;
    };

    function toDateFromJson(src) {
        return new Date(parseInt(src.substr(6)));
    }
});