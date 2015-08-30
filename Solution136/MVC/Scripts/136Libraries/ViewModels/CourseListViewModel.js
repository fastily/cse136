function CourseListViewModel() {
    var courseModelObj = new CourseListModel();
    var self = this;
    var initialBind = true;
    var courseListViewModel = ko.observableArray();
    var coursePreReqListViewModel = ko.observableArray();

    this.Initialize = function () {

        var viewModel = {
            title: ko.observable("CSE 197"),
            level: ko.observable("Upper"),
            description: ko.observable("best course ever"),
            add: function (data) {
                self.CreateCourse(data);
            }
        };

        ko.applyBindings(viewModel, document.getElementById("divCourse"));
    };

    this.Load = function () {
        // Because the Load() is a async call (asynchronous), we'll need to use
        // the callback approach to handle the data after data is loaded.
        courseModelObj.Load(function (courseListData) {

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

    this.CreateCourse = function (data) {
        var model = {
            Title: data.title(),
            CourseLevel: data.level(),
            Description: data.description()
        }

        courseModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create Course successful");
            } else {
                alert("Error creating Course occurred");
            }
        });

    };

    this.GetDetail = function (id) {

        courseModelObj.GetDetail(id, function (result) {

            var course = {
                id: result.CourseId,
                title: ko.observable(result.Title),
                level: ko.observable(result.CourseLevel),
                description: ko.observable(result.Description),
                update: function () {
                    self.UpdateCourse(this);
                }
            };

            if (initialBind) {
                ko.applyBindings(course , document.getElementById("divCourseContent"));
            }
        });
    };

    this.GetAll = function () {

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

            if (initialBind) {
                ko.applyBindings({ viewModel: courseListViewModel }, document.getElementById("divCourseListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.UpdateCourse = function (course) {
        // convert the viewModel to same structure as PLAdmin model (presentation layer model)
        var courseData = {
            CourseId: course.id,
            Title: course.title(),
            CourseLevel: course.level(),
            Description: course.description()
        };

        courseModelObj.Update(courseData, function (message) {
            $('#divMessage').html(message);
        });

    };

    ko.bindingHandlers.DeleteCourse = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                courseModelObj.Delete(id, function (result) {
                    if (result != "ok") {
                        alert("Error Delting course occurred");
                    } else {
                        courseListViewModel.remove(viewModel);
                    }
                });
            });
        }
    };
}
