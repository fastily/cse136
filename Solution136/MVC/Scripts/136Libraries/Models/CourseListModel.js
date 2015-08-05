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
}
