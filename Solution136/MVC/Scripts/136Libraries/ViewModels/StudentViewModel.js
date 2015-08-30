function StudentViewModel() {

    var StudentModelObj = new StudentModel();
    var self = this;
    //var initialBind = true;
    var studentListViewModel = ko.observableArray();

    // this.Initialize = function() {

    var viewModel = {
        id: ko.observable("A0000111"),
        first: ko.observable("Bruce"),
        last: ko.observable("Wayne"),
        email: ko.observable("bwayne@ucsd.edu"),
        password: ko.observable("password"),
        add: function (data) {
            self.CreateStudent(data);
        }
        //   };

        //  ko.applyBindings(viewModel, document.getElementById("divStudentContent"));
    };

    this.Initialize2 = function () {
        ko.applyBindings(viewModel, document.getElementById("divStudentContent"));
    };


    this.CreateStudent = function (data) {
        var model = {
            StudentId: data.id(),
            FirstName: data.first(),
            LastName: data.last(),
            Email: data.email(),
            Password: data.password()
        }

        StudentModelObj.Create(model, function (result) {
            if (result == "ok") {
                alert("Create student successful");
            } else {
                alert("Error occurred");
            }
        });

    };

    this.GetAll = function () {

        StudentModelObj.GetAll(function (studentList) {
            studentListViewModel.removeAll();

            for (var i = 0; i < studentList.length; i++) {
                studentListViewModel.push({
                    id: studentList[i].StudentId,
                    first: studentList[i].FirstName,
                    last: studentList[i].LastName,
                    email: studentList[i].Email
                });
            }

            if (initialBind) {
                ko.applyBindings({ viewModel: studentListViewModel }, document.getElementById("divStudentListContent"));
                initialBind = false; // this is to prevent binding multiple time because "Delete" functio calls GetAll again
            }
        });
    };

    this.GetDetail = function (id) {

        StudentModelObj.GetDetail(id, function (result) {

            var student = {
                id: ko.observable(result.StudentId),
                first: ko.observable(result.FirstName),
                last: ko.observable(result.LastName),
                email: ko.observable(result.Email),
                password: ko.oberervable(result.Password),
                update: function () {
                    self.UpdateStudent(this);
                }
            };

            ko.applyBindings(student, document.getElementById("divStudentContent"));
        });
    };

    this.UpdateStudent = function (viewModel) {
        // convert the viewModel to same structure as PLAdmin model (presentation layer model)
        var stduentData = {
            first: ko.observable(result.FirstName),
            last: ko.observable(result.LastName),
            email: ko.observable(result.email),
            password: ko.observable(result.password),
            id: result.Id,
        };

        StduentModelObj.Update(stduentData, function (message) {
            $('#divMessage').html(message);
        });

    };

    ko.bindingHandlers.DeleteStudent = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            $(element).click(function () {
                var id = viewModel.id;

                StudentModelObj.Delete(id, function (result) {
                    if (result != "ok") {
                        alert("Error occurred");
                    } else {
                        studentListViewModel.remove(viewModel);
                    }
                });
            });
        }
    }

}
