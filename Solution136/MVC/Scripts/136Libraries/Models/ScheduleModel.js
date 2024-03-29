﻿function ScheduleModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.Create = function (schedule, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Schedule/AddCourseToSchedule",
            data: schedule,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding Course to schedule.  Is your service layer running?');
            }
        });
    };

    this.Update = function (schedule, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Schedule/UpdateCourseFromSchedule",
            data: schedule,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding Course to schedule.  Is your service layer running?');
            }
        });
    };


    this.DeleteAllFromSchedule = function (year, quarter, callback) {
        var schedule = {
            quarter: quarter,
            year: year
        };
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Schedule/DeleteAllFromSchedule",
            data: schedule,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while deleteing course from schedule.  Is your service layer running?');
            }
        });
    };

    this.Delete = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "GET",
            url: "http://localhost:9393/Api/Schedule/DeleteCourseFromSchedule?id=" + id,
            data: "",
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while deleteing course from schedule.  Is your service layer running?');
            }
        });
    };

    this.GetAll = function (year, quarter, callback) {
        var url = "http://localhost:9393/Api/Schedule/GetScheduleList?year=" + year + "&quarter=" + quarter;
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

    this.GetScheduleById = function (id, callback) {
        var url = "http://localhost:9393/Api/Schedule/GetScheduleById?id=" + id;
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

    this.GetAllMin = function (callback) {
        var url = "http://localhost:9393/Api/Schedule/GetScheduleListMin";
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
                alert('Error while loading schedule list min.  Is your service layer running?');
            }
        });
    };

    this.GetStudentScheduleMin = function (id, callback) {
        var url = "http://localhost:9393/Api/Schedule/GetStudentScheduleListMin?id=" + id;
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
                alert('Error while loading schedule list min.  Is your service layer running?');
            }
        });
    };

    this.GetDays = function (callback) {
        var url = "http://localhost:9393/Api/Schedule/GetDays";
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
                alert('Error while loading day list.  Is your service layer running?');
            }
        });
    };

    this.GetTimes = function (callback) {
        var url = "http://localhost:9393/Api/Schedule/GetTimes";
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
                alert('Error while loading time list.  Is your service layer running?');
            }
        });
    };

    this.GetInstructorSchedule = function (instructorId, callback) {
        var url = "http://localhost:9393/Api/Schedule/GetInstructorSchedule?instructorId=" + instructorId;
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
                alert('Error while loading schedule list min.  Is your service layer running?');
            }
        });
    };

    this.GetTaBySchedule = function (scheduleId, callback) {
        var url = "http://localhost:9393/Api/Schedule/GetTaBySchedule?instructorId=" + scheduleId;
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
                alert('Error while loading schedule list min.  Is your service layer running?');
            }
        });
    };
}
