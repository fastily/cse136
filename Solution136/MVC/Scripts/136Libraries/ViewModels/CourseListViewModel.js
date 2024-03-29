﻿function CourseListViewModel() {
    var viewModel = null;
    var courseModelObj = new CourseListModel();
    var self = this;
    var initialBind = true;
    var courseListViewModel = ko.observableArray();
    var currentPreReqListViewModel = ko.observableArray();
    var coursePreReqListViewModel = ko.observableArray();

    var PreReqModelObj = new CourseListModel();
    var studentModelObj = new StudentModel();


    var CreateViewModel = function () {
        this.currentPreReqList = ko.observableArray([]);
        this.coursePreReqList = ko.observableArray([]);
        this.coursesList = ko.observableArray([]);

        this.viewCourse = {
            id: ko.observable(),
            title: ko.observable(""),
            level: ko.observable(""),
            description: ko.observable("")
        };

        this.addCourseAsPreReq = {
            CourseId: ko.observable(),
            Title: ko.observable(""),
            CourseLevel: ko.observable(""),
            Description: ko.observable("")
        };

        this.AddNewPreReq = AddNewPreReq;
        this.RemovePreReq = RemovePreReq;
    };

    AddNewPreReq = function (data) {
        courseModelObj.AssignPreReq(viewModel.viewCourse.id(), data.CourseId, function (result) {
            if (result = 'ok') {
                viewModel.currentPreReqList.push(data)
                alert("success adding pre req");
            }
            else {
                alert('Error occurs during add preReq');
            }
        });
    };

    RemovePreReq = function (data) {
        courseModelObj.RemovePreReq(viewModel.viewCourse.id, data.CourseId, function (result) {
            if (result = 'ok') {
                alert("succes");
                viewModel.currentPreReqList.remove(data)
            }
            else {
                alert('Error occurs during delete preReq');
            }
        });
    };

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
            Level: data.level(),
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
                level: ko.observable(result.Level),
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

    this.GetDetailForPreReq = function (id) {

        courseModelObj.GetDetail(id, function (result) {
            viewModel.viewCourse.id(result.CourseId);
            viewModel.viewCourse.title(result.Title);
            viewModel.viewCourse.level(result.Level);
            viewModel.viewCourse.description(result.Description);
        });
    };

    this.GetAll = function () {

        courseModelObj.GetAll(function (courseList) {
            courseListViewModel.removeAll();

            for (var i = 0; i < courseList.length; i++) {
                courseListViewModel.push({
                    id: courseList[i].CourseId,
                    title: courseList[i].Title,
                    level: courseList[i].Level,
                    description: courseList[i].Description,
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: courseListViewModel }, document.getElementById("divCourseListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetAllCoursesForPreReq = function () {

        courseModelObj.GetAll(function (courseList) {
            courseListViewModel.removeAll();

            for (var i = 0; i < courseList.length; i++) {
                courseListViewModel.push({
                    id: courseList[i].CourseId,
                    title: courseList[i].Title,
                    level: courseList[i].Level,
                    description: courseList[i].Description,
                });
            }
            viewModel.coursesList(courseList);
        });
    };

    this.GetAllPreReqs = function (courseId) {

        courseModelObj.GetPreReqList(courseId, function (coursePreReqList) {
            currentPreReqListViewModel.removeAll();

            for (var i = 0; i < coursePreReqList.length; i++) {
                currentPreReqListViewModel.push({
                    id: coursePreReqList[i].CourseId,
                    title: coursePreReqList[i].Title,
                    level: coursePreReqList[i].Level,
                    description: coursePreReqList[i].Description,
                });
            }
            viewModel.currentPreReqList(coursePreReqList);
        });
    };

    this.UpdateCourse = function (course) {
        // convert the viewModel to same structure as PLAdmin model (presentation layer model)
        var courseData = {
            CourseId: course.id,
            Title: course.title(),
            Level: course.level(),
            Description: course.description()
        };

        courseModelObj.Update(courseData, function (message) {
            $('#divMessage').html(message);
        });

    };

    this.LoadPreReq = function (id) {
        if (viewModel == null) {
            viewModel = new CreateViewModel();
        }

        this.GetAllCoursesForPreReq();
        this.GetAllPreReqs(id);
        this.GetDetailForPreReq(id);

        ko.applyBindings({ viewModel: viewModel }, document.getElementById("divPreReqManagement"));
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




    var MakeViewModel = function () {
        this.capeList = ko.observableArray([]);
        this.prereqListResult = ko.observableArray([]);
        this.UpdatePreReqFromSchedule = UpdatePreReqFromSchedule;
        this.StudentId = ko.observable("");
    };

    this.LoadPreReqScheduleList = function (studentId) {
        if (viewModel == null) {
            viewModel = new MakeViewModel();
        }

        viewModel.StudentId(studentId);
        this.GetPreReqScheduleList();
        ko.applyBindings({ viewModel: viewModel }, document.getElementById("divPreReqListContent"));
    }


    this.GetPreReqScheduleList = function () {
        PreReqModelObj.GetSome(function (prereqResult) {
            viewModel.prereqListResult(prereqResult);
        });
    };

    UpdatePreReqFromSchedule = function (data) {
        var pr = {
            StudentId: viewModel.StudentId(),
            ScheduleId: data.ScheduleId
        };
        
        studentModelObj.RequestPreReqOverrride(pr, function (result) {
            if (result == 'ok') {
                alert("success requesting PreREq");
            }
            else {
                alert('Error occurs during requesting PreREq');
            }
        });       
    };
}
