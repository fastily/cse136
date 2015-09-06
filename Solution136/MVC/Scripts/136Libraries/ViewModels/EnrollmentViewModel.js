function EnrollmentViewModel() {
    var enrollmentModelObj = new EnrollmentModel();
    var GradeChangeModelObj = new GradeChangeModel();
    var self = this;
    var viewModel = null;
    var initialBind = true;

    var CreateViewModel = function () {
        this.studentEnrollmentList = ko.observableArray([]);
        this.currentStudentEnrollmentList = ko.observableArray([]);

        this.ScheduleId = ko.observable();

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

        //this.gradeChange = {
        //    StudentId: student_id,
        //    id: ko.observable(1),
        //    course: ko.observable("CSE 3"),
        //    desired: ko.observable("A+"),
        //    reason: ko.observable("Reason for your request"),
        //    add: function (data) {
        //        self.AddGradeChange(data, student_id);
        //    }
        //};

    //    GradeChangeId: data.id(),
    //    Student_id: student_id,
    //    Schedule_id: 1, //data.Schedule_id,
    //    Approved: false,
    //    Course_id: "1", //data.Course_id,
    //    Desired: data.desired(),
    //    Description: data.reason()
    //}

    //GradeChangeModelObj.Create(model, function(result) {
    //    if (result == "ok") {
    //        alert("Create GradeChange successful");
    //    } else {
    //        alert("Error creating GradeChange occurred");
    //    }
    //});

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

    this.GetAll = function (studentId) {
        enrollmentModelObj.GetAllEnrollmentsByStudentId(studentId, function (enrollmentList) {
            viewModel.studentEnrollmentList(enrollmentList);
        });
    };

    this.GetStudentByScheduleId = function (scheduleId) {
        enrollmentModelObj.GetStudentsByScheduleId(scheduleId, function (enrollmentList) {
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

    this.LoadStudentsByScheduleId = function (scheduleId) {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        viewModel.ScheduleId(scheduleId);
        this.GetStudentByScheduleId(scheduleId);

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("divStudentEnrollments"));
    };

    this.LoadStudentEnrollmentsForGradeChange = function (studentId) {
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

    ko.bindingHandlers.DeleteStudentEnrollment = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var enrollment = {
                    StudentId: viewModel.StudentId,
                    ScheduleId: bindingContext.$parents[1].viewModel.ScheduleId
                };

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