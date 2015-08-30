function ScheduleModel(asyncIndicator) {
    if (asyncIndicator == undefined) {
        asyncIndicator = true;
    }

    this.Create = function (schedule, instructorId, dayId, timeId, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Schedule/AddCourseToSchedule",
            data: {schedule: schedule, instructorId: instructorId, dayId: dayId, timeId: timeId},
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding Course to schedule.  Is your service layer running?');
            }
        });
    };

    this.Delete = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Schedule/DeleteCourseFromSchedule",
            data: id,
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
}
