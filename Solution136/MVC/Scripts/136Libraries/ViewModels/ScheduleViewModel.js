﻿function ScheduleViewModel() {
    var scheduleModelObj = new ScheduleModel();
    var self = this;
    var initialBind = true;
    var scheduleListViewModel = ko.observableArray();
    var DayListViewModel = ko.observableArray();
    var TimeListViewModel = ko.observableArray();

    this.Initialize = function () {

        var viewModel = {
            id: ko.observable(1),
            year: ko.observable("2015"),
            quarter: ko.observable("winter"),
            session: ko.observable("B00"),
            course: ko.observable({
                id: ko.observable(1),
                title: ko.observable("CSE 103"),
                level: ko.observable(1),
                description: ko.observable("Best Course Ever")
            }),
            add: function (data) {
                self.CreateSchedule(data);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divSchedule"));
    };

    this.CreateSchedule = function (data) {
        var model = {
            ScheduleId: data.id(),
            Year: data.year(),
            Quarter: data.quarter(),
            Session: data.session(),
            Course: {
                CourseId: data.course.id(),
                Title: data.course.title(),
                CourseLevel: data.course.level(),
                Description: data.course.description()
            }
        }

        scheduleModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create schedule was successful");
            } else {
                alert("Error creating schedule occurred");
            }
        });

    };

    this.GetAll = function (year, quarter) {

        scheduleModelObj.GetAll(year, quarter, function (scheduleList) {
            scheduleListViewModel.removeAll();

            for (var i = 0; i < scheduleList.length; i++) {
                scheduleListViewModel.push({
                    id: scheduleList[i].ScheduleId,
                    year: scheduleList[i].Year,
                    quarter: scheduleList[i].Quarter,
                    session: scheduleList[i].Session,
                    day: {
                        name: scheduleList[i].Day.Day,
                    },
                    time: {
                        name: scheduleList[i].Time.Time
                    },
                    instructor: {
                        id: scheduleList[i].Instructor.InstructorId,
                        name: scheduleList[i].Instructor.FirstName + scheduleList[i].Instructor.LastName,
                    },
                    course: {
                        id: scheduleList[i].Course.CourseId,
                        title: scheduleList[i].Course.Title,
                        level: scheduleList[i].Course.CourseLevel,
                        description: scheduleList[i].Course.Description
                    }
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: scheduleListViewModel }, document.getElementById("divScheduleListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetScheduleById = function (id) {

        scheduleModelObj.GetScheduleById(id, function (result) {

            var schedule = {
                id: result.ScheduleId,
                year: result.Year,
                quarter: result.Quarter,
                session: result.Session,
                day: {
                    name: ko.observable(result.Day.Day),
                },
                time: {
                    name: ko.observable(result.Time.Time)
                },
                instructor: {
                    id: result.Instructor.InstructorId,
                    first: ko.observable(result.Instructor.FirstName),
                    last: ko.observable(result.Instructor.LastName),
                    name: result.Instructor.FirstName + " " + result.Instructor.LastName
                },
                course: {
                    id: result.Course.CourseId,
                    title: result.Course.Title,
                    level: result.Course.CourseLevel,
                    description: result.Course.Description
                }
            };
            if (initialBind) {
                ko.applyBindings(schedule, document.getElementById("divScheduleContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetAllMin = function () {

        scheduleModelObj.GetAllMin(function (scheduleList) {
            scheduleListViewModel.removeAll();

            for (var i = 0; i < scheduleList.length; i++) {
                scheduleListViewModel.push({
                    year: scheduleList[i].Year,
                    quarter: scheduleList[i].Quarter
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: scheduleListViewModel }, document.getElementById("divScheduleListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetStudentScheduleMin = function (id) {

        scheduleModelObj.GetStudentScheduleMin(id, function (scheduleList) {
            scheduleListViewModel.removeAll();

            for (var i = 0; i < scheduleList.length; i++) {
                scheduleListViewModel.push({
                    year: scheduleList[i].Year,
                    quarter: scheduleList[i].Quarter
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: scheduleListViewModel }, document.getElementById("divScheduleListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetDays = function () {

        scheduleModelObj.GetDays(function (dayList) {
            DayListViewModel.removeAll();

            for (var i = 0; i < dayList.length; i++) {
                DayListViewModel.push({
                    id: dayList[i].DayId,
                    day: dayList[i].Day
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: dayList }, document.getElementById("dayDropdown"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetTimes = function () {

        scheduleModelObj.GetTimes(function (timeList) {
            TimeListViewModel.removeAll();

            for (var i = 0; i < timeList.length; i++) {
                TimeListViewModel.push({
                    id: timeList[i].TimeId,
                    time: timeList[i].Time
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: timeList }, document.getElementById("timeDropdown"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    ko.bindingHandlers.DeleteSchedule = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                courseModelObj.Delete(id, function (result) {
                    if (result != "ok") {
                        alert("Error Delting schedule occurred");
                    } else {
                        scheduleListViewModel.remove(viewModel);
                    }
                });
            });
        }
    };
}