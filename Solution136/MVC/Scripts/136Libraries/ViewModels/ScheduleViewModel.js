function ScheduleViewModel() {
    var viewModel = null;
    var scheduleModelObj = new ScheduleModel();
    var self = this;
    var initialBind = true;
    var scheduleListViewModel = ko.observableArray();
    var DayListViewModel = ko.observableArray();
    var TimeListViewModel = ko.observableArray();


    var CreateViewModel = function () {
            // These are the initial options
        this.availableYears = ko.observableArray(['2015', '2016', '2017']);
        this.availableQuarters = ko.observableArray(['Fall', 'Winter', 'Spring', 'Summer Session 1', 'Summer Session 2', 'Special Summer Session']);
        this.availableSessions = ko.observableArray(['A00', 'B00']);
        this.availableTimes = ko.observableArray(['8:00 AM', '9:00AM']);
        //this.availableDays = ko.observableArray(['Mon/Wed/Fri', 'Tues/Thurs', 'Mon', 'Tues', 'Wed', 'Thurs', 'Fri']);
        this.availableInstructors = ko.observableArray(['Isaac Chu', 'Mia Minnes']);
        this.availableCourses = ko.observableArray(['CSE 105', 'CSE 134B', 'CSE 136']);

        var allQuarters = ["Winter", "Spring", "Summer 1", "Summer 2", "Fall"];
        var allYears = ["2015", "2016", "2017"];
        var allSessions = ["A00", "B00"];

        this.yearList = ko.observableArray(allYears);
        this.quarterList = ko.observableArray(allQuarters);
        this.sessionList = ko.observableArray(allSessions);
        this.scheduleTimeList = ko.observableArray([]);
        this.scheduleDayList = ko.observableArray([]);
        this.instructorList = ko.observableArray([]);

        this.newSchedule = {
            ScheduleId: ko.observable(),
            Year: ko.observable(""),
            Quarter: ko.observable(""),
            Session: ko.observable(""),
            ScheduleDay: {
                DayId: ko.observable(),
                Day: ko.observable("")
            },
            ScheduleTime: {
                TimeId: ko.observable(),
                Time: ko.observable("")
            },
            Instructor: {
                InstructorId: ko.observable(),
                FirstName: ko.observable(""),
                LastName: ko.observable(""),
                Title: ko.observable("")
            },
            Course: {
                CourseId: ko.observable(),
                Title: ko.observable(""),
                CourseLevel: ko.observable(""),
                Description: ko.observable("")
            },
        };

        this.AddNewCourseSchedule = AddNewCourseSchedule;
    };

    var AddNewCourseSchedule = function (data) {
        var courseScheduleData = {
            Year: viewModel.newCourse.Year(),
            Quarter: viewModel.newCourse.Quarter(),
            Session: viewModel.newCourse.Session(),
            Day: {
                ScheduleDayId: viewModel.newCourse.ScheduleDayId()
            },
            Time: {
                ScheduleTimeId: viewModel.newCourse.ScheduleTimeId()
            },
            Professor: {
                InstructorId: viewModel.newCourse.InstructorId()
            },
            Course: {
                CourseId: viewModel.newCourse.CourseId
            }
        };

        courseScheduleModelObj.InsertCourseSchedule(courseScheduleData, function (result) {
            if (result = 'ok') {
                LoadCourseSchedule("");
                viewModel.newCourse.CourseId("");
                viewModel.newCourse.Year("");
                viewModel.newCourse.Quarter("");
                viewModel.newCourse.Session("");
                viewModel.newCourse.ScheduleDayId("");
                viewModel.newCourse.ScheduleTimeId("");
                viewModel.newCourse.InstructorId("");
            }
            else {
                alert('Error occurs during Insert new Course Schedule!!');
            }
        });       
    };

    this.Initialize = function () {

        var tviewModel = {
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

        ko.applyBindings(tviewModel, document.getElementById("divSchedule"));
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
            viewModel.scheduleDayList(dayList);
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
            viewModel.scheduleTimeList(timeList);
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

    this.LoadCreateCourse = function () {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        this.GetDays();
        this.GetTimes();

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("divScheduleAdd"));
       
    };
}