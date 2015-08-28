function CourseListModel() {

    this.Load = function (callback) {
        $.ajax({
            url: "http://localhost:9393/Api/Course/GetCourseList",
            data: "",
            dataType: "json",
            success: function (courseListData) {
                callback(courseListData);
            },
            error: function () {
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };

    this.Create = function (course, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Course/InsertCourse",
            data: course,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while adding Course.  Is your service layer running?');
            }
        });
    };

    this.Update = function (course, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Course/UpdateCourse",
            data: course,
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while updating course.  Is your service layer running?');
            }
        });
    };

    this.Delete = function (id, callback) {
        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: "http://localhost:9393/Api/Course/DeleteCourse?id=" + id,
            data: '',
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while deleteing course.  Is your service layer running?');
            }
        });
    };

    this.GetAll = function (callback) {
        var url = "http://localhost:9393/Api/Course/GetCourseList";
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
                alert('Error while loading course list.  Is your service layer running?');
            }
        });
    };

    this.GetDetail = function (id, callback) {
        var url = "http://localhost:9393/Api/Course/GetCourse?id=" + id + "&bust=" + new Date();

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
                alert('Error while loading Course detail.  Is your service layer running?');
            }
        });
    };

    this.AssignPreReq = function (cid, cpid, callback) {
        var url = "http://localhost:9393/Api/Course/AssignPreReq";

        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: url,
            data: { courseId: cid, preReqCourseId: cpid },
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while assigning preReq.  Is your service layer running?');
            }
        });
    };

    this.RemovePreReq = function (cid, cpid, callback) {
        var url = "http://localhost:9393/Api/Course/RemovePreReq";

        $.ajax({
            async: asyncIndicator,
            method: "POST",
            url: url,
            data: { courseId: cid, preReqCourseId: cpid },
            dataType: "json",
            success: function (result) {
                callback(result);
            },
            error: function () {
                alert('Error while removing preReq.  Is your service layer running?');
            }
        });
    };

    this.GetPreReqList = function (id, callback) {
        var url = "http://localhost:9393/Api/Course/GetPreReqList?courseId=" + id;

        $.ajax({
            method: "GET",
            url: url,
            data: "",
            dataType: "json",
            success: function (coursePreReqListData) {
                callback(coursePreReqListData);
            },
            error: function () {
                alert('Error while loading course PreReq list.  Is your service layer running?');
            }
        });
    };
}
