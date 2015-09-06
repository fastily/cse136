function EnrollmentViewModel() {
    var enrollmentModelObj = new EnrollmentModel();
    var self = this;
    var viewModel = null;
    var initialBind = true;

    var CreateViewModel = function () {
        this.studentEnrollmentList = ko.observableArray([]);
        this.currentStudentEnrollmentList = ko.observableArray([]);

        this.newEnrollment = {
            ScheduleId: ko.observable(),
            StudentId: ko.observable(""),
            Grade: ko.observable(""),
            EnrolledSchedule : {
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
            }          
        };

        //this.AddNewCourseSchedule = AddNewCourseSchedule;

    };

    //UpdateCourseFromSchedule = function (data) {
    //    var courseScheduleData = {
    //        ScheduleId: viewModel.newSchedule.ScheduleId(),
    //        Year: viewModel.newSchedule.Year(),
    //        Quarter: viewModel.newSchedule.Quarter(),
    //        Session: viewModel.newSchedule.Session(),
    //        Day: {
    //            DayId: viewModel.newSchedule.ScheduleDay.DayId()
    //        },
    //        Time: {
    //            TimeId: viewModel.newSchedule.ScheduleTime.TimeId()
    //        },
    //        Instructor: {
    //            InstructorId: viewModel.newSchedule.Instructor.InstructorId()
    //        },
    //        Course: {
    //            CourseId: viewModel.newSchedule.Course.CourseId
    //        }
    //    };

    //    scheduleModelObj.Update(courseScheduleData, function (result) {
    //        if (result = 'ok') {
    //            alert("success updating schedule");
    //        }
    //        else {
    //            alert('Error occurs during Insert new Course Schedule!!');
    //        }
    //    });       
    //};

    //AddNewCourseSchedule = function (data) {
    //    var courseScheduleData = {
    //        Year: viewModel.newSchedule.Year(),
    //        Quarter: viewModel.newSchedule.Quarter(),
    //        Session: viewModel.newSchedule.Session(),
    //        Day: {
    //            DayId: viewModel.newSchedule.ScheduleDay.DayId()
    //        },
    //        Time: {
    //            TimeId: viewModel.newSchedule.ScheduleTime.TimeId()
    //        },
    //        Instructor: {
    //            InstructorId: viewModel.newSchedule.Instructor.InstructorId()
    //        },
    //        Course: {
    //            CourseId: viewModel.newSchedule.Course.CourseId
    //        }
    //    };

    //    scheduleModelObj.Create(courseScheduleData, function (result) {
    //        if (result = 'ok') {
    //            alert("success updating schedule");
    //        }
    //        else {
    //            alert('Error occurs during Insert new Course Schedule!!');
    //        }
    //    });
    //};

    //this.Initialize = function () {

    //    var tviewModel = {
    //        id: ko.observable(1),
    //        year: ko.observable("2015"),
    //        quarter: ko.observable("winter"),
    //        session: ko.observable("B00"),
    //        course: ko.observable({
    //            id: ko.observable(1),
    //            title: ko.observable("CSE 103"),
    //            level: ko.observable(1),
    //            description: ko.observable("Best Course Ever")
    //        }),
    //        add: function (data) {
    //            self.CreateSchedule(data);
    //        }
    //    };

    //    ko.applyBindings(tviewModel, document.getElementById("divSchedule"));
    //};

    //this.CreateSchedule = function (data) {
    //    var model = {
    //        ScheduleId: data.id(),
    //        Year: data.year(),
    //        Quarter: data.quarter(),
    //        Session: data.session(),
    //        Course: {
    //            CourseId: data.course.id(),
    //            Title: data.course.title(),
    //            CourseLevel: data.course.level(),
    //            Description: data.course.description()
    //        }
    //    }

    //    scheduleModelObj.Create(model, function (result) {
    //        if (result == "ok") {
    //            alert("Create schedule was successful");
    //        } else {
    //            alert("Error creating schedule occurred");
    //        }
    //    });

    //};

    this.GetAll = function (studentId) {

        enrollmentModelObj.GetAllEnrollmentsByStudentId(studentId, function (enrollmentList) {
            //studentEnrollmentListViewModel.removeAll();

            //for (var i = 0; i < enrollmentList.length; i++) {
            //    studentEnrollmentListViewModel.push({
            //        ScheduleId: enrollmentList[i].ScheduleId,
            //        StudentId: enrollmentList[i].StudentId,
            //        Grade: enrollmentList[i].Grade,
            //        EnrolledSchedule: {
            //            ScheduleId: enrollmentList[i].EnrolledSchedule.ScheduleId,
            //            Year: enrollmentList[i].EnrolledSchedule.Year,
            //            Quarter: enrollmentList[i].EnrolledSchedule.Quarter,
            //            Session: enrollmentList[i].EnrolledSchedule.Session,
            //            Instructor: {
            //                InstructorId: enrollmentList[i].EnrolledSchedule.Instructor.InstructorId,
            //                FirstName: enrollmentList[i].EnrolledSchedule.Instructor.FirstName,
            //                LastName: enrollmentList[i].EnrolledSchedule.Instructor.LastName,
            //            },
            //            Course: {
            //                CourseId: enrollmentList[i].EnrolledSchedule.Course.CourseId,
            //                Title: enrollmentList[i].EnrolledSchedule.Course.Title,
            //            }
            //        }
            //    });
            //}
            viewModel.studentEnrollmentList(enrollmentList);
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

    this.LoadStudentEnrollments = function (studentId) {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        viewModel.newEnrollment.StudentId = ko.observable(studentId);
        this.GetAll(studentId);

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("divStudentEnrollments"));
    };

    ko.bindingHandlers.DeleteEnrollment = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var enrollment = viewModel;

                enrollmentModelObj.RemoveEnrollment(enrollment, function (result) {
                    if (result != "ok") {
                        alert("Error Delting schedule occurred");
                    } else {
                        bindingContext.$parents[1].viewModel.studentEnrollmentList.remove(viewModel);
                    }
                });
            });
        }
    };
}