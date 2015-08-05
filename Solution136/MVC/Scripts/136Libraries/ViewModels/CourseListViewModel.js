function CourseListViewModel() {
    this.Load = function () {
        var courseListModelObj = new CourseListModel();

        // Because the Load() is a async call (asynchronous), we'll need to use
        // the callback approach to handle the data after data is loaded.
        courseListModelObj.Load(function (courseListData) {

            // courseList - presentation layer model retrieved from /Course/GetCourseList route.
            // courseListViewModel - view model for the html content
            var courseListViewModel = new Array();

            // DTO from the JSON model to the view model. In this case, courseListViewModel doesn't need the "id" attribute
            for (var i = 0; i < courseListData.length; i++) {
                courseListViewModel[i] = { title: courseListData[i].Title, description: courseListData[i].Description };
            }

            // this is using knockoutjs to bind the viewModel and the view (Home/Index.cshtml)
            ko.applyBindings({ viewModel: courseListViewModel }, document.getElementById("divCourseListContent"));
        });
    };
}
