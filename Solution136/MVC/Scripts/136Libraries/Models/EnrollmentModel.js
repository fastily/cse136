﻿function EnrollmentModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.CreateEnrollment = function (enrollment, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Enrollment/AddEnrollment",
            data: enrollment,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding Course to schedule.  Is your service layer running?');
            }
        });
    };

    this.RemoveEnrollment = function (enrollment, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Enrollment/RemoveEnrollment",
            data: enrollment,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding Course to schedule.  Is your service layer running?');
            }
        });
    };

    this.GetAllEnrollmentsByStudentId = function (studentId, callback) {
        var url = "http://localhost:9393/Api/Enrollment/GetAllStudentEnrolledSchedules?studentId=" + studentId;
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: url,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading schedule list.  Is your service layer running?');
            }
        });
    };

    this.GetAllEnrollments = function (studentId, year, quarter, callback) {
        var url = "http://localhost:9393/Api/Enrollment/GetStudentEnrolledSchedulesByQuarter?studentId=" + studentId + '&year=' + year + '&quarter=' + quarter;
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: url,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading schedule list.  Is your service layer running?');
            }
        });
    };

    this.GetStudentsByScheduleId = function (scheduleId, callback) {
        var url = "http://localhost:9393/Api/Enrollment/GetStudentsByScheduleId?scheduleId=" + scheduleId;
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: url,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading schedule list.  Is your service layer running?');
            }
        });
    };

    this.UpdateEnrollment = function (er, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Enrollment/UpdateEnrollment",
            data: er,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding Course to schedule.  Is your service layer running?');
            }
        });
    };
   
    this.GetEnrollmentDetail = function (studentId, scheduleId, callback) {
        var url = "http://localhost:9393/Api/Enrollment/GetEnrollmentDetail?studentId=" + studentId +'&scheduleId=' + scheduleId;
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: url,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while loading schedule list.  Is your service layer running?');
            }
        });
    };
}
