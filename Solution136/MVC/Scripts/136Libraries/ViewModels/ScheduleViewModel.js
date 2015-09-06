function ScheduleViewModel() {
    var viewModel = null;
    var scheduleModelObj = new ScheduleModel();
    var courseModelObj = new CourseListModel();
    var InstructorModelObj = new InstructorModel();
    var EnrollmentModelObj = new EnrollmentModel();
    var self = this;
    var initialBind = true;
    var scheduleListViewModel = ko.observableArray();
    var DayListViewModel = ko.observableArray();
    var TimeListViewModel = ko.observableArray();
    var courseListViewModel = ko.observableArray();
    var instructorListViewModel = ko.observableArray();

    var CreateViewModel = function () {
        var allQuarters = ["Winter", "Spring", "Summer 1", "Summer 2", "Fall"];
        var allYears = ["2015", "2016", "2017"];
        var allSessions = ["A00", "B00"];

        this.yearList = ko.observableArray(allYears);
        this.quarterList = ko.observableArray(allQuarters);
        this.sessionList = ko.observableArray(allSessions);
        this.scheduleTimeList = ko.observableArray([]);
        this.scheduleDayList = ko.observableArray([]);
        this.scheduleInstructorList = ko.observableArray([]);
        this.scheduleCourseList = ko.observableArray([]);

        this.studentScheduleList = ko.observableArray([]);
        this.scheduleList = ko.observableArray([]);

        this.studentId = ko.observable("");

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
            }
        };

        this.AddNewCourseSchedule = AddNewCourseSchedule;
        this.UpdateCourseFromSchedule = UpdateCourseFromSchedule;
    };

    UpdateCourseFromSchedule = function (data) {
        var courseScheduleData = {
            ScheduleId: viewModel.newSchedule.ScheduleId(),
            Year: viewModel.newSchedule.Year(),
            Quarter: viewModel.newSchedule.Quarter(),
            Session: viewModel.newSchedule.Session(),
            Day: {
                DayId: viewModel.newSchedule.ScheduleDay.DayId()
            },
            Time: {
                TimeId: viewModel.newSchedule.ScheduleTime.TimeId()
            },
            Instructor: {
                InstructorId: viewModel.newSchedule.Instructor.InstructorId()
            },
            Course: {
                CourseId: viewModel.newSchedule.Course.CourseId
            }
        };

        scheduleModelObj.Update(courseScheduleData, function (result) {
            if (result = 'ok') {
                alert("success updating schedule");
            }
            else {
                alert('Error occurs during Insert new Course Schedule!!');
            }
        });       
    };

    AddNewCourseSchedule = function (data) {
        var courseScheduleData = {
            Year: viewModel.newSchedule.Year(),
            Quarter: viewModel.newSchedule.Quarter(),
            Session: viewModel.newSchedule.Session(),
            Day: {
                DayId: viewModel.newSchedule.ScheduleDay.DayId()
            },
            Time: {
                TimeId: viewModel.newSchedule.ScheduleTime.TimeId()
            },
            Instructor: {
                InstructorId: viewModel.newSchedule.Instructor.InstructorId()
            },
            Course: {
                CourseId: viewModel.newSchedule.Course.CourseId
            }
        };

        scheduleModelObj.Create(courseScheduleData, function (result) {
            if (result = 'ok') {
                alert("success updating schedule");
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

    this.GetAllForStudent = function (year, quarter) {
        scheduleModelObj.GetAll(year, quarter, function (scheduleList) {
            viewModel.scheduleList(scheduleList);
        });
    };

    this.GetScheduleById = function (id) {
        scheduleModelObj.GetScheduleById(id, function (result) {
            viewModel.newSchedule.ScheduleId(result.ScheduleId);
            viewModel.newSchedule.Session(result.Session);
            viewModel.newSchedule.ScheduleDay.Day(result.Day.Day);
            viewModel.newSchedule.ScheduleDay.DayId(result.Day.DayId);
            viewModel.newSchedule.ScheduleTime.Time(result.Time.Time);
            viewModel.newSchedule.ScheduleTime.TimeId(result.Time.TimeId);
            viewModel.newSchedule.Instructor.InstructorId(result.Instructor.InstructorId);
            viewModel.newSchedule.Instructor.FirstName(result.Instructor.FirstName);
            viewModel.newSchedule.Instructor.LastName(result.Instructor.LastName);      

            viewModel.newSchedule.Course.CourseId(result.Course.CourseId);
            viewModel.newSchedule.Course.Title(result.Course.Title);
            viewModel.newSchedule.Course.CourseLevel(result.Course.Level);
            viewModel.newSchedule.Course.Description(result.Course.Description);
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

    this.GetCourses = function () {

        courseModelObj.GetAll(function (courseList) {
            courseListViewModel.removeAll();

            for (var i = 0; i < courseList.length; i++) {
                courseListViewModel.push({
                    id: courseList[i].CourseId,
                    title: courseList[i].Title,
                    level: courseList[i].CourseLevel,
                    description: courseList[i].Description
                });
            }
            viewModel.scheduleCourseList(courseList);
        });
    };

    this.GetInstructors = function () {

        InstructorModelObj.GetAll(function (instructorList) {
            instructorListViewModel.removeAll();

            for (var i = 0; i < instructorList.length; i++) {
                instructorListViewModel.push({
                    id: instructorList[i].InstructorId,
                    first: instructorList[i].FirstName,
                    last: instructorList[i].LastName,
                    title: instructorList[i].Title
                });
            }
            viewModel.scheduleInstructorList(instructorList);
        });
    };

    this.GetStudentSchedule = function (sid, year, quarter) {
        EnrollmentModelObj.GetAllEnrollments(sid, year, quarter, function (enrolledList) {      
            viewModel.studentScheduleList(enrolledList);
        });
    };

    this.LoadCreateCourse = function () {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        this.GetDays();
        this.GetTimes();
        this.GetCourses();
        this.GetInstructors();

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("divScheduleAdd"));
       
    };

    this.LoadAddCourse = function (year, quarter, courseId) {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        viewModel.newSchedule.Year = ko.observable(year);
        viewModel.newSchedule.Quarter = ko.observable(quarter);
        viewModel.newSchedule.Course.CourseId = ko.observable(courseId);
        this.GetDays();
        this.GetTimes();
        this.GetInstructors();

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("divScheduleAddCourse"));
    };

    this.LoadEditCourse = function (year, quarter, courseId) {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        viewModel.newSchedule.Year = ko.observable(year);
        viewModel.newSchedule.Quarter = ko.observable(quarter);
        viewModel.newSchedule.Course.CourseId = ko.observable(courseId);
        this.GetDays();
        this.GetTimes();
        this.GetInstructors();
        this.GetScheduleById(courseId);

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("divScheduleEditCourse"));
    };

    this.LoadStudentEnrollments = function (year, quarter, studentId) {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        viewModel.newSchedule.Year = ko.observable(year);
        viewModel.newSchedule.Quarter = ko.observable(quarter);
        viewModel.studentId(studentId);

        this.GetAllForStudent(year, quarter);
        this.GetStudentSchedule(studentId, year, quarter);

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("StudentAddCourseToSchedule"));
    };

    ko.bindingHandlers.DeleteSchedule = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                scheduleModelObj.Delete(id, function (result) {
                    if (result != "ok") {
                        alert("Error Delting schedule occurred");
                    } else {
                        scheduleListViewModel.remove(viewModel);
                    }
                });
            });
        }
    };

    ko.bindingHandlers.DeleteWholeSchedule = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var year = viewModel.year;
                var quarter = viewModel.quarter;

                scheduleModelObj.DeleteAllFromSchedule(year, quarter, function (result) {
                    if (result != "ok") {
                        alert("Error Deleting schedule occurred");
                    } else {
                        scheduleListViewModel.remove(viewModel);
                    }
                });
            });
        }
    };

    ko.bindingHandlers.AddCourseToStudent = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var enrollment = {
                    ScheduleId: viewModel.ScheduleId,
                    StudentId: bindingContext.$parents[1].viewModel.studentId(),
                    Grade: ko.observable(""),
                    EnrolledSchedule: {
                        ScheduleId: viewModel.ScheduleId,
                        Year: viewModel.Year,
                        Quarter: viewModel.Quarter,
                        Session: viewModel.Session,
                        Day: {
                            DayId: viewModel.Day.DayId,
                            Day: viewModel.Day.Day
                        },
                        Time: {
                            TimeId: viewModel.Time.TimeId,
                            Time: viewModel.Time.Time
                        },
                        Instructor: {
                            InstructorId: viewModel.Instructor.InstructorId,
                            FirstName: viewModel.Instructor.FirstName,
                            LastName: viewModel.Instructor.LastName
                        },
                        Course: {
                            CourseId: viewModel.Course.CourseId,
                            Title: viewModel.Course.Title,
                            Description: viewModel.Course.Description
                        }
                    }
                };         

                EnrollmentModelObj.CreateEnrollment(enrollment, function (result) {
                    if (result = 'ok') {
                        alert("success adding enrollment");
                        bindingContext.$parents[1].viewModel.studentScheduleList.push(enrollment);
                    }
                    else {
                        alert('Error occurs during add enrollment');
                    }
                });
            });
        }
    };

    ko.bindingHandlers.RemoveCourseFromSchedule = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                EnrollmentModelObj.RemoveEnrollment(viewModel, function (result) {
                    if (result = 'ok') {
                        alert("success deleting enrollment");
                        bindingContext.$parents[1].viewModel.studentScheduleList.remove(viewModel);
                    }
                    else {
                        alert('Error occurs during delete enrollment');
                    }
                });
            });
        }
    }; 
}