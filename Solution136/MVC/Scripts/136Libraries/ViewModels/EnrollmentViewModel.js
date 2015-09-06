function EnrollmentViewModel() {
    var enrollmentModelObj = new EnrollmentModel();
    var GradeChangeModelObj = new GradeChangeModel();
    var StudentModelObj = new StudentModel();

    var self = this;
    var viewModel = null;
    var initialBind = true;

    var CreateViewModel = function () {
        this.studentEnrollmentList = ko.observableArray([]);
        this.currentStudentEnrollmentList = ko.observableArray([]);
        this.studentList = ko.observableArray([]);

        this.ScheduleId = ko.observable();
        this.StudentId = ko.observable("");

        this.enrollmentUpdate = {
            ScheduleId: ko.observable(),
            StudentId: ko.observable(""),
            Grade: ko.observable(""),
        }

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

        this.EnrollStudent = EnrollStudent;

        this.UpdateEnrollment = UpdateEnrollment;
    };

    EnrollStudent = function (data) {
        var enrollment = {
            StudentId: viewModel.StudentId,
            ScheduleId: viewModel.ScheduleId
        };

        var student = {};

        StudentModelObj.GetDetail(viewModel.StudentId(), function (result) {
            student.StudentId = result.StudentId;
            student.FirstName = result.FirstName;
            student.LastName = result.LastName;
            student.Email = result.Email;
            student.Grade = "";
        });

        enrollmentModelObj.CreateEnrollment(enrollment, function (result) {
            if (result == 'ok') {
                viewModel.studentEnrollmentList.push(student);
            }
            else {
                alert('Error occurs during Insert new Course Schedule!!');
            }
        });
    };

    this.GetAllStudents = function () {
        StudentModelObj.GetAll(function (studentList) {
            viewModel.studentList(studentList);
        });
    };

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
        this.GetAllStudents();
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

    this.LoadUpdateGrade = function (studentId, scheduleId) {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        viewModel.newEnrollment.StudentId = ko.observable(studentId);
        viewModel.enrollmentUpdate.StudentId = ko.observable(studentId);
        viewModel.enrollmentUpdate.ScheduleId = ko.observable(scheduleId);
        this.GetDetail(studentId, scheduleId);

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("UpdateEnrollment"));
    };

    this.GetDetail = function (studentId, scheduleId) {
        enrollmentModelObj.GetEnrollmentDetail(studentId, scheduleId, function (result) {
            viewModel.enrollmentUpdate.Grade = ko.observable(result.Grade);
        });
    };

    UpdateEnrollment = function () {
        var enrollment = {
            ScheduleId: viewModel.enrollmentUpdate.ScheduleId,
            StudentId: viewModel.enrollmentUpdate.StudentId,
            Grade: viewModel.enrollmentUpdate.Grade
        }
        enrollmentModelObj.UpdateEnrollment(enrollment, function (result) {
            if (result == 'ok') {
                alert("updated enrollment successfully");
            }
            else {
                alert('Error occurs during update grade');
            }
        });
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